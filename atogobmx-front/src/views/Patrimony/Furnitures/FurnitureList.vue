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
        <i class="bi bi-easel2-fill"></i>
        Agregar mobiliario
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
      :items="furnitures"
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
            @click="RemoveFurniture(items.mobiliarioId)"
            class="m-1"
            variant="outline-danger"
          >
            <i class="bi bi-trash3" />
            Archivar
          </b-dropdown-item>
          <b-dropdown-item
            class="m-1"
            variant="outline-warning"
            :to="{
              name: 'Mobiliarios-Edit',
              params: { MobiliarioId: items.mobiliarioId }
            }"
            ><i class="bi bi-pencil-square" /> Editar</b-dropdown-item
          >
        </b-dropdown>
      </template>
      <template #item-status="items">
        <b-badge :variant="items.archivado === false ? 'success' : 'danger'">
          {{ items.archivado === false ? 'Activo' : 'Dado de baja' }}
        </b-badge>
      </template>
    </EasyDataTable>
    <b-modal
      id="modal-employee"
      v-model="showModal"
      title="Agregar Mobiliario"
      size="xl"
      hide-footer
      centered
      button-size="lg"
      lazy
    >
      <Form @submit="addFurniture">
        <b-row cols="3">
          <b-col>
            <b-form-group class="mt-3" label="Nomenclatura">
              <Field name="FolioField" :rules="validateFolio" as="text">
                <b-form-input
                  v-model="furnituresFields.codigoInventario"
                  :state="folioState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="FolioField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Tipo de mobiliario">
              <Field
                name="typeFurnitureField"
                :rules="validateTypeFurniture"
                as="text"
              >
                <b-form-select
                  v-model="furnituresFields.tipoMobiliarioId"
                  autofocus
                  :options="typeFurnitures"
                  value-field="tipoMobiliarioId"
                  text-field="nombre"
                  :state="typeFurnitureState"
                />
              </Field>
              <ErrorMessage
                class="text-danger"
                name="typeFurnitureField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Departamento">
              <Field
                name="DepartamentField"
                :rules="validateDepartament"
                as="number"
              >
                <b-form-select
                  v-model="furnituresFields.departamentoId"
                  autofocus
                  :options="departaments"
                  value-field="departamentoId"
                  text-field="nombre"
                  :state="departamentState"
                >
                </b-form-select>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="DepartamentField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Descripción">
              <Field
                name="DescriptionField"
                :rules="validateDescription"
                as="number"
              >
                <b-form-textarea
                  v-model="furnituresFields.descripción"
                  :state="descriptionState"
                  rows="4"
                />
              </Field>
              <ErrorMessage
                class="text-danger"
                name="DescriptionField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Fecha de adquisicion">
              <Field
                name="DateField"
                :rules="validateDate"
                as="number"
              >
                <Datepicker
                  v-model="furnituresFields.fechaAdquisición"
                  locale="es"
                  autoApply
                  :enableTimePicker="false"
                  :state="dateState"
                >
                </Datepicker>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="DateField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Costo">
              <Field
                name="DescriptionField"
                :rules="validateCost"
                as="number"
              >
                <b-form-input
                  v-model="furnituresFields.costo"
                  :state="costState"
                />
              </Field>
              <ErrorMessage
                class="text-danger"
                name="DescriptionField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            @click="resetFurnitureFields"
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
import FurnitureServices from '@/Services/furniture.Services'
import DepartamentServices from '@/Services/departament.Services'
import Datepicker from '@vuepic/vue-datepicker'
import { Form, Field, ErrorMessage } from 'vee-validate'
import { ref, inject } from 'vue'
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
    const {
      getFurnitures,
      createFurniture,
      deleteFurniture,
      getTypeFurnitures
    } = FurnitureServices()
    const swal = inject('$swal')
    const { getDepartaments } = DepartamentServices()
    const showModal = ref(false)
    const furnitures = ref([])
    const typeFurnitures = ref([])
    const departaments = ref([])
    const areas = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('codigoInventario')
    const typeFurnitureState = ref(false)
    const descriptionState = ref(false)
    const folioState = ref(false)
    const dateState = ref(false)
    const costState = ref(false)
    const departamentState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      {
        text: 'Patrimonio publico',
        to: '/PatrimonioMunicipal'
      },
      { text: 'Mobiliario' }
    ])
    const furnituresFields = ref({
      mobiliarioId: 0,
      codigoInventario: '',
      descripción: '',
      fechaAdquisición: null,
      costo: null,
      tipoMobiliarioId: null,
      departamentoId: null,
      archivado: false
    })
    const furnituresFieldsBlank = ref(
      JSON.parse(JSON.stringify(furnituresFields))
    )
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
    const fields = ref([
      { value: 'codigoInventario', text: 'Nomenclatura', sortable: true },
      { value: 'descripción', text: 'Nombre' },
      { value: 'departamentos.nombre', text: 'Departamento' },
      { value: 'fechaAdquisición', text: 'Fecha de adquisición' },
      { value: 'costo', text: 'Costo' },
      { value: 'status', text: 'Estado' },
      { value: 'actions', text: 'Acciones' }
    ])
    const resetFurnitureFields = () => {
      showModal.value = false
      furnituresFields.value = JSON.parse(JSON.stringify(furnituresFieldsBlank))
      typeFurnitureState.value = false
      descriptionState.value = false
      folioState.value = false
      departamentState.value = false
    }
    getFurnitures(data => {
      furnitures.value = data
      if (furnitures.value.length > 0) {
        isloading.value = false
      } else {
        if (furnitures.value.length <= 0) {
          isloading.value = false
        }
      }
    })
    getTypeFurnitures(data => {
      typeFurnitures.value = data
      if (data.length === 0) {
        swal.fire({
          title: '¡No se encuentrar tipos de mobiliarios registrados!',
          text: 'Registre un tipo de mobiliario primero para continuar',
          icon: 'warning'
        })
      }
    })
    const onFiltered = filteredItems => {
      currentPage.value = 1
    }
    const refreshTable = () => {
      isloading.value = true
      getFurnitures(data => {
        furnitures.value = data
        if (furnitures.value.length > 0) {
          isloading.value = false
        } else {
          if (furnitures.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }

    const addFurniture = () => {
      createFurniture(furnituresFields.value, data => {
        refreshTable()
        swal.fire({
          title: '¡Inmueble registrado correctamente!',
          text: 'El inmueble se ha registrado al sistema satisfactoriamente.',
          icon: 'success'
        })
      })
      showModal.value = false
      resetFurnitureFields()
    }

    const RemoveFurniture = mobiliarioId => {
      isloading.value = true
      swal
        .fire({
          title: '¿Estas seguro?',
          text: 'No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Si, Archivar Inmueble!',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            deleteFurniture(mobiliarioId, data => {
              refreshTable()
            })
            swal.fire({
              title: '¡Inmueble archivado!',
              text: 'El ¡inmueble ha sido archivado satisfactoriamente .',
              icon: 'success'
            })
          } else {
            isloading.value = false
          }
        })
    }
    // VALIDATIONS
    const validateTypeFurniture = () => {
      if (!furnituresFields.value.tipoMobiliarioId) {
        typeFurnitureState.value = false
        return 'Este campo es requerido'
      }
      typeFurnitureState.value = true
      return true
    }
    const validateFolio = () => {
      if (!furnituresFields.value.codigoInventario) {
        folioState.value = false
        return 'Este campo es requerido'
      }
      // eslint-disable-next-line no-useless-escape
      if (
        !/^(?=.*\d)(?=.*[a-zA-Z])([A-ZñÑáéíóúÁÉÍÓÚ])[A-Z0-9]+$/i.test(
          furnituresFields.value.codigoInventario
        )
      ) {
        folioState.value = false
        return 'El nombre del area solo puede contener letras'
      }
      folioState.value = true
      return true
    }
    const validateDepartament = () => {
      if (!furnituresFields.value.departamentoId) {
        departamentState.value = false
        return 'Este campo es requerido'
      }
      departamentState.value = true
      return true
    }
    const validateDescription = () => {
      if (!furnituresFields.value.descripción) {
        descriptionState.value = false
        return 'Este campo es requerido'
      }
      descriptionState.value = true
      return true
    }
    const validateDate = () => {
      if (!furnituresFields.value.descripción) {
        dateState.value = false
        return 'Este campo es requerido'
      }
      dateState.value = true
      return true
    }
    const validateCost = () => {
      if (!furnituresFields.value.descripción) {
        costState.value = false
        return 'Este campo es requerido'
      }
      costState.value = true
      return true
    }
    return {
      furnitures,
      typeFurnitures,
      departaments,
      fields,
      breadcrumbItems,
      perPage,
      currentPage,
      filter,
      perPageSelect,
      areas,
      furnituresFieldsBlank,
      furnituresFields,
      isloading,
      searchValue,
      searchField,
      typeFurnitureState,
      descriptionState,
      departamentState,
      folioState,
      dateState,
      costState,
      showModal,

      onFiltered,
      addFurniture,
      refreshTable,
      RemoveFurniture,
      validateTypeFurniture,
      validateDescription,
      validateDepartament,
      validateFolio,
      validateDate,
      validateCost,
      resetFurnitureFields
    }
  }
}
</script>

<style></style>
