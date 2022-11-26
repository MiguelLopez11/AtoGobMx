<template>
  <b-card class="m-2">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-card>
      <div>
        <h3>Editar Direccion Cementerio</h3>
      </div>
      <Form @submit="onUpdateCementeryService">
        <b-row cols="2">
          <!--Agregar Nombre del propietario -->
          <b-col>
            <b-form-group class="mt-3" label="Nombre del propietario">
              <Field
                name="PropietaryField"
                :rules="validatePropietary"
                as="text"
              >
                <b-form-input
                  v-model="cementeryService.nombrePropietario"
                  :state="PropietaryState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="PropietaryField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--Agregar numero de espacios -->
          <b-col>
            <b-form-group class="mt-3" label="Numero de espacios">
              <Field name="SpacesField" :rules="validateSpaces" as="number">
                <b-form-input
                  v-model="cementeryService.numeroEspasios"
                  :state="SpacesState"
                  type="number"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="SpacesField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--Agregar Metros correspondientes -->
          <b-col>
            <b-form-group class="mt-3" label="Metros correspondientes">
              <Field name="MeterField" :rules="validateMeter" as="number">
                <b-form-input
                  v-model="cementeryService.metrosCorrespondientes"
                  :state="MeterState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="MeterField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--Agregar espacios disponibles -->
          <b-col>
            <b-form-group class="mt-3" label="Espacios Disponibles">
              <Field
                name="AvailableField"
                :rules="validateAvailable"
                as="number"
              >
                <b-form-input
                  v-model="cementeryService.espaciosDisponibles"
                  :state="AvailableState"
                  type="number"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="AvailableField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>

        </b-row>
        <b-row align-h="end">
          <b-button
            class="col-1 m-2 text-white"
            variant="primary"
            to="/ServiciosPublicos/Cementerios/list"
            type="reset"
          >
            Cancelar</b-button
          >
          <b-button type="success" class="col-1 m-2" variant="success">
            Guardar
          </b-button>
        </b-row>
      </Form>
    </b-card>
  </b-card>
</template>

<script>
import CementeryService from '@/Services/cementery.Services'
import { ref, inject } from 'vue'
import { useRoute, useRouter } from 'vue-router'
// import { useToast } from 'vue-toast-notification'
import { Form, Field, ErrorMessage } from 'vee-validate'
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
    // const $toast = useToast()
    const cementeryService = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const PropietaryState = ref(false)
    const SpacesState = ref(false)
    const MeterState = ref(false)
    const AvailableState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Cementerios', to: '/ServiciosPublicos/Cementerios/list' },
      { text: 'Editar-cementerios' }
    ])
    const onUpdateCementeryService = () => {
      updateCementery(cementeryService.value, (data) => {})
      swal.fire({
        title: '¡Cementerio modificado correctamente!',
        text: 'La cementerio se ha modificado  satisfactoriamente.',
        icon: 'success'
      }).then(result => {
        if (result.isConfirmed) {
          redirect.push('/ServiciosPublicos/Cementerios/list')
        }
      })
    }
    getCementeryById(router.params.CementeriosId, (data) => {
      cementeryService.value = data
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

      // if (!cementeryService.value.nombrePropietario.trim().length > 0) {
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

      // if (!cementeryService.value.numeroEspasios.trim().length > 0) {
      //   SpacesState.value = false
      //   return 'Este campo no puede contener espacios'
      // }

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

      // if (!cementeryService.value.metrosCorrespondientes.trim().length > 0) {
      //   MeterState.value = false
      //   return 'Este campo no puede contener espacios'
      // }

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

      // if (!cementeryService.value.espaciosDisponibles.trim().length > 0) {
      //   AvailableState.value = false
      //   return 'Este campo no puede contener espacios'
      // }

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
      breadcrumbItems,
      PropietaryState,
      SpacesState,
      MeterState,
      AvailableState,
      //   router

      onUpdateCementeryService,
      validateState,
      validatePropietary,
      validateSpaces,
      validateMeter,
      validateAvailable
    }
  }
}
</script>

<style>

</style>
