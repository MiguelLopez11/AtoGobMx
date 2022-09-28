<template>
  <b-card class="m-3">
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
        variant="primary"
        style="
          height: 50px;
          width: auto;
          font-size: 18px;
          margin-right: 15px;
          margin-left: 20px;
        "
        v-b-modal.modal-users
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
      :table-height="330"
    >
      <template #header-actions="header">
        {{ header.text }}
      </template>
      <template #item-actions="items">
        <b-button
          @click="RemoveUser(items.usuarioId)"
          class="m-1"
          variant="outline-danger"
          ><i class="bi bi-trash3"></i
        ></b-button>
        <b-button
          class="m-1"
          variant="outline-warning"
          :to="{
            name: 'Usuarios-Edit',
            params: { usuarioId: items.usuarioId }
          }"
        >
          <i class="bi bi-pencil-square" />
        </b-button>
      </template>
    </EasyDataTable>
    <b-modal
      id="modal-users"
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
              <Field name="userNameField" :rules="validateUserName">
                <b-form-input type="text" v-model="userFields.nombreUsuario" :state="userNameState">
                </b-form-input>
              </Field>
              <ErrorMessage name="userNameField">
                <span>Este campo es requerido </span
                ><i class="bi bi-exclamation-circle"></i
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Contraseña">
              <Field name="passwordField" :rules="validatePassword">
                <b-form-input type="password" v-model="userFields.contraseña" :state="passwordState"/>
              </Field>
              <ErrorMessage name="passwordField">
                <span>{{errorMessage}} </span>
                <i class="bi bi-exclamation-circle"></i>
              </ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Confirmar Contraseña">
              <Field name="ConfirmPasswordField" :rules="validateConfirmPassword">
                <b-form-input
                  type="password"
                  v-model="userFields.confirmarContraseña"
                  :state="confirmPasswordState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage name="ConfirmPasswordField">
                <span>{{confirmErrorMessage}}</span>
                <i class="bi bi-exclamation-circle"></i>
              </ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Role">
              <Field name="roleField" :rules="validateRole">
                <b-form-select
                  autofocus
                  :options="roles"
                  value-field="roleId"
                  text-field="nombre"
                  v-model="userFields.roleId"
                  :state="roleState"

                ></b-form-select>
              </Field>
              <ErrorMessage name="roleField">
                <span>Este campo es requerido </span
                ><i class="bi bi-exclamation-circle"></i
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Empleado">
              <Field name="employeeField" :rules="validateEmployee">
                <b-form-select
                  autofocus
                  :options="employees"
                  value-field="empleadoId"
                  text-field="nombreCompleto"
                  v-model="userFields.empleadoId"
                  :state="employeeState"
                ></b-form-select>
              </Field>
              <ErrorMessage name="employeeField">
                <span>Este campo es requerido </span
                ><i class="bi bi-exclamation-circle"></i
              ></ErrorMessage>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            v-b-modal.modal-users
          >
            Cancelar
          </b-button>
          <b-button class="w-auto m-2" variant="success" type="submit">
            Guardar
          </b-button>
        </b-row>
      </Form>
    </b-modal>
    {{roles.length}}
  </b-card>
</template>

