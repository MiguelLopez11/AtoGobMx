<template>
  <b-card class="m-2">
    <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
  </b-card>
  <b-card class="m-2">
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
        @click="showModal = !showModal"
        type="submit"
      >
        <i class="bi bi-car-front-fill" />
        Agregar Arma
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
      :items="weapons"
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
            @click="RemoveArmory(items.armaId)"
            class="m-1"
            variant="outline-danger"
          >
            <i class="bi bi-trash3"> Archivar </i>
          </b-dropdown-item>
          <b-dropdown-item
            class="m-1"
            variant="outline-warning"
            :to="{
              name: 'Armeria-Edit',
              params: { ArmaId: items.armaId }
            }"
          >
            <i class="bi bi-pencil-square" />
            Editar
          </b-dropdown-item>
        </b-dropdown>
      </template>
    </EasyDataTable>
    <b-modal
      v-model="showModal"
      title="Registrar Arma"
      size="xl"
      centered
      button-size="lg"
      hide-footer
    >
      <Form @submit="addArmory">
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
                  v-model="weaponsFields.nomenclatura"
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
                  v-model="weaponsFields.marca"
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
              <Field name="BrandField" :rules="validateTypeWeapon" as="text">
                <b-form-input
                  placeholder="Ingresa el tipo de arma"
                  v-model="weaponsFields.tipoArma"
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
                  v-model="weaponsFields.calibre"
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
                  v-model="weaponsFields.empleadoId"
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
              <Field name="memoryField" :rules="validateDate" as="text">
                <Datepicker
                  v-model="weaponsFields.fechaAdquisicion"
                  locale="es"
                  autoApply
                  :enableTimePicker="false"
                  :state="dateState"
                >
                </Datepicker>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="memoryField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Costo">
              <Field name="CostField" :rules="validateCost" as="text">
                <b-form-input
                  v-model="weaponsFields.costo"
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
        </b-row>
        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            @click="resetArmoryFields()"
          >
            Cancelar
          </b-button>
          <b-button class="w-auto m-2" variant="success" type="submit">
            Guardar
          </b-button>
        </b-row>
      </Form>
    </b-modal>
  </b-card>
</template>

