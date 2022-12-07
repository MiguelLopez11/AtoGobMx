<template>
  <b-card class="m-2">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-card>
      <div>
        <h3>Editar Vehiculo</h3>
      </div>
      <Form @submit="onUpdateVehicle">
        <b-row cols="3">
          <b-col>
            <b-form-group class="mt-3" label="Marca">
              <Field name="BrandField" :rules="validateBrand" as="text">
                <b-form-input
                  placeholder="Ingresa la marca y/o modelo del equipo"
                  v-model="vehicles.marca"
                  :state="brandState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="BrandField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Submarca">
              <Field name="modelField" :rules="validateModel" as="text">
                <b-form-input
                  placeholder="Ingresa el modelo del vehiculo"
                  v-model="vehicles.modelo"
                  :state="modelState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="modelField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Color">
              <Field name="ColorField" :rules="validateColor" as="text">
                <b-form-input
                  placeholder="Ingresa el color del vehiculo"
                  v-model="vehicles.color"
                  :state="colorState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="ColorField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Placa">
              <Field name="LicensePlateField" :rules="validateLicensePlate" as="text">
                <b-form-input
                  placeholder="Ingresa la placa del vehiculo"
                  v-model="vehicles.placa"
                  :state="licensePlateState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="LicensePlateField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Numero de serie">
              <Field
                name="SerialNumberField"
                :rules="validateSerialNumber"
                as="text"
              >
                <b-form-input
                  placeholder="Ingresa el numero de serie del vehiculo"
                  v-model="vehicles.numeroSerie"
                  :state="serialNumberState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="SerialNumberField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Modelo">
              <Field name="ReleaseYearField" :rules="validateReleaseYear" as="number">
                <b-form-input
                  placeholder="Ingresa el modelo del vehiculo"
                  v-model="vehicles.añoLanzamiento"
                  :state="releaseYearState"
                  type="number"
                >
                </b-form-input>
              </Field>
              <ErrorMessage class="text-danger" name="ReleaseYearField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Puertas">
              <Field name="DoorsField" :rules="validateDoors" as="number">
                <b-form-input
                  placeholder="Ingresa la cantidad de puertas que cuenta el vehiculo"
                  v-model="vehicles.puertas"
                  type="number"
                  :state="doorsState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage class="text-danger" name="DoorsField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Transimisión">
              <Field name="TransmissionField" :rules="validateTransmission" as="number">
                <b-form-select
                  v-model="vehicles.transmisión"
                  autofocus
                  :state="transmissionState"
                  value-field="nombre"
                  text-field="nombre"
                  :options="typeTransmissions"
                >
                </b-form-select>
              </Field>
              <ErrorMessage class="text-danger" name="TransmissionField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Estado">
              <Field
                name="StateField"
                :rules="validateVehicleState"
                as="number"
              >
                <b-form-select
                  v-model="vehicles.estatusVehiculoId"
                  autofocus
                  :state="vehicleState"
                  value-field="estatusVehiculoId"
                  text-field="nombre"
                  :options="statusVehicles"
                >
                </b-form-select>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="StateField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            to="/PatrimonioMunicipal/Vehiculos/list"
          >
            Cancelar
          </b-button>
          <b-button class="w-auto m-2" variant="success" type="submit">
            Guardar
          </b-button>
        </b-row>
      </Form>
    </b-card>
  </b-card>
</template>

