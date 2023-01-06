<template>
  <b-card class="m-2">
    <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
  </b-card>
  <b-card class="m-2">
    <b-row align-h="end" class="mb-3 mr-1">
      <b-form-input
        size="lg"
        style="width: 350px"
        v-model="searchValue"
        type="search"
        placeholder="Buscar estatus vale..."
      ></b-form-input>
      <b-button
        variant="primary"
        style="
          background-color: rgb(94, 80, 238);
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
        Estatus vale
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
      :items="statusVoucher"
      :rows-per-page="5"
      :search-field="searchField"
      :search-value="searchValue"
      theme-color="#7367f0"
    >
      <template #header-actions="header">
        {{ header.text }}
      </template>
      <template #item-actions="items">
        <b-dropdown
          id="ActionsDropdown"
          size="lg"
          style="text-color: black"
          variant="link"
          toggle-class="text-decoration-none"
          dropright
          no-caret
        >
          <template #button-content>
            <i class="bi bi-three-dots-vertical"></i>
          </template>
          <b-dropdown-item
            @click="RemoveStatusVoucher(items.estatusValeId)"
            class="m-1"
            variant="outline-danger"
            ><i class="bi bi-trash3"> Archivar</i></b-dropdown-item
          >
          <b-dropdown-item
            class="m-1"
            variant="outline-warning"
            :to="{
              name: 'EstatusVale-Edit',
              params: { EstatusValeId: items.estatusValeId }
            }"
            ><i class="bi bi-pencil-square" /> Editar</b-dropdown-item
          >
        </b-dropdown>
      </template>
    </EasyDataTable>
    <b-modal
      id="modal-statusVoucher"
      title="Agregar Estatus vale"
      v-model="showModal"
      size="xl"
      hide-footer
      button-size="lg"
      lazy
    >
      <Form @submit="addStatusVoucher">
        <b-row cols="2">
          <!--Agregar Nombre Estatus-->
          <b-col>
            <b-form-group class="mt-3" label="Nombre">
              <Field name="NameField" :rules="validateName" as="text">
                <b-form-input
                  v-model="statusVoucherFields.nombre"
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
                <b-form-textarea
                  v-model="statusVoucherFields.descripcion"
                  rows="4"
                ></b-form-textarea>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            @click="resetStatusVoucherFields"
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
import StatusVoucherServices from '@/Services/statusvoucher.Services'
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
    const { getStatusVoucher, createStatusVoucher, deleteStatusVoucher } =
      StatusVoucherServices()
    const statusVoucher = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('nombre')
    const NameState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Proveeduria', to: '/Proveeduria' },
      { text: 'Estatus del producto' }
    ])

    const statusVoucherFields = ref({
      estatusValeId: 0,
      nombre: null,
      descripcion: null,
      archivado: false
    })

    const statusVoucherFieldsBlank = ref(
      JSON.parse(JSON.stringify(statusVoucherFields))
    )
    const fields = ref([
      // { value: 'estatusValeId', text: 'ID', sortable: true },
      { value: 'nombre', text: 'Nombre' },
      { value: 'descripcion', text: 'Descripcion' },
      { value: 'actions', text: 'Acciones' }
    ])

    const resetStatusVoucherFields = () => {
      showModal.value = false
      statusVoucherFields.value = JSON.parse(
        JSON.stringify(statusVoucherFieldsBlank)
      )
      NameState.value = false
    }

    getStatusVoucher(data => {
      statusVoucher.value = data
      if (statusVoucher.value.length > 0) {
        isloading.value = false
      } else {
        if (statusVoucher.value.length <= 0) {
          isloading.value = false
        }
      }
    })

    const onFiltered = filteredItems => {
      currentPage.value = 1
    }

    const validateName = () => {
      if (!statusVoucherFields.value.nombre) {
        NameState.value = false
        return 'Este campo es requerido'
      }
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(statusVoucherFields.value.nombre)) {
        NameState.value = false
        return 'El nombre de estatus de obras publicas solo puede contener letras'
      }
      if (!statusVoucherFields.value.nombre.trim().length > 0) {
        NameState.value = false
        return 'Este campo no puede contener espacios'
      }
      NameState.value = true
      return true
    }

    // pone mis cambios de mis campos vacios de nuevo
    const refreshTable = () => {
      isloading.value = true
      getStatusVoucher(data => {
        statusVoucher.value = data
        if (statusVoucher.value.length > 0) {
          isloading.value = false
        } else {
          if (statusVoucher.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }

    const addStatusVoucher = () => {
      createStatusVoucher(statusVoucherFields.value, data => {
        refreshTable()
        swal.fire({
          title: '¡Estatus vale registrado correctamente!',
          text: 'El estatus vale se ha registrado al sistema satisfactoriamente.',
          icon: 'success'
        })
      })
      showModal.value = false
      resetStatusVoucherFields()
    }

    const RemoveStatusVoucher = StatusVoucherId => {
      isloading.value = true
      swal
        .fire({
          title: '¿Estas seguro?',
          text: 'No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Si, Archivar estatus vale!',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            deleteStatusVoucher(StatusVoucherId, data => {
              refreshTable()
            })
            swal.fire({
              title: '¡Estatus vale archivado!',
              text: 'El estatus vale ha sido archivado satisfactoriamente .',
              icon: 'success'
            })
          } else {
            isloading.value = false
          }
        })
    }

    return {
      statusVoucher,
      statusVoucherFields,
      showModal,
      breadcrumbItems,
      perPage,
      currentPage,
      filter,
      perPageSelect,
      isloading,
      searchValue,
      searchField,
      statusVoucherFieldsBlank,
      fields,
      NameState,

      onFiltered,
      addStatusVoucher,
      refreshTable,
      RemoveStatusVoucher,
      validateName,
      resetStatusVoucherFields
    }
  }
}
</script>

<style></style>
