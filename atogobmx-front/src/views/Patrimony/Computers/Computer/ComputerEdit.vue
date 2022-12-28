<template>
  <b-card class="m-2">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-card>
      <div>
        <h3>Editar Equipo</h3>
      </div>
      <b-tabs>
        <b-tab title="Datos generales del Equipo">
          <Form @submit="onUpdateComputer()">
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
                <b-form-group class="mt-3" label="Marca">
                  <Field name="BrandField" :rules="validateBrand" as="text">
                    <b-form-input
                      placeholder="Ingresa la marca y/o modelo del equipo"
                      v-model="computer.marca"
                      :state="brandState"
                    >
                    </b-form-input>
                  </Field>
                  <ErrorMessage
                    class="text-danger"
                    name="BrandField"
                  ></ErrorMessage>
                </b-form-group>
              </b-col>
              <b-col>
                <b-form-group class="mt-3" label="Memoria RAM">
                  <Field name="memoryField" :rules="validateMemory" as="text">
                    <b-form-input
                      placeholder="Ingresa la cantidad de memoria que contiene el equipo"
                      v-model="computer.memoriaRAM"
                      :state="memoryState"
                    >
                    </b-form-input>
                  </Field>
                  <ErrorMessage
                    class="text-danger"
                    name="memoryField"
                  ></ErrorMessage>
                </b-form-group>
              </b-col>
              <b-col>
                <b-form-group class="mt-3" label="Almacenamiento">
                  <Field name="StorageField" :rules="validateStorage" as="text">
                    <b-form-input
                      placeholder="Describe brevemente el almacenamiento que tiene el equipo"
                      v-model="computer.almacenamiento"
                      :state="storageState"
                    >
                    </b-form-input>
                  </Field>
                  <ErrorMessage
                    class="text-danger"
                    name="StorageField"
                  ></ErrorMessage>
                </b-form-group>
              </b-col>
              <b-col>
                <b-form-group class="mt-3" label="Procesador">
                  <Field
                    name="ProcessorField"
                    :rules="validateProcessor"
                    as="text"
                  >
                    <b-form-input
                      placeholder="Ingresa los datos del procesador que tiene el equipo"
                      v-model="computer.procesador"
                      :state="processorState"
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
          </Form>
        </b-tab>
        <b-tab title="Monitor">
          <DisplayCrud :EquipoComputoId="computerId" />
        </b-tab>
        <!-- <b-tab title="Teclado">
          <KeyboardCrud :EquipoComputoId="computerId" />
        </b-tab>
        <b-tab title="Mouse">
          <MouseCrud :EquipoComputoId="computerId" />
        </b-tab> -->
      </b-tabs>
    </b-card>
  </b-card>
</template>

<script>
import DisplayCrud from '@/views/Patrimony/Computers/Display/DisplayCrud.vue'
// import KeyboardCrud from '@/views/Patrimony/Computers/Keyboards/KeyboardCrud.vue'
// import MouseCrud from '@/views/Patrimony/Computers/Mouse/MouseCrud.vue'
import ComputerServices from '@/Services/computer.Services'
import DepartamentServices from '@/Services/departament.Services'
import { ref, inject } from 'vue'
import { useRoute, useRouter } from 'vue-router'
// import { useToast } from 'vue-toast-notification'
import { Form, Field, ErrorMessage } from 'vee-validate'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    Form,
    Field,
    ErrorMessage,
    DisplayCrud
    // KeyboardCrud,
    // MouseCrud
  },
  setup () {
    const { getComputer, updateComputer, getStatus } = ComputerServices()
    const { getDepartaments } = DepartamentServices()
    const swal = inject('$swal')
    // const $toast = useToast()
    const computer = ref([])
    const areas = ref([])
    const departaments = ref([])
    const statusComputers = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const folioState = ref(false)
    const brandState = ref(false)
    const memoryState = ref(false)
    const storageState = ref(false)
    const processorState = ref(false)
    const departamentState = ref(false)
    const stateComputerState = ref(false)
    const computerId = ref(parseInt(router.params.EquipoComputoId))
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Equipos de computo', to: '/PatrimonioMunicipal/EquiposComputo/list' },
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
          text:
            'No se encuentran estatus registrados en el sistema, registre primero un estatus para continuar.',
          icon: 'warning'
        })
      }
    })
    getDepartaments(data => {
      departaments.value = data
      if (data.length === 0) {
        swal.fire({
          title: 'No se encuentran departamentos registrados!',
          text:
            'No se encuentran departamentos registrados en el sistema, registre primero un departamento para continuar.',
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
      if (!/^(?=.*\d)(?=.*[a-zA-Z])([A-ZñÑáéíóúÁÉÍÓÚ])[A-Z0-9]+$/i.test(computer.value.codigoInventario)) {
        validateState()
        return 'El nombre del area solo puede contener letras'
      }
      validateState()
      return true
    }
    const validateBrand = () => {
      if (!computer.value.marca) {
        validateState()
        return 'Este campo es requerido'
      }
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(computer.value.marca)) {
        validateState()
        return 'El campo no puede contener solo espacios'
      }
      validateState()
      return true
    }
    const validateMemory = () => {
      if (!computer.value.memoriaRAM) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateStorage = () => {
      if (!computer.value.almacenamiento) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateProcessor = () => {
      if (!computer.value.procesador) {
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
        stateComputerState.value = false
        return 'Este campo es requerido'
      }
      stateComputerState.value = true
      return true
    }
    const validateState = () => {
      brandState.value =
        computer.value.marca !== '' &&
        computer.value.marca !== null &&
        /^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(computer.value.marca)
      memoryState.value =
        computer.value.memoriaRAM !== '' && computer.value.memoriaRAM !== null
      storageState.value =
        computer.value.almacenamiento !== '' &&
        computer.value.almacenamiento !== null
      processorState.value =
        computer.value.procesador !== '' && computer.value.procesador !== null
      departamentState.value =
        computer.value.departament !== 0 && computer.value.departament !== null
      stateComputerState.value =
        computer.value.estatusEquipoId !== 0 &&
        computer.value.estatusEquipoId !== null
      folioState.value = computer.value.codigoInventario !== null && /^(?=.*\d)(?=.*[a-zA-Z])([A-ZñÑáéíóúÁÉÍÓÚ])[A-Z0-9]+$/i.test(computer.value.codigoInventario)
      return ''
    }
    return {
      computer,
      breadcrumbItems,
      brandState,
      memoryState,
      storageState,
      processorState,
      departamentState,
      areas,
      departaments,
      computerId,
      statusComputers,
      stateComputerState,
      folioState,

      validateBrand,
      validateMemory,
      validateStorage,
      validateProcessor,
      validateDepartament,
      onUpdateComputer,
      validateState,
      validateStateComputer,
      validateFolio
    }
  }
}
</script>

<style></style>
