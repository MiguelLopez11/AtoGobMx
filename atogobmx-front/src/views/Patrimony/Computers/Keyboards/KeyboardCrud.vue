<template>
  <b-card class="m-2">
    <b-row align-h="end" class="mb-3 mr-1">
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
        <i class="bi bi-keyboard-fill"></i>
        Agregar teclado
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
      :items="keyboards"
      :rows-per-page="5"
      theme-color="#7367f0"
    >
      <template #header-actions="header">
        {{ header.text }}
      </template>
      <template #item-actions="items">
        <b-button
          @click="RemoveKeyboard(items.tecladoId)"
          class="m-1"
          variant="outline-danger"
          ><i class="bi bi-trash3"></i
        ></b-button>
      </template>
    </EasyDataTable>
    <b-modal
      v-model="showModal"
      title="Agregar Teclado"
      size="xl"
      centered
      button-size="lg"
      hide-footer
    >
      <Form @submit="addKeyboard">
        <b-row cols="3">
          <b-col>
            <b-form-group class="mt-3" label="Marca">
              <Field
                name="BrandField"
                :rules="validateBrand"
                as="text"
              >
                <b-form-input v-model="keyboardFields.marca" :state="brandState"> </b-form-input>
              </Field>
              <ErrorMessage class="text-danger" name="BrandField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Tipo de conexión">
              <Field
                name="typeConnectionField"
                :rules="validateTypeConnection"
                as="text"
              >
                <b-form-select
                    v-model="keyboardFields.tipoConexion"
                    autofocus
                    :options="typeConnections"
                    :state="typeConnectionState"
                >
                </b-form-select>
                <!-- <b-form-input type="number" v-model="keyboardFields.tipoConexion" :state="typeConnectionState"> </b-form-input> -->
              </Field>
              <ErrorMessage class="text-danger" name="typeConnectionField"></ErrorMessage>
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
  props: {
    EquipoComputoId: {
      type: Number,
      required: true
    }
  },
  setup (props) {
    const swal = inject('$swal')
    const { getKeyboard, createKeyboard, deleteKeyboard } = ComputerServices()
    // const $toast = useToast()
    const keyboards = ref([])
    const typeConnections = ref(['Alambrica', 'Inhalambrica'])
    const perPage = ref(5)
    const currentPage = ref(1)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const brandState = ref(false)
    const typeConnectionState = ref(false)
    const showModal = ref(false)
    const keyboardFields = ref({
      tecladoId: 0,
      marca: null,
      tipoConexion: null,
      equipoComputoId: props.EquipoComputoId,
      archivado: false
    })
    const displayFieldsBlank = ref(JSON.parse(JSON.stringify(keyboardFields)))
    const fields = ref([
      { value: 'marca', text: 'Marca' },
      { value: 'tipoConexion', text: 'Tipo de conexion' },
      { value: 'actions', text: 'Acciones' }
    ])
    getKeyboard((data) => {
      keyboards.value = data
      if (keyboards.value.length > 0) {
        isloading.value = false
      } else {
        if (keyboards.value.length <= 0) {
          isloading.value = false
        }
      }
    })
    const onFiltered = (filteredItems) => {
      currentPage.value = 1
    }
    const validateBrand = () => {
      if (!keyboardFields.value.marca) {
        brandState.value = false
        return 'Este campo es requerido'
      }
      brandState.value = true
      return true
    }
    const validateTypeConnection = () => {
      if (!keyboardFields.value.tipoConexion) {
        typeConnectionState.value = false
        return 'Este campo es requerido'
      }
      typeConnectionState.value = true
      return true
    }
    const refreshTable = () => {
      isloading.value = true
      getKeyboard((data) => {
        keyboards.value = data
        if (keyboards.value.length > 0) {
          isloading.value = false
        } else {
          if (keyboards.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }
    const addKeyboard = () => {
      createKeyboard(keyboardFields.value, (data) => {
        refreshTable()
        swal.fire({
          title: 'Teclado registrado correctamente!',
          text: 'El teclado se ha registrado al sistema satisfactoriamente.',
          icon: 'success'
        })
      })
      resetRoleFields()
    }
    const resetRoleFields = () => {
      showModal.value = false
      keyboardFields.value = JSON.parse(JSON.stringify(displayFieldsBlank))
      brandState.value = false
      typeConnectionState.value = false
    }
    const RemoveKeyboard = (tecladoId) => {
      isloading.value = true
      swal
        .fire({
          title: '¿Estas seguro?',
          text: 'No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Si, archivar teclado!',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            swal
              .fire({
                title: 'Teclado archivado!',
                text: 'El teclado ha sido archivado satisfactoriamente .',
                icon: 'success'
              })
              .then(result => {
                if (result.isConfirmed) {
                  deleteKeyboard(tecladoId, (data) => {
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
      keyboards,
      typeConnections,
      fields,
      perPage,
      currentPage,
      perPageSelect,
      displayFieldsBlank,
      keyboardFields,
      isloading,
      onFiltered,
      addKeyboard,
      refreshTable,
      RemoveKeyboard,
      brandState,
      typeConnectionState,
      showModal,

      validateBrand,
      validateTypeConnection,
      resetRoleFields
    }
  }
}
</script>

<style>

</style>
