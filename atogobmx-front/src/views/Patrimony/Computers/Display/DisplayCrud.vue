<template>
  <b-card class="m-2">
    <b-row align-h="end" class="mb-3 mr-1">
      <b-button
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
        <i class="bi bi-display-fill"></i>
        Agregar Monitor
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
      :items="displays"
      :rows-per-page="5"
      :search-field="searchField"
      :search-value="searchValue"
    >
      <template #header-actions="header">
        {{ header.text }}
      </template>
      <template #item-actions="items">
        <b-button
          @click="RemoveDisplay(items.monitorId)"
          class="m-1"
          variant="outline-danger"
          ><i class="bi bi-trash3"></i
        ></b-button>
      </template>
    </EasyDataTable>
    <b-modal
      v-model="showModal"
      title="Agregar Monitor"
      size="xl"
      centered
      button-size="lg"
      hide-footer
    >
      <Form @submit="addDisplay">
        <b-row cols="3">
          <b-col>
            <b-form-group class="mt-3" label="Marca">
              <Field
                name="BrandField"
                :rules="validateBrand"
                as="text"
              >
                <b-form-input v-model="displayFields.marca" :state="brandState"> </b-form-input>
              </Field>
              <ErrorMessage class="text-danger" name="BrandField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Pulgadas">
              <Field
                name="inchesField"
                :rules="validateInches"
                as="text"
              >
                <b-form-input type="number" v-model="displayFields.pulgadas" :state="inchesState"> </b-form-input>
              </Field>
              <ErrorMessage class="text-danger" name="inchesField"></ErrorMessage>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            @click="resetRoleFields()"
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
import ComputerServices from '@/Services/computer.Services'
import { Form, Field, ErrorMessage } from 'vee-validate'
import { ref, inject } from 'vue'
// import { useToast } from 'vue-toast-notification'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    Form,
    Field,
    ErrorMessage,
    EasyDataTable: window['vue3-easy-data-table']
  },
  props: {
    EquipoComputoId: {
      type: Number,
      required: true
    }
  },
  setup (props) {
    const swal = inject('$swal')
    const { getDisplays, createDisplay, deleteDisplay } = ComputerServices()
    // const $toast = useToast()
    const displays = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('marca')
    const brandState = ref(false)
    const inchesState = ref(false)
    const showModal = ref(false)
    const displayFields = ref({
      monitorId: 0,
      codigoInventario: null,
      marca: null,
      pulgadas: null,
      equipoComputoId: props.EquipoComputoId,
      archivado: false
    })
    const displayFieldsBlank = ref(JSON.parse(JSON.stringify(displayFields)))
    const fields = ref([
      { value: 'marca', text: 'Marca' },
      { value: 'pulgadas', text: 'Pulgadas' },
      { value: 'actions', text: 'Acciones' }
    ])
    getDisplays((data) => {
      displays.value = data
      if (displays.value.length > 0) {
        isloading.value = false
      } else {
        if (displays.value.length <= 0) {
          isloading.value = false
        }
      }
    })
    const onFiltered = (filteredItems) => {
      currentPage.value = 1
    }
    const validateBrand = () => {
      if (!displayFields.value.marca) {
        brandState.value = false
        return 'Este campo es requerido'
      }
      brandState.value = true
      return true
    }
    const validateInches = () => {
      if (!displayFields.value.pulgadas) {
        inchesState.value = false
        return 'Este campo es requerido'
      }
      inchesState.value = true
      return true
    }
    const refreshTable = () => {
      isloading.value = true
      getDisplays((data) => {
        displays.value = data
        if (displays.value.length > 0) {
          isloading.value = false
        } else {
          if (displays.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }
    const addDisplay = () => {
      createDisplay(displayFields.value, (data) => {
        refreshTable()
        swal.fire({
          title: 'Monitor registrado correctamente!',
          text: 'El monitor se ha registrado al sistema satisfactoriamente.',
          icon: 'success'
        })
      })
      resetRoleFields()
    }
    const resetRoleFields = () => {
      showModal.value = false
      displayFields.value = JSON.parse(JSON.stringify(displayFieldsBlank))
      brandState.value = false
      inchesState.value = false
    }
    const RemoveDisplay = (monitorId) => {
      isloading.value = true
      swal
        .fire({
          title: '¿Estas seguro?',
          text: 'No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Si, archivar monitor!',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            swal
              .fire({
                title: 'Monitor archivado!',
                text: 'El monitor ha sido archivado satisfactoriamente .',
                icon: 'success'
              })
              .then(result => {
                if (result.isConfirmed) {
                  deleteDisplay(monitorId, (data) => {
                    refreshTable()
                  })
                }
              })
          } else {
            isloading.value = false
          }
        })
    }
    return {
      displays,
      fields,
      perPage,
      currentPage,
      filter,
      perPageSelect,
      displayFieldsBlank,
      displayFields,
      isloading,
      searchValue,
      searchField,
      onFiltered,
      addDisplay,
      refreshTable,
      RemoveDisplay,
      brandState,
      inchesState,
      showModal,

      validateBrand,
      validateInches,
      resetRoleFields
    }
  }
}
</script>

<style>

</style>
