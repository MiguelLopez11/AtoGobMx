<template>
  <b-card class="m-2">
    <b-row align-h="end" class="mb-3 mr-1">
      <b-form-input
        size="lg"
        style="width: 350px"
        v-model="searchValue"
        type="search"
        placeholder="Buscar Expediente Alumbrado..."
      ></b-form-input>
      <b-button
        variant="primary"
        style="
          background-color: rgb(94,80,238);
          height: 50px;
          width: auto;
          font-size: 18px;
          margin-right: 15px;
          margin-left: 20px;
        "
        @click="showModal = !showModal"
        type="submit"
      >
        <i class="bi bi-folder-fill m-1"></i>
        crear Expediente
      </b-button>
    </b-row>
    <EasyDataTable
      rows-per-page-message="registros por pagina"
      empty-message="No se encontro ningun registro"
      table-class-name="customize-table"
      buttons-pagination
      border-cell
      :loading="isloading"
      :headers="fields"
      :items="expedientLighting"
      :rows-per-page="5"
      :search-field="searchField"
      :search-value="searchValue"
    >
      <template #header-actions="header">
        {{ header.text }}
      </template>
      <template #item-actions="items">
        <b-button
          @click="RemoveExpedientLighting(items.expedienteAlumbradoId)"
          class="m-1"
          variant="outline-danger"
          ><i class="bi bi-trash3"></i
        ></b-button>
        <b-button
          class="m-1"
          variant="outline-warning"
          :to="{
            name: 'ExpedienteAlumbrado-Edit',
            params: { ExpedienteAlumbradoId: items.expedienteAlumbradoId }
          }"
          ><i class="bi bi-pencil-square"></i
        ></b-button>
      </template>
    </EasyDataTable>
  </b-card>
  <b-modal
    id="modal-expedientlighting"
    title="Agregar Expediente Alumbrado"
    v-model="showModal"
    size="xl"
    hide-footer
    centered
    button-size="lg"
    lazy
  >
  <b-row>
      <b-form-group class="mt-3" label="Domicilio de registro alumbrado: ">
        <b-form-select
          autofocus
          :options="employees"
          value-field="alumbradoId"
          text-field="domicilio"
          v-model="expedientLightingFields.domicilio"
        />
      </b-form-group>
    </b-row>
    <b-row align-h="end">
      <b-button
        class="w-auto m-2 text-white"
        variant="primary"
        @click="showModal = !showModal"
      >
        Cancelar
      </b-button>
      <b-button class="w-auto m-2" variant="success" @click="onAddExpedient()">
        Guardar
      </b-button>
    </b-row>
  </b-modal>
</template>

<script>
import ExpedientlightingServices from '@/Services/expedientlighting.Services'
// import AreaServices from '@/Services/area.Services'
// import DepartamentServices from '@/Services/departament.Services'
// import publiclightingServices from '@/Services/publiclighting.Services'
import { ref, inject } from 'vue'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    EasyDataTable: window['vue3-easy-data-table']
  },
  setup () {
    const swal = inject('$swal')
    const showModal = ref(false)
    // const { getAreasByDepartament } = AreaServices()
    // const { getDepartaments } = DepartamentServices()
    // const { } = publiclightingServices()
    const {
      getExpedientLighting,
      createExpedientLighting,
      deleteExpedientLighting
    } = ExpedientlightingServices()
    const expedientLighting = ref([])
    // const departaments = ref([])
    const areas = ref([])
    const isOpen = ref(false)
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('lugarPublico')
    const expedientLightingFields = ref({
      expedienteAlumbradoId: 0,
      // descripcionSolucion: null,
      fechaAlta: null,
      // lugarPublico: null,
      // localidad: null,
      // departamentoId: null,
      // areaId: null,
      archivado: false
    })

    // getDepartaments(data => {
    //   departaments.value = data
    //   if (data.length === 0) {
    //     swal.fire({
    //       title: 'No se encuentran departamentos registrados!',
    //       text:
    //         'No se encuentran departamentos registrados en el sistema, registre primero un departamento para continuar.',
    //       icon: 'warning'
    //     })
    //   }
    // })

    // const getAreas = departamentoId => {
    //   getAreasByDepartament(departamentoId, data => {
    //     areas.value = data
    //     if (data.length === 0) {
    //       swal.fire({
    //         title: 'No se encuentran areas registradas!',
    //         text:
    //           'No se encuentran areas registradas en el departamento seleccionado, registre primero una area para continuar.',
    //         icon: 'warning'
    //       })
    //     }
    //   })
    // }

    // pone mis cambios de mis campos vacios de nuevo
    const expedientLightingFieldsBlank = ref(
      JSON.parse(JSON.stringify(expedientLightingFields))
    )

    const fields = ref([
      { value: 'expedienteAlumbradoId', text: 'ID', sortable: true },
      { value: 'fechaAlta', text: 'Fecha alta' },
      { value: 'lugarPublico', text: 'Lugar publico' },
      { value: 'localidad', text: 'Localidad' },
      { value: 'departamentoId', text: 'Departamento' },
      { value: 'areaId', text: 'Area' },
      { value: 'actions', text: 'Acciones' }
    ])

    getExpedientLighting(data => {
      expedientLighting.value = data
      // rows.value = data.length
      if (expedientLighting.value.length > 0) {
        isloading.value = false
      } else {
        if (expedientLighting.value.length <= 0) {
          isloading.value = false
        }
      }
    })

    const onFiltered = filteredItems => {
      currentPage.value = 1
    }

    const refreshTable = () => {
      isloading.value = true
      getExpedientLighting(data => {
        expedientLighting.value = data
        if (expedientLighting.value.length > 0) {
          isloading.value = false
        } else {
          if (expedientLighting.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }

    const addExpedientLighting = () => {
      createExpedientLighting(expedientLightingFields.value, data => {
        refreshTable()
        swal.fire({
          title: '¡Expediente publico agregado correctamente!',
          text: 'Expediente registrado satisfactoriamente',
          icon: 'success'
        })
      })
      showModal.value = false
      // expedientLightingFields.value = JSON.parse(
      //   JSON.stringify(expedientLightingFieldsBlank)
      // )
    }

    const RemoveExpedientLighting = StreetLightingId => {
      isloading.value = true
      swal
        .fire({
          title: '¿Estas seguro',
          text: 'No podras revertir esto',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Si, Archivar Expediente!',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            deleteExpedientLighting(StreetLightingId, data => {
              refreshTable()
            })
            swal.fire({
              title: '¡Expediente archivado!',
              text: 'El expediente ha sido archivado satisfactoriamente.',
              icon: 'success'
            })
          } else {
            isloading.value = false
          }
        })
    }

    return {
      expedientLighting,
      expedientLightingFields,
      showModal,
      isOpen,
      perPage,
      currentPage,
      filter,
      perPageSelect,
      isloading,
      searchValue,
      searchField,
      expedientLightingFieldsBlank,
      fields,
      areas,

      onFiltered,
      // getAreas,
      addExpedientLighting,
      refreshTable,
      RemoveExpedientLighting
    }
  }
}
</script>

<style></style>
