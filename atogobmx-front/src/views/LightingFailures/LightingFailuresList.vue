<template>
  <b-card class="m-3">
    <b-row align-h="end" class="mb-2 mr-1">
      <b-col cols="3">
        <b-form-group>
          <b-form-input
            size="lg"
            v-model="searchValue"
            type="search"
            placeholder="Buscar Falla..."
          ></b-form-input>
        </b-form-group>
      </b-col>
      <b-col cols="3" style="float: right">
        <b-form-group>
          <button
            class="btn btn-primary"
            style="height: 50px; width: auto; font-size: 18px"
            id="buttonAdd"
            v-b-modal.modal-lightingfailures
            type="submit"
          >
            <i class="bi bi-person-plus-fill"></i>
            Agregar Falla
          </button>
        </b-form-group>
      </b-col>
    </b-row>
    <EasyDataTable
      rows-per-page-message="registros por pagina"
      empty-message="No se encontro ningun registro"
      table-class-name="customize-table"
      buttons-pagination
      border-cell
      :loading="isloading"
      :headers="fields"
      :items="lightingFailures"
      :rows-per-page="5"
      :search-field="searchField"
      :search-value="searchValue"
      :table-height="330"
    >
      <template #header-actions="header">
        {{ header.text }}
      </template>
      <template #item-actions="items">
        <b-button
          @click="RemoveLightingFailures(items.fallaId)"
          class="m-1"
          variant="outline-danger"
          ><i class="bi bi-trash3"></i
        ></b-button>
        <b-button
          class="m-1"
          variant="outline-warning"
          :to="{
            name: 'FallasAlumbrado-Edit',
            params: { FallaId: items.fallaId },
          }"
          ><i class="bi bi-pencil-square"></i
        ></b-button>
      </template>
    </EasyDataTable>

    <b-modal
      id="modal-lightingfailures"
      @ok="addLightingFailures"
      title="Agregar Falla"
      size="xl"
      centered
      button-size="lg"
      lazy
      ok-title="Registrar Falla"
      cancel-title="Cancelar"
    >
      <form ref="form">
        <b-row cols="3">
          <b-col>
            <b-form-group class="mt-3" label="NombreFalla">
              <b-form-input
                required
                v-model="lightingFailuresFields.nombreFalla"
              ></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Descripcion">
              <b-form-input
                required
                v-model="lightingFailuresFields.descripcion"
              ></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Fecha">
              <Datepicker
                locale="es"
                :enableTimePicker="false"
                autoApply
                v-model="lightingFailuresFields.fecha"
              ></Datepicker>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Descripcion Domicilio">
              <b-form-input
                required
                v-model="lightingFailuresFields.descripcionDomicilio"
              ></b-form-input>
            </b-form-group>
          </b-col>
        </b-row>
      </form>
    </b-modal>
  </b-card>
</template>

<script>
import LightingfailuresServices from '@/Services/lightingfailures.Services'
import Datepicker from '@vuepic/vue-datepicker'
import { ref } from 'vue'
// import { useToast } from 'vue-toast-notification'
export default {
  components: {
    Datepicker,
    EasyDataTable: window['vue3-easy-data-table']
  },
  setup () {
    const {
      getLightingFailures,
      createLightingFailures,
      deleteLightingFailures
    } = LightingfailuresServices()
    // const $toast = useToast([])
    const lightingFailures = ref([])
    const isOpen = ref(false)
    const perPage = ref(5)
    const currentPage = ref(1)
    // const rows = ref(null)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('nombreFalla')
    const lightingFailuresFields = ref({
      fallaId: 0,
      nombreFalla: null,
      descripcion: null,
      fecha: null,
      descripcionDomicilio: null,
      archivado: false
    })

    const lightingFailuresFieldsBlank = ref(
      JSON.parse(JSON.stringify(lightingFailuresFields))
    )

    const fields = ref([
      { value: 'fallaId', text: 'ID', sortable: true },
      { value: 'nombreFalla', text: 'NombreFalla' },
      { value: 'descripcion', text: 'Descripción' },
      { value: 'fecha', text: 'Fecha' },
      { value: 'descripcionDomicilio', text: 'Descripcion Domicilio' },
      { value: 'actions', text: 'Acciones' }
    ])
    getLightingFailures((data) => {
      lightingFailures.value = data
      // rows.value = data.length
      if (lightingFailures.value.length > 0) {
        isloading.value = false
      } else {
        if (lightingFailures.value.length <= 0) {
          isloading.value = false
        }
      }
    })

    const onFiltered = (filteredItems) => {
      // rows.value = filteredItems.length
      currentPage.value = 1
    }

    const refreshTable = () => {
      isloading.value = true
      getLightingFailures((data) => {
        lightingFailures.value = data
        // rows.value = data.length
        if (lightingFailures.value.length > 0) {
          isloading.value = false
        } else {
          if (lightingFailures.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }

    const addLightingFailures = () => {
      createLightingFailures(lightingFailuresFields.value, (data) => {
        refreshTable()
        // $toast.succes('La falla se registro correctamente', {
        //   position: 'top-rigth',
        //   duration: 2000
        // })
      })
      lightingFailuresFields.value = JSON.parse(JSON.stringify(lightingFailuresFieldsBlank))
    }

    const RemoveLightingFailures = (LightingFailuresId) => {
      isloading.value = true
      deleteLightingFailures(LightingFailuresId, (data) => {
        refreshTable()
      })
    }
    return {
      lightingFailures,
      lightingFailuresFields,
      isOpen,
      perPage,
      currentPage,
      // rows,
      filter,
      perPageSelect,
      isloading,
      searchValue,
      searchField,
      lightingFailuresFieldsBlank,
      // lightingFailuresValues,
      fields,

      onFiltered,
      addLightingFailures,
      refreshTable,
      RemoveLightingFailures
    }
  }
}
</script>

<style>
.customize-table {
  /* --easy-table-border: 1px solid #445269;
  --easy-table-row-border: 1px solid #445269; */

  --easy-table-header-font-size: 18px;
  --easy-table-header-height: 50px;
  --easy-table-header-font-color: #ffffff;
  --easy-table-header-background-color: #42b883;

  --easy-table-header-item-padding: 10px 15px;
  --easy-table-header-item-align: center;
  /* --easy-table-body-even-row-font-color: #fff;
  --easy-table-body-even-row-background-color: #4c5d7a; */

  /* --easy-table-body-row-font-color: #c0c7d2;
  --easy-table-body-row-background-color: #2d3a4f; */
  --easy-table-body-row-height: 50px;
  --easy-table-body-row-font-size: 15px;
  --easy-table-border-radius: 15px;
  /*
  --easy-table-body-row-hover-font-color: #2d3a4f;
  --easy-table-body-row-hover-background-color: #eee; */

  --easy-table-body-item-padding: 10px 15px;

  /* --easy-table-footer-background-color: #138BC2;
  --easy-table-footer-font-color: #D1D1D1; */
  --easy-table-footer-font-size: 15px;
  --easy-table-footer-padding: 0px 10px;
  --easy-table-footer-height: 50px;

  /* --easy-table-scrollbar-track-color: #2d3a4f;
  --easy-table-scrollbar-color: #2d3a4f;
  --easy-table-scrollbar-thumb-color: #4c5d7a;;
  --easy-table-scrollbar-corner-color: #2d3a4f;

  --easy-table-loading-mask-background-color: #2d3a4f; */
}
</style>
