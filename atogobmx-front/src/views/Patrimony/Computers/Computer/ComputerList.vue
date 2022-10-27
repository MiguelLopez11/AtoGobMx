<template>
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
        <i class="bi bi-person-video2"></i>
        Agregar Computadora
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
    >
      <template #header-actions="header">
        {{ header.text }}
      </template>
      <template #item-actions="items">
        <b-button
          @click="RemoveRole(items.equipoComputoId)"
          class="m-1"
          variant="outline-danger"
          ><i class="bi bi-trash3"></i
        ></b-button>
        <b-button class="m-1" variant="outline-warning"
          :to="{
            name: 'EquiposComputo-Edit',
            params: { EquipoComputoId: items.equipoComputoId},
          }"
        >
          <i class="bi bi-pencil-square" />
        </b-button>
      </template>
      <template #item-inventarioEstatus="items">
        <span class="bg-success text-white rounded">
        {{items.inventarioEstatus.nombre}}
        </span>
      </template>
    </EasyDataTable>
    <b-modal
      v-model="showModal"
      title="Agregar Departamento"
      size="xl"
      centered
      button-size="lg"
      hide-footer
    >
      <Form @submit="addComputer">
        <b-row cols="3">
          <b-col>
            <b-form-group class="mt-3" label="Marca">
              <Field name="BrandField" :rules="validateBrand" as="text">
                <b-form-input
                  placeholder="Ingresa la marca y/o modelo del equipo"
                  v-model="computerFields.marca"
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
                  v-model="computerFields.memoriaRAM"
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
                  v-model="computerFields.almacenamiento"
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
              <Field name="ProcessorField" :rules="validateProcessor" as="text">
                <b-form-input
                  placeholder="Ingresa los datos del procesador que tiene el equipo"
                  v-model="computerFields.procesador"
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
                  v-model="computerFields.departamentoId"
                  autofocus
                  :options="departaments"
                  value-field="departamentoId"
                  text-field="nombre"
                  :state="departamentState"
                  @input="getAreas(computerFields.departamentoId)"
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
            <b-form-group class="mt-3" label="Area">
              <Field name="AreaField" :rules="validateArea" as="number">
                <b-form-select
                  v-model="computerFields.areaId"
                  autofocus
                  :options="areas"
                  value-field="areaId"
                  text-field="nombre"
                  :state="areaState"
                >
                </b-form-select>
              </Field>
              <ErrorMessage class="text-danger" name="AreaField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Estatus">
              <Field name="StatusField" :rules="validateArea" as="number">
                <b-form-select
                  v-model="computerFields.estatusId"
                  autofocus
                  :options="statusComputers"
                  value-field="inventarioEstatusId"
                  text-field="nombre"
                  :state="areaState"
                >
                </b-form-select>
              </Field>
              <ErrorMessage class="text-danger" name="StatusField"></ErrorMessage>
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
import AreaServices from '@/Services/area.Services'
import DepartamentServices from '@/Services/departament.Services'
import { Form, Field, ErrorMessage } from 'vee-validate'
import { ref, inject } from 'vue'
// import { useToast } from 'vue-toast-notification'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    Form,
    Field,
    ErrorMessage,
    EasyDataTable: window['vue3-easy-data-table']
  },
  setup () {
    const swal = inject('$swal')
    const { getComputers, createComputer, deleteComputer, getStatus } = ComputerServices()
    const { getAreasByDepartament } = AreaServices()
    const { getDepartaments } = DepartamentServices()
    // const $toast = useToast()
    const Computers = ref([])
    const areas = ref([])
    const departaments = ref([])
    const statusComputers = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('area.nombre')
    const brandState = ref(false)
    const memoryState = ref(false)
    const storageState = ref(false)
    const processorState = ref(false)
    const departamentState = ref(false)
    const areaState = ref(false)
    const showModal = ref(false)
    const computerFields = ref({
      equipoComputoId: 0,
      marca: '',
      memoriaRAM: '',
      almacenamiento: '',
      procesador: '',
      departamentoId: null,
      areaId: null,
      estatusId: null,
      archivado: false
    })
    const computerFieldsBlank = ref(JSON.parse(JSON.stringify(computerFields)))
    const fields = ref([
      { value: 'equipoComputoId', text: 'ID', sortable: true },
      { value: 'marca', text: 'Marca' },
      { value: 'memoriaRAM', text: 'Memoria RAM' },
      { value: 'almacenamiento', text: 'Almacenamiento' },
      { value: 'procesador', text: 'Procesador' },
      { value: 'area.nombre', text: 'Area' },
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
          text:
            'No se encuentran departamentos registrados en el sistema, registre primero un departamento para continuar.',
          icon: 'warning'
        })
      }
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
    const getAreas = departamentoId => {
      getAreasByDepartament(departamentoId, data => {
        areas.value = data
        if (data.length === 0) {
          swal.fire({
            title: 'No se encuentran areas registradas!',
            text:
              'No se encuentran areas registradas en el departamento seleccionado, registre primero una area para continuar.',
            icon: 'warning'
          })
        }
      })
    }
    const validateBrand = () => {
      if (!computerFields.value.marca) {
        brandState.value = false
        return 'Este campo es requerido'
      }
      // eslint-disable-next-line no-useless-escape
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(computerFields.value.marca)) {
        brandState.value = false
        return 'El campo no puede contener solo espacios'
      }
      brandState.value = true
      return true
    }
    const validateMemory = () => {
      if (!computerFields.value.memoriaRAM) {
        memoryState.value = false
        return 'Este campo es requerido'
      }
      // eslint-disable-next-line no-useless-escape
      if (!computerFields.value.memoriaRAM.trim() > 0) {
        processorState.value = false
        return 'El campo no puede contener solo espacios'
      }
      memoryState.value = true
      return true
    }
    const validateStorage = () => {
      if (!computerFields.value.almacenamiento) {
        storageState.value = false
        return 'Este campo es requerido'
      }
      // eslint-disable-next-line no-useless-escape
      if (!computerFields.value.almacenamiento.trim() > 0) {
        processorState.value = false
        return 'El campo no puede contener solo espacios'
      }
      storageState.value = true
      return true
    }
    const validateProcessor = () => {
      if (!computerFields.value.procesador) {
        processorState.value = false
        return 'Este campo es requerido'
      }
      if (!computerFields.value.procesador.trim() > 0) {
        processorState.value = false
        return 'El campo no puede contener solo espacios'
      }
      processorState.value = true
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
    const validateArea = () => {
      if (!computerFields.value.areaId) {
        areaState.value = false
        return 'Este campo es requerido'
      }
      areaState.value = true
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
      createComputer(computerFields.value, data => {
        refreshTable()
        swal.fire({
          title: 'Equipo registrado correctamente!',
          text:
            'El equipo de computo se ha registrado al sistema satisfactoriamente.',
          icon: 'success'
        })
      })
      resetRoleFields()
    }
    const resetRoleFields = () => {
      showModal.value = false
      computerFields.value = JSON.parse(JSON.stringify(computerFieldsBlank))
      brandState.value = false
    }
    const RemoveRole = equipoComputoId => {
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
                text:
                  'El Equipo de computo ha sido archivado satisfactoriamente .',
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
      RemoveRole,
      brandState,
      memoryState,
      storageState,
      processorState,
      departamentState,
      areaState,
      showModal,
      areas,
      departaments,
      statusComputers,

      validateBrand,
      validateMemory,
      validateStorage,
      validateProcessor,
      resetRoleFields,
      validateDepartament,
      validateArea,
      getAreas
    }
  }
}
</script>

<style></style>
