<template>
  <b-card class="m-2">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-car>
      <div>
        <h3>Editar Direccion Cementerios</h3>
      </div>
      <Form @submit="onUpdateAddressCementeryService">
        <b-row cols="2">
          <!-- 1 -->
          <b-col>
            <b-form-group class="mt-3" label="Nombre del cementerio">
              <Field
                name="NameCementeryField"
                :rules="validateNameCementery"
                as="text"
              >
                <b-form-input
                  v-model="addressCementeryService.nombreCementerio"
                  :state="NameCementeryState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="NameCementeryField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!-- 2 -->
          <b-col>
            <b-form-group class="mt-3" label="Municipio">
              <Field
                name="MunicipalityField"
                :rules="validateMunicipality"
                as="text"
              >
                <b-form-input
                  v-model="addressCementeryService.municipio"
                  :state="MunicipalityState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="MunicipalityField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!-- 3 -->
          <b-col>
            <b-form-group class="mt-3" label="Localidad">
              <Field name="LocationField" :rules="validateLocation" as="text">
                <b-form-input
                  v-model="addressCementeryService.localidad"
                  :state="LocationState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="LocationField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!-- 4 -->
          <b-col>
            <b-form-group class="mt-3" label="Calle">
              <Field name="StreetField" :rules="validateStreet" as="text">
                <b-form-input
                  v-model="addressCementeryService.calle"
                  :state="StreetState"
                  type="text"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="StreetField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!-- 5 -->
          <b-col>
            <b-form-group class="mt-3" label="Numero exterior">
              <Field
                name="NumberOutsideField"
                :rules="validateStreet"
                as="text"
              >
                <b-form-input
                  v-model="addressCementeryService.numeroExterior"
                  :state="NumberOutsideState"
                  type="number"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="NumberOutsideField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
        </b-row>

        <b-row align-h="end">
          <b-button
            class="col-1 m-2 text-white"
            variant="primary"
            to="/DireccionCementerios/list"
            type="reset"
          >
            Cancelar</b-button
          >
          <b-button type="success" class="col-1 m-2" variant="success">
            Guardar
          </b-button>
        </b-row>
      </Form>
    </b-car>
  </b-card>
</template>

<script>
import AddressCementeryService from '@/Services/cementery.Services'
import { Form, Field, ErrorMessage } from 'vee-validate'
import { ref, inject } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    Form,
    Field,
    ErrorMessage
  },
  setup () {
    const swal = inject('$swal')
    const { getAddressCementeryById, updateAddressCementery } = AddressCementeryService()
    const addressCementeryService = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const NameCementeryState = ref(false)
    const MunicipalityState = ref(false)
    const LocationState = ref(false)
    const StreetState = ref(false)
    const NumberOutsideState = ref(false)
    const breadcrumbItems = ref([
      { Text: 'Inicio', to: '/' },
      { Text: 'Inventario cementerio', to: '/DireccionCementerios/list' },
      { Text: 'Editar-Direccion Cementerios' }
    ])

    const onUpdateAddressCementeryService = () => {
      updateAddressCementery(addressCementeryService.value, data => {})
      swal
        .fire({
          title: '¡Direccion cementerio modificado correctamente!',
          text: 'La direccion cementerio se ha modificado  satisfactoriamente.',
          icon: 'success'
        })
        .then(result => {
          if (result.isConfirmed) {
            redirect.push('/DireccionCementerios/list')
          }
        })
    }

    getAddressCementeryById(router.params.CementeriosId, data => {
      addressCementeryService.value = data
    })

    const validateNameCementery = () => {
      if (!addressCementeryService.value.nombreCementerio) {
        validateState()
        return 'Este campo es requerido'
      }

      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(addressCementeryService.value.nombreCementerio)) {
        NameCementeryState.value = false
        return 'Este campo solo puede contener letras'
      }

      validateState()
      return true
    }

    const validateMunicipality = () => {
      if (!addressCementeryService.value.municipio) {
        validateState()
        return 'Este campo es requerido'
      }

      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(addressCementeryService.value.municipio)) {
        MunicipalityState.value = false
        return 'Este campo solo puede contener letras'
      }

      validateState()
      return true
    }

    const validateLocation = () => {
      if (!addressCementeryService.value.localidad) {
        validateState()
        return 'Este campo es requerido'
      }

      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(addressCementeryService.value.localidad)) {
        LocationState.value = false
        return 'Este campo solo puede contener letras'
      }

      validateState()
      return true
    }

    const validateStreet = () => {
      if (!addressCementeryService.value.calle) {
        validateState()
        return 'Este campo es requerido'
      }

      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(addressCementeryService.value.calle)) {
        StreetState.value = false
        return 'Este campo solo puede contener letras'
      }

      validateState()
      return true
    }

    const validateNumberOutside = () => {
      if (!addressCementeryService.value.numeroExterior) {
        validateState()
        return 'Este campo es requerido'
      }

      if (!/^[0-9]+$/i.test(addressCementeryService.value.numeroExterior)) {
        NumberOutsideState.value = false
        return 'Este campo solo puede contener numeros'
      }

      validateState()
      return true
    }

    const validateState = () => {
      // eslint-disable-next-line no-unneeded-ternary
      NameCementeryState.value = addressCementeryService.value.nombreCementerio !== ''
      // eslint-disable-next-line no-unneeded-ternary
      MunicipalityState.value = addressCementeryService.value.municipio !== ''
      // eslint-disable-next-line no-unneeded-ternary
      LocationState.value = addressCementeryService.value.localidad !== ''
      // eslint-disable-next-line no-unneeded-ternary
      StreetState.value = addressCementeryService.value.calle !== ''
      // eslint-disable-next-line no-unneeded-ternary
      NumberOutsideState.value = addressCementeryService.value.numeroExterior !== ''
    }

    return {
      addressCementeryService,
      breadcrumbItems,
      NameCementeryState,
      MunicipalityState,
      LocationState,
      StreetState,
      NumberOutsideState,

      onUpdateAddressCementeryService,
      validateNameCementery,
      validateMunicipality,
      validateLocation,
      validateStreet,
      validateNumberOutside,
      validateState

    }
  }
}
</script>

<style></style>
