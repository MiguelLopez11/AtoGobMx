<template>
  <b-card class="m-2">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-card>
      <div>
        <h3>Editar Arma</h3>
      </div>
      <b-tabs>
        <b-tab title="Datos generales">
          <Form @submit="onUpdateVehicle">
            <b-row cols="3">
              <b-col>
                <b-form-group class="mt-3" label="Nomenclatura">
                  <Field
                    name="NomenclatureField"
                    :rules="validateNomeclature"
                    as="text"
                  >
                    <b-form-input
                      placeholder="Ingresa una nomenclatura"
                      v-model="weapon.nomenclatura"
                      :state="nomenclatureState"
                    >
                    </b-form-input>
                  </Field>
                  <ErrorMessage
                    class="text-danger"
                    name="NomenclatureField"
                  ></ErrorMessage>
                </b-form-group>
              </b-col>
              <b-col>
                <b-form-group class="mt-3" label="Marca">
                  <Field name="BrandField" :rules="validateBrand" as="text">
                    <b-form-input
                      placeholder="Ingresa la marca del arma"
                      v-model="weapon.marca"
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
                <b-form-group class="mt-3" label="Tipo de arma">
                  <Field
                    name="BrandField"
                    :rules="validateTypeWeapon"
                    as="text"
                  >
                    <b-form-input
                      placeholder="Ingresa el tipo de arma"
                      v-model="weapon.tipoArma"
                      :state="typeWeaponState"
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
                <b-form-group class="mt-3" label="Calibre">
                  <Field name="modelField" :rules="validateGauge" as="text">
                    <b-form-input
                      placeholder="Ingresa el calibre del arma"
                      v-model="weapon.calibre"
                      :state="gaugeState"
                      type="number"
                    >
                    </b-form-input>
                  </Field>
                  <ErrorMessage
                    class="text-danger"
                    name="modelField"
                  ></ErrorMessage>
                </b-form-group>
              </b-col>
              <b-col>
                <b-form-group class="mt-3" label="Empleado portador">
                  <Field name="ColorField" :rules="validateEmployee" as="text">
                    <b-form-select
                      v-model="weapon.empleadoId"
                      autofocus
                      :state="employeeState"
                      value-field="empleadoId"
                      text-field="nombreCompleto"
                      :options="employeesArmory"
                    >
                    </b-form-select>
                  </Field>
                  <ErrorMessage
                    class="text-danger"
                    name="ColorField"
                  ></ErrorMessage>
                </b-form-group>
              </b-col>
              <b-col>
                <b-form-group class="mt-3" label="Fecha de Adquisición">
                  <Field name="DateField" :rules="validateDate" as="datetime">
                    <Datepicker
                      v-model="weapon.fechaAdquisicion"
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
                    <b-form-input v-model="weapon.costo" :state="costState">
                    </b-form-input>
                  </Field>
                  <ErrorMessage
                    class="text-danger"
                    name="CostField"
                  ></ErrorMessage>
                </b-form-group>
              </b-col>
            </b-row>
            <b-row align-h="end">
              <b-button
                class="w-auto m-2 text-white"
                variant="primary"
                to="/PatrimonioMunicipal/Armeria/list"
              >
                Cancelar
              </b-button>
              <b-button class="w-auto m-2" variant="success" type="submit">
                Guardar
              </b-button>
            </b-row>
          </Form>
        </b-tab>
        <b-tab
          title="Cartuchos"
        >
          <cart-ridge :ArmaId="armaId"/>
        </b-tab>
        <b-tab
          title="Documentos"
        >
          <armory-documents :ArmaId="armaId"/>
        </b-tab>
      </b-tabs>
    </b-card>
  </b-card>
</template>

<script>
import ArmoryServices from '@/Services/armory.Services'
import EmployeeServices from '@/Services/employee.Services'
import CartRidge from './Cartridge/CartridgeList.vue'
import ArmoryDocuments from './ArmoryDocuments/ArmoryDocuments.vue'
import { Field, Form, ErrorMessage } from 'vee-validate'
import { ref, inject } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import Datepicker from '@vuepic/vue-datepicker'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    Field,
    Form,
    ErrorMessage,
    Datepicker,
    CartRidge,
    ArmoryDocuments
  },
  setup () {
    const swal = inject('$swal')
    const { getEmployeesArmory } = EmployeeServices()
    const { getWeapon, updateWeapon } = ArmoryServices()
    const weapon = ref([])
    const employeesArmory = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const nomenclatureState = ref(false)
    const brandState = ref(false)
    const typeWeaponState = ref(false)
    const gaugeState = ref(false)
    const employeeState = ref(false)
    const dateState = ref(false)
    const costState = ref(false)
    const armaId = ref(parseInt(router.params.ArmaId))
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Armeria', to: '/PatrimonioMunicipal/Armeria/list' },
      { text: 'Editar Arma' }
    ])
    getWeapon(router.params.ArmaId, data => {
      weapon.value = data
      validateState()
    })
    getEmployeesArmory(data => {
      employeesArmory.value = data
      if (data.length === 0) {
        swal.fire({
          title: '¡No se encuentran empleados!',
          text: 'Registre un empleado al que se pueda asignar un arma',
          icon: 'warning'
        })
      }
    })
    const onUpdateVehicle = () => {
      updateWeapon(weapon.value, data => {})
      swal
        .fire({
          title: 'Arma modificada correctamente!',
          text: 'El arma se ha modificado al sistema satisfactoriamente.',
          icon: 'success'
        })
        .then(result => {
          if (result.isConfirmed) {
            redirect.push('/PatrimonioMunicipal/Armeria/list')
          }
        })
    }
    // VALIDATIONS
    const validateNomeclature = () => {
      if (!weapon.value.nomenclatura) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateBrand = () => {
      if (!weapon.value.marca) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateTypeWeapon = () => {
      if (!weapon.value.tipoArma) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateGauge = () => {
      if (!weapon.value.calibre) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateEmployee = () => {
      if (!weapon.value.empleadoId) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateDate = () => {
      if (!weapon.value.fechaAdquisicion) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateCost = () => {
      if (!weapon.value.costo) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateState = () => {
      nomenclatureState.value =
        weapon.value.nomenclatura !== null && weapon.value.nomenclatura !== ''
      brandState.value =
        weapon.value.marca !== null && weapon.value.marca !== ''
      typeWeaponState.value =
        weapon.value.tipoArma !== null && weapon.value.tipoArma !== ''
      gaugeState.value =
        weapon.value.calibre !== null && weapon.value.calibre !== ''
      employeeState.value = weapon.value.empleadoId !== null
      costState.value = weapon.value.costo !== '' && weapon.value.costo !== null
      dateState.value =
        weapon.value.fechaAdquisicion !== '' &&
        weapon.value.fechaAdquisicion !== null
      return ''
    }
    return {
      weapon,
      employeesArmory,
      breadcrumbItems,
      router,
      redirect,
      nomenclatureState,
      brandState,
      typeWeaponState,
      gaugeState,
      employeeState,
      costState,
      dateState,
      armaId,

      validateNomeclature,
      validateBrand,
      validateTypeWeapon,
      validateGauge,
      validateEmployee,
      validateDate,
      validateCost,

      onUpdateVehicle,
      validateState
    }
  }
}
</script>

<style></style>
