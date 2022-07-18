<template>
    <div>
        <h1>Empleados:</h1>
    </div>
    <div>
        <table class="table">
            <thead class="thead-dark">
                <tr>
                    <th scope="col">ID</th>
                    <th scope="col">Nombre</th>
                    <th scope="col">Apellido Paterno</th>
                    <th scope="col">Apellido Materno</th>
                    <th scope="col">Acciones</th>
                </tr>
            </thead>
            <tbody  v-for="data in employees" :key="data">
                <tr>
                    <td scope="row">{{data.idEmpleado}}</td>
                    <td>{{data.nombre}}</td>
                    <td scope="row">{{data.apellidoPaterno}}</td>
                    <td>{{data.apellidoMaterno}}</td>
                    <td>
                        <button class="btn btn-primary m-2 ">Editar</button>
                        <button class="btn btn-danger m-2">Eliminar</button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script>
import EmployeeServices from '@/Services/employee.Services'
import { ref } from 'vue'
export default {
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
