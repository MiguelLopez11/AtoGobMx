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
        placeholder="Buscar Equipo..."
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
        <i class="bi bi-pc-display-horizontal"></i>
        Agregar equipo
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
      :items="Computers"
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
            @click="RemoveComputer(items.equipoComputoId)"
            class="m-1"
            variant="outline-danger"
          >
            <i class="bi bi-trash3" />
            Archivar
          </b-dropdown-item>
          <b-dropdown-item
            class="m-1"
            variant="outline-warning"
            :to="{
              name: 'EquiposComputo-Edit',
              params: { EquipoComputoId: items.equipoComputoId }
            }"
            ><i class="bi bi-pencil-square" /> Editar</b-dropdown-item
          >
        </b-dropdown>
      </template>
      <template #item-inventarioEstatus="items">
        <b-badge
          :variant="
            items.estatusEquipo.nombre === 'En Funcionamiento'
              ? 'success'
              : '' || items.estatusEquipo.nombre === 'En Mantenimiento'
              ? 'warning'
              : '' || items.estatusEquipo.nombre === 'Dado de baja'
              ? 'danger'
              : ''
          "
        >
          {{ items.estatusEquipo.nombre }}
        </b-badge>
      </template>
    </EasyDataTable>
    <b-modal
      v-model="showModal"
      title="Agregar Equipo"
      size="xl"
      centered
      button-size="lg"
      hide-footer
    >
      <Form @submit="addComputer">
        <b-row cols="3">
          <b-col>
            <b-form-group class="mt-3" label="Nomenclatura">
              <Field name="FolioField" :rules="validateFolio" as="text">
                <b-form-input
                  v-model="computerFields.codigoInventario"
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
              <Field name="DescriptionField" :rules="validateDescription" as="text">
                <b-form-textarea
                  placeholder="Ingresa las Caracteristicas del equipo"
                  v-model="computerFields.caracteristicas"
                  :state="DescriptionState"
                  rows="4"
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
            <b-form-group class="mt-3" label="Fecha de Adquisición">
              <Field name="memoryField" :rules="validateDate" as="text">
                <Datepicker
                  v-model="computerFields.fechaAdquisicion"
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
            <b-form-group class="mt-3" label="Costo de adquisición">
              <Field name="CostField" :rules="validateCost" as="text">
                <b-form-input
                  placeholder="Describe brevemente el almacenamiento que tiene el equipo"
                  v-model="computerFields.costo"
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
              <Field name="ProcessorField" :rules="validateSerialNumber" as="text">
                <b-form-input
                  placeholder="Ingresa el numero de serie del equipo"
                  v-model="computerFields.numeroSerie"
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
                  v-model="computerFields.departamentoId"
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
                  v-model="computerFields.estatusEquipoId"
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
            @click="resetRoleFields()"
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
import ComputerServices from '@/Services/computer.Services'
import DepartamentServices from '@/Services/departament.Services'
import { Form, Field, ErrorMessage } from 'vee-validate'
import { ref, inject } from 'vue'
import Datepicker from '@vuepic/vue-datepicker'
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
    const { getComputers, createComputer, deleteComputer, getStatus } =
      ComputerServices()
    const { getDepartaments } = DepartamentServices()
    // const $toast = useToast()
    const Computers = ref([])
    const departaments = ref([])
    const statusComputers = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('codigoInventario')
    const folioState = ref(false)
    const DescriptionState = ref(false)
    const dateState = ref(false)
    const costState = ref(false)
    const serialNumberState = ref(false)
    const departamentState = ref(false)
    const stateComputerState = ref(false)
    const showModal = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      {
        text: 'Patrimonio publico',
        to: '/PatrimonioMunicipal'
      },
      { text: 'Equipos de computo' }
    ])
    const computerFields = ref({
      equipoComputoId: 0,
      codigoInventario: null,
      caracteristicas: null,
      fechaAdquisicion: null,
      costo: null,
      numeroSerie: null,
      departamentoId: null,
      estatusEquipoId: null,
      archivado: false
    })
    const computerFieldsBlank = ref(JSON.parse(JSON.stringify(computerFields)))
    const fields = ref([
      { value: 'codigoInventario', text: 'Nomenclatura', sortable: true },
      { value: 'caracteristicas', text: 'Descripción/Caracteristicas' },
      { value: 'fechaAdquisicion', text: 'Fecha de Adquisición' },
      { value: 'costo', text: 'Costo' },
      { value: 'numeroSerie', text: 'Numero de Serie' },
      { value: 'inventarioEstatus', text: 'Estatus' },
      { value: 'actions', text: 'Acciones' }
    ])
    getComputers(data => {
      Computers.value = data
      if (Computers.value.length > 0) {
        isloading.value = false
      } else {
        if (Computers.value.length <= 0) {
          isloading.value = false
        }
      }
    })
    const onFiltered = filteredItems => {
      currentPage.value = 1
    }
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
    const validateFolio = () => {
      if (!computerFields.value.codigoInventario) {
        folioState.value = false
        return 'Este campo es requerido'
      }
      // eslint-disable-next-line no-useless-escape
      if (
        !/^(?=.*\d)(?=.*[a-zA-Z])([A-ZñÑáéíóúÁÉÍÓÚ])[A-Z0-9]+$/i.test(
          computerFields.value.codigoInventario
        )
      ) {
        folioState.value = false
        return 'El nombre del area solo puede contener letras'
      }
      folioState.value = true
      return true
    }
    const validateDescription = () => {
      if (!computerFields.value.caracteristicas) {
        DescriptionState.value = false
        return 'Este campo es requerido'
      }
      DescriptionState.value = true
      return true
    }
    const validateDate = () => {
      if (!computerFields.value.fechaAdquisicion) {
        dateState.value = false
        return 'Este campo es requerido'
      }
      dateState.value = true
      return true
    }
    const validateCost = () => {
      if (!computerFields.value.costo) {
        costState.value = false
        return 'Este campo es requerido'
      }
      costState.value = true
      return true
    }
    const validateSerialNumber = () => {
      if (!computerFields.value.numeroSerie) {
        serialNumberState.value = false
        return 'Este campo es requerido'
      }
      if (!computerFields.value.numeroSerie.trim() > 0) {
        serialNumberState.value = false
        return 'El campo no puede contener solo espacios'
      }
      serialNumberState.value = true
      return true
    }
    const validateDepartament = () => {
      if (!computerFields.value.departamentoId) {
        departamentState.value = false
        return 'Este campo es requerido'
      }
      departamentState.value = true
      return true
    }
    const validateStateComputer = () => {
      if (!computerFields.value.estatusEquipoId) {
        stateComputerState.value = false
        return 'Este campo es requerido'
      }
      stateComputerState.value = true
      return true
    }
    const refreshTable = () => {
      isloading.value = true
      getComputers(data => {
        Computers.value = data
        if (Computers.value.length > 0) {
          isloading.value = false
        } else {
          if (Computers.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }
    const addComputer = () => {
      createComputer(computerFields.value, data => {})
      swal.fire({
        title: 'Equipo registrado correctamente!',
        text: 'El equipo de computo se ha registrado al sistema satisfactoriamente.',
        icon: 'success'
      })
      refreshTable()
      resetRoleFields()
    }
    const resetRoleFields = () => {
      showModal.value = false
      computerFields.value = JSON.parse(JSON.stringify(computerFieldsBlank))
    }
    const RemoveComputer = equipoComputoId => {
      isloading.value = true
      swal
        .fire({
          title: '¿Estas seguro?',
          text: 'No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Si, archivar Equipo!',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            swal
              .fire({
                title: 'Equipo archivado!',
                text: 'El Equipo de computo ha sido archivado satisfactoriamente .',
                icon: 'success'
              })
              .then(result => {
                if (result.isConfirmed) {
                  deleteComputer(equipoComputoId, data => {
                    refreshTable()
                  })
                }
              })
          } else {
            isloading.value = false
          }
        })
    }
    return {
      Computers,
      fields,
      breadcrumbItems,
      perPage,
      currentPage,
      filter,
      perPageSelect,
      computerFieldsBlank,
      computerFields,
      isloading,
      searchValue,
      searchField,
      onFiltered,
      addComputer,
      refreshTable,
      RemoveComputer,
      costState,
      dateState,
      DescriptionState,
      serialNumberState,
      departamentState,
      stateComputerState,
      showModal,
      departaments,
      statusComputers,
      folioState,

      validateDescription,
      validateDate,
      validateSerialNumber,
      validateCost,
      validateDepartament,
      resetRoleFields,
      validateStateComputer,
      validateFolio
    }
  }
}
</script>

<style></style>
