<template>
  <b-card class="m-2">
    <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
  </b-card>
  <b-card class="m-2">
    <b-tabs content-class="mt-3">
      <b-tab title="Editar ruta" active>
          <Form @submit="onUpdateRoadService">
            <b-row cols="3">
              <!-- Agregar Nombre -->
              <b-col>
                <b-form-group class="mt-3" label="Nombre de la ruta">
                  <Field
                    name="NameRoadField"
                    :rules="validateNameRoad"
                    as="text"
                  >
                    <b-form-input
                      v-model="RoadService.nombre"
                      :state="NameRoadState"
                    >
                    </b-form-input>
                  </Field>
                  <ErrorMessage
                    class="text-danger"
                    name="NameRoadField"
                  ></ErrorMessage>
                </b-form-group>
              </b-col>
              <!-- Agregar Observacion -->
              <b-col>
                <b-form-group class="mt-3" label="Observacion">
                  <Field
                    name="ObservationField"
                    :rules="validateObservation"
                    as="text"
                  >
                    <b-form-input
                      v-model="RoadService.observacion"
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
              <!-- Agregar horario -->
              <b-col>
                <b-form-group class="mt-3" label="Horario de ruta">
                  <Field name="HoraryField" :rules="validateHorary" as="text">
                    <b-form-input
                      v-model="RoadService.horario"
                      placeholder="Ingresa el rango de horas del horario de la ruta"
                      :state="HoraryState"
                    >
                    </b-form-input>
                  </Field>
                  <ErrorMessage
                    class="text-danger"
                    name="HoraryField"
                  ></ErrorMessage>
                </b-form-group>
              </b-col>
            </b-row>
             <b-card class="m-2">
          <b-row cols="2">
            <b-col>
              <b-button
                class="mb-5 w-100"
                variant="primary"
                @click="onAddPolyline"
              >
                Marcar Ruta
              </b-button>
            </b-col>
            <b-col>
              <b-button
                class="mb-5 w-100"
                variant="primary"
                @click="ResetPolyline"
              >
                Reiniciar ruta
              </b-button>
            </b-col>
            <GoogleMap
              api-key="AIzaSyCYAwe7Fk4PQLI3bBBqxUViN4IOXVGd_z0"
              style="width: 100%; height: 450px"
              :center="center"
              :zoom="14"
              @click="addMaker"
            >
              <MarkerCluster>
                <Marker
                  v-for="(location, i) in locations"
                  :options="{
                    position: location,
                    label: `${i === 0 ? 'Inicio' : ''}`
                  }"
                  :key="i"
                />
                <Polyline :options="flightPath" />
              </MarkerCluster>
            </GoogleMap>
          </b-row>
        </b-card>
            <b-row align-h="end">
              <b-button
                class="w-auto m-2 text-white"
                variant="primary"
                to="/ServiciosPublicos/Ruta/list"
              >
                Cancelar
              </b-button>
              <b-button class="w-auto m-2" variant="success" type="submit">
                Guardar
              </b-button>
            </b-row>
          </Form>
      </b-tab>
      <b-tab title="Empleados">
        <CleannessEmployee :RutaId="rutaId" />
      </b-tab>
      <b-tab title="Vehiculos">
        <CleannessVehicle :RutaId="rutaId" />
      </b-tab>
    </b-tabs>
  </b-card>
</template>

