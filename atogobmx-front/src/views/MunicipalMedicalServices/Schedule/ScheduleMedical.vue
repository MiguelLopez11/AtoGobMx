<template>
  <b-card class="m-2">
    <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
  </b-card>
  <b-card class="m-2">
    <b-card class="m-1 border border-3">
      <h3>Agenda Municipal</h3>
    </b-card>
    <b-row>
      <b-col>
        <vue-cal
          style="height: 600px; width: 100%; margin-left: 2px; margin-top: 5px"
          class="vuecal--green-theme"
          active-view="week"
          locale="es"
          :events="schedules"
          :on-event-click="onEventClick"
        />
      </b-col>
      <b-col>
        <b-card class="m-1 border border-3" style="height: 600px">
          <h2>Agendar cita</h2>
          <Form @submit="addMedicalAppointment">
            <b-row cols="1">
              <b-col>
                <b-form-group label="Empleado Citante">
                  <Field
                    name="NomenclatureField"
                    as="text"
                    :rules="validateEmployee"
                  >
                    <b-form-select
                      v-model="schedulesFields.empleadoId"
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
                    name="NomenclatureField"
                  ></ErrorMessage>
                </b-form-group>
              </b-col>
              <b-col>
                <b-form-group label="Motivo">
                  <Field name="ReasonField" as="text" :rules="validateReason">
                    <b-form-input
                      v-model="schedulesFields.motivo"
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
                  <ErrorMessage
                    class="text-danger"
                    name="DateField"
                  ></ErrorMessage>
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
                      v-model="schedulesFields.descripcion"
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
            <b-button
              type="success"
              style="
                background-color: rgb(94, 80, 238);
                height: 50px;
                width: auto;
                font-size: 18px;
                margin-right: 15px;
                margin-left: 20px;
              "
            >
              Agregar
            </b-button>
          </Form>
        </b-card>
      </b-col>
    </b-row>
  </b-card>
  <b-modal
    v-model="showModal"
    title="Detalles Cita"
    size="xl"
    centered
    hide-footer
  >
    <strong style="font-size: 30px">
      Detalles:
      <br />
    </strong>
    <ul v-if="showModal" style="font-size: 25px">
      <li>Fecha: {{ scheduleSelected.start.format('DD/MM/YYYY') }}</li>
      <li>
        Desde: {{ scheduleSelected.start.formatTime() }} Hasta:
        {{ scheduleSelected.end.formatTime() }}
      </li>
      <li>Detalles: {{ scheduleSelected.content }}</li>
    </ul>
  </b-modal>
</template>

<script>
import VueCal from 'vue-cal'
import { Form, Field, ErrorMessage } from 'vee-validate'
import Datepicker from '@vuepic/vue-datepicker'
import MunicipalMedicalServices from '@/Services/municipalMedical.Services'
import EmployeeServices from '@/Services/employee.Services'
import { useRouter } from 'vue-router'
import 'vue-cal/dist/vuecal.css'
import '@vuepic/vue-datepicker/dist/main.css'
import { ref, inject } from 'vue'
export default {
  components: {
    VueCal,
    Form,
    Field,
    ErrorMessage,
    Datepicker
  },
  setup () {
    const swal = inject('$swal')
    const { getEmployeesUnfiled } = EmployeeServices()
    const { getMedicalAppointments, createMedicalAppointment } =
      MunicipalMedicalServices()
    const redirect = useRouter()
    const date = ref([])
    const employees = ref([])
    const schedules = ref([])
    const scheduleSelected = ref()
    const employeeState = ref(false)
    const reasonState = ref(false)
    const dateState = ref(false)
    const descriptionState = ref(false)
    const showModal = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      {
        text: 'Servicios medicos',
        to: '/ServiciosMedicosMunicipales'
      },
      { text: 'Agenda' }
    ])
    const schedulesFields = ref({
      citaId: 0,
      motivo: '',
      fechaDesde: null,
      fechaHasta: null,
      empleadoId: null,
      descripcion: '',
      archivado: false
    })
    const schedulesFieldsBlank = ref(
      JSON.parse(JSON.stringify(schedulesFields))
    )
    getEmployeesUnfiled(data => {
      employees.value = data
    })
    getMedicalAppointments(data => {
      for (let i = 0; i < data.length; i++) {
        schedules.value.push({
          start: data[i].fechaDesde,
          end: data[i].fechaHasta,
          title: data[i].motivo,
          content:
            data[i].empleados.nombreCompleto +
            ', ' +
            data[i].empleados.departamentos.nombre +
            ', ' +
            data[i].descripcion
        })
      }
    })
    const refreshSchedules = () => {
      schedules.value = []
      getMedicalAppointments(data => {
        for (let i = 0; i < data.length; i++) {
          schedules.value.push({
            start: data[i].fechaDesde,
            end: data[i].fechaHasta,
            title: data[i].motivo,
            content: data[i].descripcion
          })
        }
      })
    }
    const resetFields = () => {
      employeeState.value = false
      reasonState.value = false
      descriptionState.value = false
      dateState.value = false
      schedulesFields.value = JSON.parse(JSON.stringify(schedulesFieldsBlank))
      date.value = []
    }
    const addMedicalAppointment = () => {
      schedulesFields.value.fechaDesde = date.value[0]
      schedulesFields.value.fechaHasta = date.value[1]
      createMedicalAppointment(schedulesFields.value, data => {
        swal
          .fire({
            title: 'Cita registrada correctamente!',
            text: 'La cita se ha registrado al sistema satisfactoriamente.',
            icon: 'success'
          })
          .then(result => {
            if (result.isConfirmed) {
              redirect.push('/ServiciosMedicos/Cita/list')
            }
          })
      })
    }
    const onEventClick = event => {
      scheduleSelected.value = event
      showModal.value = true
    }
    // VALIDATIONS
    const validateEmployee = () => {
      if (!schedulesFields.value.empleadoId) {
        employeeState.value = false
        return 'Este campo es requerido'
      }
      employeeState.value = true
      return true
    }
    const validateReason = () => {
      if (!schedulesFields.value.motivo) {
        reasonState.value = false
        return 'Este campo es requerido'
      }
      reasonState.value = true
      return true
    }
    const validateDate = () => {
      if (!date.value[0]) {
        dateState.value = false
        return 'Este campo es requerido'
      }
      dateState.value = true
      return true
    }
    const validateDescription = () => {
      if (!schedulesFields.value.descripcion) {
        descriptionState.value = false
        return 'Este campo es requerido'
      }
      descriptionState.value = true
      return true
    }
    return {
      // DATA
      employees,
      schedules,
      breadcrumbItems,
      schedulesFields,
      scheduleSelected,
      schedulesFieldsBlank,
      date,
      employeeState,
      reasonState,
      dateState,
      descriptionState,
      showModal,
      // METHODS
      refreshSchedules,
      addMedicalAppointment,
      resetFields,
      onEventClick,
      // VALIDATIONS
      validateEmployee,
      validateReason,
      validateDate,
      validateDescription
    }
  }
}
</script>

<style>
.vuecal--green-theme .vuecal__menu,
.vuecal--green-theme .vuecal__cell-events-count {
  background-color: #7267f0c3;
  border-top-left-radius: 5px;
  border-top-right-radius: 5px;
  border-color: #ebe9f1;
}
.vuecal--green-theme .vuecal__title-bar {
  background-color: #ebe9f1;
  color: #fff;
}
.vuecal__event {
  border-radius: 5px;
  color: #fff;
  background-color: #7367f0;
}
</style>
