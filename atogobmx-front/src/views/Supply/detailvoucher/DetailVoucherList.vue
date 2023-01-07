<template>
  <b-row align-h="end" class="mb-3 mr-1">
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
      Agregar Producto
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
    :items="detailVoucher"
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
          @click="RemoveDetailVoucher(items.detalleValeId)"
          class="m-1"
          variant="outline-danger"
          ><i class="bi bi-trash3"> Archivar</i></b-dropdown-item
        >
        <!-- <b-dropdown-item
            class="m-1"
            variant="outline-warning"
            :to="{
              name: 'DetalleProducto-Edit',
              params: { DetalleValeId: items.detalleValeId }
            }"
            ><i class="bi bi-pencil-square" /> Editar</b-dropdown-item
          > -->
      </b-dropdown>
    </template>
  </EasyDataTable>
  <b-modal
    id="modal-detailVoucher"
    title="Agregar Producto"
    v-model="showModal"
    size="xl"
    hide-footer
    button-size="lg"
    lazy
  >
    <Form @submit="addDetailVoucher">
      <b-row cols="2">
        <!--Agregar Producto-->
        <b-col>
          <b-form-group class="mt-3" label="Producto: ">
            <Field name="ProductField" :rules="validateProduct" as="number">
              <b-form-select
                v-model="detailVoucherFields.productoId"
                autofocus
                :options="productVoucher"
                value-field="productoId"
                text-field="nombre"
                :state="ProductState"
              >
              </b-form-select>
            </Field>
            <ErrorMessage class="text-danger" name="ProductField"></ErrorMessage>
          </b-form-group>
        </b-col>
        <!--Agregar cantidad -->
        <b-col>
          <b-form-group class="mt-3" label="Cantidad">
            <Field name="QuantityField" :rules="validateQuantity" as="number">
              <b-form-input
                v-model="detailVoucherFields.cantidad"
                :state="QuantityState"
                min="1"
                type="number"
              >
              </b-form-input>
            </Field>
            <ErrorMessage
              class="text-danger"
              name="QuantityField"
            ></ErrorMessage>
          </b-form-group>
        </b-col>
        <!--Agregar importe -->
        <b-col>
          <b-form-group class="mt-3" label="Importe">
            <b-form-input v-model="detailVoucherFields.importe" type="number">
            </b-form-input>
          </b-form-group>
        </b-col>
        <!--Agregar  total-->
        <b-col>
          <b-form-group class="mt-3" label="Total">
            <b-form-input v-model="detailVoucherFields.subtotal" type="number">
            </b-form-input>
          </b-form-group>
        </b-col>
        <!--Agregar medida -->
          <b-col>
            <b-form-group class="mt-3" label="Medida">
              <b-form-input v-model="detailVoucherFields.medida" >
              </b-form-input>
            </b-form-group>
          </b-col>
          <!--Agregar Precio -->
          <b-col>
            <b-form-group class="mt-3" label="Precio">
              <b-form-input v-model="detailVoucherFields.precio" type="number">
              </b-form-input>
            </b-form-group>
          </b-col>
        <!--termino de cada campo a registrar-->
      </b-row>
      <b-row align-h="end">
        <b-button
          class="w-auto m-2 text-white"
          variant="primary"
          @click="resetDetailVoucherFields"
        >
          Cancelar
        </b-button>
        <b-button class="w-auto m-2" variant="success" type="submit">
          Guardar
        </b-button>
      </b-row>
    </Form>
  </b-modal>
</template>