<script>
import RoadServices from '@/Services/road.Services'
import CleannessEmployee from '@/views/ServiciosPublicos/cleanness/cleannessemployee/CleannessEmployeeList.vue'
import CleannessVehicle from '@/views/ServiciosPublicos/cleanness/cleannessvehicle/CleannessVehicleList.vue'
import { ref, inject } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { GoogleMap, Marker, MarkerCluster, Polyline } from 'vue3-google-map'
// import { useToast } from 'vue-toast-notification'
import { Form, Field, ErrorMessage } from 'vee-validate'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    Form,
    Field,
    ErrorMessage,
    GoogleMap,
    Marker,
    MarkerCluster,
    Polyline,
    CleannessEmployee,
    CleannessVehicle
  },
  setup () {
    const swal = inject('$swal')
    const { getRoadById, updateRoad, getCoordsRoad } = RoadServices()
    // const $toast = useToast()
    const RoadService = ref([])
    const date = ref()
    const router = useRoute()
    const redirect = useRouter()
    const OriginState = ref(false)
    const DestinationState = ref(false)
    const ObservationState = ref(false)
    const NameRoadState = ref(false)
    const HoraryState = ref(false)
    const rutaId = ref(parseInt(router.params.RutaId))
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Ruta', to: '/ServiciosPublicos/Ruta/list' },
      { text: 'Editar-ruta' }
    ])
    const CoordsRoadFields = ref({
      coordenadaId: 0,
      latitud: '',
      longitud: '',
      rutaId: 0,
      ordenCoordenada: null
    })
    const locations = ref([])
    const flightPath = ref({
      path: [],
      geodesic: true,
      strokeColor: '#5e50ee',
      strokeOpacity: 1.0,
      strokeWeight: 5
    })
    const center = ref({ lat: 20.5530788, lng: -102.5316817 })
    const addMaker = location => {
      locations.value.push({
        lat: location.latLng.lat(),
        lng: location.latLng.lng()
      })
    }
    const onAddPolyline = () => {
      if (flightPath.value.path.length > 0) {
        flightPath.value.path = []
      }
      flightPath.value = {
        path: locations.value,
        geodesic: true,
        strokeColor: '#5e50ee',
        strokeOpacity: 1.0,
        strokeWeight: 5
      }
    }
    const ResetPolyline = () => {
      locations.value = []
      flightPath.value = {
        path: [],
        geodesic: true,
        strokeColor: '#5e50ee',
        strokeOpacity: 1.0,
        strokeWeight: 5
      }
    }
    const onUpdateRoadService = () => {
      updateRoad(RoadService.value, data => {})
      swal
        .fire({
          title: '¡Ruta modificado correctamente!',
          text: 'La ruta se ha modificado  satisfactoriamente.',
          icon: 'success'
        })
        .then(result => {
          if (result.isConfirmed) {
            redirect.push('/ServiciosPublicos/Ruta/list')
          }
        })
    }
    getRoadById(router.params.RutaId, data => {
      RoadService.value = data
    })
    getCoordsRoad(router.params.RutaId, data => {
      for (let i = 0; i < data.length; i++) {
        locations.value.push({ lat: data[i].latitud, lng: data[i].longitud })
      }
      flightPath.value = {
        path: locations.value,
        geodesic: true,
        strokeColor: '#5e50ee',
        strokeOpacity: 1.0,
        strokeWeight: 5
      }
    })

    const validateNameRoad = () => {
      if (!RoadService.value.nombre) {
        validateState()
        return 'Este campo es requerido'
      }
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ 0-9]+$/i.test(RoadService.value.nombre)) {
        NameRoadState.value = false
        return 'Este campo solo puede contener letras'
      }
      if (!RoadService.value.nombre.trim().length > 0) {
        NameRoadState.value = false
        return 'Este campo no puede contener espacios'
      }

      validateState()
      return true
    }

    const validateObservation = () => {
      if (!RoadService.value.observacion) {
        validateState()
        return 'Este campo es requerido'
      }

      if (
        !/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ ,;:. 0-9]+$/i.test(
          RoadService.value.observacion
        )
      ) {
        ObservationState.value = false
        return 'Este campo solo puede contener numeros'
      }

      validateState()
      return true
    }
    const validateHorary = () => {
      if (!RoadService.value.horario) {
        validateState()
        return 'Este campo es requerido'
      }

      validateState()
      return true
    }

    const validateState = () => {
      NameRoadState.value =
        RoadService.value.nombre !== '' && RoadService.value.nombre !== null
      ObservationState.value =
        RoadService.value.observacion !== '' &&
        RoadService.value.observacion !== null
      HoraryState.value =
        RoadService.value.horario !== '' && RoadService.value.horario !== null
    }

    return {
      RoadService,
      rutaId,
      breadcrumbItems,
      CoordsRoadFields,
      OriginState,
      DestinationState,
      ObservationState,
      NameRoadState,
      // fields,
      center,
      locations,
      flightPath,
      date,
      HoraryState,
      //   router

      onUpdateRoadService,
      validateState,
      // validateOrigin,
      // validateDestination,
      validateObservation,
      validateNameRoad,
      validateHorary,
      addMaker,
      onAddPolyline,
      ResetPolyline
    }
  }
}
</script>

<style></style>
