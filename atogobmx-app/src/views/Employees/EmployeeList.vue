<template>
  <b-card class="m-3">
    <div class="col-3 row form-inline">
      <b-form-input
        inline
        v-model="filter"
        type="search"
        placeholder="Buscar..."
      >
      </b-form-input>
    </div>
    <b-table
      id="employeeTable"
      :items="employees"
      :fields="fields"
      :filter="filter"
      :per-page="perPage"
      :current-page="currentPage"
      @filtered="onFiltered"
    >
    </b-table>
    <b-pagination
      v-model="currentPage"
      :total-rows="rows"
      :per-page="perPage"
      aria-controls="employeeTable"
      align="right"
    >
    </b-pagination>
    <!-- <div class="row justify-content-center mt-5">
    <div class="col-12">
      <b-table-simple
        hover
        outlined
        caption-top
      >
        <b-thead head-variant="Light">
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
              <div>
                <b-dropdown
                  text="Acciones"
                  dropright
                  offset="-50"
                  variant="primary"
                >
                  <b-dropdown-item>First Action</b-dropdown-item>
                  <b-dropdown-item variant="primary">Second Action</b-dropdown-item>
                  <b-dropdown-item active>Active action</b-dropdown-item>
                  <b-dropdown-item disabled>Disabled action</b-dropdown-item>
                  <b-dropdown-item href="Badge">Badge</b-dropdown-item>
                </b-dropdown>
              </div>
            </b-td>
          </b-tr>
        </b-tbody>
      </b-table-simple>
    </div>
  </div> -->
  </b-card>
</template>

<script>
import EmployeeServices from "@/Services/employee.Services";
import { ref } from "vue";
export default {
  setup() {
    const { getEmployees } = EmployeeServices();
    const employees = ref([]);
    const perPage = ref(5);
    const currentPage = ref(1);
    const rows = ref(null);
    const filter = ref(null);
    const fields = ref([
      { key: "empleadoId", label: "ID" },
      { key: "nombre", label: "Nombre" },
      { key: "apellidoPaterno", label: "Apellido Paterno" },
      { key: "apellidoPaterno", label: "Apellido Materno" },
      { key: "area.nombre", label: "Area de Trabajo" },
      { key: "Acciones", label: "Acciones" },
    ]);
    getEmployees((data) => {
      employees.value = data;
      rows.value = data.length;
    });
    // onMounted(() => {
    //   rows.value = employees.value.length;
    // });
    const onFiltered = (filteredItems) => {
      rows.value = filteredItems.length;
      currentPage.value = 1;
    };
    return {
      employees,
      fields,
      perPage,
      currentPage,
      rows,
      filter,

      onFiltered,
    };
  },
};
</script>

<style lang="scss" scoped></style>
