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
          <form ref="form">
            <b-row cols="2">
              <b-col>
                <b-form-group class="mt-3" label="Estado">
                  <b-form-input />
                </b-form-group>
              </b-col>
              <b-col>
                <b-form-group class="mt-3" label="Municipio">
                  <b-form-input />
                </b-form-group>
              </b-col>
              <b-col>
                <b-form-group class="mt-3" label="Localidad">
                  <b-form-input />
                </b-form-group>
              </b-col>
              <b-col>
                <b-form-group class="mt-3" label="Calle">
                  <b-form-input />
                </b-form-group>
              </b-col>
              <b-col>
                <b-form-group class="mt-3" label="Numero exterior">
                  <b-form-input />
                </b-form-group>
              </b-col>
              <b-col>
                <b-form-group class="mt-3" label="Numero interior">
                  <b-form-input />
                </b-form-group>
              </b-col>
              <b-col>
                <b-form-group class="mt-3" label="Codigo postal">
                  <b-form-input />
                </b-form-group>
              </b-col>
              <b-col>
                <b-form-group class="mt-3" label="Correo electronico">
                  <b-form-input />
                </b-form-group>
              </b-col>
            </b-row>
          </form>
        </b-tab>
        <b-tab title="Expediente">
          <span>
            En proceso¡
          </span>
        </b-tab>
      </b-tabs>
    </b-card>
  </b-card>
  <!-- @ok="addEmployee" -->
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
import { useRoute } from 'vue-router'
import { useToast } from 'vue-toast-notification'
import FileServices from '@/Services/file.Services'
export default {
  setup () {
    const { createExpedientPhotoProfile } = FileServices()
    const refFile = ref()
    const refAvatar = ref()
    const router = useRoute()
    const $toast = useToast()
    const expedienteDigitalId = ref(router.params.ExpedienteDigitalId)
    const fileState = ref(false)
    const file = ref('')
    const onChangeFile = e => {
      file.value = refFile.value.files[0]
    }
    const submitPhoto = () => {
      const formData = new FormData()
      formData.append('file', file.value)
      if (!file.value) {
        $toast.open({
          message:
            'No se ha seleccionado una imagen, por favor ingrese una imagen de perfil.',
          position: 'top-left',
          duration: 0,
          dismissible: true,
          type: 'error'
        })
      }
      createExpedientPhotoProfile(
        router.params.ExpedienteDigitalId,
        formData,
        data => {
          $toast.open({
            message: `${data.data}`,
            position: 'top-left',
            duration: 0,
            dismissible: true,
            type: 'error'
          })
        }
      )
    }
    return {
      expedienteDigitalId,
      fileState,
      refFile,
      file,
      refAvatar,

      onChangeFile,
      submitPhoto
    }
  }
}
</script>

<style></style>
