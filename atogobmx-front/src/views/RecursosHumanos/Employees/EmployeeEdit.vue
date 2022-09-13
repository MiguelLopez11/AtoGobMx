<template>
  <b-card class="m-3">
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
              >
                <b-form-input
                  v-model="employee.nombreCompleto"
                  :state="nameState"
                />
              </Field>
              <ErrorMessage name="nameField"
                ><span>Este campo es requerido </span
                ><i class="bi bi-exclamation-circle"></i
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Fecha de nacimiento">
              <Field
                name="DateField"
                :rules="validateDate"
              >
                <Datepicker
                  v-model="employee.fechaNacimiento"
                  name="date"
                  locale="es"
                  autoApply
                  :enableTimePicker="false"
                  :state="dateState"
                >
                </Datepicker>
              </Field>
              <ErrorMessage name="DateField"
                ><span>Este campo es requerido </span
                ><i class="bi bi-exclamation-circle"></i
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Area">
              <Field
                name="AreaField"
                :rules="validateArea"
              >
                <b-form-select
                  v-model="employee.areaId"
                  autofocus
                  :options="areas"
                  value-field="areaId"
                  text-field="nombre"
                  :state="areaState"
                >
                </b-form-select>
              </Field>
              <ErrorMessage name="AreaField"
                ><span>Este campo es requerido </span
                ><i class="bi bi-exclamation-circle"></i
              ></ErrorMessage>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            v-b-modal.modal-employee
          >
            Cancelar
          </b-button>
          <b-button class="w-auto m-2" variant="success" type="submit"
            >Guardar</b-button
          >
        </b-row>
      </Form>
    </b-card>
  </b-card>
</template>

<script>
import EmployeeServices from '@/Services/employee.Services'
import AreaServices from '@/Services/area.Services'
import { Field, Form, ErrorMessage } from 'vee-validate'
import { ref } from 'vue'
import { useRoute } from 'vue-router'
import Datepicker from '@vuepic/vue-datepicker'
import { useToast } from 'vue-toast-notification'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    Datepicker,
    Field,
    Form,
    ErrorMessage
  },
  setup () {
    const { getEmployee, updateEmployee } = EmployeeServices()
    const { getAreas } = AreaServices()
    const $toast = useToast()
    const employee = ref([])
    const areas = ref([])
    const router = useRoute()
    const nameState = ref(false)
    const dateState = ref(false)
    const areaState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Empleados', to: '/Empleados/list' },
      { text: 'Editar-Empleados' }
    ])
    getEmployee(router.params.EmpleadoId, (data) => {
      employee.value = data
    })
    const onUpdateEmployee = () => {
      updateEmployee(employee.value, (data) => {})
      $toast.success('Empleado actualizado correctamente.', {
        position: 'top-right',
        duration: 1500
      })
    }
    getAreas((data) => {
      areas.value = data
    })
    const validateName = () => {
      if (!employee.value.nombreCompleto) {
        nameState.value = false
        return 'Este campo es requerido'
      }
      nameState.value = true
      return true
    }
    const validateDate = () => {
      if (!employee.value.fechaNacimiento) {
        dateState.value = false
        return 'Este campo es requerido'
      }
      dateState.value = true
      return true
    }
    const validateArea = () => {
      if (!employee.value.areaId) {
        areaState.value = false
        return 'Este campo es requerido'
      }
      areaState.value = true
      return true
    }

    return {
      employee,
      areas,
      breadcrumbItems,
      router,
      nameState,
      dateState,
      areaState,
      onUpdateEmployee,
      validateName,
      validateDate,
      validateArea
    }
  }
}
</script>

<style>
</style>
