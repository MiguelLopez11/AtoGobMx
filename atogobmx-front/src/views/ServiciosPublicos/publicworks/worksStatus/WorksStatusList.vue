<template>
  <b-card class="m-2">
    <b-row align-h="end" class="mb-3 mr-1">
      <b-form-input
        size="lg"
        style="width: 350px"
        v-model="searchValue"
        type="search"
        placeholder="Buscar Estatus de obras publicas..."
      ></b-form-input>
      <b-button
        variant="primary"
        style="
          background-color: rgb(94,80,238);
          height: 50px;
          width: auto;
          font-size: 18px;
          margin-right: 15px;
          margin-left: 20px;
        "
        @click="showModal = !showModal"
        type="submit"
      >
        <i class="bi bi-person-plus-fill"></i>
        Estatus de  obras publicas
      </b-button>
    </b-row>
    <EasyDataTable
      rows-per-page-message="registros por pagina"
      empty-message="No se encontro ningun registro"
      table-class-name="customize-table"
      buttons-pagination
      border-cell
      :loading="isloading"
      :headers="fields"
      :items="worksStatus"
      :rows-per-page="5"
      :search-field="searchField"
      :search-value="searchValue"
    >
      <template #header-actions="header">
        {{ header.text }}
      </template>
      <template #item-actions="items">
        <b-button
          @click="RemoveWorksStatus(items.estatusObraId)"
          class="m-1"
          variant="outline-danger"
          ><i class="bi bi-trash3"></i
        ></b-button>
        <b-button
          class="m-1"
          variant="outline-warning"
          :to="{
            name: 'EstatusOP-Edit',
            params: { EstatusObraId: items.estatusObraId }
          }"
          ><i class="bi bi-pencil-square"></i
        ></b-button>
      </template>
    </EasyDataTable>
    <b-modal
      id="modal-worksstatus"
      title="Agregar Estatus OP"
      v-model="showModal"
      size="xl"
      hide-footer
      button-size="lg"
      lazy
    >
      <Form @submit="addWorksStatus">
        <b-row cols="2">
          <!--Agregar Nombre Estatus-->
          <b-col>
            <b-form-group class="mt-3" label="Nombre">
              <Field name="NameField" :rules="validateName" as="text">
                <b-form-input
                  v-model="worksStatusFields.nombre"
                  :state="NameState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage class="text-danger" name="NameField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--Agregar Descripcion-->
          <b-col>
            <b-form-group class="mt-3" label="Descripcion">
              <Field name="DescriptionField" :rules="validateDescription" as="text">
                <b-form-input
                  v-model="worksStatusFields.descripcion"
                  :state="DescriptionState"
                ></b-form-input>
              </Field>
              <ErrorMessage class="text-danger" name="DescriptionField"></ErrorMessage>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            @click="resetWorksStatusFields"
          >
            Cancelar
          </b-button>
          <b-button class="w-auto m-2" variant="success" type="submit">
            Guardar
          </b-button>
        </b-row>
      </Form>
    </b-modal>
  </b-card>
</template>

<script>
import worksStatusServices from '@/Services/worksstatus.Services'
import { Form, Field, ErrorMessage } from 'vee-validate'
import { ref, inject } from 'vue'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    EasyDataTable: window['vue3-easy-data-table'],
    Form,
    Field,
    ErrorMessage
  },
  setup () {
    const swal = inject('$swal')
    const showModal = ref(false)
    const { getWorksStatus, createWorksStatus, deleteWorksStatus } = worksStatusServices()
    const worksStatus = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('nombre')
    const NameState = ref(false)
    const DescriptionState = ref(false)
    const worksStatusFields = ref({
      estatusObraId: 0,
      nombre: null,
      descripcion: null,
      archivado: false
    })

    const worksStatusFieldsBlank = ref(
      JSON.parse(JSON.stringify(worksStatusFields))
    )
    const fields = ref([
      { value: 'estatusObraId', text: 'ID', sortable: true },
      { value: 'nombre', text: 'Nombre' },
      { value: 'descripcion', text: 'Descripcion' },
      { value: 'actions', text: 'Acciones' }
    ])

    const resetWorksStatusFields = () => {
      showModal.value = false
      worksStatusFields.value = JSON.parse(
        JSON.stringify(worksStatusFieldsBlank)
      )
      NameState.value = false
      DescriptionState.value = false
    }

    getWorksStatus(data => {
      worksStatus.value = data
      if (worksStatus.value.length > 0) {
        isloading.value = false
      } else {
        if (worksStatus.value.length <= 0) {
          isloading.value = false
        }
      }
    })

    const onFiltered = filteredItems => {
      currentPage.value = 1
    }

    const validateName = () => {
      if (!worksStatusFields.value.nombre) {
        NameState.value = false
        return 'Este campo es requerido'
      }
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(worksStatusFields.value.nombre)) {
        NameState.value = false
        return 'El nombre de estatus de obras publicas solo puede contener letras'
      }
      if (!worksStatusFields.value.nombre.trim().length > 0) {
        NameState.value = false
        return 'Este campo no puede contener espacios'
      }
      NameState.value = true
      return true
    }

    const validateDescription = () => {
      if (!worksStatusFields.value.descripcion) {
        DescriptionState.value = false
        return 'Este campo es requerido'
      }
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(worksStatusFields.value.descripcion)) {
        DescriptionState.value = false
        return 'La descripcion solo puede contener letras'
      }
      if (!worksStatusFields.value.descripcion.trim().length > 0) {
        DescriptionState.value = false
        return 'Este campo no puede contener espacios'
      }
      DescriptionState.value = true
      return true
    }

    // pone mis cambios de mis campos vacios de nuevo
    const refreshTable = () => {
      isloading.value = true
      getWorksStatus(data => {
        worksStatus.value = data
        if (worksStatus.value.length > 0) {
          isloading.value = false
        } else {
          if (worksStatus.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }
    const addWorksStatus = () => {
      createWorksStatus(worksStatusFields.value, data => {
        refreshTable()
        swal.fire({
          title: '¡Estatus de obras publicas registrado correctamente!',
          text: 'El estatus de obras publicas se ha registrado al sistema satisfactoriamente.',
          icon: 'success'
        })
      })
      showModal.value = false
      resetWorksStatusFields()
    }
    const RemoveWorksStatus = WorksStatusId => {
      isloading.value = true
      swal.fire({
        title: '¿Estas seguro?',
        text: 'No podrás revertir esto!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, Archivar estatus de obras pblicas!',
        cancelButtonText: 'Cancelar'
      })
        .then(result => {
          if (result.isConfirmed) {
            deleteWorksStatus(WorksStatusId, (data) => {
              refreshTable()
            })
            swal
              .fire({
                title: '¡Estatus de obras publicas archivado!',
                text: 'El estatus de obras publicas ha sido archivado satisfactoriamente.',
                icon: 'success'
              })
          } else {
            isloading.value = false
          }
        })
    }
    return {
      worksStatus,
      worksStatusFields,
      showModal,
      perPage,
      currentPage,
      filter,
      perPageSelect,
      isloading,
      searchValue,
      searchField,
      worksStatusFieldsBlank,
      fields,
      NameState,
      DescriptionState,

      onFiltered,
      addWorksStatus,
      refreshTable,
      RemoveWorksStatus,
      validateName,
      validateDescription,
      resetWorksStatusFields
    }
  }
}
</script>

<style>

</style>
