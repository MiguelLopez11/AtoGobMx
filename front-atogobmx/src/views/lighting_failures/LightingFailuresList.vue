<template >
    <b-card
  class="m-3"
>
  <div class="row justify-content-center mt-5">
    <div class="col-12">
      <!-- bordered -->
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
                <!-- <b-button variant="primary" class="m-1">
                  <i class="bi bi-file-earmark-person">Expediente</i>
                </b-button>
                <b-button variant="danger">
                  <i class="bi bi-trash"></i>
                </b-button> -->
              </div>
            </b-td>
          </b-tr>
        </b-tbody>
      </b-table-simple>
    </div>
  </div>
</b-card>
</template>
<script>
export default {
}
</script>
<style lang="">
</style>
