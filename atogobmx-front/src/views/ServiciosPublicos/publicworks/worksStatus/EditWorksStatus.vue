<template>
  <b-card class="m-2">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-card>
      <div>
        <h3>Editar Obras Publicas Estatus</h3>
      </div>
      <Form @submit="onUpdateWorksStatus">
        <b-row cols="2">
          <!--Agregar nombre-->
          <b-col>
            <b-form-group class="mt-3" label="Nombre">
              <Field name="NameField" :rules="validateName" as="text">
                <b-form-input
                  v-model="worksStatus.nombre"
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
              <Field name="DescriptionField" :rules="validateDescription" as="text">
                <b-form-input
                  v-model="worksStatus.descripcion"
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
            to="/EstatusOP/list"
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
import WorksStatusServices from '@/Services/worksstatus.Services'
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
    const { getWorksStatusById, updateWorksStatus } = WorksStatusServices()
    const worksStatus = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const NameState = ref(false)
    const DescriptionState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Departamento estatus OP', to: '/EstatusOp/list' },
      { text: 'Editar-Obras publicas estatus' }
    ])
    const onUpdateWorksStatus = () => {
      updateWorksStatus(worksStatus.value, (data) => {})
      swal.fire({
        title: '¡Estatus de ibras publicas modificado correctamente!',
        text: 'El estatus de obras publicas se ha modificado  satisfactoriamente.',
        icon: 'success'
      }).then(result => {
        if (result.isConfirmed) {
          redirect.push('/EstatusOP/list')
        }
      })
    }
    getWorksStatusById(router.params.EstatusObraId, (data) => {
      worksStatus.value = data
    })

    const validateName = () => {
      if (!worksStatus.value.nombre) {
        validateState()
        return 'Este campo es requerido'
      }
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(worksStatus.value.nombre)) {
        NameState.value = false
        return 'El nombre de estatus solo puede contener letras'
      }
      validateState()
      return true
    }

    const validateDescription = () => {
      if (!worksStatus.value.descripcion) {
        validateState()
        return 'Este campo es requerido'
      }
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(worksStatus.value.descripcion)) {
        DescriptionState.value = false
        return 'La descripcion solo puede contener letras'
      }
      validateState()
      return true
    }

    const validateState = () => {
      // eslint-disable-next-line no-unneeded-ternary
      NameState.value = worksStatus.value.nombre === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      DescriptionState.value = worksStatus.value.descripcion === '' ? false : true
    }

    return {
      worksStatus,
      breadcrumbItems,
      NameState,
      DescriptionState,

      onUpdateWorksStatus,
      validateName,
      validateState,
      validateDescription
    }
  }
}
</script>

<style>

</style>
