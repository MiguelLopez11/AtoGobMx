<template>
  <b-card class="m-2">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-card>
      <div>
        <h3>Editar Estatus Alumbrado</h3>
      </div>
      <Form @submit="onUpdateStatusLighting">
        <b-row cols="2">
          <!--Agregar nombre-->
          <b-col>
            <b-form-group class="mt-3" label="Nombre">
              <Field name="NameField" :rules="validateName" as="text">
                <b-form-input
                  v-model="statusLighting.nombreEstatus"
                  :state="NameState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage class="text-danger" name="NameField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--Agregar descripcion-->
          <b-col>
            <b-form-group class="mt-3" label="Descripcion">
                <b-form-textarea
                  v-model="statusLighting.descripcion"
                  rows="4"
                ></b-form-textarea>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="col-1 m-2 text-white"
            variant="primary"
            to="/ServiciosPublicos/EstatusAlumbrado/list"
            type="reset"
          >
            Cancelar
          </b-button>
          <b-button type="success" class="col-1 m-2" variant="success">
            Guardar
          </b-button>
        </b-row>
      </Form>
    </b-card>
  </b-card>
</template>

<script>
import StatusLightinServices from '@/Services/statuslighting.Services'
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
    const swal = inject('$swal')
    const { getStatusById, updateStatusLighting } = StatusLightinServices()
    // const $toast = useToast()
    const statusLighting = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const NameState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Departamento estatus', to: '/ServiciosPublicos/EstatusAlumbrado/list' },
      { text: 'Editar-Estatus Alumbrado' }
    ])
    const onUpdateStatusLighting = () => {
      updateStatusLighting(statusLighting.value, data => {})
      swal
        .fire({
          title: '¡Estatus modificado correctamente!',
          text: 'El estatus se ha modificado  satisfactoriamente.',
          icon: 'success'
        })
        .then(result => {
          if (result.isConfirmed) {
            redirect.push('/ServiciosPublicos/EstatusAlumbrado/list')
          }
        })
      // $toast.open({
      //   message: 'Estetus Alumbrado modificado correctamente',
      //   position: 'top',
      //   duration: 2000,
      //   dismissible: true,
      //   type: 'success',
      //   onDismiss: () => redirect.push('/EstatusAlumbrado/list')
      // })
    }
    getStatusById(router.params.EstatusId, data => {
      statusLighting.value = data
    })

    const validateName = () => {
      if (!statusLighting.value.nombreEstatus) {
        validateState()
        return 'Este campo es requerido'
      }
      if (
        !/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(statusLighting.value.nombreEstatus)
      ) {
        NameState.value = false
        return 'El nombre de estatus solo puede contener letras'
      }
      if (!statusLighting.value.nombreEstatus.trim().length > 0) {
        NameState.value = false
        return 'Este campo no puede contener espacios'
      }
      validateState()
      return true
    }

    const validateState = () => {
      NameState.value = statusLighting.value.nombreEstatus !== ''
    }

    return {
      statusLighting,
      breadcrumbItems,
      NameState,
      //   router

      onUpdateStatusLighting,
      validateName,
      validateState
    }
  }
}
</script>

<style></style>
