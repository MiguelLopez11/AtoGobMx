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
          margin-left: 20px;"
      type="submit"
      v-b-modal.modal-expedientDigital
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
        variant="outline-danger"
      >
        <i class="bi bi-trash3"></i>
      </b-button>
      <b-button
        class="m-1"
        variant="outline-warning"
        :href="
          `https://localhost:7065/api/Archivos/Documentos/Descargar/${expedienteDigitalId}/${items.archivoId}`
        "
      >
        <i class="bi bi-download"></i>
      </b-button>
    </template>
  </EasyDataTable>
  <b-modal
    ref="DocumentModal"
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
      <b-button variant="outline-primary" @click="submitFiles()"
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
    const { getDocuments, deleteDocument, createDocuments } = FileServices()
    const refFile = ref()
    const DocumentModal = ref()
    const swal = inject('$swal')
    const documents = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('nombre')
    const expedienteDigitalId = ref(props.ExpedientDigitalId)
    const formData = new FormData()
    // const files = ref([])
    const fields = ref([
      { value: 'archivoId', text: 'ID' },
      { value: 'nombre', text: 'Nombre Documento', sortable: true },
      { value: 'tipoArchivo', text: 'Tipo Documento', sortable: true },
      { value: 'actions', text: 'Acciones' }
    ])
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
    const onFiltered = () => {
      currentPage.value = 1
    }
    const onChangeFile = () => {
      for (const file of refFile.value.files) {
        formData.append('files', file, file.name)
      }
    }
    const submitFiles = () => {
      if (refFile.value.files.length === 0) {
        swal.fire({
          title: 'Documento no seleccionado!',
          text: 'No se ha seleccionado ningún documento.',
          icon: 'error'
        })
        return ''
      }
      createDocuments(props.ExpedientDigitalId, formData, data => {
        swal
          .fire({
            title: 'Documento(s) Guardado(s)!',
            text: 'Documento(s) se ha(n) guardado correctamente.',
            icon: 'success'
          })
          .then(result => {
            if (result.isConfirmed) {
              refreshTable()
            }
          })
      })
      refFile.value.files = []
    }
    const RemoveDocument = archivoId => {
      swal
        .fire({
          title: '¿Estas seguro?',
          text: 'No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Si, Eliminar Documento!',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            swal
              .fire({
                title: 'Documento Eliminado!',
                text: 'Tu documento ha sido eliminado.',
                icon: 'success'
              })
              .then(result => {
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
      DocumentModal,
      perPage,
      currentPage,
      filter,
      refFile,
      perPageSelect,
      isloading,
      searchValue,
      searchField,
      documents,
      expedienteDigitalId,

      onFiltered,
      RemoveDocument,
      refreshTable,
      onChangeFile,
      submitFiles
    }
  }
}
</script>

<style scoped></style>
