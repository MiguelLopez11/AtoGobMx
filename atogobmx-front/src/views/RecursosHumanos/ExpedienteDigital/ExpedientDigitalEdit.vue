<template>
  <b-card class="m-3">
    <abbr title="Cambiar Foto de perfil">
      <b-avatar
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
    hide-footer
  >
    <div class="input-group mb-3">
      <input type="file" class="form-control" list v-on:change="onChangeFile" :dropzone="true" :draggable="true" />
      <b-button variant="outline-primary">Cargar imagen</b-button>
    </div>
  </b-modal>
</template>

<script>
import { ref } from 'vue'
import { useRoute } from 'vue-router'
import FileServices from '@/Services/file.Services'
export default {
  setup () {
    const { createExpedientPhotoProfile } = FileServices()
    const router = useRoute()
    const expedienteDigitalId = ref(router.params.ExpedienteDigitalId)
    const fileState = ref(false)
    const onChangeFile = e => {
      const files = e.target.files || e.dataTransfer.files
      console.log(files)
      createExpedientPhotoProfile(router.params.ExpedienteDigitalId, files[0], data => {
      })
    }
    return {
      expedienteDigitalId,
      fileState,

      onChangeFile
    }
  }
}
</script>

<style></style>
