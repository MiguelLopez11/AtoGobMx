<template>
  <b-card class="m-2">
   <Carousel>
    <Slide v-for="index in links.length" :key="index">
      <img class="carousel__item" :src="links[index -1]" alt="">
    </Slide>

    <template #addons>
      <Navigation />
      <Pagination />
    </template>
  </Carousel>
  </b-card>
</template>

<script>
import { ref } from 'vue'
import { Carousel, Navigation, Pagination, Slide } from 'vue3-carousel'

import 'vue3-carousel/dist/carousel.css'
export default {
  components: {
    Carousel,
    Slide,
    Navigation,
    Pagination
  },
  setup () {
    const currentSlide = ref(0)
    const links = ref(['https://www.atotonilco.gob.mx/templates/it_headlines/custom/images/logo_h.png'])
    const slideTo = (val) => {
      currentSlide.value = val
    }
    return {
      links,
      currentSlide,
      slideTo
    }
  }
}
</script>

<style scoped>
.carousel__item {
  min-height: 50%;
  max-height: 100%;
  width: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
}

.carousel__slide {
  padding: 10px;
}

.carousel__prev,
.carousel__next {
  box-sizing: content-box;
  border: 5px solid white;
}
</style>
