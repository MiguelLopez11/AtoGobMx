<template>
  <b-card class="m-2">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-card>
      <div>
        <h3>Editar Obras Publicas </h3>
      </div>
      <Form @submit="onUpdatePublicWorks">
        <b-row cols="2">
          <!--Agregar nombre de obra-->
          <b-col>
            <b-form-group class="mt-3" label="Nombre obra">
              <Field name="NameWorksField" :rules="validateNameWorks" as="text">
                <b-form-input
                  v-model="publicWorks.nombre"
                  :state="NameWorksState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="NameWorksField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--Agregar latitud-->
          <b-col>
            <b-form-group class="mt-3" label="Latitud">
              <Field
                name="LatitudeField"
                :rules="validateLatitude"
                as="number"
              >
                <b-form-input
                  v-model="publicWorks.latitud"
                  :state="LatitudeState"
                  type="number"
                ></b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="LatitudeField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--Agregar longitud-->
          <b-col>
            <b-form-group class="mt-3" label="Longitud">
              <Field
                name="LengthField"
                :rules="validateLength"
                as="number"
              >
                <b-form-input
                  v-model="publicWorks.longitud"
                  :state="LengthState"
                  type="number"
                ></b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="LengthField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--Descripcion-->
          <b-col>
            <b-form-group class="mt-3" label="Descripcion">
              <Field
                name="DescriptionField"
                :rules="validateDescription"
                as="text"
              >
                <b-form-textarea
                  v-model="publicWorks.descripcion"
                  :state="DescriptionState"
                  rows="4"
                ></b-form-textarea>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="DescriptionField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--agregar un estatus obra-->
          <b-col>
            <b-form-group class="mt-3" label="Estatus de la Obra">
              <Field name="WorkStatusField" :rules="validateWorkStatus" as="text">
                <b-form-select
                  v-model="publicWorks.estatusObraId"
                  autofocus
                  :state="WorkStatusState"
                  :options="worksStatus"
                  value-field="estatusObraId"
                  text-field="nombreEstatus"
                ></b-form-select>
              </Field>
              <ErrorMessage class="text-danger" name="WorkStatusField"/>
            </b-form-group>
          </b-col>

        </b-row>
        <b-row align-h="end">
          <b-button
            class="col-1 m-2 text-white"
            variant="primary"
            to="/ObrasPublicas/list"
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
import PublicWorksServices from '@/Services/publickworks.Services'
import worksStatusServices from '@/Services/worksstatus.Services'
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
    const { getPublicWorksById, updatePublicWorks } = PublicWorksServices()
    const { getWorksStatus } = worksStatusServices()
    const publicWorks = ref([])
    const worksStatus = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const NameWorksState = ref(false)
    const LatitudeState = ref(false)
    const LengthState = ref(false)
    const DescriptionState = ref(false)
    const WorkStatusState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Departamento obras publicas', to: '/ObrasPublicas/list' },
      { text: 'Editar-Obras publicas' }
    ])

    getWorksStatus(data => {
      worksStatus.value = data
      if (data.length === 0) {
        swal.fire({
          title: 'No se encuentra un tipo de obra publica registrado!',
          text:
            'No se encuentra tipo de obra publica registrado en el departamento seleccionado, registre primero un tipo de obra publica para continuar',
          icon: 'warning'
        })
      }
    })

    const onUpdatePublicWorks = () => {
      updatePublicWorks(publicWorks.value, (data) => {})
      swal.fire({
        title: '¡Obras publicas modificado correctamente!',
        text: 'Las obras publicas se ha modificado  satisfactoriamente.',
        icon: 'success'
      }).then(result => {
        if (result.isConfirmed) {
          redirect.push('/ObrasPublicas/list')
        }
      })
    }
    getPublicWorksById(router.params.ObraId, (data) => {
      publicWorks.value = data
    })

    worksStatus(data => {
      publicWorks.value = data
      if (data.length === 0) {
        swal.fire({
          title: 'No se encuentra un estatus_OP registrado!',
          text:
            'No se encuentra estatus_OP registrado en el departamento seleccionado, registre primero un tipo de estatus_OP para continuar',
          icon: 'warning'
        })
      }
    })

    const validateNameWorks = () => {
      if (!publicWorks.value.nombre) {
        validateState()
        return 'Este campo es requerido'
      }
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(publicWorks.value.nombre)) {
        validateState()
        return 'El nombre de la obra solo puede contener letras'
      }
      validateState()
      return true
    }

    const validateLatitude = () => {
      if (!publicWorks.value.latitud) {
        validateState()
        return 'Este campo es requerido'
      }

      if (!/^[0-9]+$/i.test(publicWorks.value.latitud)) {
        validateState()
        return 'Este campo solo puede contener numeros'
      }
      validateState()
      return true
    }

    const validateLength = () => {
      if (!publicWorks.value.longitud) {
        validateState()
        return 'Este campo es requerido'
      }

      if (!/^[0-9]+$/i.test(publicWorks.value.longitud)) {
        validateState()
        return 'Este campo solo puede contener numeros'
      }
      validateState()
      return true
    }

    const validateDescription = () => {
      if (!publicWorks.value.descripcion) {
        validateState()
        return 'Este campo es requerido'
      }
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(publicWorks.value.descripcion)) {
        validateState()
        return 'La descripcion solo puede contener letras'
      }
      validateState()
      return true
    }

    const validateWorkStatus = () => {
      if (!publicWorks.value.estatusObraId) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }

    const validateState = () => {
      // eslint-disable-next-line no-unneeded-ternary
      NameWorksState.value = publicWorks.value.nombre === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      LatitudeState.value = publicWorks.value.latitud === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      LengthState.value = publicWorks.value.longitud === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      DescriptionState.value = publicWorks.value.descripcion === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      WorkStatusState.value = publicWorks.value.estatusObraId === '' ? false : true
    }

    return {
      publicWorks,
      breadcrumbItems,
      NameWorksState,
      LatitudeState,
      LengthState,
      DescriptionState,
      WorkStatusState,

      onUpdatePublicWorks,
      validateNameWorks,
      validateLatitude,
      validateLength,
      validateDescription,
      validateWorkStatus,
      validateState
    }
  }
}
</script>

<style>

</style>
