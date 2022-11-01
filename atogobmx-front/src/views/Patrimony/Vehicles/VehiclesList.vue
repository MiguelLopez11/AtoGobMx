<template>
  <b-card class="m-2">
    <b-row align-h="end" class="mb-3 mr-1">
      <b-form-input
        size="lg"
        style="width: 350px"
        v-model="searchValue"
        type="search"
        placeholder="Buscar vehiculo..."
      >
      </b-form-input>
      <b-button
        style="
          background-color: rgb(94,80,238);
          height: 50px;
          width: auto;
          font-size: 18px;
          margin-right: 15px;
          margin-left: 20px;
        "
        @click="showModal = !showModal"
        type="submit"
      >
        <i class="bi bi-pc-display-horizontal"></i>
        Agregar vehiculo
      </b-button>
    </b-row>
    <EasyDataTable
      rows-per-page-message="registros por pagina"
      empty-message="No se encuentran registros"
      table-class-name="customize-table"
      buttons-pagination
      border-cell
      :loading="isloading"
      :headers="fields"
      :items="vehicles"
      :rows-per-page="5"
      :search-field="searchField"
      :search-value="searchValue"
    >
      <template #header-actions="header">
        {{ header.text }}
      </template>
      <template #item-actions="items">
        <b-button
          @click="RemoveVehicle(items.vehiculoId)"
          class="m-1"
          variant="outline-danger"
          ><i class="bi bi-trash3"></i
        ></b-button>
        <b-button class="m-1" variant="outline-warning"
          :to="{
            name: 'EquiposComputo-Edit',
            params: { EquipoComputoId: items.vehiculoId},
          }"
        >
          <i class="bi bi-pencil-square" />
        </b-button>
      </template>
    </EasyDataTable>
    <b-modal
      v-model="showModal"
      title="Agregar Departamento"
      size="xl"
      centered
      button-size="lg"
      hide-footer
    >
      <Form @submit="addVehicle">
        <b-row cols="3">
          <b-col>
            <b-form-group class="mt-3" label="Marca">
              <Field name="BrandField" :rules="validateBrand" as="text">
                <b-form-input
                  placeholder="Ingresa la marca y/o modelo del equipo"
                  v-model="vehiclesFields.marca"
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
                  v-model="vehiclesFields.modelo"
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
                  v-model="vehiclesFields.color"
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
                  v-model="vehiclesFields.placa"
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
                  v-model="vehiclesFields.numeroSerie"
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
                  v-model="vehiclesFields.añoLanzamiento"
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
                  v-model="vehiclesFields.puertas"
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
                  v-model="vehiclesFields.transmisión"
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
        </b-row>
        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            @click="resetVehicleFields()"
          >
            Cancelar
          </b-button>
          <b-button class="w-auto m-2" variant="success" type="submit">
            Guardar
          </b-button>
        </b-row>
      </Form>
    </b-modal>
  </b-card>
</template>

