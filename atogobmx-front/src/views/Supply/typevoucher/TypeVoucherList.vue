<template>
  <b-card class="m-2">
    <b-row align-h="end" class="mb-3 mr-1">
      <b-form-input
        size="lg"
        style="width: 350px"
        v-model="searchValue"
        type="search"
        placeholder="Buscar tipo de vale..."
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
        Tipo de vale
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
      :items="typeVoucher"
      :rows-per-page="5"
      :search-field="searchField"
      :search-value="searchValue"
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
            @click="RemoveTypeVoucher(items.tipoId)"
            class="m-1"
            variant="outline-danger"
            ><i class="bi bi-trash3"> Archivar</i></b-dropdown-item
          >
          <b-dropdown-item
            class="m-1"
            variant="outline-warning"
            :to="{
              name: 'TipoVale-Edit',
              params: { TipoId: items.tipoId }
            }"
            ><i class="bi bi-pencil-square" /> Editar</b-dropdown-item
          >
        </b-dropdown>
      </template>
    </EasyDataTable>
    <b-modal
      id="modal-typeVoucher"
      title="Agregar tipo de vale"
      v-model="showModal"
      size="xl"
      hide-footer
      button-size="lg"
      lazy
    >
      <Form @submit="addTypeVoucher">
        <b-row cols="2">
          <!--Agregar Nombre-->
          <b-col>
            <b-form-group class="mt-3" label="Nombre">
              <Field name="NameField" :rules="validateName" as="text">
                <b-form-input
                  v-model="typeVoucherFields.nombre"
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
                  v-model="typeVoucherFields.descripcion"
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
            @click="resetTypeVoucherFields"
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
import TypeVoucherServices from '@/Services/typevoucher.Services'
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
    const { getTypeVoucher, createTypeVoucher, deleteTypeVoucher } = TypeVoucherServices()
    const typeVoucher = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('nombre')
    const NameState = ref(false)
    const DescriptionState = ref(false)
    const typeVoucherFields = ref({
      tipoId: 0,
      nombre: null,
      descripcion: null,
      archivado: false
    })

    const typeVoucherFieldsBlank = ref(
      JSON.parse(JSON.stringify(typeVoucherFields))
    )
    const fields = ref([
      { value: 'tipoId', text: 'ID', sortable: true },
      { value: 'nombre', text: 'Nombre' },
      { value: 'descripcion', text: 'Descripcion' },
      { value: 'actions', text: 'Acciones' }
    ])

    const resetTypeVoucherFields = () => {
      showModal.value = false
      typeVoucherFields.value = JSON.parse(
        JSON.stringify(typeVoucherFieldsBlank)
      )
      NameState.value = false
      DescriptionState.value = false
    }

    getTypeVoucher(data => {
      typeVoucher.value = data
      if (typeVoucher.value.length > 0) {
        isloading.value = false
      } else {
        if (typeVoucher.value.length <= 0) {
          isloading.value = false
        }
      }
    })

    const onFiltered = filteredItems => {
      currentPage.value = 1
    }

    const validateName = () => {
      if (!typeVoucherFields.value.nombre) {
        NameState.value = false
        return 'Este campo es requerido'
      }
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(typeVoucherFields.value.nombre)) {
        NameState.value = false
        return 'El nombre de estatus de obras publicas solo puede contener letras'
      }
      if (!typeVoucherFields.value.nombre.trim().length > 0) {
        NameState.value = false
        return 'Este campo no puede contener espacios'
      }
      NameState.value = true
      return true
    }

    const validateDescription = () => {
      if (!typeVoucherFields.value.descripcion) {
        DescriptionState.value = false
        return 'Este campo es requerido'
      }
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(typeVoucherFields.value.descripcion)) {
        DescriptionState.value = false
        return 'La descripcion solo puede contener letras'
      }
      if (!typeVoucherFields.value.descripcion.trim().length > 0) {
        DescriptionState.value = false
        return 'Este campo no puede contener espacios'
      }
      DescriptionState.value = true
      return true
    }

    // pone mis cambios de mis campos vacios de nuevo
    const refreshTable = () => {
      isloading.value = true
      getTypeVoucher(data => {
        typeVoucher.value = data
        if (typeVoucher.value.length > 0) {
          isloading.value = false
        } else {
          if (typeVoucher.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }

    const addTypeVoucher = () => {
      createTypeVoucher(typeVoucherFields.value, data => {
        refreshTable()
        swal.fire({
          title: '¡Tipo vale registrado correctamente!',
          text: 'El tipo vale se ha registrado al sistema satisfactoriamente.',
          icon: 'success'
        })
      })
      showModal.value = false
      resetTypeVoucherFields()
    }

    const RemoveTypeVoucher = TypeVoucherId => {
      isloading.value = true
      swal.fire({
        title: '¿Estas seguro?',
        text: 'No podrás revertir esto!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, Archivar tipo vale!',
        cancelButtonText: 'Cancelar'
      })
        .then(result => {
          if (result.isConfirmed) {
            deleteTypeVoucher(TypeVoucherId, (data) => {
              refreshTable()
            })
            swal
              .fire({
                title: '¡Tipo vale archivado!',
                text: 'El tipo vale ha sido archivado satisfactoriamente .',
                icon: 'success'
              })
          } else {
            isloading.value = false
          }
        })
    }

    return {
      typeVoucher,
      typeVoucherFields,
      showModal,
      perPage,
      currentPage,
      filter,
      perPageSelect,
      isloading,
      searchValue,
      searchField,
      typeVoucherFieldsBlank,
      fields,
      NameState,
      DescriptionState,

      onFiltered,
      addTypeVoucher,
      refreshTable,
      RemoveTypeVoucher,
      validateName,
      validateDescription,
      resetTypeVoucherFields
    }
  }
}
</script>

<style>

</style>
