<template>
  <b-card class="m-2">
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
        <i class="bi bi-person-workspace m-1" />
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
      theme-color="#7367f0"
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
            @click="RemoveArea(items.areaId)"
            class="m-1"
            variant="outline-danger"
            ><i class="bi bi-trash3"> Archivar</i></b-dropdown-item
          >
          <b-dropdown-item
            class="m-1"
            variant="outline-warning"
            :to="{
              name: 'Area-Edit',
              params: { AreaId: items.areaId }
            }"
            ><i class="bi bi-pencil-square" /> Editar</b-dropdown-item
          >
        </b-dropdown>
      </template>
    </EasyDataTable>
    <b-modal
      v-model="showModal"
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
              <Field name="NameField" :rules="validateArea" as="text">
                <b-form-input v-model="areaFields.nombre" :state="nameState">
                </b-form-input>
              </Field>
              <ErrorMessage class="text-danger" name="NameField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Departamento">
              <Field
                name="DepartamentField"
                :rules="validateDepartament"
                as="number"
              >
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
              <ErrorMessage
                class="text-danger"
                name="DepartamentField"
              ></ErrorMessage>
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
            @click="resetForm()"
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
import { ref, watch, inject } from 'vue'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    Form,
    Field,
    ErrorMessage,
    EasyDataTable: window['vue3-easy-data-table']
  },
  setup () {
    const swal = inject('$swal')
    const { getAreas, createArea, deleteArea } = AreaServices()
    const { getDepartaments } = DepartamentServices()
    const showModal = ref(false)
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
    watch(departaments, values => {
      if (values.length === 0) {
        swal.fire({
          title: 'No se encuentran departamentos registrados!',
          text:
            'No se encuentran departamentos registrados en el sistema, registre primero un departamento para continuar.',
          icon: 'warning'
        })
      }
    })
    const areasFieldsBlank = ref(JSON.parse(JSON.stringify(areaFields)))
    const fields = ref([
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
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(areaFields.value.nombre)) {
        nameState.value = false
        return 'El nombre solo puede contener letras'
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
        swal.fire({
          title: '¡Area de trabajo registrado correctamente!',
          text:
            'El Area de trabajo se ha registrado al sistema satisfactoriamente.',
          icon: 'success'
        })
        refreshTable()
        resetForm()
      })
    }
    const resetForm = () => {
      showModal.value = false
      areaFields.value = JSON.parse(JSON.stringify(areasFieldsBlank))
      nameState.value = false
      departamentState.value = false
    }
    const RemoveArea = areaId => {
      isloading.value = true
      swal
        .fire({
          title: '¿Estas seguro?',
          text: 'No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Si, Archivar Puesto de trabajo!',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            swal
              .fire({
                title: 'Area de trabajo archivado!',
                text:
                  'El Area de trabajo ha sido archivado satisfactoriamente .',
                icon: 'success'
              })
              .then(result => {
                if (result.isConfirmed) {
                  deleteArea(areaId, data => {
                    refreshTable()
                  })
                }
              })
          } else {
            isloading.value = false
          }
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
      showModal,

      validateDepartament,
      validateArea,
      resetForm
    }
  }
}
</script>

<style></style>