<script>
import VehiclesServices from '@/Services/vehicles.Services'
import { Form, Field, ErrorMessage } from 'vee-validate'
import { ref, inject } from 'vue'
// import { useToast } from 'vue-toast-notification'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    Form,
    Field,
    ErrorMessage,
    EasyDataTable: window['vue3-easy-data-table']
  },
  setup () {
    const swal = inject('$swal')
    const { getVehicles, createVehicle, deleteVehicle } = VehiclesServices()
    // const $toast = useToast()
    const vehicles = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const typeTransmissions = ref([{ id: 1, nombre: 'Manual' }, { id: 2, nombre: 'Automático' }])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('area.nombre')
    const brandState = ref(false)
    const modelState = ref(false)
    const colorState = ref(false)
    const licensePlateState = ref(false)
    const serialNumberState = ref(false)
    const releaseYearState = ref(false)
    const stateComputerState = ref(false)
    const doorsState = ref(false)
    const transmissionState = ref(false)
    const showModal = ref(false)
    const vehiclesFields = ref({
      vehiculoId: 0,
      marca: null,
      modelo: null,
      color: null,
      placa: null,
      numeroSerie: null,
      añoLanzamiento: null,
      puertas: null,
      transmisión: null,
      archivado: false
    })
    const vehiclesFieldsBlank = ref(JSON.parse(JSON.stringify(vehiclesFields)))
    const fields = ref([
      { value: 'vehiculoId', text: 'ID', sortable: true },
      { value: 'marca', text: 'Marca' },
      { value: 'modelo', text: 'Submarca' },
      { value: 'color', text: 'Color' },
      { value: 'placa', text: 'Placa' },
      { value: 'numeroSerie', text: 'No. Serie' },
      { value: 'añoLanzamiento', text: 'Modelo' },
      { value: 'puertas', text: 'Cantidad de puertas' },
      { value: 'transmisión', text: 'Tipo de transmision' },
      { value: 'actions', text: 'Acciones' }
    ])
    getVehicles(data => {
      vehicles.value = data
      if (vehicles.value.length > 0) {
        isloading.value = false
      } else {
        if (vehicles.value.length <= 0) {
          isloading.value = false
        }
      }
    })
    const onFiltered = filteredItems => {
      currentPage.value = 1
    }
    // VALIDATIONS
    const validateBrand = () => {
      if (!vehiclesFields.value.marca) {
        brandState.value = false
        return 'Este campo es requerido'
      }
      brandState.value = true
      return true
    }
    const validateModel = () => {
      if (!vehiclesFields.value.modelo) {
        modelState.value = false
        return 'Este campo es requerido'
      }
      modelState.value = true
      return true
    }
    const validateColor = () => {
      if (!vehiclesFields.value.color) {
        colorState.value = false
        return 'Este campo es requerido'
      }
      colorState.value = true
      return true
    }
    const validateLicensePlate = () => {
      if (!vehiclesFields.value.placa) {
        licensePlateState.value = false
        return 'Este campo es requerido'
      }
      if (!/^(?=.*\d)(?=.*[a-zA-Z])([A-ZñÑáéíóúÁÉÍÓÚ])[A-Z0-9]{5,6}$/.test(vehiclesFields.value.placa)) {
        licensePlateState.value = false
        return 'Este campo solo acepta mayusculas y numeros y no acepta mas de 6 caracteres'
      }
      licensePlateState.value = true
      return true
    }
    const validateSerialNumber = () => {
      if (!vehiclesFields.value.numeroSerie) {
        serialNumberState.value = false
        return 'Este campo es requerido'
      }
      if (!/^(?=.*\d)(?=.*[a-zA-Z])([A-ZñÑáéíóúÁÉÍÓÚ])[A-Z0-9]{17,17}$/.test(vehiclesFields.value.numeroSerie)) {
        serialNumberState.value = false
        return 'Este campo solo acepta mayusculas y numeros y no acepta mas de 17 caracteres'
      }
      serialNumberState.value = true
      return true
    }
    const validateReleaseYear = () => {
      if (!vehiclesFields.value.añoLanzamiento) {
        releaseYearState.value = false
        return 'Este campo es requerido'
      }
      releaseYearState.value = true
      return true
    }
    const validateDoors = () => {
      if (!vehiclesFields.value.puertas) {
        doorsState.value = false
        return 'Este campo es requerido'
      }
      if (vehiclesFields.value.puertas < 2) {
        doorsState.value = false
        return 'No puede contener un vehiculo con menos de 2 puertas'
      }
      if (vehiclesFields.value.puertas > 5) {
        doorsState.value = false
        return 'No puede contener un vehiculo con mas de 5 puertas'
      }
      doorsState.value = true
      return true
    }
    const validateTransmission = () => {
      if (!vehiclesFields.value.transmisión) {
        transmissionState.value = false
        return 'Este campo es requerido'
      }
      transmissionState.value = true
      return true
    }
    const refreshTable = () => {
      isloading.value = true
      getVehicles(data => {
        vehicles.value = data
        if (vehicles.value.length > 0) {
          isloading.value = false
        } else {
          if (vehicles.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }
    const addVehicle = () => {
      createVehicle(vehiclesFields.value, data => {
        refreshTable()
        swal.fire({
          title: 'Equipo registrado correctamente!',
          text:
            'El equipo de computo se ha registrado al sistema satisfactoriamente.',
          icon: 'success'
        })
      })
      resetVehicleFields()
    }
    const resetVehicleFields = () => {
      showModal.value = false
      brandState.value = false
      modelState.value = false
      colorState.value = false
      licensePlateState.value = false
      serialNumberState.value = false
      releaseYearState.value = false
      stateComputerState.value = false
      doorsState.value = false
      transmissionState.value = false
      vehiclesFields.value = JSON.parse(JSON.stringify(vehiclesFieldsBlank))
    }
    const RemoveVehicle = vehiculoId => {
      isloading.value = true
      swal
        .fire({
          title: '¿Estas seguro?',
          text: 'No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Si, archivar vehículo!',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            swal
              .fire({
                title: 'Vehículo archivado!',
                text:
                  'El vehículo de computo ha sido archivado satisfactoriamente .',
                icon: 'success'
              })
              .then(result => {
                if (result.isConfirmed) {
                  deleteVehicle(vehiculoId, data => {
                    refreshTable()
                  })
                }
              })
          } else {
            isloading.value = false
          }
        })
    }
    return {
      vehicles,
      fields,
      perPage,
      currentPage,
      filter,
      perPageSelect,
      vehiclesFieldsBlank,
      vehiclesFields,
      isloading,
      searchValue,
      searchField,
      onFiltered,
      addVehicle,
      refreshTable,
      RemoveVehicle,
      brandState,
      modelState,
      colorState,
      licensePlateState,
      serialNumberState,
      releaseYearState,
      stateComputerState,
      doorsState,
      transmissionState,
      typeTransmissions,
      showModal,

      validateBrand,
      validateModel,
      validateColor,
      validateLicensePlate,
      resetVehicleFields,
      validateSerialNumber,
      validateReleaseYear,
      validateDoors,
      validateTransmission
    }
  }
}
</script>

<style></style>
