<template>
  <b-card class="m-2">
    <b-row cols="3" align-h="end" class="m-3">
        <b-col>
        <b-form-input
          size="lg"
          v-model="searchValue"
          type="search"
          placeholder="Buscar Documento..."
        />
        </b-col>
        <b-col>
        <b-button
          variant="success"
          type="submit"
          class="w-100"
          style="
          height: 55px;
          font-size: 15px;
          margin-bottom: 15px;
        "
          @click="showModal = !showModal"
        >
          <i class="bi bi-file-earmark-plus"></i>
          Agregar Documento
        </b-button>
        </b-col>
        <b-col>
        <b-button
          :disabled="disableButtonDownload"
          v-if="documents.length > 0"
          variant="primary"
          size="lg"
          class="w-100"
          style="
            font-size: 18px;
            text-align: center;
          "
          type="submit"
          @click="onDownloadFiles"
        >
          <i class="bi bi-download"></i>
          Descargar Documentos
        </b-button>
        </b-col>
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
      theme-color="#7367f0"
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
          @click="onDownloadFile(items)"
        >
          <i class="bi bi-download"></i>
        </b-button>
      </template>
    </EasyDataTable>
  </b-card>
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
import { axiosPrivate } from '@/common/axiosPrivate.js'
export default {
  props: {
    CementerioId: {
      type: Number,
      required: true
    },
    Cementerio: {
      type: Object,
      required: true
    }
  },
  components: {
    EasyDataTable: window['vue3-easy-data-table']
  },
  setup (props) {
    const {
      getDocumentsCementerios,
      deleteDocumentsCementerios,
      createDocumentsCementerios
    } = FileServices()
    const showModal = ref(false)
    const refFile = ref()
    const disableButton = ref(true)
    const disableButtonDownload = ref(false)
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
    const cementerioId = ref(props.CementerioId)
    const formData = new FormData()
    const fields = ref([
      { value: 'nombre', text: 'Nombre Documento', sortable: true },
      { value: 'tipoArchivo', text: 'Tipo Documento', sortable: true },
      { value: 'actions', text: 'Acciones' }
    ])
    getDocumentsCementerios(props.CementerioId, data => {
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
      getDocumentsCementerios(props.CementerioId, data => {
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
      }
    }
    const submitFiles = () => {
      disableButton.value = false
      if (refFile.value.files.length === 0) {
        swal.fire({
          title: 'Documento no seleccionado!',
          text: 'No se ha seleccionado ningún documento.',
          icon: 'error'
        })
        return ''
      }
      createDocumentsCementerios(props.CementerioId, formData, data => {
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
      disableButton.value = true
      // refFile.value.files = []
    }
    const onDownloadFiles = () => {
      isloading.value = true
      disableButtonDownload.value = true
      axiosPrivate({
        url: `/Archivos/Documentos22/${props.CementerioId}/Zip`,
        method: 'GET',
        responseType: 'blob' // important
      })
        .then(response => {
          const url = window.URL.createObjectURL(new Blob([response.data]))
          const link = document.createElement('a')
          link.href = url
          link.setAttribute(
            'download',
            'Cementerio_Documentos.zip'
          )
          document.body.appendChild(link)
          link.click()
        })
        .then(result => {
          isloading.value = false
          disableButtonDownload.value = false
        })
    }
    const onDownloadFile = (Cementerio) => {
      isloading.value = true
      disableButtonDownload.value = true
      axiosPrivate({
        url: `/Archivos/Documents/Dowload2/${props.CementerioId}/${Cementerio.archivosCementerioId}`,
        method: 'GET',
        responseType: 'blob' // important
      })
        .then(response => {
          const url = window.URL.createObjectURL(new Blob([response.data]))
          const link = document.createElement('a')
          link.href = url
          link.setAttribute(
            'download',
            `${Cementerio.nombre}${Cementerio.tipoArchivo}`
          )
          document.body.appendChild(link)
          link.click()
        })
        .then(result => {
          isloading.value = false
          disableButtonDownload.value = false
        })
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
            deleteDocumentsCementerios(props.CementerioId, archivoId, data => {
              swal
                .fire({
                  title: 'Documento Eliminado!',
                  text: 'Tu documento ha sido eliminado.',
                  icon: 'success'
                })
                .then(result => {
                  if (result.isConfirmed) {
                    showModal.value = false
                    refreshTable()
                    refFile.value = null
                  }
                })
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
      cementerioId,
      showModal,
      disableButton,
      disableButtonDownload,

      onFiltered,
      RemoveDocument,
      refreshTable,
      onChangeFile,
      submitFiles,
      onDownloadFiles,
      onDownloadFile
    }
  }
}
</script>

<style scoped></style>
