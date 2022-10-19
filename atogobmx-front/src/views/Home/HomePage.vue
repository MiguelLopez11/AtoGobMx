<template>
  <b-card class="m-3">
    <b-card
      align="center"
      class="m-3"
      :img-src="background"
      no-body
      style="max-width: 200rem"
    >
    {{ userData }}
    </b-card>
  </b-card>
</template>

<script>
import { ref } from 'vue'
import LogoAtogobMx from '@/Images/LogoAtogobMx.jpg'
// import { useRoute } from 'vue-router'
export default {
  setup () {
    // const router = useRoute()
    const background = ref(LogoAtogobMx)
    const userData = window.localStorage.getItem('User')
    // console.log(userData.value)
    return {
      background,
      userData
    }
  }
}
</script>

<style lang="scss" scoped></style>
