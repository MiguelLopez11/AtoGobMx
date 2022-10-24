<template>
  <b-card class="m-2">
    <b-row align-h="end" class="mb-3 mr-1">
      <b-form-input
        size="lg"
        style="width: 350px"
        v-model="searchValue"
        type="search"
        placeholder="Buscar Zona..."
      ></b-form-input>
      <b-button
        variant="primary"
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
        <!-- v-b-modal.modal-cementery -->
        <i class="bi bi-person-plus-fill"></i>
        Agregar Zona
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
      :items="zoneService"
      :rows-per-page="5"
      :search-field="searchField"
      :search-value="searchValue"
      :table-height="330"
    >
      <template #header-actions="header">
        {{ header.text }}
      </template>
      <template #item-actions="items">
        <b-button
          @click="RemoveZoneService(items.zonaId)"
          class="m-1"
          variant="outline-danger"
          ><i class="bi bi-trash3"></i
        ></b-button>
        <b-button
          class="m-1"
          variant="outline-warning"
          :to="{
            name: 'Zona-Edit',
            params: { ZonaId: items.zonaId }
          }"
          ><i class="bi bi-pencil-square"></i
        ></b-button>
      </template>
    </EasyDataTable>

    <b-modal
      id="modal-clenness"
      tittle="Agregar Zona"
      v-model="showModal"
      size="xl"
      hide-footer
      button-size="lg"
      lazy
    >
      <Form @submit="addZoneService">
        <b-row cols="2">
          <b-col>
            <!-- 1 -->
            <b-form-group class="mt-3" label="Nombre">
              <Field
                name="NameField"
                :rules="validateName"
                as="text"
              >
                <b-form-input
                  v-model="RouteServiceFields.origen"
                  :state="OriginState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="OriginField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!-- 2 -->
          <b-col>
            <b-form-group class="mt-3" label="Destino">
              <Field name="DestinationField" :rules="validateDestination" as="text">
                <b-form-input
                  v-model="RouteServiceFields.destino"
                  :state="DestinationState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="DestinationField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!-- 3 -->
          <b-col>
            <b-form-group class="mt-3" label="Observacion">
              <Field name="ObservationField" :rules="validateObservation" as="text">
                <b-form-input
                  v-model="RouteServiceFields.obsevacion"
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
        </b-row>

        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            @click="resetRouteServiceFields"
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
import RouteService from '@/Services/route.Services'
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
    const { getRoute, createRoute, deleteRoute } = RouteService()
    // const $toast = useToast()
    const routeService = ref([])
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
    const RouteServiceFields = ref({
      rutaId: 0,
      origen: null,
      destino: null,
      obsevacion: null,
      archivado: false
    })

    const RouteServiceFieldsBlank = ref(
      JSON.parse(JSON.stringify(RouteServiceFields))
    )

    const fields = ref([
      { value: 'rutaId', text: 'ID', sortable: true },
      { value: 'origen', text: 'Origen' },
      { value: 'destino', text: 'Destino' },
      { value: 'obsevacion', text: 'Observacion' },
      { value: 'actions', text: 'Acciones' }
    ])

    const resetRouteServiceFields = () => {
      showModal.value = false
      RouteServiceFields.value = JSON.parse(
        JSON.stringify(RouteServiceFieldsBlank)
      )
      OriginState.value = false
      DestinationState.value = false
      ObservationState.value = false
    }

    getRoute(data => {
      routeService.value = data

      if (routeService.value.length > 0) {
        isloading.value = false
      } else {
        if (routeService.value.length <= 0) {
          isloading.value = false
        }
      }
    })

    const onFiltered = filteredItems => {
      currentPage.value = 1
    }

    const validateOrigin = () => {
      if (!RouteServiceFields.value.origen) {
        OriginState.value = false
        return 'Este campo es requerido'
      }

      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(RouteServiceFields.value.origen)) {
        OriginState.value = false
        return 'Este campo solo puede contener letras'
      }

      if (!RouteServiceFields.value.origen.trim().length > 0) {
        OriginState.value = false
        return 'Este campo no puede contener espacios'
      }

      OriginState.value = true
      return true
    }

    const validateDestination = () => {
      if (!RouteServiceFields.value.destino) {
        DestinationState.value = false
        return 'Este campo es requerido'
      }

      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(RouteServiceFields.value.destino)) {
        DestinationState.value = false
        return 'Este campo solo puede contener numeros'
      }

      if (!RouteServiceFields.value.destino.trim().length > 0) {
        DestinationState.value = false
        return 'Este campo no puede contener espacios'
      }

      DestinationState.value = true
      return true
    }

    const validateObservation = () => {
      if (!RouteServiceFields.value.obsevacion) {
        ObservationState.value = false
        return 'Este campo es requerido'
      }

      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(RouteServiceFields.value.obsevacion)) {
        ObservationState.value = false
        return 'Este campo solo puede contener numeros'
      }

      if (!RouteServiceFields.value.obsevacion.trim().length > 0) {
        ObservationState.value = false
        return 'Este campo no puede contener espacios'
      }

      ObservationState.value = true
      return true
    }

    const refreshTable = () => {
      isloading.value = true
      getRoute(data => {
        routeService.value = data

        if (routeService.value.length > 0) {
          isloading.value = false
        } else {
          if (routeService.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }

    const addRouteService = () => {
      createRoute(RouteServiceFields.value, data => {
        refreshTable()
        swal.fire({
          title: '¡Ruta agregada correctamente!',
          text: 'Ruta registrado satisfactoriamente',
          icon: 'success'
        })
      })
      showModal.value = false
      resetRouteServiceFields()
    }

    const RemoveRouteService = routeId => {
      isloading.value = true
      swal.fire({
        title: '¿Estas seguro',
        text: 'No podras revertir esto',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, Archivar Ruta!',
        cancelButtonText: 'Cancelar'
      }).then(result => {
        if (result.isConfirmed) {
          deleteRoute(routeId, (data) => {
            refreshTable()
          })
          swal.fire({
            title: '¡Ruta archivada!',
            text:
                'La ruta ha sido archivado satisfactoriamente.',
            icon: 'success'
          })
        } else {
          isloading.value = false
        }
      })
    }

    return {
      routeService,
      RouteServiceFields,
      perPage,
      currentPage,
      filter,
      showModal,
      perPageSelect,
      isloading,
      searchValue,
      searchField,
      RouteServiceFieldsBlank,
      fields,
      OriginState,
      DestinationState,
      ObservationState,

      onFiltered,
      addRouteService,
      RemoveRouteService,
      refreshTable,
      validateOrigin,
      validateDestination,
      validateObservation,
      resetRouteServiceFields
    }
  }
}
</script>

<style></style>
