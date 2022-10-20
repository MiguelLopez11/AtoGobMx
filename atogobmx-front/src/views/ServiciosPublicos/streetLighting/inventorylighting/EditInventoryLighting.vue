<template>
  <b-card class="m-2">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-card>
      <div>
        <h3>Inventario Alumbrado</h3>
      </div>
      <Form @submit="onUpdateInventoryLighting">
        <b-row cols="2">
          <b-col>
            <b-form-group class="mt-3" label="Nombre del tramite">
              <Field name="TaskField" :rules="validateTask">
                <b-form-select
                  v-model="inventoryLighting.tarea"
                  autofocus
                  :state="TaskState"
                  :options="statusPublicLighting"
                  value-field="estatusId"
                  text-field="nombreEstatus"
                ></b-form-select>
              </Field>
              <ErrorMessage name="TaskField"
                ><span>Este campo es requerido </span
                ><i class="bi bi-exclamation-circle"></i>
              </ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Fecha Alta">
              <Field name="HighDateField" :rules="validateHighDate">
                <Datepicker
                  locale="es"
                  name="Date"
                  text-input
                  v-model="inventoryLighting.fechaAlta"
                  :state="HighDateState"
                ></Datepicker>
              </Field>
              <ErrorMessage name="HighDateField"
                ><span>Este campo es requerido </span
                ><i class="bi bi-exclamation-circle"></i>
              </ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Domicilio">
              <Field name="DomicileField" :rules="validateDomicile">
                <b-form-input
                  v-model="inventoryLighting.domicilio"
                  :state="DomicileState"
                ></b-form-input>
              </Field>
              <ErrorMessage name="DomicileField"
                ><span>Este campo es requerido </span
                ><i class="bi bi-exclamation-circle"></i>
              </ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Fecha Baja">
              <Field name="LowDateField" :rules="validateLowDate">
                <Datepicker
                  locale="es"
                  name="date"
                  text-input
                  v-model="inventoryLighting.fechaBaja"
                  :state="LowDateState"
                ></Datepicker>
              </Field>
              <ErrorMessage name="LowDateField"
                ><span>Este campo es requerido llenarlo </span
                ><i class="bi bi-exclamation-circle"></i>
              </ErrorMessage>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            to="/InventarioAlumbrado/list"
          >
            Cancelar
          </b-button>
          <b-button class="w-auto m-2" variant="success" type="submit"
            >Guardar</b-button
          >
        </b-row>
      </Form>
    </b-card>
  </b-card>
</template>

<script>
import InventorylightingServices from '@/Services/inventorylighting.Services'
// import StatusServices from '@/Services/statuslighting.Services'
import { Field, Form, ErrorMessage } from 'vee-validate'
// import AreaServices from '@/Services/area.Services'
import { ref } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import Datepicker from '@vuepic/vue-datepicker'
import { useToast } from 'vue-toast-notification'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    Datepicker,
    Field,
    Form,
    ErrorMessage
  },
  setup () {
    const {
      getInventoryLightingById,
      updatInventoryLighting
    } = InventorylightingServices()
    // const { getStatusById } = StatusServices()
    const $toast = useToast()
    const inventoryLighting = ref([])
    const statusPublicLighting = ref([])
    const router = useRoute()
    const redirect = useRouter()
    // const route = useRouter()
    const TaskState = ref(false)
    const HighDateState = ref(false)
    const DomicileState = ref(false)
    const LowDateState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Alumbrado', to: '/InventarioAlumbrado/list' },
      { text: 'Editar-InventarioAlumbrado' }
    ])

    // getStatusById((data) => {
    //   statusPublicLighting.value = data
    // })

    getInventoryLightingById(router.params.InventarioAlumbradoId, data => {
      // streetLighting.value = data
      // validateState()
      inventoryLighting.value = data
      // eslint-disable-next-line no-unneeded-ternary
      TaskState.value = data.tarea === null ? false : true
    })

    const onUpdateInventoryLighting = () => {
      updatInventoryLighting(inventoryLighting.value, data => {})
      $toast.open({
        message: 'El inventario alumbrado se a modificado correcta mente',
        position: 'top',
        duration: 1000,
        dismissible: true,
        type: 'success',
        onDismiss: () => redirect.push('/InventarioAlumbrado/list')
      })
    }

    const validateTask = () => {
      if (!inventoryLighting.value.tarea) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }

    const validateDomicile = () => {
      if (!inventoryLighting.value.domicilio) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }

    const validateLowDate = () => {
      if (!inventoryLighting.value.fechaBaja) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }

    const validateHighDate = () => {
      if (!inventoryLighting.value.fechaAlta) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }

    const validateState = () => {
      // eslint-disable-next-line no-unneeded-ternary
      TaskState.value = inventoryLighting.value.tarea !== ''
      // eslint-disable-next-line no-unneeded-ternary
      HighDateState.value = inventoryLighting.value.fechaAlta !== null
      // eslint-disable-next-line no-unneeded-ternary
      DomicileState.value = inventoryLighting.value.domicilio !== ''
      // eslint-disable-next-line no-unneeded-ternary
      LowDateState.value = inventoryLighting.value.fechaBaja !== null
    }
    // getAreas((data) => {
    //   areas.value = data
    // })
    return {
      inventoryLighting,
      breadcrumbItems,
      statusPublicLighting,
      router,
      TaskState,
      HighDateState,
      DomicileState,
      LowDateState,
      onUpdateInventoryLighting,
      validateTask,
      validateHighDate,
      validateLowDate,
      validateDomicile
    }
  }
}
</script>

<style></style>
