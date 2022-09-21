<template>
  <b-card class="m-3">
    <b-row align-h="end" class="mb-3 mr-1">
          <b-form-input
            size="lg"
            style="width: 350px"
            v-model="searchValue"
            type="search"
            placeholder="Buscar Falla..."
          ></b-form-input>
          <b-button
            variant="primary"
            style="height: 50px; width: auto; font-size: 18px; margin-right: 15px; margin-left: 20px"
            v-b-modal.modal-lightingfailures
            type="submit"
          >
            <i class="bi bi-person-plus-fill"></i>
            Agregar Falla
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
      :items="lightingFailures"
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
          @click="RemoveLightingFailures(items.fallaId)"
          class="m-1"
          variant="outline-danger"
          ><i class="bi bi-trash3"></i
        ></b-button>
        <b-button
          class="m-1"
          variant="outline-warning"
          :to="{
            name: 'FallasAlumbrado-Edit',
            params: { FallaId: items.fallaId },
          }"
          ><i class="bi bi-pencil-square"></i
        ></b-button>
      </template>
    </EasyDataTable>
    <b-modal
      id="modal-lightingfailures"
      title="Agregar Falla"
      size="xl"
      hide-footer
      centered
      button-size="lg"
      lazy
    >
      <Form @submit="addLightingFailures">
        <b-row cols="3">
          <b-col>
            <b-form-group class="mt-3" label="NombreFalla">
              <Field  name="faultNameField" :rules="validateFaultName">
                <b-form-input
                  v-model="lightingFailuresFields.nombreFalla"
                  :state="FaultNameState"
                ></b-form-input>
              </Field>
              <ErrorMessage name="faultNameField"
                ><span>Este campo es requerido </span
                ><i class="bi bi-exclamation-circle"></i>
              </ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Fecha">
              <Field name="DateField" :rules="validateDate">
                <Datepicker
                  locale="es"
                  name="date"
                  :enableTimePicker="false"
                  autoApply
                  v-model="lightingFailuresFields.fecha"
                  :state="dateState"
                ></Datepicker>
              </Field>
              <ErrorMessage name="DateField"
                ><span>Este campo es requerido llenarlo </span
                ><i class="bi bi-exclamation-circle"></i>
              </ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Descripcion">
              <Field name="descriptionField" :rules="validateDescription">
                <b-form-textarea
                  v-model="lightingFailuresFields.descripcion"
                  :state="descriptionState"
                ></b-form-textarea>
              </Field>
              <ErrorMessage name="descriptionField"
                ><span>Este campo es requerido </span
                ><i class="bi bi-exclamation-circle"></i>
              </ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Descripcion Domicilio">
              <Field
                name="addresdescriptionField"
                :rules="validateAddresdescription"
              >
                <b-form-textarea
                  v-model="lightingFailuresFields.descripcionDomicilio"
                  :state="addresdescriptionState"
                ></b-form-textarea>
              </Field>
              <ErrorMessage name="addresdescriptionField"
                ><span>Este campo es requerido llenarlo </span
                ><i class="bi bi-exclamation-circle"></i>
              </ErrorMessage>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            v-b-modal.modal-lightingfailures
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
import LightingfailuresServices from '@/Services/lightingfailures.Services'
import Datepicker from '@vuepic/vue-datepicker'
import { Form, Field, ErrorMessage } from 'vee-validate'
import { ref } from 'vue'
import { useToast } from 'vue-toast-notification'
export default {
  components: {
    Datepicker,
    EasyDataTable: window['vue3-easy-data-table'],
    Form,
    Field,
    ErrorMessage
  },
  setup () {
    const {
      getLightingFailures,
      createLightingFailures,
      deleteLightingFailures
    } = LightingfailuresServices()
    const $toast = useToast()
    const lightingFailures = ref([])
    const isOpen = ref(false)
    const perPage = ref(5)
    const currentPage = ref(1)
    // const rows = ref(null)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('nombreFalla')
    const FaultNameState = ref(false)
    const descriptionState = ref(false)
    const dateState = ref(false)
    const addresdescriptionState = ref(false)
    const lightingFailuresFields = ref({
      fallaId: 0,
      nombreFalla: null,
      descripcion: null,
      fecha: null,
      descripcionDomicilio: null,
      archivado: false
    })

    const validateFaultName = () => {
      if (!lightingFailuresFields.value.nombreFalla) {
        FaultNameState.value = false
        return 'Este campo es requerido'
      }
      FaultNameState.value = true
      return true
    }

    const validateDescription = () => {
      if (!lightingFailuresFields.value.descripcion) {
        descriptionState.value = false
        return 'Este campo es requerido'
      }
      descriptionState.value = true
      return true
    }

    const validateDate = () => {
      if (!lightingFailuresFields.value.fecha) {
        dateState.value = false
        return 'Este campo es requerido'
      }
      dateState.value = true
      return true
    }

    const validateAddresdescription = () => {
      if (!lightingFailuresFields.value.descripcionDomicilio) {
        addresdescriptionState.value = false
        return 'Este campo es requerido'
      }
      addresdescriptionState.value = true
      return true
    }

    // pone mis cambios de mis campos vacios de nuevo
    const lightingFailuresFieldsBlank = ref(
      JSON.parse(JSON.stringify(lightingFailuresFields))
    )

    const fields = ref([
      { value: 'fallaId', text: 'ID', sortable: true },
      { value: 'nombreFalla', text: 'NombreFalla' },
      { value: 'descripcion', text: 'Descripción' },
      { value: 'fecha', text: 'Fecha' },
      { value: 'descripcionDomicilio', text: 'Descripcion Domicilio' },
      { value: 'actions', text: 'Acciones' }
    ])
    getLightingFailures((data) => {
      lightingFailures.value = data
      // rows.value = data.length
      if (lightingFailures.value.length > 0) {
        isloading.value = false
      } else {
        if (lightingFailures.value.length <= 0) {
          isloading.value = false
        }
      }
    })

    const onFiltered = (filteredItems) => {
      // rows.value = filteredItems.length
      currentPage.value = 1
    }

    const refreshTable = () => {
      isloading.value = true
      getLightingFailures((data) => {
        lightingFailures.value = data
        // rows.value = data.length
        if (lightingFailures.value.length > 0) {
          isloading.value = false
        } else {
          if (lightingFailures.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }

    const addLightingFailures = () => {
      createLightingFailures(lightingFailuresFields.value, (data) => {
        refreshTable()
        $toast.success('Falla registrada correctamente.', {
          position: 'top-right',
          duration: 1500
        })
      })
      lightingFailuresFields.value = JSON.parse(
        JSON.stringify(lightingFailuresFieldsBlank)
      )
    }

    const RemoveLightingFailures = (LightingFailuresId) => {
      isloading.value = true
      deleteLightingFailures(LightingFailuresId, (data) => {
        refreshTable()
      })
    }
    return {
      lightingFailures,
      lightingFailuresFields,
      isOpen,
      perPage,
      currentPage,
      // rows,
      filter,
      perPageSelect,
      isloading,
      searchValue,
      searchField,
      lightingFailuresFieldsBlank,
      // lightingFailuresValues,
      fields,
      FaultNameState,
      descriptionState,
      dateState,
      addresdescriptionState,

      onFiltered,
      addLightingFailures,
      refreshTable,
      RemoveLightingFailures,
      validateFaultName,
      validateDescription,
      validateDate,
      validateAddresdescription
    }
  }
}
</script>

<style>
.customize-table {
  /* --easy-table-border: 1px solid #445269;
  --easy-table-row-border: 1px solid #445269; */

  --easy-table-header-font-size: 18px;
  --easy-table-header-height: 50px;
  --easy-table-header-font-color: #ffffff;
  --easy-table-header-background-color: #42b883;

  --easy-table-header-item-padding: 10px 15px;
  --easy-table-header-item-align: center;
  /* --easy-table-body-even-row-font-color: #fff;
  --easy-table-body-even-row-background-color: #4c5d7a; */

  /* --easy-table-body-row-font-color: #c0c7d2;
  --easy-table-body-row-background-color: #2d3a4f; */
  --easy-table-body-row-height: 50px;
  --easy-table-body-row-font-size: 15px;
  --easy-table-border-radius: 15px;
  /*
  --easy-table-body-row-hover-font-color: #2d3a4f;
  --easy-table-body-row-hover-background-color: #eee; */

  --easy-table-body-item-padding: 10px 15px;

  /* --easy-table-footer-background-color: #138BC2;
  --easy-table-footer-font-color: #D1D1D1; */
  --easy-table-footer-font-size: 15px;
  --easy-table-footer-padding: 0px 10px;
  --easy-table-footer-height: 50px;

  /* --easy-table-scrollbar-track-color: #2d3a4f;
  --easy-table-scrollbar-color: #2d3a4f;
  --easy-table-scrollbar-thumb-color: #4c5d7a;;
  --easy-table-scrollbar-corner-color: #2d3a4f;

  --easy-table-loading-mask-background-color: #2d3a4f; */
}
</style>
