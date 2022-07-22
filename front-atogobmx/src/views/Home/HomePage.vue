<template>
    <div>
        Home Page
    </div>
</template>

<script>
import { ref } from 'vue'
export default {
  setup () {
    const clicked = ref(false)
    const method = () => {
      clicked.value = true
    }
    return {
      clicked,

      method
    }
  }
}
</script>

<style lang="scss" scoped>
</style>
