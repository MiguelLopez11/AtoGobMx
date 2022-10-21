<template>
  <b-card class="m-2">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-card>
      <div>
        <h3>Expediente Alumbrado</h3>
      </div>
      <Form @submit="onUpdateExpedientLighting">
        <b-row cols="2">
          <!-- <b-col>
            <b-form-group class="mt-3" label="Nombre del Tipo Tarea">
              <Field name="TypeTaskField" :rules="validateTypeTask">
                <b-form-select
                  v-model="expedientLighting.tarea"
                  autofocus
                  :state="TypeTaskState"
                  :options="statusPublicLighting"
                  value-field="estatusId"
                  text-field="nombreEstatus"
                ></b-form-select>
              </Field>
              <ErrorMessage name="TypeTaskField"
                ><span>Este campo es requerido </span
                ><i class="bi bi-exclamation-circle"></i>
              </ErrorMessage>
            </b-form-group>
          </b-col> -->
          <!--1-->
          <b-col>
            <b-form-group class="mt-3" label="Lugar publico">
              <Field
                name="PublicPlaceField"
                :rules="validatePublicPlace"
                as="text"
              >
                <b-form-input
                  v-model="expedientLighting.lugarPublico"
                  :state="PublicPlaceState"
                ></b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="PublicPlaceField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--2-->
          <b-col>
            <b-form-group class="mt-3" label="Localidad">
              <Field name="LocationField" :rules="validateLocation" as="text">
                <b-form-input
                  v-model="expedientLighting.localidad"
                  :state="LocationState"
                ></b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="LocationField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--3-->
          <b-col>
            <b-form-group class="mt-3" label="Fecha Alta">
              <Field name="HighDateField" :rules="validateHighDate" as="">
                <Datepicker
                  locale="es"
                  name="date"
                  text-input
                  v-model="expedientLighting.fechaAlta"
                  :state="HighDateState"
                ></Datepicker>
              </Field>
              <ErrorMessage name="HighDateField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--4-->
          <b-col>
            <b-form-group class="mt-3" label="Departamento">
              <Field
                name="DepartamentField"
                :rules="validateDepartament"
                as="number"
              >
                <b-form-select
                  v-model="expedientLighting.departamentoId"
                  autofocus
                  :options="departaments"
                  value-field="departamentoId"
                  text-field="nombre"
                  :state="departamentState"
                  @input="getAreas(EmployeesFields.departamentoId)"
                >
                </b-form-select>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="DepartamentField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--5-->
          <b-col>
            <b-form-group class="mt-3" label="Area">
              <Field name="AreaField" :rules="validateArea" as="number">
                <b-form-select
                  v-model="expedientLighting.areaId"
                  autofocus
                  :options="areas"
                  value-field="areaId"
                  text-field="nombre"
                  :state="areaState"
                  @input="getWorkStation(EmployeesFields.areaId)"
                >
                </b-form-select>
              </Field>
              <ErrorMessage class="text-danger" name="AreaField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--6-->
          <b-col>
            <b-form-group class="mt-3" label="Descripcion Solucion">
              <Field
                name="DescriptionSolutionField"
                :rules="validateDescriptionSolution"
                as="text"
              >
                <b-form-textarea
                  v-model="expedientLighting.descripcionSolucion"
                  :state="DescriptionSolutionState"
                  rows="4"
                ></b-form-textarea>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="DescriptionSolutionField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            to="/ExpedienteAlumbrado/list"
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
import ExpedientlightingServices from '@/Services/expedientlighting.Services'
import AreaServices from '@/Services/area.Services'
import DepartamentServices from '@/Services/departament.Services'
// import StatusServices from '@/Services/statuslighting.Services'
import { Field, Form, ErrorMessage } from 'vee-validate'
// import AreaServices from '@/Services/area.Services'
import { ref, inject } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import Datepicker from '@vuepic/vue-datepicker'
// import { useToast } from 'vue-toast-notification'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  props: {
    expedientLightingId: Number
  },
  components: {
    Datepicker,
    Field,
    Form,
    ErrorMessage
  },
  setup (props) {
    const swal = inject('$swal')
    const { getAreasByDepartament } = AreaServices()
    const { getDepartaments } = DepartamentServices()
    const {
      getExpedientLightingById,
      updatExpedientLighting
    } = ExpedientlightingServices()
    // const { getStatusById } = StatusServices()
    // const $toast = useToast()
    const expedientLighting = ref([])
    const departaments = ref([])
    const areas = ref([])
    // const statusPublicLighting = ref([])
    const router = useRoute()
    const redirect = useRouter()
    // const route = useRouter()
    const PublicPlaceState = ref(false)
    const LocationState = ref(false)
    const HighDateState = ref(false)
    const departamentState = ref(false)
    const areaState = ref(false)
    const DescriptionSolutionState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Alumbrado', to: '/ExpedienteAlumbrado/list' },
      { text: 'Editar-ExpedienteAlumbrado' }
    ])

    // getStatusById((data) => {
    //   statusPublicLighting.value = data
    // })

    getExpedientLightingById(
      router.params.ExpedienteAlumbradoId || props.expedientLightingId,
      data => {
        // streetLighting.value = data
        // validateState()
        expedientLighting.value = data
        // eslint-disable-next-line no-unneeded-ternary
        PublicPlaceState.value = data.lugarPublico === null ? false : true
      }
    )
    const onUpdateExpedientLighting = () => {
      updatExpedientLighting(expedientLighting.value, data => {
        swal
          .fire({
            title: '¡Expediente modificado correctamente!',
            text: 'El expediente se ha modificado  satisfactoriamente.',
            icon: 'success'
          })
          .then(result => {
            if (result.isConfirmed) {
              redirect.push('/ExpedienteAlumbrado/list')
            }
          })
      })

      // $toast.open({
      //   message: 'El expediente alumbrado se a modificado correcta mente',
      //   position: 'top',
      //   duration: 1000,
      //   dismissible: true,
      //   type: 'success',
      //   onDismiss: () => redirect.push('/ExpedienteAlumbrado/list')
      // })
    }

    getDepartaments(data => {
      departaments.value = data
      if (data.length === 0) {
        swal.fire({
          title: 'No se encuentran departamentos registrados!',
          text: 'No se encuentran departamentos registrados en el sistema, registre primero un departamento para continuar.',
          icon: 'error'
        })
      }
    })

    const getAreas = (departamentoId) => {
      if (departamentoId) {
        getAreasByDepartament(departamentoId, data => {
          areas.value = data
          if (data.length === 0) {
            swal.fire({
              title: 'No se encuentran areas registradas!',
              text: 'No se encuentran areas registradas en el departamento seleccionado, registre primero una area para continuar.',
              icon: 'error'
            })
          }
        })
      }
    }

    const validatePublicPlace = () => {
      if (!expedientLighting.value.lugarPublico) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }

    const validateLocation = () => {
      if (!expedientLighting.value.localidad) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }

    const validateDepartament = () => {
      if (!expedientLighting.value.departamentoId) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }

    const validateArea = () => {
      if (!expedientLighting.value.areaId) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }

    const validateDescriptionSolution = () => {
      if (!expedientLighting.value.descripcionSolucion) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }

    const validateHighDate = () => {
      if (!expedientLighting.value.fechaAlta) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }

    const validateState = () => {
      // eslint-disable-next-line no-unneeded-ternary
      PublicPlaceState.value = expedientLighting.value.lugarPublico !== ''
      // eslint-disable-next-line no-unneeded-ternary
      HighDateState.value = expedientLighting.value.fechaAlta !== null
      // eslint-disable-next-line no-unneeded-ternary
      LocationState.value = expedientLighting.value.localidad !== ''
      // eslint-disable-next-line no-unneeded-ternary
      departamentState.value = expedientLighting.value.departamentoId !== ''
      // eslint-disable-next-line no-unneeded-ternary
      DescriptionSolutionState.value = expedientLighting.value.descripcionSolucion !== ''
      // eslint-disable-next-line no-unneeded-ternary
      areaState.value = expedientLighting.value.areaId !== null
    }
    // getAreas((data) => {
    //   areas.value = data
    // })
    return {
      expedientLighting,
      breadcrumbItems,
      areas,
      departaments,
      PublicPlaceState,
      HighDateState,
      LocationState,
      departamentState,
      areaState,
      router,
      DescriptionSolutionState,
      onUpdateExpedientLighting,
      validatePublicPlace,
      validateLocation,
      validateDepartament,
      validateArea,
      validateDescriptionSolution,
      validateHighDate,
      getAreas
    }
  }
}
</script>

<style></style>
