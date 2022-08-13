<template>
  <b-card class="m-3">
      <b-row align-h="end" class="mb-3 mr-1">
        <b-col cols="3">
          <b-form-input
            size="lg"
            v-model="searchValue"
            type="search"
            placeholder="Buscar empleado..."
          >
          </b-form-input>
          </b-col>
        <b-col cols="2">
          <button
            class="btn btn-primary"
            style="height: 50px; width: 150px"
            v-b-modal.modal-employee
          >
            Agregar
          </button>
        </b-col>
      </b-row>
    <EasyDataTable
      ref="refemployeesTable"
      rows-per-page-message="registros por pagina"
      empty-message="No se encuentran registros"
      buttons-pagination
      border-cell
      table-class-name="customize-table"
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
        <b-button class="m-1" variant="outline-warning" :to="{ name:'ExpedienteEmpleados', params:{ empleadoId: items.empleadoId }}"
          ><i class="bi bi-pencil-square"></i
        ></b-button>
      </template>
    </EasyDataTable>
    <b-modal
      id="modal-employee"
      @ok="addEmployee"
      hide-footer
      title="Agregar empleados"
      size="xl"
      centered
      hide-backdrop
      button-size="lg"
    >
      <form ref="form">
        <b-row cols="3">
          <b-col>
            <b-form-group
              class="mt-3"
              label="Nombre"
            >
              <b-form-input
                v-model="EmployeesFields.nombre"
                required
              ></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Apellido Paterno">
              <b-form-input
                v-model="EmployeesFields.apellidoPaterno"
                required
              ></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Apellido Materno">
              <b-form-input
                v-model="EmployeesFields.apellidoMaterno"
                required
              ></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Fecha de nacimiento">
              <Datepicker
                locale="es"
                selectText="Seleccionar"
                cancelText="Cerrar"
                :enableTimePicker="false"
                autoApply
                v-model="EmployeesFields.fechaNacimiento"
              ></Datepicker>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Dirección">
              <b-form-input
                v-model="EmployeesFields.direccion"
                required
              ></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Area">
              <b-form-select
                autofocus
                :options="areas"
                value-field="areaId"
                text-field="nombre"
                v-model="EmployeesFields.areaId"
              ></b-form-select>
            </b-form-group>
          </b-col>
        </b-row>
      </form>
    </b-modal>
  </b-card>
</template>

<script>
import EmployeeServices from '@/Services/employee.Services'
import AreaServices from '@/Services/area.Services'
import Datepicker from '@vuepic/vue-datepicker'
import { ref } from 'vue'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    Datepicker,
    EasyDataTable: window['vue3-easy-data-table']
  },
  setup () {
    const { getEmployees, createEmployee, deleteEmployee } = EmployeeServices()
    const { getAreas } = AreaServices()
    const refemployeesTable = ref()
    const employees = ref([])
    const areas = ref([])
    const isOpen = ref(false)
    const perPage = ref(5)
    const currentPage = ref(1)
    const rows = ref(null)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
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
      { value: 'apellidoMaterno', text: 'Apellido Materno' },
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
    const refreshTable = () => {
      getEmployees((data) => {
        employees.value = data
        rows.value = data.length
      })
      return 'datos recargados'
    }
    const addEmployee = () => {
      createEmployee(EmployeesFields.value, (data) => {
        refreshTable()
      })
    }
    const RemoveEmployee = (employeeId) => {
      deleteEmployee(employeeId, (data) => {
        refreshTable()
      })
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
      isOpen,
      refemployeesTable,

      onFiltered,
      addEmployee,
      refreshTable,
      RemoveEmployee
    }
  }
}
</script>

<style>
#buttonAdd {
  background: #0d6efd;
  color: #ffffff;
  height: auto;
  width: 100px;
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
  --easy-table-message-font-size: 20px;
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
