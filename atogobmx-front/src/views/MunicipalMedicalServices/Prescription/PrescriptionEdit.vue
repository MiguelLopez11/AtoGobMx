<template>
  <b-card class="m-3">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
      <b-row cols="1">
        <b-col>
          <b-form-group style="float: " label="Diagnostico">
            <b-form-textarea
              placeholder="Diagnostico"
              rows="4"
              v-model="prescription.diagnostico"
            />
          </b-form-group>
        </b-col>
      </b-row>
      <ProductPrescription :RecetaId="prescriptionId" />
      <b-row align-h="end">
        <b-button
          class="w-auto m-2 text-white"
          variant="primary"
          to="/ServiciosMedicos/Receta/list"
        >
          Cancelar
        </b-button>
        <b-button class="w-auto m-2" variant="success" type="submit" @click="onUpdatePrescription">
          Guardar
        </b-button>
      </b-row>
  </b-card>
</template>

<script>
import PrescriptionServices from '@/Services/prescription.Services'
import ProductPrescription from '@/views/MunicipalMedicalServices/Prescription/ProductPrescription/ProductPrescription.vue'
import { ref, inject } from 'vue'
import { useRoute, useRouter } from 'vue-router'
export default {
  components: {
    ProductPrescription
  },
  setup () {
    const { getPrescription, updatePrescription } = PrescriptionServices()
    const swal = inject('$swal')
    const prescription = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const prescriptionId = ref(parseInt(router.params.RecetaId))
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Recetas', to: '/ServiciosMedicos/Receta/list' },
      { text: 'Editar Receta' }
    ])

    const onUpdatePrescription = () => {
      updatePrescription(prescription.value, data => {})
      swal
        .fire({
          title: 'Receta modificada correctamente!',
          text: 'La receta se ha modificado al sistema satisfactoriamente.',
          icon: 'success'
        })
        .then(result => {
          if (result.isConfirmed) {
            redirect.push('/ServiciosMedicos/Receta/list')
          }
        })
    }
    getPrescription(prescriptionId.value, data => {
      prescription.value = data
    })
    return {
      prescription,
      prescriptionId,
      breadcrumbItems,
      onUpdatePrescription
    }
  }
}
</script>

<style></style>
