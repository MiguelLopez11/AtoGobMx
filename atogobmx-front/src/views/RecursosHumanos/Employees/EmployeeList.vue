<template>
  <b-card class="m-2">
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
        style="
          background-color: rgb(94,80,238);
          height: 50px;
          width: auto;
          font-size: 18px;
          margin-right: 15px;
          margin-left: 20px;
        "
        @click="showModal = !showModal"
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
      theme-color="#7367f0"
    >
      <template #header-actions="header">
        {{ header.text }}
      </template>
      <template #item-actions="items">
        <b-dropdown
          :disabled="items.archivado && items.puestoTrabajo.nombre !== 'Administrador' || 'Director'"
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
            @click="RemoveEmployee(items.empleadoId)"
            class="m-1"
            variant="outline-danger"
            ><i class="bi bi-trash3"> Archivar</i></b-dropdown-item
          >
          <b-dropdown-item
            class="m-1"
            variant="outline-warning"
            :to="{
              name: 'Empleados-Edit',
              params: { EmpleadoId: items.empleadoId }
            }"
          >
              <i class="bi bi-pencil-square" />
              Editar
          </b-dropdown-item>
          <b-dropdown-item
            class="m-1"
            variant="outline-warning"
            @click="onClickExpedient(items.empleadoId)"
          >
            <i class="bi bi-archive-fill" />
            Expediente Digital
          </b-dropdown-item>
          <b-dropdown-item
            class="m-1"
            variant="outline-warning"
            @click="onClickMedicalExpedient(items.empleadoId)"
          >
            <i class="bi bi-journal-medical" />
            Expediente médico
          </b-dropdown-item>
        </b-dropdown>
      </template>
      <template #item-status="items">
        <b-badge :variant="items.archivado === false ? 'success': 'danger'">
        {{items.archivado === false ? 'Activo' : 'Archivado'}}
        </b-badge>
      </template>
    </EasyDataTable>
    <b-modal
      id="modal-employee"
      v-model="showModal"
      title="Agregar empleados"
      size="xl"
      hide-footer
      centered
      button-size="lg"
      lazy
    >
      <Form @submit="addEmployee">
        <b-row cols="3">
          <b-col>
            <b-form-group class="mt-3" label="Nombre Completo">
              <Field name="nameField" :rules="validateName" as="text">
                <b-form-input
                  v-model="EmployeesFields.nombreCompleto"
                  :state="nameState"
                />
              </Field>
              <ErrorMessage class="text-danger" name="nameField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Departamento">
              <Field
                name="DepartamentField"
                :rules="validateDepartament"
                as="number"
              >
                <b-form-select
                  v-model="EmployeesFields.departamentoId"
                  autofocus
                  :options="departaments"
                  value-field="departamentoId"
                  text-field="nombre"
                  :state="departamentState"
                  @input="getWorkStation(EmployeesFields.departamentoId)"
                >
                </b-form-select>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="DepartamentField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!-- <b-col>
            <b-form-group class="mt-3" label="Area">
              <Field name="AreaField" :rules="validateArea" as="number">
                <b-form-select
                  v-model="EmployeesFields.areaId"
                  autofocus
                  :options="areas"
                  value-field="areaId"
                  text-field="nombre"
                  :state="areaState"
                  @input="getWorkStation(EmployeesFields.areaId)"
                >
                </b-form-select>
              </Field>
              <ErrorMessage class="text-danger" name="AreaField"></ErrorMessage>
            </b-form-group>
          </b-col> -->
          <b-col>
            <b-form-group class="mt-3" label="Puesto de trabajo">
              <Field
                name="workStationField"
                :rules="validateWorkSation"
                as="number"
              >
                <b-form-select
                  v-model="EmployeesFields.puestoTrabajoId"
                  autofocus
                  :options="workStations"
                  value-field="puestoTrabajoId"
                  text-field="nombre"
                  :state="workStationState"
                >
                </b-form-select>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="workStationField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Fecha de Contratación">
              <Field name="DateWorkField" :rules="validateWorkDate" as="text">
                <Datepicker
                  v-model="EmployeesFields.fechaAlta"
                  locale="es"
                  autoApply
                  :enableTimePicker="false"
                  :state="dateWorkState"
                >
                </Datepicker>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="DateWorkField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            @click="resetEmployeesFields"
          >
            Cancelar
          </b-button>
          <b-button class="w-auto m-2" variant="success" type="submit">
            Guardar
          </b-button>
        </b-row>
      </Form>
    </b-modal>
  </b-card>
