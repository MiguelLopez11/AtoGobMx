<template>
  <b-card class="m-2">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-card>
      <div>
        <h3>Editar Cita</h3>
      </div>
      <Form @submit="onUpdateMedicalAppointment">
        <b-row cols="1">
          <b-col>
            <b-form-group label="Empleado Citante">
              <Field name="EmployeeField" as="text" :rules="validateEmployee">
                <b-form-select
                  v-model="medicalAppointment.empleadoId"
                  autofocus
                  :state="employeeState"
                  value-field="empleadoId"
                  text-field="nombreCompleto"
                  :options="employees"
                >
                </b-form-select>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="EmployeeField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group label="Motivo">
              <Field name="ReasonField" as="text" :rules="validateReason">
                <b-form-input
                  v-model="medicalAppointment.motivo"
                  placeholder="Ingresa el motivo de la cita"
                  size="xl"
                  :state="reasonState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="ReasonField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <!-- :rules="validateBrand" -->
            <b-form-group label="Fecha y hora">
              <Field name="DateField" as="text" :rules="validateDate">
                <Datepicker
                  v-model="date"
                  range
                  placeholder="Seleccionar ..."
                  auto-range="0"
                  selectText="Seleccionar"
                  cancelText="Cancelar"
                  modelType="yyyy-MM-dd HH:mm"
                  locale="es"
                  :state="dateState"
                >
                </Datepicker>
              </Field>
              <ErrorMessage class="text-danger" name="DateField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group label="Descripcion">
              <!--  -->
              <Field
                name="descriptionField"
                as="text"
                :rules="validateDescription"
              >
                <b-form-textarea
                  v-model="medicalAppointment.descripcion"
                  placeholder="Describe el motivo de la cita"
                  :state="descriptionState"
                >
                </b-form-textarea>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="descriptionField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            to="/ServiciosMedicos/Cita/list"
          >
            Cancelar
          </b-button>
          <b-button class="w-auto m-2" variant="success" type="submit">
            Guardar
          </b-button>
        </b-row>
      </Form>
    </b-card>
  </b-card>
</template>

<script>
import MunicipalMedicalServices from '@/Services/municipalMedical.Services'
import { ref, inject } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import Datepicker from '@vuepic/vue-datepicker'
import { Form, Field, ErrorMessage } from 'vee-validate'
import EmployeeServices from '@/Services/employee.Services'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    Form,
    Field,
    ErrorMessage,
    Datepicker
  },
  setup () {
    const { getMedicalAppointment, updateMedicalAppointment } =
      MunicipalMedicalServices()
    const { getEmployeesUnfiled } = EmployeeServices()
    const swal = inject('$swal')
    const medicalAppointment = ref([])
    const employees = ref([])
    const date = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const employeeState = ref(false)
    const reasonState = ref(false)
    const dateState = ref(false)
    const descriptionState = ref(false)
    const citaId = ref(parseInt(router.params.CitaId))
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Citas', to: '/ServiciosMedicos/Cita/list' },
      { text: 'Editar cita' }
    ])
    const onUpdateMedicalAppointment = () => {
      medicalAppointment.value.fechaDesde = date.value[0]
      medicalAppointment.value.fechaHasta = date.value[1]
      updateMedicalAppointment(medicalAppointment.value, data => {
        swal
          .fire({
            title: 'Cita modificada correctamente!',
            text: 'La cita se ha modificado  satisfactoriamente.',
            icon: 'success'
          })
          .then(result => {
            if (result.isConfirmed) {
              redirect.push('/ServiciosMedicos/Cita/list')
            }
          })
      })
    }
    getMedicalAppointment(citaId.value, data => {
      medicalAppointment.value = data
      date.value.push(data.fechaDesde)
      date.value.push(data.fechaHasta)
      validateState()
    })
    getEmployeesUnfiled(data => {
      employees.value = data
    })
    // VALIDATIONS
    const validateEmployee = () => {
      if (!medicalAppointment.value.empleadoId) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateReason = () => {
      if (!medicalAppointment.value.motivo) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateDate = () => {
      if (!date.value[0]) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateDescription = () => {
      if (!medicalAppointment.value.descripcion) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateState = () => {
      employeeState.value = medicalAppointment.value.empleadoId !== null
      reasonState.value = medicalAppointment.value.motivo !== '' && medicalAppointment.value.motivo !== null
      dateState.value = date.value[0] !== '' && date.value[0] !== null
      descriptionState.value = medicalAppointment.value.descripcion !== '' && medicalAppointment.value.descripcion !== null
      return ''
    }
    return {
      medicalAppointment,
      breadcrumbItems,
      citaId,
      employees,
      date,
      employeeState,
      reasonState,
      dateState,
      descriptionState,

      validateEmployee,
      validateReason,
      validateDate,
      validateDescription,
      onUpdateMedicalAppointment,
      validateState
    }
  }
}
</script>

<style></style>
