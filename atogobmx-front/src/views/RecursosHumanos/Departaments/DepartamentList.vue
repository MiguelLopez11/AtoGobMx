<template>
  <b-card class="m-2">
    <b-row align-h="end" class="mb-3 mr-1">
      <b-form-input
        size="lg"
        style="width: 350px"
        v-model="searchValue"
        type="search"
        placeholder="Buscar Departamento..."
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
        <i class="bi bi-building m-1" />
        Agregar Departamento
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
      :items="departaments"
      :rows-per-page="5"
      :search-field="searchField"
      :search-value="searchValue"
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
            @click="RemoveDepatamento(items.departamentoId)"
            class="m-1"
            variant="outline-danger"
            ><i class="bi bi-trash3"> Archivar</i></b-dropdown-item
          >
          <b-dropdown-item
            class="m-1"
            variant="outline-warning"
            :to="{
              name: 'Departamentos-Edit',
              params: { DepartamentoId: items.departamentoId }
            }"
            ><i class="bi bi-pencil-square" /> Editar</b-dropdown-item
          >
        </b-dropdown>
      </template>
    </EasyDataTable>
    <b-modal
      id="modal-departamento"
      v-model="showModal"
      title="Agregar Departamento"
      size="xl"
      centered
      button-size="lg"
      hide-footer
    >
      <Form @submit="addDepartamento">
        <b-row cols="3">
          <b-col>
            <b-form-group class="mt-3" label="Nombre">
              <Field name="NameField" :rules="validateArea" as="text">
                <b-form-input
                  v-model="departamentFields.nombre"
                  :state="nameState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage class="text-danger" name="NameField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Descripción">
              <b-form-input v-model="departamentFields.descripcion">
              </b-form-input>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            @click="resetDepartamentFields()"
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
import DepartamentServices from '@/Services/departament.Services'
import { Form, Field, ErrorMessage } from 'vee-validate'
import { ref, inject } from 'vue'
// import { useToast } from 'vue-toast-notification'
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
    const {
      getDepartaments,
      createDepartament,
      deleteDepartament
    } = DepartamentServices()
    // const $toast = useToast()
    const departaments = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    const rows = ref(null)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('nombre')
    const nameState = ref(false)
    const showModal = ref(false)
    const departamentFields = ref({
      departamentoId: 0,
      nombre: null,
      descripcion: null,
      archivado: false
    })
    const departamentFieldsBlank = ref(
      JSON.parse(JSON.stringify(departamentFields))
    )
    const fields = ref([
      { value: 'nombre', text: 'Nombre' },
      { value: 'descripcion', text: 'Descripcion' },
      { value: 'actions', text: 'Acciones' }
    ])
    getDepartaments(data => {
      departaments.value = data
      if (departaments.value.length > 0) {
        isloading.value = false
      } else {
        if (departaments.value.length <= 0) {
          isloading.value = false
        }
      }
    })
    const onFiltered = filteredItems => {
      rows.value = filteredItems.length
      currentPage.value = 1
    }
    const validateArea = () => {
      if (!departamentFields.value.nombre) {
        nameState.value = false
        return 'Este campo es requerido'
      }
      // eslint-disable-next-line no-useless-escape
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(departamentFields.value.nombre)) {
        nameState.value = false
        return 'El nombre del area solo puede contener letras'
      }
      nameState.value = true
      return true
    }
    const refreshTable = () => {
      isloading.value = true
      getDepartaments(data => {
        departaments.value = data
        if (departaments.value.length > 0) {
          isloading.value = false
        } else {
          if (departaments.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }
    const addDepartamento = () => {
      createDepartament(departamentFields.value, data => {
        refreshTable()
        swal.fire({
          title: 'Departamento registrado correctamente!',
          text:
            'El departamento se ha registrado al sistema satisfactoriamente.',
          icon: 'success'
        })
      })
      resetDepartamentFields()
    }
    const resetDepartamentFields = () => {
      showModal.value = false
      departamentFields.value = JSON.parse(
        JSON.stringify(departamentFieldsBlank)
      )
      nameState.value = false
    }
    const RemoveDepatamento = departamentoId => {
      isloading.value = true
      swal
        .fire({
          title: '¿Estas seguro?',
          text: 'No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Si, archivar departamento!',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            swal
              .fire({
                title: 'Departamento archivado!',
                text: 'El departamento ha sido archivado satisfactoriamente .',
                icon: 'success'
              })
              .then(result => {
                if (result.isConfirmed) {
                  deleteDepartament(departamentoId, data => {
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
      fields,
      perPage,
      currentPage,
      rows,
      filter,
      perPageSelect,
      departamentFieldsBlank,
      departamentFields,
      isloading,
      searchValue,
      searchField,
      onFiltered,
      addDepartamento,
      refreshTable,
      RemoveDepatamento,
      nameState,
      showModal,

      validateArea,
      resetDepartamentFields
    }
  }
}
</script>

<style></style>
