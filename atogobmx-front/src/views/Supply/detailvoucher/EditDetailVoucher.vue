<template>
  <b-card class="m-2">
    <b-card>
      <div>
        <h3>Editar vale</h3>
      </div>
      <Form @submit="onUpdateDetailVoucher">
        <b-row cols="2">
          <!--Agregar cantidad -->
          <b-col>
            <b-form-group class="mt-3" label="Cantidad">
              <Field name="QuantityField" :rules="validateQuantity" as="number">
                <b-form-input
                  v-model="detailVoucher.cantidad"
                  :state="QuantityState"
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
          <!--Agregar medida -->
          <b-col>
            <b-form-group class="mt-3" label="Medida">
              <b-form-input v-model="detailVoucher.medida" >
              </b-form-input>
            </b-form-group>
          </b-col>
          <!--Agregar Precio -->
          <b-col>
            <b-form-group class="mt-3" label="Precio">
              <b-form-input v-model="detailVoucher.precio" type="number">
              </b-form-input>
            </b-form-group>
          </b-col>
          <!--Agregar importe -->
          <b-col>
            <b-form-group class="mt-3" label="Importe">
              <b-form-input v-model="detailVoucher.importe" type="number">
              </b-form-input>
            </b-form-group>
          </b-col>
          <!--Agregar Total -->
          <b-col>
            <b-form-group class="mt-3" label="Total">
              <b-form-input v-model="detailVoucher.subtotal" type="number">
              </b-form-input>
            </b-form-group>
          </b-col>
          <!--Agregar Producto-->
          <b-col>
            <b-form-group class="mt-3" label="Producto: ">
              <Field name="ProductField" :rules="validateProduct" as="text">
                <b-form-select
                  v-model="detailVoucher.productoId"
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
            to="/ServiciosPublicos/DetalleVale/list"
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
import DetailVoucherServices from '@/Services/detailvoucher.Services'
import ProductVoucherServices from '@/Services/productvoucher.Services'
import { ref, inject } from 'vue'
import { useRoute, useRouter } from 'vue-router'
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
    const { getDetailVoucherById, updateDetailVoucher } =
      DetailVoucherServices()
    const { getProductVoucher } = ProductVoucherServices()
    const detailVoucher = ref([])
    const productVoucher = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const QuantityState = ref(false)
    const IVAState = ref(false)
    const ProductState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Detalle vale', to: '/ServiciosPublicos/DetalleVale/list' },
      { text: 'Editar-Detalle vale' }
    ])
    const onUpdateDetailVoucher = () => {
      updateDetailVoucher(detailVoucher.value, data => {})
      swal
        .fire({
          title: '¡Detalle vale modificado correctamente!',
          text: 'El detalle vale se ha modificado  satisfactoriamente.',
          icon: 'success'
        })
        .then(result => {
          if (result.isConfirmed) {
            redirect.push('/ServiciosPublicos/DetalleVale/list')
          }
        })
    }
    getDetailVoucherById(router.params.DetalleValeId, data => {
      detailVoucher.value = data
    })

    getProductVoucher(data => {
      productVoucher.value = data
      // if (data.length === 0) {
      //   swal.fire({
      //     title: 'No se encuentra un tipo de producto registrado!',
      //     text: 'No se encuentra tipo de producto registrado en el departamento seleccionado, registre primero un tipo de producto vale para continuar',
      //     icon: 'warning'
      //   })
      // }
    })

    const validateQuantity = () => {
      if (!detailVoucher.value.cantidad) {
        validateState()
        return 'Este campo es requerido'
      }

      if (!/^[0-9]+$/i.test(detailVoucher.value.cantidad)) {
        QuantityState.value = false
        return 'Este campo solo puede contener numeros'
      }
      validateState()
      return true
    }

    const validateIVA = () => {
      if (!detailVoucher.value.iva) {
        validateState()
        return 'Este campo es requerido'
      }

      if (!/^[0-9]+$/i.test(detailVoucher.value.iva)) {
        IVAState.value = false
        return 'Este campo solo puede contener numeros'
      }
      validateState()
      return true
    }

    const validateProduct = () => {
      if (!detailVoucher.value.productoId) {
        ProductState.value = false
        return 'Este campo es requerido'
      }
      ProductState.value = false
      return true
    }

    const validateState = () => {
      // eslint-disable-next-line no-unneeded-ternary
      QuantityState.value = detailVoucher.value.cantidad === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      IVAState.value = detailVoucher.value.iva === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      ProductState.value = detailVoucher.value.productoId === '' ? false : true
    }

    return {
      detailVoucher,
      productVoucher,
      breadcrumbItems,
      QuantityState,
      IVAState,
      ProductState,

      onUpdateDetailVoucher,
      validateQuantity,
      validateIVA,
      validateProduct
    }
  }
}
</script>

<style></style>
