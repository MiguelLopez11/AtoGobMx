<template>
  <b-card class="m-3">
    <abbr title="Cambiar Foto de perfil">
      <b-avatar
        ref="refAvatar"
        :src="
          `https://localhost:7065/api/Archivos/FotoPerfil/${expedienteDigitalId}`
        "
        size="200px"
        style="cursor: pointer; margin-bottom: 30px"
        v-b-modal.modal-profilePhoto
        badge-variant="light"
        alt=""
      >
        <template #badge>
          <i class="bi bi-pencil-square" />
        </template>
      </b-avatar>
    </abbr>
    <b-card>
      <b-tabs content-class="mt-3">
        <b-tab title="Datos personales" active>
          <b-card>
            <Form @submit="submitExpedient">
              <b-row cols="3">
                <b-col>
                  <b-form-group class="mt-3" label="Estado">
                    <Field name="StateField" :rules="validateStateField">
                      <b-form-input
                        v-model="expedient.estado"
                        :state="EstadoState"
                      />
                    </Field>
                    <ErrorMessage name="StateField">
                      <span class="text-danger">Este campo es requerido </span>
                      <i class="bi bi-exclamation-circle"></i
                    ></ErrorMessage>
                  </b-form-group>
                </b-col>
                <b-col>
                  <b-form-group class="mt-3" label="Municipio">
                    <Field
                      name="municipalityField"
                      :rules="validateMunicipality"
                    >
                      <b-form-input
                        v-model="expedient.municipio"
                        :state="municipalityState"
                      />
                    </Field>
                    <ErrorMessage name="municipalityField">
                      <span class="text-danger">Este campo es requerido </span>
                      <i class="bi bi-exclamation-circle"></i
                    ></ErrorMessage>
                  </b-form-group>
                </b-col>
                <b-col>
                  <b-form-group class="mt-3" label="Localidad">
                    <b-form-input v-model="expedient.localidad" />
                  </b-form-group>
                </b-col>
                <b-col>
                  <b-form-group class="mt-3" label="Calle">
                    <Field name="streetField" :rules="validateStreet">
                      <b-form-input
                        v-model="expedient.calle"
                        :state="streetState"
                      />
                    </Field>
                    <ErrorMessage name="streetField">
                      <span class="text-danger">Este campo es requerido </span>
                      <i class="bi bi-exclamation-circle"></i
                    ></ErrorMessage>
                  </b-form-group>
                </b-col>
                <b-col>
                  <b-form-group class="mt-3" label="Numero exterior">
                    <Field
                      name="numberExteriorField"
                      :rules="validateNumberExterior"
                    >
                      <b-form-input
                        v-model="expedient.numeroExterior"
                        :state="exteriorNumberState"
                      />
                    </Field>
                    <ErrorMessage name="numberExteriorField">
                      <span class="text-danger">Este campo es requerido </span>
                      <i class="bi bi-exclamation-circle"></i
                    ></ErrorMessage>
                  </b-form-group>
                </b-col>
                <b-col>
                  <b-form-group class="mt-3" label="Numero interior">
                    <b-form-input v-model="expedient.numeroInterior" />
                  </b-form-group>
                </b-col>
                <b-col>
                  <b-form-group class="mt-3" label="Codigo postal">
                    <Field name="postalCodeField" :rules="validatePostalCode">
                      <b-form-input
                        v-model="expedient.codigoPostal"
                        :state="postalCodeState"
                      />
                    </Field>
                    <ErrorMessage name="postalCodeField">
                      <span class="text-danger">Este campo es requerido </span>
                      <i class="bi bi-exclamation-circle"></i
                    ></ErrorMessage>
                  </b-form-group>
                </b-col>
                <b-col>
                  <b-form-group class="mt-3" label="Correo electronico">
                    <Field
                      name="emailField"
                      type="email"
                      :rules="validateEmail"
                    >
                      <b-form-input
                        v-model="expedient.correoElectronico"
                        :state="emailState"
                      />
                    </Field>
                    <ErrorMessage name="emailField">
                      <span>{{ emailMessage }} </span>
                      <i class="bi bi-exclamation-circle"></i
                    ></ErrorMessage>
                  </b-form-group>
                </b-col>
              </b-row>
              <b-row align-h="end">
                <b-button
                  class="w-auto m-2 text-white"
                  variant="primary"
                  to="/ExpedientesDigitales/list"
                >
                  Cancelar
                </b-button>
                <b-button class="w-auto m-2" variant="success" type="submit">
                  Guardar
                </b-button>
              </b-row>
            </Form>
          </b-card>
        </b-tab>
        <b-tab title="Documentos">
          <ExpedientDocuments />
        </b-tab>
      </b-tabs>
    </b-card>
  </b-card>
  <b-modal
    id="modal-profilePhoto"
    title="Imagen de Perfil"
    size="xl"
    centered
    hide-backdrop
    hide-footer
  >
    <div class="input-group mb-3">
      <input
        type="file"
        class="form-control"
        v-on:change="onChangeFile"
        ref="refFile"
        id="file"
      />
      <b-button variant="outline-primary" @click="submitPhoto"
        >Cargar imagen</b-button
      >
    </div>
  </b-modal>
