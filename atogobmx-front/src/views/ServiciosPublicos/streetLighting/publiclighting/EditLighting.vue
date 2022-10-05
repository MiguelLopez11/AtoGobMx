<template>
  <b-card class="m-3">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-card>
      <b-tabs card>
        <b-tab title="Datos Generales" active>
          <div>
            <h3>Reporte Alumbrado</h3>
          </div>
          <Form @submit="onUpdatePublicLighting">
            <b-row cols="2">
              <b-col>
                <b-form-group class="mt-3" label="Tipo Tarea">
                  <Field name="TaskField" :rules="validateTask">
                    <b-form-select
                      v-model="publicLighting.tarea"
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
                      v-model="publicLighting.fechaAlta"
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
                      v-model="publicLighting.domicilio"
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
                      v-model="publicLighting.fechaBaja"
                      :state="LowDateState"
                    ></Datepicker>
                  </Field>
                  <ErrorMessage name="LowDateField"
                    ><span>Este campo es requerido llenarlo </span
                    ><i class="bi bi-exclamation-circle"></i>
                  </ErrorMessage>
                </b-form-group>
              </b-col>
              <b-col>
                <b-form-group class="mt-3" label="Descripcion Domicilio">
                  <Field
                    name="addresdescriptionField"
                    :rules="validateAddresdescription"
                  >
                    <b-form-textarea
                      v-model="publicLighting.descripcionDomicilio"
                      :state="addresdescriptionState"
                      rows="4"
                    ></b-form-textarea>
                  </Field>
                  <ErrorMessage name="addresdescriptionField"
                    ><span>Este campo es requerido llenarlo </span
                    ><i class="bi bi-exclamation-circle"></i>
                  </ErrorMessage>
                </b-form-group>
              </b-col>
              <b-col>
                <b-form-group class="mt-3" label="Solucion Del Problema">
                  <Field
                    name="DescriptionSolutionField"
                    :rules="validateDescriptionSolution"
                  >
                    <b-form-textarea
                      v-model="publicLighting.descripcionSolucion"
                      :state="DescriptionSolutionState"
                      rows="4"
                    ></b-form-textarea>
                  </Field>
                  <ErrorMessage name="DescriptionSolutionField"
                    ><span>Este campo es requerido </span
                    ><i class="bi bi-exclamation-circle"></i>
                  </ErrorMessage>
                </b-form-group>
              </b-col>
            </b-row>
            <b-row align-h="end">
              <b-button
                class="w-auto m-2 text-white"
                variant="primary"
                to="/Alumbrado/list"
              >
                Cancelar
              </b-button>
              <b-button class="w-auto m-2" variant="success" type="submit"
                >Guardar</b-button
              >
            </b-row>
          </Form>
        </b-tab>
        <b-tab title="Expediente Alumbrado">
          <ExpedientPublic
            :expedientLightingId="1"
          />
        </b-tab>
      </b-tabs>
    </b-card>
  </b-card>
</template>

<script>
import StreetlightingServices from '@/Services/publiclighting.Services'
import ExpedientPublic from '@/views/ServiciosPublicos/streetLighting/expedientlighting/EditExpedientLighting.vue'
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
    ErrorMessage,
    ExpedientPublic
  },
  setup () {
    const { getPublicLightingById, updatePublicLighting } =
      StreetlightingServices()
    // const { getStatusById } = StatusServices()
    const $toast = useToast()
    const publicLighting = ref([])
    const statusPublicLighting = ref([])
    const router = useRoute()
    const redirect = useRouter()
    // const route = useRouter()
    const TaskState = ref(false)
    const DescriptionSolutionState = ref(false)
    const HighDateState = ref(false)
    const addresdescriptionState = ref(false)
    const DomicileState = ref(false)
    const LowDateState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Alumbrado', to: '/Alumbrado/list' },
      { text: 'Editar-Alumbrado' }
    ])

    // jenerar propiedades

    // getStatusById((data) => {
    //   statusPublicLighting.value = data
    // })

    getPublicLightingById(router.params.AlumbradoId, (data) => {
      // streetLighting.value = data
      // validateState()
      publicLighting.value = data
      // eslint-disable-next-line no-unneeded-ternary
      TaskState.value = data.tarea === null ? false : true
    })
    const onUpdatePublicLighting = () => {
      updatePublicLighting(publicLighting.value, (data) => {})
      $toast.open({
        message: 'El alumbrado se a modificado correcta mente',
        position: 'top',
        duration: 1000,
        dismissible: true,
        type: 'success',
        onDismiss: () => redirect.push('/Alumbrado/list')
      })
    }

    const validateTask = () => {
      if (!publicLighting.value.tarea) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateDescriptionSolution = () => {
      if (!publicLighting.value.descripcionSolucion) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateDomicile = () => {
      if (!publicLighting.value.domicilio) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateLowDate = () => {
      if (!publicLighting.value.fechaBaja) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateHighDate = () => {
      if (!publicLighting.value.fechaAlta) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateAddresdescription = () => {
      if (!publicLighting.value.descripcionDomicilio) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateState = () => {
      // eslint-disable-next-line no-unneeded-ternary
      TaskState.value = publicLighting.value.tarea !== ''
      // eslint-disable-next-line no-unneeded-ternary
      HighDateState.value = publicLighting.value.fechaAlta !== null
      // eslint-disable-next-line no-unneeded-ternary
      DomicileState.value = publicLighting.value.domicilio !== ''
      // eslint-disable-next-line no-unneeded-ternary
      addresdescriptionState.value =
        publicLighting.value.descripcionDomicilio !== ''
      // eslint-disable-next-line no-unneeded-ternary
      DescriptionSolutionState.value =
        publicLighting.value.descripcionSolucion !== ''
      // eslint-disable-next-line no-unneeded-ternary
      LowDateState.value = publicLighting.value.fechaBaja !== null
    }
    // getAreas((data) => {
    //   areas.value = data
    // })
    return {
      publicLighting,
      breadcrumbItems,
      statusPublicLighting,
      router,
      TaskState,
      DescriptionSolutionState,
      HighDateState,
      addresdescriptionState,
      DomicileState,
      LowDateState,
      onUpdatePublicLighting,
      validateTask,
      validateDescriptionSolution,
      validateHighDate,
      validateAddresdescription,
      validateLowDate,
      validateDomicile
    }
  }
}
</script>

  <style>
</style>
