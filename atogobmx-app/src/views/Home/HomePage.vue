<template>
  <b-card class="m-3">
    <b-card
      align="center"
      class="m-3"
      :img-src="background"
      no-body
      style="max-width: 200rem"
    >
      <!-- overlay -->
      <!-- <div>
        <img :src="background" class="img-responsive m-3"
          height="570" width="1100 ">
    </div> -->
    </b-card>
  </b-card>
</template>

<script>
import { ref } from "vue";
import LogoAtogobMx from "@/Images/LogoAtogobMx.jpg";
export default {
  setup() {
    const clicked = ref(false);
    const background = ref(LogoAtogobMx);
    const method = () => {
      clicked.value = true;
    };
    return {
      clicked,
      background,

      method,
    };
  },
};
</script>

<style lang="scss" scoped></style>
