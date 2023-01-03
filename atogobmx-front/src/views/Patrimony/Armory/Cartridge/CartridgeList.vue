<template>
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
        <i class="bi bi-car-front-fill" />
        Agregar Cartucho
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
      :items="cartridges"
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
            @click="RemoveCartRidge(items.cartuchoId)"
            class="m-1"
            variant="outline-danger"
          >
            <i class="bi bi-trash3"> Archivar </i>
          </b-dropdown-item>
          <!-- <b-dropdown-item
            class="m-1"
            variant="outline-warning"
            :to="{
              name: 'Armeria-Edit',
              params: { ArmaId: items.armaId }
            }"
          >
            <i class="bi bi-pencil-square" />
            Editar
          </b-dropdown-item> -->
        </b-dropdown>
      </template>
    </EasyDataTable>
    <b-modal
      v-model="showModal"
      title="Registrar Arma"
      size="xl"
      centered
      button-size="lg"
      hide-footer
    >
      <Form @submit="addArmory">
        <b-row cols="3">
          <b-col>
            <b-form-group class="mt-3" label="Nomenclatura">
              <Field
                name="NomenclatureField"
                :rules="validateNomeclature"
                as="text"
              >
                <b-form-input
                  placeholder="Ingresa una nomenclatura"
                  v-model="cartridgeFields.nomenclatura"
                  :state="nomenclatureState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="NomenclatureField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Marca">
              <Field name="BrandField" :rules="validateBrand" as="text">
                <b-form-input
                  placeholder="Ingresa la marca del cartucho"
                  v-model="cartridgeFields.marca"
                  :state="brandState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="BrandField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Tipo de cartucho">
              <Field name="BrandField" :rules="validateTypeWeapon" as="text">
                <b-form-input
                  placeholder="Ingresa el tipo de cartucho"
                  v-model="cartridgeFields.tipoArma"
                  :state="typeWeaponState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="BrandField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Calibre">
              <Field name="modelField" :rules="validateGauge" as="text">
                <b-form-input
                  placeholder="Ingresa el calibre del cartucho"
                  v-model="cartridgeFields.calibre"
                  :state="gaugeState"
                  type="number"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="modelField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Fecha de Adquisición">
              <Field name="memoryField" :rules="validateDate" as="text">
                <Datepicker
                  v-model="cartridgeFields.fechaAdquisicion"
                  locale="es"
                  autoApply
                  :enableTimePicker="false"
                  :state="dateState"
                >
                </Datepicker>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="memoryField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Costo">
              <Field name="CostField" :rules="validateCost" as="text">
                <b-form-input
                  v-model="cartridgeFields.costo"
                  :state="costState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="CostField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            @click="resetCartRidgeFields()"
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
import ArmoryServices from '@/Services/armory.Services'
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
  props: {
    ArmaId: {
      type: Number,
      required: true
    }
  },
  setup (props) {
    const swal = inject('$swal')
    const { getCartridges, createCartridge, deleteCartridge } = ArmoryServices()
    const cartridges = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('nomenclatura')
    const nomenclatureState = ref(false)
    const brandState = ref(false)
    const typeWeaponState = ref(false)
    const gaugeState = ref(false)
    const dateState = ref(false)
    const costState = ref(false)
    const showModal = ref(false)
    const cartridgeFields = ref({
      cartuchoId: 0,
      nomenclatura: null,
      marca: null,
      tipoArma: null,
      calibre: null,
      armeriaId: props.ArmaId,
      fechaAdquisicion: null,
      costo: null,
      archivado: false
    })
    const cartridgeFieldsBlank = ref(JSON.parse(JSON.stringify(cartridgeFields)))
    const fields = ref([
      { value: 'nomenclatura', text: 'Nomenclatura' },
      { value: 'marca', text: 'Marca' },
      { value: 'tipoArma', text: 'Tipo de Cartucho' },
      { value: 'calibre', text: 'Calibre' },
      { value: 'fechaAdquisicion', text: 'Fecha de adquisición' },
      { value: 'costo', text: 'Costo de adquisición' },
      { value: 'actions', text: 'Acciones' }
    ])
    getCartridges(props.ArmaId, data => {
      cartridges.value = data
      if (cartridges.value.length > 0) {
        isloading.value = false
      } else {
        if (cartridges.value.length <= 0) {
          isloading.value = false
        }
      }
    })
    const refreshTable = () => {
      isloading.value = true
      getCartridges(props.ArmaId, data => {
        cartridges.value = data
        if (cartridges.value.length > 0) {
          isloading.value = false
        } else {
          if (cartridges.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }
    const addArmory = () => {
      createCartridge(cartridgeFields.value, data => {
        refreshTable()
        swal.fire({
          title: 'Cartucho registrado correctamente!',
          text: 'El cartucho se ha registrado al sistema satisfactoriamente.',
          icon: 'success'
        })
      })
      resetCartRidgeFields()
    }
    const RemoveCartRidge = ArmaId => {
      isloading.value = true
      swal
        .fire({
          title: '¿Estas seguro?',
          text: 'No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Si, archivar cartucho!',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            swal
              .fire({
                title: 'Cartucho archivado!',
                text: 'El cartucho ha sido archivado satisfactoriamente .',
                icon: 'success'
              })
              .then(result => {
                if (result.isConfirmed) {
                  deleteCartridge(ArmaId, data => {
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
    // VALIDATIONS
    const validateNomeclature = () => {
      if (!cartridgeFields.value.nomenclatura) {
        nomenclatureState.value = false
        return 'Este campo es requerido'
      }
      nomenclatureState.value = true
      return true
    }
    const validateBrand = () => {
      if (!cartridgeFields.value.marca) {
        brandState.value = false
        return 'Este campo es requerido'
      }
      brandState.value = true
      return true
    }
    const validateTypeWeapon = () => {
      if (!cartridgeFields.value.tipoArma) {
        typeWeaponState.value = false
        return 'Este campo es requerido'
      }
      typeWeaponState.value = true
      return true
    }
    const validateGauge = () => {
      if (!cartridgeFields.value.calibre) {
        gaugeState.value = false
        return 'Este campo es requerido'
      }
      gaugeState.value = true
      return true
    }
    const validateDate = () => {
      if (!cartridgeFields.value.fechaAdquisicion) {
        dateState.value = false
        return 'Este campo es requerido'
      }
      dateState.value = true
      return true
    }
    const validateCost = () => {
      if (!cartridgeFields.value.costo) {
        costState.value = false
        return 'Este campo es requerido'
      }
      costState.value = true
      return true
    }
    const resetCartRidgeFields = () => {
      showModal.value = false
      nomenclatureState.value = false
      brandState.value = false
      typeWeaponState.value = false
      gaugeState.value = false
      costState.value = false
      dateState.value = false
      cartridgeFields.value = JSON.parse(JSON.stringify(cartridgeFieldsBlank))
    }
    return {
      cartridges,
      fields,
      perPage,
      currentPage,
      filter,
      perPageSelect,
      cartridgeFieldsBlank,
      cartridgeFields,
      isloading,
      searchValue,
      searchField,
      nomenclatureState,
      brandState,
      typeWeaponState,
      gaugeState,
      costState,
      dateState,
      showModal,

      onFiltered,
      addArmory,
      refreshTable,
      RemoveCartRidge,
      resetCartRidgeFields,
      validateNomeclature,
      validateBrand,
      validateTypeWeapon,
      validateGauge,
      validateDate,
      validateCost
    }
  }
}
</script>

<style></style>
