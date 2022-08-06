<template>
  <b-card class="m-3">
    <div>
      <!-- <b-form inline> -->
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
        <div class="form-inline">
          <v-select
            style="width: 90px"
            v-model="perPage"
            :value="perPage"
            :options="perPageSelect"
          >
          </v-select>
          <div>
            <span>Mostrar: </span>
          </div>
        </div>
      </div>
      <!-- </b-form> -->
    </div>
    <!-- <b-form inline>
      <v-select
        class="mb-2 mr-sm-2 mb-sm-0 col-2"
        style="color: black"
        v-model="perPage"
        :value="perPage"
        :options="perPageSelect"
      >
      </v-select>
      <b-button
        class="mb-2 mr-sm-2 mb-sm-0"
        style="float: right"
        variant="success"
      >
        Agregar
      </b-button>
      <b-form-input
        class="mb-2 mr-sm-2 mb-sm-0"
        v-model="filter"
        type="search"
        placeholder="Buscar..."
      >
      </b-form-input>
    </b-form> -->
    <b-table
      id="employeeTable"
      hover
      caption-top
      head-row-variant="primary"
      bordered
      show-empty
      empty-text="No se encuentran empleados registrados"
      class="mt-5 mb-3"
      fixed
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
    >
      <!-- @show="resetModal"
      @hidden="resetModal"
      @ok="handleOk" -->
      <template #modal-header="{ close }">
        <!-- Emulate built in modal header close button action -->
        <div class="mx-auto h5" style="width: 200px">Agregar Empleado</div>
        <div>
          <b-button size="sm" variant="outline-danger" @click="close()">
            Cerrar
          </b-button>
        </div>
      </template>
      <form ref="form" @submit.stop.prevent="handleSubmit">
        <b-row cols="3">
          <b-col>
            <b-form-group class="mt-3" label="Nombre">
              <b-form-input required></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Apellido Paterno">
              <b-form-input required></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Apellido Materno">
              <b-form-input required></b-form-input>
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
              ></b-form-datepicker>
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
import { ref } from "vue";
export default {
  setup() {
    const { getEmployees } = EmployeeServices();
    const employees = ref([]);
    const perPage = ref(5);
    const currentPage = ref(1);
    const rows = ref(null);
    const filter = ref(null);
    const perPageSelect = ref([5, 10, 25, 50, 100]);
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
