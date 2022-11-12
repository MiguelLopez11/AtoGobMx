<template>
  <b-card class="m-2">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-card>
      <div>
        <h3>Editar estatus</h3>
      </div>
      <Form @submit="onUpdateStatus()">
        <b-row cols="3">
          <b-col>
            <b-form-group class="mt-3" label="Nombre">
              <Field
                name="NameField"
                :rules="validateName"
                as="text"
              >
                <b-form-input
                  v-model="statusVehicle.nombre"
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
                v-model="statusVehicle.descripcion"
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
import VehiclesServices from '@/Services/vehicles.Services'
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
    const { getEstatusVehicle, updateEstatusVehicle } = VehiclesServices()
    const swal = inject('$swal')
    // const $toast = useToast()
    const statusVehicle = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const nameState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Estatus vehiculo', to: '/EstatusVehiculo/list' },
      { text: 'Editar Estatus Vehiculo' }
    ])
    const onUpdateStatus = () => {
      updateEstatusVehicle(statusVehicle.value, data => {
        swal.fire({
          title: 'Estatus modificado correctamente!',
          text: 'El estatus se ha modificado  satisfactoriamente.',
          icon: 'success'
        }).then(result => {
          if (result.isConfirmed) {
            redirect.push('/EstatusVehiculo/list')
          }
        })
      })
    }
    getEstatusVehicle(router.params.EstatusVehiculoId, data => {
      statusVehicle.value = data
    })
    const validateName = () => {
      if (!statusVehicle.value.nombre) {
        validateState()
        return 'Este campo es requerido'
      }
      // eslint-disable-next-line no-useless-escape
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(statusVehicle.value.nombre)) {
        validateState()
        return 'El nombre del area solo puede contener letras'
      }
      validateState()
      return true
    }
    const validateState = () => {
      nameState.value = statusVehicle.value.nombre !== ''
      return ''
    }
    return {
      statusVehicle,
      breadcrumbItems,
      //   router

      onUpdateStatus,
      validateName,
      validateState,
      nameState
    }
  }
}
</script>

<style></style>
