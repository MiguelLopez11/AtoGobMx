<template>
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
      :table-height="330"
    >
      <template #header-actions="header">
        {{ header.text }}
      </template>
      <template #item-actions="items">
        <b-button
          @click="RemoveCementeryService(items.cementeriosId)"
          class="m-1"
          variant="outline-danger"
          ><i class="bi bi-trash3"></i
        ></b-button>
        <b-button
          class="m-1"
          variant="outline-warning"
          :to="{
            name: 'Cementerios-Edit',
            params: { CementeriosId: items.cementeriosId }
          }"
          ><i class="bi bi-pencil-square"></i
        ></b-button>
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
          <!-- 1 -->
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
          <!-- 2 -->
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
          <!-- 3 -->
          <b-col>
            <b-form-group class="mt-3" label="Metros correspondientes">
              <Field name="MeterField" :rules="validateMeter" as="number">
                <b-form-input
                  v-model="cementeryServiceFields.metrosCorrespondientes"
                  :state="MeterState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="MeterField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!-- 4 -->
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
    const {
      getCementery,
      createCementery,
      deleteCementery
    } = CementeryService()
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

    const fields = ref([
      { value: 'cementeriosId', text: 'ID', sortable: true },
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

      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(cementeryServiceFields.value.nombrePropietario)) {
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

      if (!/^\d*\.\d+$/i.test(cementeryServiceFields.value.metrosCorrespondientes)) {
        MeterState.value = false
        return 'Este campo solo puede contener numeros'
      }

      if (!cementeryServiceFields.value.metrosCorrespondientes.trim().length > 0) {
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
      swal.fire({
        title: '¿Estas seguro',
        text: 'No podras revertir esto',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, Archivar Cementerios!',
        cancelButtonText: 'Cancelar'
      }).then(result => {
        if (result.isConfirmed) {
          deleteCementery(cementeryId, (data) => {
            refreshTable()
          })
          swal.fire({
            title: '¡Cementerio archivado!',
            text:
                'El cementerio ha sido archivado satisfactoriamente.',
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
