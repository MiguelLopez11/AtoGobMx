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
        placeholder="Buscar Obras Publicas..."
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
        Obras Publicas
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
      :items="publicWorks"
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
            @click="RemovePublicWorks(items.obraId)"
            class="m-1"
            variant="outline-danger"
            ><i class="bi bi-trash3"> Archivar</i></b-dropdown-item
          >
          <b-dropdown-item
            class="m-1"
            variant="outline-warning"
            :to="{
              name: 'ObrasPublicas-Edit',
              params: { ObraId: items.obraId }
            }"
            ><i class="bi bi-pencil-square" /> Editar</b-dropdown-item
          >
        </b-dropdown>
      </template>
    </EasyDataTable>
    <b-modal
      id="modal-publicworks"
      title="Agregar Obras Publicas"
      v-model="showModal"
      size="xl"
      hide-footer
      button-size="lg"
      lazy
    >
      <Form @submit="addPublicWorks">
        <b-row cols="2">
          <!--Agregar nombre de obra-->
          <b-col>
            <b-form-group class="mt-3" label="Nombre obra">
              <Field name="NameWorksField" :rules="validateNameWorks" as="text">
                <b-form-input
                  v-model="publicWorksFields.nombre"
                  :state="NameWorksState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="NameWorksField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--Checkbox-->
          <b-col>
            <b-form-group
              horizontal
              class="mt-3"
              label="¿Contrató una agencia de operaciones?"
            >
              <b-form-checkbox
                style=""
                v-model="isAgency"
                size="lg"
                :state="true"
              >
              </b-form-checkbox>
            </b-form-group>
          </b-col>
          <!--Agregar encargado de obra-->
          <b-col v-if="isAgency === false">
            <b-form-group class="mt-3" label="Encargado obra">
              <b-form-input v-model="publicWorksFields.encargado">
              </b-form-input>
            </b-form-group>
          </b-col>
          <!--Agregar operador de obra-->
          <b-col v-if="isAgency === false">
            <b-form-group class="mt-3" label="Operador de la obra">
              <b-form-input v-model="publicWorksFields.operadorObra">
              </b-form-input>
            </b-form-group>
          </b-col>
          <!--Agregar operador de vehiculo-->
          <b-col v-if="isAgency === false">
            <b-form-group class="mt-3" label="Operador del vehiculo">
              <b-form-input v-model="publicWorksFields.operadorVehiculo">
              </b-form-input>
            </b-form-group>
          </b-col>
          <!--Agregar nombre agencia-->
          <b-col v-if="isAgency">
            <b-form-group class="mt-3" label="Nombre agencia">
              <b-form-input v-model="publicWorks.agencia"> </b-form-input>
            </b-form-group>
          </b-col>
          <!--Agregar representante legal-->
          <b-col v-if="isAgency">
            <b-form-group class="mt-3" label="Representante legal">
              <b-form-input v-model="publicWorksFields.representante">
              </b-form-input>
            </b-form-group>
          </b-col>
          <!--Agregar RFC-->
          <b-col v-if="isAgency">
            <b-form-group class="mt-3" label="RFC">
              <b-form-input v-model="publicWorksFields.rFC"> </b-form-input>
            </b-form-group>
          </b-col>
          <!--agregar un estatus obra-->
          <b-col>
            <b-form-group class="mt-3" label="Estatus de la obra">
              <Field
                name="WorkStatusField"
                :rules="validateWorkStatus"
                as="text"
              >
                <b-form-select
                  v-model="publicWorksFields.estatusObraId"
                  autofocus
                  :state="WorkStatusState"
                  :options="worksStatus"
                  value-field="estatusObraId"
                  text-field="nombre"
                ></b-form-select>
              </Field>
              <ErrorMessage class="text-danger" name="WorkStatusField" />
            </b-form-group>
          </b-col>
          <!--Descripcion-->
          <b-col>
            <b-form-group class="mt-3" label="Descripcion">
              <Field
                name="DescriptionField"
                :rules="validateDescription"
                as="text"
              >
                <b-form-textarea
                  v-model="publicWorksFields.descripcion"
                  :state="DescriptionState"
                  rows="4"
                ></b-form-textarea>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="DescriptionField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row cols="1" align-h="center">
          <b-button v-b-toggle.collapse-1 variant="primary"> Mapa </b-button>
          <b-collapse id="collapse-1" class="mt-2">
            <b-card>
              <b-alert variant="warning" dismissible show
                >Arrastra el punto del mapa al lugar donde se encuentra su
                gabeta.</b-alert
              >
              <GMapMap
                :center="center"
                :zoom="15"
                :options="{
                  zoomControl: true,
                  mapTypeControl: false,
                  scaleControl: false,
                  rotateControl: true,
                  disableDefaultUi: false
                }"
                style="width: 100%; height: 500px"
                @click="onAddMaker"
              >
                <GMapMarker
                  :position="maker.position"
                  :draggable="true"
                />
              </GMapMap>
            </b-card>
          </b-collapse>
          <!-- {{markers}} -->
        </b-row>
        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            @click="resetPublicWorksFields"
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
import PublicWorksServices from '@/Services/publickworks.Services'
import worksStatusServices from '@/Services/worksstatus.Services'
import CementeryService from '@/Services/cementery.Services'
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
    const { getPublicWorks, createPublicWorks, deletePublicWorks } =
      PublicWorksServices()
    const { getWorksStatus } = worksStatusServices()
    const { getCementeryById } = CementeryService()
    const publicWorks = ref([])
    const worksStatus = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('obraId')
    const NameWorksState = ref(false)
    const DescriptionState = ref(false)
    const WorkStatusState = ref(false)
    const isAgency = ref(false)
    // const AgencyNameState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Obras publicas', to: '/Obras' },
      { text: 'Obras publicas' }
    ])

    const publicWorksFields = ref({
      obraId: 0,
      nombre: null,
      encargado: null,
      operadorObra: null,
      operadorVehiculo: null,
      latitud: null,
      longitud: null,
      descripcion: null,
      agencia: null,
      representante: null,
      rFC: null,
      estatusObraId: null,
      archivado: false
    })

    getWorksStatus(data => {
      worksStatus.value = data
      if (data.length === 0) {
        swal.fire({
          title:
            'No se encuentra un tipo de estatus en obras publicas registrado!',
          text: 'No se encuentra tipo de estatus en obras publicas registrado en el departamento seleccionado, registre primero un tipo de estatus en obras publicas para continuar',
          icon: 'warning'
        })
      }
    })

    const center = ref({ lat: 20.5413702, lng: -102.691446 })
    const maker = ref({
      position: {
        lat: 0,
        lng: 0
      }
    })

    const onSelectCementery = () => {
      getCementeryById(publicWorksFields.value.cementerioId, data => {
        center.value.lat = data.latitud
        center.value.lng = data.longitud
      })
    }
    const onAddMaker = location => {
      maker.value.position.lat = location.latLng.lat()
      maker.value.position.lng = location.latLng.lng()
      publicWorksFields.value.latitud = location.latLng.lat()
      publicWorksFields.value.longitud = location.latLng.lng()
    }

    const publicWorksFieldsBlank = ref(
      JSON.parse(JSON.stringify(publicWorksFields))
    )

    const fields = ref([
      // { value: 'obraId', text: 'ID', sortable: true },
      { value: 'nombre', text: 'Nombre de la obra' },
      { value: 'descripcion', text: 'Descripcion' },
      { value: 'oP_EstatusObras.nombre', text: 'Estatus de la obra' },
      { value: 'actions', text: 'Acciones' }
    ])

    const resetPublicWorksFields = () => {
      showModal.value = false
      publicWorksFields.value = JSON.parse(
        JSON.stringify(publicWorksFieldsBlank)
      )
      NameWorksState.value = false
      DescriptionState.value = false
      WorkStatusState.value = false
      // AgencyNameState.value = false
    }

    getPublicWorks(data => {
      publicWorks.value = data
      // rows.value = data.length
      if (publicWorks.value.length > 0) {
        isloading.value = false
      } else {
        if (publicWorks.value.length <= 0) {
          isloading.value = false
        }
      }
    })

    const onFiltered = filteredItems => {
      currentPage.value = 1
    }

    const validateNameWorks = () => {
      if (!publicWorksFields.value.nombre) {
        NameWorksState.value = false
        return 'Este campo es requerido'
      }
      if (
        !/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ 0-9]+$/i.test(publicWorksFields.value.nombre)
      ) {
        NameWorksState.value = false
        return 'El nombre de la obra solo puede contener letras'
      }
      if (!publicWorksFields.value.nombre.trim().length > 0) {
        NameWorksState.value = false
        return 'Este campo no puede contener espacios'
      }
      NameWorksState.value = true
      return true
    }

    const validateDescription = () => {
      if (!publicWorksFields.value.descripcion) {
        DescriptionState.value = false
        return 'Este campo es requerido'
      }
      if (
        !/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(publicWorksFields.value.descripcion)
      ) {
        DescriptionState.value = false
        return 'La descripcion solo puede contener letras y numeros'
      }
      if (!publicWorksFields.value.descripcion.trim().length > 0) {
        DescriptionState.value = false
        return 'Este campo no puede contener espacios'
      }
      DescriptionState.value = true
      return true
    }

    const validateWorkStatus = () => {
      if (!publicWorksFields.value.estatusObraId) {
        WorkStatusState.value = false
        return 'Este campo es requerido'
      }
      WorkStatusState.value = true
      return true
    }

    // pone mis cambios de mis campos vacios de nuevo
    const refreshTable = () => {
      isloading.value = true
      getPublicWorks(data => {
        publicWorks.value = data
        if (publicWorks.value.length > 0) {
          isloading.value = false
        } else {
          if (publicWorks.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }

    const addPublicWorks = () => {
      if (maker.value.position.lat === 0 && maker.value.position.lng === 0) {
        swal.fire({
          title: '¡Mapa vacío!',
          text: 'Marque en el mapa donde se encuentra el cementerio.',
          icon: 'error'
        })
      } else {
        publicWorksFields.value.latitud = maker.value.position.lat
        publicWorksFields.value.longitud = maker.value.position.lng
        createPublicWorks(publicWorksFields.value, data => {
          refreshTable()
          swal.fire({
            title: '¡Obras publicas registrado correctamente!',
            text: 'La obra publica se ha registrado al sistema satisfactoriamente.',
            icon: 'success'
          })
        })
        showModal.value = false
        resetPublicWorksFields()
      }
    }

    const RemovePublicWorks = StreetLightingId => {
      isloading.value = true
      swal
        .fire({
          title: '¿Estas seguro?',
          text: 'No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Si, Archivar obra!',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            deletePublicWorks(StreetLightingId, data => {
              refreshTable()
            })
            swal.fire({
              title: '¡Obras publicas archivado!',
              text: 'La obra publica ha sido archivado satisfactoriamente .',
              icon: 'success'
            })
          } else {
            isloading.value = false
          }
        })
    }

    return {
      publicWorks,
      worksStatus,
      publicWorksFields,
      breadcrumbItems,
      showModal,
      center,
      maker,
      perPage,
      currentPage,
      filter,
      perPageSelect,
      isloading,
      searchValue,
      searchField,
      publicWorksFieldsBlank,
      fields,
      NameWorksState,
      WorkStatusState,
      DescriptionState,
      // AgencyNameState,
      isAgency,

      onFiltered,
      addPublicWorks,
      refreshTable,
      RemovePublicWorks,
      onAddMaker,
      onSelectCementery,
      validateNameWorks,
      // validateInCharge,
      validateDescription,
      validateWorkStatus,
      // validateAgencyName,
      resetPublicWorksFields
    }
  }
}
</script>

<style scoped>
.form-check {
  display: inline-block;
}
</style>
