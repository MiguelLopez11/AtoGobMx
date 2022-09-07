<template>
  <b-card class="m-3">
    <b-row align-h="end" class="mb-3 mr-1">
      <b-form-input size="lg" style="width: 350px" v-model="searchValue" type="search" placeholder="Buscar Usuario...">
      </b-form-input>
      <b-button variant="primary" style="
          height: 50px;
          width: auto;
          font-size: 18px;
          margin-right: 15px;
          margin-left: 20px;
        " v-b-modal.modal-area type="submit">
        <i class="bi bi-person-plus-fill"></i>
        Agregar Usuario
      </b-button>
    </b-row>
    <EasyDataTable rows-per-page-message="registros por pagina" empty-message="No se encuentran registros"
      table-class-name="customize-table" buttons-pagination border-cell :loading="isloading" :headers="fields"
      :items="users" :rows-per-page="5" :search-field="searchField" :search-value="searchValue" :table-height="330">
      <template #header-actions="header">{{ header.text }}</template>
      <template #item-actions="items">
        <b-button @click="RemoveUser(items.usuarioId)" class="m-1" variant="outline-danger">
          <i class="bi bi-trash3"></i>
        </b-button>
        <b-button class="m-1" variant="outline-warning" :to="{
          name: 'Usuarios-Edit',
          params: { usuarioId: items.usuarioId },
        }">
          <i class="bi bi-pencil-square" />
        </b-button>
      </template>
    </EasyDataTable>
    <b-modal id="modal-area" @ok.prevent="addUser" title="Agregar Usuario" size="xl" centered button-size="lg" lazy
      ok-title="Registrar area" cancel-title="Cancelar">
      <form ref="form">
        <b-row cols="3">
          <b-col>
            <b-form-group class="mt-3" label="Nombre de Usuario">
              <b-form-input type="text" v-model="userFields.nombre"></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Contraseña">
              <b-form-input type="password" v-model="userFields.contraseña"></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Confirmar Contraseña">
              <b-form-input type="password" v-model="userFields.confirmarContraseña"></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Empleado">
              <b-form-select autofocus :options="employees" value-field="empleadoId" text-field="nombre"
                v-model="userFields.empleadoId"></b-form-select>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Role">
              <b-form-select autofocus :options="areas" value-field="areaId" text-field="nombre"
                v-model="userFields.roleId"></b-form-select>
            </b-form-group>
          </b-col>
        </b-row>
      </form>
    </b-modal>
  </b-card>
</template>

<script>
import UsersServices from '@/Services/users.Services'
import EmployeeServices from '@/Services/employee.Services'
import RoleServices from '@/Services/role.Services'
import { ref } from 'vue'
import { useToast } from 'vue-toast-notification'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    EasyDataTable: window['vue3-easy-data-table']
  },
  setup () {
    const { getUsers, deleteUser, createUser } = UsersServices()
    const { getEmployees } = EmployeeServices()
    const { getRoles } = RoleServices()
    const $toast = useToast()
    const users = ref([])
    const employees = ref([])
    const roles = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('nombre')
    const userFields = ref({
      usuarioId: 0,
      nombreUsuario: '',
      contraseña: '',
      confirmarContraseña: '',
      archivado: false,
      roleId: null
    })
    const areasFieldsBlank = ref(JSON.parse(JSON.stringify(userFields)))
    const fields = ref([
      { value: 'usuarioId', text: 'ID', sortable: true },
      { value: 'nombreUsuario', text: 'Nombre de usuario' },
      { value: 'role.nombre', text: 'Role' },
      { value: 'actions', text: 'Acciones' }
    ])
    getEmployees((data) => {
      employees.value = data
    })
    getRoles((data) => {
      roles.value = data
    })
    getUsers((data) => {
      users.value = data
      if (users.value.length > 0) {
        isloading.value = false
      } else {
        if (users.value.length <= 0) {
          isloading.value = false
        }
      }
    })
    const onFiltered = (filteredItems) => {
      currentPage.value = 1
    }
    const refreshTable = () => {
      isloading.value = true

      getUsers((data) => {
        users.value = data
        if (users.value.length > 0) {
          isloading.value = false
        } else {
          if (users.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }
    const addUser = () => {
      createUser(userFields.value, (data) => {
        refreshTable()
        $toast.success('Empleado registrado correctamente.', {
          position: 'top-right',
          duration: 2000
        })
      })
      userFields.value = JSON.parse(JSON.stringify(areasFieldsBlank))
    }
    const RemoveUser = (areaId) => {
      isloading.value = true
      deleteUser(areaId, (data) => {
        refreshTable()
      })
    }
    return {
      fields,
      perPage,
      currentPage,
      //   rows,
      filter,
      perPageSelect,
      users,
      employees,
      roles,
      areasFieldsBlank,
      userFields,
      isloading,
      searchValue,
      searchField,
      onFiltered,
      addUser,
      refreshTable,
      RemoveUser
    }
  }
}
</script>

<style>
.customize-table {
  /* --easy-table-border: 1px solid #445269;
  --easy-table-row-border: 1px solid #445269; */

  --easy-table-header-font-size: 16px;
  --easy-table-header-height: 50px;
  --easy-table-header-font-color: #fcf6f5ff;
  --easy-table-header-background-color: #2bae66ff;

  --easy-table-header-item-padding: 10px 15px;
  --easy-table-header-item-align: center;
  --easy-table-message-font-size: 17px;
  /* --easy-table-body-even-row-font-color: #fff;
  --easy-table-body-even-row-background-color: #4c5d7a; */

  /* --easy-table-body-row-font-color: #c0c7d2;
  --easy-table-body-row-background-color: #2d3a4f; */
  --easy-table-body-row-height: 50px;
  --easy-table-body-row-font-size: 17px;
  --easy-table-border-radius: 15px;

  --easy-table-body-row-hover-font-color: rgb(0, 0, 0);
  --easy-table-body-row-hover-background-color: rgb(212, 212, 212);

  --easy-table-body-item-padding: 10px 15px;

  --easy-table-footer-background-color: #2bae66ff;
  --easy-table-footer-font-color: #fcf6f5ff;
  --easy-table-footer-font-size: 17px;
  --easy-table-footer-padding: 0px 10px;
  --easy-table-footer-height: 50px;

  /* --easy-table-scrollbar-track-color: #2d3a4f;
  --easy-table-scrollbar-color: #2d3a4f;
  --easy-table-scrollbar-thumb-color: #4c5d7a;;
  --easy-table-scrollbar-corner-color: #2d3a4f;

  --easy-table-loading-mask-background-color: #2d3a4f; */
}
</style>
