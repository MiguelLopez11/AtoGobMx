<template>
  <b-card class="m-2">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-card>
      <div>
        <h3>Reporte Alumbrado</h3>
      </div>
      <Form @submit="onUpdatePublicLighting">
        <b-row cols="3">
          <!--Tipo de tarea a agregar-->
          <b-col>
            <b-form-group class="mt-3" label="Tipo de Tarea">
              <Field name="TaskField" :rules="validateTask" as="text">
                <b-form-select
                  v-model="publicLighting.tareaTipoId"
                  autofocus
                  :state="TaskState"
                  :options="typeTaskLighting"
                  value-field="tareaTipoId"
                  text-field="nombreTarea"
                ></b-form-select>
              </Field>
              <ErrorMessage class="text-danger" name="TaskField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--agregar un estatus-->
          <b-col>
            <b-form-group class="mt-3" label="Estatus">
              <Field name="StatusField" :rules="validateStatus" as="text">
                <b-form-select
                  v-model="publicLighting.estatusId"
                  autofocus
                  :state="StatusState"
                  :options="statusPublicLighting"
                  value-field="estatusAlumbradoId"
                  text-field="nombreEstatus"
                ></b-form-select>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="StatusField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--agregar nombreobra-->
          <b-col>
            <b-form-group class="mt-3" label="Obra o servicio de alumbrado">
              <Field name="NameWorkField" :rules="validateNameWork" as="text">
                <b-form-input
                  v-model="publicLighting.nombreObra"
                  :state="NameWorkState"
                ></b-form-input>
              </Field>
              <ErrorMessage class="text-danger" name="NameWorkField" />
            </b-form-group>
          </b-col>
          <!--agregar domicilio-->
          <b-col>
            <b-form-group class="mt-3" label="Domicilio">
              <Field name="DomicileField" :rules="validateDomicile" as="text">
                <b-form-input
                  v-model="publicLighting.domicilio"
                  :state="DomicileState"
                ></b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="DomicileField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--agregar descripcion de un domicilio-->
          <b-col>
            <b-form-group class="mt-3" label="Descripcion Domicilio">
              <Field
                name="addresdescriptionField"
                :rules="validateAddresdescription"
                as="text"
              >
                <b-form-textarea
                  v-model="publicLighting.descripcionDomicilio"
                  :state="addresdescriptionState"
                  rows="4"
                ></b-form-textarea>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="addresdescriptionField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--Agregar una descripcion del problema-->
          <b-col>
            <b-form-group class="mt-3" label="Descripcion del problema">
              <Field name="ProblemField" :rules="validateProblem" as="text">
                <b-form-textarea
                  v-model="publicLighting.descripcionProblema"
                  :state="ProblemState"
                  rows="4"
                ></b-form-textarea>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="ProblemField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
        </b-row>

        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            to="/ServiciosPublicos/Alumbrado/list"
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
import StreetlightingServices from '@/Services/publiclighting.Services'
// import ExpedientPublic from '@/views/ServiciosPublicos/streetLighting/expedientlighting/EditExpedientLighting.vue'
import StatusLightingServices from '@/Services/statuslighting.Services'
import TypeTaskLightingServices from '@/Services/tasktypelighting.Services'
import { Field, Form, ErrorMessage } from 'vee-validate'
import { ref, inject } from 'vue'
import { useRoute, useRouter } from 'vue-router'
// import Datepicker from '@vuepic/vue-datepicker'
// import { useToast } from 'vue-toast-notification'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    // Datepicker,
    Field,
    Form,
    ErrorMessage
    // ExpedientPublic
  },
  setup () {
    const swal = inject('$swal')
    const {
      getPublicLightingById,
      updatePublicLighting
    } = StreetlightingServices()
    const { getStatus } = StatusLightingServices()
    const { getTaskTypeLighting } = TypeTaskLightingServices()
    const publicLighting = ref([])
    const statusPublicLighting = ref([])
    const typeTaskLighting = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const TaskState = ref(false)
    const NameWorkState = ref(false)
    const addresdescriptionState = ref(false)
    const DomicileState = ref(false)
    const ProblemState = ref(false)
    const StatusState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Alumbrado', to: '/ServiciosPublicos/Alumbrado/list' },
      { text: 'Editar-Alumbrado' }
    ])

    getPublicLightingById(router.params.AlumbradoId, data => {
      publicLighting.value = data
      validateState()
    })

    getTaskTypeLighting(data => {
      typeTaskLighting.value = data
      if (data.length === 0) {
        swal.fire({
          title: 'No se encuentra un tipo de tare registrada!',
          text:
            'No se encuentra tipo de tarea registradas en el departamento seleccionado, registre primero un tipo de tarea para continuar',
          icon: 'warning'
        })
      }
    })

    getStatus(data => {
      statusPublicLighting.value = data
      if (data.length === 0) {
        swal.fire({
          title: 'No se encuentra un estatus registrado!',
          text:
            'No se encuentra estatus registrado en el departamento seleccionado, registre primero un tipo de estatus para continuar',
          icon: 'warning'
        })
      }
    })

    const onUpdatePublicLighting = () => {
      updatePublicLighting(publicLighting.value, data => {})
      swal
        .fire({
          title: '¡Alumbrado modificado correctamente!',
          text: 'El alumbrado se ha modificado  satisfactoriamente.',
          icon: 'success'
        })
        .then(result => {
          if (result.isConfirmed) {
            redirect.push('/ServiciosPublicos/Alumbrado/list')
          }
        })
    }

    const validateTask = () => {
      if (!publicLighting.value.tareaTipoId) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }

    const validateStatus = () => {
      if (!publicLighting.value.estatusId) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }

    const validateNameWork = () => {
      if (!publicLighting.value.nombreObra) {
        validateState()
        return 'Este campo es requerido'
      }
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(publicLighting.value.nombreObra)) {
        NameWorkState.value = false
        return 'Este campo solo puede contener letras'
      }
      validateState()
      return true
    }

    const validateProblem = () => {
      if (!publicLighting.value.descripcionProblema) {
        validateState()
        return 'Este campo es requerido'
      }
      if (
        !/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ 0-9]+$/i.test(
          publicLighting.value.descripcionProblema
        )
      ) {
        ProblemState.value = false
        return 'Este campo solo puede contener letras'
      }
      validateState()
      return true
    }

    const validateDomicile = () => {
      if (!publicLighting.value.domicilio) {
        validateState()
        return 'Este campo es requerido'
      }
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(publicLighting.value.domicilio)) {
        DomicileState.value = false
        return 'Este campo solo puede contener letras'
      }
      validateState()
      return true
    }

    const validateAddresdescription = () => {
      if (!publicLighting.value.descripcionDomicilio) {
        validateState()
        return 'Este campo es requerido'
      }
      if (
        !/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ 0-9]+$/i.test(
          publicLighting.value.descripcionDomicilio
        )
      ) {
        addresdescriptionState.value = false
        return 'Este campo solo puede contener letras'
      }
      validateState()
      return true
    }

    const validateState = () => {
      // eslint-disable-next-line no-unneeded-ternary
      TaskState.value = publicLighting.value.tareaTipoId !== null
      // eslint-disable-next-line no-unneeded-ternary
      StatusState.value = publicLighting.value.estatusId !== null
      // eslint-disable-next-line no-unneeded-ternary
      NameWorkState.value = publicLighting.value.nombreObra !== ''
      // eslint-disable-next-line no-unneeded-ternary
      DomicileState.value = publicLighting.value.domicilio !== ''
      // eslint-disable-next-line no-unneeded-ternary
      addresdescriptionState.value =
        publicLighting.value.descripcionDomicilio !== ''
      // eslint-disable-next-line no-unneeded-ternary
      ProblemState.value = publicLighting.value.descripcionProblema !== ''
    }

    return {
      publicLighting,
      breadcrumbItems,
      statusPublicLighting,
      typeTaskLighting,
      router,
      TaskState,
      NameWorkState,
      addresdescriptionState,
      DomicileState,
      StatusState,
      ProblemState,
      onUpdatePublicLighting,
      validateTask,
      validateProblem,
      validateNameWork,
      validateAddresdescription,
      validateStatus,
      validateDomicile
    }
  }
}
</script>

<style></style>
