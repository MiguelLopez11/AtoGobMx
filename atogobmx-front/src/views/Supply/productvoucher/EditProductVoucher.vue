<template>
  <b-card class="m-2">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-card>
      <div>
        <h3>Editar Producto</h3>
      </div>
      <Form @submit="onUpdateProductVoucher">
        <b-row cols="2">
          <!--Agregar Nombre-->
          <b-col>
            <b-form-group class="mt-3" label="Nombre">
              <Field name="NameField" :rules="validateName" as="text">
                <b-form-input
                  v-model="productVoucher.nombre"
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
                  v-model="productVoucher.descripcion"
                  :state="DescriptionState"
                ></b-form-input>
              </Field>
              <ErrorMessage class="text-danger" name="DescriptionField"></ErrorMessage>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="col-1 m-2 text-white"
            variant="primary"
            to="/Producto/list"
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
    const { getProductVoucherById, updateProductVoucher } = ProductVoucherServices()
    const productVoucher = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const NameState = ref(false)
    const DescriptionState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Departamento productos', to: '/Producto/list' },
      { text: 'Editar-Producto' }
    ])
    const onUpdateProductVoucher = () => {
      updateProductVoucher(productVoucher.value, data => {})
      swal
        .fire({
          title: '¡Producto modificado correctamente!',
          text: 'El producto se ha modificado  satisfactoriamente.',
          icon: 'success'
        })
        .then(result => {
          if (result.isConfirmed) {
            redirect.push('/Producto/list')
          }
        })
    }
    getProductVoucherById(router.params.ProductoId, data => {
      productVoucher.value = data
    })

    const validateName = () => {
      if (!productVoucher.value.nombre) {
        validateState()
        return 'Este campo es requerido'
      }
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(productVoucher.value.nombre)) {
        NameState.value = false
        return 'El nombre de estatus de obras publicas solo puede contener letras'
      }
      validateState()
      return true
    }

    const validateDescription = () => {
      if (!productVoucher.value.descripcion) {
        validateState()
        return 'Este campo es requerido'
      }
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(productVoucher.value.descripcion)) {
        DescriptionState.value = false
        return 'La descripcion solo puede contener letras'
      }
      validateState()
      return true
    }

    const validateState = () => {
      // eslint-disable-next-line no-unneeded-ternary
      NameState.value = productVoucher.value.nombre === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      DescriptionState.value = productVoucher.value.descripcion === '' ? false : true
    }

    return {
      productVoucher,
      breadcrumbItems,
      NameState,
      DescriptionState,

      onUpdateProductVoucher,
      validateName,
      validateState,
      validateDescription
    }
  }
}
</script>

<style></style>
