<template>
  <b-card class="m-2">
    <b-row align-h="end" class="mb-3 mr-1">
      <b-form-input
        size="lg"
        style="width: 350px"
        v-model="searchValue"
        type="search"
        placeholder="Buscar vehiculo..."
      ></b-form-input>
      <b-button
        variant="primary"
        style="
          background-color: rgb(94, 80, 238);
          height: 50px;
          width: auto;
          font-size: 18px;
          margin-right: 15px;
          margin-left: 20px;
        "
        @click="showModal = !showModal"
        type="submit"
      >
        <i class="bi bi-person-plus-fill"></i>
        Agregar vehiculo
      </b-button>
    </b-row>
    <EasyDataTable
      rows-per-page-message="registros por pagina"
      empty-message="No se encontro ningun registro"
      table-class-name="customize-table"
      buttons-pagination
      border-cell
      :loading="isloading"
      :headers="fields"
      :items="publicWorkVehicle"
      :rows-per-page="5"
      :search-field="searchField"
      :search-value="searchValue"
      :table-height="330"
    >
      <template #header-actions="header">
        {{ header.text }}
      </template>
      <template #item-actions="items">
        <b-dropdown
          id="ActionsDropdown"
          size="lg"
          style="text-color: black"
          variant="link"
          toggle-class="text-decoration-none"
          dropright
          no-caret
        >
          <template #button-content>
            <i class="bi bi-three-dots-vertical"></i>
          </template>
          <b-dropdown-item
            @click="RemovePublicWorkVehicle(items.OpVehiculoId)"
            class="m-1"
            variant="outline-danger"
            ><i class="bi bi-trash3"> Archivar</i></b-dropdown-item
          >
        </b-dropdown>
      </template>
    </EasyDataTable>
    <b-modal
      id="modal-lightingemployee"
      tittle="Agregar vehiculos a obras publicas"
      v-model="showModal"
      size="xl"
      hide-footer
      button-size="lg"
      lazy
    >
      <Form @submit="addPublicWorkVehicle">
        <b-row>
          <!-- Nombre del vehiculo -->
          <b-col>
            <b-form-group
              class="mt-3"
              label="Selecciona un o varios vehículo(s):"
            >
              <Field name="VehicleField" :rules="validateVehicle" as="text">
                <v-select
                  multiple
                  v-model="vehicleSelected"
                  :options="vehicles"
                  :key="vehicles.vehiculoId"
                  :reduce="vehicles => vehicles.vehiculoId"
                  label="color"
                >
                  <template
                    v-slot:option="{
                      marca,
                      modelo,
                      añoLanzamiento,
                      color,
                      placa
                    }"
                  >
                    <span
                      >{{ marca }}-{{ modelo }}-{{ añoLanzamiento }} -
                      {{ color }}- {{ placa }}</span
                    >
                  </template>
                  <template
                    v-slot:selected-option="{
                      marca,
                      modelo,
                      añoLanzamiento,
                      color,
                      placa
                    }"
                  >
                    <span
                      >{{ marca }}-{{ modelo }}-{{ añoLanzamiento }} -
                      {{ color }}- {{ placa }}</span
                    >
                  </template>
                </v-select>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="VehicleField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            @click="resetPublicWorkVehicleFields"
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
import PublicWorkVehicleServices from '@/Services/publicworksvehicles.Services'
import VehiclesServices from '@/Services/vehicles.Services'
import { Form, Field, ErrorMessage } from 'vee-validate'
import { ref, inject } from 'vue'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    EasyDataTable: window['vue3-easy-data-table'],
    Form,
    Field,
    ErrorMessage
  },
  props: {
    ObraId: {
      type: Number,
      required: true
    }
  },
  setup (props) {
    const swal = inject('$swal')
    const showModal = ref(false)
    const {
      getPublicWorkVehicle,
      createPublicWorkVehicle,
      deletePublicWorkVehicle
    } = PublicWorkVehicleServices()
    const { getVehicles } = VehiclesServices()
    const publicWorkVehicle = ref([])
    const vehicles = ref([])
    const vehicle = ref({
      marca: '',
      submarca: ''
    })
    const vehicleSelected = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('vehiculoId')
    const NameState = ref(false)
    const publicWorkVehicleFields = ref({
      opVehiculoId: 0,
      obraId: props.ObraId,
      vehiculoId: 0,
      archivado: false
    })

    const getFieldText = item => {
      return `${item.marca} - ${item.submarca} `
    }

    getVehicles(data => {
      vehicles.value = data
      if (data.length === 0) {
        swal.fire({
          title: 'No se encuentra un vehiculo',
          text: 'No se encuentran vehiculos registrados en el departamento seleccionado, registre primero un vehiculo para continuar',
          icon: 'warning'
        })
      }
    })

    const PublicWorkVehicleFieldsBlank = ref(
      JSON.parse(JSON.stringify(publicWorkVehicleFields))
    )

    const fields = ref([
      // { value: 'vehiculoAlumbradoId', text: 'ID', sortable: true },
      { value: 'vehiculo.marca', text: 'Marca' },
      { value: 'vehiculo.modelo', text: 'Modelo' },
      { value: 'vehiculo.añoLanzamiento', text: 'Año de lanzamiento' },
      { value: 'vehiculo.color', text: 'Color' },
      { value: 'vehiculo.placa', text: 'Placa' },
      { value: 'actions', text: 'Acciones' }
    ])

    const resetPublicWorkVehicleFields = () => {
      showModal.value = false
      publicWorkVehicleFields.value = JSON.parse(
        JSON.stringify(PublicWorkVehicleFieldsBlank)
      )
      NameState.value = false
    }

    getPublicWorkVehicle(data => {
      publicWorkVehicle.value = data

      if (publicWorkVehicle.value.length > 0) {
        isloading.value = false
      } else {
        if (publicWorkVehicle.value.length <= 0) {
          isloading.value = false
        }
      }
    })

    const onFiltered = filteredItems => {
      currentPage.value = 1
    }

    const validateVehicle = () => {
      if (!vehicleSelected.value.length === 0) {
        NameState.value = false
        return 'Este campo es requerido'
      }

      NameState.value = true
      return true
    }

    const refreshTable = () => {
      isloading.value = true
      getPublicWorkVehicle(data => {
        publicWorkVehicle.value = data

        if (publicWorkVehicle.value.length > 0) {
          isloading.value = false
        } else {
          if (publicWorkVehicle.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }

    const addPublicWorkVehicle = () => {
      for (let i = 0; i < vehicleSelected.value.length; i++) {
        publicWorkVehicleFields.value.vehiculoId = vehicleSelected.value[i]
        createPublicWorkVehicle(publicWorkVehicleFields.value, data => {
          refreshTable()
        })
      }
      swal.fire({
        title: '¡El vehiculo se agregado correctamente!',
        text: 'El vehiculo se ha registrado satisfactoriamente',
        icon: 'success'
      })
      showModal.value = false
      resetPublicWorkVehicleFields()
    }

    const RemovePublicWorkVehicle = OpVehiculoId => {
      isloading.value = true
      swal
        .fire({
          title: '¿Estas seguro',
          text: 'No podras revertir esto',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Si, Archivar vehiculo!',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            deletePublicWorkVehicle(OpVehiculoId, data => {
              refreshTable()
            })
            swal.fire({
              title: '¡vehiculo archivado!',
              text: 'El vehiculo ha sido archivado satisfactoriamente.',
              icon: 'success'
            })
          } else {
            isloading.value = false
          }
        })
    }

    return {
      publicWorkVehicle,
      publicWorkVehicleFields,
      perPage,
      currentPage,
      filter,
      showModal,
      perPageSelect,
      isloading,
      searchValue,
      searchField,
      PublicWorkVehicleFieldsBlank,
      fields,
      NameState,
      vehicles,
      vehicleSelected,
      vehicle,
      getFieldText,

      onFiltered,
      addPublicWorkVehicle,
      RemovePublicWorkVehicle,
      refreshTable,
      validateVehicle,
      resetPublicWorkVehicleFields
    }
  }
}
</script>

<style></style>
