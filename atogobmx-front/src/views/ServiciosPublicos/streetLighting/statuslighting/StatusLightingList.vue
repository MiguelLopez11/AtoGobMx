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
        placeholder="Buscar Estatus Alumbrado..."
      ></b-form-input>
      <b-button
        variant="primary"
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
        <!-- v-b-modal.modal-statuslighting -->
        <i class="bi bi-person-plus-fill"></i>
        Estatus Alumbrado
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
      :items="statusLighting"
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
            @click="RemoveStatusLighting(items.estatusAlumbradoId)"
            class="m-1"
            variant="outline-danger"
            ><i class="bi bi-trash3"> Archivar</i></b-dropdown-item
          >
          <b-dropdown-item
            class="m-1"
            variant="outline-warning"
            :to="{
              name: 'EstatusAlumbrado-Edit',
              params: { EstatusId: items.estatusAlumbradoId }
            }"
            ><i class="bi bi-pencil-square" /> Editar</b-dropdown-item
          >
        </b-dropdown>
      </template>
    </EasyDataTable>
    <b-modal
      id="modal-statuslighting"
      title="Agregar Estatus Alumbrado"
      v-model="showModal"
      size="xl"
      hide-footer
      button-size="lg"
      lazy
    >
      <Form @submit="addStatusLighting">
        <b-row cols="2">
          <!--Agregar nombre-->
          <b-col>
            <b-form-group class="mt-3" label="Nombre">
              <Field name="NameField" :rules="validateName" as="text">
                <b-form-input
                  v-model="statusLightingFields.nombreEstatus"
                  :state="NameState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage class="text-danger" name="NameField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--Agregar descripcion-->
          <b-col>
            <b-form-group class="mt-3" label="Descripcion">
                <b-form-textarea
                  v-model="statusLightingFields.descripcion"
                  rows="4"
                ></b-form-textarea>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            @click="resetPublicLightingFields"
          >
            <!-- v-b-modal.modal-statuslighting -->
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
import StatusLightingServices from '@/Services/statuslighting.Services'
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
    const { getStatus, createStatusLighting, deleteStatusLighting } =
      StatusLightingServices()
    // const $toast = useToast()
    const statusLighting = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    // const rows = ref(null)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('nombreEstatus')
    const NameState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Alumbrado', to: '/ServiciosPublicos/AlumbradoPublico/list' },
      { text: 'Estatus alumbrado' }
    ])

    const statusLightingFields = ref({
      estatusAlumbradoId: 0,
      nombreEstatus: null,
      descripcion: null,
      archivado: false
    })

    const statusLightingFieldsBlank = ref(
      JSON.parse(JSON.stringify(statusLightingFields))
    )
    const fields = ref([
      // { value: 'estatusAlumbradoId', text: 'ID', sortable: true },
      { value: 'nombreEstatus', text: 'Nombre' },
      { value: 'descripcion', text: 'Descripcion' },
      { value: 'actions', text: 'Acciones' }
    ])

    const resetPublicLightingFields = () => {
      showModal.value = false
      statusLightingFields.value = JSON.parse(
        JSON.stringify(statusLightingFieldsBlank)
      )
      NameState.value = false
    }

    getStatus(data => {
      statusLighting.value = data
      // rows.value = data.length
      if (statusLighting.value.length > 0) {
        isloading.value = false
      } else {
        if (statusLighting.value.length <= 0) {
          isloading.value = false
        }
      }
    })

    const onFiltered = filteredItems => {
      // rows.value = filteredItems.length
      currentPage.value = 1
    }

    const validateName = () => {
      if (!statusLightingFields.value.nombreEstatus) {
        NameState.value = false
        return 'Este campo es requerido'
      }
      if (
        !/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(
          statusLightingFields.value.nombreEstatus
        )
      ) {
        NameState.value = false
        return 'El nombre de estatus solo puede contener letras'
      }
      if (!statusLightingFields.value.nombreEstatus.trim().length > 0) {
        NameState.value = false
        return 'Este campo no puede contener espacios'
      }
      NameState.value = true
      return true
    }
    const refreshTable = () => {
      isloading.value = true
      getStatus(data => {
        statusLighting.value = data
        // rows.value = data.length
        if (statusLighting.value.length > 0) {
          isloading.value = false
        } else {
          if (statusLighting.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }
    const addStatusLighting = () => {
      createStatusLighting(statusLightingFields.value, data => {
        refreshTable()
        swal.fire({
          title: '¡Estatus registrado correctamente!',
          text: 'El estatus se ha registrado al sistema satisfactoriamente.',
          icon: 'success'
        })
        // $toast.success('Estatus alumbrado registrado correctamente.', {
        //   position: 'top-right',
        //   duration: 1500
        // })
      })
      // resetStreetLightingFields()
      // statusLightingFields.value = JSON.parse(
      //   JSON.stringify(statusLightingFieldsBlank)
      // )
      showModal.value = false
      resetPublicLightingFields()
    }
    const RemoveStatusLighting = StreetLightingId => {
      isloading.value = true
      swal
        .fire({
          title: '¿Estas seguro?',
          text: 'No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Si, Archivar empleado!',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            deleteStatusLighting(StreetLightingId, data => {
              refreshTable()
            })
            swal.fire({
              title: '¡Estatus archivado!',
              text: 'El estatus ha sido archivado satisfactoriamente .',
              icon: 'success'
            })
          } else {
            isloading.value = false
          }
        })
      // deleteStatusLighting(StreetLightingId, data => {
      //   refreshTable()
      // })
    }
    return {
      statusLighting,
      statusLightingFields,
      breadcrumbItems,
      showModal,
      perPage,
      currentPage,
      // rows,
      filter,
      perPageSelect,
      isloading,
      searchValue,
      searchField,
      statusLightingFieldsBlank,
      // lightingFailuresValues,
      fields,
      NameState,
      // option,
      onFiltered,
      addStatusLighting,
      refreshTable,
      RemoveStatusLighting,
      validateName,
      resetPublicLightingFields
      // resetStreetLightingFields
    }
  }
}
</script>

<style></style>
