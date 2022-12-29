<template>
  <b-card class="m-2">
    <b-row align-h="end" class="mb-3 mr-1">
      <b-form-input
        size="lg"
        class="w-25"
        v-model="searchValue"
        type="search"
        placeholder="Buscar..."
      >
      </b-form-input>
      <b-button
        style="
          background-color: rgb(94,80,238);
          height: 50px;
          width: auto;
          font-size: 18px;
          margin-right: 15px;
          margin-left: 20px;
        "
        to="/ServiciosMedicos/Receta/list"
      >
        <i class="bi bi-car-front-fill" />
        Nueva Receta
      </b-button>
    </b-row>
    <EasyDataTable
      rows-per-page-message="registros por pagina"
      empty-message="No se encuentran registros"
      table-class-name="customize-table"
      buttons-pagination
      border-cell
      :loading="isloading"
      :headers="fields"
      :items="medicalAppointments"
      :rows-per-page="5"
      :search-field="searchField"
      :search-value="searchValue"
      theme-color="#7367f0"
    >
      <template #header-actions="header">
        {{ header.text }}
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
            @click="RemoveMedicalAppointment(items.recetaId)"
            class="m-1"
            variant="outline-danger"
          >
            <i class="bi bi-trash3">
              Archivar
            </i>
          </b-dropdown-item>
        </b-dropdown>
      </template>
      <template #item-status="items">
        <b-badge :variant="items.estatusReceta.nombre === 'Pendiente' ? 'warning': 'success'">
        {{items.estatusReceta.nombre}}
        </b-badge>
      </template>
    </EasyDataTable>
  </b-card>
</template>

<script>
import PrescriptionServices from '@/Services/prescription.Services'
import { ref, inject } from 'vue'
export default {
  components: {

  },
  props: {
    EmpleadoId: {
      type: Number,
      required: true
    }
  },
  setup (props) {
    const swal = inject('$swal')
    const {
      getPrescriptionByEmpleadoId,
      deletePrescription
    } = PrescriptionServices()
    const medicalAppointments = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('diagnostico')
    const fields = ref([
      { value: 'empleados.nombreCompleto', text: 'Citante' },
      { value: 'diagnostico', text: 'Diagnostico' },
      { value: 'fechaAlta', text: 'Fecha' },
      { value: 'status', text: 'Estado' },
      { value: 'actions', text: 'Acciones' }
    ])
    getPrescriptionByEmpleadoId(props.EmpleadoId, data => {
      medicalAppointments.value = data
      if (medicalAppointments.value.length > 0) {
        isloading.value = false
      } else {
        if (medicalAppointments.value.length <= 0) {
          isloading.value = false
        }
      }
    })
    const refreshTable = () => {
      isloading.value = true
      getPrescriptionByEmpleadoId(props.EmpleadoId, data => {
        medicalAppointments.value = data
        if (medicalAppointments.value.length > 0) {
          isloading.value = false
        } else {
          if (medicalAppointments.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }
    const RemoveMedicalAppointment = RecetaId => {
      isloading.value = true
      swal
        .fire({
          title: '¿Estas seguro?',
          text: 'No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Si, archivar receta!',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            swal
              .fire({
                title: 'Receta archivada!',
                text: 'La receta ha sido archivado satisfactoriamente .',
                icon: 'success'
              })
              .then(result => {
                if (result.isConfirmed) {
                  deletePrescription(RecetaId, data => {
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
      medicalAppointments,
      fields,
      perPage,
      currentPage,
      filter,
      perPageSelect,
      isloading,
      searchValue,
      searchField,

      onFiltered,
      refreshTable,
      RemoveMedicalAppointment
    }
  }
}
</script>

<style scoped></style>
