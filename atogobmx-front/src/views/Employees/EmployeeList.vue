<template>
  <b-card class="m-3">
    <b-row class="justify-content-end">
      <div class="col-3">
      <b-form-select
        class="mt-3"
        :options="searchFieldFilter"
        v-model="searchField"
      ></b-form-select>
      </div>
      <b-form-input
        class="w-25 m-3"
        v-model="searchValue"
        type="search"
        placeholder="Buscar..."
      >
      </b-form-input>
      <button id="buttonAdd" class="btn col-1 m-3" v-b-modal.modal-add-employee>
        Agregar
      </button>
    </b-row>
    <EasyDataTable
      :headers="fields"
      :items="employees"
      buttons-pagination
      border-cell
      :rows-per-page="5"
      table-class-name="customize-table"
      :search-field="searchField"
      :search-value="searchValue"
    >
      <template #header-actions="header">
        {{ header.text }}
      </template>
      <template #item-actions>
        <b-button class="m-1" variant="outline-danger"
          ><i class="bi bi-trash3"></i
        ></b-button>
        <b-button class="m-1" variant="outline-warning"
          ><i class="bi bi-pencil-square"></i
        ></b-button>
        <!-- <b-dropdown text="Acciones" variant="primary" dropright>
          <b-dropdown-item variant="danger">
            <i class="bi bi-trash-fill"></i> Eliminar
          </b-dropdown-item>
          <b-dropdown-item>
            <i class="bi bi-pencil-square"></i> Editar
          </b-dropdown-item>
        </b-dropdown> -->
      </template>
    </EasyDataTable>
    <!-- <b-table
      id="employeeTable"
      hover
      caption-top
      head-row-variant="primary"
      bordered
      show-empty
      empty-text="No se encuentran empleados registrados"
      class="mt-3 mb-3"
      ref="refEmployeeTable"
      :items="employees"
      :fields="fields"
      @filtered="onFiltered"
      :filter="filter"
      :perPage="perPage"
      :currentpage="currentPage"
    >
      <template #cell(actions)>
        <b-dropdown text="Acciones" variant="primary" dropright>
          <b-dropdown-item variant="danger">
            <i class="bi bi-trash-fill"></i> Eliminar
          </b-dropdown-item>
          <b-dropdown-item>
            <i class="bi bi-pencil-square"></i> Editar
          </b-dropdown-item>
        </b-dropdown>
      </template>
    </b-table>
    <b-pagination
      pills
      v-model="currentPage"
      :total-rows="rows"
      :per-page="perPage"
      aria-controls="employeeTable"
      align="right"
    >
    </b-pagination> -->
    <b-modal
      id="modal-add-employee"
      ref="modal"
      centered
      no-close-on-esc
      size="xl"
      hide-footer
    >
      <form ref="form">
        <b-row cols="3">
          <b-col>
            <b-form-group class="mt-3" label="Nombre">
              <b-form-input
                required
                v-model="EmployeesFields.nombre"
              ></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Apellido Paterno">
              <b-form-input
                required
                v-model="EmployeesFields.apellidoPaterno"
              ></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Apellido Materno">
              <b-form-input
                required
                v-model="EmployeesFields.apellidoMaterno"
              ></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Fecha de nacimiento">
              <Datepicker
                v-model="EmployeesFields.fechaNacimiento"
              ></Datepicker>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Dirección">
              <b-form-input
                required
                v-model="EmployeesFields.direccion"
              ></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Area">
              <b-form-select
                autofocus
                :options="areas"
                :reduce="(areas) => areas.areaId"
                v-model="EmployeesFields.areaId"
              ></b-form-select>
            </b-form-group>
          </b-col>
        </b-row>
      </form>
      <!-- <b-button>
        Guardar
      </b-button> -->
    </b-modal>
  </b-card>
</template>

<script>
import EmployeeServices from '@/Services/employee.Services'
import AreaServices from '@/Services/area.Services'
import Datepicker from '@vuepic/vue-datepicker'
import '@vuepic/vue-datepicker/dist/main.css'
import { ref } from 'vue'
export default {
  components: {
    Datepicker,
    EasyDataTable: window['vue3-easy-data-table']
  },
  setup () {
    const { getEmployees, createEmployee } = EmployeeServices()
    const { getAreas } = AreaServices()

    const employees = ref([])
    const areas = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    const rows = ref(null)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const searchFieldFilter = ref(['nombre', 'empleadoId'])
    const loading = ref(false)
    const searchValue = ref('')
    const searchField = ref('nombre')
    const EmployeesFields = ref({
      empleadoId: 0,
      nombre: null,
      apellidoPaterno: null,
      apellidoMaterno: null,
      fechaNacimiento: null,
      direccion: null,
      fechaAlta: null,
      fechaBaja: null,
      archivado: false,
      areaId: null
    })
    const fields = ref([
      { value: 'empleadoId', text: 'ID', sortable: true },
      { value: 'nombre', text: 'Nombre' },
      { value: 'apellidoPaterno', text: 'Apellido Paterno' },
      { value: 'apellidoPaterno', text: 'Apellido Materno' },
      { value: 'area.nombre', text: 'Area de Trabajo' },
      { value: 'actions', text: 'Acciones' }
    ])
    getEmployees((data) => {
      employees.value = data
      rows.value = data.length
    })
    getAreas((data) => {
      areas.value = data
    })
    const onFiltered = (filteredItems) => {
      rows.value = filteredItems.length
      currentPage.value = 1
    }
    const addEmployee = () => {
      console.log('holi')
      createEmployee(EmployeesFields.value)
      refreshData()
    }
    const refreshData = () => {
      // refEmployeeTable.value.refresh()
      this.$refs.table.refresh()
    }
    return {
      employees,
      fields,
      perPage,
      currentPage,
      rows,
      filter,
      perPageSelect,
      areas,
      EmployeesFields,
      loading,
      searchValue,
      searchField,
      searchFieldFilter,

      onFiltered,
      addEmployee,
      refreshData
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
