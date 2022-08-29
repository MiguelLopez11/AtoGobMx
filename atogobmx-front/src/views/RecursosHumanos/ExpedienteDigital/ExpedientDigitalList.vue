<template>
  <b-card
    class="m-3"
  >
 <b-row align-h="end" class="mb-3 mr-1">
        <b-form-input
          size="lg"
          style="width: 350px"
          v-model="searchValue"
          type="search"
          placeholder="Buscar Expediente..."
        >
        </b-form-input>
        <button
          class="btn btn-primary"
          style="height: 50px; width: auto; font-size: 18px; margin-right: 15px; margin-left: 20px"
          v-b-modal.modal-employee
          type="submit"
        >
        <i class="bi bi-person-plus-fill"></i>
          Agregar Expediente Digital
        </button>
    </b-row>
    <EasyDataTable
      rows-per-page-message="registros por pagina"
      empty-message="No se encuentran registros"
      table-class-name="customize-table"
      buttons-pagination
      border-cell
      :loading="isloading"
      :headers="fields"
      :items="expedients"
      :rows-per-page="5"
      :search-field="searchField"
      :search-value="searchValue"
    >
      <template #header-actions="header">
        {{ header.text }}
      </template>
      <template #item-actions="items">
        <b-button
          @click="RemoveEmployee(items.empleadoId)"
          class="m-1"
          variant="outline-danger"
          ><i class="bi bi-trash3"></i
        ></b-button>
        <b-button
          class="m-1"
          variant="outline-warning"
          :to="{
            name: 'Empleados-Edit',
            params: { EmpleadoId: items.empleadoId },
          }"
          ><i class="bi bi-pencil-square"></i
        ></b-button>
      </template>
    </EasyDataTable>
  </b-card>
</template>

<script>
import { ref } from 'vue'
import ExpedientDigitalServices from '@/Services/expedientdigital.Services'
export default {
  components: {
    EasyDataTable: window['vue3-easy-data-table']
  },
  setup () {
    const { getExpedients } = ExpedientDigitalServices()
    const expedients = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('expedienteDigitalId')
    const fields = ref([
      { value: 'empleado.nombreCompleto', text: 'Empleado', sortable: true },
      { value: 'estado', text: 'Nombre' },
      { value: 'municipio', text: 'Apellido Paterno' },
      { value: 'localidad', text: 'Apellido Materno' },
      { value: 'calle', text: 'Area de Trabajo' },
      { value: 'actions', text: 'Acciones' }
    ])
    getExpedients(data => {
      expedients.value = data
      if (expedients.value.length > 0) {
        isloading.value = false
      } else {
        if (expedients.value.length <= 0) {
          isloading.value = false
        }
      }
    })
    const onFiltered = (filteredItems) => {
      currentPage.value = 1
    }
    return {
      fields,
      perPage,
      currentPage,
      filter,
      perPageSelect,
      isloading,
      searchValue,
      searchField,
      expedients,

      onFiltered
    }
  }
}
</script>

<style>
.customize-table {
  /* --easy-table-border: 1px solid #445269;
  --easy-table-row-border: 1px solid #445269; */

  --easy-table-header-font-size: 16px;
  --easy-table-header-height: 50px;
  --easy-table-header-font-color: #fcf6f5ff;
  --easy-table-header-background-color: #2bae66ff;

  --easy-table-header-item-padding: 10px 15px;
  --easy-table-header-item-align: center;
  --easy-table-message-font-size: 17px;
  /* --easy-table-body-even-row-font-color: #fff;
  --easy-table-body-even-row-background-color: #4c5d7a; */

  /* --easy-table-body-row-font-color: #c0c7d2;
  --easy-table-body-row-background-color: #2d3a4f; */
  --easy-table-body-row-height: 50px;
  --easy-table-body-row-font-size: 17px;
  --easy-table-border-radius: 15px;

  --easy-table-body-row-hover-font-color: rgb(0, 0, 0);
  --easy-table-body-row-hover-background-color: rgb(212, 212, 212);

  --easy-table-body-item-padding: 10px 15px;

  --easy-table-footer-background-color: #2bae66ff;
  --easy-table-footer-font-color: #fcf6f5ff;
  --easy-table-footer-font-size: 17px;
  --easy-table-footer-padding: 0px 10px;
  --easy-table-footer-height: 50px;

  /* --easy-table-scrollbar-track-color: #2d3a4f;
  --easy-table-scrollbar-color: #2d3a4f;
  --easy-table-scrollbar-thumb-color: #4c5d7a;;
  --easy-table-scrollbar-corner-color: #2d3a4f;

  --easy-table-loading-mask-background-color: #2d3a4f; */
}

</style>
