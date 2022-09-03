<template>
  <b-card class="m-3">
    <abbr title="Cambiar Foto de perfil">
      <b-avatar
        :src="`https://localhost:7065/api/Archivos/FotoPerfil/${expedienteDigitalId}`"
        size="200px"
        style="cursor: pointer; margin-bottom: 30px"
        v-b-modal.modal-profilePhoto
        badge-variant="light"
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
        <b-tab title="Usuario">
          <span>
            En proceso¡
          </span>
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
    button-size="lg"
    lazy
    ok-title="Guardar"
    cancel-title="Cancelar"
  >
    <form ref="form">
      <div class="mb-3">
        <label for="formFile" class="form-label"> Selecciona una imagen </label>
        <input class="form-control w-75" type="file" id="formFile" />
      </div>
    </form>
  </b-modal>
</template>

<script>
import { ref } from 'vue'
// import FileServices from '@/Services/file.Services'
import { useRoute } from 'vue-router'
export default {
  setup () {
    // const {} = FileServices()
    const router = useRoute()
    const expedienteDigitalId = ref(router.params.ExpedienteDigitalId)
    return {
      expedienteDigitalId
    }
  }
}
</script>

<style>
</style>
