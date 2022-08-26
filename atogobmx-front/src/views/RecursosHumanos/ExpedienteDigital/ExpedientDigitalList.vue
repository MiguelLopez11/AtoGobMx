<template>
  <b-card
    class="m-3"
  >
  <b-row align-h="end" class="mb-2 mr-1">
      <b-col cols="3">
        <b-form-group>
        <b-form-input
          size="lg"
          v-model="searchValue"
          type="search"
          placeholder="Buscar Expediente..."
        >
        </b-form-input>
        </b-form-group>
      </b-col>
      <b-col cols="3" style="float: right">
        <b-form-group>
        <button
          class="btn btn-primary"
          style="height: 50px; width: auto; font-size: 18px"
          v-b-modal.modal-employee
          type="submit"
        >
        <i class="bi bi-person-plus-fill"></i>
          Agregar Empleado
        </button>
        </b-form-group>
      </b-col>
    </b-row>
    <EasyDataTable
      rows-per-page-message="registros por pagina"
      empty-message="No se encuentran registros"
      table-class-name="customize-table"
      buttons-pagination
      border-cell
      :loading="isloading"
      :headers="fields"
      :items="employees"
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
export default {
  components: {
    EasyDataTable: window['vue3-easy-data-table']
  },
  setup () {
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('nombre')
    const fields = ref([
      { value: 'empleadoId', text: 'ID', sortable: true },
      { value: 'nombre', text: 'Nombre' },
      { value: 'apellidoPaterno', text: 'Apellido Paterno' },
      { value: 'apellidoMaterno', text: 'Apellido Materno' },
      { value: 'area.nombre', text: 'Area de Trabajo' },
      { value: 'actions', text: 'Acciones' }
    ])
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

      onFiltered
    }
  }
}
</script>

<style>

</style>
