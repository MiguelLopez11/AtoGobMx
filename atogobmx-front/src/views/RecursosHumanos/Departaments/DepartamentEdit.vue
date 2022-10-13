<template>
  <b-card class="m-3">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-card>
      <div>
        <h3>Editar Departamento</h3>
      </div>
      <Form @submit="onUpdateDepartamento()">
        <b-row cols="3">
          <b-col>
            <b-form-group class="mt-3" label="Nombre">
              <Field
                name="NameField"
                :rules="validateArea"
                as="text"
              >
                <b-form-input
                  v-model="departament.nombre"
                  :state="nameState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage class="text-danger" name="NameField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group
              class="mt-3"
              label="Descripción"
            >
              <b-form-input
                v-model="departament.descripcion"
              />
            </b-form-group>
          </b-col>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="col-1 m-2 text-white"
            variant="primary"
            to="/Departamentos/list"
            type="reset"
          >
            Cancelar</b-button
          >
          <b-button type="success" class="col-1 m-2" variant="success">
            Guardar
          </b-button>
        </b-row>
      </Form>
    </b-card>
  </b-card>
</template>

<script>
import DepartamentServices from '@/Services/departament.Services'
import { ref, inject } from 'vue'
import { useRoute, useRouter } from 'vue-router'
// import { useToast } from 'vue-toast-notification'
import { Form, Field, ErrorMessage } from 'vee-validate'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    Form,
    Field,
    ErrorMessage
  },
  setup () {
    const { getDepartament, updateDepartament } = DepartamentServices()
    const swal = inject('$swal')
    // const $toast = useToast()
    const departament = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const nameState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Departamentos', to: '/Departamentos/list' },
      { text: 'Editar-Departamento' }
    ])
    const onUpdateDepartamento = () => {
      updateDepartament(departament.value, data => {
        swal.fire({
          title: 'Departamento modificado correctamente!',
          text: 'El departamento se ha modificado  satisfactoriamente.',
          icon: 'success'
        }).then(result => {
          if (result.isConfirmed) {
            redirect.push('/Departamentos/list')
          }
        })
      })
      // $toast.open({
      //   message: 'Departamento modificado correctamente',
      //   position: 'top',
      //   duration: 2000,
      //   dismissible: true,
      //   type: 'success',
      //   onDismiss: () => redirect.push('')
      // })
    }
    getDepartament(router.params.DepartamentoId, data => {
      departament.value = data
    })
    const validateArea = () => {
      if (!departament.value.nombre) {
        validateState()
        return 'Este campo es requerido'
      }
      // eslint-disable-next-line no-useless-escape
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(departament.value.nombre)) {
        nameState.value = false
        return 'El nombre del area solo puede contener letras'
      }
      validateState()
      return true
    }
    const validateState = () => {
      // eslint-disable-next-line no-unneeded-ternary
      nameState.value = departament.value.nombre === '' ? false : true
      return 'HOli'
    }
    return {
      departament,
      breadcrumbItems,
      //   router

      onUpdateDepartamento,
      validateArea,
      validateState,
      nameState
    }
  }
}
</script>

<style></style>
