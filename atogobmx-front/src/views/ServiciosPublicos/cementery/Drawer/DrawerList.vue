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
        placeholder="Buscar Gabetas..."
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
        Agregar Gabeta
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
      :items="drawerService"
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
            @click="RemoveDrawerService(items.gabetaId)"
            class="m-1"
            variant="outline-danger"
            ><i class="bi bi-trash3"> Archivar</i></b-dropdown-item
          >
          <b-dropdown-item
            class="m-1"
            variant="outline-warning"
            :to="{
              name: 'Gabetas-Edit',
              params: { GabetaId: items.gabetaId }
            }"
            ><i class="bi bi-pencil-square" /> Editar</b-dropdown-item
          >
        </b-dropdown>
      </template>
    </EasyDataTable>

    <b-modal
      id="modal-drawer"
      tittle="Agregar Gabeta"
      v-model="showModal"
      size="xl"
      hide-footer
      button-size="lg"
      lazy
    >
      <Form @submit="addDrawerService">
        <b-row cols="2">
          <!-- Registrar el panteon de la gabeta -->
          <b-col>
            <b-form-group class="mt-3" label="Nombre cementerio">
              <Field
                name="PropietaryField"
                :rules="validatePropietary"
                as="text"
              >
                <b-form-select
                  v-model="drawerServiceFields.cementerioId"
                  autofocus
                  :state="PropietaryState"
                  :options="cementeryService"
                  value-field="cementerioId"
                  text-field="nombreCementerio"
                ></b-form-select>
              </Field>
              <ErrorMessage class="text-danger" name="StatusField" />
            </b-form-group>
          </b-col>
          <!--Agregar propietario -->
          <b-col>
            <b-form-group class="mt-3" label="Nombre del propietario">
              <Field
                name="PropietaryField"
                :rules="validatePropietary"
                as="text"
              >
                <b-form-input
                  v-model="drawerServiceFields.nombrePropietario"
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
                  v-model="drawerServiceFields.numeroEspasios"
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
                  v-model="drawerServiceFields.metrosCorrespondientes"
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
            <b-form-group class="mt-3" label="Espacios disponibles">
              <Field
                name="AvailableField"
                :rules="validateAvailable"
                as="number"
              >
                <b-form-input
                  v-model="drawerServiceFields.espaciosDisponibles"
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
          <!-- {{markers}} -->
        </b-row>
        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            @click="resetDrawerServiceFields"
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
import DrawerService from '@/Services/drawer.Services'
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
    const { getDrawer, createDrawer, deleteDrawer } =
      DrawerService()
    const { getCementery } = CementeryService()
    const drawerService = ref([])
    const cementeryService = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('nombrePropietario')
    const SpacesState = ref(false)
    const MeterState = ref(false)
    const AvailableState = ref(false)
    const PropietaryState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      {
        text: 'Cementerios',
        to: '/ServiciosPublicos/CementeriosPublicos/list'
      },
      { text: 'Gabetas' }
    ])
    const drawerServiceFields = ref({
      gabetaId: 0,
      nombrePropietario: '',
      numeroEspasios: null,
      metrosCorrespondientes: null,
      espaciosDisponibles: null,
      cementerioId: null,
      latitud: null,
      longitud: null,
      archivado: false
    })

    getCementery(data => {
      cementeryService.value = data
      if (data.length === 0) {
        swal.fire({
          title: 'No se encuentra un cementerio registrado!',
          text: 'No se encuentra un cementerio registrado en el departamento seleccionado, registre primero un tipo de cementerio para continuar',
          icon: 'warning'
        })
      }
    })

    const DrawerServiceFieldsBlank = ref(
      JSON.parse(JSON.stringify(drawerServiceFields))
    )

    const center = ref({ lat: 20.5546629, lng: -102.4953904 })

    const updateCoordinates = location => {
      drawerServiceFields.value.latitud = location.latLng.lat()
      drawerServiceFields.value.longitud = location.latLng.lng()
    }
    const fields = ref([
      { value: 'cementerio.nombreCementerio', text: 'Nombre del cementerio' },
      { value: 'nombrePropietario', text: 'Nombre de propietario' },
      { value: 'numeroEspasios', text: 'Espacios' },
      { value: 'metrosCorrespondientes', text: 'Metros' },
      { value: 'espaciosDisponibles', text: 'Espacios disponibles' },
      { value: 'actions', text: 'Acciones' }
    ])

    const resetDrawerServiceFields = () => {
      showModal.value = false
      drawerServiceFields.value = JSON.parse(
        JSON.stringify(DrawerServiceFieldsBlank)
      )
      PropietaryState.value = false
      SpacesState.value = false
      MeterState.value = false
      AvailableState.value = false
    }

    getDrawer(data => {
      drawerService.value = data

      if (drawerService.value.length > 0) {
        isloading.value = false
      } else {
        if (drawerService.value.length <= 0) {
          isloading.value = false
        }
      }
    })

    const onFiltered = filteredItems => {
      currentPage.value = 1
    }

    const validateStatus = () => {
      if (!drawerServiceFields.value.cementerioId) {
        PropietaryState.value = false
        return 'Este campo es requerido'
      }
      PropietaryState.value = true
      return true
    }

    const validatePropietary = () => {
      if (!drawerServiceFields.value.nombrePropietario) {
        PropietaryState.value = false
        return 'Este campo es requerido'
      }

      if (
        !/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(
          drawerServiceFields.value.nombrePropietario
        )
      ) {
        PropietaryState.value = false
        return 'Este campo solo puede contener letras'
      }

      if (!drawerServiceFields.value.nombrePropietario.trim().length > 0) {
        PropietaryState.value = false
        return 'Este campo no puede contener espacios'
      }

      PropietaryState.value = true
      return true
    }

    const validateSpaces = () => {
      if (!drawerServiceFields.value.numeroEspasios) {
        SpacesState.value = false
        return 'Este campo es requerido'
      }

      if (!/^[0-9]+$/i.test(drawerServiceFields.value.numeroEspasios)) {
        SpacesState.value = false
        return 'Este campo solo puede contener numeros'
      }

      if (!drawerServiceFields.value.numeroEspasios.trim().length > 0) {
        SpacesState.value = false
        return 'Este campo no puede contener espacios'
      }

      SpacesState.value = true
      return true
    }

    const validateMeter = () => {
      if (!drawerServiceFields.value.metrosCorrespondientes) {
        MeterState.value = false
        return 'Este campo es requerido'
      }

      if (
        !/^\d*\.\d+$/i.test(drawerServiceFields.value.metrosCorrespondientes)
      ) {
        MeterState.value = false
        return 'Este campo solo puede contener numeros'
      }

      if (
        !drawerServiceFields.value.metrosCorrespondientes.trim().length > 0
      ) {
        MeterState.value = false
        return 'Este campo no puede contener espacios'
      }

      MeterState.value = true
      return true
    }

    const validateAvailable = () => {
      if (!drawerServiceFields.value.espaciosDisponibles) {
        AvailableState.value = false
        return 'Este campo es requerido'
      }

      if (!/^[0-9]+$/i.test(drawerServiceFields.value.espaciosDisponibles)) {
        AvailableState.value = false
        return 'Este campo solo puede contener numeros'
      }

      if (!drawerServiceFields.value.espaciosDisponibles.trim().length > 0) {
        AvailableState.value = false
        return 'Este campo no puede contener espacios'
      }

      AvailableState.value = true
      return true
    }

    const refreshTable = () => {
      isloading.value = true
      getDrawer(data => {
        drawerService.value = data

        if (drawerService.value.length > 0) {
          isloading.value = false
        } else {
          if (drawerService.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }

    const addDrawerService = () => {
      createDrawer(drawerServiceFields.value, data => {
        refreshTable()
        swal.fire({
          title: '¡Cementerios agregado correctamente!',
          text: 'Cementerios registrado satisfactoriamente',
          icon: 'success'
        })
      })
      showModal.value = false
      resetDrawerServiceFields()
    }

    const RemoveDrawerService = drawerId => {
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
            deleteDrawer(drawerId, data => {
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
      drawerService,
      drawerServiceFields,
      cementeryService,
      breadcrumbItems,
      perPage,
      currentPage,
      filter,
      showModal,
      perPageSelect,
      isloading,
      searchValue,
      searchField,
      DrawerServiceFieldsBlank,
      fields,
      PropietaryState,
      SpacesState,
      MeterState,
      AvailableState,
      center,
      updateCoordinates,

      onFiltered,
      validatePropietary,
      validateSpaces,
      validateMeter,
      validateAvailable,
      validateStatus,
      addDrawerService,
      RemoveDrawerService,
      refreshTable,
      resetDrawerServiceFields
    }
  }
}
</script>

<style></style>
