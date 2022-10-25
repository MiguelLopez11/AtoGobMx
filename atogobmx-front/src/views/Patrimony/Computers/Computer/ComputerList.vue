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
      :table-height="330"
    >
      <template #header-actions="header">
        {{ header.text }}
      </template>
      <template #item-actions="items">
        <b-button
          @click="RemoveRole(items.roleId)"
          class="m-1"
          variant="outline-danger"
          ><i class="bi bi-trash3"></i
        ></b-button>
        <b-button
          class="m-1"
          variant="outline-warning"
          :to="{
            name: 'Roles-Edit',
            params: { RoleId: items.roleId },
          }"
        >
          <i class="bi bi-pencil-square" />
        </b-button>
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
            <b-form-group class="mt-3" label="Nombre">
              <Field
                name="NameField"
                :rules="validateName"
                as="text"
              >
                <b-form-input v-model="computerFields.nombre" :state="nameState"> </b-form-input>
              </Field>
              <ErrorMessage class="text-danger" name="NameField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Descripción">
              <b-form-input v-model="computerFields.descripcion"> </b-form-input>
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
    const { getComputers, createComputer, deleteComputer } = ComputerServices()
    // const $toast = useToast()
    const Computers = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('area.nombre')
    const nameState = ref(false)
    const showModal = ref(false)
    const computerFields = ref({
      equipoComputoId: 0,
      marca: '',
      memoriaRAM: 0,
      almacenamiento: '',
      procesador: '',
      areaId: 0,
      estatusId: 0,
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
      { value: 'actions', text: 'Acciones' }
    ])
    getComputers((data) => {
      Computers.value = data
      if (Computers.value.length > 0) {
        isloading.value = false
      } else {
        if (Computers.value.length <= 0) {
          isloading.value = false
        }
      }
    })
    const onFiltered = (filteredItems) => {
      currentPage.value = 1
    }
    const validateName = () => {
      if (!computerFields.value.nombre) {
        nameState.value = false
        return 'Este campo es requerido'
      }
      // eslint-disable-next-line no-useless-escape
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(computerFields.value.nombre)) {
        nameState.value = false
        return 'El nombre del area solo puede contener letras'
      }
      nameState.value = true
      return true
    }
    const refreshTable = () => {
      isloading.value = true
      getComputers((data) => {
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
      createComputer(computerFields.value, (data) => {
        refreshTable()
        swal.fire({
          title: 'Role registrado correctamente!',
          text: 'El role se ha registrado al sistema satisfactoriamente.',
          icon: 'success'
        })
      })
      resetRoleFields()
    }
    const resetRoleFields = () => {
      showModal.value = false
      computerFields.value = JSON.parse(JSON.stringify(computerFieldsBlank))
      nameState.value = false
    }
    const RemoveRole = (roleId) => {
      isloading.value = true
      swal
        .fire({
          title: '¿Estas seguro?',
          text: 'No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Si, archivar departamento!',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            swal
              .fire({
                title: 'Departamento archivado!',
                text: 'El departamento ha sido archivado satisfactoriamente .',
                icon: 'success'
              })
              .then(result => {
                if (result.isConfirmed) {
                  deleteComputer(roleId, (data) => {
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
      nameState,
      showModal,

      validateName,
      resetRoleFields
    }
  }
}
</script>

<style>

</style>
