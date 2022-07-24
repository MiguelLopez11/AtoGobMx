<template>
  <div class="row justify-content-center mt-5">
    <div class="col-9">
      <b-table-simple responsive>
        <colgroup>
          <col />
          <col />
        </colgroup>
        <b-thead head-variant="white">
          <b-tr>
            <b-th scope="col">ID</b-th>
            <b-th scope="col">Nombre</b-th>
            <b-th scope="col">Apellido Paterno</b-th>
            <b-th scope="col">Apellido Materno</b-th>
            <b-th scope="col">Acciones</b-th>
          </b-tr>
        </b-thead>
        <b-tbody v-for="data in employees" :key="data">
          <b-tr>
            <b-td scope="row">{{ data.empleadoId }}</b-td>
            <b-td>{{ data.nombre }}</b-td>
            <b-td scope="row">{{ data.apellidoPaterno }}</b-td>
            <b-td>{{ data.apellidoMaterno }}</b-td>
            <b-td>
              <div class="col-6">
                <b-button variant="primary" class="m-1">
                  <i class="bi bi-file-earmark-person">Expediente</i>
                </b-button>
                <b-button variant="danger">
                  <i class="bi bi-trash"></i>
                </b-button>
              </div>
              <div></div>
            </b-td>
          </b-tr>
        </b-tbody>
      </b-table-simple>
    </div>
  </div>
</template>

<script>
import EmployeeServices from '@/Services/employee.Services'
import { ref } from 'vue'
// import { BButton } from 'bootstrap-vue'
export default {
  components: {
    // BButton
  },
  setup () {
    const { getEmployees } = EmployeeServices()
    const employees = ref([])
    getEmployees((data) => {
      employees.value = data
      console.log(employees.value)
    })
    return {
      employees
    }
  }
}
</script>

<style lang="scss" scoped>
</style>
