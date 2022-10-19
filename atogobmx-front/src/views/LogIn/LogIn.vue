<template>
    <section>
      <div class="container-fluid h-custom">
        <div class="row d-flex justify-content-center align-items-center h-80">
          <div class="col-md-9 col-lg-6 col-xl-5">
            <img
              src="https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-login-form/draw2.webp"
              class="img-fluid"
              alt="Sample image"
            />
          </div>
          <div class="col-md-8 col-lg-6 col-xl-4 offset-xl-1">
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
                <label class="form-label" for="form3Example4">Contraseña</label>
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
    const user = ref({
      userName: '',
      password: ''
    })
    const onLogIn = () => {
      LogIn(user.value, data => {
        if (data.usuarioId) {
          window.localStorage.setItem('User', JSON.stringify(data))
          swal.fire({
            title: 'Inicio de sesión correcto.!',
            text: 'Se ha iniciado sesion correctamente.',
            icon: 'success'
          }).then(result => {
            if (result.isConfirmed) {
              redirect.push({ name: 'Home', params: { User: data.nombreUsuario, Rol: data.role.nombre, userDepartament: data.empleado.departamentos.nombre } })
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
      userNameState.value = user.value.userName !== '' && user.value.userName !== null && user.value.userName.trim().length > 0 && /^[a-zA]+[A-Z0-9.-]+$/i.test(user.value.userName)
      passwordState.value = user.value.password !== '' && user.value.password !== null && user.value.password.trim().length > 0
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
.divider:after,
.divider:before {
  content: '';
  flex: 1;
  height: 1px;
  background: #eee;
}
.h-custom {
  height: calc(100% - 73px);
}
@media (max-width: 450px) {
  .h-custom {
    height: 100%;
  }
}
</style>
