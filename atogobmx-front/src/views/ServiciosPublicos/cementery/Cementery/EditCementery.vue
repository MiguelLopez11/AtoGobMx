<template>
  <b-card class="m-2 mb-4">
    <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
  </b-card>
  <b-card class="m-2">
    <b-tabs content-class="mt-3">
      <b-tab title="Cementerios" active>
        <Form @submit="onUpdateCementeryService">
          <b-row cols="2">
            <!-- Agregar nombre -->
            <b-col>
              <b-form-group class="mt-3" label="Nombre del cementerio">
                <Field
                  name="NameCementeryField"
                  :rules="validateNameCementery"
                  as="text"
                >
                  <b-form-input
                    v-model="cementeryService.nombreCementerio"
                    :state="NameCementeryState"
                  >
                  </b-form-input>
                </Field>
                <ErrorMessage
                  class="text-danger"
                  name="NameCementeryField"
                ></ErrorMessage>
              </b-form-group>
            </b-col>
            <!-- Agregar comunidad -->
            <b-col>
              <b-form-group class="mt-3" label="Comunidad">
                  <b-form-input
                    v-model="cementeryService.comunidad"
                  >
                  </b-form-input>
              </b-form-group>
            </b-col>
            <!-- Agregar localidad -->
            <b-col>
              <b-form-group class="mt-3" label="Localidad">
                <Field name="LocationField" :rules="validateLocation" as="text">
                  <b-form-input
                    v-model="cementeryService.localidad"
                    :state="LocationState"
                  >
                  </b-form-input>
                </Field>
                <ErrorMessage
                  class="text-danger"
                  name="LocationField"
                ></ErrorMessage>
              </b-form-group>
            </b-col>
            <!-- Agregar calle -->
            <b-col>
              <b-form-group class="mt-3" label="Calle">
                <Field name="StreetField" :rules="validateStreet" as="text">
                  <b-form-input
                    v-model="cementeryService.calle"
                    :state="StreetState"
                    type="text"
                  >
                  </b-form-input>
                </Field>
                <ErrorMessage
                  class="text-danger"
                  name="StreetField"
                ></ErrorMessage>
              </b-form-group>
            </b-col>
            <!-- Agregar numero exterior -->
            <b-col>
              <b-form-group class="mt-3" label="Numero exterior">
                <Field
                  name="NumberOutsideField"
                  :rules="validateNumberOutside"
                  as="text"
                >
                  <b-form-input
                    v-model="cementeryService.numeroExterior"
                    :state="NumberOutsideState"
                    type="number"
                  >
                  </b-form-input>
                </Field>
                <ErrorMessage
                  class="text-danger"
                  name="NumberOutsideField"
                ></ErrorMessage>
              </b-form-group>
            </b-col>
          </b-row>
          <b-row cols="1">
            <b-card>
              <b-alert variant="warning" dismissible show
                >Arrastra el punto del mapa al lugar donde se encuentra su
                gabeta.</b-alert
              >
              <GMapMap
                :center="center"
                map-type-id="satellite"
                :zoom="20"
                :options="{
                  zoomControl: true,
                  mapTypeControl: false,
                  scaleControl: false,
                  rotateControl: true,
                  disableDefaultUi: false
                }"
                style="width: 100%; height: 500px"
              >
                <GMapMarker
                  :zoom="10"
                  :position="center"
                  :draggable="true"
                  @drag="updateCoordinates"
                />
              </GMapMap>
            </b-card>
          </b-row>
          <b-row cols="2" align-h="end">
            <b-col>
              <b-button
                class="w-75 m-2 text-white"
                variant="primary"
                to="/ServiciosPublicos/Cementerios/list"
                type="reset"
              >
                Cancelar</b-button
              >
            </b-col>
            <b-col>
              <b-button type="success" class="w-75 m-2" variant="success">
                Guardar
              </b-button>
            </b-col>
          </b-row>
        </Form>
      </b-tab>
      <b-tab title="Documentos">
        <ExpedientDocumentsCementery :CementerioId="cementerioId" />
      </b-tab>
    </b-tabs>
  </b-card>
</template>

