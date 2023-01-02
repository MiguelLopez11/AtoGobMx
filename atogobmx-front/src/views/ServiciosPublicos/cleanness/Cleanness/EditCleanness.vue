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
                <b-form-group class="mt-3" label="Nombre de quie reporta: ">
                  <Field name="NameField" :rules="validateName" as="text">
                    <b-form-input
                      v-model="cleannessServiceFields.nombre"
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
              <!--Agregar Establecimiento publico -->
              <b-col>
                <b-form-group class="mt-3" label="Establecimiento publico">
                  <Field
                    name="PublicEstablishmentField"
                    :rules="validatePublicEstablishment"
                    as="text"
                  >
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
              <!-- Agregar Domicilio -->
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
              <!-- Agregar Objetivo -->
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
import ExpedientDocumentsCleanness from '@/views/ServiciosPublicos/cleanness/Cleanness/DocumentsCleanness.vue'
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
    const PublicEstablishmentState = ref(false)
    const DomicileState = ref(false)
    const ObjectiveState = ref(false)
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

      if (
        !/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(cleannessService.value.nombre)
      ) {
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

    const validatePublicEstablishment = () => {
      if (!cleannessService.value.establecimientoPublico) {
        validateState()
        return 'Este campo es requerido'
      }

      if (
        !/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(
          cleannessService.value.establecimientoPublico
        )
      ) {
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
      NameState.value =
        cleannessService.value.nombre === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      PublicEstablishmentState.value =
        cleannessService.value.establecimientoPublico === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      DomicileState.value =
        cleannessService.value.domicilio === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      ObjectiveState.value =
        cleannessService.value.objetivo === '' ? false : true
    }

    return {
      cleannessService,
      breadcrumbItems,
      NameState,
      PublicEstablishmentState,
      DomicileState,
      aseoId,
      ObjectiveState,
      //   router

      onUpdateCleannessService,
      validateState,
      validateName,
      validatePublicEstablishment,
      validateDomicile,
      validateObjective
    }
  }
}
</script>

<style></style>
