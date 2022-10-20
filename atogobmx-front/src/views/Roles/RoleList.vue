<template>
  <b-card class="m-2">
    <b-row align-h="end" class="mb-3 mr-1">
      <b-form-input
        size="lg"
        style="width: 350px"
        v-model="searchValue"
        type="search"
        placeholder="Buscar Role..."
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
        <i class="bi bi-person-video2"></i>
        Agregar Role
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
      :items="roles"
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
          @click="RemoveRole(items.roleId)"
          class="m-1"
          variant="outline-danger"
          ><i class="bi bi-trash3"></i
        ></b-button>
        <b-button
          class="m-1"
          variant="outline-warning"
          :to="{
            name: 'Roles-Edit',
            params: { RoleId: items.roleId },
          }"
        >
          <i class="bi bi-pencil-square" />
        </b-button>
      </template>
    </EasyDataTable>
    <b-modal
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
              <Field
                name="NameField"
                :rules="validateName"
                as="text"
              >
                <b-form-input v-model="roleFields.nombre" :state="nameState"> </b-form-input>
              </Field>
              <ErrorMessage class="text-danger" name="NameField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Descripción">
              <b-form-input v-model="roleFields.descripcion"> </b-form-input>
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
import RoleServices from '@/Services/role.Services'
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
    const { getRoles, createRole, deleteRole } = RoleServices()
    // const $toast = useToast()
    const roles = ref([])
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
    const roleFields = ref({
      departamentoId: 0,
      nombre: null,
      descripcion: null,
      archivado: false
    })
    const roleFieldsBlank = ref(JSON.parse(JSON.stringify(roleFields)))
    const fields = ref([
      { value: 'roleId', text: 'ID', sortable: true },
      { value: 'nombre', text: 'Nombre' },
      { value: 'descripcion', text: 'Descripcion' },
      { value: 'actions', text: 'Acciones' }
    ])
    getRoles((data) => {
      roles.value = data
      if (roles.value.length > 0) {
        isloading.value = false
      } else {
        if (roles.value.length <= 0) {
          isloading.value = false
        }
      }
    })
    const onFiltered = (filteredItems) => {
      rows.value = filteredItems.length
      currentPage.value = 1
    }
    const validateName = () => {
      if (!roleFields.value.nombre) {
        nameState.value = false
        return 'Este campo es requerido'
      }
      // eslint-disable-next-line no-useless-escape
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(roleFields.value.nombre)) {
        nameState.value = false
        return 'El nombre del area solo puede contener letras'
      }
      nameState.value = true
      return true
    }
    const refreshTable = () => {
      isloading.value = true
      getRoles((data) => {
        roles.value = data
        if (roles.value.length > 0) {
          isloading.value = false
        } else {
          if (roles.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }
    const addDepartamento = () => {
      createRole(roleFields.value, (data) => {
        refreshTable()
        swal.fire({
          title: 'Role registrado correctamente!',
          text: 'El role se ha registrado al sistema satisfactoriamente.',
          icon: 'success'
        })
      })
      resetRoleFields()
    }
    const resetRoleFields = () => {
      showModal.value = false
      roleFields.value = JSON.parse(JSON.stringify(roleFieldsBlank))
      nameState.value = false
    }
    const RemoveRole = (roleId) => {
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
                  deleteRole(roleId, (data) => {
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
      roles,
      fields,
      perPage,
      currentPage,
      rows,
      filter,
      perPageSelect,
      roleFieldsBlank,
      roleFields,
      isloading,
      searchValue,
      searchField,
      onFiltered,
      addDepartamento,
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

<style>

</style>
