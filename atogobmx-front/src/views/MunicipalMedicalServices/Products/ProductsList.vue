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
        placeholder="Buscar..."
      >
      </b-form-input>
      <b-button
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
        <i class="bi bi-plus-circle-fill" />
        Nuevo Producto
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
      :items="medicalProducts"
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
            class="m-1"
            variant="outline-warning"
            @click="openSupplyModal(items.productoId)"
          >
            <i class="bi bi-plus-square-dotted" />
            Resurtir Producto
          </b-dropdown-item>
          <b-dropdown-item
            @click="RemoveProduct(items.productoId)"
            class="m-1"
            variant="outline-danger"
          >
            <i class="bi bi-trash3"> Archivar </i>
          </b-dropdown-item>
          <b-dropdown-item
            class="m-1"
            variant="outline-warning"
            :to="{
              name: 'ServiciosMedicos-Productos-Edit',
              params: { ProductoId: items.productoId }
            }"
          >
            <i class="bi bi-pencil-square" />
            Editar
          </b-dropdown-item>
        </b-dropdown>
      </template>
      <template #item-status="items">
        <b-badge
          class="text-white"
          :variant="
            items.cantidadDisponible === 0
              ? 'danger'
              : '' || items.cantidadDisponible < 5
              ? 'warning'
              : '' || items.cantidadDisponible >= 5
              ? 'success'
              : ''
          "
        >
          <h6 align="center">
            <!-- {{ items.cantidadDisponible }} <br> -->
            {{
              items.cantidadDisponible === 0
                ? 'Sin existencia'
                : '' || items.cantidadDisponible < 5
                ? 'Poca existencia'
                : '' || items.cantidadDisponible >= 5
                ? 'En existencia'
                : ''
            }}
          </h6>
        </b-badge>
      </template>
    </EasyDataTable>
    <b-modal
      v-model="showModal"
      title="Nueva receta"
      size="xl"
      centered
      button-size="lg"
      hide-footer
    >
      <Form @submit="addPrescription">
        <b-row cols="2">
          <b-col>
            <b-form-group class="mt-3" label="Nombre">
              <Field name="NameField" :rules="validateName" as="text">
                <b-form-input
                  v-model="productsFields.nombre"
                  :state="nameState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage class="text-danger" name="NameField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Contenido">
              <Field name="ContentField" :rules="validateContent" as="text">
                <b-form-input
                  v-model="productsFields.contenido"
                  :state="contentState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="ContentField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Fecha de vencimiento">
              <Field
                name="ExpirationDateField"
                :rules="validateExpirationDate"
                as="text"
              >
                <Datepicker
                  v-model="productsFields.fechaVencimiento"
                  locale="es"
                  autoApply
                  :enableTimePicker="false"
                  :state="expirationDateState"
                >
                </Datepicker>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="ExpirationDateField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Stock (Cantidad disponible)">
              <Field name="StockField" :rules="validateStock" as="text">
                <b-form-input
                  v-model="productsFields.cantidadDisponible"
                  :state="stockState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="StockField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            @click="() => (showModal = !showModal)"
          >
            Cancelar
          </b-button>
          <b-button class="w-auto m-2" variant="success" type="submit">
            Guardar
          </b-button>
        </b-row>
      </Form>
    </b-modal>
    <b-modal
      v-model="showModalSupply"
      title="Resurtir producto"
      centered
      button-size="lg"
      hide-footer
    >
      <b-row>
        <b-col>
          <b-form-group title="Cantidad">
            <b-form-input
              v-model="supplyAmount"
              class="m-2"
              placeholder="Ingresa la cantidad a resurtir"
              type="number"
            />
          </b-form-group>
        </b-col>
        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            @click="showModalSupply = !showModalSupply"
          >
            Cancelar
          </b-button>
          <b-button
            class="w-auto m-2"
            variant="success"
            @click="onSupplyProduct()"
          >
            Guardar
          </b-button>
        </b-row>
      </b-row>
    </b-modal>
  </b-card>
</template>

<script>
import MunicipalMedicalServices from '@/Services/municipalMedical.Services'
import { Form, Field, ErrorMessage } from 'vee-validate'
import { ref, inject } from 'vue'
import Datepicker from '@vuepic/vue-datepicker'

