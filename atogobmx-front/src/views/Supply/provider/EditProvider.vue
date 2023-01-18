<template>
  <b-card class="m-2">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-card>
      <div>
        <h3>Editar proveedor</h3>
      </div>
      <Form @submit="onUpdateProvider">
        <b-row cols="2">
          <!--Agregar Nombre-->
          <b-col>
            <b-form-group class="mt-3" label="Nombre">
              <Field name="NameField" :rules="validateName" as="text">
                <b-form-input v-model="provider.nombre" :state="NameState">
                </b-form-input>
              </Field>
              <ErrorMessage class="text-danger" name="NameField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--Agregar RFC-->
          <b-col>
            <b-form-group class="mt-3" label="RFC">
              <Field name="RFCField" :rules="validateRFC" as="text">
                <b-form-input v-model="provider.rfc" :state="RFCState">
                </b-form-input>
              </Field>
              <ErrorMessage class="text-danger" name="RFCField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--Agregar Representante legal-->
          <b-col>
            <b-form-group class="mt-3" label="Representante legal">
              <Field
                name="LegalRepresentativeField"
                :rules="validateLegalRepresentative"
                as="text"
              >
                <b-form-input
                  v-model="provider.representanteLegal"
                  :state="LegalRepresentativeState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="LegalRepresentativeField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--Agregar Direccion-->
          <b-col>
            <b-form-group class="mt-3" label="Direccion">
              <Field name="DirectionField" :rules="validateDirection" as="text">
                <b-form-input
                  v-model="provider.direccion"
                  :state="DirectionState"
                ></b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="DirectionField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--Agregar Telefono-->
          <b-col>
            <b-form-group class="mt-3" label="Telefono">
              <Field name="PhoneField" :rules="validatePhone" as="number">
                <b-form-input
                  v-model="provider.telefono"
                  :state="PhoneState"
                  type="number"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="PhoneField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--Agregar email-->
          <b-col>
            <b-form-group class="mt-3" label="Correo electronico">
              <Field
                name="emailField"
                type="email"
                :rules="validateEmail"
                as="text"
              >
                <b-form-input v-model="provider.email" :state="emailState" />
              </Field>
              <ErrorMessage name="emailField">
                <span class="text-danger">{{ emailMessage }} </span>
                <i class="bi bi-exclamation-circle"></i
              ></ErrorMessage>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="col-1 m-2 text-white"
            variant="primary"
            to="/ServiciosPublicos/Proveedor/list"
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
import ProviderServices from '@/Services/provider.Services'
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
    const { getProviderById, updateProvider } = ProviderServices()
    const provider = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const NameState = ref(false)
    const RFCState = ref(false)
    const LegalRepresentativeState = ref(false)
    const DirectionState = ref(false)
    const PhoneState = ref(false)
    const emailState = ref(false)
    const emailMessage = ref('')
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Proveedor', to: '/ServiciosPublicos/Proveedor/list' },
      { text: 'Editar-Proveedor' }
    ])
    const onUpdateProvider = () => {
      updateProvider(provider.value, data => {})
      swal
        .fire({
          title: '¡Proveedor modificado correctamente!',
          text: 'El proveedor se ha modificado  satisfactoriamente.',
          icon: 'success'
        })
        .then(result => {
          if (result.isConfirmed) {
            redirect.push('/ServiciosPublicos/Proveedor/list')
          }
        })
    }
    getProviderById(router.params.ProveedorId, data => {
      provider.value = data
    })

    const validateName = () => {
      if (!provider.value.nombre) {
        validateState()
        return 'Este campo es requerido'
      }
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(provider.value.nombre)) {
        NameState.value = false
        return 'El nombre de estatus de obras publicas solo puede contener letras'
      }
      validateState()
      return true
    }

    const validateRFC = () => {
      if (!provider.value.rfc) {
        validateState()
        return 'Este campo es requerido'
      }
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ 0-9]+$/i.test(provider.value.rfc)) {
        RFCState.value = false
        return 'El RFC solo puede contener letras'
      }
      validateState()
      return true
    }

    const validateLegalRepresentative = () => {
      if (!provider.value.representanteLegal) {
        validateState()
        return 'Este campo es requerido'
      }
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(provider.value.representanteLegal)) {
        LegalRepresentativeState.value = false
        return 'El representante solo puede contener letras'
      }
      validateState()
      return true
    }

    const validateDirection = () => {
      if (!provider.value.direccion) {
        validateState()
        return 'Este campo es requerido'
      }
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ .# 0-9]+$/i.test(provider.value.direccion)) {
        DirectionState.value = false
        return 'La direccion solo puede contener letras y numeros'
      }
      validateState()
      return true
    }

    const validatePhone = () => {
      if (!provider.value.telefono) {
        validateState()
        return 'Este campo es requerido'
      }

      if (!/^([0-9]{10})+$/i.test(provider.value.telefono)) {
        PhoneState.value = false
        return 'Este campo solo puede contener numeros'
      }

      validateState()
      return true
    }

    const validateEmail = () => {
      if (!provider.value.email) {
        validateState()
        emailMessage.value = 'Este campo es requerido'
        return emailMessage.value
      }
      const regex = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i
      if (!regex.test(provider.value.email)) {
        emailState.value = false
        emailMessage.value = 'Este campo debe ser un correo electrónico válido'
        return emailMessage.value
      }
      emailState.value = true
      return true
    }

    const validateState = () => {
      // eslint-disable-next-line no-unneeded-ternary
      NameState.value = provider.value.nombre === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      RFCState.value = provider.value.rfc === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      LegalRepresentativeState.value = provider.value.representanteLegal === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      DirectionState.value = provider.value.direccion === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      PhoneState.value = provider.value.telefono === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      emailState.value = provider.value.email === '' ? false : true
    }

    return {
      provider,
      breadcrumbItems,
      emailState,
      NameState,
      RFCState,
      LegalRepresentativeState,
      DirectionState,
      PhoneState,

      onUpdateProvider,
      validateName,
      validateState,
      validateRFC,
      validateLegalRepresentative,
      validateDirection,
      validatePhone,
      validateEmail
    }
  }
}
</script>

<style></style>
