<template>
<b-row align-h="end" class="mb-3 mr-1">
      <b-form-input
        size="xl"
        style="width: 350px"
        v-model="searchValue"
        type="search"
        placeholder="Buscar Documento..."
      >
      </b-form-input>
      <b-button
        variant="success"
        style="
          height: 50px;
          width: auto;
          font-size: 15px;
          margin-right: 15px;
          margin-left: 20px;
        "
        v-b-modal.modal-expedientDigital
        type="submit"
      >
        <i class="bi bi-person-plus-fill"></i>
        Agregar Documento
      </b-button>
    </b-row>
    <EasyDataTable
      rows-per-page-message="registros por pagina"
      empty-message="No se encuentran registros"
      table-class-name="customize-table"
      buttons-pagination
      border-cell
      :loading="isloading"
      :headers="fields"
      :items="documents"
      :rows-per-page="5"
      :search-field="searchField"
      :search-value="searchValue"
    >
      <template #header-actions="header">
        {{ header.text }}
      </template>
      <template #item-actions="items">
        <b-button
          @click="RemoveDocument(items.archivoId)"
          class="m-1"
          variant="outline-danger">
          <i class="bi bi-trash3"></i>
        </b-button>
        <b-button
          class="m-1"
          variant="outline-warning"
          :href="`https://localhost:7065/api/Archivos/Documentos/Descargar/${expedienteDigitalId}/${items.archivoId}`"
          >
         <i class="bi bi-download"></i>
        </b-button>
      </template>
    </EasyDataTable>
  <b-modal
    id="modal-expedientDigital"
    title="Documentos"
    size="xl"
    centered
    hide-footer
    button-size="lg"
    lazy
  >
  <div class="input-group mb-3">
      <input
        type="file"
        class="form-control"
        v-on:change="onChangeFile"
        ref="refFile"
        id="file"
        multiple
      />
      <b-button variant="outline-primary" @click="submitPhoto"
        >Cargar Archivo(s)</b-button
      >
    </div>
  </b-modal>
</template>

<script>
import { ref, inject } from 'vue'
import FileServices from '@/Services/file.Services'
export default {
  props: {
    ExpedientDigitalId: {
      type: Number,
      required: true
    }
  },
  components: {
    EasyDataTable: window['vue3-easy-data-table']
  },
  setup (props) {
    const { getDocuments, deleteDocument } = FileServices()
    const swal = inject('$swal')
    const documents = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    // const files = ref([])
    const searchValue = ref('')
    const searchField = ref('nombre')
    const expedienteDigitalId = ref(props.ExpedientDigitalId)
    const fields = ref([
      { value: 'archivoId', text: 'ID' },
      { value: 'nombre', text: 'Nombre Documento', sortable: true },
      { value: 'tipoArchivo', text: 'Tipo Documento', sortable: true },
      { value: 'actions', text: 'Acciones' }
    ])
    // const onChangeFile = e => {
    //   files.value = refFile.value.files[0]
    // }
    getDocuments(props.ExpedientDigitalId, data => {
      documents.value = data
      if (documents.value.length > 0) {
        isloading.value = false
      } else {
        if (documents.value.length <= 0) {
          isloading.value = false
        }
      }
    })
    const refreshTable = () => {
      isloading.value = true
      getDocuments(props.ExpedientDigitalId, data => {
        documents.value = data
        if (documents.value.length > 0) {
          isloading.value = false
        } else {
          if (documents.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }
    const onFiltered = (filteredItems) => {
      currentPage.value = 1
    }
    const RemoveDocument = (archivoId) => {
      swal.fire({
        title: '¿Estas seguro?',
        text: 'No podrás revertir esto!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, Eliminar Documento!',
        cancelButtonText: 'Cancelar'
      }).then((result) => {
        if (result.isConfirmed) {
          swal.fire({
            title: 'Documento Eliminado!',
            text: 'Tu documento ha sido eliminado.',
            icon: 'success'
          }).then((result) => {
            if (result.isConfirmed) {
              deleteDocument(props.ExpedientDigitalId, archivoId, data => {
                refreshTable()
              })
            }
          })
        }
      })
    }
    return {
      fields,
      perPage,
      currentPage,
      filter,
      perPageSelect,
      isloading,
      searchValue,
      searchField,
      documents,
      expedienteDigitalId,

      onFiltered,
      RemoveDocument,
      refreshTable
    }
  }
}
</script>

<style scoped>
.customize-table {
    --easy-table-border: 1px solid #ffffff;
    --easy-table-row-border: 1px solid #ffffff;

    --easy-table-header-font-size: 14px;
    --easy-table-header-height: 50px;
    --easy-table-header-font-color: rgb(255, 255, 255);
    --easy-table-header-background-color:#2E86C1;

    --easy-table-header-item-padding: 10px 15px;

    --easy-table-body-even-row-font-color: #fff;
    --easy-table-body-even-row-background-color: #2E86C1;

    --easy-table-body-row-font-color: #000000;
    --easy-table-body-row-background-color: #ffffff;
    --easy-table-body-row-height: 50px;
    --easy-table-body-row-font-size: 14px;

    --easy-table-body-row-hover-font-color: #1b1b1b;
    --easy-table-body-row-hover-background-color: #D6EAF8;

    --easy-table-body-item-padding: 10px 15px;

    --easy-table-footer-background-color: #2E86C1;
    --easy-table-footer-font-color: #ffffff;
    --easy-table-footer-font-size: 14px;
    --easy-table-footer-padding: 0px 10px;
    --easy-table-footer-height: 50px;

    /* --easy-table-scrollbar-track-color: #2d3a4f;
    --easy-table-scrollbar-color: #2d3a4f;
    --easy-table-scrollbar-thumb-color: #4c5d7a;
    --easy-table-scrollbar-corner-color: #2d3a4f; */

    /* --easy-table-loading-mask-background-color: #2d3a4f; */
  }
</style>
