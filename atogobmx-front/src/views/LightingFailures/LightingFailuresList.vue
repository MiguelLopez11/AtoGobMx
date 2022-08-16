<template>
  <b-card class="m-3">
    <b-row>
      <b-form-input
        class="w-25 m-3"
        v-model="filter"
        type="search"
        placeholder="Buscar..."
      ></b-form-input>
      <button
        id="buttonAdd"
        class="btn col-1 m-3"
        v-b-modal.modal-add-lightingfailures
      >
        Agregar
      </button>
    </b-row>

    <EasyDataTable
      :headers="fields"
      :items="lightingfailures"
      buttons-pagination
      border-cell
      :rows-per-page="5"
      table-class-name="ccustomize-table"
    >
      <template #header-actions="header">
        {{ header.text }}
      </template>
      <template #item-actions>
        <b-dropdown text="Acciones" variant="primary" dropright>
          <b-dropdow-item variant="danger">
            <i class="bi bi-trash-fill"></i>Eliminar
          </b-dropdow-item>
          <b-dropdow-item>
            <i class="bi bi-pencil-square"></i> Editar
          </b-dropdow-item>
        </b-dropdown>
      </template>
    </EasyDataTable>

    <b-modal
      id="modal-add-lightingfailures"
      ref="modal"
      centered
      no-close-on-esc
      size="xl"
      @ok="addLightingFailures"
    >
      <template #modal-header="{ close }">
        <div class="mx-auto h5" style="width: 200px">
          Agregar Fallas de Alumbrado
        </div>
        <div>
          <b-button size="sm" variant="outline-danger" @click="close()"
            >Cerrar</b-button
          >
        </div>
      </template>
      <form ref="form">
        <b-row cols="3">
          <b-col>
            <b-form-group class="mt-3" label="nombreFalla">
              <b-form-input
                required
                v-model="LightingFailuresFields.nombreFalla"
              ></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="descripcion">
              <b-form-input
                required
                v-model="LightingFailuresFields.descripcion"
              ></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="fecha">
              <Datepicker
                v-model="LightingFailuresFields.fecha"
              ></Datepicker>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="descripcionDomicilio">
              <b-form-input
                required
                v-model="LightingFailuresFields.descripcionDomicilio"
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
export default {
  components: {
    Datepicker,
    EasyDataTable: window['vue3-easy-data-table']
  },
  setup () {
    const { getLightingFailures } = LightingfailuresServices()
    const Lightingfailures = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    const rows = ref(null)
    const filter = ref(null)
    const fields = ref([
      { key: 'fallaId', label: 'ID' },
      { key: 'nombreFalla', label: 'NombreFalla' },
      { key: 'descripcion', label: 'Descripcion' },
      { key: 'fecha', label: 'Fecha' }
    ])
    getLightingFailures((data) => {
      Lightingfailures.value = data
      rows.value = data.length
    })

    const onFiltered = (filteredItems) => {
      rows.value = filteredItems.length
      currentPage.value = 1
    }
    return {
      Lightingfailures,
      perPage,
      currentPage,
      rows,
      filter,
      fields,

      onFiltered
    }
  }
}
</script>

<style>
#buttonAdd {
  background: #0d6efd;
  color: #ffffff;
}
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
  --easy-table-body-row-font-size: 20px;
  --easy-table-border-radius: 15px;
  /*
  --easy-table-body-row-hover-font-color: #2d3a4f;
  --easy-table-body-row-hover-background-color: #eee; */

  --easy-table-body-item-padding: 10px 15px;

  /* --easy-table-footer-background-color: #138BC2;
  --easy-table-footer-font-color: #D1D1D1; */
  --easy-table-footer-font-size: 20px;
  --easy-table-footer-padding: 0px 10px;
  --easy-table-footer-height: 50px;

  /* --easy-table-scrollbar-track-color: #2d3a4f;
  --easy-table-scrollbar-color: #2d3a4f;
  --easy-table-scrollbar-thumb-color: #4c5d7a;;
  --easy-table-scrollbar-corner-color: #2d3a4f;

  --easy-table-loading-mask-background-color: #2d3a4f; */
}
</style>
