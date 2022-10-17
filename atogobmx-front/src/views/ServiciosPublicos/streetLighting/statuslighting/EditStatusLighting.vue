<template>
  <b-card class="m-3">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-card>
      <div>
        <h3>Editar Estatus Alumbrado</h3>
      </div>
      <Form @submit="onUpdateStatusLighting">
        <b-row cols="2">
          <!--1-->
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
          <!--2-->
          <b-col>
            <b-form-group class="mt-3" label="Descripcion">
              <Field name="DescriptionField" :rules="validateDescription" as="text">
                <b-form-input
                  v-model="statusLighting.descripcion"
                  :state="DescriptionState"
                ></b-form-input>
              </Field>
              <ErrorMessage class="text-danger" name="DescriptionField"></ErrorMessage>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="col-1 m-2 text-white"
            variant="primary"
            to="/EstatusAlumbrado/list"
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
    const DescriptionState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Departamento estatus', to: '/EstatusAlumbrado/list' },
      { text: 'Editar-Estatus Alumbrado' }
    ])
    const onUpdateStatusLighting = () => {
      updateStatusLighting(statusLighting.value, (data) => {})
      swal.fire({
        title: '¡Estatus modificado correctamente!',
        text: 'El estatus se ha modificado  satisfactoriamente.',
        icon: 'success'
      }).then(result => {
        if (result.isConfirmed) {
          redirect.push('/EstatusAlumbrado/list')
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
    getStatusById(router.params.EstatusId, (data) => {
      statusLighting.value = data
    })

    const validateName = () => {
      if (!statusLighting.value.nombreEstatus) {
        validateState()
        return 'Este campo es requerido'
      }
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(statusLighting.value.nombreEstatus)) {
        NameState.value = false
        return 'El nombre de estatus solo puede contener letras'
      }
      validateState()
      return true
    }

    const validateDescription = () => {
      if (!statusLighting.value.descripcion) {
        validateState()
        return 'Este campo es requerido'
      }
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(statusLighting.value.descripcion)) {
        DescriptionState.value = false
        return 'La descripcion solo puede contener letras'
      }
      validateState()
      return true
    }

    const validateState = () => {
      // eslint-disable-next-line no-unneeded-ternary
      NameState.value = statusLighting.value.nombreEstatus === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      DescriptionState.value = statusLighting.value.descripcion === '' ? false : true
    }

    return {
      statusLighting,
      breadcrumbItems,
      NameState,
      DescriptionState,
      //   router

      onUpdateStatusLighting,
      validateName,
      validateState,
      validateDescription
    }
  }
}
</script>

<style>

</style>
