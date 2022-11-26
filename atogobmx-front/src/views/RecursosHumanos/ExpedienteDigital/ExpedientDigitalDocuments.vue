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
      type="submit"
      @click="showModal = !showModal"
    >
      <i class="bi bi-file-earmark-plus"></i>
      Agregar Documento
    </b-button>
    <b-button
      v-if="documents.length > 0"
      variant="primary"
      style="
        height: 50px;
        width: auto;
        font-size: 16px;
        margin-right: 15px;
        margin-left: 20px;
        text-align: center;
      "
      type="submit"
      :href="`https://localhost:7065/api/Archivos/Documentos/${expedienteDigitalId}/Zip`"
    >
      <i class="bi bi-download"></i>
      Descargar Documentos
    </b-button>
  </b-row>
  <b-alert variant="warning" show dismissible>
    Si el documento no se descarga, contacte a soporte para corregirlo.
  </b-alert>
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
        :href="`https://localhost:7065/api/Archivos/Documentos/Descargar/${expedienteDigitalId}/${items.archivoId}`"
      >
        <i class="bi bi-download"></i>
      </b-button>
    </template>
  </EasyDataTable>
  <b-modal
    ref="DocumentModal"
    v-model="showModal"
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
        accept=".doc, .docx,.pdf"
      />
      <b-button
        :disabled="disableButton"
        variant="outline-primary"
        @click="submitFiles()"
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
    const showModal = ref(false)
    const refFile = ref()
    const disableButton = ref(true)
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
    const fields = ref([
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
        disableButton.value = false
        documents.value.forEach(element => {
          if (element.nombre === file.name) {
            swal.fire({
              title: 'Documento ya existe!',
              text: 'Posiblemente uno o más de los documentos seleccionados ya existen en el sistema.',
              icon: 'error'
            })
            disableButton.value = true
          }
        })
        if (file.type === 'image/jpeg' || file.type === 'image/png' || file.type === 'image/jpg') {
          swal.fire({
            title: 'Documento no válido!',
            text: 'Se ha seleccionado un documento que no es valido, revise e intentelo de nuevo.',
            icon: 'error'
          })
          disableButton.value = true
        }
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
        showModal.value = false
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
                    showModal.value = false
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
      showModal,
      disableButton,

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
