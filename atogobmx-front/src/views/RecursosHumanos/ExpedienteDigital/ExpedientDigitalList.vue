<template>
  <b-card class="m-2">
    <b-row align-h="end" class="mb-3 mr-1">
      <b-form-input
        size="lg"
        style="width: 350px"
        v-model="searchValue"
        type="search"
        placeholder="Buscar Expediente..."
      >
      </b-form-input>
      <b-button
        :disabled="employees.length < 1"
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
        <i class="bi bi-folder-fill m-1" />
        Crear Expediente
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
      :items="expedients"
      :rows-per-page="5"
      :search-field="searchField"
      :search-value="searchValue"
      theme-color="#7367f0"
    >
      <template #header-actions="header">
        {{ header.text }}
      </template>
      <template #item-status="items">
        <b-badge :variant="items.archivado === false ? 'success': 'danger'">
        {{items.archivado === false ? 'Activo' : 'Archivado'}}
        </b-badge>
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
            @click="RemoveExpedient(items.expedienteDigitalId)"
            class="m-1"
            variant="outline-danger"
            ><i class="bi bi-trash3"> Archivar</i></b-dropdown-item
          >
          <b-dropdown-item
            class="m-1"
            variant="outline-warning"
            :to="{
              name: 'ExpedienteDigital-edit',
              params: { ExpedienteDigitalId: items.expedienteDigitalId }
            }"
          >
            <i class="bi bi-pencil-square" />
              Editar
            </b-dropdown-item>
        </b-dropdown>
      </template>
    </EasyDataTable>
  </b-card>
  <b-modal
    v-model="showModal"
    title="Expediente Digital"
    size="xl"
    centered
    hide-footer
  >
    <b-row>
      <b-form-group class="mt-3" label="Empleado:">
        <b-form-select
          autofocus
          :options="employees"
          value-field="empleadoId"
          text-field="nombreCompleto"
          v-model="expedientFields.empleadoId"
        />
      </b-form-group>
    </b-row>
    <b-row align-h="end">
      <b-button
        class="w-auto m-2 text-white"
        variant="primary"
        @click="showModal = !showModal"
      >
        Cancelar
      </b-button>
      <b-button class="w-auto m-2" variant="success" @click="onAddExpedient()">
        Guardar
      </b-button>
    </b-row>
  </b-modal>
</template>

<script>
import { ref, inject } from 'vue'
import ExpedientDigitalServices from '@/Services/expedientdigital.Services'
import EmployeeServices from '@/Services/employee.Services'
export default {
  components: {
    EasyDataTable: window['vue3-easy-data-table']
  },
  setup () {
    const showModal = ref(false)
    const swal = inject('$swal')
    const { getExpedients, deleteExpedient, createExpedient } =
      ExpedientDigitalServices()
    const { getEmployeesWithoutExpedient } = EmployeeServices()
    const expedients = ref([])
    const employees = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('empleados.nombreCompleto')
    const expedientFields = ref({
      expedienteDigitalId: 0,
      empleadoId: null,
      archivado: false
    })
    const expedientFieldsBlank = ref(
      JSON.parse(JSON.stringify(expedientFields))
    )
    const fields = ref([
      { value: 'expedienteDigitalId', text: 'No.Expediente', sortable: true },
      { value: 'empleados.nombreCompleto', text: 'Empleado', sortable: true },
      { value: 'status', text: 'Estado' },
      { value: 'actions', text: 'Acciones' }
    ])
    const getEmployees = () => {
      getEmployeesWithoutExpedient(data => {
        employees.value = data
      })
      return ''
    }
    getExpedients(data => {
      expedients.value = data
      if (expedients.value.length > 0) {
        isloading.value = false
      } else {
        if (expedients.value.length <= 0) {
          isloading.value = false
        }
      }
      getEmployees()
    })
    const refreshTable = () => {
      isloading.value = true
      getExpedients(data => {
        expedients.value = data
        if (expedients.value.length > 0) {
          isloading.value = false
        } else {
          if (expedients.value.length <= 0) {
            isloading.value = false
          }
        }
      })
    }
    const onFiltered = filteredItems => {
      currentPage.value = 1
    }
    const onAddExpedient = () => {
      createExpedient(expedientFields.value, data => {
        showModal.value = false
        expedientFields.value = JSON.parse(JSON.stringify(expedientFieldsBlank))
        refreshTable()
        getEmployees()
        swal.fire({
          title: '¡Expediente Registrado!',
          text: 'El expediente ha sido registrado al sistema satisfactoriamente .',
          icon: 'success'
        })
      })
    }
    const RemoveExpedient = expedienteDigitalId => {
      isloading.value = true
      swal
        .fire({
          title: '¿Estas seguro?',
          text: 'No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Si, Archivar Expediente!',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            deleteExpedient(expedienteDigitalId, data => {
              refreshTable()
              getEmployees()
            })
            swal.fire({
              title: '¡Empleado archivado!',
              text: 'El empleado ha sido archivado satisfactoriamente .',
              icon: 'success'
            })
          } else {
            isloading.value = false
          }
        })
    }
    return {
      fields,
      perPage,
      currentPage,
      filter,
      perPageSelect,
      isloading,
      searchValue,
      searchField,
      expedients,
      employees,
      showModal,
      expedientFields,

      onFiltered,
      RemoveExpedient,
      refreshTable,
      onAddExpedient
    }
  }
}
</script>

<style></style>
