<template>
  <b-card class="m-2">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-card>
      <div>
        <h3>Editar empleado</h3>
      </div>
      <Form @submit="onUpdateEmployee">
        <b-row cols="3">
          <b-col>
            <b-form-group class="mt-3" label="Nombre Completo">
              <Field
                name="nameField"
                :rules="validateName"
                as="text"
              >
                <b-form-input
                  v-model="employee.nombreCompleto"
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
                  v-model="employee.departamentoId"
                  autofocus
                  :options="departaments"
                  value-field="departamentoId"
                  text-field="nombre"
                  :state="departamentState"
                  @input="getWorkStation(employee.departamentoId)"
                >
                </b-form-select>
              </Field>
              <ErrorMessage class="text-danger" name="DepartamentField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <!-- <b-col>
            <b-form-group class="mt-3" label="Area">
              <Field
                name="AreaField"
                :rules="validateArea"
                as="number"
              >
                <b-form-select
                  v-model="employee.areaId"
                  autofocus
                  :options="areas"
                  value-field="areaId"
                  text-field="nombre"
                  :state="areaState"
                  @input="getWorkStation(employee.areaId)"
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
                  v-model="employee.puestoTrabajoId"
                  autofocus
                  :options="workStations"
                  value-field="puestoTrabajoId"
                  text-field="nombre"
                  :state="workStationState"
                >
                </b-form-select>
              </Field>
              <ErrorMessage class="text-danger" name="workStationField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Fecha de Contratación">
              <Field
                name="DateWorkField"
                :rules="validateWorkDate"
                as="date"
              >
                <Datepicker
                  v-model="employee.fechaAlta"
                  locale="es"
                  autoApply
                  :enableTimePicker="false"
                  :state="dateWorkState"
                >
                </Datepicker>
              </Field>
              <ErrorMessage class="text-danger" name="DateWorkField"></ErrorMessage>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="col-1 m-2 text-white"
            variant="primary"
            to="/RecursosHumanos/Empleados/list"
            type="reset"
          >
            Cancelar</b-button
          >
          <b-button type="success" class="col-1 m-2" variant="success">
            Guardar
          </b-button>
        </b-row>
      </Form>
    </b-card>
  </b-card>
</template>

<script>
import EmployeeServices from '@/Services/employee.Services'
// import AreaServices from '@/Services/area.Services'
import DepartamentServices from '@/Services/departament.Services'
import workStationServices from '@/Services/workStation.Services'
import { Field, Form, ErrorMessage } from 'vee-validate'
import { ref, inject } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import Datepicker from '@vuepic/vue-datepicker'
// import { useToast } from 'vue-toast-notification'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    Datepicker,
    Field,
    Form,
    ErrorMessage
  },
  setup () {
    const swal = inject('$swal')
    const { getEmployee, updateEmployee } = EmployeeServices()
    // const { getAreasByDepartament } = AreaServices()
    const { getDepartaments } = DepartamentServices()
    const { getWorkStationByDepartament } = workStationServices()
    // const $toast = useToast()
    const employee = ref([])
    // const areas = ref([])
    const departaments = ref([])
    const workStations = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const nameState = ref(false)
    const areaState = ref(false)
    const dateWorkState = ref(false)
    const workStationState = ref(false)
    const departamentState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Empleados', to: '/RecursosHumanos/Empleados/list' },
      { text: 'Editar-Empleados' }
    ])
    getEmployee(router.params.EmpleadoId, (data) => {
      employee.value = data
      // getAreas(data.departamentoId)
      getWorkStation(data.departamentoId)
      validateState()
    })
    getDepartaments(data => {
      departaments.value = data
      if (data.length === 0) {
        swal.fire({
          title: 'No se encuentran departamentos registrados!',
          text: 'No se encuentran departamentos registrados en el sistema, registre primero un departamento para continuar.',
          icon: 'error'
        })
      }
    })

    // const getAreas = (departamentoId) => {
    //   if (departamentoId) {
    //     getAreasByDepartament(departamentoId, data => {
    //       areas.value = data
    //       if (data.length === 0) {
    //         swal.fire({
    //           title: 'No se encuentran areas registradas!',
    //           text: 'No se encuentran areas registradas en el departamento seleccionado, registre primero una area para continuar.',
    //           icon: 'error'
    //         })
    //       }
    //     })
    //   }
    // }
    const getWorkStation = (departamentoId) => {
      if (departamentoId) {
        getWorkStationByDepartament(departamentoId, data => {
          workStations.value = data
          if (data.length === 0) {
            swal.fire({
              title: 'No se encuentran puestos de trabajo registrados!',
              text: 'No se encuentran puestos de trabajo registrados en el area seleccionado, registre primero un puesto de trabajo para continuar.',
              icon: 'error'
            })
          }
        })
      }
    }
    const onUpdateEmployee = () => {
      updateEmployee(employee.value, (data) => {
        swal.fire({
          title: '¡Empleado modificado correctamente!',
          text: 'El empleado se ha modificado  satisfactoriamente.',
          icon: 'success'
        }).then(result => {
          if (result.isConfirmed) {
            redirect.push('/RecursosHumanos/Empleados/list')
          }
        })
      })
    }
    const validateName = () => {
      if (!employee.value.nombreCompleto) {
        validateState()
        return 'Este campo es requerido'
      }
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(employee.value.nombreCompleto)) {
        nameState.value = false
        return 'El nombre solo puede contener letras'
      }
      validateState()
      return true
    }
    // const validateArea = () => {
    //   if (!employee.value.areaId) {
    //     validateState()
    //     return 'Este campo es requerido'
    //   }
    //   validateState()
    //   return true
    // }
    const validateDepartament = () => {
      if (!employee.value.departamentoId) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateWorkSation = () => {
      if (!employee.value.puestoTrabajoId) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateWorkDate = () => {
      if (!employee.value.fechaAlta) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateState = () => {
      nameState.value = employee.value.nombreCompleto !== ''
      // areaState.value = employee.value.areaId !== null
      departamentState.value = employee.value.departamentoId !== null
      workStationState.value = employee.value.puestoTrabajoId !== null
      dateWorkState.value = employee.value.fechaAlta !== null
      return ''
    }

    return {
      employee,
      // areas,
      workStations,
      departaments,
      breadcrumbItems,
      router,
      nameState,
      areaState,
      departamentState,
      workStationState,
      dateWorkState,
      redirect,
      onUpdateEmployee,
      validateName,
      // validateArea,
      validateState,
      // getAreas,
      getWorkStation,
      validateDepartament,
      validateWorkSation,
      validateWorkDate
    }
  }
}
</script>

<style>
</style>
