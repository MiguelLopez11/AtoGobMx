<template>
  <b-card class="m-2">
    <b-row align-h="end" class="mb-3 mr-1">
      <b-form-input
        size="lg"
        style="width: 350px"
        v-model="searchValue"
        type="search"
        placeholder="Buscar empleado alumbrado..."
      ></b-form-input>
      <b-button
        variant="primary"
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
        <i class="bi bi-person-plus-fill"></i>
        Agregar empleado OP
      </b-button>
    </b-row>
    <EasyDataTable
      rows-per-page-message="registros por pagina"
      empty-message="No se encontro ningun registro"
      table-class-name="customize-table"
      buttons-pagination
      border-cell
      :loading="isloading"
      :headers="fields"
      :items="publicWorksEmployeeService"
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
            @click="RemovePublickWorksEmployeeService(items.empleadoObrasId)"
            class="m-1"
            variant="outline-danger"
            ><i class="bi bi-trash3"> Archivar</i></b-dropdown-item
          >
        </b-dropdown>
      </template>
    </EasyDataTable>
    <b-modal
      id="modal-lightingemployee"
      tittle="Agregar empleado OP"
      v-model="showModal"
      size="xl"
      hide-footer
      button-size="lg"
      lazy
    >
      <Form @submit="addPublickWorksEmployeeService">
        <b-row cols="2">
          <!-- Nombre del empleado -->
          <b-col>
            <b-form-group class="mt-3" label="Nombre empleado">
              <Field name="NameField" :rules="validateName" as="text">
                <v-select
                  multiple
                  v-model="employeesSelected"
                  :options="employees"
                  label="nombreCompleto"
                  :key="employees.empleadoId"
                  :reduce="employees => employees.empleadoId"
                />
              </Field>
              <ErrorMessage class="text-danger" name="NameField"></ErrorMessage>
            </b-form-group>
          </b-col>
        </b-row>

        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            @click="resetPublicWorksEmployeeServiceFields"
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
import PublickWorksEmployeeService from '@/Services/publicworksemployee.Services'
import EmployeeServices from '@/Services/employee.Services'
import { Form, Field, ErrorMessage } from 'vee-validate'
import { ref, inject } from 'vue'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    EasyDataTable: window['vue3-easy-data-table'],
    Form,
    Field,
    ErrorMessage
  },
  props: {
    ObraId: {
      type: Number,
      required: true
    }
  },
  setup (props) {
    const swal = inject('$swal')
    const showModal = ref(false)
    const {
      getPublickWorksEmployee,
      createPublickWorksEmployee,
      deletePublickWorksEmployee
    } = PublickWorksEmployeeService()
    const { getEmployees } = EmployeeServices()
    const publicWorksEmployeeService = ref([])
    const employees = ref([])
    const employeesSelected = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('nombreCompleto')
    const NameState = ref(false)
    const publicWorksEmployeeFields = ref({
      empleadoObrasId: 0,
      ObraId: props.ObraId,
      empleadoId: 0,
      // obraId: 0,
      archivado: false
    })

    getEmployees(data => {
      employees.value = data
      if (data.length === 0) {
        swal.fire({
          title: 'No se encuentra un tipo de Empleado alumbrado',
          text:
            'No se encuentra tipo de empleado alumbrado registrado en el departamento seleccionado, registre primero un tipo de empleado para continuar',
          icon: 'warning'
        })
      }
    })

    const PublicWorksEmployeeServiceFieldsBlank = ref(
      JSON.parse(JSON.stringify(publicWorksEmployeeFields))
    )

    const fields = ref([
      { value: 'empleadoObrasId', text: 'ID', sortable: true },
      { value: 'empleados.nombreCompleto', text: 'Nombre empleado' },
      { value: 'actions', text: 'Acciones' }
    ])

    const resetPublicWorksEmployeeServiceFields = () => {
      showModal.value = false
      publicWorksEmployeeFields.value = JSON.parse(
        JSON.stringify(PublicWorksEmployeeServiceFieldsBlank)
      )
      NameState.value = false
    }

    getPublickWorksEmployee(data => {
      publicWorksEmployeeService.value = data

      if (publicWorksEmployeeService.value.length > 0) {
        isloading.value = false
      } else {
        if (publicWorksEmployeeService.value.length <= 0) {
          isloading.value = false
        }
      }
    })

    const onFiltered = filteredItems => {
      currentPage.value = 1
    }

    const validateName = () => {
      if (!employeesSelected.value.length === 0) {
        NameState.value = false
        return 'Este campo es requerido'
      }

      NameState.value = true
      return true
    }

    const refreshTable = () => {
      isloading.value = true
      getPublickWorksEmployee(data => {
        publicWorksEmployeeService.value = data

        if (publicWorksEmployeeService.value.length > 0) {
          isloading.value = false
        } else {
          if (publicWorksEmployeeService.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }

    const addPublickWorksEmployeeService = () => {
      for (let i = 0; i < employeesSelected.value.length; i++) {
        publicWorksEmployeeFields.value.empleadoId = employeesSelected.value[i]
        createPublickWorksEmployee(
          publicWorksEmployeeFields.value,
          data => {
            refreshTable()
          })
      }
      swal.fire({
        title: '¡Empleado alumbrado agregado correctamente!',
        text: 'Empleado alumbrado registrado satisfactoriamente',
        icon: 'success'
      })
      showModal.value = false
      resetPublicWorksEmployeeServiceFields()
    }

    const RemovePublickWorksEmployeeService = empleadoObrasId => {
      isloading.value = true
      swal
        .fire({
          title: '¿Estas seguro',
          text: 'No podras revertir esto',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Si, Archivar Empleado alumbrado!',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            deletePublickWorksEmployee(empleadoObrasId, data => {
              refreshTable()
            })
            swal.fire({
              title: '¡Empleado alumbrado archivado!',
              text:
                'El empleado alumbrado ha sido archivado satisfactoriamente.',
              icon: 'success'
            })
          } else {
            isloading.value = false
          }
        })
    }

    return {
      publicWorksEmployeeService,
      publicWorksEmployeeFields,
      perPage,
      currentPage,
      filter,
      showModal,
      perPageSelect,
      isloading,
      searchValue,
      searchField,
      PublicWorksEmployeeServiceFieldsBlank,
      fields,
      NameState,
      employees,
      employeesSelected,

      onFiltered,
      addPublickWorksEmployeeService,
      RemovePublickWorksEmployeeService,
      refreshTable,
      validateName,
      resetPublicWorksEmployeeServiceFields
    }
  }
}
</script>

<style></style>
