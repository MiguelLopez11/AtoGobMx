<template>
  <b-card class="m-2">
    <b-row align-h="end" class="mb-3 mr-1">
      <b-form-input
        size="lg"
        style="width: 350px"
        v-model="searchValue"
        type="search"
        placeholder="Buscar Zona..."
      ></b-form-input>
      <b-button
        variant="primary"
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
        <!-- v-b-modal.modal-cementery -->
        <i class="bi bi-person-plus-fill"></i>
        Agregar Zona
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
      :items="zoneService"
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
          @click="RemoveZoneService(items.zonaId)"
          class="m-1"
          variant="outline-danger"
          ><i class="bi bi-trash3"></i
        ></b-button>
        <b-button
          class="m-1"
          variant="outline-warning"
          :to="{
            name: 'Zona-Edit',
            params: { ZonaId: items.zonaId }
          }"
          ><i class="bi bi-pencil-square"></i
        ></b-button>
      </template>
    </EasyDataTable>
    <b-modal
      id="modal-zone"
      tittle="Agregar Zona"
      v-model="showModal"
      size="xl"
      hide-footer
      button-size="lg"
      lazy
    >
      <Form @submit="addZoneService">
        <b-row cols="2">
          <!-- 1 -->
          <b-col>
            <b-form-group class="mt-3" label="Nombre">
              <Field
                name="NameField"
                :rules="validateName"
                as="text"
              >
                <b-form-input
                  v-model="zoneServiceFields.nombre"
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
          <!-- 2 -->
          <b-col>
            <b-form-group class="mt-3" label="Description">
              <Field name="DescriptionField" :rules="validateDescription" as="text">
                <b-form-input
                  v-model="zoneServiceFields.descripcion"
                  :state="DescriptionState"
                >
                </b-form-input>
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
            @click="resetZoneServiceFields"
          >
            <!-- v-b-modal.modal-cementery -->
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
import ZoneService from '@/Services/zone.Services'
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
    const { getZone, createZone, deleteZone } = ZoneService()
    // const $toast = useToast()
    const zoneService = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('nombre')
    const NameState = ref(false)
    const DescriptionState = ref(false)
    const zoneServiceFields = ref({
      zonaId: 0,
      nombre: null,
      descripcion: null,
      archivado: false
    })

    const ZoneServiceFieldsBlank = ref(
      JSON.parse(JSON.stringify(zoneServiceFields))
    )

    const fields = ref([
      { value: 'nombre', text: 'Nombre' },
      { value: 'descripcion', text: 'Descripcion' },
      { value: 'actions', text: 'Acciones' }
    ])

    const resetZoneServiceFields = () => {
      showModal.value = false
      zoneServiceFields.value = JSON.parse(
        JSON.stringify(ZoneServiceFieldsBlank)
      )
      NameState.value = false
      DescriptionState.value = false
    }

    getZone(data => {
      zoneService.value = data

      if (zoneService.value.length > 0) {
        isloading.value = false
      } else {
        if (zoneService.value.length <= 0) {
          isloading.value = false
        }
      }
    })

    const onFiltered = filteredItems => {
      currentPage.value = 1
    }

    const validateName = () => {
      if (!zoneServiceFields.value.nombre) {
        NameState.value = false
        return 'Este campo es requerido'
      }

      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(zoneServiceFields.value.nombre)) {
        NameState.value = false
        return 'Este campo solo puede contener letras'
      }

      if (!zoneServiceFields.value.nombre.trim().length > 0) {
        NameState.value = false
        return 'Este campo no puede contener espacios'
      }

      NameState.value = true
      return true
    }

    const validateDescription = () => {
      if (!zoneServiceFields.value.descripcion) {
        DescriptionState.value = false
        return 'Este campo es requerido'
      }

      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(zoneServiceFields.value.descripcion)) {
        DescriptionState.value = false
        return 'Este campo solo puede contener numeros'
      }

      if (!zoneServiceFields.value.descripcion.trim().length > 0) {
        DescriptionState.value = false
        return 'Este campo no puede contener espacios'
      }

      DescriptionState.value = true
      return true
    }

    const refreshTable = () => {
      isloading.value = true
      getZone(data => {
        zoneService.value = data

        if (zoneService.value.length > 0) {
          isloading.value = false
        } else {
          if (zoneService.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }

    const addZoneService = () => {
      createZone(zoneServiceFields.value, data => {
        refreshTable()
        swal.fire({
          title: '¡Zona agregado correctamente!',
          text: 'Zona registrado satisfactoriamente',
          icon: 'success'
        })
      })
      showModal.value = false
      resetZoneServiceFields()
    }

    const RemoveZoneService = zoneId => {
      isloading.value = true
      swal.fire({
        title: '¿Estas seguro',
        text: 'No podras revertir esto',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, Archivar Zona!',
        cancelButtonText: 'Cancelar'
      }).then(result => {
        if (result.isConfirmed) {
          deleteZone(zoneId, (data) => {
            refreshTable()
          })
          swal.fire({
            title: '¡Zona archivado!',
            text:
                'La Zona ha sido archivado satisfactoriamente.',
            icon: 'success'
          })
        } else {
          isloading.value = false
        }
      })
    }

    return {
      zoneService,
      zoneServiceFields,
      perPage,
      currentPage,
      filter,
      showModal,
      perPageSelect,
      isloading,
      searchValue,
      searchField,
      ZoneServiceFieldsBlank,
      fields,
      NameState,
      DescriptionState,

      onFiltered,
      addZoneService,
      RemoveZoneService,
      refreshTable,
      validateName,
      validateDescription,
      resetZoneServiceFields
    }
  }
}
</script>

<style></style>
