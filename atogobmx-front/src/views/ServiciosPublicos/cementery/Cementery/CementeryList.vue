<template>
  <b-card class="m-3">
    <b-row align-h="end" class="mb-3 mr-1">
      <b-form-input
        size="lg"
        style="width: 350px"
        v-model="searchvalue"
        type="search"
        placeholder="Buscar Cementerios..."
      ></b-form-input>
      <b-button
        variant="primary"
        style="
            height: 50px;
            width: auto;
            font-size: 18px;
            margin-right: 15px;
            margin-left: 20px;
        "
        v-b-modal.modal-cementery
        type="submit"
      >
        <i class="bi bi-person-plus-fill"></i>
        Cementerios
      </b-button>
    </b-row>
    <EasyDataTable
      rows-per-page-message="registro por pagina"
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
      :tbale-height="330"
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
      tittle="Agregar Cementerio"
      size="xl"
      hide-footer
      button-size="lg"
      lazy
    >
      <Form @submit="addCementeryService">
        <b-row cols="2">
          <b-col>
            <!-- 1 -->
            <b-form-group class="mt-3" label="Nombre del propietario">
              <Field name="PropietaryField" :rules="validatePropietary">
                <b-form-input
                  v-modal="cementeryServiceFields.nombrePropietario"
                  :state="PropietaryState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage name="PropietaryField">
                <span>Este campo es requerido</span>
                <i class="bi bi-exclamation-circle" />
              </ErrorMessage>
            </b-form-group>
          </b-col>
          <!-- 2 -->
          <b-col>
            <b-form-group class="mt-3" label="Numero de espacios">
              <Field name="SpacesField" :rules="validateSpaces">
                <b-form-input
                  v-modal="cementeryServiceFields.numeroEspacios"
                  :state="SpacesState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage name="SpacesField">
                <span>Este campo es requerido</span>
                <i class="bi bi-exclamation-circle" />
              </ErrorMessage>
            </b-form-group>
          </b-col>
          <!-- 3 -->
          <b-col>
            <b-form-group class="mt-3" label="Metros que le corresponden">
              <Field name="MeterField" :rules="validateMeter">
                <b-form-input
                  v-modal="cementeryServiceFields.metrosCorrespondientes"
                  :state="MeterState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage name="MeterField">
                <span>Este campo es requerido</span>
                <i class="bi bi-exclamation-circle" />
              </ErrorMessage>
            </b-form-group>
          </b-col>
          <!-- 4 -->
          <b-col>
            <b-form-group class="mt-3" label="Espacios Disponibles">
              <Field name="AvailableField" :rules="validateAvailable">
                <b-form-input
                  v-modal="cementeryServiceFields.espaciosDisponibles"
                  :state="AvailableState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage name="AvailableField">
                <span>Este campo es requerido</span>
                <i class="bi bi-exclamation-circle" />
              </ErrorMessage>
            </b-form-group>
          </b-col>
        </b-row>

        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            v-b-modal.modal-cementery
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
import CementeryService from '@/Services/cementery.Services'
import { Form, Field, ErrorMessage } from 'vee-validate'
import { ref } from 'vue'
import { useToast } from 'vue-toast-notification'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    EasyDataTable: window['vue3-easy-data-table'],
    Form,
    Field,
    ErrorMessage
  },
  setup () {
    const {
      getCementery,
      createCementery,
      deleteCementery
    } = CementeryService()
    const $toast = useToast()
    const cementeryService = ref([])
    const perPage = ref(6)
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
      nombrePropietario: null,
      numeroEspasios: null,
      metrosCorrespondientes: null,
      espaciosDisponibles: null,
      archivado: false
    })

    const CementeryServiceFieldsBlank = ref(
      JSON.parse(JSON.stringify(cementeryServiceFields))
    )

    const fields = ref([
      { value: 'cementeriosID', Text: 'ID', sortable: true },
      { value: 'nombrePropietario', Text: 'Nombre de propietario' },
      { value: 'numeroEspasios', Text: 'Espacios' },
      { value: 'metrosCorrespondientes', Text: 'Metros' },
      { value: 'espaciosDisponibles', Text: 'Espacios Disponibles' },
      { value: 'actions', Text: 'Acciones' }
    ])

    getCementery(data => {
      cementeryService.value = data

      if (cementeryService.value.length > 0) {
        isloading.value = false
      } else {
        if (CementeryService.VALUE.length <= 0) {
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
      PropietaryState.value = true
      return true
    }

    const validateSpaces = () => {
      if (!cementeryServiceFields.value.numeroEspasios) {
        SpacesState.value = false
        return 'Este campo es requerido'
      }
      SpacesState.value = true
      return true
    }

    const validateMeter = () => {
      if (!cementeryServiceFields.value.metrosCorrespondientes) {
        MeterState.value = false
        return 'Este campo es requerido'
      }
      MeterState.value = true
      return true
    }

    const validateAvailable = () => {
      if (!cementeryServiceFields.value.espaciosDisponibles) {
        AvailableState.value = false
        return 'Este campo es requerido'
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
        $toast.success('Cementerios registradoo correctamente.', {
          position: 'top-right',
          duration: 1500
        })
      })
      cementeryServiceFields.value = JSON.parse(
        JSON.stringify(CementeryServiceFieldsBlank))
    }

    const RemoveCementeryService = cementeryId => {
      isloading.value = true
      deleteCementery(cementeryId, data => {
        refreshTable()
      })
    }

    return {
      cementeryService,
      cementeryServiceFields,
      perPage,
      currentPage,
      filter,
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
      refreshTable
    }
  }
}

</script>

<style>
.customize-table {
  /* --easy-table-border: 1px solid #445269;
        --easy-table-row-border: 1px solid #445269; */
  --easy-table-header-font-size: 16px;
  --easy-table-header-height: 50px;
  --easy-table-header-font-color: #fcf6f5ff;
  --easy-table-header-background-color: #2bae66ff;
  --easy-table-header-item-padding: 10px 15px;
  --easy-table-header-item-align: center;
  --easy-table-message-font-size: 17px;
  /* --easy-table-body-even-row-font-color: #fff;
        --easy-table-body-even-row-background-color: #4c5d7a; */
  /* --easy-table-body-row-font-color: #c0c7d2;
        --easy-table-body-row-background-color: #2d3a4f; */
  --easy-table-body-row-height: 50px;
  --easy-table-body-row-font-size: 17px;
  --easy-table-border-radius: 15px;
  --easy-table-body-row-hover-font-color: rgb(0, 0, 0);
  --easy-table-body-row-hover-background-color: rgb(212, 212, 212);
  --easy-table-body-item-padding: 10px 15px;
  --easy-table-footer-background-color: #2bae66ff;
  --easy-table-footer-font-color: #fcf6f5ff;
  --easy-table-footer-font-size: 17px;
  --easy-table-footer-padding: 0px 10px;
  --easy-table-footer-height: 50px;
  /* --easy-table-scrollbar-track-color: #2d3a4f;
        --easy-table-scrollbar-color: #2d3a4f;
        --easy-table-scrollbar-thumb-color: #4c5d7a;;
        --easy-table-scrollbar-corner-color: #2d3a4f;
        --easy-table-loading-mask-background-color: #2d3a4f; */
}
</style>
