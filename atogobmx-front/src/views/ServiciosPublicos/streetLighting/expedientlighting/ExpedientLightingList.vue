<template>
  <b-card class="m-2">
    <b-row align-h="end" class="mb-3 mr-1">
      <b-form-input
        size="lg"
        style="width: 350px"
        v-model="searchValue"
        type="search"
        placeholder="Buscar Expediente Alumbrado..."
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
        <!-- v-b-modal.modal-expedientlighting -->
        <i class="bi bi-person-plus-fill"></i>
        Agregar Expediente
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
      :items="expedientLighting"
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
          @click="RemoveExpedientLighting(items.expedienteAlumbradoId)"
          class="m-1"
          variant="outline-danger"
          ><i class="bi bi-trash3"></i
        ></b-button>
        <b-button
          class="m-1"
          variant="outline-warning"
          :to="{
            name: 'ExpedienteAlumbrado-Edit',
            params: { ExpedienteAlumbradoId: items.expedienteAlumbradoId }
          }"
          ><i class="bi bi-pencil-square"></i
        ></b-button>
      </template>
    </EasyDataTable>
    <b-modal
      id="modal-expedientlighting"
      title="Agregar Expediente Alumbrado"
      size="xl"
      hide-footer
      centered
      button-size="lg"
      lazy
    >
      <Form @submit="addExpedientLighting">
        <b-row cols="2">
          <!--1-->
          <b-col>
            <b-form-group class="mt-3" label="Lugar publico">
              <Field
                name="PublicPlaceField"
                :rules="validatePublicPlace"
                as="text"
              >
                <b-form-input
                  v-model="expedientLightingFields.lugarPublico"
                  :state="PublicPlaceState"
                ></b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="PublicPlaceField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--2-->
          <b-col>
            <b-form-group class="mt-3" label="Localidad">
              <Field name="LocationField" :rules="validateLocation" as="text">
                <b-form-input
                  v-model="expedientLightingFields.localidad"
                  :state="LocationState"
                ></b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="LocationField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--3-->
          <b-col>
            <b-form-group class="mt-3" label="Fecha Alta">
              <Field name="HighDateField" :rules="validateHighDate" as="">
                <Datepicker
                  locale="es"
                  name="date"
                  text-input
                  v-model="expedientLightingFields.fechaAlta"
                  :state="HighDateState"
                ></Datepicker>
              </Field>
              <ErrorMessage name="HighDateField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--4-->
          <b-col>
            <b-form-group class="mt-3" label="Departamento">
              <Field
                name="DepartamentField"
                :rules="validateDepartament"
                as="number"
              >
                <b-form-select
                  v-model="expedientLightingFields.departamentoId"
                  autofocus
                  :options="departaments"
                  value-field="departamentoId"
                  text-field="nombre"
                  :state="departamentState"
                  @input="getAreas(EmployeesFields.departamentoId)"
                >
                </b-form-select>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="DepartamentField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--5-->
          <b-col>
            <b-form-group class="mt-3" label="Area">
              <Field name="AreaField" :rules="validateArea" as="number">
                <b-form-select
                  v-model="expedientLightingFields.areaId"
                  autofocus
                  :options="areas"
                  value-field="areaId"
                  text-field="nombre"
                  :state="areaState"
                  @input="getWorkStation(EmployeesFields.areaId)"
                >
                </b-form-select>
              </Field>
              <ErrorMessage class="text-danger" name="AreaField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--6-->
          <b-col>
            <b-form-group class="mt-3" label="Descripcion Solucion">
              <Field
                name="DescriptionSolutionField"
                :rules="validateDescriptionSolution"
                as="text"
              >
                <b-form-textarea
                  v-model="expedientLightingFields.descripcionSolucion"
                  :state="DescriptionSolutionState"
                  rows="4"
                ></b-form-textarea>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="DescriptionSolutionField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            @click="resetexpedientLightingFields"
          >
            <!-- v-b-modal.modal-expedientlighting -->
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
import ExpedientlightingServices from '@/Services/expedientlighting.Services'
// import StatusLightingServices from '@/Services/statuslighting.Services'
import AreaServices from '@/Services/area.Services'
import DepartamentServices from '@/Services/departament.Services'
import Datepicker from '@vuepic/vue-datepicker'
import { Form, Field, ErrorMessage } from 'vee-validate'
import { ref, inject } from 'vue'
// import { useToast } from 'vue-toast-notification'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    Datepicker,
    EasyDataTable: window['vue3-easy-data-table'],
    Form,
    Field,
    ErrorMessage
  },
  setup () {
    const swal = inject('$swal')
    const showModal = ref(false)
    const { getAreasByDepartament } = AreaServices()
    const { getDepartaments } = DepartamentServices()
    const { getExpedientLighting, createExpedientLighting, deleteExpedientLighting } = ExpedientlightingServices()
    // const { getStatus } = StatusLightingServices()
    // const $toast = useToast()
    const expedientLighting = ref([])
    const departaments = ref([])
    const areas = ref([])
    // const statusPublicLighting = ref([])
    const isOpen = ref(false)
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('tarea')
    const PublicPlaceState = ref(false)
    const LocationState = ref(false)
    const HighDateState = ref(false)
    const departamentState = ref(false)
    const areaState = ref(false)
    const DescriptionSolutionState = ref(false)
    const expedientLightingFields = ref({
      expedienteAlumbradoId: 0,
      descripcionSolucion: null,
      fechaAlta: null,
      lugarPublico: null,
      localidad: null,
      departamentoId: null,
      areaId: null,
      archivado: false
    })

    // getStatus(data => {
    //   statusPublicLighting.value = data
    // })

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

    const getAreas = (departamentoId) => {
      getAreasByDepartament(departamentoId, data => {
        areas.value = data
        if (data.length === 0) {
          swal.fire({
            title: 'No se encuentran areas registradas!',
            text: 'No se encuentran areas registradas en el departamento seleccionado, registre primero una area para continuar.',
            icon: 'warning'
          })
        }
      })
    }

    const validatePublicPlace = () => {
      if (!expedientLightingFields.value.lugarPublico) {
        PublicPlaceState.value = false
        return 'Este campo es requerido'
      }
      PublicPlaceState.value = true
      return true
    }

    const validateLocation = () => {
      if (!expedientLightingFields.value.localidad) {
        LocationState.value = false
        return 'Este campo es requerido'
      }
      LocationState.value = true
      return true
    }

    const validateDepartament = () => {
      if (!expedientLightingFields.value.departamentoId) {
        departamentState.value = false
        return 'Este campo es requerido'
      }
      departamentState.value = true
      return true
    }

    const validateArea = () => {
      if (!expedientLightingFields.value.areaId) {
        areaState.value = false
        return 'Este campo es requerido'
      }
      areaState.value = true
      return true
    }

    const validateDescriptionSolution = () => {
      if (!expedientLightingFields.value.descripcionSolucion) {
        DescriptionSolutionState.value = false
        return 'Este campo es requerido'
      }
      DescriptionSolutionState.value = true
      return true
    }

    const validateHighDate = () => {
      if (!expedientLightingFields.value.fechaAlta) {
        HighDateState.value = false
        return 'Este campo es requerido'
      }
      HighDateState.value = true
      return true
    }

    // pone mis cambios de mis campos vacios de nuevo
    const expedientLightingFieldsBlank = ref(
      JSON.parse(JSON.stringify(expedientLightingFields))
    )

    const fields = ref([
      { value: 'expedienteAlumbradoId', text: 'ID', sortable: true },
      { value: 'lugarPublico', text: 'Tarea' },
      { value: 'localidad', text: 'Fecha Alta' },
      { value: 'fechaAlta', text: 'Domicilio' },
      { value: 'departamentoId', text: 'Descripcion Domicilio' },
      { value: 'areaId', text: 'Descripcion Solucion' },
      { value: 'actions', text: 'Acciones' }
    ])

    const resetexpedientLightingFields = () => {
      showModal.value = false
      expedientLightingFields.value = JSON.parse(
        JSON.stringify(expedientLightingFieldsBlank)
      )
      PublicPlaceState.value = false
      LocationState.value = false
      HighDateState.value = false
      departamentState.value = false
      areaState.value = false
      DescriptionSolutionState.value = false
    }

    getExpedientLighting(data => {
      expedientLighting.value = data
      // rows.value = data.length
      if (expedientLighting.value.length > 0) {
        isloading.value = false
      } else {
        if (expedientLighting.value.length <= 0) {
          isloading.value = false
        }
      }
    })

    const onFiltered = filteredItems => {
      // rows.value = filteredItems.length
      currentPage.value = 1
    }

    const refreshTable = () => {
      isloading.value = true
      getExpedientLighting(data => {
        expedientLighting.value = data
        // rows.value = data.length
        if (expedientLighting.value.length > 0) {
          isloading.value = false
        } else {
          if (expedientLighting.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }

    const addExpedientLighting = () => {
      createExpedientLighting(expedientLightingFields.value, data => {
        refreshTable()
        swal.fire({
          title: '¡Expediente publico agregado correctamente!',
          text: 'Expediente registrado satisfactoriamente',
          icon: 'success'
        })
        // $toast.success('Alumbrado registrado correctamente.', {
        //   position: 'top-right',
        //   duration: 1500
        // })
      })
      showModal.value = false
      resetexpedientLightingFields()
      // resetStreetLightingFields()
      // expedientLightingFields.value = JSON.parse(
      //   JSON.stringify(expedientLightingFieldsBlank)
      // )
    }

    const RemoveExpedientLighting = StreetLightingId => {
      isloading.value = true
      swal
        .fire({
          title: '¿Estas seguro',
          text: 'No podras revertir esto',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Si, Archivar Expediente!',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            deleteExpedientLighting(StreetLightingId, data => {
              refreshTable()
            })
            swal.fire({
              title: '¡Expediente archivado!',
              text: 'El expediente ha sido archivado satisfactoriamente.',
              icon: 'success'
            })
          } else {
            isloading.value = false
          }
        })
    }

    return {
      expedientLighting,
      // statusPublicLighting,
      expedientLightingFields,
      showModal,
      isOpen,
      perPage,
      currentPage,
      filter,
      perPageSelect,
      isloading,
      searchValue,
      searchField,
      expedientLightingFieldsBlank,
      fields,
      DescriptionSolutionState,
      HighDateState,
      PublicPlaceState,
      LocationState,
      departamentState,
      areaState,
      areas,

      onFiltered,
      getAreas,
      addExpedientLighting,
      refreshTable,
      RemoveExpedientLighting,
      validateDescriptionSolution,
      validateHighDate,
      resetexpedientLightingFields,
      validatePublicPlace,
      validateLocation,
      validateDepartament,
      validateArea
    }
  }
}
</script>

<style></style>
