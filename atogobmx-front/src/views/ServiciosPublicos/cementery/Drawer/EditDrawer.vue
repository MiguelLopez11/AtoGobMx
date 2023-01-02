<template>
  <b-card class="m-2">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-card>
      <div>
        <h3>Editar gabeta</h3>
      </div>
      <Form @submit="onUpdateDrawerService">
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
                  v-model="drawerService.nombrePropietario"
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
                  v-model="drawerService.numeroEspasios"
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
                  v-model="drawerService.metrosCorrespondientes"
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
                  v-model="drawerService.espaciosDisponibles"
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
        <b-row cols="1" align-h="center">
          <b-alert variant="warning" dismissible show
                >Arrastra el punto del mapa al lugar donde se encuentra su
                gabeta.</b-alert
              >
            <b-card>
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
        <b-row cols="2" class="m-2" align-h="end">
          <b-col>

          <b-button
            variant="primary"
            to="/ServiciosPublicos/Gabetas/list"
            type="reset"
          >
            Cancelar</b-button
          >
          </b-col>
          <b-col>

          <b-button type="success" variant="success">
            Guardar
          </b-button>
          </b-col>
        </b-row>
      </Form>
    </b-card>
  </b-card>
</template>

<script>
import DrawerService from '@/Services/drawer.Services'
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
    const { getDrawerById, updateDrawer } = DrawerService()
    // const $toast = useToast()
    const drawerService = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const PropietaryState = ref(false)
    const SpacesState = ref(false)
    const MeterState = ref(false)
    const AvailableState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Cementerios', to: '/ServiciosPublicos/Gabetas/list' },
      { text: 'Editar-cementerios' }
    ])
    const center = ref({ lat: 0, lng: 0 })
    getDrawerById(router.params.GabetaId, (data) => {
      drawerService.value = data
      center.value.lat = data.latitud
      center.value.lng = data.longitud
    })

    const updateCoordinates = (location) => {
      drawerService.value.latitud = location.latLng.lat()
      drawerService.value.longitud = location.latLng.lng()
    }

    const onUpdateDrawerService = () => {
      updateDrawer(drawerService.value, (data) => {})
      swal.fire({
        title: '¡Cementerio modificado correctamente!',
        text: 'La cementerio se ha modificado  satisfactoriamente.',
        icon: 'success'
      }).then(result => {
        if (result.isConfirmed) {
          redirect.push('/ServiciosPublicos/Gabetas/list')
        }
      })
    }

    const validatePropietary = () => {
      if (!drawerService.value.nombrePropietario) {
        validateState()
        return 'Este campo es requerido'
      }

      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(drawerService.value.nombrePropietario)) {
        PropietaryState.value = false
        return 'Este campo solo puede contener letras'
      }

      validateState()
      return true
    }

    const validateSpaces = () => {
      if (!drawerService.value.numeroEspasios) {
        validateState()
        return 'Este campo es requerido'
      }

      if (!/^[0-9]+$/i.test(drawerService.value.numeroEspasios)) {
        SpacesState.value = false
        return 'Este campo solo puede contener numeros'
      }

      validateState()
      return true
    }

    const validateMeter = () => {
      if (!drawerService.value.metrosCorrespondientes) {
        validateState()
        return 'Este campo es requerido'
      }

      if (!/^\d*\.\d+$/i.test(drawerService.value.metrosCorrespondientes)) {
        MeterState.value = false
        return 'Este campo solo puede contener numeros'
      }

      validateState()
      return true
    }

    const validateAvailable = () => {
      if (!drawerService.value.espaciosDisponibles) {
        validateState()
        return 'Este campo es requerido'
      }

      if (!/^[0-9]+$/i.test(drawerService.value.espaciosDisponibles)) {
        AvailableState.value = false
        return 'Este campo solo puede contener numeros'
      }

      validateState()
      return true
    }

    const validateState = () => {
      // eslint-disable-next-line no-unneeded-ternary
      PropietaryState.value = drawerService.value.nombrePropietario === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      SpacesState.value = drawerService.value.numeroEspasios === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      MeterState.value = drawerService.value.metrosCorrespondientes === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      AvailableState.value = drawerService.value.espaciosDisponibles === '' ? false : true
    }

    return {
      drawerService,
      breadcrumbItems,
      PropietaryState,
      SpacesState,
      MeterState,
      AvailableState,
      center,
      //   router

      onUpdateDrawerService,
      updateCoordinates,
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
