<template>
  <b-card>
    <b-row align-h="end" class="mb-3 mr-1">
      <b-form-input
        size="lg"
        style="width: 350px"
        v-model="searchValue"
        type="search"
        placeholder="Buscar..."
      >
      </b-form-input>
      <b-button
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
        <i class="bi bi-car-front-fill" />
        Agregar
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
      :items="detailProduct"
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
            @click="RemoveDetailProduct(items.detalleProductoId)"
            class="m-1"
            variant="outline-danger"
          >
            <i class="bi bi-trash3"> Eliminar </i>
          </b-dropdown-item>
        </b-dropdown>
      </template>
    </EasyDataTable>
    <b-modal
      v-model="showModal"
      title="Registrar detalle producto"
      size="xl"
      centered
      button-size="lg"
      hide-footer
    >
      <Form @submit="addDetailProduct">
        <b-row cols="2">
          <!--Agregar Producto-->
          <b-col>
            <b-form-group class="mt-3" label="Producto: ">
              <Field name="ProductField" :rules="validateProduct" as="text">
                <b-form-select
                  v-model="detailProductFields.productoId"
                  autofocus
                  :options="a"
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
            @click="resetDetailProductFields()"
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
import { Form, Field, ErrorMessage } from 'vee-validate'
import { ref, inject } from 'vue'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    Form,
    Field,
    ErrorMessage,
    EasyDataTable: window['vue3-easy-data-table']
  },
  props: {
    DetalleValeId: {
      type: Number,
      required: true
    }
  },
  setup (props) {
    const swal = inject('$swal')
    const {
      getProductsPrescriptionByRecetaId,
      createProductPrescription,
      deleteProductPrescription
    } = DetailProductServices()
    // const { getProducts } = MedicalProductsServices()
    // const redirect = useRouter()
    const detailProduct = ref([])
    // const products = ref([])
    const productState = ref(false)
    const amountState = ref(false)
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('marca')
    const showModal = ref(false)
    const productPrescriptionFields = ref({
      detalleProductoId: 0,
      detalleValeId: props.DetalleValeId,
      productoId: null,
      archivado: false
    })
    const productPrescriptionFieldsBlank = ref(
      JSON.parse(JSON.stringify(productPrescriptionFields.value))
    )
    const fields = ref([
      { value: 'producto.nombre', text: 'Medicamento' },
      { value: 'producto.contenido', text: 'Contenido' },
      { value: 'cantidad', text: 'cantidad' },
      { value: 'descripcion', text: 'Sugerencias' },
      { value: 'actions', text: 'Acciones' }
    ])
    getProductsPrescriptionByRecetaId(props.RecetaId, data => {
      detailProduct.value = data
      if (detailProduct.value.length > 0) {
        isloading.value = false
      } else {
        if (detailProduct.value.length <= 0) {
          isloading.value = false
        }
      }
    })
    // getProducts(data => {
    //   products.value = data
    //   if (data.length === 0) {
    //     swal.fire({
    //       title: '¡No se encuentran Productos!',
    //       text: 'Registre un producto al sistema para continuar',
    //       icon: 'warning'
    //     })
    //   }
    // })
    const refreshTable = () => {
      isloading.value = true
      getProductsPrescriptionByRecetaId(props.RecetaId, data => {
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
      createProductPrescription(productPrescriptionFields.value, data => {
        swal
          .fire({
            title: 'Producto registrado correctamente!',
            text: 'El producto se ha registrado al sistema satisfactoriamente.',
            icon: 'success'
          })
          .then(result => {
            if (result.isConfirmed) {
              resetDetailProductFields()
              refreshTable()
            }
          })
      })
    }
    const resetDetailProductFields = () => {
      productPrescriptionFields.value = JSON.parse(
        JSON.stringify(productPrescriptionFieldsBlank.value)
      )
      showModal.value = false
      productState.value = false
      amountState.value = false
    }
    const RemoveDetailProduct = productPrescriptionId => {
      isloading.value = true
      swal
        .fire({
          title: '¿Estas seguro?',
          text: 'No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Si, eliminar producto!',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            swal
              .fire({
                title: 'Producto archivado!',
                text: 'El producto ha sido archivado satisfactoriamente .',
                icon: 'success'
              })
              .then(result => {
                if (result.isConfirmed) {
                  deleteProductPrescription(
                    props.RecetaId,
                    productPrescriptionId,
                    data => {
                      refreshTable()
                    }
                  )
                }
              })
          } else {
            isloading.value = false
          }
        })
    }
    const onFiltered = filteredItems => {
      currentPage.value = 1
    }
    // VALIDATIONS
    const validateProduct = () => {
      if (!productPrescriptionFields.value.productoId) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateAmount = () => {
      if (!productPrescriptionFields.value.cantidad) {
        validateState()
        return 'Este campo es requerido'
      }
      if (productPrescriptionFields.value.cantidad < 1) {
        validateState()
        return 'Se necesita al menos 1'
      }
      validateState()
      return true
    }
    const validateState = () => {
      productState.value = productPrescriptionFields.value.productoId !== null
      amountState.value =
        productPrescriptionFields.value.cantidad !== null &&
        productPrescriptionFields.value.cantidad > 0
    }
    return {
      detailProduct,
      // products,
      productState,
      amountState,
      fields,
      perPage,
      currentPage,
      filter,
      perPageSelect,
      productPrescriptionFieldsBlank,
      productPrescriptionFields,
      isloading,
      searchValue,
      searchField,
      showModal,

      onFiltered,
      addDetailProduct,
      refreshTable,
      RemoveDetailProduct,
      resetDetailProductFields,
      validateProduct,
      validateAmount
    }
  }
}
</script>

<style></style>
