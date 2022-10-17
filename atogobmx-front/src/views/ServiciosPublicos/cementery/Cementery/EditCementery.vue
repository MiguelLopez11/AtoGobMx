<template>
  <b-card class="m-3">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-car>
      <div>
        <h3>Editar Cementerios</h3>
      </div>
      <Form @submit="onUpdateCementeryService">
        <b-row cols="2">
          <b-col>
            <!-- 1 -->
            <b-form-group class="mt-3" label="Nombre del propietario" as="text">
              <Field name="PropietaryField" :rules="validatePropietary">
                <b-form-input
                  v-modal="cementeryService.nombrePropietario"
                  :state="PropietaryState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage name="PropietaryField">
                <span>Este campo es requerido</span>
                <i class="bi bi-exclamation-circle" />
              </ErrorMessage>
            </b-form-group>
          </b-col>
          <!-- 2 -->
          <b-col>
            <b-form-group class="mt-3" label="Numero de espacios">
              <Field name="SpacesField" :rules="validateSpaces">
                <b-form-input
                  v-modal="cementeryService.numeroEspacios"
                  :state="SpacesState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage name="SpacesField">
                <span>Este campo es requerido</span>
                <i class="bi bi-exclamation-circle" />
              </ErrorMessage>
            </b-form-group>
          </b-col>
          <!-- 3 -->
          <b-col>
            <b-form-group class="mt-3" label="Metros que le corresponden">
              <Field name="MeterField" :rules="validateMeter">
                <b-form-input
                  v-modal="cementeryService.metrosCorrespondientes"
                  :state="MeterState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage name="MeterField">
                <span>Este campo es requerido</span>
                <i class="bi bi-exclamation-circle" />
              </ErrorMessage>
            </b-form-group>
          </b-col>
          <!-- 4 -->
          <b-col>
            <b-form-group class="mt-3" label="Espacios Disponibles">
              <Field name="AvailableField" :rules="validateAvailable">
                <b-form-input
                  v-modal="cementeryService.espaciosDisponibles"
                  :state="AvailableState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage name="AvailableField">
                <span>Este campo es requerido</span>
                <i class="bi bi-exclamation-circle" />
              </ErrorMessage>
            </b-form-group>
          </b-col>
        </b-row>

        <b-row align-h="end">
          <b-button
            class="col-1 m-2 text-white"
            variant="primary"
            to=""
            type="reset"
          >
            Cancelar</b-button
          >
          <b-button type="success" class="col-1 m-2" variant="success">
            Guardar
          </b-button>
        </b-row>
      </Form>
    </b-car>
  </b-card>
</template>

<script>
import CementeryService from '@/Services/cementery.Services'
import { Form, Field, ErrorMessage } from 'vee-validate'
import { ref } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { useToast } from 'vue-toast-notification'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    Form,
    Field,
    ErrorMessage
  },
  setup () {
    const { getCementeryById, updateCementery } = CementeryService()
    const $toast = useToast()
    const cementeryService = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const PropietaryState = ref(false)
    const SpacesState = ref(false)
    const MeterState = ref(false)
    const AvailableState = ref(false)
    const breadcrumbItems = ref([
      { Text: 'Inicio', to: '/' },
      { Text: 'Inventario cementerio', to: '/Cementerios/list' },
      { Text: 'Editar-Cementerios' }
    ])

    const onUpdateCementeryService = () => {
      updateCementery(CementeryService.value, data => {})
      $toast.open({
        message: 'Cementerio modificado correctamente',
        position: 'top',
        duration: 2000,
        dismissible: true,
        type: 'success',
        onDismiss: () => redirect.push('/Cementerios/list')
      })
    }
    getCementeryById(router.params.CementeriosId, data => {
      CementeryService.value = data
    })

    const validatePropietary = () => {
      if (!cementeryService.value.nombrePropietario) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }

    const validateSpaces = () => {
      if (!cementeryService.value.numeroEspasios) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }

    const validateMeter = () => {
      if (!cementeryService.value.metrosCorrespondientes) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }

    const validateAvailable = () => {
      if (!cementeryService.value.espaciosDisponibles) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }

    const validateState = () => {
      // eslint-disable-next-line no-unneeded-ternary
      PropietaryState.value = cementeryService.value.nombrePropietario === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      SpacesState.value = cementeryService.value.numeroEspasios === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      MeterState.value = cementeryService.value.metrosCorrespondientes === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      AvailableState.value = cementeryService.value.espaciosDisponibles === '' ? false : true
    }

    return {
      cementeryService,
      PropietaryState,
      SpacesState,
      MeterState,
      AvailableState,
      breadcrumbItems,

      onUpdateCementeryService,
      validatePropietary,
      validateSpaces,
      validateMeter,
      validateAvailable
    }
  }
}
</script>

<style></style>
