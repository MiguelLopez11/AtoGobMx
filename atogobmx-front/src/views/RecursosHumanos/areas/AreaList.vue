<template>
  <b-card class="m-3">
    <b-row align-h="end" class="mb-3 mr-1">
      <b-form-input
        size="lg"
        style="width: 350px"
        v-model="searchValue"
        type="search"
        placeholder="Buscar Area..."
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
        v-b-modal.modal-area
        type="submit"
      >
        <i class="bi bi-person-plus-fill"></i>
        Agregar Area
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
      :items="areas"
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
          @click="RemoveArea(items.areaId)"
          class="m-1"
          variant="outline-danger"
          ><i class="bi bi-trash3"></i
        ></b-button>
        <b-button
          class="m-1"
          variant="outline-warning"
          :to="{
            name: 'Area-Edit',
            params: { AreaId: items.areaId }
          }"
        >
          <i class="bi bi-pencil-square" />
        </b-button>
      </template>
    </EasyDataTable>
    <b-modal
      id="modal-area"
      ref="refAreaModal"
      title="Agregar areas"
      size="xl"
      centered
      button-size="lg"
      hide-footer
    >
      <Form @submit="addArea">
        <b-row cols="3">
          <b-col>
            <b-form-group class="mt-3" label="Nombre">
              <Field name="NameField" :rules="validateArea">
                <b-form-input v-model="areaFields.nombre" :state="nameState">
                </b-form-input>
              </Field>
              <ErrorMessage name="NameField">
                <span>Este campo es requerido</span>
                <i class="bi bi-exclamation-circle" />
              </ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Departamento">
              <Field name="DepartamentField" :rules="validateDepartament">
                <b-form-select
                  v-model="areaFields.departamentoId"
                  autofocus
                  :options="departaments"
                  value-field="departamentoId"
                  text-field="nombre"
                  :state="departamentState"
                >
                </b-form-select>
              </Field>
              <ErrorMessage name="DepartamentField">
                <span>Este campo es requerido</span>
                <i class="bi bi-exclamation-circle" />
              </ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Descripción">
              <b-form-input v-model="areaFields.descripcion"> </b-form-input>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            v-b-modal.modal-area
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
import AreaServices from '@/Services/area.Services'
import DepartamentServices from '@/Services/departament.Services'
import { Form, Field, ErrorMessage } from 'vee-validate'
import { ref, onMounted } from 'vue'
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
    const { getAreas, createArea, deleteArea } = AreaServices()
    const { getDepartaments } = DepartamentServices()
    const $toast = useToast()
    const refAreaModal = ref()
    const departaments = ref([])
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
    const departamentState = ref(false)
    const areaFields = ref({
      areaId: 0,
      nombre: null,
      descripcion: null,
      departamentoId: 0,
      archivado: false
    })
    onMounted(() => {
      if (departaments.value.length === 0) {
        $toast.open({
          message: 'No se encuentran departamentos registrados en el sistema, registre primero un departamento para continuar',
          position: 'top-left',
          duration: 0,
          dismissible: true,
          type: 'error'
        })
      }
    })
    const areasFieldsBlank = ref(JSON.parse(JSON.stringify(areaFields)))
    const fields = ref([
      { value: 'areaId', text: 'ID', sortable: true },
      { value: 'nombre', text: 'Nombre' },
      { value: 'descripcion', text: 'Descripcion' },
      { value: 'departamentos.nombre', text: 'Departamento' },
      { value: 'actions', text: 'Acciones' }
    ])
    getDepartaments(data => {
      departaments.value = data
    })
    getAreas(data => {
      areas.value = data
      if (areas.value.length > 0) {
        isloading.value = false
      } else {
        if (areas.value.length <= 0) {
          isloading.value = false
        }
      }
    })
    const onFiltered = filteredItems => {
      rows.value = filteredItems.length
      currentPage.value = 1
    }
    const validateArea = () => {
      if (!areaFields.value.nombre) {
        nameState.value = false
        return 'Este campo es requerido'
      }
      nameState.value = true
      return true
    }
    const validateDepartament = () => {
      if (!areaFields.value.departamentoId) {
        departamentState.value = false
        return 'Este campo es requerido'
      }
      departamentState.value = true
      return true
    }
    const refreshTable = () => {
      isloading.value = true

      getAreas(data => {
        areas.value = data
        if (areas.value.length > 0) {
          isloading.value = false
        } else {
          if (areas.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }
    const addArea = () => {
      createArea(areaFields.value, data => {
        refreshTable()
        $toast.open({
          message: 'Area modificado correctamente',
          position: 'top',
          duration: 1000,
          dismissible: true,
          type: 'success',
          onDismiss: () => refAreaModal.value.hide()
        })
      })
      areaFields.value = JSON.parse(JSON.stringify(areasFieldsBlank))
      nameState.value = false
      departamentState.value = false
    }
    const RemoveArea = areaId => {
      isloading.value = true
      deleteArea(areaId, data => {
        refreshTable()
      })
    }
    return {
      departaments,
      refAreaModal,
      fields,
      perPage,
      currentPage,
      rows,
      filter,
      perPageSelect,
      areas,
      areasFieldsBlank,
      areaFields,
      isloading,
      searchValue,
      searchField,
      onFiltered,
      addArea,
      refreshTable,
      RemoveArea,
      nameState,
      departamentState,

      validateDepartament,
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
