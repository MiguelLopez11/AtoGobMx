<template>
  <b-card class="m-2">
    <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
  </b-card>
  <b-card class="m-2">
    <b-row align-h="end" class="mb-3 mr-1">
      <b-form-input
        size="lg"
        style="width: 350px"
        v-model="searchValue"
        type="search"
        placeholder="Buscar Cementerios..."
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
        <!-- v-b-modal.modal-cementery -->
        <i class="bi bi-person-plus-fill"></i>
        Agregar Cementerios
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
      :items="cementeryService"
      :rows-per-page="5"
      :search-field="searchField"
      :search-value="searchValue"
      theme-color="#7367f0"
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
            @click="RemoveCementeryService(items.cementeriosId)"
            class="m-1"
            variant="outline-danger"
            ><i class="bi bi-trash3"> Archivar</i></b-dropdown-item
          >
          <b-dropdown-item
            class="m-1"
            variant="outline-warning"
            :to="{
              name: 'Cementerios-Edit',
              params: { CementeriosId: items.cementeriosId }
            }"
            ><i class="bi bi-pencil-square" /> Editar</b-dropdown-item
          >
        </b-dropdown>
      </template>
    </EasyDataTable>

    <b-modal
      id="modal-cementery"
      tittle="Agregar Cementerios"
      v-model="showModal"
      size="xl"
      hide-footer
      button-size="lg"
      lazy
    >
      <Form @submit="addCementeryService">
        <b-row cols="2">
          <!--Agregar propietario -->
          <b-col>
            <b-form-group class="mt-3" label="Nombre del propietario">
              <Field
                name="PropietaryField"
                :rules="validatePropietary"
                as="text"
              >
                <b-form-input
                  v-model="cementeryServiceFields.nombrePropietario"
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
          <!--Agregar espacios -->
          <b-col>
            <b-form-group class="mt-3" label="Numero de espacios">
              <Field name="SpacesField" :rules="validateSpaces" as="number">
                <b-form-input
                  v-model="cementeryServiceFields.numeroEspasios"
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
          <!--Agregar metros correspondientes -->
          <b-col>
            <b-form-group class="mt-3" label="Metros correspondientes">
              <Field name="MeterField" :rules="validateMeter" as="number">
                <b-form-input
                  v-model="cementeryServiceFields.metrosCorrespondientes"
                  :state="MeterState"
                  type="number"
                  min="1"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="MeterField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--Agregar espacios -->
          <b-col>
            <b-form-group class="mt-3" label="Espacios Disponibles">
              <Field
                name="AvailableField"
                :rules="validateAvailable"
                as="number"
              >
                <b-form-input
                  v-model="cementeryServiceFields.espaciosDisponibles"
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
          <b-button v-b-toggle.collapse-1 variant="primary"> Mapa </b-button>
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
                style="width: 1000px; height: 500px"
              >
                <GMapMarker
                  :zoom="10"
                  :key="index"
                  v-for="(m, index) in markers"
                  :position="m.position"
                  :clickable="true"
                  :draggable="true"
                  @click="center = m.position"
                />
              </GMapMap>
            </b-card>
          </b-collapse>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            @click="resetCementeryServiceFields"
          >
            <!-- v-b-modal.modal-cementery -->
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
import CementeryService from '@/Services/cementery.Services'
import { Form, Field, ErrorMessage } from 'vee-validate'
import { ref, inject } from 'vue'
// import { useToast } from 'vue-toast-notification'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    EasyDataTable: window['vue3-easy-data-table'],
    Form,
    Field,
    ErrorMessage
  },
  setup () {
    const swal = inject('$swal')
    const showModal = ref(false)
    const { getCementery, createCementery, deleteCementery } =
      CementeryService()
    // const $toast = useToast()
    const cementeryService = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('nombrePropietario')
    const PropietaryState = ref(false)
    const SpacesState = ref(false)
    const MeterState = ref(false)
    const AvailableState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      {
        text: 'Cementerios',
        to: '/ServiciosPublicos/CementeriosPublicos/list'
      },
      { text: 'Gabetas' }
    ])

    const cementeryServiceFields = ref({
      cementeriosId: 0,
      nombrePropietario: '',
      numeroEspasios: null,
      metrosCorrespondientes: null,
      espaciosDisponibles: null,
      archivado: false
    })

    const CementeryServiceFieldsBlank = ref(
      JSON.parse(JSON.stringify(cementeryServiceFields))
    )
    const markers = ref([
      {
        position: {
          lat: 20.5546629,
          lng: -102.4953904
        }
      }
    ])

    // GEvent.addListener(map, 'click', function (overlay, point) {
    //   if (point) {
    //     marker.setPoint(point)
    //     document.posicion.x.value = point.x
    //     document.posicion.y.value = point.y
    //   }
    // })

    const center = ref({ lat: 20.5546629, lng: -102.4953904 })
    const fields = ref([
      { value: 'nombrePropietario', text: 'Nombre de propietario' },
      { value: 'numeroEspasios', text: 'Espacios' },
      { value: 'metrosCorrespondientes', text: 'Metros' },
      { value: 'espaciosDisponibles', text: 'Espacios Disponibles' },
      { value: 'actions', text: 'Acciones' }
    ])

    const resetCementeryServiceFields = () => {
      showModal.value = false
      cementeryServiceFields.value = JSON.parse(
        JSON.stringify(CementeryServiceFieldsBlank)
      )
      PropietaryState.value = false
      SpacesState.value = false
      MeterState.value = false
      AvailableState.value = false
    }

    getCementery(data => {
      cementeryService.value = data

      if (cementeryService.value.length > 0) {
        isloading.value = false
      } else {
        if (cementeryService.value.length <= 0) {
          isloading.value = false
        }
      }
    })

    const onFiltered = filteredItems => {
      currentPage.value = 1
    }

    const validatePropietary = () => {
      if (!cementeryServiceFields.value.nombrePropietario) {
        PropietaryState.value = false
        return 'Este campo es requerido'
      }

      if (
        !/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(
          cementeryServiceFields.value.nombrePropietario
        )
      ) {
        PropietaryState.value = false
        return 'Este campo solo puede contener letras'
      }

      if (!cementeryServiceFields.value.nombrePropietario.trim().length > 0) {
        PropietaryState.value = false
        return 'Este campo no puede contener espacios'
      }

      PropietaryState.value = true
      return true
    }

    const validateSpaces = () => {
      if (!cementeryServiceFields.value.numeroEspasios) {
        SpacesState.value = false
        return 'Este campo es requerido'
      }

      if (!/^[0-9]+$/i.test(cementeryServiceFields.value.numeroEspasios)) {
        SpacesState.value = false
        return 'Este campo solo puede contener numeros'
      }

      if (!cementeryServiceFields.value.numeroEspasios.trim().length > 0) {
        SpacesState.value = false
        return 'Este campo no puede contener espacios'
      }

      SpacesState.value = true
      return true
    }

    const validateMeter = () => {
      if (!cementeryServiceFields.value.metrosCorrespondientes) {
        MeterState.value = false
        return 'Este campo es requerido'
      }

      if (
        !/^\d*\.\d+$/i.test(cementeryServiceFields.value.metrosCorrespondientes)
      ) {
        MeterState.value = false
        return 'Este campo solo puede contener numeros'
      }

      if (
        !cementeryServiceFields.value.metrosCorrespondientes.trim().length > 0
      ) {
        MeterState.value = false
        return 'Este campo no puede contener espacios'
      }

      MeterState.value = true
      return true
    }

    const validateAvailable = () => {
      if (!cementeryServiceFields.value.espaciosDisponibles) {
        AvailableState.value = false
        return 'Este campo es requerido'
      }

      if (!/^[0-9]+$/i.test(cementeryServiceFields.value.espaciosDisponibles)) {
        AvailableState.value = false
        return 'Este campo solo puede contener numeros'
      }

      if (!cementeryServiceFields.value.espaciosDisponibles.trim().length > 0) {
        AvailableState.value = false
        return 'Este campo no puede contener espacios'
      }

      AvailableState.value = true
      return true
    }

    const refreshTable = () => {
      isloading.value = true
      getCementery(data => {
        cementeryService.value = data

        if (cementeryService.value.length > 0) {
          isloading.value = false
        } else {
          if (cementeryService.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }

    const addCementeryService = () => {
      createCementery(cementeryServiceFields.value, data => {
        refreshTable()
        swal.fire({
          title: '¡Cementerios agregado correctamente!',
          text: 'Cementerios registrado satisfactoriamente',
          icon: 'success'
        })
      })
      showModal.value = false
      resetCementeryServiceFields()
    }

    const RemoveCementeryService = cementeryId => {
      isloading.value = true
      swal
        .fire({
          title: '¿Estas seguro',
          text: 'No podras revertir esto',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Si, Archivar Cementerios!',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            deleteCementery(cementeryId, data => {
              refreshTable()
            })
            swal.fire({
              title: '¡Cementerio archivado!',
              text: 'El cementerio ha sido archivado satisfactoriamente.',
              icon: 'success'
            })
          } else {
            isloading.value = false
          }
        })
    }

    return {
      cementeryService,
      cementeryServiceFields,
      breadcrumbItems,
      perPage,
      currentPage,
      filter,
      showModal,
      perPageSelect,
      isloading,
      searchValue,
      searchField,
      CementeryServiceFieldsBlank,
      fields,
      PropietaryState,
      SpacesState,
      MeterState,
      AvailableState,
      markers,
      center,

      onFiltered,
      validatePropietary,
      validateSpaces,
      validateMeter,
      validateAvailable,
      addCementeryService,
      RemoveCementeryService,
      refreshTable,
      resetCementeryServiceFields
    }
  }
}
</script>

<style></style>
