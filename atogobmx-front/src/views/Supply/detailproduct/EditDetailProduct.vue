<template>
  <b-card class="m-2">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-card>
      <div>
        <h3>Editar Detalle Producto</h3>
      </div>
      <Form @submit="onUpdateDetailProduct">
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
                  v-model="detailProduct.detalleValeId"
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
                  v-model="detailProduct.productoId"
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
            class="col-1 m-2 text-white"
            variant="primary"
            to="/DetalleProducto/list"
            type="reset"
          >
            Cancelar
          </b-button>
          <b-button type="success" class="col-1 m-2" variant="success">
            Guardar
          </b-button>
        </b-row>
      </Form>
    </b-card>
  </b-card>
</template>

<script>
import DetailProductServices from '@/Services/detailproduct.Services'
import DetailVoucherServices from '@/Services/detailvoucher.Services'
import ProductVoucherServices from '@/Services/productvoucher.Services'
import { ref, inject } from 'vue'
import { useRoute, useRouter } from 'vue-router'
// import { useToast } from 'vue-toast-notification'
import { Form, Field, ErrorMessage } from 'vee-validate'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    Form,
    Field,
    ErrorMessage
  },
  setup () {
    const swal = inject('$swal')
    const { getDetailProductById, updateDetailProduct } = DetailProductServices()
    const { getDetailVoucher } = DetailVoucherServices()
    const { getProductVoucher } = ProductVoucherServices()
    const detailProduct = ref([])
    const detailVoucher = ref([])
    const productVoucher = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const DetailVoucherState = ref(false)
    const ProductState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Departamento detalle producto', to: '/DetalleProducto/list' },
      { text: 'Editar-Detalle producto' }
    ])

    const onUpdateDetailProduct = () => {
      updateDetailProduct(detailProduct.value, data => {})
      swal
        .fire({
          title: '¡Detalle producto modificado correctamente!',
          text:
            'El detalle producto se ha modificado  satisfactoriamente.',
          icon: 'success'
        })
        .then(result => {
          if (result.isConfirmed) {
            redirect.push('/DetalleProducto/list')
          }
        })
    }

    getDetailProductById(router.params.DetalleProductoId, data => {
      detailProduct.value = data
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

    const validateProduct = () => {
      if (!detailProduct.value.productoId) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }

    const validateDetailVoucher = () => {
      if (!detailProduct.value.detalleValeId) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }

    const validateState = () => {
      // eslint-disable-next-line no-unneeded-ternary
      DetailVoucherState.value = detailProduct.value.productoId === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      ProductState.value = detailProduct.value.detalleValeId === '' ? false : true
    }

    return {
      detailProduct,
      detailVoucher,
      productVoucher,
      breadcrumbItems,
      DetailVoucherState,
      ProductState,

      onUpdateDetailProduct,
      validateProduct,
      validateState,
      validateDetailVoucher
    }
  }
}
</script>

<style></style>