<script>
import CementeryService from '@/Services/cementery.Services'
import ExpedientDocumentsCementery from './Documents/DocumentsCementery.vue'
import { ref, inject } from 'vue'
import { useRoute, useRouter } from 'vue-router'
// import { useToast } from 'vue-toast-notification'
import { Form, Field, ErrorMessage } from 'vee-validate'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    Form,
    Field,
    ErrorMessage,
    ExpedientDocumentsCementery
  },
  setup () {
    const swal = inject('$swal')
    const { getCementeryById, updateCementery } = CementeryService()
    // const $toast = useToast()
    const cementeryService = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const NameCementeryState = ref(false)
    const LocationState = ref(false)
    const StreetState = ref(false)
    const NumberOutsideState = ref(false)
    const cementerioId = ref(parseInt(router.params.CementerioId))
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      {
        text: 'Direccion cementerio',
        to: '/ServiciosPublicos/Cementerios/list'
      },
      { text: 'Editar-Estatus Alumbrado' }
    ])

    const markers = ref([
      {
        position: {
          lat: 0,
          lng: 0
        }
      }
    ])

    const center = ref({ lat: 0, lng: 0 })

    const updateCoordinates = location => {
      markers.value = {
        position: {
          lat: location.latLng.lat(),
          lng: location.latLng.lng()
        }
      }
      cementeryService.value.latitud = location.latLng.lat()
      cementeryService.value.longitud = location.latLng.lng()
    }

    const onUpdateCementeryService = () => {
      updateCementery(cementeryService.value, data => {})
      swal
        .fire({
          title: '¡Direccion cementerio modificado correctamente!',
          text: 'La direccion cementerio se ha modificado  satisfactoriamente.',
          icon: 'success'
        })
        .then(result => {
          if (result.isConfirmed) {
            redirect.push('/ServiciosPublicos/Cementerios/list')
          }
        })
    }

    getCementeryById(router.params.CementerioId, data => {
      cementeryService.value = data
      center.value = { lat: data.latitud, lng: data.longitud }
      markers.value.position = { lat: data.latitud, lng: data.longitud }
    })

    const validateNameCementery = () => {
      if (!cementeryService.value.nombreCementerio) {
        validateState()
        return 'Este campo es requerido'
      }

      if (
        !/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(
          cementeryService.value.nombreCementerio
        )
      ) {
        NameCementeryState.value = false
        return 'Este campo solo puede contener letras'
      }

      // if (!cementeryService.value.nombreCementerio.trim().length > 0) {
      //   NameCementeryState.value = false
      //   return 'Este campo no puede contener espacios'
      // }

      validateState()
      return true
    }

    const validateLocation = () => {
      if (!cementeryService.value.localidad) {
        validateState()
        return 'Este campo es requerido'
      }

      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(cementeryService.value.localidad)) {
        LocationState.value = false
        return 'Este campo solo puede contener letras'
      }

      // if (!cementeryService.value.localidad.trim().length > 0) {
      //   LocationState.value = false
      //   return 'Este campo no puede contener espacios'
      // }

      validateState()
      return true
    }

    const validateStreet = () => {
      if (!cementeryService.value.calle) {
        validateState()
        return 'Este campo es requerido'
      }

      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(cementeryService.value.calle)) {
        StreetState.value = false
        return 'Este campo solo puede contener letras'
      }

      // if (!cementeryService.value.calle.trim().length > 0) {
      //   StreetState.value = false
      //   return 'Este campo no puede contener espacios'
      // }

      validateState()
      return true
    }

    const validateNumberOutside = () => {
      if (!cementeryService.value.numeroExterior) {
        validateState()
        return 'Este campo es requerido'
      }

      if (!/^[0-9]+$/i.test(cementeryService.value.numeroExterior)) {
        NumberOutsideState.value = false
        return 'Este campo solo puede contener numeros'
      }

      // if (!cementeryService.value.numeroExterior.trim().length > 0) {
      //   NumberOutsideState.value = false
      //   return 'Este campo no puede contener espacios'
      // }

      validateState()
      return true
    }

    const validateState = () => {
      NameCementeryState.value = cementeryService.value.nombreCementerio !== ''
      LocationState.value = cementeryService.value.localidad !== ''
      StreetState.value = cementeryService.value.calle !== ''
      NumberOutsideState.value = cementeryService.value.numeroExterior !== ''
    }

    return {
      cementeryService,
      breadcrumbItems,
      NameCementeryState,
      LocationState,
      StreetState,
      NumberOutsideState,
      markers,
      cementerioId,
      center,
      //   router

      onUpdateCementeryService,
      validateState,
      validateNameCementery,
      validateLocation,
      validateStreet,
      validateNumberOutside,
      updateCoordinates
    }
  }
}
</script>

<style></style>
