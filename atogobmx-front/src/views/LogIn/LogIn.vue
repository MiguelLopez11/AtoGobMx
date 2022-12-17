<template>
  <section class="mt-5">
    <div
      class="px-4 py-5 px-md-5 text-center text-lg-start"
    >
      <div class="container">
        <div class="row gx-lg-5 align-items-center">
          <div class="col-lg-6 mb-5 mb-lg-0">
            <img
              src="https://www.atotonilco.gob.mx/templates/it_headlines/custom/images/logo_h.png"
              class="img-fluid"
              alt="Sample image"
            />
          </div>

          <div class="col-lg-6 mb-5 mb-lg-0">
            <div class="card" style="background-color: #f8f9fa">
              <b-img center height="200" width="200" src="https://img.icons8.com/external-flaticons-flat-flat-icons/344/external-login-web-development-flaticons-flat-flat-icons.png"></b-img>
              <div class="card-body py-3 px-md-5">
                <Form @submit="onLogIn">
                  <div class="form-outline mb-4">
                    <label class="form-label" for="form3Example3"
                      >Nombre de usuario</label
                    >
                    <Field
                      name="userNameField"
                      :rules="validateUserName"
                      as="text"
                    >
                      <b-form-input
                        v-model="user.userName"
                        :state="userNameState"
                        placeholder="Ingresa un usuario válido"
                        size="lg"
                      >
                      </b-form-input>
                    </Field>
                    <ErrorMessage class="text-danger" name="userNameField" />
                  </div>
                  <div class="form-outline mb-3">
                    <label class="form-label" for="form3Example4"
                      >Contraseña</label
                    >
                    <Field
                      name="PasswordField"
                      :rules="validatePassword"
                      as="text"
                    >
                      <b-form-input
                        v-model="user.password"
                        :state="passwordState"
                        placeholder="Ingresa una contraseña válida"
                        size="lg"
                        type="password"
                      >
                      </b-form-input>
                    </Field>
                    <ErrorMessage class="text-danger" name="PasswordField" />
                  </div>
                  <div class="text-center text-lg-start mt-4 pt-2">
                    <button
                      type="success"
                      class="btn btn-lg"
                      style="padding-left: 2.5rem; background-color: rgb(94,80,238); color:white; padding-right: 2.5rem;"
                    >
                      Iniciar Sesión
                    </button>
                  </div>
                </Form>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
import LogInServices from '@/Services/logIn.Services'
import { ref, inject } from 'vue'
import { useRouter } from 'vue-router'
import { Form, Field, ErrorMessage } from 'vee-validate'
export default {
  components: {
    Form,
    Field,
    ErrorMessage
  },
  setup () {
    const swal = inject('$swal')
    const redirect = useRouter()
    const { LogIn } = LogInServices()
    const userNameState = ref(false)
    const passwordState = ref(false)
    window.sessionStorage.removeItem('session')
    const user = ref({
      userName: '',
      password: ''
    })
    const onLogIn = () => {
      LogIn(user.value, data => {
        if (data.token) {
          window.sessionStorage.setItem('isLogged', true)

          window.sessionStorage.setItem('Token', data.token)
          window.sessionStorage.setItem('RefreshToken', data.refreshToken)
          // window.sessionStorage.setItem('Departamento', data.empleado.departamentos.nombre)
          // window.sessionStorage.setItem('Area', data.empleado.area.nombre)
          // window.sessionStorage.setItem('Empleado', data.empleado.nombreCompleto)
          // window.sessionStorage.setItem('EmpleadoId', data.empleado.empleadoId)
          swal
            .fire({
              title: 'Inicio de sesión correcto.!',
              text: 'Se ha iniciado sesion correctamente.',
              icon: 'success',
              confirmButtonText: 'Ir a inicio'
            })
            .then(result => {
              if (result.isConfirmed) {
                redirect.go('/')
              }
            })
        } else {
          swal.fire({
            title: 'Inicio de sesión Fallido.!',
            text: 'Usuario o contraseña ingresado no es correcto.',
            icon: 'error'
          })
        }
      })
    }
    const validateUserName = () => {
      if (!user.value.userName) {
        validateState()
        return 'Este campo es requerido'
      }
      if (!user.value.userName.trim().length > 0) {
        validateState()
        return 'el campo no puede tener solo espacios'
      }
      if (!/^[a-zA]+[A-Z0-9.-]+$/i.test(user.value.userName)) {
        validateState()
        return 'El nombre de usuario no puede contener espacios'
      }
      validateState()
      return true
    }
    const validatePassword = () => {
      if (!user.value.password) {
        validateState()
        return 'Este campo es requerido'
      }
      if (!user.value.password.trim().length > 0) {
        validateState()
        return 'el campo no puede tener solo espacios'
      }
      validateState()
      return true
    }
    const validateState = () => {
      userNameState.value =
        user.value.userName !== '' &&
        user.value.userName !== null &&
        user.value.userName.trim().length > 0 &&
        /^[a-zA]+[A-Z0-9.-]+$/i.test(user.value.userName)
      passwordState.value =
        user.value.password !== '' &&
        user.value.password !== null &&
        user.value.password.trim().length > 0
      return ''
    }
    return {
      user,
      userNameState,
      passwordState,

      validateUserName,
      validatePassword,
      validateState,
      onLogIn
    }
  }
}
</script>

<style scoped>

</style>
