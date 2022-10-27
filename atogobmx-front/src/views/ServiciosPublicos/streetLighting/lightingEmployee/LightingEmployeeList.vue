<template>
  <b-card class="m-2">
    <b-row align-h="end" class="mb-3 mr-1">
      <b-form-input
        size="lg"
        style="width: 350px"
        v-model="searchValue"
        type="search"
        placeholder="Buscar Alumbrado empleado..."
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
        <!-- v-b-modal.modal-cementery -->
        <i class="bi bi-person-plus-fill"></i>
        Agregar Alumbrado empleado
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
      :items="lightingEmployeeService"
      :rows-per-page="5"
      :search-field="searchField"
      :search-value="searchValue"
      :table-height="330"
    >
      <template #header-actions="header">
        {{ header.text }}
      </template>
      <template #item-actions="items">
        <b-button
          @click="RemoveLightingEmployeeService(items.alumbradoempleadoId)"
          class="m-1"
          variant="outline-danger"
          ><i class="bi bi-trash3"></i
        ></b-button>
        <b-button
          class="m-1"
          variant="outline-warning"
          :to="{
            name: 'AlumbradoEmpleado-Edit',
            params: { AlumbradoEmpleadoId: items.alumbradoempleadoId }
          }"
          ><i class="bi bi-pencil-square"></i
        ></b-button>
      </template>
    </EasyDataTable>
    <b-modal
      id="modal-lightingemployee"
      tittle="Agregar Alumbrado Empleado"
      v-model="showModal"
      size="xl"
      hide-footer
      button-size="lg"
      lazy
    >
      <Form @submit="addLightingEmployeeService">
        <b-row cols="2">
          <!-- 1 -->
          <b-col>
            <b-form-group class="mt-3" label="Nombre empleado">
              <Field
                name="NameField"
                :rules="validateName"
                as="text"
              >
                <b-form-select
                  v-model="lightingEmployeeServiceFields.empleadoId"
                  :state="NameState"
                  autofocus
                  :options="employees"
                  value-field="empleadoId"
                  text-field="nombreCompleto"
                >
                </b-form-select>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="NameField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!-- 2 -->
          <!-- <b-col>
            <b-form-group class="mt-3" label="Description">
              <Field name="DescriptionField" :rules="validateDescription" as="text">
                <b-form-input
                  v-model="zoneServiceFields.descripcion"
                  :state="DescriptionState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="DescriptionField"
              ></ErrorMessage>
            </b-form-group>
          </b-col> -->
        </b-row>

        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            @click="resetLightingEmployeeServiceFields"
          >
            <!-- v-b-modal.modal-cementery -->
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
import LightingEmployeeService from '@/Services/lightingemployee.Services'
import EmployeeServices from '@/Services/employee.Services'
import { Form, Field, ErrorMessage } from 'vee-validate'
import { ref, inject } from 'vue'
// import { useToast } from 'vue-toast-notification'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    EasyDataTable: window['vue3-easy-data-table'],
    Form,
    Field,
    ErrorMessage
  },
  setup () {
    const swal = inject('$swal')
    const showModal = ref(false)
    const { getAddressLightingEmployee, createAddressLightingEmployee, deleteAddressLightingEmployee } = LightingEmployeeService()
    const { getEmployees } = EmployeeServices()
    // const $toast = useToast()
    const lightingEmployeeService = ref([])
    const employees = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('empleadoId')
    const NameState = ref(false)
    const lightingEmployeeServiceFields = ref({
      alumbradoEmpleadoId: 0,
      empleadoId: null,
      archivado: false
    })

    getEmployees(data => {
      employees.value = data
      if (data.length === 0) {
        swal.fire({
          title: 'No se encuentra un tipo de Empleado alumbrado',
          text: 'No se encuentra tipo de empleado alumbrado registrado en el departamento seleccionado, registre primero un tipo de empleado para continuar',
          icon: 'warning'
        })
      }
    })

    const LightingEmployeeServiceFieldsBlank = ref(
      JSON.parse(JSON.stringify(lightingEmployeeServiceFields))
    )

    const fields = ref([
      { value: 'alumbradoEmpleadoId', text: 'ID', sortable: true },
      { value: 'empleadoId', text: 'Nombre empleado' },
      { value: 'actions', text: 'Acciones' }
    ])

    const resetLightingEmployeeServiceFields = () => {
      showModal.value = false
      lightingEmployeeServiceFields.value = JSON.parse(
        JSON.stringify(LightingEmployeeServiceFieldsBlank)
      )
      NameState.value = false
    }

    getAddressLightingEmployee(data => {
      lightingEmployeeService.value = data

      if (lightingEmployeeService.value.length > 0) {
        isloading.value = false
      } else {
        if (lightingEmployeeService.value.length <= 0) {
          isloading.value = false
        }
      }
    })

    const onFiltered = filteredItems => {
      currentPage.value = 1
    }

    const validateName = () => {
      if (!lightingEmployeeServiceFields.value.empleadoId) {
        NameState.value = false
        return 'Este campo es requerido'
      }

      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(lightingEmployeeServiceFields.value.empleadoId)) {
        NameState.value = false
        return 'Este campo solo puede contener letras'
      }

      if (!lightingEmployeeServiceFields.value.empleadoId.trim().length > 0) {
        NameState.value = false
        return 'Este campo no puede contener espacios'
      }

      NameState.value = true
      return true
    }

    // const validateDescription = () => {
    //   if (!zoneServiceFields.value.descripcion) {
    //     DescriptionState.value = false
    //     return 'Este campo es requerido'
    //   }

    //   if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(zoneServiceFields.value.descripcion)) {
    //     DescriptionState.value = false
    //     return 'Este campo solo puede contener numeros'
    //   }

    //   if (!zoneServiceFields.value.descripcion.trim().length > 0) {
    //     DescriptionState.value = false
    //     return 'Este campo no puede contener espacios'
    //   }

    //   DescriptionState.value = true
    //   return true
    // }

    const refreshTable = () => {
      isloading.value = true
      getAddressLightingEmployee(data => {
        lightingEmployeeService.value = data

        if (lightingEmployeeService.value.length > 0) {
          isloading.value = false
        } else {
          if (lightingEmployeeService.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }

    const addLightingEmployeeService = () => {
      createAddressLightingEmployee(lightingEmployeeServiceFields.value, data => {
        refreshTable()
        swal.fire({
          title: '¡Empleado alumbrado agregado correctamente!',
          text: 'Empleado alumbrado registrado satisfactoriamente',
          icon: 'success'
        })
      })
      showModal.value = false
      resetLightingEmployeeServiceFields()
    }

    const RemoveLightingEmployeeService = lightingemployeeId => {
      isloading.value = true
      swal.fire({
        title: '¿Estas seguro',
        text: 'No podras revertir esto',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, Archivar Empleado alumbrado!',
        cancelButtonText: 'Cancelar'
      }).then(result => {
        if (result.isConfirmed) {
          deleteAddressLightingEmployee(lightingemployeeId, (data) => {
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
      lightingEmployeeService,
      lightingEmployeeServiceFields,
      perPage,
      currentPage,
      filter,
      showModal,
      perPageSelect,
      isloading,
      searchValue,
      searchField,
      LightingEmployeeServiceFieldsBlank,
      fields,
      NameState,
      employees,

      onFiltered,
      addLightingEmployeeService,
      RemoveLightingEmployeeService,
      refreshTable,
      validateName,
      resetLightingEmployeeServiceFields
    }
  }
}
</script>

<style></style>
