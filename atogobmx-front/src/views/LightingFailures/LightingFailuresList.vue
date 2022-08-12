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
      <div class="mx-auto h5" style="width: 200px">Agregar Fallas de Alumbrado</div>
      <div>
        <b-button size="sm" variant="outline-danger" @click="close()">Cerrar</b-button>
      </div>
    </template>
      <form ref="form">
        <b-row cols="3">
          <b-col>
            <b-form-group class="mt-3" label="Nombrefalla">
              <b-form-input required v-model="LightingFailuresFields.nombre"></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="">
            <b-form-input re quired v-model="LightingFailuresFields."></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="">

            </b-form-group>
          </b-col>
        </b-row>
      </form>
    </b-modal>
    
    <b-table
      id="lightingfailuresTable"
      :items="lightingfailures"
      :fields="fields"
      :filter="filter"
      :per-page="perPage"
      :current-page="currentPage"
      @filtered="onFiltered"
    >
    </b-table>
    <b-pagination
      v-model="currentPage"
      :total-rows="rows"
      :per-page="perPage"
      aria-controls="lightingfailuresTable"
      align="right"
    ></b-pagination>
  </b-card>
</template>

<script>
import LightingfailuresServices from '@/Services/lightingfailures.Services'
import { ref } from 'vue'
export default {
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

<style lang="scss" scoped></style>
