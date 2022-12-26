<template>
  <b-card class="m-2">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-card>
      <div>
        <h3>Expediente Alumbrado</h3>
      </div>
      <b-tabs content-class="mt-3">
        <b-tab title="Datos generales" active>
          <b-card>
            <Form @submit="onUpdateExpedientLighting">
              <b-row cols="2">
                <!--Nomenclatura-->
                <b-col>
                  <b-form-group class="mt-3" label="Nomenclatura">
                    <Field
                      name="NomenclatureField"
                      :rules="validateNomenclature"
                      as="text"
                    >
                      <b-form-input
                        v-model="expedientLighting.nomenclatura"
                        :state="NomenclatureState"
                      />
                    </Field>
                    <ErrorMessage name="NomenclatureField">
                      <span class="text-danger">Este campo es requerido </span>
                      <i class="bi bi-exclamation-circle"></i
                    ></ErrorMessage>
                  </b-form-group>
                </b-col>
                <!--Fecha alta-->
                <b-col>
                  <b-form-group class="mt-3" label="Fecha Alta">
                    <Field name="HighDateField" :rules="validateHighDate" as="">
                      <Datepicker
                        disabled
                        locale="es"
                        name="date"
                        text-input
                        v-model="expedientLighting.fechaAlta"
                        :state="HighDateState"
                      ></Datepicker>
                    </Field>
                    <ErrorMessage name="HighDateField">
                      <span class="text-danger">Este campo es requerido </span>
                      <i class="bi bi-exclamation-circle"></i>
                    </ErrorMessage>
                  </b-form-group>
                </b-col>
                <!--Checkbox-->
                <b-col>
                  <b-form-group horizontal class="mt-3" label="¿Es un lugar Público?">
                      <b-form-checkbox
                        style=""
                        size="lg"
                        v-model="isPublicPlace"
                        :state="PublicPlaceState"
                      />
                  </b-form-group>
                </b-col>
                <!--Lugar publico-->
                <b-col v-if="isPublicPlace">
                  <b-form-group class="mt-3" label="Direccion">
                    <Field
                      name="PublicPlaceField"
                      :rules="validatePublicPlace"
                      as="text"
                    >
                      <b-form-input
                        v-model="expedientLighting.lugarPublico"
                        :state="PublicPlaceState"
                      />
                    </Field>
                    <ErrorMessage name="PublicPlaceField">
                      <span class="text-danger">Este campo es requerido </span>
                      <i class="bi bi-exclamation-circle"></i
                    ></ErrorMessage>
                  </b-form-group>
                </b-col>
                <!--Localidad-->
                <b-col v-if="isPublicPlace">
                  <b-form-group class="mt-3" label="Localidad">
                    <Field
                      name="LocationField"
                      :rules="validateLocation"
                      as="text"
                    >
                      <b-form-input
                        v-model="expedientLighting.localidad"
                        :state="LocationState"
                      ></b-form-input>
                    </Field>
                    <ErrorMessage name="LocationField">
                      <span class="text-danger">Este campo es requerido </span>
                      <i class="bi bi-exclamation-circle"></i
                    ></ErrorMessage>
                  </b-form-group>
                </b-col>
                <!--Departamento-->
                <b-col v-if="!isPublicPlace">
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
                      >
                      </b-form-select>
                    </Field>
                    <ErrorMessage name="DepartamentField">
                      <span class="text-danger">Este campo es requerido </span>
                      <i class="bi bi-exclamation-circle"></i
                    ></ErrorMessage>
                  </b-form-group>
                </b-col>
                <!--Descripcion solucion-->
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
                    <ErrorMessage name="DescriptionSolutionField">
                      <span class="text-danger">Este campo es requerido </span>
                      <i class="bi bi-exclamation-circle"></i
                    ></ErrorMessage>
                  </b-form-group>
                </b-col>
              </b-row>
              <b-row align-h="end">
                <b-button
                  class="w-auto m-2 text-white"
                  variant="primary"
                  to="/ServiciosPublicos/ExpedienteAlumbrado/list"
                >
                  Cancelar
                </b-button>
                <b-button class="w-auto m-2" variant="success" type="submit"
                  >Guardar</b-button
                >
              </b-row>
            </Form>
          </b-card>
        </b-tab>
        <b-tab title="Empleados">
          <EmployeeLighting :expedienteAlumbradoId="expedienteAlumbradoId" />
        </b-tab>
        <b-tab title="Vehiculos">
          <VehiclesLighting :expedienteAlumbradoId="expedienteAlumbradoId" />
        </b-tab>
      </b-tabs>
    </b-card>
  </b-card>
