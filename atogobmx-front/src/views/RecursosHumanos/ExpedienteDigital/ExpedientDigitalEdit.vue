<template>
  <b-card class="m-3">
    <abbr title="Cambiar Foto de perfil">
      <b-img
        :src="`https://localhost:7065/api/Archivos/FotoPerfil/${expedienteDigitalId}`"
        fluid
        width="150"
        height="150"
        rounded="circle"
        block
        style="cursor: pointer"
        v-b-modal.modal-profilePhoto
      >
        <!-- @click="change" -->
      </b-img>
    </abbr>
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
        <label for="formFile" class="form-label">
          Selecciona una imagen
          </label>
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
    const change = () => {
      alert('Holi')
    }
    return {
      expedienteDigitalId,
      change
    }
  }
}
</script>

<style>
</style>