// import { useToast } from 'vue-toast-notification'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    Form,
    Field,
    ErrorMessage,
    Datepicker,
    EasyDataTable: window['vue3-easy-data-table']
  },
  setup () {
    const swal = inject('$swal')
    const {
      getMedicalProducts,
      createMedicalProduct,
      deleteMedicalProduct,
      supplyMedicalProduct
    } = MunicipalMedicalServices()
    const medicalProducts = ref([])
    const supplyAmount = ref()
    const productId = ref()
    const showModal = ref(false)
    const showModalSupply = ref(false)
    const nameState = ref(false)
    const contentState = ref(false)
    const expirationDateState = ref(false)
    const stockState = ref(false)
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('marca')
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      {
        text: 'Servicios medicos',
        to: '/ServiciosMedicosMunicipales'
      },
      { text: 'Productos' }
    ])
    const productsFields = ref({
      productoId: 0,
      nombre: '',
      contenido: '',
      fechaVencimiento: null,
      cantidadDisponible: null,
      archivado: false
    })
    const productsFieldsBlank = ref(JSON.parse(JSON.stringify(productsFields)))
    const fields = ref([
      { value: 'nombre', text: 'Nombre' },
      { value: 'contenido', text: 'Contenido' },
      { value: 'fechaVencimiento', text: 'Fecha de vencimiento' },
      { value: 'cantidadDisponible', text: 'Stock(Cantidad disponible)' },
      { value: 'cantidadFaltante', text: 'Cantidad Faltante' },
      { value: 'status', text: 'Estado' },
      { value: 'actions', text: 'Acciones' }
    ])
    getMedicalProducts(data => {
      medicalProducts.value = data
      if (medicalProducts.value.length > 0) {
        isloading.value = false
      } else {
        if (medicalProducts.value.length <= 0) {
          isloading.value = false
        }
      }
    })
    const refreshTable = () => {
      isloading.value = true
      getMedicalProducts(data => {
        medicalProducts.value = data
        if (medicalProducts.value.length > 0) {
          isloading.value = false
        } else {
          if (medicalProducts.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }
    const addPrescription = () => {
      createMedicalProduct(productsFields.value, data => {
        swal.fire({
          title: 'Producto registrado correctamente!',
          text: 'El producto se ha registrado al sistema satisfactoriamente.',
          icon: 'success'
        })
        resetProductsFields()
        refreshTable()
      })
    }
    const RemoveProduct = ProductoId => {
      isloading.value = true
      swal
        .fire({
          title: '¿Estas seguro?',
          text: 'No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Si, archivar Producto!',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            swal
              .fire({
                title: 'Producto archivado!',
                text: 'El producto ha sido archivado satisfactoriamente .',
                icon: 'success'
              })
              .then(result => {
                if (result.isConfirmed) {
                  deleteMedicalProduct(ProductoId, data => {
                    refreshTable()
                  })
                }
              })
          } else {
            isloading.value = false
          }
        })
    }
    const onFiltered = filteredItems => {
      currentPage.value = 1
    }
    const openSupplyModal = productoId => {
      productId.value = productoId
      showModalSupply.value = !showModalSupply.value
    }
    const onSupplyProduct = () => {
      supplyMedicalProduct(productId.value, supplyAmount.value, data => {
        swal.fire({
          title: 'Producto resurtido correctamente!',
          text: 'El producto se ha resurtido al sistema satisfactoriamente.',
          icon: 'success'
        })
        supplyAmount.value = null
        showModalSupply.value = !showModalSupply.value
        refreshTable()
      })
    }
    // VALIDATIONS
    const validateName = () => {
      if (!productsFields.value.nombre) {
        nameState.value = false
        return 'Este campo es requerido'
      }
      nameState.value = true
      return true
    }
    const validateContent = () => {
      if (!productsFields.value.contenido) {
        contentState.value = false
        return 'Este campo es requerido'
      }
      contentState.value = true
      return true
    }
    const validateExpirationDate = () => {
      if (!productsFields.value.fechaVencimiento) {
        expirationDateState.value = false
        return 'Este campo es requerido'
      }
      expirationDateState.value = true
      return true
    }
    const validateStock = () => {
      if (!productsFields.value.cantidadDisponible) {
        stockState.value = false
        return 'Este campo es requerido'
      }
      stockState.value = true
      return true
    }
    const resetProductsFields = () => {
      showModal.value = false
      nameState.value = false
      contentState.value = false
      expirationDateState.value = false
      stockState.value = false
      productsFields.value = JSON.parse(JSON.stringify(productsFieldsBlank))
    }
    return {
      medicalProducts,
      supplyAmount,
      breadcrumbItems,
      productId,
      fields,
      perPage,
      currentPage,
      filter,
      perPageSelect,
      productsFieldsBlank,
      productsFields,
      isloading,
      searchValue,
      showModal,
      showModalSupply,
      searchField,
      nameState,
      contentState,
      expirationDateState,
      stockState,
      //   employeeState,

      onFiltered,
      addPrescription,
      refreshTable,
      RemoveProduct,
      validateName,
      validateContent,
      validateExpirationDate,
      validateStock,
      resetProductsFields,
      openSupplyModal,
      onSupplyProduct
    }
  }
}
</script>

<style></style>
