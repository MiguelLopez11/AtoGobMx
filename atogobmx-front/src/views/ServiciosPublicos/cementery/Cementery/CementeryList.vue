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
        placeholder="Buscar cementerio..."
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
        Agregar Cementerio
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
            @click="RemoveCementeryService(items.cementerioId)"
            class="m-1"
            variant="outline-danger"
            ><i class="bi bi-trash3"> Archivar</i></b-dropdown-item
          >
          <b-dropdown-item
            class="m-1"
            variant="outline-warning"
            :to="{
              name: 'Cementerios-Edit',
              params: { CementerioId: items.cementerioId }
            }"
            ><i class="bi bi-pencil-square" /> Editar</b-dropdown-item
          >
        </b-dropdown>
      </template>
    </EasyDataTable>

    <b-modal
      id="modal-Cementery"
      tittle="Agregar Cementerios"
      v-model="showModal"
      size="xl"
      hide-footer
      button-size="lg"
      lazy
    >
      <Form @submit="addCementeryService">
        <b-row cols="2">
          <!--Agregar Nombre Cementerio -->
          <b-col>
            <b-form-group class="mt-3" label="Nombre del cementerio">
              <Field
                name="NameCementeryField"
                :rules="validateNameCementery"
                as="text"
              >
                <b-form-input
                  v-model="cementeryServiceFields.nombreCementerio"
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
          <!--Agregar comunidad -->
          <b-col>
            <b-form-group class="mt-3" label="Comunidad">
                <b-form-input
                  v-model="cementeryServiceFields.comunidad"
                >
                </b-form-input>
            </b-form-group>
          </b-col>
          <!--Agregar Localidad -->
          <b-col>
            <b-form-group class="mt-3" label="Localidad">
              <Field name="LocationField" :rules="validateLocation" as="text">
                <b-form-input
                  v-model="cementeryServiceFields.localidad"
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
          <!--Agregar Calle -->
          <b-col>
            <b-form-group class="mt-3" label="Calle">
              <Field name="StreetField" :rules="validateStreet" as="text">
                <b-form-input
                  v-model="cementeryServiceFields.calle"
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
          <!--Agregar Numero Exterior -->
          <b-col>
            <b-form-group class="mt-3" label="Numero exterior">
              <Field
                name="NumberOutsideField"
                :rules="validateNumberOutside"
                as="text"
              >
                <b-form-input
                  v-model="cementeryServiceFields.numeroExterior"
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
    const {
      getCementery,
      createCementery,
      deleteCementery
    } = CementeryService()
    const cementeryService = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('nombreCementerio')
    const NameCementeryState = ref(false)
    // const MunicipalityState = ref(false)
    const LocationState = ref(false)
    const StreetState = ref(false)
    const NumberOutsideState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      {
        text: 'Cementerios',
        to: '/ServiciosPublicos/CementeriosPublicos/list'
      },
      { text: 'Cementerios' }
    ])

    const cementeryServiceFields = ref({
      cementerioId: 0,
      nombreCementerio: null,
      comunidad: null,
      localidad: null,
      calle: null,
      numeroExterior: null,
      latitud: 0,
      longitud: 0,
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

    const center = ref({ lat: 20.5546629, lng: -102.4953904 })

    const updateCoordinates = (location) => {
      markers.value = {
        position: {
          lat: location.latLng.lat(),
          lng: location.latLng.lng()
        }
      }
    }

    const fields = ref([
      { value: 'nombreCementerio', text: 'Nombre cementerio' },
      { value: 'comunidad', text: 'Comunidad' },
      { value: 'localidad', text: 'Localidad' },
      { value: 'calle', text: 'Calle' },
      { value: 'numeroExterior', text: 'Numero exterior' },
      { value: 'actions', text: 'Acciones' }
    ])

    const resetCementeryServiceFields = () => {
      showModal.value = false
      cementeryServiceFields.value = JSON.parse(
        JSON.stringify(CementeryServiceFieldsBlank)
      )
      NameCementeryState.value = false
      // MunicipalityState.value = false
      LocationState.value = false
      StreetState.value = false
      NumberOutsideState.value = false
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

    const validateNameCementery = () => {
      if (!cementeryServiceFields.value.nombreCementerio) {
        NameCementeryState.value = false
        return 'Este campo es requerido'
      }

      if (
        !/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(
          cementeryServiceFields.value.nombreCementerio
        )
      ) {
        NameCementeryState.value = false
        return 'Este campo solo puede contener letras'
      }

      if (
        !cementeryServiceFields.value.nombreCementerio.trim().length > 0
      ) {
        NameCementeryState.value = false
        return 'Este campo no puede contener espacios'
      }

      NameCementeryState.value = true
      return true
    }

    // const validateMunicipality = () => {
    //   if (!cementeryServiceFields.value.comunidad) {
    //     MunicipalityState.value = false
    //     return 'Este campo es requerido'
    //   }

    //   if (
    //     !/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(
    //       cementeryServiceFields.value.comunidad
    //     )
    //   ) {
    //     MunicipalityState.value = false
    //     return 'Este campo solo puede contener letras'
    //   }

    //   if (!cementeryServiceFields.value.comunidad.trim().length > 0) {
    //     MunicipalityState.value = false
    //     return 'Este campo no puede contener espacios'
    //   }

    //   MunicipalityState.value = true
    //   return true
    // }

    const validateLocation = () => {
      if (!cementeryServiceFields.value.localidad) {
        LocationState.value = false
        return 'Este campo es requerido'
      }

      if (
        !/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(
          cementeryServiceFields.value.localidad
        )
      ) {
        LocationState.value = false
        return 'Este campo solo puede contener letras'
      }

      if (!cementeryServiceFields.value.localidad.trim().length > 0) {
        LocationState.value = false
        return 'Este campo no puede contener espacios'
      }

      LocationState.value = true
      return true
    }

    const validateStreet = () => {
      if (!cementeryServiceFields.value.calle) {
        StreetState.value = false
        return 'Este campo es requerido'
      }

      if (
        !/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(
          cementeryServiceFields.value.calle
        )
      ) {
        StreetState.value = false
        return 'Este campo solo puede contener letras'
      }

      if (!cementeryServiceFields.value.calle.trim().length > 0) {
        StreetState.value = false
        return 'Este campo no puede contener espacios'
      }

      StreetState.value = true
      return true
    }

    const validateNumberOutside = () => {
      if (!cementeryServiceFields.value.numeroExterior) {
        NumberOutsideState.value = false
        return 'Este campo es requerido'
      }

      if (
        !/^[0-9]+$/i.test(cementeryServiceFields.value.numeroExterior)
      ) {
        NumberOutsideState.value = false
        return 'Este campo solo puede contener numeros'
      }

      if (
        !cementeryServiceFields.value.numeroExterior.trim().length > 0
      ) {
        NumberOutsideState.value = false
        return 'Este campo no puede contener espacios'
      }

      NumberOutsideState.value = true
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
      cementeryServiceFields.value.latitud = markers.value.position.lat
      cementeryServiceFields.value.longitud = markers.value.position.lng
      createCementery(cementeryServiceFields.value, data => {
        refreshTable()
        swal.fire({
          title: '¡Direccion cementerios agregado correctamente!',
          text: 'Direccion cementerios registrado satisfactoriamente',
          icon: 'success'
        })
      })
      showModal.value = false
      resetCementeryServiceFields()
    }

    const RemoveCementeryService = CementeryId => {
      isloading.value = true
      swal
        .fire({
          title: '¿Estas seguro',
          text: 'No podras revertir esto',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Si, Archivar Direccion Cementerios!',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            deleteCementery(CementeryId, data => {
              refreshTable()
            })
            swal.fire({
              title: '¡Direccion cementerios archivado!',
              text: 'El direccion de cementerios ha sido archivado satisfactoriamente.',
              icon: 'success'
            })
          } else {
            isloading.value = false
          }
        })
    }

    return {
      showModal,
      cementeryService,
      cementeryServiceFields,
      breadcrumbItems,
      perPage,
      currentPage,
      filter,
      perPageSelect,
      isloading,
      searchValue,
      searchField,
      fields,
      NameCementeryState,
      // MunicipalityState,
      LocationState,
      StreetState,
      NumberOutsideState,
      markers,
      center,

      resetCementeryServiceFields,
      onFiltered,
      refreshTable,
      addCementeryService,
      RemoveCementeryService,
      validateNameCementery,
      // validateMunicipality,
      validateLocation,
      validateStreet,
      validateNumberOutside,
      updateCoordinates
    }
  }
}
</script>
