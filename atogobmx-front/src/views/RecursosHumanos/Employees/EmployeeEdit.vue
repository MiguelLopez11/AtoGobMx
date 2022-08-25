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
            <b-form-group class="mt-3" label="Nombre">
              <b-form-input
                v-model="employee.nombre"
                required
              ></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Apellido Paterno">
              <b-form-input
                v-model="employee.apellidoPaterno"
                required
              ></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Apellido Materno">
              <b-form-input
                v-model="employee.apellidoMaterno"
                required
              ></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Fecha de nacimiento">
              <Datepicker
                locale="es"
                :enableTimePicker="false"
                autoApply
                v-model="employee.fechaNacimiento"
              ></Datepicker>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Area">
              <b-form-select
                autofocus
                :options="areas"
                value-field="areaId"
                text-field="nombre"
                v-model="employee.areaId"
              ></b-form-select>
            </b-form-group>
          </b-col>
        </b-row>
      </form>
      <b-row align-h="end">
          <b-button class="col-1 m-2 text-white" variant="primary" to="/Empleados/list" type="reset">Cancelar</b-button>
          <b-button class="col-1 m-2" variant="success" @click="onUpdateEmployee()">Guardar</b-button>
      </b-row>
    </b-card>
  </b-card>
</template>

<script>
import EmployeeServices from '@/Services/employee.Services'
import AreaServices from '@/Services/area.Services'
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
    const { getEmployee, updateEmployee } = EmployeeServices()
    const { getAreas } = AreaServices()
    const $toast = useToast()
    const employee = ref([])
    const areas = ref([])
    const router = useRoute()
    const route = useRouter()
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Empleados', to: '/Empleados/list' },
      { text: 'Editar-Empleados' }
    ])
    getEmployee(router.params.EmpleadoId, data => {
      employee.value = data[0]
    })
    const onUpdateEmployee = () => {
      updateEmployee(employee.value, data => {
        route.push('/Empleados/list')
        $toast.success('Empleado actualizado correctamente.', {
          position: 'top-right',
          duration: 10,
          dismissible: false
        })
      })
    }
    getAreas((data) => {
      areas.value = data
    })

    return {
      employee,
      areas,
      breadcrumbItems,
      router,

      onUpdateEmployee
    }
  }

}
</script>

<style>

</style>