<script>
import VehiclesServices from '@/Services/vehicles.Services'
import { Field, Form, ErrorMessage } from 'vee-validate'
import { ref, inject } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    Field,
    Form,
    ErrorMessage
  },
  setup () {
    const swal = inject('$swal')
    const { getVehicle, updateVehicle, getEstatusVehicles } = VehiclesServices()
    const typeTransmissions = ref([{ id: 1, nombre: 'Manual' }, { id: 2, nombre: 'Automático' }])
    const vehicles = ref([])
    const statusVehicles = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const brandState = ref(false)
    const modelState = ref(false)
    const colorState = ref(false)
    const licensePlateState = ref(false)
    const serialNumberState = ref(false)
    const releaseYearState = ref(false)
    const vehicleState = ref(false)
    // const stateComputerState = ref(false)
    const doorsState = ref(false)
    const transmissionState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Vehiculos', to: '/PatrimonioMunicipal/Vehiculos/list' },
      { text: 'Editar Vehiculo' }
    ])
    getVehicle(router.params.VehiculoId, data => {
      vehicles.value = data
    })
    getEstatusVehicles(data => {
      statusVehicles.value = data
    })
    const onUpdateVehicle = () => {
      updateVehicle(vehicles.value, (data) => {
      })
      swal.fire({
        title: 'Vehiculo modificado correctamente!',
        text: 'El vehiculo se ha modificado al sistema satisfactoriamente.',
        icon: 'success'
      }).then(result => {
        if (result.isConfirmed) {
          redirect.push('/PatrimonioMunicipal/Vehiculos/list')
        }
      })
    }
    // VALIDATIONS
    const validateBrand = () => {
      if (!vehicles.value.marca) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateModel = () => {
      if (!vehicles.value.modelo) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateColor = () => {
      if (!vehicles.value.color) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateLicensePlate = () => {
      if (!vehicles.value.placa) {
        validateState()
        return 'Este campo es requerido'
      }
      if (!/^(?=.*\d)(?=.*[a-zA-Z])([A-ZñÑáéíóúÁÉÍÓÚ])[A-Z0-9]{5,6}$/.test(vehicles.value.placa)) {
        licensePlateState.value = false
        return 'Este campo solo acepta mayusculas y numeros y no acepta mas de 6 caracteres'
      }
      validateState()
      return true
    }
    const validateSerialNumber = () => {
      if (!vehicles.value.numeroSerie) {
        validateState()
        return 'Este campo es requerido'
      }
      if (!/^(?=.*\d)(?=.*[a-zA-Z])([A-ZñÑáéíóúÁÉÍÓÚ])[A-Z0-9]{17,17}$/.test(vehicles.value.numeroSerie)) {
        serialNumberState.value = false
        return 'Este campo solo acepta mayusculas y numeros y no acepta mas de 17 caracteres'
      }
      validateState()
      return true
    }
    const validateReleaseYear = () => {
      if (!vehicles.value.añoLanzamiento) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateDoors = () => {
      if (!vehicles.value.puertas) {
        validateState()
        return 'Este campo es requerido'
      }
      if (vehicles.value.puertas < 2) {
        doorsState.value = false
        return 'No puede contener un vehiculo con menos de 2 puertas'
      }
      if (vehicles.value.puertas > 5) {
        doorsState.value = false
        return 'No puede contener un vehiculo con mas de 5 puertas'
      }
      validateState()
      return true
    }
    const validateTransmission = () => {
      if (!vehicles.value.transmisión) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateVehicleState = () => {
      if (!vehicles.value.estatusVehiculoId) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateState = () => {
      brandState.value = vehicles.value.marca !== null && vehicles.value.marca !== ''
      modelState.value = vehicles.value.modelo !== null && vehicles.value.modelo !== ''
      colorState.value = vehicles.value.color !== null && vehicles.value.color !== ''
      licensePlateState.value = vehicles.value.placa !== null && vehicles.value.placa !== ''
      serialNumberState.value = vehicles.value.numeroSerie !== null && vehicles.value.numeroSerie !== ''
      releaseYearState.value = vehicles.value.añoLanzamiento !== null && vehicles.value.añoLanzamiento !== 0
      doorsState.value = vehicles.value.puertas !== null
      transmissionState.value = vehicles.value.transmisión !== null && vehicles.value.transmisión !== ''
      vehicleState.value = vehicles.value.estatusVehiculoId !== null
      return ''
    }
    return {
      vehicles,
      breadcrumbItems,
      router,
      redirect,
      brandState,
      modelState,
      colorState,
      licensePlateState,
      serialNumberState,
      releaseYearState,
      doorsState,
      transmissionState,
      vehicleState,
      typeTransmissions,
      statusVehicles,
      validateBrand,
      validateModel,
      validateColor,
      validateLicensePlate,
      validateSerialNumber,
      validateReleaseYear,
      validateDoors,
      validateTransmission,
      onUpdateVehicle,
      validateState,
      validateVehicleState
    }
  }
}
</script>

<style>
</style>
