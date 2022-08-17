<template>
  <b-card class="m-3">
    <b-row align-h="end" class="mb-2 mr-1">
      <b-col cols="3">
        <b-form-group>
        <b-form-input
          size="lg"
          v-model="searchValue"
          type="search"
          placeholder="Buscar empleado..."
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
            name: 'ExpedienteEmpleados',
            params: { empleadoId: items.empleadoId },
          }"
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
      lazy
      ok-title="Registrar empleado"
      cancel-title="Cancelar"
    >
      <form ref="form">
        <b-row cols="3">
          <b-col>
            <b-form-group class="mt-3" label="Nombre">
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
                :enableTimePicker="false"
                autoApply
                v-model="EmployeesFields.fechaNacimiento"
              ></Datepicker>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Estado">
              <b-form-select
                autofocus
                :options="states"
                v-model="EmployeesFields.estado"
              ></b-form-select>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Municipio">
              <b-form-input
                v-model="EmployeesFields.municipio"
                required
              ></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Localidad">
              <b-form-input
                v-model="EmployeesFields.localidad"
                required
              ></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Calle">
              <b-form-input
                v-model="EmployeesFields.calle"
                required
              ></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Numero Exterior">
              <b-form-input
                v-model="EmployeesFields.numeroExterior"
                required
              ></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Numero Interior">
              <b-form-input
                v-model="EmployeesFields.numeroInterior"
                required
              ></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Codigo Postal">
              <b-form-input
                v-model="EmployeesFields.codigoPostal"
                required
                type="number"
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
import { useToast } from 'vue-toast-notification'
// import VueSimpleAlert from 'vue-simple-alert'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    Datepicker,
    EasyDataTable: window['vue3-easy-data-table']
  },
  setup () {
    const { getEmployees, createEmployee, deleteEmployee } = EmployeeServices()
    const { getAreas } = AreaServices()
    // const refemployeesTable = ref()
    const $toast = useToast()
    const employees = ref([])
    const areas = ref([])
    const isOpen = ref(false)
    const perPage = ref(5)
    const currentPage = ref(1)
    const rows = ref(null)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('nombre')
    const states = ref(['Aguascalientes',
      'Baja California',
      'Baja California Sur',
      'Campeche',
      'Chiapas',
      'Chihuahua',
      'Coahuila',
      'Colima',
      'Distrito Federal',
      'Durango',
      'Guanajuato',
      'Guerrero',
      'Hidalgo',
      'Jalisco',
      'México',
      'Michoacán',
      'Morelos',
      'Nayarit',
      'Nuevo León',
      'Oaxaca',
      'Puebla',
      'Querétaro',
      'Quintana Roo',
      'San Luis Potosí',
      'Sinaloa',
      'Sonora',
      'Tabasco',
      'Tamaulipas',
      'Tlaxcala',
      'Veracruz',
      'Yucatán',
      'Zacatecas'])
    const EmployeesFields = ref({
      empleadoId: 0,
      nombre: null,
      apellidoPaterno: null,
      apellidoMaterno: null,
      fechaNacimiento: null,
      estado: null,
      municipio: null,
      localidad: null,
      calle: null,
      numeroExterior: null,
      numeroInterior: null,
      codigoPostal: null,
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
      if (employees.value.length > 0) {
        isloading.value = false
      } else {
        if (employees.value.length <= 0) {
          isloading.value = false
        }
      }
    })
    getAreas((data) => {
      areas.value = data
    })
    const onFiltered = (filteredItems) => {
      rows.value = filteredItems.length
      currentPage.value = 1
    }
    const refreshTable = () => {
      isloading.value = true
      getEmployees((data) => {
        employees.value = data
        rows.value = data.length
        if (employees.value.length > 0) {
          isloading.value = false
        } else {
          if (employees.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }
    const addEmployee = () => {
      createEmployee(EmployeesFields.value, (data) => {
        refreshTable()
        $toast.success('Empleado registrado correctamente.', {
          position: 'top-right',
          duration: 10
        })
      })
    }
    const RemoveEmployee = (employeeId) => {
      // $toast.warning('',?options)
      isloading.value = true
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
      isloading,
      searchValue,
      searchField,
      isOpen,
      states,
      // refemployeesTable,

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
