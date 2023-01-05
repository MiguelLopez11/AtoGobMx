<template>
  <b-card class="m-2">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-card>
      <b-tabs content-class="mt-3" card>
        <b-tab title="Datos generales" active>
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
                  <ErrorMessage
                    class="text-danger"
                    name="SexField"
                  ></ErrorMessage>
                </b-form-group>
              </b-col>
              <b-col>
                <b-form-group class="mt-3" label="Alergias">
                  <b-form-textarea v-model="expedientMedical.alergias" />
                </b-form-group>
              </b-col>
              <b-col>
                <b-form-group class="mt-3" label="Discapacidades">
                  <b-form-textarea v-model="expedientMedical.discapacidad" />
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
        </b-tab>
        <b-tab title="Citas">
          <b-row align-h="end" class="mb-3 mr-1">
            <b-form-input
              size="lg"
              style="width: 350px"
              v-model="searchValue"
              type="search"
              placeholder="Buscar..."
            >
            </b-form-input>
            <b-button
              style="
                background-color: rgb(94, 80, 238);
                height: 50px;
                width: auto;
                font-size: 18px;
                margin-right: 15px;
                margin-left: 20px;
              "
              to="/ServiciosMedicos/Agenda"
            >
              <i class="bi bi-car-front-fill" />
              Agendar Cita
            </b-button>
          </b-row>
          <EasyDataTable
            rows-per-page-message="registros por pagina"
            empty-message="No se encuentran registros"
            table-class-name="customize-table"
            buttons-pagination
            border-cell
            :loading="isloading"
            :headers="fields"
            :items="medicalAppointments"
            :rows-per-page="5"
            :search-field="searchField"
            :search-value="searchValue"
            theme-color="#7367f0"
          >
            <template #header-actions="header">
              {{ header.text }}
            </template>
            <template #item-actions="items">
              <b-dropdown
                id="ActionsDropdown"
                size="lg"
                style="text-color: black"
                variant="link"
                toggle-class="text-decoration-none"
                dropright
                no-caret
              >
                <template #button-content>
                  <i class="bi bi-three-dots-vertical"></i>
                </template>
                <b-dropdown-item
                  @click="RemoveMedicalAppointment(items.citaId)"
                  class="m-1"
                  variant="outline-danger"
                >
                  <i class="bi bi-trash3"> Archivar </i>
                </b-dropdown-item>
                <b-dropdown-item
                  class="m-1"
                  variant="outline-warning"
                  :to="{
                    name: 'ServiciosMedicos-Cita-Edit',
                    params: { CitaId: items.citaId }
                  }"
                >
                  <i class="bi bi-pencil-square" />
                  Editar
                </b-dropdown-item>
              </b-dropdown>
            </template>
          </EasyDataTable>
        </b-tab>
        <b-tab
          title="Recetas"
        >
        <expedient-medical-appointment-list :EmpleadoId="empleadoId"></expedient-medical-appointment-list>
        </b-tab>
      </b-tabs>
    </b-card>
  </b-card>
</template>

<script>
import MunicipalMedicalServices from '@/Services/municipalMedical.Services'
import ExpedientMedicalAppointmentList from '@/views/MunicipalMedicalServices/MedicalExpedient/ExpedientMedicalAppointmentList.vue'
import { ref, inject } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { Form, Field, ErrorMessage } from 'vee-validate'
export default {
  components: {
    Form,
    Field,
    ErrorMessage,
    ExpedientMedicalAppointmentList
  },
  setup () {
    const swal = inject('$swal')
    const {
      getExpedientMedical,
      updateExpedientMedical,
      getMedicalAppointmentByEmployee,
      deleteMedicalAppointment
    } = MunicipalMedicalServices()
    const router = useRoute()
    // const $toast = useToast()
    const expedientMedical = ref([])
    const medicalAppointments = ref([])
    const empleadoId = ref(parseInt(router.params.EmpleadoId))
    const redirect = useRouter()
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('marca')
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
    const fields = ref([
      { value: 'empleados.nombreCompleto', text: 'Citante' },
      { value: 'motivo', text: 'Motivo de cita' },
      { value: 'fechaDesde', text: 'Fecha y hora de inicio' },
      { value: 'fechaHasta', text: 'Fecha y hora de termino' },
      { value: 'descripcion', text: 'Descripción' },
      { value: 'actions', text: 'Acciones' }
    ])
    getExpedientMedical(router.params.ExpedienteMedicoId, data => {
      expedientMedical.value = data
      getAppointments(data.empleadoId)
    })
    const getAppointments = empleadoId => {
      getMedicalAppointmentByEmployee(empleadoId, data => {
        medicalAppointments.value = data
        if (medicalAppointments.value.length > 0) {
          isloading.value = false
        } else {
          if (medicalAppointments.value.length <= 0) {
            isloading.value = false
          }
        }
      })
    }
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
    const RemoveMedicalAppointment = citaId => {
      isloading.value = true
      swal
        .fire({
          title: '¿Estas seguro?',
          text: 'No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Si, cancelar cita!',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            swal
              .fire({
                title: 'Arma archivada!',
                text: 'El arma ha sido archivado satisfactoriamente .',
                icon: 'success'
              })
              .then(result => {
                if (result.isConfirmed) {
                  deleteMedicalAppointment(citaId, data => {
                    redirect.push('/ServiciosMedicos/ExpedienteMedico/list')
                  })
                }
              })
          } else {
            isloading.value = false
          }
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
      heightState.value =
        expedientMedical.value.estatura !== null &&
        expedientMedical.value.estatura !== ''
      weightState.value =
        expedientMedical.value.peso !== null &&
        expedientMedical.value.peso !== ''
      bloodTypeState.value =
        expedientMedical.value.tipoSangre !== null &&
        expedientMedical.value.tipoSangre !== ''
      sexState.value =
        expedientMedical.value.sexo !== null &&
        expedientMedical.value.sexo !== ''
      return ''
    }
    return {
      expedientMedical,
      breadcrumbItems,
      empleadoId,
      heightState,
      weightState,
      bloodTypeState,
      sexState,
      isloading,
      searchValue,
      searchField,
      fields,
      perPage,
      currentPage,
      filter,
      perPageSelect,
      medicalAppointments,

      onUpdateWorkStation,
      validateState,
      validateHeight,
      validateWeight,
      validateBloodType,
      validateSex,
      getAppointments,
      RemoveMedicalAppointment
    }
  }
}
</script>

<style></style>
