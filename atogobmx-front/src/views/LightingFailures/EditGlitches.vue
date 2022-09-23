<template>
  <b-card class="m-3">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-card>
      <b-tabs card>
        <b-tab title="Datos Generales" active>
          <div>
        <h3>Editar Fallas</h3>
      </div>
      <Form @submit="onUpdateLightingFailures">
        <b-row cols="2">
          <b-col>
            <b-form-group class="mt-3" label="Tipo Falla">
              <Field name="FaultTypeField" :rules="validateFaultType">
                <b-form-input
                  v-model="lightingFailure.tipoFalla"
                  :state="FaultTypeState"
                ></b-form-input>
              </Field>
              <ErrorMessage name="FaultTypeField"
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
                  :enableTimePicker="false"
                  autoApply
                  v-model="lightingFailure.fechaAlta"
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
                  v-model="lightingFailure.domicilio"
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
                  :enableTimePicker="false"
                  autoApply
                  v-model="lightingFailure.fechaBaja"
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
                  v-model="lightingFailure.descripcionDomicilio"
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
                  v-model="lightingFailure.descripcionSolucion"
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
            to="/FallasAlumbrado/list"
          >
            Cancelar
          </b-button>
          <b-button class="w-auto m-2" variant="success" type="submit"
            >Guardar</b-button
          >
        </b-row>
      </Form>
        </b-tab>
        <b-tab title="Tab 2">
        </b-tab>
      </b-tabs>
    </b-card>
  </b-card>
</template>

<script>
import LightingfailuresServices from '@/Services/lightingfailures.Services'
import { Field, Form, ErrorMessage } from 'vee-validate'
// import AreaServices from '@/Services/area.Services'
import { ref } from 'vue'
import { useRoute, useRouter } from 'vue-router'
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
    const { getLightingFailure, updateLightingFailures } =
      LightingfailuresServices()
    const $toast = useToast()
    const lightingFailure = ref([])
    const router = useRoute()
    const redirect = useRouter()
    // const route = useRouter()
    const FaultTypeState = ref(false)
    const DescriptionSolutionState = ref(false)
    const HighDateState = ref(false)
    const addresdescriptionState = ref(false)
    const DomicileState = ref(false)
    const LowDateState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'FallasAlumbrado', to: '/FallasAlumbrado/list' },
      { text: 'Editar-Fallas' }
    ])
    getLightingFailure(router.params.FallaId, (data) => {
      lightingFailure.value = data
      // eslint-disable-next-line no-unneeded-ternary
      FaultTypeState.value = data.tipoFalla === null ? false : true
    })
    const onUpdateLightingFailures = () => {
      updateLightingFailures(lightingFailure.value, (data) => {})
      $toast.open({
        message: 'La Falla se a modificado correcta mente',
        position: 'top',
        duration: 2000,
        dismissible: true,
        type: 'success',
        onDismiss: () => redirect.push('/FallasAlumbrado/list')
      })
    }

    const validateFaultType = () => {
      if (!lightingFailure.value.tipoFalla) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }

    const validateDescriptionSolution = () => {
      if (!lightingFailure.value.descripcionSolucion) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }

    const validateDomicile = () => {
      if (!lightingFailure.value.domicilio) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }

    const validateLowDate = () => {
      if (!lightingFailure.value.fechaBaja) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }

    const validateHighDate = () => {
      if (!lightingFailure.value.fechaAlta) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }

    const validateAddresdescription = () => {
      if (!lightingFailure.value.descripcionDomicilio) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }

    const validateState = () => {
      // eslint-disable-next-line no-unneeded-ternary
      FaultTypeState.value = lightingFailure.value.tipoFalla !== ''
      // eslint-disable-next-line no-unneeded-ternary
      HighDateState.value =
        lightingFailure.value.fechaAlta !== null
      // eslint-disable-next-line no-unneeded-ternary
      DomicileState.value = lightingFailure.value.domicilio !== ''
      // eslint-disable-next-line no-unneeded-ternary
      addresdescriptionState.value =
        lightingFailure.value.descripcionDomicilio !== ''
      // eslint-disable-next-line no-unneeded-ternary
      DescriptionSolutionState.value =
        lightingFailure.value.descripcionSolucion !== ''
      // eslint-disable-next-line no-unneeded-ternary
      LowDateState.value =
        lightingFailure.value.fechaBaja !== null
    }
    // getAreas((data) => {
    //   areas.value = data
    // })

    return {
      lightingFailure,
      breadcrumbItems,
      router,
      FaultTypeState,
      DescriptionSolutionState,
      HighDateState,
      addresdescriptionState,
      DomicileState,
      LowDateState,

      onUpdateLightingFailures,
      validateFaultType,
      validateDescriptionSolution,
      validateHighDate,
      validateAddresdescription,
      validateLowDate,
      validateDomicile
    }
  }
}
</script>

<style>
</style>
