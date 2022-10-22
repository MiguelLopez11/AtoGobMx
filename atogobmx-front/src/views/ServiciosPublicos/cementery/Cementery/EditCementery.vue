<template>
  <b-card class="m-2">
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
            <b-form-group class="mt-3" label="Nombre del propietario">
              <Field name="PropietaryField" :rules="validatePropietary" as="text">
                <b-form-input
                  v-modal="cementeryService.nombrePropietario"
                  :state="PropietaryState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage class="text-danger" name="PropietaryField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <!-- 2 -->
          <b-col>
            <b-form-group class="mt-3" label="Numero de espacios">
              <Field name="SpacesField" :rules="validateSpaces" as="number">
                <b-form-input
                  v-modal="cementeryService.numeroEspacios"
                  :state="SpacesState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage class="text-danger" name="SpacesField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <!-- 3 -->
          <b-col>
            <b-form-group class="mt-3" label="Metros que le corresponden">
              <Field name="MeterField" :rules="validateMeter" as="number">
                <b-form-input
                  v-modal="cementeryService.metrosCorrespondientes"
                  :state="MeterState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage class="text-danger" name="MeterField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <!-- 4 -->
          <b-col>
            <b-form-group class="mt-3" label="Espacios Disponibles">
              <Field name="AvailableField" :rules="validateAvailable" as="number">
                <b-form-input
                  v-modal="cementeryService.espaciosDisponibles"
                  :state="AvailableState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage class="text-danger" name="AvailableField"></ErrorMessage>
            </b-form-group>
          </b-col>
        </b-row>

        <b-row align-h="end">
          <b-button
            class="col-1 m-2 text-white"
            variant="primary"
            to="/Cementerios/list"
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
import { ref, inject } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    Form,
    Field,
    ErrorMessage
  },
  setup () {
    const swal = inject('$swal')
    const { getCementeryById, updateCementery } = CementeryService()
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
      swal
        .fire({
          title: '¡Cementerio modificado correctamente!',
          text: 'El cementerio se ha modificado  satisfactoriamente.',
          icon: 'success'
        })
        .then(result => {
          if (result.isConfirmed) {
            redirect.push('/Cementerios/list')
          }
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

      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(cementeryService.value.nombrePropietario)) {
        PropietaryState.value = false
        return 'Este campo solo puede contener letras'
      }
      // if (!cementeryServiceFields.value.nombrePropietario.trim().length > 0) {
      //   PropietaryState.value = false
      //   return 'Este campo no puede contener espacios'
      // }

      validateState()
      return true
    }

    const validateSpaces = () => {
      if (!cementeryService.value.numeroEspasios) {
        validateState()
        return 'Este campo es requerido'
      }

      if (!/^[0-9]+$/i.test(cementeryService.value.numeroEspasios)) {
        SpacesState.value = false
        return 'Este campo solo puede contener numeros'
      }

      validateState()
      return true
    }

    const validateMeter = () => {
      if (!cementeryService.value.metrosCorrespondientes) {
        validateState()
        return 'Este campo es requerido'
      }

      if (!/^\d*\.\d+$/i.test(cementeryService.value.metrosCorrespondientes)) {
        MeterState.value = false
        return 'Este campo solo puede contener numeros'
      }

      validateState()
      return true
    }

    const validateAvailable = () => {
      if (!cementeryService.value.espaciosDisponibles) {
        validateState()
        return 'Este campo es requerido'
      }

      if (!/^[0-9]+$/i.test(cementeryService.value.espaciosDisponibles)) {
        AvailableState.value = false
        return 'Este campo solo puede contener numeros'
      }

      validateState()
      return true
    }

    const validateState = () => {
      // eslint-disable-next-line no-unneeded-ternary
      PropietaryState.value =
        cementeryService.value.nombrePropietario !== ''
      // eslint-disable-next-line no-unneeded-ternary
      SpacesState.value =
        cementeryService.value.numeroEspasios !== ''
      // eslint-disable-next-line no-unneeded-ternary
      MeterState.value =
        cementeryService.value.metrosCorrespondientes !== ''
      // eslint-disable-next-line no-unneeded-ternary
      AvailableState.value =
        cementeryService.value.espaciosDisponibles !== ''
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
