<template>
  <b-card class="m-2">
    <b-row align-h="end" class="mb-3 mr-1">
      <b-form-input
        size="lg"
        style="width: 350px"
        v-model="searchValue"
        type="search"
        placeholder="Buscar categoria de mobiliario..."
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
        <i class="bi bi-columns"></i>
        Agregar categoria mobiliario
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
      :items="typeFurnitures"
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
            @click="RemoveRole(items.tipoMobiliarioId)"
            class="m-1"
            variant="outline-danger"
            ><i class="bi bi-trash3"> Archivar</i></b-dropdown-item
          >
          <b-dropdown-item
            class="m-1"
            variant="outline-warning"
            :to="{
              name: 'CategoriasMobiliario-Edit',
              params: { TipoMobiliarioId: items.tipoMobiliarioId }
            }"
            ><i class="bi bi-pencil-square" /> Editar</b-dropdown-item
          >
        </b-dropdown>
      </template>
    </EasyDataTable>
    <b-modal
      v-model="showModal"
      title="Agregar categoria mobiliario"
      size="xl"
      centered
      button-size="lg"
      hide-footer
    >
      <Form @submit="addtypeFurniture">
        <b-row cols="3">
          <b-col>
            <b-form-group class="mt-3" label="Nombre">
              <Field name="NameField" :rules="validateName" as="text">
                <b-form-input
                  v-model="typeFurnitureFields.nombre"
                  :state="nameState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage class="text-danger" name="NameField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Descripción">
              <b-form-input v-model="typeFurnitureFields.descripcion">
              </b-form-input>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            @click="resetRoleFields()"
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
import FurnitureServices from '@/Services/furniture.Services'
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
      getTypeFurnitures,
      createTypeFurniture,
      deleteTypeFurniture
    } = FurnitureServices()
    // const $toast = useToast()
    const typeFurnitures = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('nombre')
    const nameState = ref(false)
    const showModal = ref(false)
    const typeFurnitureFields = ref({
      nombre: null,
      descripcion: null,
      archivado: false
    })
    const typeFurnitureFieldsBlank = ref(
      JSON.parse(JSON.stringify(typeFurnitureFields))
    )
    const fields = ref([
      { value: 'nombre', text: 'Nombre' },
      { value: 'descripcion', text: 'Descripcion' },
      { value: 'actions', text: 'Acciones' }
    ])
    getTypeFurnitures(data => {
      typeFurnitures.value = data
      if (typeFurnitures.value.length > 0) {
        isloading.value = false
      } else {
        if (typeFurnitures.value.length <= 0) {
          isloading.value = false
        }
      }
    })
    const onFiltered = filteredItems => {
      currentPage.value = 1
    }
    const validateName = () => {
      if (!typeFurnitureFields.value.nombre) {
        nameState.value = false
        return 'Este campo es requerido'
      }
      // eslint-disable-next-line no-useless-escape
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(typeFurnitureFields.value.nombre)) {
        nameState.value = false
        return 'El nombre del area solo puede contener letras'
      }
      nameState.value = true
      return true
    }
    const refreshTable = () => {
      isloading.value = true
      getTypeFurnitures(data => {
        typeFurnitures.value = data
        if (typeFurnitures.value.length > 0) {
          isloading.value = false
        } else {
          if (typeFurnitures.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }
    const addtypeFurniture = () => {
      createTypeFurniture(typeFurnitureFields.value, data => {
        refreshTable()
        swal.fire({
          title: 'Categoría mobiliario registrado correctamente!',
          text:
            'El categoría mobiliario se ha registrado al sistema satisfactoriamente.',
          icon: 'success'
        })
      })
      resetRoleFields()
    }
    const resetRoleFields = () => {
      showModal.value = false
      typeFurnitureFields.value = JSON.parse(
        JSON.stringify(typeFurnitureFieldsBlank)
      )
      nameState.value = false
    }
    const RemoveRole = roleId => {
      isloading.value = true
      swal
        .fire({
          title: '¿Estas seguro?',
          text: 'No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Si, archivar Categoria!',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            swal
              .fire({
                title: 'Categoria archivado!',
                text: 'El categoría ha sido archivado satisfactoriamente .',
                icon: 'success'
              })
              .then(result => {
                if (result.isConfirmed) {
                  deleteTypeFurniture(roleId, data => {
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
      typeFurnitures,
      fields,
      perPage,
      currentPage,
      filter,
      perPageSelect,
      typeFurnitureFieldsBlank,
      typeFurnitureFields,
      isloading,
      searchValue,
      searchField,
      onFiltered,
      addtypeFurniture,
      refreshTable,
      RemoveRole,
      nameState,
      showModal,

      validateName,
      resetRoleFields
    }
  }
}
</script>

<style></style>
