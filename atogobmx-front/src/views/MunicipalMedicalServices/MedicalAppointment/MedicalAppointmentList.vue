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
  </b-card>
</template>

<script>
import MunicipalMedicalServices from '@/Services/municipalMedical.Services'
import EmployeeServices from '@/Services/employee.Services'
import { ref, inject } from 'vue'
export default {
  setup () {
    const swal = inject('$swal')
    const { getMedicalAppointments, deleteMedicalAppointment } =
      MunicipalMedicalServices()
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
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      {
        text: 'Servicios medicos',
        to: '/ServiciosMedicosMunicipales'
      },
      { text: 'Citas' }
    ])
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
      { value: 'motivo', text: 'Motivo de cita' },
      { value: 'fechaDesde', text: 'Fecha y hora de inicio' },
      { value: 'fechaHasta', text: 'Fecha y hora de termino' },
      { value: 'descripcion', text: 'Descripción' },
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
    return {
      medicalAppointments,
      employees,
      //   employeesArmory,
      fields,
      breadcrumbItems,
      perPage,
      currentPage,
      filter,
      perPageSelect,
      weaponsFieldsBlank,
      medicalAppointmentFields,
      isloading,
      searchValue,
      searchField,
      // nomenclatureState,
      // brandState,
      // typeWeaponState,
      // gaugeState,
      // employeeState,

      onFiltered,
      refreshTable,
      RemoveMedicalAppointment
      // resetArmoryFields,
      // validateNomeclature,
      // validateBrand,
      // validateTypeWeapon,
      // validateGauge,
      // validateEmployee
    }
  }
}
</script>

<style scoped></style>
