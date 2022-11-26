<template>
  <b-card class="m-2">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-card>
      <div>
        <h3>Editar Cita</h3>
      </div>
      <Form @submit="addMedicalAppointment">
        <b-row cols="1">
          <b-col>
            <b-form-group label="Empleado Citante">
              <Field
                name="NomenclatureField"
                as="text"
                :rules="validateEmployee"
              >
                <b-form-select
                  v-model="medicalAppointments.empleadoId"
                  autofocus
                  :state="employeeState"
                  value-field="empleadoId"
                  text-field="nombreCompleto"
                  :options="employees"
                >
                </b-form-select>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="NomenclatureField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group label="Motivo">
              <Field name="ReasonField" as="text" :rules="validateReason">
                <b-form-input
                  v-model="medicalAppointments.motivo"
                  placeholder="Ingresa el motivo de la cita"
                  size="xl"
                  :state="reasonState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="ReasonField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <!-- :rules="validateBrand" -->
            <b-form-group label="Fecha y hora">
              <Field name="DateField" as="text" :rules="validateDate">
                <Datepicker
                  v-model="date"
                  range
                  placeholder="Seleccionar ..."
                  auto-range="0"
                  selectText="Seleccionar"
                  cancelText="Cancelar"
                  modelType="yyyy-MM-dd HH:mm"
                  locale="es"
                  :state="dateState"
                >
                </Datepicker>
              </Field>
              <ErrorMessage class="text-danger" name="DateField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group label="Descripcion">
              <!--  -->
              <Field
                name="descriptionField"
                as="text"
                :rules="validateDescription"
              >
                <b-form-textarea
                  v-model="medicalAppointments.descripcion"
                  placeholder="Describe el motivo de la cita"
                  :state="descriptionState"
                >
                </b-form-textarea>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="descriptionField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            to="/ServiciosMedicos/Cita/list"
          >
            Cancelar
          </b-button>
          <b-button class="w-auto m-2" variant="success" type="submit">
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
import Datepicker from '@vuepic/vue-datepicker'
// import { useToast } from 'vue-toast-notification'
import { Form, Field, ErrorMessage } from 'vee-validate'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    Form,
    Field,
    ErrorMessage,
    Datepicker
  },
  setup () {
    const { getMedicalAppointment, updateMedicalAppointment } =
      MunicipalMedicalServices()
    const swal = inject('$swal')
    const medicalAppointments = ref([])
    const statusComputers = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const folioState = ref(false)
    const brandState = ref(false)
    const memoryState = ref(false)
    const storageState = ref(false)
    const processorState = ref(false)
    const departamentState = ref(false)
    const areaState = ref(false)
    const stateComputerState = ref(false)
    const citaId = ref(parseInt(router.params.CitaId))
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Equipos de computo', to: '/ServiciosMedicos/Cita/list' },
      { text: 'Editar-Equipo' }
    ])
    const onUpdateComputer = () => {
      updateMedicalAppointment(medicalAppointments.value, data => {
        swal
          .fire({
            title: 'Equipo modificado correctamente!',
            text: 'El equipo se ha modificado  satisfactoriamente.',
            icon: 'success'
          })
          .then(result => {
            if (result.isConfirmed) {
              redirect.push('/ServiciosMedicos/Cita/list')
            }
          })
      })
    }
    getMedicalAppointment(citaId.value, data => {
      medicalAppointments.value = data
      validateState()
    })
    // VALIDATIONS
    const validateFolio = () => {
      if (!medicalAppointments.value.codigoInventario) {
        validateState()
        return 'Este campo es requerido'
      }
      // eslint-disable-next-line no-useless-escape
      if (
        !/^(?=.*\d)(?=.*[a-zA-Z])([A-ZñÑáéíóúÁÉÍÓÚ])[A-Z0-9]+$/i.test(
          medicalAppointments.value.codigoInventario
        )
      ) {
        validateState()
        return 'El nombre del area solo puede contener letras'
      }
      validateState()
      return true
    }
    const validateBrand = () => {
      if (!medicalAppointments.value.marca) {
        validateState()
        return 'Este campo es requerido'
      }
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(medicalAppointments.value.marca)) {
        validateState()
        return 'El campo no puede contener solo espacios'
      }
      validateState()
      return true
    }
    const validateMemory = () => {
      if (!medicalAppointments.value.memoriaRAM) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateStorage = () => {
      if (!medicalAppointments.value.almacenamiento) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateProcessor = () => {
      if (!medicalAppointments.value.procesador) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateDepartament = () => {
      if (!medicalAppointments.value.departamentoId) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateArea = () => {
      if (!medicalAppointments.value.areaId) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateStateComputer = () => {
      if (!medicalAppointments.value.estatusEquipoId) {
        stateComputerState.value = false
        return 'Este campo es requerido'
      }
      stateComputerState.value = true
      return true
    }
    const validateState = () => {
      brandState.value =
        medicalAppointments.value.marca !== '' &&
        medicalAppointments.value.marca !== null &&
        /^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(medicalAppointments.value.marca)
      memoryState.value =
        medicalAppointments.value.memoriaRAM !== '' &&
        medicalAppointments.value.memoriaRAM !== null
      storageState.value =
        medicalAppointments.value.almacenamiento !== '' &&
        medicalAppointments.value.almacenamiento !== null
      processorState.value =
        medicalAppointments.value.procesador !== '' &&
        medicalAppointments.value.procesador !== null
      departamentState.value =
        medicalAppointments.value.departament !== 0 &&
        medicalAppointments.value.departament !== null
      areaState.value =
        medicalAppointments.value.areaId !== 0 &&
        medicalAppointments.value.areaId !== null
      stateComputerState.value =
        medicalAppointments.value.estatusEquipoId !== 0 &&
        medicalAppointments.value.estatusEquipoId !== null
      folioState.value =
        medicalAppointments.value.codigoInventario !== null &&
        /^(?=.*\d)(?=.*[a-zA-Z])([A-ZñÑáéíóúÁÉÍÓÚ])[A-Z0-9]+$/i.test(
          medicalAppointments.value.codigoInventario
        )
      return ''
    }
    return {
      medicalAppointments,
      breadcrumbItems,
      brandState,
      memoryState,
      storageState,
      processorState,
      departamentState,
      areaState,
      citaId,
      statusComputers,
      stateComputerState,
      folioState,

      validateBrand,
      validateMemory,
      validateStorage,
      validateProcessor,
      validateDepartament,
      validateArea,
      onUpdateComputer,
      validateState,
      validateStateComputer,
      validateFolio
    }
  }
}
</script>

<style></style>
