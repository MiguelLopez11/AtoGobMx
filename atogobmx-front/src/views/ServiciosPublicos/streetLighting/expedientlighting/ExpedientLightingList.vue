<template>
  <b-card class="m-3">
    <b-row align-h="end" class="mb-3 mr-1">
      <b-form-input
        size="lg"
        style="width: 350px"
        v-model="searchValue"
        type="search"
        placeholder="Buscar Expediente Alumbrado..."
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
        v-b-modal.modal-expedientlighting
        type="submit"
      >
        <i class="bi bi-person-plus-fill"></i>
        Agregar Expediente
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
      :items="expedientLighting"
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
          @click="RemoveExpedientLighting(items.expedienteAlumbradoId)"
          class="m-1"
          variant="outline-danger"
          ><i class="bi bi-trash3"></i
        ></b-button>
        <b-button
          class="m-1"
          variant="outline-warning"
          :to="{
            name: 'ExpedienteAlumbrado-Edit',
            params: { ExpedienteAlumbradoId: items.expedienteAlumbradoId }
          }"
          ><i class="bi bi-pencil-square"></i
        ></b-button>
      </template>
    </EasyDataTable>
    <b-modal
      id="modal-expedientlighting"
      title="Agregar Expediente Alumbrado"
      size="xl"
      hide-footer
      button-size="lg"
      lazy
    >
      <Form @submit="addExpedientLighting">
        <b-row cols="2">
          <b-col>
            <b-form-group class="mt-3" label="Nombre del tipo Tarea">
              <Field name="TypeTaskField" :rules="validateTypeTask">
                <b-form-select
                  v-model="expedientLightingFields.tarea"
                  autofocus
                  :state="TypeTaskState"
                  :options="statusPublicLighting"
                  value-field="estatusId"
                  text-field="nombreEstatus"
                ></b-form-select>
              </Field>
              <ErrorMessage name="TypeTaskField"
                ><span>Este campo es requerido </span
                ><i class="bi bi-exclamation-circle"></i>
              </ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Fecha Alta">
              <Field name="HighDateField" :rules="validateHighDate">
                <Datepicker
                  locale="es"
                  name="date"
                  text-input
                  v-model="expedientLightingFields.fechaAlta"
                  :state="HighDateState"
                ></Datepicker>
              </Field>
              <ErrorMessage name="HighDateField"
                ><span>Este campo es requerido </span
                ><i class="bi bi-exclamation-circle"></i>
              </ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Domicilio">
              <Field name="DomicileField" :rules="validateDomicile">
                <b-form-input
                  v-model="expedientLightingFields.domicilio"
                  :state="DomicileState"
                ></b-form-input>
              </Field>
              <ErrorMessage name="DomicileField"
                ><span>Este campo es requerido </span
                ><i class="bi bi-exclamation-circle"></i>
              </ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Fecha Alta">
              <Field name="LowDateField" :rules="validateLowDate">
                <Datepicker
                  locale="es"
                  name="date"
                  text-input
                  v-model="expedientLightingFields.fechaBaja"
                  :state="LowDateState"
                ></Datepicker>
              </Field>
              <ErrorMessage name="LowDateField"
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
                  v-model="expedientLightingFields.descripcionDomicilio"
                  :state="addresdescriptionState"
                  rows="4"
                ></b-form-textarea>
              </Field>
              <ErrorMessage name="addresdescriptionField"
                ><span>Este campo es requerido </span
                ><i class="bi bi-exclamation-circle"></i>
              </ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Descripcion Solucion">
              <Field
                name="DescriptionSolutionField"
                :rules="validateDescriptionSolution"
              >
                <b-form-textarea
                  v-model="expedientLightingFields.descripcionSolucion"
                  :state="DescriptionSolutionState"
                  rows="4"
                ></b-form-textarea>
              </Field>
              <ErrorMessage name="DescriptionSolutionField"
                ><span>Este campo es requerido </span
                ><i class="bi bi-exclamation-circle"></i>
              </ErrorMessage>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            v-b-modal.modal-expedientlighting
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
import ExpedientlightingServices from '@/Services/expedientlighting.Services'
import StatusLightingServices from '@/Services/statuslighting.Services'
import Datepicker from '@vuepic/vue-datepicker'
import { Form, Field, ErrorMessage } from 'vee-validate'
import { ref } from 'vue'
import { useToast } from 'vue-toast-notification'
import '@vuepic/vue-datepicker/dist/main.css'
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
      getExpedientLighting,
      createExpedientLighting,
      deleteExpedientLighting
    } = ExpedientlightingServices()
    const { getStatus } = StatusLightingServices()
    const $toast = useToast()
    const expedientLighting = ref([])
    const statusPublicLighting = ref([])
    const isOpen = ref(false)
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('tarea')
    const TypeTaskState = ref(false)
    const DescriptionSolutionState = ref(false)
    const HighDateState = ref(false)
    const addresdescriptionState = ref(false)
    const DomicileState = ref(false)
    const LowDateState = ref(false)
    const expedientLightingFields = ref({
      expedienteAlumbradoId: 0,
      tarea: null,
      descripcionSolucion: null,
      fechaAlta: null,
      fechaBaja: null,
      domicilio: null,
      descripcionDomicilio: null,
      archivado: false
    })

    getStatus(data => {
      statusPublicLighting.value = data
    })

    const validateTypeTask = () => {
      if (!expedientLightingFields.value.tarea) {
        TypeTaskState.value = false
        return 'Este campo es requerido'
      }
      TypeTaskState.value = true
      return true
    }

    const validateDescriptionSolution = () => {
      if (!expedientLightingFields.value.descripcionSolucion) {
        DescriptionSolutionState.value = false
        return 'Este campo es requerido'
      }
      DescriptionSolutionState.value = true
      return true
    }

    const validateDomicile = () => {
      if (!expedientLightingFields.value.domicilio) {
        DomicileState.value = false
        return 'Este campo es requerido'
      }
      DomicileState.value = true
      return true
    }

    const validateLowDate = () => {
      if (!expedientLightingFields.value.fechaBaja) {
        LowDateState.value = false
        return 'Este campo es requerido'
      }
      LowDateState.value = true
      return true
    }

    const validateHighDate = () => {
      if (!expedientLightingFields.value.fechaAlta) {
        HighDateState.value = false
        return 'Este campo es requerido'
      }
      HighDateState.value = true
      return true
    }

    const validateAddresdescription = () => {
      if (!expedientLightingFields.value.descripcionDomicilio) {
        addresdescriptionState.value = false
        return 'Este campo es requerido'
      }
      addresdescriptionState.value = true
      return true
    }

    // pone mis cambios de mis campos vacios de nuevo
    const expedientLightingFieldsBlank = ref(
      JSON.parse(JSON.stringify(expedientLightingFields))
    )

    const fields = ref([
      { value: 'expedienteAlumbradoId', text: 'ID', sortable: true },
      { value: 'tarea', text: 'Tarea' },
      { value: 'fechaAlta', text: 'Fecha Alta' },
      { value: 'domicilio', text: 'Domicilio' },
      { value: 'descripcionDomicilio', text: 'Descripcion Domicilio' },
      { value: 'descripcionSolucion', text: 'Descripcion Solucion' },
      { value: 'fechaBaja', text: 'Fecha Baja' },
      { value: 'actions', text: 'Acciones' }
    ])

    getExpedientLighting(data => {
      expedientLighting.value = data
      // rows.value = data.length
      if (expedientLighting.value.length > 0) {
        isloading.value = false
      } else {
        if (expedientLighting.value.length <= 0) {
          isloading.value = false
        }
      }
    })

    const onFiltered = filteredItems => {
      // rows.value = filteredItems.length
      currentPage.value = 1
    }

    const refreshTable = () => {
      isloading.value = true
      getExpedientLighting(data => {
        expedientLighting.value = data
        // rows.value = data.length
        if (expedientLighting.value.length > 0) {
          isloading.value = false
        } else {
          if (expedientLighting.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }

    const addExpedientLighting = () => {
      createExpedientLighting(expedientLightingFields.value, data => {
        refreshTable()
        $toast.success('Alumbrado registrado correctamente.', {
          position: 'top-right',
          duration: 1500
        })
      })
      // resetStreetLightingFields()
      expedientLightingFields.value = JSON.parse(
        JSON.stringify(expedientLightingFieldsBlank)
      )
    }

    const RemoveExpedientLighting = StreetLightingId => {
      isloading.value = true
      deleteExpedientLighting(StreetLightingId, data => {
        refreshTable()
      })
    }
    return {
      expedientLighting,
      statusPublicLighting,
      expedientLightingFields,
      isOpen,
      perPage,
      currentPage,
      filter,
      perPageSelect,
      isloading,
      searchValue,
      searchField,
      expedientLightingFieldsBlank,
      fields,
      TypeTaskState,
      DescriptionSolutionState,
      HighDateState,
      addresdescriptionState,
      DomicileState,
      LowDateState,

      onFiltered,
      addExpedientLighting,
      refreshTable,
      RemoveExpedientLighting,
      validateTypeTask,
      validateDescriptionSolution,
      validateHighDate,
      validateAddresdescription,
      validateLowDate,
      validateDomicile
    }
  }
}
</script>

<style>

</style>
