<template>
  <b-card class="m-2">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <div>
      <h3>Editar Vehiculo</h3>
    </div>
    <b-tabs>
      <b-tab title="Datos generales">
        <b-card>
          <Form @submit="onUpdateVehicle">
            <b-row cols="3">
              <b-col>
                <b-form-group class="mt-3" label="Nomenclatura">
                  <Field
                    name="NomenclatureField"
                    :rules="validateNomenclature"
                    as="text"
                  >
                    <b-form-input
                      placeholder="Ingresa la marca y/o modelo del equipo"
                      v-model="vehicle.nomenclatura"
                      :state="NomenclatureState"
                    >
                    </b-form-input>
                  </Field>
                  <ErrorMessage
                    class="text-danger"
                    name="NomenclatureField"
                  ></ErrorMessage>
                </b-form-group>
              </b-col>
              <b-col>
                <b-form-group class="mt-3" label="Proveedor">
                  <Field name="VendorField" :rules="validateVendor" as="number">
                    <b-form-select
                      v-model="vehicle.proveedorId"
                      autofocus
                      :state="vendorState"
                      value-field="proveedorId"
                      text-field="nombre"
                      :options="providers"
                    >
                    </b-form-select>
                  </Field>
                  <ErrorMessage
                    class="text-danger"
                    name="VendorField"
                  ></ErrorMessage>
                </b-form-group>
              </b-col>
              <b-col>
                <b-form-group class="mt-3" label="No. Factura">
                  <Field
                    name="InvoiceNumberField"
                    :rules="validateInvoiceNumber"
                    as="text"
                  >
                    <b-form-input
                      placeholder="Ingresa el numero de factura"
                      v-model="vehicle.numeroFactura"
                      :state="invoiceNumberState"
                    >
                    </b-form-input>
                  </Field>
                  <ErrorMessage
                    class="text-danger"
                    name="InvoiceNumberField"
                  ></ErrorMessage>
                </b-form-group>
              </b-col>
              <b-col>
                <b-form-group class="mt-3" label="Marca">
                  <Field name="BrandField" :rules="validateBrand" as="text">
                    <b-form-input
                      placeholder="Ingresa la marca y/o modelo del equipo"
                      v-model="vehicle.marca"
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
                      v-model="vehicle.modelo"
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
                      v-model="vehicle.color"
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
                  <Field
                    name="LicensePlateField"
                    :rules="validateLicensePlate"
                    as="text"
                  >
                    <b-form-input
                      placeholder="Ingresa la placa del vehiculo"
                      v-model="vehicle.placa"
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
                      v-model="vehicle.numeroSerie"
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
                  <Field
                    name="ReleaseYearField"
                    :rules="validateReleaseYear"
                    as="number"
                  >
                    <b-form-input
                      placeholder="Ingresa el modelo del vehiculo"
                      v-model="vehicle.añoLanzamiento"
                      :state="releaseYearState"
                      type="number"
                    >
                    </b-form-input>
                  </Field>
                  <ErrorMessage
                    class="text-danger"
                    name="ReleaseYearField"
                  ></ErrorMessage>
                </b-form-group>
              </b-col>
              <b-col>
                <b-form-group class="mt-3" label="Puertas">
                  <Field name="DoorsField" :rules="validateDoors" as="number">
                    <b-form-input
                      placeholder="Ingresa la cantidad de puertas que cuenta el vehiculo"
                      v-model="vehicle.puertas"
                      type="number"
                      :state="doorsState"
                    >
                    </b-form-input>
                  </Field>
                  <ErrorMessage
                    class="text-danger"
                    name="DoorsField"
                  ></ErrorMessage>
                </b-form-group>
              </b-col>
              <b-col>
                <b-form-group class="mt-3" label="Transimisión">
                  <Field
                    name="TransmissionField"
                    :rules="validateTransmission"
                    as="number"
                  >
                    <b-form-select
                      v-model="vehicle.transmisión"
                      autofocus
                      :state="transmissionState"
                      value-field="nombre"
                      text-field="nombre"
                      :options="typeTransmissions"
                    >
                    </b-form-select>
                  </Field>
                  <ErrorMessage
                    class="text-danger"
                    name="TransmissionField"
                  ></ErrorMessage>
                </b-form-group>
              </b-col>
              <b-col>
                <b-form-group class="mt-3" label="Cilindros">
                  <Field
                    name="CylinderField"
                    :rules="validateCylinder"
                    as="number"
                  >
                    <b-form-input
                      placeholder="Ingresa la cantidad de cilindros que cuenta el vehiculo"
                      v-model="vehicle.cilindros"
                      type="number"
                      :state="cylinderState"
                    >
                    </b-form-input>
                  </Field>
                  <ErrorMessage
                    class="text-danger"
                    name="CylinderField"
                  ></ErrorMessage>
                </b-form-group>
              </b-col>
              <b-col>
                <b-form-group class="mt-3" label="Version">
                  <b-form-input
                    placeholder="Ingresa la cantidad de puertas que cuenta el vehiculo"
                    v-model="vehicle.version"
                  >
                  </b-form-input>
                </b-form-group>
              </b-col>
              <b-col>
                <b-form-group class="mt-3" label="Observaciones">
                  <b-form-input v-model="vehicle.observaciones"> </b-form-input>
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
                      v-model="vehicle.estatusVehiculoId"
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
                class="w-25 m-2 text-white"
                variant="primary"
                to="/PatrimonioMunicipal/Vehiculos/list"
              >
                Cancelar
              </b-button>
              <b-button class="w-25 m-2" variant="success" type="submit">
                Guardar
              </b-button>
            </b-row>
          </Form>
        </b-card>
      </b-tab>
      <b-tab
        title="Documentos"
      >
        <DocumentsVehicle :VehiculoId="parseInt(router.params.VehiculoId)" :Vehiculo="vehicle"/>
      </b-tab>
    </b-tabs>
  </b-card>
