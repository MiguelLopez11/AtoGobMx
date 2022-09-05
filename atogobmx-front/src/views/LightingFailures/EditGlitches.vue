<template>
  <b-card class="m-3">
    <b-card
      class="mb-4"
    >
        <b-breadcrumb
          class="p-0"
          :items="breadcrumbItems"
        >
        </b-breadcrumb>
    </b-card>
    <b-card>

    <div>
        <h3>Editar empleado</h3>
    </div>
    <form ref="form">
        <b-row cols="3">
          <b-col>
            <b-form-group class="mt-3" label="NombreFalla">
              <b-form-input
                required
                v-model="lightingFailure.nombreFalla"
              ></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Descripcion">
              <b-form-input
                required
                v-model="lightingFailure.descripcion"
              ></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Fecha">
              <Datepicker
                locale="es"
                :enableTimePicker="false"
                autoApply
                v-model="lightingFailure.fecha"
              ></Datepicker>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Descripcion Domicilio">
              <b-form-input
                required
                v-model="lightingFailure.descripcionDomicilio"
              ></b-form-input>
            </b-form-group>
          </b-col>
        </b-row>
      </form>
      <b-row align-h="end">
          <b-button class="col-1 m-2 text-white" variant="primary" to="/FallasAlumbrado/list" type="reset">Cancelar</b-button>
          <b-button class="col-1 m-2" variant="success" @click="onUpdateLightingFailures()">Guardar</b-button>
      </b-row>
    </b-card>
  </b-card>
</template>

<script>
import LightingfailuresServices from '@/Services/lightingfailures.Services'
// import AreaServices from '@/Services/area.Services'
import { ref } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import Datepicker from '@vuepic/vue-datepicker'
import { useToast } from 'vue-toast-notification'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    Datepicker
  },
  setup () {
    const { getLightingFailures, updateLightingFailures } = LightingfailuresServices()
    // const { getAreas } = AreaServices()
    const $toast = useToast()
    const lightingFailure = ref([])
    // const areas = ref([])
    const router = useRoute()
    const route = useRouter()
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'FallasAlumbrado', to: '/FallasAlumbrado/list' },
      { text: 'Editar-Fallas' }
    ])
    getLightingFailures(router.params.FallaId, data => {
      lightingFailure.value = data[0]
    })
    const onUpdateLightingFailures = () => {
      updateLightingFailures(lightingFailure.value, data => {
      })
      $toast.success('Falla actualizada correctamente.', {
        position: 'top-right',
        duration: 1500
      })
      route.push('/FallasAlumbrado/list').then(getLightingFailures)
    }
    // getAreas((data) => {
    //   areas.value = data
    // })

    return {
      lightingFailure,
      breadcrumbItems,
      router,

      onUpdateLightingFailures
    }
  }

}
</script>

<style>

</style>
