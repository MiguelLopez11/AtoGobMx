<template>
  <b-card class="m-3">
    <b-row align-h="end" class="mb-3 mr-1">
      <b-form-input
        size="lg"
        style="width: 350px"
        v-model="searchValue"
        type="search"
        placeholder="Buscar Alumbrado..."
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
        v-b-modal.modal-publiclighting
        type="submit"
      >
        <i class="bi bi-person-plus-fill"></i>
        Agregar Alumbrado
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
      :items="publicLighting"
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
          @click="RemovePublicLighting(items.alumbradoId)"
          class="m-1"
          variant="outline-danger"
          ><i class="bi bi-trash3"></i
        ></b-button>
        <b-button
          class="m-1"
          variant="outline-warning"
          :to="{
            name: 'Alumbrado-Edit',
            params: { AlumbradoId: items.alumbradoId },
          }"
          ><i class="bi bi-pencil-square"></i
        ></b-button>
      </template>
    </EasyDataTable>
    <b-modal
      id="modal-publiclighting"
      title="Agregar Alumbrado"
      size="xl"
      hide-footer
      button-size="lg"
      lazy
    >
      <Form @submit="addPublicLighting">
        <b-row cols="2">
          <b-col>
            <!-- <b-form-group class="mt-3" label="Tipo de tarea">
                <Field
                  name="TaskField"
                  :rules="validateTask"
                >
                  <b-form-select
                    v-model="streetLightingFields.tareaTipoId"
                    autofocus
                    :options="departaments"
                    value-field="departamentoId"
                    text-field="nombre"
                    :state="TaskState"
                    @input="getAreas(EmployeesFields.departamentoId)"
                  >
                  </b-form-select>
                </Field>
                <ErrorMessage name="TaskField"
                  ><span class="text-danger">Este campo es requerido </span
                  ><i class="bi bi-exclamation-circle"></i
                ></ErrorMessage>
              </b-form-group> -->
            <b-form-group class="mt-3" label="Tipo Tarea">
              <Field name="TaskField" :rules="validateTask">
                <b-form-select
                  v-model="publicLightingFields.tarea"
                  autofocus
                  :state="TaskState"
                  :options="statusPublicLighting"
                  value-field="estatusId"
                  text-field="nombreEstatus"
                ></b-form-select>
              </Field>
              <ErrorMessage name="TaskField"
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
                  v-model="publicLightingFields.fechaAlta"
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
                  v-model="publicLightingFields.domicilio"
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
                  v-model="publicLightingFields.fechaBaja"
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
                  v-model="publicLightingFields.descripcionDomicilio"
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
                  v-model="publicLightingFields.descripcionSolucion"
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
            v-b-modal.modal-publiclighting
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
//  publiclightingServices
import PubliclightingServices from '@/Services/publiclighting.Services'
import StatusLightingServices from '@/Services/statuslighting.Services'
import EditExpedientLighting from '@/Services/expedientlighting.Services'
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
      getPublicLighting,
      createPublicLighting,
      deletePublicLighting
    } = PubliclightingServices()
    const {
      getStatus
    } = StatusLightingServices()
    const { createExpedientLighting } = EditExpedientLighting()
    const $toast = useToast()
    const publicLighting = ref([])
    const statusPublicLighting = ref([])
    // const option = ref()
    const isOpen = ref(false)
    const perPage = ref(5)
    const currentPage = ref(1)
    // const rows = ref(null)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('tarea')
    const TaskState = ref(false)
    const DescriptionSolutionState = ref(false)
    const HighDateState = ref(false)
    const addresdescriptionState = ref(false)
    const DomicileState = ref(false)
    const LowDateState = ref(false)
    const expedientPublicFieldBlank = ref({
      expedienteAlumbradoId: 0,
      alumbradoId: 0,
      archivado: false
    })
    const publicLightingFields = ref({
      alumbradoId: 0,
      tarea: null,
      descripcionSolucion: null,
      fechaAlta: null,
      fechaBaja: null,
      domicilio: null,
      descripcionDomicilio: null,
      archivado: false
    })

    getStatus((data) => {
      statusPublicLighting.value = data
    })

    // const getAreas = (departamentoId) => {
    //   getAreasByDepartament(departamentoId, data => {
    //     areas.value = data
    //     if (data.length === 0) {
    //       $toast.open({
    //         message: 'No se encuentran areas registrados en el departamento seleccionado, registre primero una area para continuar',
    //         position: 'top-left',
    //         duration: 2500,
    //         dismissible: true,
    //         type: 'error'
    //       })
    //     }
    //   })
    // }

    // const option = ref([
    //   { value: 'tarea', text: 'Mantenimiento' },
    //   { value: 'tarea', text: 'Reparacion' },
    //   { value: 'tarea', text: 'Instalacion', disabled: true }
    // ])

    const validateTask = () => {
      if (!publicLightingFields.value.tarea) {
        TaskState.value = false
        return 'Este campo es requerido'
      }
      TaskState.value = true
      return true
    }

    const validateDescriptionSolution = () => {
      if (!publicLightingFields.value.descripcionSolucion) {
        DescriptionSolutionState.value = false
        return 'Este campo es requerido'
      }
      DescriptionSolutionState.value = true
      return true
    }

    const validateDomicile = () => {
      if (!publicLightingFields.value.domicilio) {
        DomicileState.value = false
        return 'Este campo es requerido'
      }
      DomicileState.value = true
      return true
    }

    const validateLowDate = () => {
      if (!publicLightingFields.value.fechaBaja) {
        LowDateState.value = false
        return 'Este campo es requerido'
      }
      LowDateState.value = true
      return true
    }

    const validateHighDate = () => {
      if (!publicLightingFields.value.fechaAlta) {
        HighDateState.value = false
        return 'Este campo es requerido'
      }
      HighDateState.value = true
      return true
    }

    const validateAddresdescription = () => {
      if (!publicLightingFields.value.descripcionDomicilio) {
        addresdescriptionState.value = false
        return 'Este campo es requerido'
      }
      addresdescriptionState.value = true
      return true
    }

    // pone mis cambios de mis campos vacios de nuevo
    const publicLightingFieldsBlank = ref(
      JSON.parse(JSON.stringify(publicLightingFields))
    )

    const fields = ref([
      { value: 'alumbradoId', text: 'ID', sortable: true },
      { value: 'tarea', text: 'Tarea' },
      { value: 'fechaAlta', text: 'Fecha Alta' },
      { value: 'domicilio', text: 'Domicilio' },
      { value: 'descripcionDomicilio', text: 'Descripcion Domicilio' },
      { value: 'descripcionSolucion', text: 'Descripcion Solucion' },
      { value: 'fechaBaja', text: 'Fecha Baja' },
      { value: 'actions', text: 'Acciones' }
    ])

    // const resetStreetLightingFields = () => {
    //   streetLightingFields.value = JSON.parse(JSON.stringify(streetLightingFieldsBlank))
    //   TaskState.value = false
    //   DescriptionSolutionState.value = false
    //   HighDateState.value = false
    //   addresdescriptionState.value = false
    //   DomicileState.value = false
    //   LowDateState.value = false
    // }

    getPublicLighting((data) => {
      publicLighting.value = data
      // rows.value = data.length
      if (publicLighting.value.length > 0) {
        isloading.value = false
      } else {
        if (publicLighting.value.length <= 0) {
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
      getPublicLighting((data) => {
        publicLighting.value = data
        // rows.value = data.length
        if (publicLighting.value.length > 0) {
          isloading.value = false
        } else {
          if (publicLighting.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }

    const addPublicLighting = () => {
      createPublicLighting(publicLightingFields.value, (data) => {
        expedientPublicFieldBlank.value.alumbradoId = data.alumbradoId
        createExpedientLighting(expedientPublicFieldBlank.value, data => {
        })
        refreshTable()
        $toast.success('Alumbrado registrado correctamente.', {
          position: 'top-right',
          duration: 1500
        })
      })
      // resetStreetLightingFields()
      publicLightingFields.value = JSON.parse(
        JSON.stringify(publicLightingFieldsBlank))
    }

    const RemovePublicLighting = (StreetLightingId) => {
      isloading.value = true
      deletePublicLighting(StreetLightingId, (data) => {
        refreshTable()
      })
    }

    return {
      publicLighting,
      statusPublicLighting,
      publicLightingFields,
      isOpen,
      perPage,
      currentPage,
      // rows,
      filter,
      perPageSelect,
      isloading,
      searchValue,
      searchField,
      publicLightingFieldsBlank,
      // lightingFailuresValues,
      fields,
      TaskState,
      DescriptionSolutionState,
      HighDateState,
      addresdescriptionState,
      DomicileState,
      LowDateState,
      // option,

      onFiltered,
      addPublicLighting,
      refreshTable,
      RemovePublicLighting,
      validateTask,
      validateDescriptionSolution,
      validateHighDate,
      validateAddresdescription,
      validateLowDate,
      validateDomicile
      // resetStreetLightingFields
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
