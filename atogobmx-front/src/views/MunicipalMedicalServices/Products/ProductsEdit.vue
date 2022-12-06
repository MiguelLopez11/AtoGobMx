<template>
  <b-card class="m-3">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <Form @submit="onUpdateProduct">
      <b-row cols="2">
        <b-col>
          <b-form-group class="mt-3" label="Nombre">
            <Field name="NameField" :rules="validateName" as="text">
              <b-form-input v-model="product.nombre" :state="nameState">
              </b-form-input>
            </Field>
            <ErrorMessage class="text-danger" name="NameField"></ErrorMessage>
          </b-form-group>
        </b-col>
        <b-col>
          <b-form-group class="mt-3" label="Contenido">
            <Field name="ContentField" :rules="validateContent" as="text">
              <b-form-input
                v-model="product.contenido"
                :state="contentState"
              >
              </b-form-input>
            </Field>
            <ErrorMessage
              class="text-danger"
              name="ContentField"
            ></ErrorMessage>
          </b-form-group>
        </b-col>
        <b-col>
          <b-form-group class="mt-3" label="Fecha de vencimiento">
            <Field
              name="ContentField"
              :rules="validateExpirationDate"
              as="text"
            >
              <Datepicker
                v-model="product.fechaVencimiento"
                locale="es"
                autoApply
                :enableTimePicker="false"
                :state="expirationDateState"
              >
              </Datepicker>
            </Field>
            <ErrorMessage
              class="text-danger"
              name="ContentField"
            ></ErrorMessage>
          </b-form-group>
        </b-col>
        <b-col>
          <b-form-group class="mt-3" label="Stock (Cantidad disponible)">
            <Field name="ContentField" :rules="validateStock" as="text">
              <b-form-input
                v-model="product.cantidadDisponible"
                :state="stockState"
              >
              </b-form-input>
            </Field>
            <ErrorMessage
              class="text-danger"
              name="ContentField"
            ></ErrorMessage>
          </b-form-group>
        </b-col>
      </b-row>
      <b-row align-h="end">
        <b-button
          class="w-auto m-2 text-white"
          variant="primary"
          to="/ServiciosMedicos/Productos/list"
        >
          Cancelar
        </b-button>
        <b-button
          class="w-auto m-2"
          variant="success"
          type="submit"
        >
          Guardar
        </b-button>
      </b-row>
    </Form>
  </b-card>
</template>

<script>
import MedicalProductsServices from '@/Services/medicalProducts.Services'
import Datepicker from '@vuepic/vue-datepicker'
import { ref, inject } from 'vue'
import { Form, Field, ErrorMessage } from 'vee-validate'
import { useRoute, useRouter } from 'vue-router'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    Datepicker,
    Form,
    Field,
    ErrorMessage
  },
  setup () {
    const { getProduct, updateProduct } = MedicalProductsServices()
    const swal = inject('$swal')
    const product = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const nameState = ref(false)
    const contentState = ref(false)
    const expirationDateState = ref(false)
    const stockState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Productos', to: '/ServiciosMedicos/Productos/list' },
      { text: 'Editar Producto' }
    ])

    const onUpdateProduct = () => {
      updateProduct(product.value, data => {})
      swal
        .fire({
          title: 'Producto modificado correctamente!',
          text: 'El producto se ha modificado al sistema satisfactoriamente.',
          icon: 'success'
        })
        .then(result => {
          if (result.isConfirmed) {
            redirect.push('/ServiciosMedicos/Productos/list')
          }
        })
    }
    getProduct(router.params.ProductoId, data => {
      product.value = data
      validateState()
    })
    const validateName = () => {
      if (!product.value.nombre) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateContent = () => {
      if (!product.value.contenido) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateExpirationDate = () => {
      if (!product.value.fechaVencimiento) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateStock = () => {
      if (!product.value.cantidadDisponible) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateState = () => {
      nameState.value = product.value.nombre !== '' || product.value.nombre !== null
      contentState.value = product.value.contenido !== '' || product.value.contenido !== null
      expirationDateState.value = product.value.fechaVencimiento !== null
      stockState.value = product.value.cantidadDisponible !== null
      return ''
    }
    return {
      product,
      breadcrumbItems,
      nameState,
      contentState,
      expirationDateState,
      stockState,

      onUpdateProduct,
      validateName,
      validateContent,
      validateExpirationDate,
      validateStock
    }
  }
}
</script>

<style></style>
