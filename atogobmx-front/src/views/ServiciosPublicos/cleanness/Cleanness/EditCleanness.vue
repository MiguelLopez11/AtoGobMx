<template>
  <b-card class="m-2">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-card>
      <div>
        <h3>Editar Direccion Cementerio</h3>
      </div>
      <Form @submit="onUpdateCleannessService">
        <b-row cols="2">
          <!-- 1 -->
          <b-col>
            <b-form-group class="mt-3" label="Nombre del servicio">
              <Field
                name="NameServiceField"
                :rules="validateNameService"
                as="text"
              >
                <b-form-input
                  v-model="cleannessService.nombreServicio"
                  :state="NameServiceState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="NameServiceField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!-- 2 -->
          <b-col>
            <b-form-group class="mt-3" label="Establecimiento publico">
              <Field name="PublicEstablishmentField" :rules="validatePublicEstablishment" as="text">
                <b-form-input
                  v-model="cleannessService.establecimientoPublico"
                  :state="PublicEstablishmentState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="PublicEstablishmentField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!-- 3 -->
          <b-col>
            <b-form-group class="mt-3" label="Domicilio">
              <Field name="DomicileField" :rules="validateDomicile" as="text">
                <b-form-input
                  v-model="cleannessService.domicilio"
                  :state="DomicileState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="DomicileField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!-- 4 -->
          <b-col>
            <b-form-group class="mt-3" label="Objetivo">
              <Field
                name="ObjectiveField"
                :rules="validateObjective"
                as="text"
              >
                <b-form-input
                  v-model="cleannessService.objetivo"
                  :state="ObjectiveState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="ObjectiveField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>

        </b-row>
        <b-row align-h="end">
          <b-button
            class="col-1 m-2 text-white"
            variant="primary"
            to="/Aseo/list"
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
import CleannessService from '@/Services/cleanness.Services'
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
    const { getCleannessById, updateCleanness } = CleannessService()
    // const $toast = useToast()
    const cleannessService = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const NameServiceState = ref(false)
    const PublicEstablishmentState = ref(false)
    const DomicileState = ref(false)
    const ObjectiveState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Departamento aseo', to: '/Aseo/list' },
      { text: 'Editar-aseo' }
    ])
    const onUpdateCleannessService = () => {
      updateCleanness(cleannessService.value, (data) => {})
      swal.fire({
        title: '¡Aseo modificado correctamente!',
        text: 'El aseo se ha modificado  satisfactoriamente.',
        icon: 'success'
      }).then(result => {
        if (result.isConfirmed) {
          redirect.push('/Aseo/list')
        }
      })
    }
    getCleannessById(router.params.AseoId, (data) => {
      cleannessService.value = data
    })

    const validateNameService = () => {
      if (!cleannessService.value.nombreServicio) {
        validateState()
        return 'Este campo es requerido'
      }

      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(cleannessService.value.nombreServicio)) {
        NameServiceState.value = false
        return 'Este campo solo puede contener letras'
      }

      //   if (!cleannessService.value.nombreServicio.trim().length > 0) {
      //     NameServiceState.value = false
      //     return 'Este campo no puede contener espacios'
      //   }

      validateState()
      return true
    }

    const validatePublicEstablishment = () => {
      if (!cleannessService.value.establecimientoPublico) {
        validateState()
        return 'Este campo es requerido'
      }

      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(cleannessService.value.establecimientoPublico)) {
        PublicEstablishmentState.value = false
        return 'Este campo solo puede contener numeros'
      }

      //   if (!cleannessService.value.establecimientoPublico.trim().length > 0) {
      //     PublicEstablishmentState.value = false
      //     return 'Este campo no puede contener espacios'
      //   }

      validateState()
      return true
    }

    const validateDomicile = () => {
      if (!cleannessService.value.domicilio) {
        validateState()
        return 'Este campo es requerido'
      }

      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(cleannessService.value.domicilio)) {
        DomicileState.value = false
        return 'Este campo solo puede contener numeros'
      }

      //   if (!cleannessService.value.domicilio.trim().length > 0) {
      //     DomicileState.value = false
      //     return 'Este campo no puede contener espacios'
      //   }

      validateState()
      return true
    }

    const validateObjective = () => {
      if (!cleannessService.value.objetivo) {
        validateState()
        return 'Este campo es requerido'
      }

      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(cleannessService.value.objetivo)) {
        ObjectiveState.value = false
        return 'Este campo solo puede contener numeros'
      }

      //   if (!cleannessService.value.objetivo.trim().length > 0) {
      //     ObjectiveState.value = false
      //     return 'Este campo no puede contener espacios'
      //   }

      validateState()
      return true
    }

    const validateState = () => {
      // eslint-disable-next-line no-unneeded-ternary
      NameServiceState.value = cleannessService.value.nombreServicio === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      PublicEstablishmentState.value = cleannessService.value.establecimientoPublico === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      DomicileState.value = cleannessService.value.domicilio === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      ObjectiveState.value = cleannessService.value.objetivo === '' ? false : true
    }

    return {
      cleannessService,
      breadcrumbItems,
      NameServiceState,
      PublicEstablishmentState,
      DomicileState,
      ObjectiveState,
      //   router

      onUpdateCleannessService,
      validateState,
      validateNameService,
      validatePublicEstablishment,
      validateDomicile,
      validateObjective
    }
  }
}
</script>

<style>

</style>
