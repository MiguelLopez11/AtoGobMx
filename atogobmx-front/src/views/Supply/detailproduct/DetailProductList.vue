<template>
  <b-card class="m-2">
    <b-row align-h="end" class="mb-3 mr-1">
      <b-form-input
        size="lg"
        style="width: 350px"
        v-model="searchValue"
        type="search"
        placeholder="Buscar detalle producto..."
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
        Detalle producto
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
      :items="detailProduct"
      :rows-per-page="5"
      :search-field="searchField"
      :search-value="searchValue"
    >
      <template #header-actions="header">
        {{ header.text }}
      </template>
      <template #item-actions="items">
        <b-button
          @click="RemoveDetailProduct(items.detalleProductoId)"
          class="m-1"
          variant="outline-danger"
          ><i class="bi bi-trash3"></i
        ></b-button>
        <b-button
          class="m-1"
          variant="outline-warning"
          :to="{
            name: 'DetalleProducto-Edit',
            params: { DetalleProductoId: items.detalleProductoId }
          }"
          ><i class="bi bi-pencil-square"></i
        ></b-button>
      </template>
    </EasyDataTable>
    <b-modal
      id="modal-detailProduct"
      title="Agregar detalle producto"
      v-model="showModal"
      size="xl"
      hide-footer
      button-size="lg"
      lazy
    >
      <Form @submit="addDetailProduct">
        <b-row cols="2">
          <!--Agregar Detalle vale-->
          <b-col>
            <b-form-group class="mt-3" label="Detalle vale: ">
              <Field
                name="DetailVoucherField"
                :rules="validateDetailVoucher"
                as="text"
              >
                <b-form-select
                  v-model="detailProductFields.detalleValeId"
                  autofocus
                  :options="detailVoucher"
                  value-field="detalleValeId"
                  text-field="cantidad"
                  :state="DetailVoucherState"
                >
                </b-form-select>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="DetailVoucherField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--Agregar Producto-->
          <b-col>
            <b-form-group class="mt-3" label="Producto: ">
              <Field
                name="ProductField"
                :rules="validateProduct"
                as="text"
              >
                <b-form-select
                  v-model="detailProductFields.productoId"
                  autofocus
                  :options="productVoucher"
                  value-field="productoId"
                  text-field="nombre"
                  :state="ProductState"
                >
                </b-form-select>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="ProductField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            @click="resetDetailProductFields"
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
import DetailProductServices from '@/Services/detailproduct.Services'
import DetailVoucherServices from '@/Services/detailvoucher.Services'
import ProductVoucherServices from '@/Services/productvoucher.Services'
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
    const { getDetailProduct, createDetailProduct, deleteDetailProduct } = DetailProductServices()
    const { getDetailVoucher } = DetailVoucherServices()
    const { getProductVoucher } = ProductVoucherServices()
    const detailProduct = ref([])
    const detailVoucher = ref([])
    const productVoucher = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('agregardato para busqueda')
    const DetailVoucherState = ref(false)
    const ProductState = ref(false)
    const detailProductFields = ref({
      detalleProductoId: 0,
      productoId: null,
      detalleValeId: null,
      archivado: false
    })

    getDetailVoucher(data => {
      detailVoucher.value = data
      if (data.length === 0) {
        swal.fire({
          title: 'No se encuentra un tipo de detalle registrado!',
          text: 'No se encuentra tipo de detalle registrado en el departamento seleccionado, registre primero un tipo de detalle vale para continuar',
          icon: 'warning'
        })
      }
    })

    getProductVoucher(data => {
      productVoucher.value = data
      if (data.length === 0) {
        swal.fire({
          title: 'No se encuentra un tipo de producto registrado!',
          text: 'No se encuentra tipo de producto registrado en el departamento seleccionado, registre primero un tipo de producto vale para continuar',
          icon: 'warning'
        })
      }
    })

    const detailProductFieldsBlank = ref(
      JSON.parse(JSON.stringify(detailProductFields))
    )
    const fields = ref([
      { value: 'detalleProductoId', text: 'ID', sortable: true },
      { value: 'proV_DetalleVale.cantidad', text: 'Cantidad' },
      { value: 'proV_Producto.nombre', text: 'Producto' },
      { value: 'actions', text: 'Acciones' }
    ])

    const resetDetailProductFields = () => {
      showModal.value = false
      detailProductFields.value = JSON.parse(
        JSON.stringify(detailProductFieldsBlank)
      )
      DetailVoucherState.value = false
      ProductState.value = false
    }

    getDetailProduct(data => {
      detailProduct.value = data
      if (detailProduct.value.length > 0) {
        isloading.value = false
      } else {
        if (detailProduct.value.length <= 0) {
          isloading.value = false
        }
      }
    })

    const onFiltered = filteredItems => {
      currentPage.value = 1
    }

    const validateProduct = () => {
      if (!detailProductFields.value.productoId) {
        ProductState.value = false
        return 'Este campo es requerido'
      }
      ProductState.value = false
      return true
    }

    const validateDetailVoucher = () => {
      if (!detailProductFields.value.detalleValeId) {
        DetailVoucherState.value = false
        return 'Este campo es requerido'
      }
      DetailVoucherState.value = false
      return true
    }

    // pone mis cambios de mis campos vacios de nuevo
    const refreshTable = () => {
      isloading.value = true
      getDetailProduct(data => {
        detailProduct.value = data
        if (detailProduct.value.length > 0) {
          isloading.value = false
        } else {
          if (detailProduct.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }

    const addDetailProduct = () => {
      createDetailProduct(detailProductFields.value, data => {
        refreshTable()
        swal.fire({
          title: '¡Detalle producto registrado correctamente!',
          text: 'El detalle producto se ha registrado al sistema satisfactoriamente.',
          icon: 'success'
        })
      })
      showModal.value = false
      resetDetailProductFields()
    }

    const RemoveDetailProduct = WorksStatusId => {
      isloading.value = true
      swal.fire({
        title: '¿Estas seguro?',
        text: 'No podrás revertir esto!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, Archivar detalle producto!',
        cancelButtonText: 'Cancelar'
      })
        .then(result => {
          if (result.isConfirmed) {
            deleteDetailProduct(WorksStatusId, (data) => {
              refreshTable()
            })
            swal
              .fire({
                title: '¡Detalle producto archivado!',
                text: 'El detalle producto ha sido archivado satisfactoriamente .',
                icon: 'success'
              })
          } else {
            isloading.value = false
          }
        })
    }

    return {
      detailProduct,
      detailVoucher,
      productVoucher,
      detailProductFields,
      showModal,
      perPage,
      currentPage,
      filter,
      perPageSelect,
      isloading,
      searchValue,
      searchField,
      detailProductFieldsBlank,
      fields,
      DetailVoucherState,
      ProductState,

      onFiltered,
      addDetailProduct,
      refreshTable,
      RemoveDetailProduct,
      validateProduct,
      validateDetailVoucher,
      resetDetailProductFields
    }
  }
}
</script>

<style>

</style>
