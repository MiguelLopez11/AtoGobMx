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
        <h3>Editar Area de trabajo</h3>
    </div>
    <form ref="form">
        <b-row cols="3">
          <b-col>
            <b-form-group class="mt-3" label="Nombre de area">
              <b-form-input
              v-model="area.nombre"
              />
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Descripción">
              <b-form-input v-model="area.descripcion" />
            </b-form-group>
          </b-col>
        </b-row>
      </form>
      <b-row align-h="end">
          <b-button class="col-1 m-2 text-white" variant="primary" to="/Empleados/list" type="reset">Cancelar</b-button>
          <b-button class="col-1 m-2" variant="success" @click="onUpdateArea()">Guardar</b-button>
      </b-row>
    </b-card>
  </b-card>
</template>

<script>
import AreaServices from '@/Services/area.Services'
import { ref } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { useToast } from 'vue-toast-notification'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
  },
  setup () {
    const { getArea, updateArea } = AreaServices()
    const $toast = useToast()
    const employee = ref([])
    const area = ref([])
    const router = useRoute()
    const route = useRouter()
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Areas', to: '/Areas/list' },
      { text: 'Editar-Area' }
    ])
    // getEmployee(router.params.EmpleadoId, data => {
    //   employee.value = data[0]
    // })
    const onUpdateArea = () => {
      updateArea(area.value, data => {
      })
      $toast.success('Empleado actualizado correctamente.', {
        position: 'top-right',
        duration: 1500
      })
      route.push('/Areas/list').then(getArea)
    }
    getArea(router.params.AreaId, data => {
      area.value = data
    })

    return {
      employee,
      area,
      breadcrumbItems,
      //   router

      onUpdateArea
    }
  }

}
</script>

<style>

</style>
