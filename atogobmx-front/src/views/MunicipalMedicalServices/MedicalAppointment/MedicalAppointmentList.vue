<template>
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
          background-color: rgb(94,80,238);
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
            <i class="bi bi-trash3">
              Archivar
            </i>
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
      button-size="lg"
      hide-footer
    >
      <Form @submit="addArmory">
        <b-row cols="3">
          <b-col>
            <b-form-group class="mt-3" label="Empleado Citante">
              <Field
                name="NomenclatureField"
                :rules="validateNomeclature"
                as="text"
              >
                <b-form-select
                  v-model="medicalAppointmentFields.empleadoId"
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
            <b-form-group class="mt-3" label="Fecha y hora">
              <Field name="BrandField" :rules="validateBrand" as="text">
                <Datepicker
                  v-model="medicalAppointmentFields.fechaAlta"
                  locale="es"
                >
                </Datepicker>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="BrandField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Descripcion">
              <Field name="BrandField" :rules="validateTypeWeapon" as="text">
                <b-form-textarea
                  placeholder="Describe el motivo de la cita"
                  v-model="medicalAppointmentFields.tipoArma"
                  :state="typeWeaponState"
                >
                </b-form-textarea>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="BrandField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <VueCal
              class="vuecal--rounded-theme vuecal--blue-theme"
              style="width: 1000px"
              ref="vuecal"
              active-view="day"
              locale="es"
              editable-events
              @cell-dblclick="
                $refs.vuecal.createEvent($event, 30, {
                  title: '',
                  class: 'blue-event'
                })
              "
            />
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
import MunicipalMedicalServices from '@/Services/municipalMedical.Services'
import Datepicker from '@vuepic/vue-datepicker'
import EmployeeServices from '@/Services/employee.Services'
import { Form, Field, ErrorMessage } from 'vee-validate'
import { ref, inject } from 'vue'
import VueCal from 'vue-cal'
import 'vue-cal/dist/vuecal.css'
// import { useToast } from 'vue-toast-notification'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    Form,
    Field,
    ErrorMessage,
    Datepicker,
    VueCal,
    EasyDataTable: window['vue3-easy-data-table']
  },
  setup () {
    const swal = inject('$swal')
    const {
      getMedicalAppointments,
      createWeapon,
      deleteWeapon
    } = MunicipalMedicalServices()
    const { getEmployeesUnfiled } = EmployeeServices()
    const medicalAppointments = ref([])
    const employees = ref([])
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
    const showModal = ref(false)
    const medicalAppointmentFields = ref({
      citaId: 0,
      fechaHora: null,
      empleadoId: null,
      descripcion: null,
      archivado: false
    })
    const weaponsFieldsBlank = ref(
      JSON.parse(JSON.stringify(medicalAppointmentFields))
    )
    const fields = ref([
      { value: 'empleados.nombreCompleto', text: 'Citante' },
      { value: 'fechaHora', text: 'Fecha y hora' },
      { value: 'descripcion', text: 'Descripción' },
      //   { value: 'calibre', text: 'Calibre' },
      //   { value: 'empleado.nombreCompleto', text: 'Portador' },
      { value: 'actions', text: 'Acciones' }
    ])
    getMedicalAppointments(data => {
      medicalAppointments.value = data
      if (medicalAppointments.value.length > 0) {
        isloading.value = false
      } else {
        if (medicalAppointments.value.length <= 0) {
          isloading.value = false
        }
      }
    })
    getEmployeesUnfiled(data => {
      employees.value = data
      if (data.length === 0) {
        swal.fire({
          title: 'No se encuentran empleados registrador!',
          text: 'Registre primero un empleado para continuar.',
          icon: 'warning'
        })
      }
    })
    const refreshTable = () => {
      isloading.value = true
      getMedicalAppointments(data => {
        medicalAppointments.value = data
        if (medicalAppointments.value.length > 0) {
          isloading.value = false
        } else {
          if (medicalAppointments.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }
    const addArmory = () => {
      createWeapon(medicalAppointmentFields.value, data => {
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
      if (!medicalAppointmentFields.value.nomenclatura) {
        nomenclatureState.value = false
        return 'Este campo es requerido'
      }
      nomenclatureState.value = true
      return true
    }
    const validateBrand = () => {
      if (!medicalAppointmentFields.value.marca) {
        brandState.value = false
        return 'Este campo es requerido'
      }
      brandState.value = true
      return true
    }
    const validateTypeWeapon = () => {
      if (!medicalAppointmentFields.value.tipoArma) {
        typeWeaponState.value = false
        return 'Este campo es requerido'
      }
      typeWeaponState.value = true
      return true
    }
    const validateGauge = () => {
      if (!medicalAppointmentFields.value.calibre) {
        gaugeState.value = false
        return 'Este campo es requerido'
      }
      gaugeState.value = true
      return true
    }
    const validateEmployee = () => {
      if (!medicalAppointmentFields.value.empleadoId) {
        employeeState.value = false
        return 'Este campo es requerido'
      }
      employeeState.value = true
      return true
    }
    const resetArmoryFields = () => {
      showModal.value = false
      nomenclatureState.value = false
      brandState.value = false
      typeWeaponState.value = false
      gaugeState.value = false
      employeeState.value = false
      medicalAppointmentFields.value = JSON.parse(
        JSON.stringify(weaponsFieldsBlank)
      )
    }
    return {
      medicalAppointments,
      employees,
      //   employeesArmory,
      fields,
      perPage,
      currentPage,
      filter,
      perPageSelect,
      weaponsFieldsBlank,
      medicalAppointmentFields,
      isloading,
      searchValue,
      searchField,
      nomenclatureState,
      brandState,
      typeWeaponState,
      gaugeState,
      employeeState,
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
      validateEmployee
    }
  }
}
</script>

<style scoped>
</style>
