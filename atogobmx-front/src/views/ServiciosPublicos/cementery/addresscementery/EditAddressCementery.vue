<template>
  <b-card class="m-2 mb-4">
    <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
  </b-card>
  <b-card class="m-2">
    <b-tabs content-class="mt-3">
      <b-tab title="Direccion Cementerios" active>
          <Form @submit="onUpdateAddressCementeryService">
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
                      v-model="addressCementeryService.nombreCementerio"
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
              <!-- Agregar municipio -->
              <b-col>
                <b-form-group class="mt-3" label="Municipio">
                  <Field
                    name="MunicipalityField"
                    :rules="validateMunicipality"
                    as="text"
                  >
                    <b-form-input
                      v-model="addressCementeryService.municipio"
                      :state="MunicipalityState"
                    >
                    </b-form-input>
                  </Field>
                  <ErrorMessage
                    class="text-danger"
                    name="MunicipalityField"
                  ></ErrorMessage>
                </b-form-group>
              </b-col>
              <!-- Agregar localidad -->
              <b-col>
                <b-form-group class="mt-3" label="Localidad">
                  <Field
                    name="LocationField"
                    :rules="validateLocation"
                    as="text"
                  >
                    <b-form-input
                      v-model="addressCementeryService.localidad"
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
                      v-model="addressCementeryService.calle"
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
                      v-model="addressCementeryService.numeroExterior"
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
                <b-button v-b-toggle.collapse-1 variant="primary">
                  Mapa
                </b-button>
                <b-collapse id="collapse-1" class="mt-2">
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
                </b-collapse>
              </b-row>
              <b-row cols="2" align-h="end">
                <b-col>

                <b-button
                  class=" w-75 m-2 text-white"
                  variant="primary"
                  to="/ServiciosPublicos/DireccionCementerios/list"
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
        <ExpedientDocumentsDirectionCementery :DireccionId="direccionId" />
      </b-tab>
    </b-tabs>
  </b-card>
</template>

<script>
import AddressCementeryService from '@/Services/addresscementery.Services'
import ExpedientDocumentsDirectionCementery from '@/views/ServiciosPublicos/cementery/addresscementery/DocumentsAddressCementery.vue'
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
    ExpedientDocumentsDirectionCementery
  },
  setup () {
    const swal = inject('$swal')
    const { getAddressCementeryById, updateAddressCementery } =
      AddressCementeryService()
    // const $toast = useToast()
    const addressCementeryService = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const NameCementeryState = ref(false)
    const MunicipalityState = ref(false)
    const LocationState = ref(false)
    const StreetState = ref(false)
    const NumberOutsideState = ref(false)
    const direccionId = ref(parseInt(router.params.DireccionId))
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      {
        text: 'Direccion cementerio',
        to: '/ServiciosPublicos/DireccionCementerios/list'
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
      addressCementeryService.value.latitud = location.latLng.lat()
      addressCementeryService.value.longitud = location.latLng.lng()
    }

    const onUpdateAddressCementeryService = () => {
      updateAddressCementery(addressCementeryService.value, data => {})
      swal
        .fire({
          title: '¡Direccion cementerio modificado correctamente!',
          text: 'La direccion cementerio se ha modificado  satisfactoriamente.',
          icon: 'success'
        })
        .then(result => {
          if (result.isConfirmed) {
            redirect.push('/ServiciosPublicos/DireccionCementerios/list')
          }
        })
    }

    getAddressCementeryById(router.params.DireccionId, data => {
      addressCementeryService.value = data
      center.value = { lat: data.latitud, lng: data.longitud }
      markers.value.position = { lat: data.latitud, lng: data.longitud }
    })

    const validateNameCementery = () => {
      if (!addressCementeryService.value.nombreCementerio) {
        validateState()
        return 'Este campo es requerido'
      }

      if (
        !/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(
          addressCementeryService.value.nombreCementerio
        )
      ) {
        NameCementeryState.value = false
        return 'Este campo solo puede contener letras'
      }

      // if (!addressCementeryService.value.nombreCementerio.trim().length > 0) {
      //   NameCementeryState.value = false
      //   return 'Este campo no puede contener espacios'
      // }

      validateState()
      return true
    }

    const validateMunicipality = () => {
      if (!addressCementeryService.value.municipio) {
        validateState()
        return 'Este campo es requerido'
      }

      if (
        !/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(
          addressCementeryService.value.municipio
        )
      ) {
        MunicipalityState.value = false
        return 'Este campo solo puede contener letras'
      }

      // if (!addressCementeryService.value.municipio.trim().length > 0) {
      //   MunicipalityState.value = false
      //   return 'Este campo no puede contener espacios'
      // }

      validateState()
      return true
    }

    const validateLocation = () => {
      if (!addressCementeryService.value.localidad) {
        validateState()
        return 'Este campo es requerido'
      }

      if (
        !/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(
          addressCementeryService.value.localidad
        )
      ) {
        LocationState.value = false
        return 'Este campo solo puede contener letras'
      }

      // if (!addressCementeryService.value.localidad.trim().length > 0) {
      //   LocationState.value = false
      //   return 'Este campo no puede contener espacios'
      // }

      validateState()
      return true
    }

    const validateStreet = () => {
      if (!addressCementeryService.value.calle) {
        validateState()
        return 'Este campo es requerido'
      }

      if (
        !/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(addressCementeryService.value.calle)
      ) {
        StreetState.value = false
        return 'Este campo solo puede contener letras'
      }

      // if (!addressCementeryService.value.calle.trim().length > 0) {
      //   StreetState.value = false
      //   return 'Este campo no puede contener espacios'
      // }

      validateState()
      return true
    }

    const validateNumberOutside = () => {
      if (!addressCementeryService.value.numeroExterior) {
        validateState()
        return 'Este campo es requerido'
      }

      if (!/^[0-9]+$/i.test(addressCementeryService.value.numeroExterior)) {
        NumberOutsideState.value = false
        return 'Este campo solo puede contener numeros'
      }

      // if (!addressCementeryService.value.numeroExterior.trim().length > 0) {
      //   NumberOutsideState.value = false
      //   return 'Este campo no puede contener espacios'
      // }

      validateState()
      return true
    }

    const validateState = () => {
      NameCementeryState.value =
        addressCementeryService.value.nombreCementerio !== ''
      MunicipalityState.value = addressCementeryService.value.municipio !== ''
      LocationState.value = addressCementeryService.value.localidad !== ''
      StreetState.value = addressCementeryService.value.calle !== ''
      NumberOutsideState.value =
        addressCementeryService.value.numeroExterior !== ''
    }

    return {
      addressCementeryService,
      breadcrumbItems,
      NameCementeryState,
      MunicipalityState,
      LocationState,
      StreetState,
      NumberOutsideState,
      markers,
      direccionId,
      center,
      //   router

      onUpdateAddressCementeryService,
      validateState,
      validateNameCementery,
      validateMunicipality,
      validateLocation,
      validateStreet,
      validateNumberOutside,
      updateCoordinates
    }
  }
}
</script>

<style></style>
