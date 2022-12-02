<template>
  <b-card class="m-3">
    <ProductPrescription :RecetaId="1"/>
  </b-card>
</template>

<script>
import ProductPrescription from '@/views/MunicipalMedicalServices/Prescription/ProductPrescription/ProductPrescription.vue'
export default {
  components: {
    ProductPrescription
  },
  setup () {
    return {

    }
  }
}
</script>

<style>

</style>
