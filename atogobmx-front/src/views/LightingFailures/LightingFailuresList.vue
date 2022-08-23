<template>
  <b-card class="m-3">
    <div>
          <b-form-input
        inline
        v-model="filter"
            type="search"
        placeholder="Buscar..."
          >
      </b-form-input>
        </div>
    <b-table
      id="lightingfailuresTable"
      :items="lightingfailures"
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
      aria-controls="lightingfailuresTable"
      align="right"
    ></b-pagination>
  </b-card>
</template>

<script>
import LightingfailuresServices from "@/Services/lightingfailures.Services";
import { ref } from "vue";
export default {
  setup() {
    const { getLightingFailures } = LightingfailuresServices();
    const Lightingfailures = ref([]);
    const perPage = ref(5);
    const currentPage = ref(1);
    const rows = ref(null);
    const filter = ref(null);
    const fields = ref([
      { key: "fallaId", label: "ID" },
      { key: "nombreFalla", label: "NombreFalla" },
      { key: "descripcion", label: "Descripcion" },
      { key: "fecha", label: "Fecha" },
    ]);
    getLightingFailures((data) => {
      Lightingfailures.value = data;
      rows.value = data.length;
    });

    const onFiltered = (filteredItems) => {
      rows.value = filteredItems.length;
      currentPage.value = 1;
    };
    return {
      Lightingfailures,
      perPage,
      currentPage,
      rows,
      filter,
      perPageSelect,
      isloading,
      searchValue,
      searchField,
      fields,

      onFiltered,
    };
  },
};
</script>

<style lang="scss" scoped></style>
