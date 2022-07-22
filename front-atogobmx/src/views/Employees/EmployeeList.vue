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
            <b-td scope="row">{{ data.idEmpleado }}</b-td>
            <b-td>{{ data.nombre }}</b-td>
            <b-td scope="row">{{ data.apellidoPaterno }}</b-td>
            <b-td>{{ data.apellidoMaterno }}</b-td>
            <b-td>
              <div class="row">
                <div class="col-3">
                  <b-button variant="primary">
                    <b-skeleton-icon>
                      <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="white"
                        class="bi bi-file-person-fill" viewBox="0 0 16 16">
                        <path
                          d="M12 0H4a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h8a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2zm-1 7a3 3 0 1 1-6 0 3 3 0 0 1 6 0zm-3 4c2.623 0 4.146.826 5 1.755V14a1 1 0 0 1-1 1H4a1 1 0 0 1-1-1v-1.245C3.854 11.825 5.377 11 8 11z" />
                      </svg>
                    </b-skeleton-icon>
                  </b-button>
                  <b-button variant="danger">
                    <b-skeleton-icon>
                      <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor"
                        class="bi bi-trash-fill" viewBox="0 0 16 16">
                        <path
                          d="M2.5 1a1 1 0 0 0-1 1v1a1 1 0 0 0 1 1H3v9a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V4h.5a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1H10a1 1 0 0 0-1-1H7a1 1 0 0 0-1 1H2.5zm3 4a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 .5-.5zM8 5a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7A.5.5 0 0 1 8 5zm3 .5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 1 0z" />
                      </svg>
                    </b-skeleton-icon>
                  </b-button>
                </div>
                <div>
                </div>
              </div>
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
    getEmployees(data => {
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
