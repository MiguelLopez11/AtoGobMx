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
        placeholder="Buscar proveedor..."
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
        <i class="bi bi-person-plus-fill"></i>
        Proveedor
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
      :items="provider"
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
            @click="RemoveProvider(items.proveedorId)"
            class="m-1"
            variant="outline-danger"
            ><i class="bi bi-trash3"> Archivar</i></b-dropdown-item
          >
          <b-dropdown-item
            class="m-1"
            variant="outline-warning"
            :to="{
              name: 'Proveedor-Edit',
              params: { ProveedorId: items.proveedorId }
            }"
            ><i class="bi bi-pencil-square" /> Editar</b-dropdown-item
          >
        </b-dropdown>
      </template>
    </EasyDataTable>
    <b-modal
      id="modal-provider"
      title="Agregar Proveedor"
      v-model="showModal"
      size="xl"
      hide-footer
      button-size="lg"
      lazy
    >
      <Form @submit="addProvider">
        <b-row cols="2">
          <!--Agregar Nombre-->
          <b-col>
            <b-form-group class="mt-3" label="Nombre">
              <Field name="NameField" :rules="validateName" as="text">
                <b-form-input
                  v-model="providerFields.nombre"
                  :state="NameState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage class="text-danger" name="NameField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--Agregar RFC-->
          <b-col>
            <b-form-group class="mt-3" label="RFC">
              <Field name="RFCField" :rules="validateRFC" as="text">
                <b-form-input v-model="providerFields.rfc" :state="RFCState">
                </b-form-input>
              </Field>
              <ErrorMessage class="text-danger" name="RFCField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--Agregar Representante legal-->
          <b-col>
            <b-form-group class="mt-3" label="Representante legal">
              <Field
                name="LegalRepresentativeField"
                :rules="validateLegalRepresentative"
                as="text"
              >
                <b-form-input
                  v-model="providerFields.representanteLegal"
                  :state="LegalRepresentativeState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="LegalRepresentativeField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--Agregar Direccion-->
          <b-col>
            <b-form-group class="mt-3" label="Direccion">
              <Field name="DirectionField" :rules="validateDirection" as="text">
                <b-form-input
                  v-model="providerFields.direccion"
                  :state="DirectionState"
                ></b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="DirectionField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--Agregar Telefono-->
          <b-col>
            <b-form-group class="mt-3" label="Telefono">
              <Field name="PhoneField" :rules="validatePhone" as="number">
                <b-form-input
                  v-model="providerFields.telefono"
                  :state="PhoneState"
                  type="number"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="PhoneField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--Agregar email-->
          <b-col>
            <b-form-group class="mt-3" label="Correo electronico">
              <Field
                name="emailField"
                type="email"
                :rules="validateEmail"
                as="text"
              >
                <b-form-input
                  v-model="providerFields.email"
                  :state="emailState"
                />
              </Field>
              <ErrorMessage name="emailField">
                <span class="text-danger">{{ emailMessage }} </span>
                <i class="bi bi-exclamation-circle"></i
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--termino de los campos de registro-->
        </b-row>
        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            @click="resetProviderFields"
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
import ProviderServices from '@/Services/provider.Services'
import { Form, Field, ErrorMessage } from 'vee-validate'
import { ref, inject } from 'vue'
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
    const { getProvider, createProvider, deleteProvider } = ProviderServices()
    const provider = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('nombre')
    const NameState = ref(false)
    const RFCState = ref(false)
    const LegalRepresentativeState = ref(false)
    const DirectionState = ref(false)
    const PhoneState = ref(false)
    const emailState = ref(false)
    const emailMessage = ref('')
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Proveeduria', to: '/Proveeduria' },
      { text: 'Proveedor' }
    ])

    const providerFields = ref({
      proveedorId: 0,
      nombre: null,
      rfc: null,
      representanteLegal: null,
      direccion: null,
      telefono: null,
      email: null,
      archivado: false
    })

    const providerFieldsBlank = ref(JSON.parse(JSON.stringify(providerFields)))
    const fields = ref([
      // { value: 'proveedorId', text: 'ID', sortable: true },
      { value: 'nombre', text: 'Nombre' },
      { value: 'rfc', text: 'RFC' },
      { value: 'representanteLegal', text: 'Representante legal' },
      { value: 'direccion', text: 'Direccion' },
      { value: 'telefono', text: 'Telefono' },
      { value: 'email', text: 'Correo electronico' },
      { value: 'actions', text: 'Acciones' }
    ])

    const resetProviderFields = () => {
      showModal.value = false
      providerFields.value = JSON.parse(JSON.stringify(providerFieldsBlank))
      NameState.value = false
      RFCState.value = false
      LegalRepresentativeState.value = false
      DirectionState.value = false
      PhoneState.value = false
      emailState.value = false
    }

    getProvider(data => {
      provider.value = data
      if (provider.value.length > 0) {
        isloading.value = false
      } else {
        if (provider.value.length <= 0) {
          isloading.value = false
        }
      }
    })

    const onFiltered = filteredItems => {
      currentPage.value = 1
    }

    const validateName = () => {
      if (!providerFields.value.nombre) {
        NameState.value = false
        return 'Este campo es requerido'
      }
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(providerFields.value.nombre)) {
        NameState.value = false
        return 'El nombre de estatus de obras publicas solo puede contener letras'
      }
      if (!providerFields.value.nombre.trim().length > 0) {
        NameState.value = false
        return 'Este campo no puede contener espacios'
      }
      NameState.value = true
      return true
    }

    const validateRFC = () => {
      if (!providerFields.value.rfc) {
        RFCState.value = false
        return 'Este campo es requerido'
      }
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(providerFields.value.rfc)) {
        RFCState.value = false
        return 'El RFC solo puede contener letras'
      }
      if (!providerFields.value.rfc.trim().length > 0) {
        RFCState.value = false
        return 'Este campo no puede contener espacios'
      }
      RFCState.value = true
      return true
    }

    const validateLegalRepresentative = () => {
      if (!providerFields.value.representanteLegal) {
        LegalRepresentativeState.value = false
        return 'Este campo es requerido'
      }
      if (
        !/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(
          providerFields.value.representanteLegal
        )
      ) {
        LegalRepresentativeState.value = false
        return 'El representante solo puede contener letras'
      }
      if (!providerFields.value.representanteLegal.trim().length > 0) {
        LegalRepresentativeState.value = false
        return 'Este campo no puede contener espacios'
      }
      LegalRepresentativeState.value = true
      return true
    }

    const validateDirection = () => {
      if (!providerFields.value.direccion) {
        DirectionState.value = false
        return 'Este campo es requerido'
      }
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(providerFields.value.direccion)) {
        DirectionState.value = false
        return 'La descripcion solo puede contener letras'
      }
      if (!providerFields.value.direccion.trim().length > 0) {
        DirectionState.value = false
        return 'Este campo no puede contener espacios'
      }
      DirectionState.value = true
      return true
    }

    const validatePhone = () => {
      if (!providerFields.value.telefono) {
        PhoneState.value = false
        return 'Este campo es requerido'
      }

      if (!/^[0-9]+$/i.test(providerFields.value.telefono)) {
        PhoneState.value = false
        return 'Este campo solo puede contener numeros'
      }
      // if (!providerFields.value.telefono.trim().length > 10) {
      //   PhoneState.value = false
      //   return 'Este campo puede tener maximo 10 numeros'
      // }

      if (!providerFields.value.telefono.trim().length > 0) {
        PhoneState.value = false
        return 'Este campo no puede contener espacios'
      }

      PhoneState.value = true
      return true
    }

    const validateEmail = () => {
      if (!providerFields.value.email) {
        emailState.value = false
        emailMessage.value = 'Este campo es requerido'
        return emailMessage.value
      }
      const regex = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i
      if (!regex.test(providerFields.value.email)) {
        emailState.value = false
        emailMessage.value = 'Este campo debe ser un correo electrónico válido'
        return emailMessage.value
      }
      emailState.value = true
      return true
    }

    // pone mis cambios de mis campos vacios de nuevo
    const refreshTable = () => {
      isloading.value = true
      getProvider(data => {
        provider.value = data
        if (provider.value.length > 0) {
          isloading.value = false
        } else {
          if (provider.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }

    const addProvider = () => {
      createProvider(providerFields.value, data => {
        refreshTable()
        swal.fire({
          title: '¡Proveedor registrado correctamente!',
          text: 'El proveedor se ha registrado al sistema satisfactoriamente.',
          icon: 'success'
        })
      })
      showModal.value = false
      resetProviderFields()
    }

    const RemoveProvider = ProviderId => {
      isloading.value = true
      swal
        .fire({
          title: '¿Estas seguro?',
          text: 'No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Si, Archivar proveedor!',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            deleteProvider(ProviderId, data => {
              refreshTable()
            })
            swal.fire({
              title: '¡Proveedor archivado!',
              text: 'El proveedor ha sido archivado satisfactoriamente .',
              icon: 'success'
            })
          } else {
            isloading.value = false
          }
        })
    }

    return {
      provider,
      providerFields,
      showModal,
      breadcrumbItems,
      perPage,
      currentPage,
      filter,
      perPageSelect,
      isloading,
      searchValue,
      searchField,
      providerFieldsBlank,
      fields,
      NameState,
      RFCState,
      LegalRepresentativeState,
      DirectionState,
      PhoneState,
      emailState,
      emailMessage,

      onFiltered,
      addProvider,
      refreshTable,
      RemoveProvider,
      validateName,
      validateRFC,
      validateLegalRepresentative,
      validateDirection,
      validatePhone,
      validateEmail,
      resetProviderFields
    }
  }
}
</script>

<style></style>