</template>

<script>
import { ref } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { useToast } from 'vue-toast-notification'
import FileServices from '@/Services/file.Services'
import ExpedientDocuments from '@/views/RecursosHumanos/ExpedienteDigital/ExpedientDigitalDocuments.vue'
import ExpedientdigitalServices from '@/Services/expedientdigital.Services'
import { Form, Field, ErrorMessage } from 'vee-validate'
export default {
  components: {
    Form,
    Field,
    ErrorMessage,
    ExpedientDocuments
  },
  setup () {
    const { createExpedientPhotoProfile } = FileServices()
    const { getExpedient, updateExpedient } = ExpedientdigitalServices()
    const refFile = ref()
    const refAvatar = ref()
    const router = useRoute()
    const $toast = useToast()
    const redirect = useRouter()
    const expedient = ref([])
    const expedienteDigitalId = ref(router.params.ExpedienteDigitalId)
    const EstadoState = ref(false)
    const municipalityState = ref(false)
    const streetState = ref(false)
    const exteriorNumberState = ref(false)
    const postalCodeState = ref(false)
    const emailState = ref(false)
    const emailMessage = ref('')
    const file = ref('')
    // Methods
    getExpedient(expedienteDigitalId.value, data => {
      expedient.value = data
      emailState.value = data.correoElectronico !== null
      validateState()
    })
    const onChangeFile = e => {
      file.value = refFile.value.files[0]
    }
    const submitExpedient = () => {
      updateExpedient(expedient.value, data => {
        $toast.open({
          message: 'Expediente modificado correctamente',
          position: 'top',
          duration: 2000,
          dismissible: true,
          type: 'success',
          onDismiss: () => redirect.push('/ExpedientesDigitales/list')
        })
      })
    }
    // Validations
    const validateStateField = () => {
      if (!expedient.value.estado) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateMunicipality = () => {
      if (!expedient.value.municipio) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateStreet = () => {
      if (!expedient.value.calle) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateNumberExterior = () => {
      if (!expedient.value.numeroExterior) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validatePostalCode = () => {
      if (!expedient.value.codigoPostal) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateEmail = () => {
      if (!expedient.value.correoElectronico) {
        emailState.value = false
        emailMessage.value = 'Este campo es requerido'
        return emailMessage.value
      }
      const regex = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i
      if (!regex.test(expedient.value.correoElectronico)) {
        emailState.value = false
        emailMessage.value = 'Este campo debe ser un correo electrónico válido'
        return emailMessage.value
      }
      emailState.value = true
      return true
    }
    const validateState = () => {
      EstadoState.value = expedient.value.estado !== null
      municipalityState.value = expedient.value.municipio !== null
      streetState.value = expedient.value.calle !== null
      exteriorNumberState.value = expedient.value.numeroExterior !== null
      postalCodeState.value = expedient.value.codigoPostal !== null
      return ''
    }
    // Cargar Imagen
    const submitPhoto = () => {
      const formData = new FormData()
      formData.append('file', file.value)
      if (!file.value) {
        $toast.open({
          message:
            'No se ha seleccionado una imagen, por favor ingrese una imagen de perfil.',
          position: 'top-left',
          duration: 2000,
          dismissible: true,
          type: 'error'
        })
      } else {
        createExpedientPhotoProfile(
          router.params.ExpedienteDigitalId,
          formData,
          data => {
            $toast.open({
              message: `${data.data}`,
              position: 'top-left',
              duration: 2000,
              dismissible: true,
              type: 'success',
              onDismiss: () => redirect.push('/ExpedientesDigitales/list')
            })
          })
      }
    }
    return {
      expedienteDigitalId,
      EstadoState,
      municipalityState,
      streetState,
      exteriorNumberState,
      postalCodeState,
      emailState,
      refFile,
      file,
      emailMessage,
      expedient,
      refAvatar,

      onChangeFile,
      submitPhoto,
      validateStateField,
      submitExpedient,
      validateState,
      validateMunicipality,
      validateStreet,
      validateNumberExterior,
      validatePostalCode,
      validateEmail
    }
  }
}
</script>

<style></style>
