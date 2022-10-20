<template>
  <b-card class="m-2">
    <b-row align-h="end" class="mb-3 mr-1">
      <b-form-input
        size="lg"
        style="width: 350px"
        v-model="searchValue"
        type="search"
        placeholder="Buscar Puesto de trabajo..."
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
        <i class="bi bi-pc-display-horizontal m-1" />
        Agregar puesto trabajo
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
      :items="workStations"
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
          @click="RemoveWorkStation(items.puestoTrabajoId)"
          class="m-1"
          variant="outline-danger"
          ><i class="bi bi-trash3"></i
        ></b-button>
        <b-button
          class="m-1"
          variant="outline-warning"
          :to="{
            name: 'PuestoTrabajo-Edit',
            params: { puestoTrabajoId: items.puestoTrabajoId }
          }"
        >
          <i class="bi bi-pencil-square" />
        </b-button>
      </template>
    </EasyDataTable>
    <b-modal
      v-model="showModal"
      title="Agregar puesto trabajo"
      size="xl"
      centered
      button-size="lg"
      hide-footer
    >
      <Form @submit="addWorkStation">
        <b-row cols="3">
          <b-col>
            <b-form-group class="mt-3" label="Nombre">
              <Field name="NameField" :rules="validateName" as="text">
                <b-form-input
                  v-model="workStationFields.nombre"
                  :state="nameState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage class="text-danger" name="NameField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Departamento">
              <Field
                name="DepartamentField"
                :rules="validateDepartament"
                as="number"
              >
                <b-form-select
                  v-model="workStationFields.departamentoId"
                  autofocus
                  :options="departaments"
                  value-field="departamentoId"
                  text-field="nombre"
                  :state="departamentState"
                  @input="getAreas(workStationFields.departamentoId)"
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
                  v-model="workStationFields.areaId"
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
        </b-row>
        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            v-b-modal.modal-puestoTrabajo
            @click="resetWorkStationFields"
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
import workStationServices from '@/Services/workStation.Services'
import AreasServices from '@/Services/area.Services'
import DepartamentServices from '@/Services/departament.Services'
import { Form, Field, ErrorMessage } from 'vee-validate'
import { ref, inject } from 'vue'
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
    const {
      getWorkStations,
      createWorkStation,
      deleteWorkStation
    } = workStationServices()
    const { getAreasByDepartament } = AreasServices()
    const { getDepartaments } = DepartamentServices()
    const showModal = ref(false)
    const workStations = ref([])
    const areas = ref([])
    const departaments = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    const rows = ref(null)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('nombre')
    const nameState = ref(false)
    const areaState = ref(false)
    const departamentState = ref(false)
    const workStationFields = ref({
      puestoTrabajoId: 0,
      nombre: '',
      departamentoId: 0,
      areaId: 0,
      archivado: false
    })
    const workStationFieldsBlank = ref(
      JSON.parse(JSON.stringify(workStationFields))
    )
    const fields = ref([
      { value: 'puestoTrabajoId', text: 'ID', sortable: true },
      { value: 'nombre', text: 'Nombre' },
      { value: 'departamentos.nombre', text: 'Departamento' },
      { value: 'area.nombre', text: 'Area' },
      { value: 'actions', text: 'Acciones' }
    ])
    getWorkStations(data => {
      workStations.value = data
      if (workStations.value.length > 0) {
        isloading.value = false
      } else {
        if (workStations.value.length <= 0) {
          isloading.value = false
        }
      }
    })
    getDepartaments(data => {
      departaments.value = data
      if (data.length === 0) {
        swal.fire({
          title: 'No se encuentran departamentos registrados!',
          text:
            'No se encuentran departamentos registrados en el sistema, registre primero un departamento para continuar.',
          icon: 'error'
        })
      }
    })
    const getAreas = departamentoId => {
      getAreasByDepartament(departamentoId, data => {
        areas.value = data
        if (data.length === 0) {
          if (data.length === 0) {
            swal.fire({
              title: 'No se encuentran areas registradas!',
              text:
                'No se encuentran areas registradas en el departamento seleccionado, registre primero una area para continuar.',
              icon: 'error'
            })
          }
        }
      })
    }
    const onFiltered = filteredItems => {
      rows.value = filteredItems.length
      currentPage.value = 1
    }
    const validateName = () => {
      if (!workStationFields.value.nombre) {
        nameState.value = false
        return 'Este campo es requerido'
      }
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(workStationFields.value.nombre)) {
        nameState.value = false
        return 'El nombre solo puede contener letras'
      }
      nameState.value = true
      return true
    }
    const validateDepartament = () => {
      if (!workStationFields.value.departamentoId) {
        departamentState.value = false
        return 'Este campo es requerido'
      }
      departamentState.value = true
      return true
    }
    const validateArea = () => {
      if (!workStationFields.value.areaId) {
        areaState.value = false
        return 'Este campo es requerido'
      }
      areaState.value = true
      return true
    }
    const refreshTable = () => {
      isloading.value = true
      showModal.value = false
      getWorkStations(data => {
        workStations.value = data
        if (workStations.value.length > 0) {
          isloading.value = false
        } else {
          if (workStations.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }
    const addWorkStation = () => {
      createWorkStation(workStationFields.value, data => {
        swal.fire({
          title: '¡Puesto de trabajo registrado correctamente!',
          text:
            'El puesto de trabajo se ha registrado al sistema satisfactoriamente.',
          icon: 'success'
        })
        refreshTable()
        resetWorkStationFields()
      })
    }
    const resetWorkStationFields = () => {
      showModal.value = false
      workStationFields.value = JSON.parse(
        JSON.stringify(workStationFieldsBlank)
      )
      nameState.value = false
      areaState.value = false
      departamentState.value = false
    }
    const RemoveWorkStation = puestoTrabajoId => {
      isloading.value = true
      swal
        .fire({
          title: '¿Estas seguro?',
          text: 'No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Si, Archivar Puesto de trabajo!',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            swal
              .fire({
                title: 'Puesto de trabajo archivado!',
                text: 'El Puesto de trabajo ha sido archivado satisfactoriamente .',
                icon: 'success'
              })
              .then(result => {
                if (result.isConfirmed) {
                  deleteWorkStation(puestoTrabajoId, data => {
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
      workStations,
      departaments,
      showModal,
      fields,
      perPage,
      currentPage,
      rows,
      areas,
      filter,
      perPageSelect,
      workStationFieldsBlank,
      workStationFields,
      isloading,
      searchValue,
      searchField,
      onFiltered,
      addWorkStation,
      refreshTable,
      RemoveWorkStation,
      nameState,
      areaState,
      departamentState,

      validateName,
      validateArea,
      validateDepartament,
      resetWorkStationFields,
      getAreas
    }
  }
}
</script>

<style></style>
