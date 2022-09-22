<template>
  <b-card class="m-3">
    <b-row align-h="end" class="mb-3 mr-1">
      <b-form-input
        size="lg"
        style="width: 350px"
        v-model="searchValue"
        type="search"
        placeholder="Buscar Puesto de trabajo..."
      >
      </b-form-input>
      <b-button
        variant="primary"
        style="
          height: 50px;
          width: auto;
          font-size: 18px;
          margin-right: 15px;
          margin-left: 20px;
        "
        v-b-modal.modal-puestoTrabajo
        type="submit"
      >
        <i class="bi bi-person-plus-fill"></i>
        Agregar puesto trabajo
      </b-button>
    </b-row>
    <EasyDataTable
      rows-per-page-message="registros por pagina"
      empty-message="No se encuentran registros"
      table-class-name="customize-table"
      buttons-pagination
      border-cell
      :loading="isloading"
      :headers="fields"
      :items="workStations"
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
          @click="RemoveWorkStation(items.puestoTrabajoId)"
          class="m-1"
          variant="outline-danger"
          ><i class="bi bi-trash3"></i
        ></b-button>
        <b-button
          class="m-1"
          variant="outline-warning"
          :to="{
            name: 'PuestoTrabajo-Edit',
            params: { puestoTrabajoId: items.puestoTrabajoId },
          }"
        >
          <i class="bi bi-pencil-square" />
        </b-button>
      </template>
    </EasyDataTable>
    <b-modal
      id="modal-puestoTrabajo"
      title="Agregar puesto trabajo"
      size="xl"
      centered
      button-size="lg"
      hide-footer
    >
      <Form @submit="addWorkStation">
        <b-row cols="3">
          <b-col>
            <b-form-group class="mt-3" label="Nombre">
              <Field name="NameField" :rules="validateName">
                <b-form-input v-model="workStationFields.nombre" :state="nameState"> </b-form-input>
              </Field>
              <ErrorMessage name="NameField">
                <span>Este campo es requerido</span>
                <i class="bi bi-exclamation-circle" />
              </ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Area">
              <Field
                name="AreaField"
                :rules="validateArea"
              >
                <b-form-select
                  v-model="workStationFields.areaId"
                  autofocus
                  :options="areas"
                  value-field="areaId"
                  text-field="nombre"
                  :state="areaState"
                >
                </b-form-select>
              </Field>
              <ErrorMessage name="AreaField"
                ><span>Este campo es requerido </span
                ><i class="bi bi-exclamation-circle"></i
              ></ErrorMessage>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            v-b-modal.modal-puestoTrabajo
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
import workStationServices from '@/Services/workStation.Services'
import AreasServices from '@/Services/area.Services'
import { Form, Field, ErrorMessage } from 'vee-validate'
import { ref } from 'vue'
import { useToast } from 'vue-toast-notification'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    Form,
    Field,
    ErrorMessage,
    EasyDataTable: window['vue3-easy-data-table']
  },
  setup () {
    const { getWorkStations, createWorkStation, deleteWorkStation } = workStationServices()
    const { getAreas } = AreasServices()
    const $toast = useToast()
    const workStations = ref([])
    const areas = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    const rows = ref(null)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('nombre')
    const nameState = ref(false)
    const areaState = ref(false)
    const workStationFields = ref({
      puestoTrabajoId: 0,
      nombre: '',
      areaId: 0,
      archivado: false
    })
    const workStationFieldsBlank = ref(JSON.parse(JSON.stringify(workStationFields)))
    const fields = ref([
      { value: 'puestoTrabajoId', text: 'ID', sortable: true },
      { value: 'nombre', text: 'Nombre' },
      { value: 'area.nombre', text: 'Area' },
      { value: 'actions', text: 'Acciones' }
    ])
    getWorkStations((data) => {
      workStations.value = data
      if (workStations.value.length > 0) {
        isloading.value = false
      } else {
        if (workStations.value.length <= 0) {
          isloading.value = false
        }
      }
    })
    getAreas(data => {
      areas.value = data
    })
    const onFiltered = (filteredItems) => {
      rows.value = filteredItems.length
      currentPage.value = 1
    }
    const validateName = () => {
      if (!workStationFields.value.nombre) {
        nameState.value = false
        return 'Este campo es requerido'
      }
      nameState.value = true
      return true
    }
    const validateArea = () => {
      if (!workStationFields.value.areaId) {
        areaState.value = false
        return 'Este campo es requerido'
      }
      areaState.value = true
      return true
    }
    const refreshTable = () => {
      isloading.value = true
      getWorkStations((data) => {
        workStations.value = data
        if (workStations.value.length > 0) {
          isloading.value = false
        } else {
          if (workStations.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }
    const addWorkStation = () => {
      createWorkStation(workStationFields.value, (data) => {
        refreshTable()
        $toast.success('Puesto de trabajo registrado correctamente.', {
          position: 'top-right',
          duration: 2000
        })
      })
      workStationFields.value = JSON.parse(JSON.stringify(workStationFieldsBlank))
      nameState.value = false
    }
    const RemoveWorkStation = (puestoTrabajoId) => {
      isloading.value = true
      deleteWorkStation(puestoTrabajoId, (data) => {
        refreshTable()
      })
    }
    return {
      workStations,
      fields,
      perPage,
      currentPage,
      rows,
      areas,
      filter,
      perPageSelect,
      workStationFieldsBlank,
      workStationFields,
      isloading,
      searchValue,
      searchField,
      onFiltered,
      addWorkStation,
      refreshTable,
      RemoveWorkStation,
      nameState,
      areaState,

      validateName,
      validateArea
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
