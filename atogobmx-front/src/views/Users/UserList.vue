<template>
  <b-card class="m-2">
    <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
  </b-card>
  <b-card class="m-2">
    <b-row align-h="end" class="mb-3 mr-1">
      <b-form-input
        size="lg"
        style="width: 350px"
        v-model="searchValue"
        type="search"
        placeholder="Buscar Usuario..."
      >
      </b-form-input>
      <b-button
        style="
          background-color: rgb(94, 80, 238);
          height: 50px;
          width: auto;
          font-size: 18px;
          margin-right: 15px;
          margin-left: 20px;
        "
        @click="showModal = !showModal"
        type="submit"
      >
        <i class="bi bi-person-plus-fill"></i>
        Agregar Usuario
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
      :items="users"
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
            @click="RemoveUser(items.nombreUsuario)"
            class="m-1"
            variant="outline-danger"
            ><i class="bi bi-trash3"> Archivar</i></b-dropdown-item
          >
        </b-dropdown>
      </template>
    </EasyDataTable>
    <b-modal
      v-model="showModal"
      title="Agregar Usuario"
      size="xl"
      centered
      button-size="lg"
      lazy
      hide-footer
    >
      <Form @submit="addUser">
        <b-row cols="3">
          <b-col>
            <b-form-group class="mt-3" label="Nombre de Usuario">
              <Field name="userNameField" :rules="validateUserName" as="text">
                <b-form-input
                  type="text"
                  v-model="userFields.username"
                  :state="userNameState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage name="userNameField" />
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Contraseña">
              <Field name="passwordField" :rules="validatePassword" as="text">
                <b-form-input
                  type="password"
                  v-model="userFields.password"
                  :state="passwordState"
                />
              </Field>
              <ErrorMessage name="passwordField" />
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="CorreoElectronico">
              <b-form-input type="email" v-model="userFields.email" />
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Numero de telefono">
              <b-form-input v-model="userFields.phoneNumber" />
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Administrador">
              <b-form-checkbox size="lg" v-model="isAdmin" />
            </b-form-group>
          </b-col>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            @click="resetRoleFields"
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
import UsersServices from '@/Services/users.Services'
import { Form, Field, ErrorMessage } from 'vee-validate'
import { ref, inject } from 'vue'
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
    // Services
    const { getUsers, deleteUser, createUser, createAdminUser } =
      UsersServices()
    // Data
    const showModal = ref(false)
    const users = ref([])
    const userNameState = ref(false)
    const passwordState = ref(false)
    const isAdmin = ref(false)
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('nombreUsuario')
    const errorMessage = ref('')
    const confirmErrorMessage = ref('')
    // Fields
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      {
        text: 'Administrador',
        to: '/Administrador'
      },
      { text: 'Usuarios' }
    ])
    const userFields = ref({
      username: null,
      email: null,
      phoneNumber: null,
      password: null
    })
    const areasFieldsBlank = ref(JSON.parse(JSON.stringify(userFields)))
    const fields = ref([
      { value: 'nombreUsuario', text: 'Nombre de usuario' },
      { value: 'email', text: 'Correo electronico' },
      { value: 'numeroTelefono', text: 'Numero de telefono' },
      { value: 'actions', text: 'Acciones' }
    ])
    // Methods
    getUsers(data => {
      users.value = data
      if (users.value.length > 0) {
        isloading.value = false
      } else {
        if (users.value.length <= 0) {
          isloading.value = false
        }
      }
    })
    const onFiltered = filteredItems => {
      currentPage.value = 1
    }
    const resetRoleFields = () => {
      showModal.value = false
      userFields.value = JSON.parse(JSON.stringify(areasFieldsBlank))
      userNameState.value = false
      passwordState.value = false
    }
    const refreshTable = () => {
      isloading.value = true
      showModal.value = false
      getUsers(data => {
        users.value = data
        if (users.value.length > 0) {
          isloading.value = false
        } else {
          if (users.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }
    const addUser = () => {
      if (isAdmin.value) {
        createAdminUser(userFields.value, data => {
          console.log(data)
          if (!data.status) {
            refreshTable()
            resetRoleFields()
            swal.fire({
              title: '¡Error!',
              text: 'El usuario logeado no tiene permiso para realizar esta acción o uno de los campos ingresados no es correcto.',
              icon: 'error'
            })
          } else {
            refreshTable()
            resetRoleFields()
            swal.fire({
              title: 'Usuario registrado correctamente!',
              text: 'El Usuario se ha registrado al sistema satisfactoriamente.',
              icon: 'success'
            })
          }
        })
      } else {
        createUser(userFields.value, data => {
          if (data.status) {
            refreshTable()
            resetRoleFields()
            swal.fire({
              title: 'Usuario Sin permiso!',
              text: 'No tiene permiso para realizar esta acción.',
              icon: 'error'
            })
          } else {
            refreshTable()
            resetRoleFields()
            swal.fire({
              title: 'Usuario registrado correctamente!',
              text: 'El Usuario se ha registrado al sistema satisfactoriamente.',
              icon: 'success'
            })
          }
        })
      }
    }
    const RemoveUser = UserName => {
      isloading.value = true
      swal
        .fire({
          title: '¿Estas seguro?',
          text: 'No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Si, eliminar usuario!',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            swal
              .fire({
                title: 'Usuario eliminado!',
                text: 'El usuario ha sido eliminado satisfactoriamente .',
                icon: 'success'
              })
              .then(result => {
                if (result.isConfirmed) {
                  deleteUser(UserName, data => {
                    refreshTable()
                    resetRoleFields()
                  })
                }
              })
          } else {
            isloading.value = false
          }
        })
    }
    // Validations
    const validateUserName = () => {
      if (!userFields.value.username) {
        userNameState.value = false
        return 'Este campo es requerido'
      }
      userNameState.value = true
      return true
    }
    const validatePassword = () => {
      if (!userFields.value.password) {
        passwordState.value = false
        return 'Este campo es requerido '
      }
      if (
        !/^(?=.*\d)(?=.*[\u0021-\u002b\u003c-\u0040])(?=.*[A-Z])(?=.*[a-z])\S{8,16}$/.test(
          userFields.value.password
        )
      ) {
        passwordState.value = false
        errorMessage.value =
          'La contraseña debe de contener minimo 8 Caracteres, minusculas y mayusculas y mínimo un simbolo '
        return errorMessage.value
      }
      passwordState.value = true
      return true
    }
    return {
      fields,
      breadcrumbItems,
      perPage,
      currentPage,
      filter,
      perPageSelect,
      users,
      areasFieldsBlank,
      userFields,
      userNameState,
      passwordState,
      isloading,
      searchValue,
      searchField,
      errorMessage,
      confirmErrorMessage,
      showModal,
      isAdmin,

      onFiltered,
      addUser,
      refreshTable,
      RemoveUser,
      resetRoleFields,
      validateUserName,
      // validateRole,
      // validateEmployee,
      // validateState,
      validatePassword
    }
  }
}
</script>

<style scoped></style>
