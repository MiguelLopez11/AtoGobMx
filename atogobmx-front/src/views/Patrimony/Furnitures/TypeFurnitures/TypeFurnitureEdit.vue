<template>
  <b-card class="m-2">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-card>
      <div>
        <h3>Editar Categoria Mobiliario</h3>
      </div>
      <Form @submit="onUpdateTypeFurniture()">
        <b-row cols="3">
          <b-col>
            <b-form-group class="mt-3" label="Nombre">
              <Field
                name="NameField"
                :rules="validateName"
                as="text"
              >
                <b-form-input
                  v-model="typeFurniture.nombre"
                  :state="nameState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage class="text-danger" name="NameField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group
              class="mt-3"
              label="Descripción"
            >
              <b-form-input
                v-model="typeFurniture.descripcion"
              />
            </b-form-group>
          </b-col>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="col-1 m-2 text-white"
            variant="primary"
            to="/PatrimonioMunicipal/CategoriasMobiliario/list"
            type="reset"
          >
            Cancelar</b-button
          >
          <b-button type="success" class="col-1 m-2" variant="success">
            Guardar
          </b-button>
        </b-row>
      </Form>
    </b-card>
  </b-card>
</template>

<script>
import FurnitureServices from '@/Services/furniture.Services'
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
    const { getTypeFurniture, updateTypeFurniture } = FurnitureServices()
    const swal = inject('$swal')
    // const $toast = useToast()
    const typeFurniture = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const nameState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Categorias Mobiliarios', to: '/PatrimonioMunicipal/CategoriasMobiliario/list' },
      { text: 'Editar Categoria Mobiliario' }
    ])
    const onUpdateTypeFurniture = () => {
      updateTypeFurniture(typeFurniture.value, data => {
        swal.fire({
          title: 'Categoria mobiliario modificado correctamente!',
          text: 'La categoria mobiliario se ha modificado  satisfactoriamente.',
          icon: 'success'
        }).then(result => {
          if (result.isConfirmed) {
            redirect.push('/PatrimonioMunicipal/CategoriasMobiliario/list')
          }
        })
      })
    }
    getTypeFurniture(router.params.TipoMobiliarioId, data => {
      typeFurniture.value = data
    })
    const validateName = () => {
      if (!typeFurniture.value.nombre) {
        validateState()
        return 'Este campo es requerido'
      }
      // eslint-disable-next-line no-useless-escape
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(typeFurniture.value.nombre)) {
        validateState()
        return 'El nombre del area solo puede contener letras'
      }
      validateState()
      return true
    }
    const validateState = () => {
      nameState.value = typeFurniture.value.nombre !== '' && /^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(typeFurniture.value.nombre)
      return ''
    }
    return {
      typeFurniture,
      breadcrumbItems,
      //   router

      onUpdateTypeFurniture,
      validateName,
      validateState,
      nameState
    }
  }
}
</script>

<style></style>