</template>

<script>
import ExpedientlightingServices from '@/Services/expedientlighting.Services'
import DepartamentServices from '@/Services/departament.Services'
import EmployeeLighting from '@/views/ServiciosPublicos/streetLighting/lightingEmployee/LightingEmployeeList.vue'
import VehiclesLighting from '@/views/ServiciosPublicos/streetLighting/lightingvehicle/LightingVehicleList.vue'
import { Field, Form, ErrorMessage } from 'vee-validate'
import { ref, inject } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import Datepicker from '@vuepic/vue-datepicker'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    Datepicker,
    Field,
    Form,
    ErrorMessage,
    EmployeeLighting,
    VehiclesLighting
  },
  setup (props) {
    const swal = inject('$swal')
    const { getDepartaments } = DepartamentServices()
    const { getExpedientLightingById, updatExpedientLighting } =
      ExpedientlightingServices()
    const expedientLighting = ref([])
    const departaments = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const expedienteAlumbradoId = ref(parseInt(router.params.ExpedienteAlumbradoId))
    const isPublicPlace = ref(true)
    const PublicPlaceState = ref(false)
    const LocationState = ref(false)
    const HighDateState = ref(false)
    const departamentState = ref(false)
    const DescriptionSolutionState = ref(false)
    const NomenclatureState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Expediente alumbrado', to: '/ServiciosPublicos/ExpedienteAlumbrado/list' },
      { text: 'Editar-Expediente' }
    ])

    getExpedientLightingById(expedienteAlumbradoId.value, data => {
      expedientLighting.value = data
      validateState()
    })

    const onUpdateExpedientLighting = () => {
      updatExpedientLighting(expedientLighting.value, data => {
        swal
          .fire({
            title: '¡Expediente modificado correctamente!',
            text: 'El expediente se ha modificado  satisfactoriamente.',
            icon: 'success'
          })
          .then(result => {
            redirect.push('/ServiciosPublicos/ExpedienteAlumbrado/list')
          })
      })
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

    // validations

    const validatePublicPlace = () => {
      if (!expedientLighting.value.lugarPublico) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }

    const validateNomenclature = () => {
      if (!expedientLighting.value.nomenclatura) {
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
      PublicPlaceState.value = expedientLighting.value.lugarPublico !== null
      // eslint-disable-next-line no-unneeded-ternary
      HighDateState.value = expedientLighting.value.fechaAlta !== null
      // eslint-disable-next-line no-unneeded-ternary
      LocationState.value = expedientLighting.value.localidad !== null
      // eslint-disable-next-line no-unneeded-ternary
      departamentState.value = expedientLighting.value.departamentoId !== null
      // eslint-disable-next-line no-unneeded-ternary
      DescriptionSolutionState.value = expedientLighting.value.descripcionSolucion !== null
      // eslint-disable-next-line no-unneeded-ternary
      NomenclatureState.value = expedientLighting.value.nomenclatura !== null
    }

    return {
      expedientLighting,
      breadcrumbItems,
      departaments,
      PublicPlaceState,
      HighDateState,
      LocationState,
      departamentState,
      NomenclatureState,
      router,
      DescriptionSolutionState,
      expedienteAlumbradoId,
      isPublicPlace,

      onUpdateExpedientLighting,
      validatePublicPlace,
      validateLocation,
      validateDepartament,
      validateDescriptionSolution,
      validateHighDate,
      validateNomenclature
    }
  }
}
</script>

<style scoped>
.form-check {
    display: inline-block
    }
</style>
