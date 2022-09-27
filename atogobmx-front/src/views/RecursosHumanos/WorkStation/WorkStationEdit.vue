<template>
  <b-card class="m-3">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-card>
      <div>
        <h3>Editar Puesto de trabajo</h3>
      </div>
      <Form @submit="onUpdateWorkStation()">
        <b-row cols="3">
          <b-col>
            <b-form-group class="mt-3" label="Nombre">
              <Field name="NameField" :rules="validateName">
                <b-form-input v-model="workStation.nombre" :state="nameState">
                </b-form-input>
              </Field>
              <ErrorMessage name="NameField">
                <span>Este campo es requerido</span>
                <i class="bi bi-exclamation-circle" />
              </ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Departamento">
              <Field
                name="DepartamentField"
                :rules="validateDepartament"
              >
                <b-form-select
                  v-model="workStation.departamentoId"
                  autofocus
                  :options="departaments"
                  value-field="departamentoId"
                  text-field="nombre"
                  :state="departamentState"
                  @input="getAreas(workStation.departamentoId)"
                >
                </b-form-select>
              </Field>
              <ErrorMessage name="DepartamentField"
                ><span>Este campo es requerido </span
                ><i class="bi bi-exclamation-circle"></i
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Area">
              <Field
                name="AreaField"
                :rules="validateArea"
              >
                <b-form-select
                  v-model="workStation.areaId"
                  autofocus
                  :options="areas"
                  value-field="areaId"
                  text-field="nombre"
                  :state="areaState"
                >
                </b-form-select>
              </Field>
              <ErrorMessage name="AreaField"
                ><span>Este campo es requerido </span
                ><i class="bi bi-exclamation-circle"></i
              ></ErrorMessage>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="col-1 m-2 text-white"
            variant="primary"
            to="/PuestosTrabajos/list"
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
import workStationServices from '@/Services/workStation.Services'
import AreasServices from '@/Services/area.Services'
import DepartamentServices from '@/Services/departament.Services'
import { ref } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { useToast } from 'vue-toast-notification'
import { Form, Field, ErrorMessage } from 'vee-validate'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    Form,
    Field,
    ErrorMessage
  },
  setup () {
    const { getWorkStation, updateWorkStation } = workStationServices()
    const { getAreasByDepartament } = AreasServices()
    const { getDepartaments } = DepartamentServices()
    const $toast = useToast()
    const workStation = ref([])
    const areas = ref([])
    const departaments = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const nameState = ref(false)
    const areaState = ref(false)
    const departamentState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Puestos de Trabajo', to: '/PuestosTrabajos/list' },
      { text: 'Editar-Puesto de trabajo' }
    ])
    const onUpdateWorkStation = () => {
      updateWorkStation(workStation.value, data => {})
      $toast.open({
        message: 'Puesto de trabajo modificado correctamente',
        position: 'top',
        duration: 2000,
        dismissible: true,
        type: 'success',
        onDismiss: () => redirect.push('/PuestosTrabajos/list')
      })
    }
    getDepartaments(data => {
      departaments.value = data
      if (data.length === 0) {
        $toast.open({
          message: 'No se encuentran departamentos registrados en el sistema, registre primero un departamento para continuar',
          position: 'top-left',
          duration: 0,
          dismissible: true,
          type: 'error'
        })
      }
    })
    const getAreas = (departamentoId) => {
      getAreasByDepartament(departamentoId, data => {
        areas.value = data
        if (data.length === 0) {
          $toast.open({
            message: 'No se encuentran areas registrados en el departamento seleccionado, registre primero una area para continuar',
            position: 'top-left',
            duration: 0,
            dismissible: true,
            type: 'error'
          })
        }
      })
    }
    getWorkStation(router.params.puestoTrabajoId, data => {
      workStation.value = data
      departamentState.value = data.departamentoId !== null
      areaState.value = data.areaId !== null
      getAreas(data.departamentoId)
    })
    const validateName = () => {
      if (!workStation.value.nombre) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateDepartament = () => {
      if (!workStation.value.departamentoId) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateArea = () => {
      if (!workStation.value.areaId) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateState = () => {
      nameState.value = workStation.value.nombre !== ''
      // eslint-disable-next-line no-unneeded-ternary
      departamentState.value = workStation.value.departamentoId === null ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      areaState.value = workStation.value.areaId === null ? false : true
      return 'HOli'
    }
    return {
      workStation,
      breadcrumbItems,
      areas,
      departaments,
      departamentState,
      areaState,
      nameState,
      //   router

      onUpdateWorkStation,
      validateName,
      validateState,
      validateDepartament,
      validateArea,
      getAreas
    }
  }
}
</script>

<style></style>
