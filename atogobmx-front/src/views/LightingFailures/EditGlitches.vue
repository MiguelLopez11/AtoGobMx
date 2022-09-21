<template>
  <b-card class="m-3">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-card>
      <div>
        <h3>Editar Fallas</h3>
      </div>
      <Form @submit="onUpdateLightingFailures">
        <b-row cols="3">
          <b-col>
            <b-form-group class="mt-3" label="Nombre">
              <Field name="faultNameField" :rules="validateFaultName">
                <b-form-input
                  v-model="lightingFailure.nombreFalla"
                  :state="FaultNameState"
                ></b-form-input>
              </Field>
              <ErrorMessage name="faultNameField"
                ><span>Este campo es requerido </span
                ><i class="bi bi-exclamation-circle"></i>
              </ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Fecha de falla">
              <Field name="DateField" :rules="validateDate">
                <Datepicker
                  locale="es"
                  name="Date"
                  :enableTimePicker="false"
                  autoApply
                  v-model="lightingFailure.fecha"
                  :state="dateState"
                ></Datepicker>
              </Field>
              <ErrorMessage name="DateField"
                ><span>Este campo es requerido </span
                ><i class="bi bi-exclamation-circle"></i>
              </ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Descripcion">
              <Field name="descriptionField" :rules="validateDescription">
                <b-form-textarea
                  v-model="lightingFailure.descripcion"
                  :state="descriptionState"
                ></b-form-textarea>
              </Field>
              <ErrorMessage name="descriptionField"
                ><span>Este campo es requerido </span
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
                ></b-form-textarea>
              </Field>
              <ErrorMessage name="addresdescriptionField"
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
    const FaultNameState = ref(false)
    const descriptionState = ref(false)
    const dateState = ref(false)
    const addresdescriptionState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'FallasAlumbrado', to: '/FallasAlumbrado/list' },
      { text: 'Editar-Fallas' }
    ])
    getLightingFailure(router.params.FallaId, (data) => {
      lightingFailure.value = data
      // eslint-disable-next-line no-unneeded-ternary
      FaultNameState.value = data.nombreFalla === null ? false : true
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

    const validateFaultName = () => {
      if (!lightingFailure.value.nombreFalla) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }

    const validateDescription = () => {
      if (!lightingFailure.value.descripcion) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }

    const validateDate = () => {
      if (!lightingFailure.value.fecha) {
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
      FaultNameState.value = lightingFailure.value.nombreFalla === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      descriptionState.value = lightingFailure.value.descripcion === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      dateState.value = lightingFailure.value.fecha === null ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      addresdescriptionState.value = lightingFailure.value.descripcionDomicilio === '' ? false : true
    }
    // getAreas((data) => {
    //   areas.value = data
    // })

    return {
      lightingFailure,
      breadcrumbItems,
      router,
      FaultNameState,
      descriptionState,
      dateState,
      addresdescriptionState,
      redirect,

      onUpdateLightingFailures,
      validateFaultName,
      validateDescription,
      validateDate,
      validateAddresdescription,
      validateState
    }
  }
}
</script>

<style>
</style>
