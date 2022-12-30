<template>
  <b-card class="m-2">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <div>
      <h3>Editar Equipo</h3>
    </div>
    <b-tabs>
      <b-tab title="Equipo">
        <Form @submit="onUpdateComputer()">
          <b-container fluid>
            <b-row cols="3">
              <b-col>
                <b-form-group class="mt-3" label="Nomenclatura">
                  <Field name="FolioField" :rules="validateFolio" as="text">
                    <b-form-input
                      v-model="computer.codigoInventario"
                      :state="folioState"
                    >
                    </b-form-input>
                  </Field>
                  <ErrorMessage
                    class="text-danger"
                    name="FolioField"
                  ></ErrorMessage>
                </b-form-group>
              </b-col>
              <b-col>
                <b-form-group class="mt-3" label="Descripción/Caracteristicas">
                  <Field
                    name="DescriptionField"
                    :rules="validateDescription"
                    as="text"
                  >
                    <b-form-textarea
                      placeholder="Ingresa las Caracteristicas del equipo"
                      v-model="computer.caracteristicas"
                      :state="DescriptionState"
                      rows="4"
                    >
                    </b-form-textarea>
                  </Field>
                  <ErrorMessage
                    class="text-danger"
                    name="DescriptionField"
                  ></ErrorMessage>
                </b-form-group>
              </b-col>
              <b-col>
                <b-form-group class="mt-3" label="Fecha de Adquisición">
                  <Field name="DateField" :rules="validateDate" as="datetime">
                    <Datepicker
                      v-model="computer.fechaAdquisicion"
                      locale="es"
                      autoApply
                      :enableTimePicker="false"
                      :state="dateState"
                    >
                    </Datepicker>
                  </Field>
                  <ErrorMessage
                    class="text-danger"
                    name="DateField"
                  ></ErrorMessage>
                </b-form-group>
              </b-col>
              <b-col>
                <b-form-group class="mt-3" label="Costo de adquisición">
                  <Field name="CostField" :rules="validateCost" as="text">
                    <b-form-input
                      placeholder="Describe brevemente el almacenamiento que tiene el equipo"
                      v-model="computer.costo"
                      :state="costState"
                    >
                    </b-form-input>
                  </Field>
                  <ErrorMessage
                    class="text-danger"
                    name="CostField"
                  ></ErrorMessage>
                </b-form-group>
              </b-col>
              <b-col>
                <b-form-group class="mt-3" label="Numero de serie">
                  <Field
                    name="ProcessorField"
                    :rules="validateSerialNumber"
                    as="text"
                  >
                    <b-form-input
                      placeholder="Ingresa el numero de serie del equipo"
                      v-model="computer.numeroSerie"
                      :state="serialNumberState"
                    >
                    </b-form-input>
                  </Field>
                  <ErrorMessage
                    class="text-danger"
                    name="ProcessorField"
                  ></ErrorMessage>
                </b-form-group>
              </b-col>
              <b-col>
                <b-form-group class="mt-3" label="Departamento">
                  <Field
                    name="DepartamentField"
                    :rules="validateDepartament"
                    as="text"
                  >
                    <b-form-select
                      v-model="computer.departamentoId"
                      autofocus
                      :options="departaments"
                      value-field="departamentoId"
                      text-field="nombre"
                      :state="departamentState"
                    >
                    </b-form-select>
                  </Field>
                  <ErrorMessage
                    class="text-danger"
                    name="DepartamentField"
                  ></ErrorMessage>
                </b-form-group>
              </b-col>
              <b-col>
                <b-form-group class="mt-3" label="Estatus">
                  <Field
                    name="StatusField"
                    :rules="validateStateComputer"
                    as="number"
                  >
                    <b-form-select
                      v-model="computer.estatusEquipoId"
                      autofocus
                      :options="statusComputers"
                      value-field="estatusEquipoId"
                      text-field="nombre"
                      :state="stateComputerState"
                    >
                    </b-form-select>
                  </Field>
                  <ErrorMessage
                    class="text-danger"
                    name="StatusField"
                  ></ErrorMessage>
                </b-form-group>
              </b-col>
            </b-row>
            <b-row align-h="end">
              <b-button
                class="w-auto m-2 text-white"
                variant="primary"
                to="/PatrimonioMunicipal/EquiposComputo/list"
              >
                Cancelar
              </b-button>
              <b-button class="w-auto m-2" variant="success" type="submit">
                Guardar
              </b-button>
            </b-row>
          </b-container>
        </Form>
      </b-tab>
      <b-tab
        title="Documentos"
      >
      <ComputerDocuments :EquipoId="computerId" :EquipoComputo="computer"/>
      </b-tab>
    </b-tabs>
  </b-card>