<script>
import UsersServices from '@/Services/users.Services'
import RoleServices from '@/Services/role.Services'
import EmployeeServices from '@/Services/employee.Services'
import { Form, Field, ErrorMessage } from 'vee-validate'
import { ref, watch } from 'vue'
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
    // Services
    const { getEmployees } = EmployeeServices()
    const { getUsers, deleteUser, createUser } = UsersServices()
    const { getRoles } = RoleServices()
    // Data
    const $toast = useToast()
    const users = ref([])
    const roles = ref([])
    const employees = ref([])
    const userNameState = ref(false)
    const passwordState = ref(false)
    const confirmPasswordState = ref(false)
    const roleState = ref(false)
    const employeeState = ref(false)
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('nombre')
    const errorMessage = ref('')
    const confirmErrorMessage = ref('')
    // Fields
    const userFields = ref({
      usuarioId: 0,
      nombreUsuario: '',
      contraseña: null,
      confirmarContraseña: null,
      archivado: false,
      roleId: 0,
      empleadoId: 0
    })
    const areasFieldsBlank = ref(JSON.parse(JSON.stringify(userFields)))
    const fields = ref([
      { value: 'usuarioId', text: 'ID', sortable: true },
      { value: 'nombreUsuario', text: 'Nombre de usuario' },
      { value: 'role.nombre', text: 'Role' },
      { value: 'actions', text: 'Acciones' }
    ])
    // Methods
    getRoles(data => {
      roles.value = data
    })
    getEmployees(data => {
      employees.value = data
    })
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
    watch(roles, (value) => {
      if (value.length === 0) {
        $toast.open({
          message: 'No se encuentran roles registrados en el sistema, registre primero un departamento para continuar',
          position: 'top-left',
          duration: 0,
          dismissible: true,
          type: 'error'
        })
      }
    })
    const onFiltered = filteredItems => {
      currentPage.value = 1
    }
    const refreshTable = () => {
      isloading.value = true
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
      createUser(userFields.value, data => {
        refreshTable()
        $toast.success('Usuario registrado correctamente.', {
          position: 'top-right',
          duration: 2000
        })
      })
      userFields.value = JSON.parse(JSON.stringify(areasFieldsBlank))
    }
    const RemoveUser = areaId => {
      isloading.value = true
      deleteUser(areaId, data => {
        refreshTable()
      })
    }
    // Validations
    const validateUserName = () => {
      if (!userFields.value.nombreUsuario) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateRole = () => {
      if (!userFields.value.roleId) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateEmployee = value => {
      if (!userFields.value.empleadoId) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validatePassword = () => {
      if (!userFields.value.contraseña) {
        passwordState.value = false
        errorMessage.value = 'Este campo es requerido '
        return errorMessage.value
      }
      const regex = /^(?=.*\d)(?=.*[a-zA-Z])[a-zA-Z0-9]{8,}$/
      if (!regex.test(userFields.value.contraseña)) {
        passwordState.value = false
        errorMessage.value = 'La contraseña debe de contener minimo 8 Caracteres, minusculas y mayusculas '
        return errorMessage.value
      }
      passwordState.value = true
      return true
    }
    const validateConfirmPassword = () => {
      if (!userFields.value.contraseña) {
        passwordState.value = false
        confirmErrorMessage.value = 'Este campo es requerido '
        return confirmErrorMessage.value
      }
      if (userFields.value.contraseña !== userFields.value.confirmarContraseña) {
        confirmPasswordState.value = false
        confirmErrorMessage.value = 'Las contraseñas no coinciden '
        return confirmErrorMessage.value
      }
      const regex = /^(?=.*\d)(?=.*[a-zA-Z])[a-zA-Z0-9]{8,}$/
      if (!regex.test(userFields.value.contraseña)) {
        passwordState.value = false
        confirmErrorMessage.value = 'La contraseña debe de contener minimo 8 Caracteres, minusculas y mayusculas '
        return confirmErrorMessage.value
      }
      confirmPasswordState.value = true
      return true
    }
    const validateState = () => {
      // eslint-disable-next-line no-unneeded-ternary
      userNameState.value = userFields.value.nombreUsuario === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      roleState.value = userFields.value.roleId === 0 ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      employeeState.value = userFields.value.empleadoId === 0 ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      // confirmPasswordState.value = userFields.value.confirmarContraseña === null ? false : true

      return ''
    }
    return {
      fields,
      perPage,
      currentPage,
      filter,
      perPageSelect,
      users,
      roles,
      employees,
      areasFieldsBlank,
      userFields,
      userNameState,
      passwordState,
      confirmPasswordState,
      roleState,
      employeeState,
      isloading,
      searchValue,
      searchField,
      errorMessage,
      confirmErrorMessage,
      onFiltered,
      addUser,
      refreshTable,
      RemoveUser,
      validateUserName,
      validateRole,
      validateEmployee,
      validateState,
      validatePassword,
      validateConfirmPassword
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
