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
        Nueva receta
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
      :items="prescriptions"
      :rows-per-page="5"
      :search-field="searchField"
      :search-value="searchValue"
      theme-color="#7367f0"
    >
      <template #header-actions="header">
        {{ header.text }}
      </template>
      <template #item-empleados="items">
        <span v-if="items.empleados !== null">
        {{ items.empleados.nombreCompleto  }}
        </span>
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
            @click="RemovePrescription(items.recetaId)"
            class="m-1"
            variant="outline-danger"
          >
            <i class="bi bi-trash3"> Archivar </i>
          </b-dropdown-item>
          <b-dropdown-item
            class="m-1"
            variant="outline-warning"
            :to="{
              name: 'ServiciosMedicos-Receta-Edit',
              params: { RecetaId: items.recetaId }
            }"
          >
            <i class="bi bi-pencil-square" />
            Editar
          </b-dropdown-item>
        </b-dropdown>
      </template>
      <template #item-status="items">
        <b-badge :variant="items.estatusReceta.nombre === 'Pendiente' ? 'warning': 'success'">
        {{items.estatusReceta.nombre}}
        </b-badge>
      </template>
    </EasyDataTable>
    <b-modal
      v-model="showModal"
      title="Nueva receta"
      size="xl"
      centered
      button-size="lg"
      hide-footer
    >
      <Form @submit="addPrescription">
        <b-row>
          <b-col>
            <b-form-group class="mt-3" label="Empleado">
              <b-form-select v-model="prescriptionFields.empleadoId" autofocus>
                <option
                  v-for="option in employees"
                  :key="option.ID"
                  :value="option.empleadoId"
                >
                  {{ option.nombreCompleto }}, {{ option.departamentos.nombre }}, {{option.area.nombre}}
                </option>
              </b-form-select>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            @click="() => (showModal = !showModal)"
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
import PrescriptionServices from '@/Services/prescription.Services'
import EmployeeServices from '@/Services/employee.Services'
import { Form } from 'vee-validate'
import { ref, inject } from 'vue'
import { useRouter } from 'vue-router'

// import { useToast } from 'vue-toast-notification'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    Form,
    // Field,
    // ErrorMessage,
    EasyDataTable: window['vue3-easy-data-table']
  },
  setup () {
    const swal = inject('$swal')
    const { getPrescriptions, createPrescription, deletePrescription } =
      PrescriptionServices()
    const { getEmployeesUnfiled } = EmployeeServices()
    const prescriptions = ref([])
    const showModal = ref(false)
    const redirect = useRouter()
    const employees = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('marca')
    const prescriptionFields = ref({
      recetaId: 0,
      empleadoId: null,
      diagnostico: '',
      archivado: false
    })
    const weaponsFieldsBlank = ref(
      JSON.parse(JSON.stringify(prescriptionFields))
    )
    const fields = ref([
      { value: 'empleados', text: 'Empleado' },
      { value: 'diagnostico', text: 'Diagnostico' },
      { value: 'fechaAlta', text: 'Fecha' },
      { value: 'status', text: 'Estado' },
      { value: 'actions', text: 'Acciones' }
    ])
    getPrescriptions(data => {
      prescriptions.value = data
      if (prescriptions.value.length > 0) {
        isloading.value = false
      } else {
        if (prescriptions.value.length <= 0) {
          isloading.value = false
        }
      }
    })
    getEmployeesUnfiled(data => {
      employees.value = data
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
      getPrescriptions(data => {
        prescriptions.value = data
        if (prescriptions.value.length > 0) {
          isloading.value = false
        } else {
          if (prescriptions.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }
    const addPrescription = () => {
      createPrescription(prescriptionFields.value, data => {
        swal.fire({
          title: 'Receta registrada correctamente!',
          text: 'La receta se ha registrado al sistema satisfactoriamente.',
          icon: 'success'
        })
        redirect.push({
          name: 'ServiciosMedicos-Receta-Edit',
          params: { RecetaId: data.recetaId }
        })
      })
    }
    const RemovePrescription = recetaId => {
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
                  deletePrescription(recetaId, data => {
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
    // const validateNomeclature = () => {
    //   if (!prescriptionFields.value.nomenclatura) {
    //     nomenclatureState.value = false
    //     return 'Este campo es requerido'
    //   }
    //   nomenclatureState.value = true
    //   return true
    // }
    // const validateBrand = () => {
    //   if (!prescriptionFields.value.marca) {
    //     brandState.value = false
    //     return 'Este campo es requerido'
    //   }
    //   brandState.value = true
    //   return true
    // }
    // const validateTypeWeapon = () => {
    //   if (!prescriptionFields.value.tipoArma) {
    //     typeWeaponState.value = false
    //     return 'Este campo es requerido'
    //   }
    //   typeWeaponState.value = true
    //   return true
    // }
    // const validateGauge = () => {
    //   if (!prescriptionFields.value.calibre) {
    //     gaugeState.value = false
    //     return 'Este campo es requerido'
    //   }
    //   gaugeState.value = true
    //   return true
    // }
    // const validateEmployee = () => {
    //   if (!prescriptionFields.value.empleadoId) {
    //     employeeState.value = false
    //     return 'Este campo es requerido'
    //   }
    //   employeeState.value = true
    //   return true
    // }
    // const resetArmoryFields = () => {
    //   showModal.value = false
    //   nomenclatureState.value = false
    //   brandState.value = false
    //   typeWeaponState.value = false
    //   gaugeState.value = false
    //   employeeState.value = false
    //   prescriptionFields.value = JSON.parse(JSON.stringify(weaponsFieldsBlank))
    // }
    return {
      prescriptions,
      employees,
      fields,
      perPage,
      currentPage,
      filter,
      perPageSelect,
      weaponsFieldsBlank,
      prescriptionFields,
      isloading,
      searchValue,
      showModal,
      searchField,
      //   nomenclatureState,
      //   brandState,
      //   typeWeaponState,
      //   gaugeState,
      //   employeeState,

      onFiltered,
      addPrescription,
      refreshTable,
      RemovePrescription
    }
  }
}
</script>

<style></style>
