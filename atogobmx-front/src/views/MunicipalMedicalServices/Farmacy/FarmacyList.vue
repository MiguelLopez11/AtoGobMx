<template>
  <b-card class="m-2">
    <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
  </b-card>
  <b-card class="m-2">
    <b-row align-h="end" class="mb-3 mr-1">
      <b-form-input
        size="lg"
        style="width: 350px; margin-right: 15px"
        v-model="searchValue"
        type="search"
        placeholder="Buscar..."
      >
      </b-form-input>
    </b-row>
    <EasyDataTable
      rows-per-page-message="registros por pagina"
      empty-message="No se encuentran registros"
      table-class-name="customize-table"
      buttons-pagination
      border-cell
      :loading="isloading"
      :headers="fields"
      :items="prescriptions"
      :rows-per-page="5"
      :search-field="searchField"
      :search-value="searchValue"
      theme-color="#7367f0"
    >
      <template #header-actions="header">
        {{ header.text }}
      </template>
      <template #item-empleados="items">
        <span v-if="items.empleados !== null">
          {{ items.empleados.nombreCompleto }}
        </span>
      </template>
      <template #item-actions="items">
        <b-dropdown
          id="ActionsDropdown"
          size="lg"
          style="text-color: black"
          variant="link"
          toggle-class="text-decoration-none"
          dropright
          no-caret
        >
          <template #button-content>
            <i class="bi bi-three-dots-vertical"></i>
          </template>
          <b-dropdown-item
            class="m-1"
            variant="outline-warning"
            @click="onClickModal(items.recetaId)"
          >
            <i class="bi bi-bell-fill" />
            Surtir Receta
          </b-dropdown-item>
          <b-dropdown-item
            class="m-1"
            variant="outline-warning"
            :to="{
              name: 'ServiciosMedicos-Receta-Edit',
              params: { RecetaId: items.recetaId }
            }"
          >
            <i class="bi bi-pencil-square" />
            Editar Receta
          </b-dropdown-item>
          <b-dropdown-item
            @click="RemovePrescription(items.recetaId)"
            class="m-1"
            variant="outline-danger"
          >
            <i class="bi bi-trash3"> Archivar </i>
          </b-dropdown-item>
        </b-dropdown>
      </template>
      <template #item-status="items">
        <b-badge
          :variant="
            items.estatusReceta.nombre === 'Pendiente' ? 'warning' : 'success'
          "
        >
          {{ items.estatusReceta.nombre }}
        </b-badge>
      </template>
    </EasyDataTable>
    <b-modal
      v-model="showModal"
      title="Surtir receta"
      cancel-title="Cancelar"
      ok-title="Aceptar"
      size="lg"
      @Ok="onFillPrescription"
    >
      <h3>Receta:</h3>
      <b-table-simple responsive>
        <b-thead>
          <b-tr>
            <b-th colspan="2" variant="light">Producto</b-th>
            <b-th colspan="2" variant="light">Contenido</b-th>
            <b-th colspan="2" variant="light">Cantidad</b-th>
            <b-th colspan="2" variant="light">Cantidad disponible</b-th>
          </b-tr>
        </b-thead>
        <b-tbody>
          <b-tr
            v-for="data in prescriptionSelected"
            :key="data.productoRecetaId"
          >
            <b-td colspan="2">{{ data.producto.nombre }}</b-td>
            <b-td colspan="2">{{ data.producto.contenido }}</b-td>
            <b-td colspan="2">{{ data.cantidad }}</b-td>
            <b-td colspan="2">
              {{ data.producto.cantidadDisponible }}
              <b-badge
                :variant="
                  data.producto.cantidadDisponible === 0
                    ? 'danger'
                    : '' || data.producto.cantidadDisponible < 5
                    ? 'warning'
                    : '' || data.producto.cantidadDisponible > 5
                    ? 'success'
                    : ''
                "
              >
                <span align="center">
                  {{
                    data.producto.cantidadDisponible === 0
                      ? 'Sin existencia'
                      : '' || data.producto.cantidadDisponible < 5
                      ? 'Poca existencia'
                      : '' || data.producto.cantidadDisponible > 5
                      ? 'En existencia'
                      : ''
                  }}
                </span>
              </b-badge>
            </b-td>
          </b-tr>
        </b-tbody>
      </b-table-simple>
    </b-modal>
  </b-card>
