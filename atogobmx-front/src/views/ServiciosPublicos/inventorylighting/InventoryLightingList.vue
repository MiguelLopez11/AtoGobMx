<template>
  <b-card class="m-3">
    <b-row align-h="end" class="mb-3 mr-1">
      <b-form-input
        size="lg"
        style="width: 350px"
        v-model="searchValue"
        type="search"
        placeholder="Buscar Inventario Alumbrado..."
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
        v-b-modal.modal-inventorylighting
        type="submit"
      >
        <i class="bi bi-person-plus-fill"></i>
        Agregar Inventario
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
      :items="inventoryLighting"
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
          @click="RemoveInventoryLighting(items.inventarioAlumbradoId)"
          class="m-1"
          variant="outline-danger"
          ><i class="bi bi-trash3"></i
        ></b-button>
        <b-button
          class="m-1"
          variant="outline-warning"
          :to="{
            name: 'InventarioAlumbrado-Edit',
            params: { InventarioAlumbradoId: items.inventarioAlumbradoId },
          }"
          ><i class="bi bi-pencil-square"></i
        ></b-button>
      </template>
    </EasyDataTable>
    <b-modal
      id="modal-inventorylighting"
      title="Agregar Inventario Alumbrado"
      size="xl"
      hide-footer
      button-size="lg"
      lazy
    >
      <Form @submit="addInventoryLighting">
        <b-row cols="2">
          <b-col>
            <b-form-group class="mt-3" label="Nombre del tramite">
              <Field name="TypeTaskField" :rules="validateTypeTask">
                <b-form-select
                  v-model="inventoryLightingFields.tarea"
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
                  v-model="inventoryLightingFields.fechaAlta"
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
                  v-model="inventoryLightingFields.domicilio"
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
            <b-form-group class="mt-3" label="Fecha Baja">
              <Field name="LowDateField" :rules="validateLowDate">
                <Datepicker
                  locale="es"
                  name="date"
                  text-input
                  v-model="inventoryLightingFields.fechaBaja"
                  :state="LowDateState"
                ></Datepicker>
              </Field>
              <ErrorMessage name="LowDateField"
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
            v-b-modal.modal-inventorylighting
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
import ExpedientlightingServices from '@/Services/inventorylighting.Services'
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
      getInventoryLighting,
      createInventoryLighting,
      deleteInventoryLighting
    } = ExpedientlightingServices()
    const { getStatus } = StatusLightingServices()
    const $toast = useToast()
    const inventoryLighting = ref([])
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
    const HighDateState = ref(false)
    const DomicileState = ref(false)
    const LowDateState = ref(false)
    const inventoryLightingFields = ref({
      inventarioAlumbradoId: 0,
      tarea: null,
      fechaAlta: null,
      fechaBaja: null,
      domicilio: null,
      archivado: false
    })

    getStatus((data) => {
      statusPublicLighting.value = data
    })

    const validateTypeTask = () => {
      if (!inventoryLightingFields.value.tarea) {
        TypeTaskState.value = false
        return 'Este campo es requerido'
      }
      TypeTaskState.value = true
      return true
    }

    const validateDomicile = () => {
      if (!inventoryLightingFields.value.domicilio) {
        DomicileState.value = false
        return 'Este campo es requerido'
      }
      DomicileState.value = true
      return true
    }

    const validateLowDate = () => {
      if (!inventoryLightingFields.value.fechaBaja) {
        LowDateState.value = false
        return 'Este campo es requerido'
      }
      LowDateState.value = true
      return true
    }

    const validateHighDate = () => {
      if (!inventoryLightingFields.value.fechaAlta) {
        HighDateState.value = false
        return 'Este campo es requerido'
      }
      HighDateState.value = true
      return true
    }

    // pone mis cambios de mis campos vacios de nuevo
    const inventoryLightingFieldsBlank = ref(
      JSON.parse(JSON.stringify(inventoryLightingFields))
    )

    const fields = ref([
      { value: 'inventarioAlumbradoId', text: 'ID', sortable: true },
      { value: 'tarea', text: 'Nombre del tramite' },
      { value: 'fechaAlta', text: 'Fecha Alta' },
      { value: 'domicilio', text: 'Domicilio' },
      { value: 'fechaBaja', text: 'Fecha Baja' },
      { value: 'actions', text: 'Acciones' }
    ])

    getInventoryLighting((data) => {
      inventoryLighting.value = data
      // rows.value = data.length
      if (inventoryLighting.value.length > 0) {
        isloading.value = false
      } else {
        if (inventoryLighting.value.length <= 0) {
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
      getInventoryLighting((data) => {
        inventoryLighting.value = data
        // rows.value = data.length
        if (inventoryLighting.value.length > 0) {
          isloading.value = false
        } else {
          if (inventoryLighting.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }

    const addInventoryLighting = () => {
      createInventoryLighting(inventoryLightingFields.value, (data) => {
        refreshTable()
        $toast.success('Inventario registrado correctamente.', {
          position: 'top-right',
          duration: 1500
        })
      })
      // resetStreetLightingFields()
      inventoryLightingFields.value = JSON.parse(
        JSON.stringify(inventoryLightingFieldsBlank)
      )
    }

    const RemoveInventoryLighting = (StreetLightingId) => {
      isloading.value = true
      deleteInventoryLighting(StreetLightingId, (data) => {
        refreshTable()
      })
    }
    return {
      inventoryLighting,
      statusPublicLighting,
      inventoryLightingFields,
      isOpen,
      perPage,
      currentPage,
      filter,
      perPageSelect,
      isloading,
      searchValue,
      searchField,
      inventoryLightingFieldsBlank,
      fields,
      TypeTaskState,
      HighDateState,
      DomicileState,
      LowDateState,

      onFiltered,
      addInventoryLighting,
      refreshTable,
      RemoveInventoryLighting,
      validateTypeTask,
      validateHighDate,
      validateLowDate,
      validateDomicile
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
