<template>
  <b-card class="m-2">
    <abbr title="Cambiar Foto de perfil">
      <b-avatar
        ref="refAvatar"
        :src="
          `https://localhost:7065/api/Archivos/FotoPerfil/${expedienteDigitalId}`
        "
        size="170px"
        style="cursor: pointer; margin-bottom: 30px"
        badge-variant="light"
        alt=""
      >
        <template #badge>
          <i @click="showModal = !showModal" class="bi bi-pencil-square" />
        </template>
      </b-avatar>
    </abbr>
    <b-card>
      <b-tabs content-class="mt-3">
        <b-tab title="Datos personales" active>
          <b-card>
            <Form @submit="submitExpedient">
              <b-row cols="4">
                <b-col>
                  <b-form-group class="mt-3" label="Estado">
                    <Field name="StateField" :rules="validateStateField" as="text">
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
                  <b-form-group class="mt-3" label="Municipio" >
                    <Field
                      name="municipalityField"
                      :rules="validateMunicipality"
                      as="text"
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
                    <Field name="streetField" :rules="validateStreet" as="text">
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
                      as="text"
                    >
                      <b-form-input
                        v-model="expedient.numeroExterior"
                        :state="exteriorNumberState"
                        type="number"
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
                    <b-form-input  type="number" v-model="expedient.numeroInterior" />
                  </b-form-group>
                </b-col>
                <b-col>
                  <b-form-group class="mt-3" label="Codigo postal">
                    <Field name="postalCodeField" :rules="validatePostalCode" as="text">
                      <b-form-input
                        v-model="expedient.codigoPostal"
                        :state="postalCodeState"
                        type="number"
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
                      as="text"
                    >
                      <b-form-input
                        v-model="expedient.correoElectronico"
                        :state="emailState"
                      />
                    </Field>
                    <ErrorMessage name="emailField">
                      <span class="text-danger">{{ emailMessage }} </span>
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
          <ExpedientDocuments
            :ExpedientDigitalId="expedienteDigitalId"
          />
        </b-tab>
      </b-tabs>
    </b-card>
  </b-card>
  <b-modal
    id="modal-expedient"
    v-model="showModal"
    title="Imagen de Perfil"
    size="xl"
    centered
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
import { ref, inject } from 'vue'
import { useRoute, useRouter } from 'vue-router'
// import { useToast } from 'vue-toast-notification'
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
    const swal = inject('$swal')
    const { createExpedientPhotoProfile } = FileServices()
    const { getExpedient, updateExpedient } = ExpedientdigitalServices()
    const refFile = ref()
    const refAvatar = ref()
    const router = useRoute()
    // const $toast = useToast()
    const redirect = useRouter()
    const showModal = ref(false)
    const expedient = ref([])
    const expedienteDigitalId = ref(parseInt(router.params.ExpedienteDigitalId))
    const EstadoState = ref(false)
    const municipalityState = ref(false)
    const streetState = ref(false)
    const exteriorNumberState = ref(false)
    const postalCodeState = ref(false)
    const emailState = ref(false)
    const emailMessage = ref('')
    const file = ref()
    // Methods
    getExpedient(expedienteDigitalId.value, data => {
      expedient.value = data
      emailState.value = data.correoElectronico !== null
      emailMessage.value = 'Este campo es requerido '
      validateState()
    })
    const onChangeFile = () => {
      file.value = refFile.value.files[0]
    }
    const submitExpedient = () => {
      updateExpedient(expedient.value, data => {
        swal.fire({
          title: '¡Expediente modificado correctamente!',
          text: 'El expediente se ha modificado correctamente.',
          icon: 'success'
        }).then(result => {
          redirect.push('/ExpedientesDigitales/list')
        })
        // $toast.open({
        //   message: 'Expediente modificado correctamente',
        //   position: 'top',
        //   duration: 2000,
        //   dismissible: true,
        //   type: 'success',
        //   onDismiss: () => redirect.push('/ExpedientesDigitales/list')
        // })
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
        showModal.value = false
        swal.fire({
          title: 'No se ha seleccionado una imagen!',
          text: 'No se ha seleccionado una imagen, por favor ingrese una imagen de perfil.',
          icon: 'warning'
        })
        return ''
      }
      createExpedientPhotoProfile(
        router.params.ExpedienteDigitalId,
        formData,
        data => {
          // const icon = ref('success')
          if (data.type) {
            showModal.value = false
            swal.fire({
              title: '¡El archivo no es válido!',
              text: 'El archivo ingresado no es válido, por favor ingresa un archivo valido.',
              icon: 'error'
            }).then(result => {
              redirect.push('/ExpedientesDigitales/list')
            })
          } else {
            showModal.value = false
            swal.fire({
              title: 'Imagen de perfil registrado correctamente!',
              text: 'La imagen de perfil se ha registrado al sistema satisfactoriamente.',
              icon: 'success'
            }).then(result => {
              redirect.push('/ExpedientesDigitales/list')
            })
          }
        })
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
      showModal,
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

<style>
</style>