</template>

<script>
import PrescriptionServices from '@/Services/prescription.Services'
import { ref, inject } from 'vue'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    EasyDataTable: window['vue3-easy-data-table']
  },
  setup () {
    const swal = inject('$swal')
    const {
      getPrescriptionsPending,
      getProductsPrescriptionByRecetaId,
      deletePrescription,
      fillPrescription
    } = PrescriptionServices()
    const prescriptions = ref([])
    const prescriptionSelected = ref([])
    const employees = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('marca')
    const showModal = ref(false)
    const recetaId = ref()
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      {
        text: 'Servicios medicos',
        to: '/ServiciosMedicosMunicipales'
      },
      { text: 'Farmacia' }
    ])

    const prescriptionFields = ref({
      recetaId: 0,
      empleadoId: null,
      diagnostico: '',
      archivado: false
    })
    const weaponsFieldsBlank = ref(
      JSON.parse(JSON.stringify(prescriptionFields))
    )
    const fields = ref([
      // eslint-disable-next-line no-constant-condition
      { value: 'empleados', text: 'Empleado' },
      { value: 'diagnostico', text: 'Diagnostico' },
      { value: 'fechaAlta', text: 'Fecha' },
      { value: 'status', text: 'Estado' },
      { value: 'actions', text: 'Acciones' }
    ])
    getPrescriptionsPending(data => {
      prescriptions.value = data
      if (prescriptions.value.length > 0) {
        isloading.value = false
      } else {
        if (prescriptions.value.length <= 0) {
          isloading.value = false
        }
      }
    })
    const refreshTable = () => {
      isloading.value = true
      getPrescriptionsPending(data => {
        prescriptions.value = data
        if (prescriptions.value.length > 0) {
          isloading.value = false
        } else {
          if (prescriptions.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }
    const onFillPrescription = () => {
      fillPrescription(recetaId.value, data => {
        swal.fire({
          title: 'Receta registrada correctamente!',
          text: 'La receta se ha registrado al sistema satisfactoriamente.',
          icon: 'success'
        })
        refreshTable()
      })
    }
    const onClickModal = RecetaId => {
      getProductsPrescriptionByRecetaId(RecetaId, data => {
        if (data.length === 0) {
          swal.fire({
            title: 'Receta sin productos!',
            text: 'La receta no contiene productos registrados.',
            icon: 'error'
          })
        } else {
          if (data[0].cantidad > data[0].producto.cantidadDisponible) {
            swal.fire({
              title: 'Producto sin disponibilidad suficiente!',
              text: 'La receta contiene productos que no contiene disponibilidad suficiente.',
              icon: 'warning'
            })
          }
          recetaId.value = RecetaId
          showModal.value = !showModal.value
          prescriptionSelected.value = data
        }
      })
    }
    const RemovePrescription = recetaId => {
      isloading.value = true
      swal
        .fire({
          title: '¿Estas seguro?',
          text: 'No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Si, archivar arma!',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            swal
              .fire({
                title: 'Arma archivada!',
                text: 'El arma ha sido archivado satisfactoriamente .',
                icon: 'success'
              })
              .then(result => {
                if (result.isConfirmed) {
                  deletePrescription(recetaId, data => {
                    refreshTable()
                  })
                }
              })
          } else {
            isloading.value = false
          }
        })
    }
    const onFiltered = filteredItems => {
      currentPage.value = 1
    }
    return {
      prescriptions,
      employees,
      recetaId,
      breadcrumbItems,
      fields,
      perPage,
      currentPage,
      filter,
      perPageSelect,
      weaponsFieldsBlank,
      prescriptionFields,
      isloading,
      searchValue,
      searchField,
      showModal,
      prescriptionSelected,
      onClickModal,

      onFiltered,
      onFillPrescription,
      refreshTable,
      RemovePrescription
    }
  }
}
</script>

<style></style>
