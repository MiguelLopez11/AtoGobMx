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
        placeholder="Buscar Ruta..."
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
        Agregar Ruta
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
      :items="roadService"
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
            @click="RemoveRoadService(items.rutaId)"
            class="m-1"
            variant="outline-danger"
            ><i class="bi bi-trash3"> Archivar</i></b-dropdown-item
          >
          <b-dropdown-item
            class="m-1"
            variant="outline-warning"
            :to="{
              name: 'Ruta-Edit',
              params: { RutaId: items.rutaId }
            }"
            ><i class="bi bi-pencil-square" /> Editar</b-dropdown-item
          >
        </b-dropdown>
      </template>
    </EasyDataTable>
    <b-modal
      id="modal-road"
      tittle="Agregar Ruta"
      v-model="showModal"
      size="xl"
      hide-footer
      button-size="lg"
      lazy
    >
      <Form @submit="addRoadService">
        <b-row cols="3">
          <!-- Agregar Observacion -->
          <b-col>
            <b-form-group class="mt-3" label="Observacion">
              <Field
                name="ObservationField"
                :rules="validateObservation"
                as="text"
              >
                <b-form-input
                  v-model="RoadServiceFields.observacion"
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
          <!-- Agregar Nombre -->
          <b-col>
            <b-form-group class="mt-3" label="Nombre de la ruta">
              <Field
                name="NameRoadField"
                :rules="validateNameRoad"
                as="text"
              >
                <b-form-input
                  v-model="RoadServiceFields.nombre"
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
          <!-- Agregar horario -->
          <b-col>
            <b-form-group class="mt-3" label="Horarios aseo">
              <Field
                name="HoraryField"
                :rules="validateHorary"
                as="text"
              >
                <Datepicker
                      v-model="date"
                      placeholder="Seleccionar ..."
                      selectText="Seleccionar"
                      cancelText="Cancelar"
                      locale="es"
                      time-picker
                      disable-time-range-validation
                      range
                      modelType="hh:mm aa"
                      :is-24="false"
                      :state="HoraryState"
                    >
                    </Datepicker>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="HoraryField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
              <b-button class="mt-5 w-100" variant="primary" @click="onAddPolyline">
                Marcar Ruta
              </b-button>
          </b-col>
          <b-col>
              <b-button class="mt-5 w-100" variant="primary" @click="ResetPolyline">
                Reiniciar ruta
              </b-button>
          </b-col>
        </b-row>
        <b-row>
          <GoogleMap
            api-key="AIzaSyCYAwe7Fk4PQLI3bBBqxUViN4IOXVGd_z0"
            style="width: 100%; height: 500px"
            :center="center"
            :zoom="20"
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
          <b-row> </b-row>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            @click="resetRoadServiceFields"
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
import RoadService from '@/Services/road.Services'
import { Form, ErrorMessage, Field } from 'vee-validate'
import { ref, inject } from 'vue'
import { GoogleMap, Marker, MarkerCluster, Polyline } from 'vue3-google-map'
import Datepicker from '@vuepic/vue-datepicker'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    EasyDataTable: window['vue3-easy-data-table'],
    Form,
    ErrorMessage,
    Field,
    GoogleMap,
    Marker,
    MarkerCluster,
    Polyline,
    Datepicker
  },
  setup () {
    const swal = inject('$swal')
    const showModal = ref(false)
    const { getRoad, createRoad, deleteRoad, createCoordsRoad } = RoadService()
    const roadService = ref([])
    const date = ref()
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('origen')
    const OriginState = ref(false)
    const DestinationState = ref(false)
    const ObservationState = ref(false)
    const NameRoadState = ref(false)
    const HoraryState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Aseo publico', to: '/ServiciosPublicos/AseoPublico/list' },
      { text: 'Ruta' }
    ])

    const RoadServiceFields = ref({
      rutaId: 0,
      origen: null,
      destino: null,
      observacion: null,
      nombre: null,
      horario: null,
      archivado: false
    })
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
    const RoadServiceFieldsBlank = ref(
      JSON.parse(JSON.stringify(RoadServiceFields))
    )
    const center = ref({ lat: 20.5546629, lng: -102.4953904 })
    const fields = ref([
      { value: 'observacion', text: 'Observacion' },
      { value: 'nombre', text: 'Nombre de la ruta' },
      { value: 'horario', text: 'Horarios' },
      { value: 'longitud', text: 'longitud' },
      { value: 'latitud', text: 'latitud' },
      { value: 'actions', text: 'Acciones' }
    ])
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
    const resetRoadServiceFields = () => {
      showModal.value = false
      RoadServiceFields.value = JSON.parse(
        JSON.stringify(RoadServiceFieldsBlank)
      )
      OriginState.value = false
      DestinationState.value = false
      ObservationState.value = false
      NameRoadState.value = false
      HoraryState.value = false
    }

    getRoad(data => {
      roadService.value = data

      if (roadService.value.length > 0) {
        isloading.value = false
      } else {
        if (roadService.value.length <= 0) {
          isloading.value = false
        }
      }
    })
    const onFiltered = filteredItems => {
      currentPage.value = 1
    }

    const validateObservation = () => {
      if (!RoadServiceFields.value.observacion) {
        ObservationState.value = false
        return 'Este campo es requerido'
      }
      if (!RoadServiceFields.value.observacion.trim().length > 0) {
        ObservationState.value = false
        return 'Este campo no puede contener espacios'
      }

      ObservationState.value = true
      return true
    }

    const validateNameRoad = () => {
      if (!RoadServiceFields.value.nombre) {
        NameRoadState.value = false
        return 'Este campo es requerido'
      }
      if (
        !/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ 0-9]+$/i.test(
          RoadServiceFields.value.nombre
        )
      ) {
        NameRoadState.value = false
        return 'Este campo solo puede contener letras'
      }
      if (!RoadServiceFields.value.nombre.trim().length > 0) {
        NameRoadState.value = false
        return 'Este campo no puede contener espacios'
      }

      NameRoadState.value = true
      return true
    }

    const validateHorary = () => {
      if (!date.value) {
        HoraryState.value = false
        return 'Este campo es requerido'
      }

      HoraryState.value = true
      return true
    }

    const refreshTable = () => {
      isloading.value = true
      getRoad(data => {
        roadService.value = data

        if (roadService.value.length > 0) {
          isloading.value = false
        } else {
          if (roadService.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }

    const addRoadService = () => {
      RoadServiceFields.value.horario = `de ${date.value[0].hours} : ${date.value[0].minutes} a ${date.value[1].hours} : ${date.value[1].minutes}  `
      createRoad(RoadServiceFields.value, data => {
        console.log(data)
        for (let i = 0; i < locations.value.length; i++) {
          createCoordsRoad({
            coordenadaId: 0,
            latitud: locations.value[i].lat,
            longitud: locations.value[i].lng,
            rutaId: data.rutaId,
            ordenCoordenada: i
          }, data => {
          })
          refreshTable()
          showModal.value = false
          resetRoadServiceFields()
          swal.fire({
            title: '¡Ruta agregada correctamente!',
            text: 'Ruta registrado satisfactoriamente',
            icon: 'success'
          })
        }
      })
    }

    const RemoveRoadService = routeId => {
      isloading.value = true
      swal
        .fire({
          title: '¿Estas seguro',
          text: 'No podras revertir esto',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Si, Archivar Ruta!',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            deleteRoad(routeId, data => {
              refreshTable()
            })
            swal.fire({
              title: '¡Ruta archivada!',
              text: 'La ruta ha sido archivado satisfactoriamente.',
              icon: 'success'
            })
          } else {
            isloading.value = false
          }
        })
    }

    return {
      roadService,
      RoadServiceFields,
      CoordsRoadFields,
      breadcrumbItems,
      perPage,
      currentPage,
      filter,
      showModal,
      perPageSelect,
      isloading,
      searchValue,
      searchField,
      RoadServiceFieldsBlank,
      fields,
      OriginState,
      DestinationState,
      ObservationState,
      NameRoadState,
      HoraryState,
      center,
      locations,
      flightPath,
      date,

      onFiltered,
      addRoadService,
      RemoveRoadService,
      refreshTable,
      // validateOrigin,
      // validateDestination,
      validateObservation,
      resetRoadServiceFields,
      validateNameRoad,
      validateHorary,
      addMaker,
      onAddPolyline,
      ResetPolyline
    }
  }
}
</script>

<style scoped>
.map {
  position: relative;
  width: 100%;
  height: 500px;
}

.map::after {
  position: absolute;
  content: '';
  width: 1px;
  height: 100%;
  top: 0;
  left: 50%;
  background: red;
}
</style>
