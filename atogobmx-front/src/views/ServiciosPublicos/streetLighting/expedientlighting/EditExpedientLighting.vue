<template>
  <b-card class="m-3">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-card>
      <div>
        <h3>Expediente Alumbrado</h3>
      </div>
      <Form @submit="onUpdateExpedientLighting">
        <b-row cols="2">
          <b-col>
            <b-form-group class="mt-3" label="Nombre del Tipo Tarea">
              <Field name="TypeTaskField" :rules="validateTypeTask">
                <b-form-select
                  v-model="expedientLighting.tarea"
                  autofocus
                  :state="TypeTaskState"
                  :options="statusPublicLighting"
                  value-field="estatusId"
                  text-field="nombreEstatus"
                ></b-form-select>
              </Field>
              <ErrorMessage name="TypeTaskField"
                ><span>Este campo es requerido </span
                ><i class="bi bi-exclamation-circle"></i>
              </ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Fecha Alta">
              <Field name="HighDateField" :rules="validateHighDate">
                <Datepicker
                  locale="es"
                  name="Date"
                  text-input
                  v-model="expedientLighting.fechaAlta"
                  :state="HighDateState"
                ></Datepicker>
              </Field>
              <ErrorMessage name="HighDateField"
                ><span>Este campo es requerido </span
                ><i class="bi bi-exclamation-circle"></i>
              </ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Domicilio">
              <Field name="DomicileField" :rules="validateDomicile">
                <b-form-input
                  v-model="expedientLighting.domicilio"
                  :state="DomicileState"
                ></b-form-input>
              </Field>
              <ErrorMessage name="DomicileField"
                ><span>Este campo es requerido </span
                ><i class="bi bi-exclamation-circle"></i>
              </ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Fecha Baja">
              <Field name="LowDateField" :rules="validateLowDate">
                <Datepicker
                  locale="es"
                  name="date"
                  text-input
                  v-model="expedientLighting.fechaBaja"
                  :state="LowDateState"
                ></Datepicker>
              </Field>
              <ErrorMessage name="LowDateField"
                ><span>Este campo es requerido llenarlo </span
                ><i class="bi bi-exclamation-circle"></i>
              </ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Descripcion Domicilio">
              <Field
                name="addresdescriptionField"
                :rules="validateAddresdescription"
              >
                <b-form-textarea
                  v-model="expedientLighting.descripcionDomicilio"
                  :state="addresdescriptionState"
                  rows="4"
                ></b-form-textarea>
              </Field>
              <ErrorMessage name="addresdescriptionField"
                ><span>Este campo es requerido llenarlo </span
                ><i class="bi bi-exclamation-circle"></i>
              </ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Solucion Del Problema">
              <Field
                name="DescriptionSolutionField"
                :rules="validateDescriptionSolution"
              >
                <b-form-textarea
                  v-model="expedientLighting.descripcionSolucion"
                  :state="DescriptionSolutionState"
                  rows="4"
                ></b-form-textarea>
              </Field>
              <ErrorMessage name="DescriptionSolutionField"
                ><span>Este campo es requerido </span
                ><i class="bi bi-exclamation-circle"></i>
              </ErrorMessage>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            to="/ExpedienteAlumbrado/list"
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
import ExpedientlightingServices from '@/Services/expedientlighting.Services'
// import StatusServices from '@/Services/statuslighting.Services'
import { Field, Form, ErrorMessage } from 'vee-validate'
// import AreaServices from '@/Services/area.Services'
import { ref } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import Datepicker from '@vuepic/vue-datepicker'
import { useToast } from 'vue-toast-notification'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  props: {
    expedientLightingId: Number
  },
  components: {
    Datepicker,
    Field,
    Form,
    ErrorMessage
  },
  setup (props) {
    const {
      getExpedientLightingById,
      updatExpedientLighting
    } = ExpedientlightingServices()
    // const { getStatusById } = StatusServices()
    const $toast = useToast()
    const expedientLighting = ref([])
    const statusPublicLighting = ref([])
    const router = useRoute()
    const redirect = useRouter()
    // const route = useRouter()
    const TypeTaskState = ref(false)
    const DescriptionSolutionState = ref(false)
    const HighDateState = ref(false)
    const addresdescriptionState = ref(false)
    const DomicileState = ref(false)
    const LowDateState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Alumbrado', to: '/ExpedienteAlumbrado/list' },
      { text: 'Editar-ExpedienteAlumbrado' }
    ])

    // getStatusById((data) => {
    //   statusPublicLighting.value = data
    // })

    getExpedientLightingById(
      router.params.ExpedienteAlumbradoId || props.expedientLightingId,
      data => {
        // streetLighting.value = data
        // validateState()
        expedientLighting.value = data
        // eslint-disable-next-line no-unneeded-ternary
        TypeTaskState.value = data.tarea === null ? false : true
      }
    )
    const onUpdateExpedientLighting = () => {
      updatExpedientLighting(expedientLighting.value, data => {})
      $toast.open({
        message: 'El expediente alumbrado se a modificado correcta mente',
        position: 'top',
        duration: 1000,
        dismissible: true,
        type: 'success',
        onDismiss: () => redirect.push('/ExpedienteAlumbrado/list')
      })
    }
    const validateTypeTask = () => {
      if (!expedientLighting.value.tarea) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateDescriptionSolution = () => {
      if (!expedientLighting.value.descripcionSolucion) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateDomicile = () => {
      if (!expedientLighting.value.domicilio) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateLowDate = () => {
      if (!expedientLighting.value.fechaBaja) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateHighDate = () => {
      if (!expedientLighting.value.fechaAlta) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateAddresdescription = () => {
      if (!expedientLighting.value.descripcionDomicilio) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateState = () => {
      // eslint-disable-next-line no-unneeded-ternary
      TypeTaskState.value = expedientLighting.value.tarea !== ''
      // eslint-disable-next-line no-unneeded-ternary
      HighDateState.value = expedientLighting.value.fechaAlta !== null
      // eslint-disable-next-line no-unneeded-ternary
      DomicileState.value = expedientLighting.value.domicilio !== ''
      // eslint-disable-next-line no-unneeded-ternary
      addresdescriptionState.value =
        expedientLighting.value.descripcionDomicilio !== ''
      // eslint-disable-next-line no-unneeded-ternary
      DescriptionSolutionState.value =
        expedientLighting.value.descripcionSolucion !== ''
      // eslint-disable-next-line no-unneeded-ternary
      LowDateState.value = expedientLighting.value.fechaBaja !== null
    }
    // getAreas((data) => {
    //   areas.value = data
    // })
    return {
      expedientLighting,
      breadcrumbItems,
      statusPublicLighting,
      router,
      TypeTaskState,
      DescriptionSolutionState,
      HighDateState,
      addresdescriptionState,
      DomicileState,
      LowDateState,
      onUpdateExpedientLighting,
      validateTypeTask,
      validateDescriptionSolution,
      validateHighDate,
      validateAddresdescription,
      validateLowDate,
      validateDomicile
    }
  }
}
</script>

<style></style>