<script>
import DetailVoucherServices from '@/Services/detailvoucher.Services'
import ProductVoucherServices from '@/Services/productvoucher.Services'
import { Form, Field, ErrorMessage } from 'vee-validate'
import { ref, inject } from 'vue'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  props: {
    ControlValeId: {
      type: Number,
      required: true
    }
  },
  components: {
    EasyDataTable: window['vue3-easy-data-table'],
    Form,
    Field,
    ErrorMessage
  },
  setup (props) {
    const swal = inject('$swal')
    const showModal = ref(false)
    const { getDetailVoucher, createDetailVoucher, deleteDetailVoucher } =
      DetailVoucherServices()
    const { getProductVoucher } = ProductVoucherServices()
    const detailVoucher = ref([])
    const productVoucher = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('agregar dato para busqueda')
    const QuantityState = ref(false)
    const ProductState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Proveeduria', to: '/Proveeduria' },
      { text: 'Detalle vale' }
    ])

    const detailVoucherFields = ref({
      detalleValeId: 0,
      productoId: null,
      importe: null,
      cantidad: null,
      total: null,
      controlValeId: props.ControlValeId,
      archivado: false
    })

    getProductVoucher(data => {
      productVoucher.value = data
      if (data.length === 0) {
        swal.fire({
          title: 'No se encuentran productos registrados!',
          text: 'No se encuentran productos registrados en el sistema, registre primero un producto para continuar.',
          icon: 'warning'
        })
      }
    })

    const detailVoucherFieldsBlank = ref(
      JSON.parse(JSON.stringify(detailVoucherFields))
    )
    const fields = ref([
      // { value: 'detalleValeId', text: 'ID', sortable: true },
      { value: 'cantidad', text: 'Cantidad' },
      // { value: 'precio', text: 'Precio' },
      { value: 'importe', text: 'Importe' },
      { value: 'total', text: 'Total' },
      { value: 'proV_Producto.nombre', text: 'Nombre del producto' },
      { value: 'actions', text: 'Acciones' }
    ])

    const resetDetailVoucherFields = () => {
      showModal.value = false
      detailVoucherFields.value = JSON.parse(
        JSON.stringify(detailVoucherFieldsBlank)
      )
      QuantityState.value = false
      ProductState.value = false
    }

    getDetailVoucher(props.ControlValeId, data => {
      detailVoucher.value = data
      if (detailVoucher.value.length > 0) {
        isloading.value = false
      } else {
        if (detailVoucher.value.length <= 0) {
          isloading.value = false
        }
      }
    })

    const onFiltered = filteredItems => {
      currentPage.value = 1
    }

    const validateQuantity = () => {
      if (!detailVoucherFields.value.cantidad) {
        QuantityState.value = false
        return 'Este campo es requerido'
      }
      QuantityState.value = true
      return true
    }

    const validateProduct = () => {
      if (!detailVoucherFields.value.productoId) {
        ProductState.value = false
        return 'Este campo es requerido'
      }
      ProductState.value = true
      return true
    }

    // pone mis cambios de mis campos vacios de nuevo
    const refreshTable = () => {
      isloading.value = true
      getDetailVoucher(props.ControlValeId, data => {
        detailVoucher.value = data
        if (detailVoucher.value.length > 0) {
          isloading.value = false
        } else {
          if (detailVoucher.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }
    const addDetailVoucher = () => {
      createDetailVoucher(detailVoucherFields.value, data => {
        refreshTable()
        swal.fire({
          title: '¡Detalle vale registrado correctamente!',
          text: 'El detalle vale se ha registrado al sistema satisfactoriamente.',
          icon: 'success'
        })
      })
      showModal.value = false
      resetDetailVoucherFields()
    }
    const RemoveDetailVoucher = WorksStatusId => {
      isloading.value = true
      swal
        .fire({
          title: '¿Estas seguro?',
          text: 'No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Si, Archivar detalle vale!',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            deleteDetailVoucher(WorksStatusId, data => {
              refreshTable()
            })
            swal.fire({
              title: '¡Detalle vale archivado!',
              text: 'El detalle vale ha sido archivado satisfactoriamente .',
              icon: 'success'
            })
          } else {
            isloading.value = false
          }
        })
    }
    return {
      detailVoucher,
      productVoucher,
      detailVoucherFields,
      breadcrumbItems,
      showModal,
      perPage,
      currentPage,
      filter,
      perPageSelect,
      isloading,
      searchValue,
      searchField,
      detailVoucherFieldsBlank,
      fields,
      QuantityState,
      ProductState,

      onFiltered,
      addDetailVoucher,
      refreshTable,
      RemoveDetailVoucher,
      validateQuantity,
      validateProduct,
      resetDetailVoucherFields
    }
  }
}
</script>

<style></style>
