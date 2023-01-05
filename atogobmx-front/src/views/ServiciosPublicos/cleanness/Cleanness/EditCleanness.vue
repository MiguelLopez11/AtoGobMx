<template>
  <b-card class="m-2">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-tabs content-class="mt-3">
      <b-tab title="Editar Direccion Cementerio" active>
        <b-card>
          <Form @submit="onUpdateCleannessService">
            <b-row cols="2">
              <!--Agregar nombre -->
              <b-col>
                <b-form-group class="mt-3" label="Nombre de quien reporta: ">
                  <Field name="NameField" :rules="validateName" as="text">
                    <b-form-input
                      v-model="cleannessService.nombre"
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
              <!--Agregar problema -->
              <b-col>
                <b-form-group class="mt-3" label="Problema">
                  <Field name="ProblemField" :rules="validateProblem" as="text">
                    <b-form-input
                      v-model="cleannessService.problema"
                      :state="ProblemState"
                    >
                    </b-form-input>
                  </Field>
                  <ErrorMessage
                    class="text-danger"
                    name="ProblemField"
                  ></ErrorMessage>
                </b-form-group>
              </b-col>
              <b-col>
                <b-form-group class="mt-3" label="Domicilio">
                  <Field
                    name="DomicileField"
                    :rules="validateDomicile"
                    as="text"
                  >
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
              <!-- Agregar descripcion -->
              <b-col>
                <b-form-group class="mt-3" label="Descripcion">
                    <b-form-textarea
                      v-model="cleannessService.descripcion"
                      rows="4"
                    ></b-form-textarea>
                </b-form-group>
              </b-col>
            </b-row>
            <b-row align-h="end">
              <b-button
                class="col-1 m-2 text-white"
                variant="primary"
                to="/ServiciosPublicos/Aseo/list"
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
      </b-tab>
      <b-tab title="Documentos">
        <ExpedientDocumentsCleanness :AseoId="aseoId" />
      </b-tab>
    </b-tabs>
  </b-card>
</template>

<script>
import CleannessService from '@/Services/cleanness.Services'
import ExpedientDocumentsCleanness from './DocumentsCleanness.vue'
import { ref, inject } from 'vue'
import { useRoute, useRouter } from 'vue-router'
// import { useToast } from 'vue-toast-notification'
import { Form, Field, ErrorMessage } from 'vee-validate'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    Form,
    Field,
    ErrorMessage,
    ExpedientDocumentsCleanness
  },
  setup () {
    const swal = inject('$swal')
    const { getCleannessById, updateCleanness } = CleannessService()
    // const $toast = useToast()
    const cleannessService = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const NameState = ref(false)
    const ProblemState = ref(false)
    const PublicEstablishmentState = ref(false)
    const DomicileState = ref(false)
    const aseoId = ref(parseInt(router.params.AseoId))
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Aseo publico', to: '/ServiciosPublicos/Aseo/list' },
      { text: 'Editar-aseo' }
    ])
    const onUpdateCleannessService = () => {
      updateCleanness(cleannessService.value, data => {})
      swal
        .fire({
          title: '¡Aseo modificado correctamente!',
          text: 'El aseo se ha modificado  satisfactoriamente.',
          icon: 'success'
        })
        .then(result => {
          if (result.isConfirmed) {
            redirect.push('/ServiciosPublicos/Aseo/list')
          }
        })
    }
    getCleannessById(router.params.AseoId, data => {
      cleannessService.value = data
    })

    const validateName = () => {
      if (!cleannessService.value.nombre) {
        validateState()
        return 'Este campo es requerido'
      }

      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(cleannessService.value.nombre)) {
        NameState.value = false
        return 'Este campo solo puede contener letras'
      }

      //   if (!cleannessService.value.nombreServicio.trim().length > 0) {
      //     NameServiceState.value = false
      //     return 'Este campo no puede contener espacios'
      //   }

      validateState()
      return true
    }

    const validateProblem = () => {
      if (!cleannessService.value.establecimientoPublico) {
        validateState()
        return 'Este campo es requerido'
      }

      if (
        !/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(
          cleannessService.value.establecimientoPublico
        )
      ) {
        ProblemState.value = false
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

      if (!cleannessService.value.domicilio.trim().length > 0) {
        DomicileState.value = false
        return 'Este campo no puede contener espacios'
      }

      validateState()
      return true
    }

    const validateState = () => {
      NameState.value = cleannessService.value.nombre !== ''
      ProblemState.value = cleannessService.value.establecimientoPublico !== ''
      DomicileState.value = cleannessService.value.domicilio !== ''
    }

    return {
      cleannessService,
      breadcrumbItems,
      NameState,
      ProblemState,
      PublicEstablishmentState,
      DomicileState,
      aseoId,
      //   router

      onUpdateCleannessService,
      validateState,
      validateName,
      validateProblem,
      validateDomicile
    }
  }
}
</script>

<style></style>
