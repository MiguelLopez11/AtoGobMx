<template>
  <b-card class="m-2">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-card>
      <div>
        <h3>Editar estatus vale</h3>
      </div>
      <Form @submit="onUpdateStatusVoucher">
        <b-row cols="2">
          <!--Agregar Nombre-->
          <b-col>
            <b-form-group class="mt-3" label="Nombre">
              <Field name="NameField" :rules="validateName" as="text">
                <b-form-input
                  v-model="statusVoucher.nombre"
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
                <b-form-textarea
                  v-model="statusVoucher.descripcion"
                ></b-form-textarea>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="col-1 m-2 text-white"
            variant="primary"
            to="/ServiciosPublicos/EstatusVale/list"
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
import StatusVoucherServices from '@/Services/statusvoucher.Services'
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
    const { getStatusVoucherById, updateStatusVoucher } = StatusVoucherServices()
    const statusVoucher = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const NameState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Estatus', to: '/ServiciosPublicos/EstatusVale/list' },
      { text: 'Editar-EstatusOP' }
    ])
    const onUpdateStatusVoucher = () => {
      updateStatusVoucher(statusVoucher.value, data => {})
      swal
        .fire({
          title: '¡Estatus modificado correctamente!',
          text: 'El EstatusOP se ha modificado  satisfactoriamente.',
          icon: 'success'
        })
        .then(result => {
          if (result.isConfirmed) {
            redirect.push('/ServiciosPublicos/EstatusVale/list')
          }
        })
    }
    getStatusVoucherById(router.params.EstatusValeId, data => {
      statusVoucher.value = data
    })

    const validateName = () => {
      if (!statusVoucher.value.nombre) {
        validateState()
        return 'Este campo es requerido'
      }
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(statusVoucher.value.nombre)) {
        NameState.value = false
        return 'El nombre de estatus de obras publicas solo puede contener letras'
      }
      validateState()
      return true
    }

    const validateState = () => {
      // eslint-disable-next-line no-unneeded-ternary
      NameState.value = statusVoucher.value.nombre === '' ? false : true
    }

    return {
      statusVoucher,
      breadcrumbItems,
      NameState,

      onUpdateStatusVoucher,
      validateName,
      validateState
    }
  }
}
</script>

<style></style>
