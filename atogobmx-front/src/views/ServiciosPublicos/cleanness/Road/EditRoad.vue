<template>
  <b-card class="m-2">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-card>
      <div>
        <h3>Editar ruta</h3>
      </div>
      <Form @submit="onUpdateRoadService">
        <b-row cols="2">
          <!-- 1 -->
          <!-- <b-col>
            <b-form-group class="mt-3" label="Origen">
              <Field
                name="OriginField"
                :rules="validateOrigin"
                as="text"
              >
                <b-form-input
                  v-model="roadService.origen"
                  :state="OriginState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="OriginField"
              ></ErrorMessage>
            </b-form-group>
          </b-col> -->
          <!-- 2 -->
          <!-- <b-col>
            <b-form-group class="mt-3" label="Destino">
              <Field name="DestinationField" :rules="validateDestination" as="text">
                <b-form-input
                  v-model="roadService.destino"
                  :state="DestinationState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="DestinationField"
              ></ErrorMessage>
            </b-form-group>
          </b-col> -->
          <!-- Agregar Observacion -->
          <b-col>
            <b-form-group class="mt-3" label="Observacion">
              <Field name="ObservationField" :rules="validateObservation" as="text">
                <b-form-input
                  v-model="roadService.obsevacion"
                  :state="ObservationState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="ObservationField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>

        </b-row>
        <b-row align-h="end">
          <b-button
            class="col-1 m-2 text-white"
            variant="primary"
            to="/ServiciosPublicos/Ruta/list"
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
import RoadService from '@/Services/road.Services'
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
    const { getRoadById, updateRoad } = RoadService()
    // const $toast = useToast()
    const roadService = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const OriginState = ref(false)
    const DestinationState = ref(false)
    const ObservationState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Ruta', to: '/ServiciosPublicos/Ruta/list' },
      { text: 'Editar-ruta' }
    ])
    const onUpdateRoadService = () => {
      updateRoad(roadService.value, (data) => {})
      swal.fire({
        title: '¡Ruta modificado correctamente!',
        text: 'La ruta se ha modificado  satisfactoriamente.',
        icon: 'success'
      }).then(result => {
        if (result.isConfirmed) {
          redirect.push('/ServiciosPublicos/Ruta/list')
        }
      })
    }
    getRoadById(router.params.RutaId, (data) => {
      roadService.value = data
    })

    // const validateOrigin = () => {
    //   if (!roadService.value.origen) {
    //     validateState()
    //     return 'Este campo es requerido'
    //   }

    //   if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(roadService.value.origen)) {
    //     OriginState.value = false
    //     return 'Este campo solo puede contener letras'
    //   }

    //   validateState()
    //   return true
    // }

    // const validateDestination = () => {
    //   if (!roadService.value.destino) {
    //     validateState()
    //     return 'Este campo es requerido'
    //   }

    //   if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(roadService.value.destino)) {
    //     DestinationState.value = false
    //     return 'Este campo solo puede contener numeros'
    //   }

    //   validateState()
    //   return true
    // }

    const validateObservation = () => {
      if (!roadService.value.obsevacion) {
        validateState()
        return 'Este campo es requerido'
      }

      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ ,;:. 0-9]+$/i.test(roadService.value.obsevacion)) {
        ObservationState.value = false
        return 'Este campo solo puede contener numeros'
      }

      validateState()
      return true
    }

    const validateState = () => {
      // eslint-disable-next-line no-unneeded-ternary
      OriginState.value = roadService.value.origen === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      DestinationState.value = roadService.value.destino === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      ObservationState.value = roadService.value.obsevacion === '' ? false : true
    }

    return {
      roadService,
      breadcrumbItems,
      OriginState,
      DestinationState,
      ObservationState,
      //   router

      onUpdateRoadService,
      validateState,
      // validateOrigin,
      // validateDestination,
      validateObservation
    }
  }
}
</script>

<style>

</style>
