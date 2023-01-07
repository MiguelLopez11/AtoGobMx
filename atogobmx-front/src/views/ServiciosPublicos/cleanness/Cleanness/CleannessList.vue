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
        placeholder="Buscar Aseo..."
      ></b-form-input>
      <b-button
        variant="primary"
        style="
          background-color: rgb(94, 80, 238);
          height: 50px;
          width: auto;
          font-size: 18px;
          margin-right : 15px;
          margin-left: 20px;
        "
        @click="showModal = !showModal"
        type="submit"
      >
        <i class="bi bi-person-plus-fill"></i>
        Agregar reporte
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
      :items="cleannessService"
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
            @click="RemoveCleannessService(items.aseoId)"
            class="m-1"
            variant="outline-danger"
            ><i class="bi bi-trash3"> Archivar</i></b-dropdown-item
          >
          <b-dropdown-item
            class="m-1"
            variant="outline-warning"
            :to="{
              name: 'Aseo-Edit',
              params: { AseoId: items.aseoId }
            }"
            ><i class="bi bi-pencil-square" /> Editar</b-dropdown-item
          >
        </b-dropdown>
      </template>
    </EasyDataTable>

    <b-modal
      id="modal-clenness"
      tittle="Agregar reporte"
      v-model="showModal"
      size="xl"
      hide-footer
      button-size="lg"
      lazy
    >
      <Form @submit="addCleannessService">
        <b-row cols="2">
          <!--Agregar nombre -->
          <b-col>
            <b-form-group class="mt-3" label="Nombre de quien reporta: ">
              <Field
                name="NameField"
                :rules="validateName"
                as="text"
              >
                <b-form-input
                  v-model="cleannessServiceFields.nombre"
                  :state="NameState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="NameField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--Agregar problema -->
          <b-col>
            <b-form-group class="mt-3" label="Problema">
              <Field
                name="ProblemField"
                :rules="validateProblem"
                as="text"
              >
                <b-form-input
                  v-model="cleannessServiceFields.problema"
                  :state="ProblemState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="ProblemField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--Agregar Fecha emicion-->
          <b-col>
            <b-form-group class="mt-3" label="Fecha">
              <Field name="DateOfIssueField" :rules="validateDateOfIssue" as="">
                <Datepicker
                  v-model="cleannessServiceFields.fecha"
                  locale="es"
                  autoApply
                  :enableTimePicker="false"
                  :state="DateOfIssueState"
                ></Datepicker>
              </Field>
              <ErrorMessage name="DateOfIssueField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--Agregar Domicilio -->
          <b-col>
            <b-form-group class="mt-3" label="Domicilio">
              <Field name="DomicileField" :rules="validateDomicile" as="text">
                <b-form-input
                  v-model="cleannessServiceFields.domicilio"
                  :state="DomicileState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="DomicileField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Descripcion del problema">
                <b-form-textarea
                  v-model="cleannessServiceFields.descripcion"
                  rows="4"
                ></b-form-textarea>
            </b-form-group>
          </b-col>
        </b-row>

        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            @click="resetCleannessServiceFields"
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
import CleannessService from '@/Services/cleanness.Services'
import { Form, Field, ErrorMessage } from 'vee-validate'
import { ref, inject } from 'vue'
import Datepicker from '@vuepic/vue-datepicker'
// import { useToast } from 'vue-toast-notification'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    EasyDataTable: window['vue3-easy-data-table'],
    Form,
    Field,
    ErrorMessage,
    Datepicker
  },
  setup () {
    const swal = inject('$swal')
    const showModal = ref(false)
    const { getCleanness, createCleanness, deleteCleanness } =
      CleannessService()
    // const $toast = useToast()
    const cleannessService = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('nombre')
    const NameState = ref(false)
    const ProblemState = ref(false)
    const DomicileState = ref(false)
    const ObjectiveState = ref(false)
    const DateOfIssueState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Aseo publico', to: '/ServiciosPublicos/AseoPublico/list' },
      { text: 'Reporte Ciuadadano' }
    ])

    const cleannessServiceFields = ref({
      aseoId: 0,
      nombre: null,
      problema: null,
      fecha: null,
      domicilio: null,
      objetivo: null,
      descripcion: null,
      archivado: false
    })

    const CleannessServiceFieldsBlank = ref(
      JSON.parse(JSON.stringify(cleannessServiceFields))
    )

    const fields = ref([
      { value: 'nombre', text: 'Nombre quien reporto' },
      { value: 'problema', text: 'Problema' },
      { value: 'fecha', text: 'fecha' },
      { value: 'domicilio', text: 'Domicilio' },
      { value: 'descripcion', text: 'Descripcion' },
      { value: 'actions', text: 'Acciones' }
    ])

    const resetCleannessServiceFields = () => {
      showModal.value = false
      cleannessServiceFields.value = JSON.parse(
        JSON.stringify(CleannessServiceFieldsBlank)
      )
      NameState.value = false
      ProblemState.value = false
      DomicileState.value = false
      ObjectiveState.value = false
      DateOfIssueState.value = false
    }

    getCleanness(data => {
      cleannessService.value = data

      if (cleannessService.value.length > 0) {
        isloading.value = false
      } else {
        if (cleannessService.value.length <= 0) {
          isloading.value = false
        }
      }
    })

    const onFiltered = filteredItems => {
      currentPage.value = 1
    }

    const validateName = () => {
      if (!cleannessServiceFields.value.nombre) {
        NameState.value = false
        return 'Este campo es requerido'
      }

      if (
        !/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(
          cleannessServiceFields.value.nombre
        )
      ) {
        NameState.value = false
        return 'Este campo solo puede contener letras'
      }

      if (!cleannessServiceFields.value.nombre.trim().length > 0) {
        NameState.value = false
        return 'Este campo no puede contener espacios'
      }

      NameState.value = true
      return true
    }

    const validateProblem = () => {
      if (!cleannessServiceFields.value.problema) {
        ProblemState.value = false
        return 'Este campo es requerido'
      }

      if (
        !/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(
          cleannessServiceFields.value.problema
        )
      ) {
        ProblemState.value = false
        return 'Este campo solo puede contener numeros'
      }

      if (
        !cleannessServiceFields.value.problema.trim().length > 0
      ) {
        ProblemState.value = false
        return 'Este campo no puede contener espacios'
      }

      ProblemState.value = true
      return true
    }

    const validateDateOfIssue = () => {
      if (!cleannessServiceFields.value.fecha) {
        DateOfIssueState.value = false
        return 'Este campo es requerido'
      }
      DateOfIssueState.value = false
      return true
    }

    const validateDomicile = () => {
      if (!cleannessServiceFields.value.domicilio) {
        DomicileState.value = false
        return 'Este campo es requerido'
      }

      if (
        !/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(
          cleannessServiceFields.value.domicilio
        )
      ) {
        DomicileState.value = false
        return 'Este campo solo puede contener numeros'
      }

      if (!cleannessServiceFields.value.domicilio.trim().length > 0) {
        DomicileState.value = false
        return 'Este campo no puede contener espacios'
      }

      DomicileState.value = true
      return true
    }

    const validateObjective = () => {
      if (!cleannessServiceFields.value.objetivo) {
        ObjectiveState.value = false
        return 'Este campo es requerido'
      }

      if (
        !/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(cleannessServiceFields.value.objetivo)
      ) {
        ObjectiveState.value = false
        return 'Este campo solo puede contener numeros'
      }

      if (!cleannessServiceFields.value.objetivo.trim().length > 0) {
        ObjectiveState.value = false
        return 'Este campo no puede contener espacios'
      }

      ObjectiveState.value = true
      return true
    }

    const refreshTable = () => {
      isloading.value = true
      getCleanness(data => {
        cleannessService.value = data

        if (cleannessService.value.length > 0) {
          isloading.value = false
        } else {
          if (cleannessService.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }

    const addCleannessService = () => {
      createCleanness(cleannessServiceFields.value, data => {
        refreshTable()
        swal.fire({
          title: '¡Aseo agregado correctamente!',
          text: 'Aseo registrado satisfactoriamente',
          icon: 'success'
        })
      })
      showModal.value = false
      resetCleannessServiceFields()
    }

    const RemoveCleannessService = cleannessId => {
      isloading.value = true
      swal
        .fire({
          title: '¿Estas seguro',
          text: 'No podras revertir esto',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Si, Archivar Aseo!',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            deleteCleanness(cleannessId, data => {
              refreshTable()
            })
            swal.fire({
              title: '¡Aseo archivado!',
              text: 'El aseo ha sido archivado satisfactoriamente.',
              icon: 'success'
            })
          } else {
            isloading.value = false
          }
        })
    }

    return {
      cleannessService,
      cleannessServiceFields,
      breadcrumbItems,
      perPage,
      currentPage,
      filter,
      DateOfIssueState,
      showModal,
      perPageSelect,
      isloading,
      searchValue,
      searchField,
      CleannessServiceFieldsBlank,
      fields,
      NameState,
      ProblemState,
      DomicileState,
      ObjectiveState,

      onFiltered,
      addCleannessService,
      RemoveCleannessService,
      refreshTable,
      validateName,
      validateProblem,
      validateDateOfIssue,
      validateDomicile,
      validateObjective,
      resetCleannessServiceFields
    }
  }
}
</script>

<style></style>