<script>
import ArmoryServices from '@/Services/armory.Services'
import EmployeeServices from '@/Services/employee.Services'
import { Form, Field, ErrorMessage } from 'vee-validate'
import { ref, inject } from 'vue'
import Datepicker from '@vuepic/vue-datepicker'
// import { useToast } from 'vue-toast-notification'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    Form,
    Field,
    ErrorMessage,
    Datepicker,
    EasyDataTable: window['vue3-easy-data-table']
  },
  setup () {
    const swal = inject('$swal')
    const { getWeapons, createWeapon, deleteWeapon } = ArmoryServices()
    const { getEmployeesArmory } = EmployeeServices()
    const weapons = ref([])
    const employeesArmory = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('marca')
    const nomenclatureState = ref(false)
    const brandState = ref(false)
    const typeWeaponState = ref(false)
    const gaugeState = ref(false)
    const employeeState = ref(false)
    const dateState = ref(false)
    const costState = ref(false)
    const showModal = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      {
        text: 'Patrimonio publico',
        to: '/PatrimonioMunicipal'
      },
      { text: 'Armeria' }
    ])
    const weaponsFields = ref({
      armaId: 0,
      nomenclatura: null,
      marca: null,
      tipoArma: null,
      calibre: null,
      empleadoId: 0,
      fechaAdquisicion: null,
      costo: null,
      archivado: false
    })
    const weaponsFieldsBlank = ref(JSON.parse(JSON.stringify(weaponsFields)))
    const fields = ref([
      { value: 'nomenclatura', text: 'Nomenclatura' },
      { value: 'marca', text: 'Marca' },
      { value: 'tipoArma', text: 'Tipo de Arma' },
      { value: 'calibre', text: 'Calibre' },
      { value: 'empleado.nombreCompleto', text: 'Portador' },
      { value: 'fechaAdquisicion', text: 'Fecha de adquisición' },
      { value: 'costo', text: 'Costo de adquisición' },
      { value: 'actions', text: 'Acciones' }
    ])
    getWeapons(data => {
      weapons.value = data
      if (weapons.value.length > 0) {
        isloading.value = false
      } else {
        if (weapons.value.length <= 0) {
          isloading.value = false
        }
      }
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
    const refreshTable = () => {
      isloading.value = true
      getWeapons(data => {
        weapons.value = data
        if (weapons.value.length > 0) {
          isloading.value = false
        } else {
          if (weapons.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }
    const addArmory = () => {
      createWeapon(weaponsFields.value, data => {
        refreshTable()
        swal.fire({
          title: 'Arma registrada correctamente!',
          text: 'El arma se ha registrado al sistema satisfactoriamente.',
          icon: 'success'
        })
      })
      resetArmoryFields()
    }
    const RemoveArmory = ArmaId => {
      isloading.value = true
      swal
        .fire({
          title: '¿Estas seguro?',
          text: 'No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Si, archivar arma!',
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
                  deleteWeapon(ArmaId, data => {
                    refreshTable()
                  })
                }
              })
          } else {
            isloading.value = false
          }
        })
    }
    const onFiltered = filteredItems => {
      currentPage.value = 1
    }
    // VALIDATIONS
    const validateNomeclature = () => {
      if (!weaponsFields.value.nomenclatura) {
        nomenclatureState.value = false
        return 'Este campo es requerido'
      }
      nomenclatureState.value = true
      return true
    }
    const validateBrand = () => {
      if (!weaponsFields.value.marca) {
        brandState.value = false
        return 'Este campo es requerido'
      }
      brandState.value = true
      return true
    }
    const validateTypeWeapon = () => {
      if (!weaponsFields.value.tipoArma) {
        typeWeaponState.value = false
        return 'Este campo es requerido'
      }
      typeWeaponState.value = true
      return true
    }
    const validateGauge = () => {
      if (!weaponsFields.value.calibre) {
        gaugeState.value = false
        return 'Este campo es requerido'
      }
      gaugeState.value = true
      return true
    }
    const validateEmployee = () => {
      if (!weaponsFields.value.empleadoId) {
        employeeState.value = false
        return 'Este campo es requerido'
      }
      employeeState.value = true
      return true
    }
    const validateDate = () => {
      if (!weaponsFields.value.fechaAdquisicion) {
        dateState.value = false
        return 'Este campo es requerido'
      }
      dateState.value = true
      return true
    }
    const validateCost = () => {
      if (!weaponsFields.value.costo) {
        costState.value = false
        return 'Este campo es requerido'
      }
      costState.value = true
      return true
    }
    const resetArmoryFields = () => {
      showModal.value = false
      nomenclatureState.value = false
      brandState.value = false
      typeWeaponState.value = false
      gaugeState.value = false
      employeeState.value = false
      costState.value = false
      dateState.value = false
      weaponsFields.value = JSON.parse(JSON.stringify(weaponsFieldsBlank))
    }
    return {
      weapons,
      employeesArmory,
      breadcrumbItems,
      fields,
      perPage,
      currentPage,
      filter,
      perPageSelect,
      weaponsFieldsBlank,
      weaponsFields,
      isloading,
      searchValue,
      searchField,
      nomenclatureState,
      brandState,
      typeWeaponState,
      gaugeState,
      employeeState,
      costState,
      dateState,
      showModal,

      onFiltered,
      addArmory,
      refreshTable,
      RemoveArmory,
      resetArmoryFields,
      validateNomeclature,
      validateBrand,
      validateTypeWeapon,
      validateGauge,
      validateDate,
      validateCost,
      validateEmployee
    }
  }
}
</script>

<style></style>