</template>

<script>
import VehiclesServices from '@/Services/vehicles.Services'
import ProviderServices from '@/Services/provider.Services'
import DocumentsVehicle from '../Vehicles/DocumentsVehicles/DocumentsVehicle.vue'
import { Field, Form, ErrorMessage } from 'vee-validate'
import { ref, inject } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    Field,
    Form,
    ErrorMessage,
    DocumentsVehicle
  },
  setup () {
    const swal = inject('$swal')
    const { getVehicle, updateVehicle, getEstatusVehicles } = VehiclesServices()
    const { getProvider } = ProviderServices()
    const typeTransmissions = ref([
      { id: 1, nombre: 'Manual' },
      { id: 2, nombre: 'Automático' }
    ])
    const vehicle = ref([])
    const providers = ref([])
    const statusVehicles = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const NomenclatureState = ref(false)
    const vendorState = ref(false)
    const invoiceNumberState = ref(false)
    const brandState = ref(false)
    const modelState = ref(false)
    const colorState = ref(false)
    const licensePlateState = ref(false)
    const serialNumberState = ref(false)
    const releaseYearState = ref(false)
    const vehicleState = ref(false)
    const doorsState = ref(false)
    const transmissionState = ref(false)
    const cylinderState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Vehiculos', to: '/PatrimonioMunicipal/Vehiculos/list' },
      { text: 'Editar Vehiculo' }
    ])
    getVehicle(router.params.VehiculoId, data => {
      vehicle.value = data
      validateState()
    })
    getEstatusVehicles(data => {
      statusVehicles.value = data
    })
    getProvider(data => {
      providers.value = data
    })
    const onUpdateVehicle = () => {
      updateVehicle(vehicle.value, data => {})
      swal
        .fire({
          title: 'Vehiculo modificado correctamente!',
          text: 'El vehiculo se ha modificado al sistema satisfactoriamente.',
          icon: 'success'
        })
        .then(result => {
          if (result.isConfirmed) {
            redirect.push('/PatrimonioMunicipal/Vehiculos/list')
          }
        })
    }
    // VALIDATIONS
    const validateNomenclature = () => {
      if (!vehicle.value.nomenclatura) {
        validateState()
        return 'Este campo es requerido'
      }
      if (!/^[a-zA]+[A-Z0-9.-]+$/i.test(vehicle.value.nomenclatura)) {
        validateState()
        return 'Este campo solo acepta mayusculas y numeros'
      }
      validateState()
      return true
    }
    const validateVendor = () => {
      if (!vehicle.value.proveedorId) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateInvoiceNumber = () => {
      if (!vehicle.value.numeroFactura) {
        validateState()
        return 'Este campo es requerido'
      }
      if (!/^[a-zA]+[A-Z0-9.-]+$/i.test(vehicle.value.numeroFactura)) {
        validateState()
        return 'Este campo solo acepta mayusculas y numeros'
      }
      validateState()
      return true
    }
    const validateBrand = () => {
      if (!vehicle.value.marca) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateModel = () => {
      if (!vehicle.value.modelo) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateColor = () => {
      if (!vehicle.value.color) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateLicensePlate = () => {
      if (!vehicle.value.placa) {
        validateState()
        return 'Este campo es requerido'
      }
      if (
        !/^(?=.*\d)(?=.*[a-zA-Z])([A-ZñÑáéíóúÁÉÍÓÚ])[A-Z0-9]{5,6}$/.test(
          vehicle.value.placa
        )
      ) {
        licensePlateState.value = false
        return 'Este campo solo acepta mayusculas y numeros y no acepta mas de 6 caracteres'
      }
      validateState()
      return true
    }
    const validateSerialNumber = () => {
      if (!vehicle.value.numeroSerie) {
        validateState()
        return 'Este campo es requerido'
      }
      if (
        !/^(?=.*\d)(?=.*[a-zA-Z])([A-ZñÑáéíóúÁÉÍÓÚ])[A-Z0-9]{17,17}$/.test(
          vehicle.value.numeroSerie
        )
      ) {
        validateState()
        return 'Este campo solo acepta mayusculas y numeros y no acepta mas de 17 caracteres'
      }
      validateState()
      return true
    }
    const validateReleaseYear = () => {
      if (!vehicle.value.añoLanzamiento) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateDoors = () => {
      if (!vehicle.value.puertas) {
        validateState()
        return 'Este campo es requerido'
      }
      if (vehicle.value.puertas < 2) {
        validateState()
        return 'No puede contener un vehiculo con menos de 2 puertas'
      }
      if (vehicle.value.puertas > 5) {
        validateState()
        return 'No puede contener un vehiculo con mas de 5 puertas'
      }
      validateState()
      return true
    }
    const validateTransmission = () => {
      if (!vehicle.value.transmisión) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateCylinder = () => {
      if (!vehicle.value.cilindros) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateVehicleState = () => {
      if (!vehicle.value.estatusVehiculoId) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateState = () => {
      brandState.value =
        vehicle.value.marca !== null && vehicle.value.marca !== ''
      modelState.value =
        vehicle.value.modelo !== null && vehicle.value.modelo !== ''
      colorState.value =
        vehicle.value.color !== null && vehicle.value.color !== ''
      licensePlateState.value =
        vehicle.value.placa !== null && vehicle.value.placa !== ''
      serialNumberState.value =
        vehicle.value.numeroSerie !== null &&
        vehicle.value.numeroSerie !== '' &&
        /^(?=.*\d)(?=.*[a-zA-Z])([A-ZñÑáéíóúÁÉÍÓÚ])[A-Z0-9]{17,17}$/.test(
          vehicle.value.numeroSerie
        )
      releaseYearState.value =
        vehicle.value.añoLanzamiento !== null &&
        vehicle.value.añoLanzamiento !== 0
      doorsState.value = vehicle.value.puertas !== null
      transmissionState.value =
        vehicle.value.transmisión !== null && vehicle.value.transmisión !== ''
      vehicleState.value = vehicle.value.estatusVehiculoId !== null
      NomenclatureState.value =
        vehicle.value.nomenclatura !== null &&
        vehicle.value.nomenclatura !== '' &&
        /^[a-zA]+[A-Z0-9.-]+$/i.test(vehicle.value.nomenclatura)
      vendorState.value = vehicle.value.proveedorId !== null
      invoiceNumberState.value =
        vehicle.value.numeroFactura !== null &&
        vehicle.value.numeroFactura !== '' &&
        /^[a-zA]+[A-Z0-9.-]+$/i.test(vehicle.value.numeroFactura)
      cylinderState.value =
        vehicle.value.cilindros !== null && vehicle.value.cilindros !== ''
      return ''
    }
    return {
      vehicle,
      providers,
      breadcrumbItems,
      router,
      redirect,
      brandState,
      modelState,
      colorState,
      NomenclatureState,
      invoiceNumberState,
      vendorState,
      licensePlateState,
      serialNumberState,
      releaseYearState,
      doorsState,
      transmissionState,
      vehicleState,
      cylinderState,
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
      validateVehicleState,
      validateCylinder,
      validateInvoiceNumber,
      validateVendor,
      validateNomenclature
    }
  }
}
</script>

<style></style>
