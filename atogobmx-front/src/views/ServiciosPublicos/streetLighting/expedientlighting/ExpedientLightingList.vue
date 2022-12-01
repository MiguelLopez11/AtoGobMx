<template>
  <b-card class="m-2">
    <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
  </b-card>
  <b-card class="m-2">
    <b-row align-h="end" class="mb-3 mr-1">
      <b-form-input
        size="lg"
        style="width: 350px"
        v-model="searchValue"
        type="search"
        placeholder="Buscar Expediente Alumbrado..."
      >
      </b-form-input>
      <b-button
        :disabled="publicLighting.length < 1"
        style="
          background-color: rgb(94, 80, 238);
          height: 50px;
          width: auto;
          font-size: 18px;
          margin-right: 15px;
          margin-left: 20px;
        "
        @click="showModal = !showModal"
        type="submit"
      >
        <i class="bi bi-folder-fill m-1" />
        Crear Expediente
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
      :items="expedientLighting"
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
            @click="RemoveExpedientLighting(items.expedienteAlumbradoId)"
            class="m-1"
            variant="outline-danger"
            ><i class="bi bi-trash3"> Archivar</i></b-dropdown-item
          >
          <b-dropdown-item
            class="m-1"
            variant="outline-warning"
            :to="{
              name: 'ExpedienteAlumbrado-Edit',
              params: { ExpedienteAlumbradoId: items.expedienteAlumbradoId }
            }"
            ><i class="bi bi-pencil-square" /> Editar</b-dropdown-item
          >
        </b-dropdown>
      </template>
    </EasyDataTable>
  </b-card>
  <b-modal
    v-model="showModal"
    title="Agregar Expediente Alumbrado"
    size="xl"
    centered
    hide-footer
  >
    <b-row>
      <b-form-group class="mt-3" label="Nombre de Alumbrado:">
        <b-form-select
          autofocus
          :options="publicLighting"
          value-field="alumbradoId"
          text-field="nombreObra"
          v-model="expedientLightingFields.alumbradoId"
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
      <b-button
        class="w-auto m-2"
        variant="success"
        @click="onAddExpedientLighting()"
      >
        Guardar
      </b-button>
    </b-row>
  </b-modal>
</template>

<script>
import { ref, inject } from 'vue'
import ExpedientLighting from '@/Services/expedientlighting.Services'
import publiclightingServices from '@/Services/publiclighting.Services'
export default {
  components: {
    EasyDataTable: window['vue3-easy-data-table']
  },
  setup () {
    const showModal = ref(false)
    const swal = inject('$swal')
    const { getPublicLightingExpedient } = publiclightingServices()
    const {
      getExpedientLighting,
      createExpedientLighting,
      deleteExpedientLighting
    } = ExpedientLighting()
    const expedientLighting = ref([])
    const publicLighting = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('nombreObra')
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      {
        text: 'Alumbrado',
        to: '/ServiciosPublicos/AlumbradoPublico/list'
      },
      { text: 'Expediente' }
    ])

    const expedientLightingFields = ref({
      expedienteAlumbradoId: 0,
      alumbradoId: null,
      archivado: false
    })
    const expedientLightingFieldsBlank = ref(
      JSON.parse(JSON.stringify(expedientLightingFields))
    )

    const fields = ref([
      { value: 'expedienteAlumbradoId', text: 'No.Expediente', sortable: true },
      {
        value: 'alumbrado.nombreObra',
        text: 'Nombre de obra alumbrado',
        sortable: true
      },
      { value: 'actions', text: 'Acciones' }
    ])
    const getPublicLighting = () => {
      getPublicLightingExpedient(data => {
        publicLighting.value = data
      })
      return ''
    }
    getExpedientLighting(data => {
      expedientLighting.value = data
      if (expedientLighting.value.length > 0) {
        isloading.value = false
      } else {
        if (expedientLighting.value.length <= 0) {
          isloading.value = false
        }
      }
      getPublicLighting()
    })
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
    }
    const onFiltered = filteredItems => {
      currentPage.value = 1
    }
    const onAddExpedientLighting = () => {
      createExpedientLighting(expedientLightingFields.value, data => {
        showModal.value = false
        expedientLightingFields.value = JSON.parse(
          JSON.stringify(expedientLightingFieldsBlank)
        )
        refreshTable()
        getPublicLighting()
        swal.fire({
          title: '¡Expediente Registrado!',
          text: 'El expediente ha sido registrado al sistema satisfactoriamente .',
          icon: 'success'
        })
      })
    }
    const RemoveExpedientLighting = expedienteDigitalId => {
      isloading.value = true
      swal
        .fire({
          title: '¿Estas seguro?',
          text: 'No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Si, Archivar Expediente!',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            deleteExpedientLighting(expedienteDigitalId, data => {
              refreshTable()
              getPublicLighting()
            })
            swal.fire({
              title: '¡Alumbrado archivado!',
              text: 'El alumbrado ha sido archivado satisfactoriamente .',
              icon: 'success'
            })
          } else {
            isloading.value = false
          }
        })
    }
    return {
      fields,
      perPage,
      breadcrumbItems,
      currentPage,
      filter,
      perPageSelect,
      isloading,
      searchValue,
      searchField,
      expedientLighting,
      publicLighting,
      showModal,
      expedientLightingFields,

      onFiltered,
      RemoveExpedientLighting,
      refreshTable,
      onAddExpedientLighting
    }
  }
}
</script>

<style></style>