</template>

<script>
import EmployeeServices from '@/Services/employee.Services'
// import AreaServices from '@/Services/area.Services'
import DepartamentServices from '@/Services/departament.Services'
import workStationServices from '@/Services/workStation.Services'
import ExpedientdigitalServices from '@/Services/expedientdigital.Services'
import MunicipalMedicalServices from '@/Services/municipalMedical.Services'
import Datepicker from '@vuepic/vue-datepicker'
import { Form, Field, ErrorMessage } from 'vee-validate'
import { useRouter } from 'vue-router'
import { ref, inject } from 'vue'
// import { useToast } from 'vue-toast-notification'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    Datepicker,
    EasyDataTable: window['vue3-easy-data-table'],
    Form,
    Field,
    ErrorMessage
  },
  setup () {
    const { getEmployees, createEmployee, deleteEmployee } = EmployeeServices()
    const swal = inject('$swal')
    // const { getAreasByDepartament } = AreaServices()
    const { getDepartaments } = DepartamentServices()
    const { getWorkStationByDepartament } = workStationServices()
    const { createExpedient, getExpedientByEmpleadoId } = ExpedientdigitalServices()
    const { createExpedientMedical, getExpedientMedicalByEmpleadoId } = MunicipalMedicalServices()
    const redirect = useRouter()
    const showModal = ref(false)
    const employees = ref([])
    const departaments = ref([])
    const areas = ref([])
    const workStations = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('nombreCompleto')
    const nameState = ref(false)
    const dateState = ref(false)
    const dateWorkState = ref(false)
    // const areaState = ref(false)
    const workStationState = ref(false)
    const departamentState = ref(false)
    const expedientFieldBlank = ref({
      expedienteDigitalId: 0,
      empleadoId: null,
      archivado: false
    })
    const expedientmedicalBlank = ref({
      expedienteMedicoId: 0,
      empleadoId: null,
      archivado: false
    })
    const EmployeesFields = ref({
      empleadoId: 0,
      nombreCompleto: '',
      archivado: false,
      tieneExpediente: true,
      // areaId: null,
      usuarioId: null,
      departamentoId: null,
      expedienteDigitalId: null,
      puestoTrabajoId: null
    })
    const EmployeesFieldsBlank = ref(
      JSON.parse(JSON.stringify(EmployeesFields))
    )
    // const getAreas = departamentoId => {
    //   getAreasByDepartament(departamentoId, data => {
    //     areas.value = data
    //     if (data.length === 0) {
    //       swal.fire({
    //         title: 'No se encuentran areas registradas!',
    //         text:
    //           'No se encuentran areas registradas en el departamento seleccionado, registre primero una area para continuar.',
    //         icon: 'warning'
    //       })
    //     }
    //   })
    // }
    const getWorkStation = departamentoId => {
      getWorkStationByDepartament(departamentoId, data => {
        workStations.value = data
        if (data.length === 0) {
          swal.fire({
            title: 'No se encuentran puestos de trabajo registrados!',
            text:
              'No se encuentran puestos de trabajo registrados en el area seleccionado, registre primero un puesto de trabajo para continuar.',
            icon: 'warning'
          })
        }
      })
    }
    getDepartaments(data => {
      departaments.value = data
      if (data.length === 0) {
        swal.fire({
          title: 'No se encuentran departamentos registrados!',
          text:
            'No se encuentran departamentos registrados en el sistema, registre primero un departamento para continuar.',
          icon: 'warning'
        })
      }
    })
    const onClickExpedient = (empleadoId) => {
      getExpedientByEmpleadoId(empleadoId, data => {
        redirect.push({
          name: 'ExpedienteDigital-edit',
          params: { ExpedienteDigitalId: empleadoId }
        })
      })
    }
    const onClickMedicalExpedient = (empleadoId) => {
      getExpedientMedicalByEmpleadoId(empleadoId, data => {
        redirect.push({
          name: 'ServiciosMedicos-ExpedienteMedico-Edit',
          params: { ExpedienteMedicoId: empleadoId }
        })
      })
    }
    const validateName = () => {
      if (!EmployeesFields.value.nombreCompleto) {
        nameState.value = false
        return 'Este campo es requerido'
      }
      if (!EmployeesFields.value.nombreCompleto.trim().length > 0) {
        nameState.value = false
        return 'Este campo no puede contener solo espacios'
      }
      if (
        !/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(EmployeesFields.value.nombreCompleto)
      ) {
        nameState.value = false
        return 'El nombre solo puede contener letras'
      }
      nameState.value = true
      return true
    }
    const validateDepartament = () => {
      if (!EmployeesFields.value.departamentoId) {
        departamentState.value = false
        return 'Este campo es requerido'
      }
      departamentState.value = true
      return true
    }
    const validateDate = () => {
      if (!EmployeesFields.value.fechaNacimiento) {
        dateState.value = false
        return 'Este campo es requerido'
      }
      dateState.value = true
      return true
    }
    const validateWorkDate = () => {
      if (!EmployeesFields.value.fechaAlta) {
        dateWorkState.value = false
        return 'Este campo es requerido'
      }
      dateWorkState.value = true
      return true
    }
    // const validateArea = () => {
    //   if (!EmployeesFields.value.areaId) {
    //     areaState.value = false
    //     return 'Este campo es requerido'
    //   }
    //   areaState.value = true
    //   return true
    // }
    const validateWorkSation = () => {
      if (!EmployeesFields.value.puestoTrabajoId) {
        workStationState.value = false
        return 'Este campo es requerido'
      }
      workStationState.value = true
      return true
    }
    const fields = ref([
      { value: 'nombreCompleto', text: 'Nombre' },
      { value: 'departamentos.nombre', text: 'Departamento' },
      // { value: 'area.nombre', text: 'Area de Trabajo' },
      { value: 'puestoTrabajo.nombre', text: 'Puesto de Trabajo' },
      { value: 'fechaAlta', text: 'Fecha de contratación' },
      { value: 'status', text: 'Estado' },
      { value: 'actions', text: 'Acciones' }
    ])
    const resetEmployeesFields = () => {
      showModal.value = false
      EmployeesFields.value = JSON.parse(JSON.stringify(EmployeesFieldsBlank))
      nameState.value = false
      dateState.value = false
      dateWorkState.value = false
      // areaState.value = false
      departamentState.value = false
      workStationState.value = false
    }
    getEmployees(data => {
      employees.value = data
      if (employees.value.length > 0) {
        isloading.value = false
      } else {
        if (employees.value.length <= 0) {
          isloading.value = false
        }
      }
    })
    const onFiltered = filteredItems => {
      currentPage.value = 1
    }
    const refreshTable = () => {
      isloading.value = true
      getEmployees(data => {
        employees.value = data
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
        expedientFieldBlank.value.empleadoId = data.empleadoId
        createExpedient(expedientFieldBlank.value, data => {})
        expedientmedicalBlank.value.empleadoId = data.empleadoId
        createExpedientMedical(expedientmedicalBlank.value, data => {})
        refreshTable()
        swal.fire({
          title: '¡Empleado registrado correctamente!',
          text: 'El empleado se ha registrado al sistema satisfactoriamente.',
          icon: 'success'
        })
      })
      showModal.value = false
      resetEmployeesFields()
    }

    const RemoveEmployee = employeeId => {
      isloading.value = true
      swal
        .fire({
          title: '¿Estas seguro?',
          text: 'No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Si, Archivar empleado!',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            deleteEmployee(employeeId, data => {
              refreshTable()
            })
            swal.fire({
              title: '¡Empleado archivado!',
              text: 'El empleado ha sido archivado satisfactoriamente .',
              icon: 'success'
            })
          } else {
            isloading.value = false
          }
        })
    }
    return {
      employees,
      departaments,
      fields,
      perPage,
      currentPage,
      filter,
      perPageSelect,
      areas,
      workStations,
      expedientFieldBlank,
      EmployeesFieldsBlank,
      EmployeesFields,
      isloading,
      searchValue,
      searchField,
      nameState,
      dateState,
      dateWorkState,
      // areaState,
      workStationState,
      departamentState,
      showModal,
      expedientmedicalBlank,

      onFiltered,
      addEmployee,
      refreshTable,
      RemoveEmployee,
      validateName,
      // validateArea,
      validateDate,
      validateWorkDate,
      validateDepartament,
      validateWorkSation,
      // getAreas,
      getWorkStation,
      resetEmployeesFields,
      onClickExpedient,
      onClickMedicalExpedient
    }
  }
}
</script>

<style></style>
