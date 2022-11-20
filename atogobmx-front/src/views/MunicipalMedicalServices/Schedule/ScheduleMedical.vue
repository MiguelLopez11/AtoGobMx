<template>
  <b-card class="m-2">
    <b-card class="m-1 border border-3">
      <h1>Agenda Municipal</h1>
    </b-card>
    <b-row>
      <b-col>
        <vue-cal
          ref="refVueCal"
          style="height: 400px; width: 1000px; margin-left: 5px"
          class="vuecal--green-theme m-1"
          active-view="week"
          locale="es"
          :events="schedules"
          editable-events
          show-time-in-cells
          today-button
        />
      </b-col>
      <b-col>
        <b-card class="m-1 border border-3">
        <h1>Agendar cita</h1>
        <Form @submit="addMedicalAppointment">
        <b-row cols="1">
          <b-col>
            <b-form-group label="Empleado Citante">
              <Field
                name="NomenclatureField"
                as="text"
              >
                <!-- :rules="validateNomeclature" -->
                  <!-- v-model="medicalAppointmentFields.empleadoId" -->
                <b-form-select
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
                  <!-- v-model="medicalAppointmentFields.fechaAlta" -->
                <!-- :rules="validateBrand" -->
            <b-form-group
              label="Fecha y hora"
            >
              <Field
                name="BrandField"
                as="text"
              >
                <Datepicker
                  v-model="date"
                  range
                  auto-range="0"
                  selectText="Pick"
                  modelType="yyyy-MM-dd hh:mm"
                  locale="es"
                >
                </Datepicker>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="BrandField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group
              label="Descripcion"
            >
                <!-- :rules="validateTypeWeapon" -->
              <Field
                name="BrandField"
                as="text"
              >
                  <!-- v-model="medicalAppointmentFields.tipoArma" -->
                <b-form-textarea
                  placeholder="Describe el motivo de la cita"
                  :state="typeWeaponState"
                >
                </b-form-textarea>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="BrandField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
        </b-row>
        <b-button type="success" size="xl" variant="primary">
          Agregar
        </b-button>
        </Form>
        </b-card>
      </b-col>
    </b-row>
    {{ date }}
  </b-card>
</template>

<script>
import VueCal from 'vue-cal'
import { Form, Field, ErrorMessage } from 'vee-validate'
import Datepicker from '@vuepic/vue-datepicker'
import 'vue-cal/dist/vuecal.css'
import '@vuepic/vue-datepicker/dist/main.css'
import { ref } from 'vue'
export default {
  components: {
    VueCal,
    Form,
    Field,
    ErrorMessage,
    Datepicker
  },
  setup () {
    const refVueCal = ref()
    const date = ref([])
    const schedules = ref([{
      start: '2022-11-20 10:00',
      end: '2022-11-20 12:37',
      title: 'Nueva Cita',
      content: 'Una nueva prueba',
      class: 'sport'
    }])
    const addMedicalAppointment = () => {
      // console.log(date.value[0])
      schedules.value.push({
        start: `${date.value[0]}`,
        end: `${date.value[1]}`,
        title: 'Prueba',
        content: 'Pvta'
      })
      // refVueCal.value.createEvent(
      //   date.value,
      //   120,
      //   { title: 'New Event', content: 'yay! 🎉'}
      // )
    }
    return {
      refVueCal,
      schedules,
      date,

      addMedicalAppointment
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
}
.vuecal--green-theme .vuecal__title-bar {
    background-color: #ebe9f1;
    color: #fff;
}
.vuecal__event{
  border-radius: 5px;
  color: #fff;
   background-color: #7367f0;
}
</style>
