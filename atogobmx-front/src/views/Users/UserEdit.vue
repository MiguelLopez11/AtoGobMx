<template>
  <b-card class="m-2">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-card>
      <div>
        <h3>Editar Usuario</h3>
      </div>
      <Form @submit="onUpdateUser">
        <b-row cols="3">
          <b-col>
            <b-form-group class="mt-3" label="Nombre de Usuario">
              <Field name="userNameField" :rules="validateUserName">
                <b-form-input type="text" v-model="user.nombreUsuario" :state="userNameState">
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
                <b-form-input type="password" v-model="user.contraseña" :state="passwordState"/>
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
                  v-model="user.confirmarContraseña"
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
                  v-model="user.roleId"
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
                  v-model="user.empleadoId"
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
            to="/Administrador/Roles/list"
          >
            Cancelar
          </b-button>
          <b-button class="w-auto m-2" variant="success" type="submit">
            Guardar
          </b-button>
        </b-row>
      </Form>
    </b-card>
  </b-card>
</template>

<script>
import UsersServices from '@/Services/users.Services'
import RoleServices from '@/Services/role.Services'
import EmployeeServices from '@/Services/employee.Services'
import { Field, Form, ErrorMessage } from 'vee-validate'
import { ref, inject } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    Field,
    Form,
    ErrorMessage
  },
  setup () {
    const swal = inject('$swal')
    const { getEmployees } = EmployeeServices()
    const { getUser, updateUser } = UsersServices()
    const { getRoles } = RoleServices()
    const employees = ref([])
    const user = ref([])
    const roles = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const userNameState = ref(false)
    const passwordState = ref(false)
    const confirmPasswordState = ref(false)
    const roleState = ref(false)
    const employeeState = ref(false)
    const errorMessage = ref('')
    const confirmErrorMessage = ref('')
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Usuarios', to: '/Administrador/Usuarios/list' },
      { text: 'Editar-Usuario' }
    ])
    getUser(router.params.usuarioId, (data) => {
      user.value = data
    })
    getEmployees(data => {
      employees.value = data
      // eslint-disable-next-line no-unneeded-ternary
      passwordState.value = data.contraseña === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      confirmPasswordState.value = data.confirmarContraseña === '' ? false : true
    })
    getRoles(data => {
      roles.value = data
    })
    const validateUserName = () => {
      if (!user.value.nombreUsuario) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateRole = () => {
      if (!user.value.roleId) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateEmployee = value => {
      if (!user.value.empleadoId) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validatePassword = () => {
      if (!user.value.contraseña) {
        passwordState.value = false
        errorMessage.value = 'Este campo es requerido '
        return errorMessage.value
      }
      const regex = /^(?=.*\d)(?=.*[a-zA-Z])[a-zA-Z0-9]{8,}$/
      if (!regex.test(user.value.contraseña)) {
        passwordState.value = false
        errorMessage.value = 'La contraseña debe de contener minimo 8 Caracteres, minusculas y mayusculas '
        return errorMessage.value
      }
      passwordState.value = true
      return true
    }
    const validateConfirmPassword = () => {
      if (!user.value.confirmarContraseña) {
        confirmPasswordState.value = false
        confirmErrorMessage.value = 'Este campo es requerido '
        return confirmErrorMessage.value
      }
      if (user.value.contraseña !== user.value.confirmarContraseña) {
        confirmPasswordState.value = false
        confirmErrorMessage.value = 'Las contraseñas no coinciden '
        return confirmErrorMessage.value
      }
      const regex = /^(?=.*\d)(?=.*[a-zA-Z])[a-zA-Z0-9]{8,}$/
      if (!regex.test(user.value.confirmarContraseña)) {
        passwordState.value = false
        confirmErrorMessage.value = 'La contraseña debe de contener minimo 8 Caracteres, minusculas y mayusculas '
        return confirmErrorMessage.value
      }
      confirmPasswordState.value = true
      return true
    }
    const validateState = () => {
      // eslint-disable-next-line no-unneeded-ternary
      userNameState.value = user.value.nombreUsuario === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      roleState.value = user.value.roleId === 0 ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      employeeState.value = user.value.empleadoId === 0 ? false : true

      return ''
    }
    const onUpdateUser = () => {
      updateUser(user.value, (data) => {
        swal.fire({
          title: 'Usuario modificado correctamente!',
          text: 'El Usuario se ha modificado al sistema satisfactoriamente.',
          icon: 'success'
        }).then(result => {
          if (result.isConfirmed) {
            redirect.push('/Administrador/Usuarios/list')
          }
        })
      })
    }
    return {
      user,
      roles,
      employees,
      breadcrumbItems,
      router,
      errorMessage,
      confirmErrorMessage,
      userNameState,
      passwordState,
      confirmPasswordState,
      roleState,
      employeeState,
      redirect,
      validateUserName,
      validateRole,
      validateEmployee,
      validateState,
      validatePassword,
      validateConfirmPassword,
      onUpdateUser
    }
  }
}
</script>

<style>
</style>
