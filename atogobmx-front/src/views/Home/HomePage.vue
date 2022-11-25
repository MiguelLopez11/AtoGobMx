<template>
  <b-card
    no-body
  >

  </b-card>
</template>

<script>
import { ref } from 'vue'
import LogoAtogobMx from '@/Images/LogoAtogobMx.jpg'
// import { useRoute } from 'vue-router'
export default {
  setup () {
    const background = ref(LogoAtogobMx)
    // const userData = window.sessionStorage.getItem('User')
    return {
      background
      // userData
    }
  }
}
</script>

<style scoped>
</style>
