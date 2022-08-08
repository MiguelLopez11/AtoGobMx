<template>
  <b-card class="m-3">
    <div>
      <div>
        <div class="col-1" style="float: right">
          <button class="btn btn-success" v-b-modal.modal-add-employee>
            Agregar
          </button>
        </div>
        <div class="col-3" style="float: right; margin-right: 20px">
          <b-form-input v-model="filter" type="search" placeholder="Buscar...">
          </b-form-input>
        </div>
        <span>Mostrar: </span>
        <div class="form-inline">
          <v-select
            style="width: 90px"
            v-model="perPage"
            :value="perPage"
            :options="perPageSelect"
          >
          </v-select>
          <div></div>
        </div>
      </div>
    </div>
    <b-table
      id="employeeTable"
      hover
      caption-top
      head-row-variant="primary"
      bordered
      show-empty
      empty-text="No se encuentran empleados registrados"
      class="mt-3 mb-3"
      fixed
      ref="refEmployeeTable"
      :items="employees"
      :fields="fields"
      :filter="filter"
      :per-page="perPage"
      :current-page="currentPage"
      @filtered="onFiltered"
    >
      <template #cell(actions)>
        <b-dropdown text="Acciones" variant="primary" dropright>
          <b-dropdown-item variant="danger">
            <i class="bi bi-trash-fill"></i> Eliminar
          </b-dropdown-item>
          <b-dropdown-item>
            <i class="bi bi-pencil-square"></i> Editar
          </b-dropdown-item>
        </b-dropdown>
      </template>
    </b-table>
    <b-pagination
      pills
      v-model="currentPage"
      :total-rows="rows"
      :per-page="perPage"
      aria-controls="employeeTable"
      align="right"
    >
    </b-pagination>
    <b-modal
      id="modal-add-employee"
      ref="modal"
      centered
      no-close-on-esc
      size="xl"
      @ok="addEmployee"
    >
      <template #modal-header="{ close }">
        <div class="mx-auto h5" style="width: 200px">Agregar Empleado</div>
        <div>
          <b-button size="sm" variant="outline-danger" @click="close()">
            Cerrar
          </b-button>
        </div>
      </template>
      <form ref="form">
        <b-row cols="3">
          <b-col>
            <b-form-group class="mt-3" label="Nombre">
              <b-form-input
                required
                v-model="EmployeesFields.nombre"
              ></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Apellido Paterno">
              <b-form-input
                required
                v-model="EmployeesFields.apellidoPaterno"
              ></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Apellido Materno">
              <b-form-input
                required
                v-model="EmployeesFields.apellidoMaterno"
              ></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Fecha de nacimiento">
              <b-form-datepicker
                today-button
                reset-button
                close-button
                locale="es"
                dropup
                v-model="EmployeesFields.fechaNacimiento"
              ></b-form-datepicker>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Dirección">
              <b-form-input
                required
                v-model="EmployeesFields.direccion"
              ></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Area">
              <v-select
                required
                :options="areas"
                label="nombre"
                :reduce="(areas) => areas.areaId"
                v-model="EmployeesFields.areaId"
              ></v-select>
            </b-form-group>
          </b-col>
        </b-row>
      </form>
    </b-modal>
  </b-card>
</template>

<script>
import "vue-select/dist/vue-select.css";

import EmployeeServices from "@/Services/employee.Services";
import AreaServices from "@/Services/area.Services";
import { ref } from "vue";
export default {
  setup() {
    const { getEmployees, createEmployee } = EmployeeServices();
    const { getAreas } = AreaServices();
    const refEmployeeTable = ref("");
    const employees = ref([]);
    const areas = ref([]);
    const perPage = ref(5);
    const currentPage = ref(1);
    const rows = ref(null);
    const filter = ref(null);
    const perPageSelect = ref([5, 10, 25, 50, 100]);
    const EmployeesFields = ref({
      empleadoId: 0,
      nombre: null,
      apellidoPaterno: null,
      apellidoMaterno: null,
      fechaNacimiento: null,
      direccion: null,
      fechaAlta: null,
      fechaBaja: null,
      archivado: false,
      areaId: null,
    });
    const fields = ref([
      { key: "empleadoId", label: "ID" },
      { key: "nombre", label: "Nombre" },
      { key: "apellidoPaterno", label: "Apellido Paterno" },
      { key: "apellidoPaterno", label: "Apellido Materno" },
      { key: "area.nombre", label: "Area de Trabajo" },
      { key: "actions", label: "Acciones" },
    ]);
    getEmployees((data) => {
      employees.value = data;
      rows.value = data.length;
    });
    getAreas((data) => {
      areas.value = data;
    });
    const onFiltered = (filteredItems) => {
      rows.value = filteredItems.length;
      currentPage.value = 1;
    };
    const addEmployee = () => {
      createEmployee(EmployeesFields.value);
      refreshTable();
    };
    const refreshTable = () => {
      // $refs.refreshTable.refresh();
      this.$refs.MyReference.$refs.refEmployeeTable.refresh();
    };
    return {
      employees,
      fields,
      perPage,
      currentPage,
      rows,
      filter,
      perPageSelect,
      areas,
      refEmployeeTable,
      EmployeesFields,

      onFiltered,
      refreshTable,
      addEmployee,
    };
  },
};
</script>

<style lang="scss" scoped></style>
