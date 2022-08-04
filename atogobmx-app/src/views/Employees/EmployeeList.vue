<template>
  <b-card class="m-3">
    <b-container fluid class="text-light text-center">
      <b-row>
        <b-col align-self="start" cols="2">
          <v-select
            style="color: black"
            v-model="perPage"
            :value="perPage"
            :options="perPageSelect"
          >
          </v-select>
        </b-col>
        <b-col align-self="end" cols="3">
          <b-form-input
            inline
            v-model="filter"
            type="search"
            placeholder="Buscar..."
          >
          </b-form-input>
        </b-col>
        <b-col align-self="end" cols="auto">
          <b-button variant="success"> Agregar </b-button>
        </b-col>
      </b-row>
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
    </b-container>
  </b-card>
</template>

<script>
import "vue-select/dist/vue-select.css";

import EmployeeServices from "@/Services/employee.Services";
import { ref } from "vue";
export default {
  setup() {
    const { getEmployees } = EmployeeServices();
    const employees = ref([]);
    const perPage = ref(10);
    const currentPage = ref(1);
    const rows = ref(null);
    const filter = ref(null);
    const perPageSelect = ref([10, 25, 50, 100]);
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
      perPageSelect,

      onFiltered,
    };
  },
};
</script>

<style lang="scss" scoped></style>
