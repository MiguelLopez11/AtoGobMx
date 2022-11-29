<template>
  <b-card class="m-2">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-card>
      <div>
        <h3>Editar expediente médico</h3>
      </div>
      <Form @submit="onUpdateWorkStation()">
        <b-row cols="3">
          <b-col>
            <b-form-group class="mt-3" label="Estatura">
              <Field name="HeightField" :rules="validateHeight" as="text">
                <b-form-input
                  v-model="expedientMedical.estatura"
                  :state="heightState"
                  type="number"
                />
              </Field>
              <ErrorMessage
                class="text-danger"
                name="HeightField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Peso (KG)">
              <Field name="WeightField" :rules="validateWeight" as="number">
                <b-form-input
                  v-model="expedientMedical.peso"
                  :state="weightState"
                  type="number"
                />
              </Field>
              <ErrorMessage
                class="text-danger"
                name="WeightField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Tipo de sangre">
              <Field
                name="BloodTypeField"
                :rules="validateBloodType"
                as="number"
              >
                <b-form-input
                  v-model="expedientMedical.tipoSangre"
                  :state="bloodTypeState"
                />
              </Field>
              <ErrorMessage
                class="text-danger"
                name="BloodTypeField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Sexo">
              <Field name="SexField" :rules="validateSex" as="number">
                <b-form-input
                  v-model="expedientMedical.sexo"
                  :state="sexState"
                />
              </Field>
              <ErrorMessage class="text-danger" name="SexField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Alergias">
              <b-form-textarea
                v-model="expedientMedical.alergias"
              />
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Discapacidades">
              <b-form-textarea
                v-model="expedientMedical.discapacidad"
              />
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Antecedentes Personales">
              <b-form-textarea
                v-model="expedientMedical.antecedentesPersonales"
              />
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Antecedentes familiares">
              <b-form-textarea
                v-model="expedientMedical.antecedentesFamiliares"
              />
            </b-form-group>
          </b-col>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="col-1 m-2 text-white"
            variant="primary"
            to="/ServiciosMedicos/ExpedienteMedico/list"
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
import MunicipalMedicalServices from '@/Services/municipalMedical.Services'
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
    const { getExpedientMedical, updateExpedientMedical } =
      MunicipalMedicalServices()
    const router = useRoute()
    // const $toast = useToast()
    const expedientMedical = ref([])
    const redirect = useRouter()
    const heightState = ref(false)
    const weightState = ref(false)
    const bloodTypeState = ref(false)
    const sexState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      {
        text: 'Expedientes médicos',
        to: '/ServiciosMedicos/ExpedienteMedico/list'
      },
      { text: 'Editar expediente médico' }
    ])
    getExpedientMedical(router.params.ExpedienteMedicoId, data => {
      expedientMedical.value = data
    })
    const onUpdateWorkStation = () => {
      updateExpedientMedical(expedientMedical.value, data => {
        swal
          .fire({
            title: 'Expediente médico modificado correctamente!',
            text: 'El expediente médico se ha modificado  satisfactoriamente.',
            icon: 'success'
          })
          .then(result => {
            if (result.isConfirmed) {
              redirect.push('/ServiciosMedicos/ExpedienteMedico/list')
            }
          })
      })
    }
    const validateHeight = () => {
      if (!expedientMedical.value.estatura) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateWeight = () => {
      if (!expedientMedical.value.peso) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateBloodType = () => {
      if (!expedientMedical.value.tipoSangre) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateSex = () => {
      if (!expedientMedical.value.sexo) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateState = () => {
      heightState.value = expedientMedical.value.estatura !== null && expedientMedical.value.estatura !== ''
      weightState.value = expedientMedical.value.peso !== null && expedientMedical.value.peso !== ''
      bloodTypeState.value = expedientMedical.value.tipoSangre !== null && expedientMedical.value.tipoSangre !== ''
      sexState.value = expedientMedical.value.sexo !== null && expedientMedical.value.sexo !== ''
      return ''
    }
    return {
      expedientMedical,
      breadcrumbItems,
      heightState,
      weightState,
      bloodTypeState,
      sexState,

      onUpdateWorkStation,
      validateState,
      validateHeight,
      validateWeight,
      validateBloodType,
      validateSex
    }
  }
}
</script>

<style></style>
