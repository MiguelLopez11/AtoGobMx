<template>
  <b-card class="m-3">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-card>
      <div>
        <h3>Editar Tarea Tipo Alumbrado</h3>
      </div>
      <Form @submit="onUpdateTaskTypeLighting">
        <b-row cols="2">
          <!--Agregar nombre-->
          <b-col>
            <b-form-group class="mt-3" label="Nombre">
              <Field name="NameField" :rules="validateName" as="text">
                <b-form-input
                  v-model="taskTypeLighting.nombreTarea"
                  :state="NameState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage class="text-danger" name="NameField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--Agregar descricpcion-->
          <b-col>
            <b-form-group class="mt-3" label="Descripcion">
                <b-form-textarea
                  v-model="taskTypeLighting.descripcion"
                  rows="4"
                ></b-form-textarea>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="col-1 m-2 text-white"
            variant="primary"
            to="/ServiciosPublicos/TareaTipoAlumbrado/list"
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
import TaskTypeLightinServices from '@/Services/tasktypelighting.Services'
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
    const { getTaskTypeLightingById, updateTaskTypeLighting } = TaskTypeLightinServices()
    // const $toast = useToast()
    const taskTypeLighting = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const NameState = ref(false)
    const DescriptionState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Departamento Tipo Tarea', to: '/ServiciosPublicos/TareaTipoAlumbrado/list' },
      { text: 'Editar-TareaTipoAlumbrado' }
    ])

    const onUpdateTaskTypeLighting = () => {
      updateTaskTypeLighting(taskTypeLighting.value, data => {})
      swal.fire({
        title: '¡Tipo tarea modificado correctamente!',
        text: 'El tipo de tarea se ha modificado  satisfactoriamente.',
        icon: 'success'
      }).then(result => {
        if (result.isConfirmed) {
          redirect.push('/ServiciosPublicos/TareaTipoAlumbrado/list')
        }
      })
    }

    getTaskTypeLightingById(router.params.TareaTipoAlumbradoId, data => {
      taskTypeLighting.value = data
    })

    const validateName = () => {
      if (!taskTypeLighting.value.nombreTarea) {
        validateState()
        return 'Este campo es requerido'
      }
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(taskTypeLighting.value.nombreTarea)) {
        NameState.value = false
        return 'El tipo de tarea solo puede contener letras'
      }
      if (!taskTypeLighting.value.nombreTarea.trim().length > 0) {
        NameState.value = false
        return 'Este campo no puede contener espacios'
      }
      validateState()
      return true
    }

    const validateState = () => {
      // eslint-disable-next-line no-unneeded-ternary
      NameState.value = taskTypeLighting.value.nombreTarea !== ''
      // eslint-disable-next-line no-unneeded-ternary
      DescriptionState.value = taskTypeLighting.value.descripcion !== ''
    }

    return {
      taskTypeLighting,
      breadcrumbItems,
      NameState,
      //   router

      onUpdateTaskTypeLighting,
      validateName,
      validateState
    }
  }
}
</script>

<style></style>
