<template>
<b-card-group>
  <b-card
    class="m-3 w-25"

  >
    <b-img
      :src="`https://localhost:7065/api/Archivos/FotoPerfil/${expedienteDigitalId}`"
      fluid
      width="100"
      height="100"
      rounded="circle"
    >
    </b-img>
    <footer>Sistemas</footer>
  </b-card>
  <b-card class="m-3 w-75"></b-card>
</b-card-group>
</template>

<script>
import { ref } from 'vue'
import FileServices from '@/Services/file.Services'
import { useRoute } from 'vue-router'
export default {
  setup () {
    const { getExpedientPhotoProfile } = FileServices()
    const photoProfile = ref()
    const router = useRoute()
    const expedienteDigitalId = ref(router.params.ExpedienteDigitalId)
    getExpedientPhotoProfile(expedienteDigitalId.value, data => {
      photoProfile.value = data
    })
    return {
      expedienteDigitalId,
      photoProfile
    }
  }
}
</script>

<style>

</style>
