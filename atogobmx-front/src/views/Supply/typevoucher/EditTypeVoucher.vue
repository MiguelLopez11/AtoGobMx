<template>
  <b-card class="m-2">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-card>
      <div>
        <h3>Editar tipo de vale</h3>
      </div>
      <Form @submit="onUpdateTypeVoucher">
        <b-row cols="2">
          <!--Agregar Nombre-->
          <b-col>
            <b-form-group class="mt-3" label="Nombre">
              <Field name="NameField" :rules="validateName" as="text">
                <b-form-input
                  v-model="typeVoucher.nombre"
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
                  v-model="typeVoucher.descripcion"
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
            to="/ServiciosPublicos/TipoVale/list"
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
import TypeVoucherServices from '@/Services/typevoucher.Services'
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
    const { getTypeVoucherById, updateTypeVoucher } = TypeVoucherServices()
    const typeVoucher = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const NameState = ref(false)
    const DescriptionState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Tipo de vale', to: '/ServiciosPublicos/TipoVale/list' },
      { text: 'Editar-TipoVale' }
    ])
    const onUpdateTypeVoucher = () => {
      updateTypeVoucher(typeVoucher.value, data => {})
      swal
        .fire({
          title: '¡Tipo de vale modificado correctamente!',
          text: 'El Tipo de vale se ha modificado  satisfactoriamente.',
          icon: 'success'
        })
        .then(result => {
          if (result.isConfirmed) {
            redirect.push('/ServiciosPublicos/TipoVale/list')
          }
        })
    }
    getTypeVoucherById(router.params.TipoId, data => {
      typeVoucher.value = data
    })

    const validateName = () => {
      if (!typeVoucher.value.nombre) {
        validateState()
        return 'Este campo es requerido'
      }
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(typeVoucher.value.nombre)) {
        NameState.value = false
        return 'El nombre de estatus de obras publicas solo puede contener letras'
      }
      validateState()
      return true
    }

    const validateDescription = () => {
      if (!typeVoucher.value.descripcion) {
        validateState()
        return 'Este campo es requerido'
      }
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(typeVoucher.value.descripcion)) {
        DescriptionState.value = false
        return 'La descripcion solo puede contener letras'
      }
      validateState()
      return true
    }

    const validateState = () => {
      // eslint-disable-next-line no-unneeded-ternary
      NameState.value = typeVoucher.value.nombre === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      DescriptionState.value = typeVoucher.value.descripcion === '' ? false : true
    }

    return {
      typeVoucher,
      breadcrumbItems,
      NameState,
      DescriptionState,

      onUpdateTypeVoucher,
      validateName,
      validateState,
      validateDescription
    }
  }
}
</script>

<style></style>
