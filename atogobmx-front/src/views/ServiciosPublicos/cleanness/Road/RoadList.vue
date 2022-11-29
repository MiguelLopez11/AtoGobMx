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
        <b-row cols="2">
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
          <GMapCluster
            :zoom="10"
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
          </GMapCluster>
          </GMapMap>
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
import { Form } from 'vee-validate'
import { ref, inject } from 'vue'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    EasyDataTable: window['vue3-easy-data-table'],
    Form
  },
  setup () {
    const swal = inject('$swal')
    const showModal = ref(false)
    const { getRoad, createRoad, deleteRoad } = RoadService()
    // const $toast = useToast()
    const roadService = ref([])
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
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Aseo publico', to: '/ServiciosPublicos/AseoPublico/list' },
      { text: 'Ruta' }
    ])

    const RoadServiceFields = ref({
      rutaId: 0,
      origen: null,
      destino: null,
      obsevacion: null,
      archivado: false
    })
    const markers = ref([
      {
        position: {
          lat: 20.5505946,
          lng: -102.4953904
        }
      },
      {
        position: {
          lat: 20.5505946,
          lng: -102.4953904
        }
      }
    ])
    const startLocation = ref({ lat: 20.5524136, lng: -102.5137753 })
    const endLocation = ref({ lat: 20.5486875, lng: -102.5073417 })
    const RoadServiceFieldsBlank = ref(
      JSON.parse(JSON.stringify(RoadServiceFields))
    )
    const center = ref({ lat: 20.5505946, lng: -102.5094758 })
    const fields = ref([
      { value: 'origen', text: 'Latitud' },
      { value: 'destino', text: 'Longitud' },
      { value: 'obsevacion', text: 'Observacion' },
      { value: 'actions', text: 'Acciones' }
    ])

    const resetRoadServiceFields = () => {
      showModal.value = false
      RoadServiceFields.value = JSON.parse(
        JSON.stringify(RoadServiceFieldsBlank)
      )
      OriginState.value = false
      DestinationState.value = false
      ObservationState.value = false
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
    const addMaker = id => {
      console.log(id)
    }
    const onFiltered = filteredItems => {
      currentPage.value = 1
    }

    const validateOrigin = () => {
      if (!RoadServiceFields.value.origen) {
        OriginState.value = false
        return 'Este campo es requerido'
      }

      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(RoadServiceFields.value.origen)) {
        OriginState.value = false
        return 'Este campo solo puede contener letras'
      }

      if (!RoadServiceFields.value.origen.trim().length > 0) {
        OriginState.value = false
        return 'Este campo no puede contener espacios'
      }

      OriginState.value = true
      return true
    }

    const validateDestination = () => {
      if (!RoadServiceFields.value.destino) {
        DestinationState.value = false
        return 'Este campo es requerido'
      }

      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(RoadServiceFields.value.destino)) {
        DestinationState.value = false
        return 'Este campo solo puede contener numeros'
      }

      if (!RoadServiceFields.value.destino.trim().length > 0) {
        DestinationState.value = false
        return 'Este campo no puede contener espacios'
      }

      DestinationState.value = true
      return true
    }

    const validateObservation = () => {
      if (!RoadServiceFields.value.obsevacion) {
        ObservationState.value = false
        return 'Este campo es requerido'
      }

      if (
        !/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(RoadServiceFields.value.obsevacion)
      ) {
        ObservationState.value = false
        return 'Este campo solo puede contener numeros'
      }

      if (!RoadServiceFields.value.obsevacion.trim().length > 0) {
        ObservationState.value = false
        return 'Este campo no puede contener espacios'
      }

      ObservationState.value = true
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
      createRoad(RoadServiceFields.value, data => {
        refreshTable()
        swal.fire({
          title: '¡Ruta agregada correctamente!',
          text: 'Ruta registrado satisfactoriamente',
          icon: 'success'
        })
      })
      showModal.value = false
      resetRoadServiceFields()
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
      center,
      markers,
      startLocation,
      endLocation,

      addMaker,
      onFiltered,
      addRoadService,
      RemoveRoadService,
      refreshTable,
      validateOrigin,
      validateDestination,
      validateObservation,
      resetRoadServiceFields
    }
  }
}
</script>

<style></style>
