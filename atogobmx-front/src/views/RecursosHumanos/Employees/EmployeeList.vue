<template>
  <b-card class="m-3">
    <b-row align-h="end" class="mb-3 mr-1">
      <b-form-input
        size="lg"
        style="width: 350px"
        v-model="searchValue"
        type="search"
        placeholder="Buscar Empleado..."
      >
      </b-form-input>
      <b-button
        variant="primary"
        style="
          height: 50px;
          width: auto;
          font-size: 18px;
          margin-right: 15px;
          margin-left: 20px;
        "
        v-b-modal.modal-employee
        type="submit"
      >
        <i class="bi bi-person-plus-fill"></i>
        Agregar Empleado
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
      :items="employees"
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
            params: { EmpleadoId: items.empleadoId }
          }"
          ><i class="bi bi-pencil-square"></i
        ></b-button>
      </template>
    </EasyDataTable>
    <b-modal
      id="modal-employee"
      title="Agregar empleados"
      size="xl"
      centered
      button-size="lg"
      lazy
      ok-title="Registrar empleado"
      cancel-title="Cancelar"
    >
      <b-form>
        <validation-provider rules="required" v-slot="{ errors }">
          <b-row cols="3">
            <b-col>
              <b-form-group class="mt-3" label="Nombre Completo">
                <b-form-input
                  name="nameField"
                  class="form-control"
                  v-model="EmployeesFields.nombreCompleto"
                  :rules="validateName"
                ></b-form-input>
                <b-form-invalid-feedback>{{ errors }}</b-form-invalid-feedback>
              </b-form-group>
            </b-col>
            <b-col>
              <b-form-group class="mt-3" label="Fecha de nacimiento">
                <Datepicker
                  locale="es"
                  :enableTimePicker="false"
                  autoApply
                  v-model="EmployeesFields.fechaNacimiento"
                >
                </Datepicker>
              </b-form-group>
            </b-col>
            <b-col>
              <b-form-group class="mt-3" label="Area">
                <b-form-select
                  v-model="EmployeesFields.areaId"
                  autofocus
                  :options="areas"
                  value-field="areaId"
                  text-field="nombre"
                >
                </b-form-select>
              </b-form-group>
            </b-col>
          </b-row>
        </validation-provider>
      </b-form>
      <b-row align-h="end">
        <b-button
          class="col-1 m-2 text-white"
          variant="primary"
          type="reset"
          v-b-modal.modal-employee
        >
          Cancelar
        </b-button>
        <b-button class="col-1 m-2" variant="success" type="submit"
          >Guardar</b-button
        >
      </b-row>
    </b-modal>
  </b-card>
</template>

<script>
import EmployeeServices from '@/Services/employee.Services'
import AreaServices from '@/Services/area.Services'
import Datepicker from '@vuepic/vue-datepicker'
import { ValidationProvider } from 'vee-validate'
import { ref } from 'vue'
import { useToast } from 'vue-toast-notification'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    Datepicker,
    ValidationProvider,
    EasyDataTable: window['vue3-easy-data-table']
  },
  setup () {
    const { getEmployees, createEmployee, deleteEmployee } = EmployeeServices()
    const { getAreas } = AreaServices()
    const $toast = useToast()
    const employees = ref([])
    const areas = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('nombre')
    const EmployeesFields = ref({
      empleadoId: 0,
      nombreCompleto: null,
      fechaNacimiento: null,
      fechaAlta: null,
      fechaBaja: null,
      archivado: false,
      areaId: null,
      usuarioId: null
    })
    const validateName = value => {
      if (!value) {
        return 'Este campo es requerido'
      }
      return true
    }
    const validateArea = value => {
      if (!value) {
        return 'Este campo es requerido'
      }
      return true
    }
    const EmployeesFieldsBlank = ref(
      JSON.parse(JSON.stringify(EmployeesFields))
    )
    const fields = ref([
      { value: 'empleadoId', text: 'ID', sortable: true },
      { value: 'nombreCompleto', text: 'Nombre' },
      { value: 'area.nombre', text: 'Area de Trabajo' },
      { value: 'actions', text: 'Acciones' }
    ])
    getEmployees(data => {
      employees.value = data
      // rows.value = data.length
      if (employees.value.length > 0) {
        isloading.value = false
      } else {
        if (employees.value.length <= 0) {
          isloading.value = false
        }
      }
    })
    getAreas(data => {
      areas.value = data
      if (areas.value.length === 0) {
        $toast.warning(
          'No se encuentran registros de areas de trabajo, registre una primero para registrar un empleado'
        )
      }
    })
    const onFiltered = filteredItems => {
      // rows.value = filteredItems.length
      currentPage.value = 1
    }
    const refreshTable = () => {
      isloading.value = true
      getEmployees(data => {
        employees.value = data
        // rows.value = data.length
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
      createEmployee(EmployeesFields.value, data => {
        refreshTable()
        $toast.success('Empleado registrado correctamente.', {
          position: 'top-right',
          duration: 1500
        })
      })
      EmployeesFields.value = JSON.parse(JSON.stringify(EmployeesFieldsBlank))
    }
    const RemoveEmployee = employeeId => {
      isloading.value = true
      deleteEmployee(employeeId, data => {
        refreshTable()
      })
    }
    return {
      employees,
      fields,
      perPage,
      currentPage,
      // rows,
      filter,
      perPageSelect,
      areas,
      EmployeesFieldsBlank,
      EmployeesFields,
      isloading,
      searchValue,
      searchField,
      onFiltered,
      addEmployee,
      refreshTable,
      RemoveEmployee,
      validateName,
      validateArea
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
