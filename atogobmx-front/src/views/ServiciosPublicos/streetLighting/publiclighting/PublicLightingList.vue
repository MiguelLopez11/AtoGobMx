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
        placeholder="Buscar Alumbrado..."
      ></b-form-input>
      <b-button
        variant="success"
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
        <!-- v-b-modal.modal-publiclighting -->
        <i class="bi bi-person-plus-fill"></i>
        Agregar Alumbrado
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
      :items="publicLighting"
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
          :disabled="
            (items.archivado && role !== 'Administrador') || 'Director'
          "
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
            @click="RemovePublicLighting(items.alumbradoId)"
            class="m-1"
            variant="outline-danger"
            ><i class="bi bi-trash3"> Archivar</i></b-dropdown-item
          >
          <b-dropdown-item
            class="m-1"
            variant="outline-warning"
            :to="{
              name: 'Alumbrado-Edit',
              params: { AlumbradoId: items.alumbradoId }
            }"
          >
            <i class="bi bi-pencil-square" />
            Editar
          </b-dropdown-item>
          <b-dropdown-item
            class="m-1"
            variant="outline-warning"
            @click="onClickExpedient(items.alumbradoId)"
          >
            <i class="bi bi-archive-fill" />
            Expediente Digital
          </b-dropdown-item>
        </b-dropdown>
        <!-- <b-button
          @click="RemovePublicLighting(items.alumbradoId)"
          class="m-1"
          variant="outline-danger"
          ><i class="bi bi-trash3"></i
        ></b-button>
        <b-button
          class="m-1"
          variant="outline-warning"
          :to="{
            name: 'Alumbrado-Edit',
            params: { AlumbradoId: items.alumbradoId }
          }"
          ><i class="bi bi-pencil-square"></i
        ></b-button> -->
      </template>
    </EasyDataTable>
    <b-modal
      id="modal-publiclighting"
      title="Agregar Alumbrado"
      v-model="showModal"
      size="xl"
      hide-footer
      centered
      button-size="lg"
      lazy
    >
      <Form @submit="addPublicLighting">
        <b-row cols="2">
          <!--Tipo de tarea a agregar-->
          <b-col>
            <b-form-group class="mt-3" label="Tipo de tarea">
              <Field name="TaskField" :rules="validateTask" as="text">
                <b-form-select
                  v-model="publicLightingFields.tareaTipoId"
                  autofocus
                  :state="TaskState"
                  :options="typeTaskLighting"
                  value-field="tareaTipoId"
                  text-field="nombreTarea"
                ></b-form-select>
              </Field>
              <ErrorMessage class="text-danger" name="TaskField" />
            </b-form-group>
          </b-col>
          <!--agregar un estatus-->
          <b-col>
            <b-form-group class="mt-3" label="Estatus">
              <Field name="StatusField" :rules="validateStatus" as="text">
                <b-form-select
                  v-model="publicLightingFields.estatusId"
                  autofocus
                  :state="StatusState"
                  :options="statusPublicLighting"
                  value-field="estatusAlumbradoId"
                  text-field="nombreEstatus"
                ></b-form-select>
              </Field>
              <ErrorMessage class="text-danger" name="StatusField" />
            </b-form-group>
          </b-col>
          <!--agregar nombreobra-->
          <b-col>
            <b-form-group class="mt-3" label="Obra o servicio de alumbrado">
              <Field name="NameWorkField" :rules="validateNameWork" as="text">
                <b-form-input
                  v-model="publicLightingFields.nombreObra"
                  :state="NameWorkState"
                ></b-form-input>
              </Field>
              <ErrorMessage class="text-danger" name="NameWorkField" />
            </b-form-group>
          </b-col>
          <!--agregar domicilio-->
          <b-col>
            <b-form-group class="mt-3" label="Domicilio">
              <Field name="DomicileField" :rules="validateDomicile" as="text">
                <b-form-input
                  v-model="publicLightingFields.domicilio"
                  :state="DomicileState"
                ></b-form-input>
              </Field>
              <ErrorMessage class="text-danger" name="DomicileField" />
            </b-form-group>
          </b-col>
          <!--agregar descripcion de un domicilio-->
          <b-col>
            <b-form-group class="mt-3" label="Descripcion Domicilio">
              <Field
                name="addresdescriptionField"
                :rules="validateAddresdescription"
                as="text"
              >
                <b-form-textarea
                  v-model="publicLightingFields.descripcionDomicilio"
                  :state="addresdescriptionState"
                  rows="4"
                ></b-form-textarea>
              </Field>
              <ErrorMessage class="text-danger" name="addresdescriptionField" />
            </b-form-group>
          </b-col>
          <!--Agregar una descripcion del problema-->
          <b-col>
            <b-form-group class="mt-3" label="Descripcion del problema">
              <Field name="ProblemField" :rules="validateProblem" as="text">
                <b-form-textarea
                  v-model="publicLightingFields.descripcionProblema"
                  :state="ProblemState"
                  rows="4"
                ></b-form-textarea>
              </Field>
              <ErrorMessage class="text-danger" name="ProblemField" />
            </b-form-group>
          </b-col>
        </b-row>

        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            @click="resetPublicLightingFields"
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
import PubliclightingServices from '@/Services/publiclighting.Services'
import TypeTaskLightingServices from '@/Services/tasktypelighting.Services'
import EditExpedientLighting from '@/Services/expedientlighting.Services'
import statusServices from '@/Services/statuslighting.Services'
import { Form, Field, ErrorMessage } from 'vee-validate'
import { ref, inject } from 'vue'
import { useRouter } from 'vue-router'
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
    const { getPublicLighting, createPublicLighting, deletePublicLighting } =
      PubliclightingServices()
    const { getTaskTypeLighting } = TypeTaskLightingServices()
    const { createExpedientLighting, getExpedientLightingByAlumbradoId } =
      EditExpedientLighting()
    const { getStatus } = statusServices()
    const redirect = useRouter()
    const showModal = ref(false)
    const publicLighting = ref([])
    const statusPublicLighting = ref([])
    const typeTaskLighting = ref([])
    const isOpen = ref(false)
    const perPage = ref(7)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('nombreObra')
    const ProblemState = ref(false)
    const NameWorkState = ref(false)
    const addresdescriptionState = ref(false)
    const DomicileState = ref(false)
    const TaskState = ref(false)
    const StatusState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Alumbrado', to: '/ServiciosPublicos/AlumbradoPublico/list' },
      { text: 'Alumbrado publico' }
    ])

    const expedientPublicFieldBlank = ref({
      expedienteAlumbradoId: 0,
      alumbradoId: null,
      archivado: false
    })
    const publicLightingFields = ref({
      alumbradoId: 0,
      descripcionProblema: null,
      nombreObra: '',
      domicilio: null,
      descripcionDomicilio: null,
      tareaTipoId: null,
      estatusId: null,
      tieneExpediente: true,
      archivado: false
    })
    // tarea: null,

    getStatus(data => {
      statusPublicLighting.value = data
      if (data.length === 0) {
        swal.fire({
          title: 'No se encuentra un tipo de estatus registrado!',
          text: 'No se encuentra tipo de estatus registrado en el departamento seleccionado, registre primero un tipo de estatus para continuar',
          icon: 'warning'
        })
      }
    })

    getTaskTypeLighting(data => {
      typeTaskLighting.value = data
      if (data.length === 0) {
        swal.fire({
          title: 'No se encuentra un tipo de tare registrada!',
          text: 'No se encuentra tipo de tarea registradas en el departamento seleccionado, registre primero un tipo de tarea para continuar',
          icon: 'warning'
        })
      }
    })

    const onClickExpedient = alumbradoId => {
      getExpedientLightingByAlumbradoId(alumbradoId, data => {
        redirect.push({
          name: 'ExpedienteAlumbrado-Edit',
          params: { ExpedienteAlumbradoId: alumbradoId }
        })
      })
    }

    const validateTask = () => {
      if (!publicLightingFields.value.tareaTipoId) {
        TaskState.value = false
        return 'Este campo es requerido'
      }
      TaskState.value = true
      return true
    }

    const validateStatus = () => {
      if (!publicLightingFields.value.estatusId) {
        StatusState.value = false
        return 'Este campo es requerido'
      }
      StatusState.value = true
      return true
    }

    const validateProblem = () => {
      if (!publicLightingFields.value.descripcionProblema) {
        ProblemState.value = false
        return 'Este campo es requerido'
      }
      if (
        !/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(
          publicLightingFields.value.descripcionProblema
        )
      ) {
        ProblemState.value = false
        return 'Este campo solo puede contener letras'
      }
      if (!publicLightingFields.value.descripcionProblema.trim().length > 0) {
        ProblemState.value = false
        return 'Este campo no puede contener espacios'
      }
      ProblemState.value = true
      return true
    }

    const validateNameWork = () => {
      if (!publicLightingFields.value.nombreObra) {
        NameWorkState.value = false
        return 'Este campo es requerido'
      }
      if (
        !/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(publicLightingFields.value.nombreObra)
      ) {
        NameWorkState.value = false
        return 'Este campo solo puede contener letras'
      }
      if (!publicLightingFields.value.nombreObra.trim().length > 0) {
        NameWorkState.value = false
        return 'Este campo no puede contener espacios'
      }
      NameWorkState.value = true
      return true
    }

    const validateDomicile = () => {
      if (!publicLightingFields.value.domicilio) {
        DomicileState.value = false
        return 'Este campo es requerido'
      }
      if (
        !/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(publicLightingFields.value.domicilio)
      ) {
        DomicileState.value = false
        return 'Este campo solo puede contener letras'
      }
      if (!publicLightingFields.value.domicilio.trim().length > 0) {
        DomicileState.value = false
        return 'Este campo no puede contener espacios'
      }
      DomicileState.value = true
      return true
    }

    const validateAddresdescription = () => {
      if (!publicLightingFields.value.descripcionDomicilio) {
        addresdescriptionState.value = false
        return 'Este campo es requerido'
      }
      if (
        !/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(
          publicLightingFields.value.descripcionDomicilio
        )
      ) {
        addresdescriptionState.value = false
        return 'Este campo solo puede contener letras'
      }
      if (!publicLightingFields.value.descripcionDomicilio.trim().length > 0) {
        addresdescriptionState.value = false
        return 'Este campo no puede contener espacios'
      }
      addresdescriptionState.value = true
      return true
    }

    // pone mis cambios de mis campos vacios de nuevo
    const publicLightingFieldsBlank = ref(
      JSON.parse(JSON.stringify(publicLightingFields))
    )

    const fields = ref([
      { value: 'aalumbradoId', text: 'ID', sortable: true },
      { value: 'estatus.nombreEstatus', text: 'Estatus' },
      { value: 'tareaTipoAlumbrado.nombreTarea', text: 'Tipo de tarea' },
      { value: 'nombreObra', text: 'nombre de Obra Alumbrado' },
      { value: 'descripcionProblema', text: 'Descripcion del problema' },
      { value: 'domicilio', text: 'Domicilio' },
      { value: 'descripcionDomicilio', text: 'Descripcion Domicilio' },
      { value: 'actions', text: 'Acciones' }
    ])

    const resetPublicLightingFields = () => {
      showModal.value = false
      publicLightingFields.value = JSON.parse(
        JSON.stringify(publicLightingFieldsBlank)
      )
      TaskState.value = false
      ProblemState.value = false
      addresdescriptionState.value = false
      DomicileState.value = false
      StatusState.value = false
      NameWorkState.value = false
    }

    getPublicLighting(data => {
      publicLighting.value = data
      if (publicLighting.value.length > 0) {
        isloading.value = false
      } else {
        if (publicLighting.value.length <= 0) {
          isloading.value = false
        }
      }
    })

    const onFiltered = filteredItems => {
      currentPage.value = 1
    }

    const refreshTable = () => {
      isloading.value = true
      getPublicLighting(data => {
        publicLighting.value = data
        // rows.value = data.length
        if (publicLighting.value.length > 0) {
          isloading.value = false
        } else {
          if (publicLighting.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }

    const addPublicLighting = () => {
      createPublicLighting(publicLightingFields.value, data => {
        expedientPublicFieldBlank.value.alumbradoId = data.alumbradoId
        createExpedientLighting(expedientPublicFieldBlank.value, data => {
          refreshTable()
          swal.fire({
            title: '¡Alumbrado publico agregado correctamente!',
            text: 'Alumbrado registrado satisfactoriamente',
            icon: 'success'
          })
        })
      })
      showModal.value = false
      resetPublicLightingFields()
    }

    const RemovePublicLighting = StreetLightingId => {
      isloading.value = true
      swal
        .fire({
          title: '¿Estas seguro',
          text: 'No podras revertir esto',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Si, Archivar alumbrado!',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            deletePublicLighting(StreetLightingId, data => {
              refreshTable()
            })
            swal.fire({
              title: '¡Alumbrado archivado!',
              text: 'El alumbrado publico ha sido archivado satisfactoriamente.',
              icon: 'success'
            })
          } else {
            isloading.value = false
          }
        })
    }

    return {
      publicLighting,
      statusPublicLighting,
      breadcrumbItems,
      typeTaskLighting,
      publicLightingFields,
      isOpen,
      perPage,
      currentPage,
      filter,
      perPageSelect,
      isloading,
      searchValue,
      searchField,
      publicLightingFieldsBlank,
      fields,
      TaskState,
      NameWorkState,
      addresdescriptionState,
      DomicileState,
      StatusState,
      showModal,
      ProblemState,

      onFiltered,
      onClickExpedient,
      addPublicLighting,
      refreshTable,
      RemovePublicLighting,
      validateTask,
      validateNameWork,
      validateAddresdescription,
      validateDomicile,
      validateStatus,
      resetPublicLightingFields,
      validateProblem
    }
  }
}
</script>

<style></style>