</template>

<script>
import ComputerServices from '@/Services/computer.Services'
import DepartamentServices from '@/Services/departament.Services'
import ComputerDocuments from './ComputerDocuments.vue'
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
    Datepicker,
    ComputerDocuments
  },
  setup () {
    const { getComputer, updateComputer, getStatus } = ComputerServices()
    const { getDepartaments } = DepartamentServices()
    const swal = inject('$swal')
    const computer = ref([])
    const areas = ref([])
    const departaments = ref([])
    const statusComputers = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const folioState = ref(false)
    const DescriptionState = ref(false)
    const dateState = ref(false)
    const costState = ref(false)
    const serialNumberState = ref(false)
    const departamentState = ref(false)
    const stateComputerState = ref(false)
    const computerId = ref(parseInt(router.params.EquipoComputoId))
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      {
        text: 'Equipos de computo',
        to: '/PatrimonioMunicipal/EquiposComputo/list'
      },
      { text: 'Editar-Equipo' }
    ])
    const onUpdateComputer = () => {
      updateComputer(computer.value, data => {
        swal
          .fire({
            title: 'Equipo modificado correctamente!',
            text: 'El equipo se ha modificado  satisfactoriamente.',
            icon: 'success'
          })
          .then(result => {
            if (result.isConfirmed) {
              redirect.push('/PatrimonioMunicipal/EquiposComputo/list')
            }
          })
      })
    }
    getComputer(computerId.value, data => {
      computer.value = data
      validateState()
    })
    getStatus(data => {
      statusComputers.value = data
      if (data.length === 0) {
        swal.fire({
          title: 'No se encuentran estatus registrados!',
          text: 'No se encuentran estatus registrados en el sistema, registre primero un estatus para continuar.',
          icon: 'warning'
        })
      }
    })
    getDepartaments(data => {
      departaments.value = data
      if (data.length === 0) {
        swal.fire({
          title: 'No se encuentran departamentos registrados!',
          text: 'No se encuentran departamentos registrados en el sistema, registre primero un departamento para continuar.',
          icon: 'warning'
        })
      }
    })
    // VALIDATIONS
    const validateFolio = () => {
      if (!computer.value.codigoInventario) {
        validateState()
        return 'Este campo es requerido'
      }
      // eslint-disable-next-line no-useless-escape
      if (
        !/^(?=.*\d)(?=.*[a-zA-Z])([A-ZñÑáéíóúÁÉÍÓÚ])[A-Z0-9]+$/i.test(
          computer.value.codigoInventario
        )
      ) {
        validateState()
        return 'El nombre del area solo puede contener letras y numeros'
      }
      validateState()
      return true
    }
    const validateDescription = () => {
      if (!computer.value.caracteristicas) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateDate = () => {
      if (!computer.value.fechaAdquisicion) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateCost = () => {
      if (!computer.value.costo) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateSerialNumber = () => {
      if (!computer.value.numeroSerie) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateDepartament = () => {
      if (!computer.value.departamentoId) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateStateComputer = () => {
      if (!computer.value.estatusEquipoId) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateState = () => {
      costState.value =
        computer.value.costo !== '' && computer.value.costo !== null
      dateState.value =
        computer.value.fechaAdquisicion !== '' &&
        computer.value.fechaAdquisicion !== null
      DescriptionState.value =
        computer.value.caracteristicas !== '' &&
        computer.value.caracteristicas !== null
      serialNumberState.value =
        computer.value.numeroSerie !== '' && computer.value.numeroSerie !== null
      departamentState.value =
        computer.value.departament !== 0 && computer.value.departament !== null
      stateComputerState.value =
        computer.value.estatusEquipoId !== 0 &&
        computer.value.estatusEquipoId !== null
      folioState.value =
        computer.value.codigoInventario !== null &&
        /^(?=.*\d)(?=.*[a-zA-Z])([A-ZñÑáéíóúÁÉÍÓÚ])[A-Z0-9]+$/i.test(
          computer.value.codigoInventario
        )
      return ''
    }
    return {
      computer,
      breadcrumbItems,
      costState,
      DescriptionState,
      serialNumberState,
      departamentState,
      dateState,
      stateComputerState,
      areas,
      departaments,
      computerId,
      statusComputers,
      folioState,

      validateDescription,
      validateDate,
      validateSerialNumber,
      validateCost,
      validateDepartament,
      validateStateComputer,
      onUpdateComputer,
      validateState,
      validateFolio
    }
  }
}
</script>

<style></style>
