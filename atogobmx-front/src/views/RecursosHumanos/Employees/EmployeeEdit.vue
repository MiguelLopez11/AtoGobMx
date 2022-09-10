<template>
  <b-card class="m-3">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-card>
      <div>
        <h3>Editar empleado</h3>
      </div>
      <Form  @submit="onUpdateEmployee">
        <b-row cols="3">
          <b-col>
            <b-form-group class="mt-3" label="Nombre">
              <Field
                name="nameField"
                class="form-control"
                v-model="employee.nombreCompleto"
                :rules="validateName"
              />
              <ErrorMessage name="nameField" style="color:red;"/>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Fecha de nacimiento">
              <Datepicker
                locale="es"
                :enableTimePicker="false"
                autoApply
                v-model="employee.fechaNacimiento"
              ></Datepicker>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Area">
              <b-form-select
                autofocus
                :options="areas"
                value-field="areaId"
                text-field="nombre"
                v-model="employee.areaId"
              ></b-form-select>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="col-1 m-2 text-white"
            variant="primary"
            to="/Empleados/list"
            type="reset"
            >Cancelar</b-button
          >
          <b-button class="col-1 m-2" variant="success" type="submit">Guardar</b-button>
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
    const validateName = (value) => {
      if (!value) {
        return 'Este campo es requerido'
      }
      return true
    }

    return {
      employee,
      areas,
      breadcrumbItems,
      router,
      validateName,

      onUpdateEmployee
    }
  }
}
</script>

<style>
</style>
