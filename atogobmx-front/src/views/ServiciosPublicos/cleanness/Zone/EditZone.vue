<template>
  <b-card class="m-2">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-card>
      <div>
        <h3>Editar Zona</h3>
      </div>
      <Form @submit="onUpdateZoneService">
        <b-row cols="2">
          <!-- 1 -->
          <b-col>
            <b-form-group class="mt-3" label="Nombre">
              <Field
                name="NameField"
                :rules="validateName"
                as="text"
              >
                <b-form-input
                  v-model="zoneService.nombre"
                  :state="NameState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="NameField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!-- 2 -->
          <b-col>
            <b-form-group class="mt-3" label="Description">
              <Field name="DescriptionField" :rules="validateDescription" as="text">
                <b-form-input
                  v-model="zoneService.descripcion"
                  :state="DescriptionState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="DescriptionField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>

        </b-row>
        <b-row align-h="end">
          <b-button
            class="col-1 m-2 text-white"
            variant="primary"
            to="/Zona/list"
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
import ZoneService from '@/Services/zone.Services'
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
    const { getZoneById, updateZone } = ZoneService()
    // const $toast = useToast()
    const zoneService = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const NameState = ref(false)
    const DescriptionState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Departamento zona', to: '/Zona/list' },
      { text: 'Editar-zona' }
    ])
    const onUpdateZoneService = () => {
      updateZone(zoneService.value, (data) => {})
      swal.fire({
        title: '¡Zona modificado correctamente!',
        text: 'La zona se ha modificado  satisfactoriamente.',
        icon: 'success'
      }).then(result => {
        if (result.isConfirmed) {
          redirect.push('/Zona/list')
        }
      })
    }
    getZoneById(router.params.ZonaId, (data) => {
      zoneService.value = data
    })

    const validateName = () => {
      if (!zoneService.value.nombre) {
        validateState()
        return 'Este campo es requerido'
      }

      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(zoneService.value.nombre)) {
        NameState.value = false
        return 'Este campo solo puede contener letras'
      }

      //   if (!zoneService.value.nombre.trim().length > 0) {
      //     NameState.value = false
      //     return 'Este campo no puede contener espacios'
      //   }

      validateState()
      return true
    }

    const validateDescription = () => {
      if (!zoneService.value.descripcion) {
        validateState()
        return 'Este campo es requerido'
      }

      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(zoneService.value.descripcion)) {
        DescriptionState.value = false
        return 'Este campo solo puede contener numeros'
      }

      //   if (!zoneService.value.descripcion.trim().length > 0) {
      //     DescriptionState.value = false
      //     return 'Este campo no puede contener espacios'
      //   }

      validateState()
      return true
    }

    const validateState = () => {
      // eslint-disable-next-line no-unneeded-ternary
      NameState.value = zoneService.value.nombre === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      DescriptionState.value = zoneService.value.descripcion === '' ? false : true
    }

    return {
      zoneService,
      breadcrumbItems,
      NameState,
      DescriptionState,
      //   router

      onUpdateZoneService,
      validateState,
      validateName,
      validateDescription
    }
  }
}
</script>

<style>

</style>
