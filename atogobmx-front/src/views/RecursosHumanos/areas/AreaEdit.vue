<template>
  <b-card class="m-3">
    <b-card
      class="mb-4"
    >
        <b-breadcrumb
          class="p-0"
          :items="breadcrumbItems"
        >
        </b-breadcrumb>
    </b-card>
    <b-card>

    <div>
        <h3>Editar Area de trabajo</h3>
    </div>
    <Form @submit="onUpdateArea()">
        <b-row cols="3">
          <b-col>
            <b-form-group class="mt-3" label="Nombre de area">
              <Field name="NameField" :rules="validateArea">
                <b-form-input v-model="area.nombre" :state="nameState"> </b-form-input>
              </Field>
              <ErrorMessage name="NameField">
                <span>Este campo es requerido</span>
                <i class="bi bi-exclamation-circle" />
              </ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Descripción">
              <b-form-input v-model="area.descripcion" />
            </b-form-group>
          </b-col>
        </b-row>
      </Form>
      <b-row align-h="end">
          <b-button class="col-1 m-2 text-white" variant="primary" to="/Empleados/list" type="reset">Cancelar</b-button>
          <b-button class="col-1 m-2" variant="success" @click="onUpdateArea()">Guardar</b-button>
      </b-row>
    </b-card>
  </b-card>
</template>

<script>
import AreaServices from '@/Services/area.Services'
import { ref } from 'vue'
import { useRoute } from 'vue-router'
import { useToast } from 'vue-toast-notification'
import { Form, Field, ErrorMessage } from 'vee-validate'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    Form,
    Field,
    ErrorMessage
  },
  setup () {
    const { getArea, updateArea } = AreaServices()
    const $toast = useToast()
    const employee = ref([])
    const area = ref([])
    const router = useRoute()
    const nameState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Areas', to: '/Areas/list' },
      { text: 'Editar-Area' }
    ])
    // getEmployee(router.params.EmpleadoId, data => {
    //   employee.value = data[0]
    // })
    const onUpdateArea = () => {
      updateArea(area.value, data => {
      })
      $toast.success('Empleado actualizado correctamente.', {
        position: 'top-right',
        duration: 1500
      })
    }
    getArea(router.params.AreaId, data => {
      area.value = data
    })
    const validateArea = () => {
      if (!area.value.nombre) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateState = () => {
      // eslint-disable-next-line no-unneeded-ternary
      nameState.value = employee.value.nombreCompleto === '' ? false : true
      return 'HOli'
    }
    return {
      employee,
      area,
      breadcrumbItems,
      //   router

      onUpdateArea,
      validateArea,
      validateState,
      nameState
    }
  }

}
</script>

<style>

</style>
