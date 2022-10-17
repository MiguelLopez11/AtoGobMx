<template>
  <b-card class="m-3">
    <b-row align-h="end" class="mb-3 mr-1">
      <b-form-input
        size="lg"
        style="width: 350px"
        v-model="searchValue"
        type="search"
        placeholder="Buscar tipo de tarea alumbrado..."
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
        v-b-modal.modal-tasktypelighting
        type="submit"
      >
        <i class="bi bi-person-plus-fill"></i>
        Tare Tipo Alumbrado
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
      :items="taskTypeLighting"
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
          @click="RemoveTaskTypeLighting(items.tareaTipoId)"
          class="m-1"
          variant="outline-danger"
          ><i class="bi bi-trash3"></i
        ></b-button>
        <b-button
          class="m-1"
          variant="outline-warning"
          :to="{
            name: 'TareaTipoAlumbrado-Edit',
            params: { TareaTipoAlumbradoId: items.tareaTipoId }
          }"
          ><i class="bi bi-pencil-square"></i
        ></b-button>
      </template>
    </EasyDataTable>
    <b-modal
      id="modal-tasktypelighting"
      title="Agregar Tipo de Tarea Alumbrado"
      size="xl"
      hide-footer
      button-size="lg"
      lazy
    >
      <Form @submit="addTaskTypeLighting">
        <b-row cols="2">
          <b-col>
            <b-form-group class="mt-3" label="Nombre">
              <Field name="NameField" :rules="validateName" as="text">
                <b-form-input
                  v-model="taskTypeLightingFields.nombreTarea"
                  :state="NameState"
                  onchange="validar"
                >
                </b-form-input>
              </Field>
              <ErrorMessage class="text-danger" name="NameField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Descripcion">
              <Field name="DescriptionField" :rules="validateDescription" as="text">
                <b-form-input
                  v-model="taskTypeLightingFields.descripcion"
                  :state="DescriptionState"
                ></b-form-input>
              </Field>
              <ErrorMessage class="text-danger" name="DescriptionField"></ErrorMessage>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            v-b-modal.modal-tasktypelighting
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
import TaskTypeLightingServices from '@/Services/tasktypelighting.Services'
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
    const { getTaskTypeLighting, createTaskTypeLighting, deleteTaskTypeLighting } = TaskTypeLightingServices()
    // const $toast = useToast()
    const taskTypeLighting = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    // const rows = ref(null)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('nombreTarea')
    const DescriptionState = ref(false)
    const NameState = ref(false)
    const taskTypeLightingFields = ref({
      tareaTipoId: 0,
      nombreTarea: null,
      descripcion: null,
      archivado: false
    })

    const taskTypeLightingFieldsBlank = ref(
      JSON.parse(JSON.stringify(taskTypeLightingFields))
    )
    const fields = ref([
      { value: 'tareaTipoId', text: 'ID', sortable: true },
      { value: 'nombreTarea', text: 'Nombre' },
      { value: 'descripcion', text: 'Descripcion' },
      { value: 'actions', text: 'Acciones' }
    ])

    const resetPublicLightingFields = () => {
      showModal.value = false
      taskTypeLightingFields.value = JSON.parse(
        JSON.stringify(taskTypeLightingFieldsBlank)
      )
      NameState.value = false
      DescriptionState.value = false
    }

    getTaskTypeLighting(data => {
      taskTypeLighting.value = data
      // rows.value = data.length
      if (taskTypeLighting.value.length > 0) {
        isloading.value = false
      } else {
        if (taskTypeLighting.value.length <= 0) {
          isloading.value = false
        }
      }
    })

    const onFiltered = filteredItems => {
      // rows.value = filteredItems.length
      currentPage.value = 1
    }

    const validateName = () => {
      if (!taskTypeLightingFields.value.nombreTarea) {
        NameState.value = false
        return 'Este campo es requerido'
      }
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(taskTypeLightingFields.value.nombreTarea)) {
        NameState.value = false
        return 'El nombre del tipo de tarea solo puede contener letras'
      }
      NameState.value = true
      return true
    }

    // const input = document.getTaskTypeLighting('inputName')
    // input.addTaskTypeLighting('input', function () {
    //   this.value = this.value.trim()
    // })
    // const validateDescription = () => {
    //   if (!statusLightingFields.value.descripcionDomicilio) {
    //     DescriptionState.value = false
    //     return 'Este campo es requerido'
    //   }
    //   DescriptionState.value = true
    //   return true
    // }

    const validateDescription = () => {
      if (!taskTypeLightingFields.value.descripcion) {
        DescriptionState.value = false
        return 'Este campo es requerido'
      }
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(taskTypeLightingFields.value.descripcion)) {
        DescriptionState.value = false
        return 'la descripcion del tipo de tarea solo puede contener letras'
      }
      DescriptionState.value = true
      return true
    }

    // function validar (taskTypeLightingFields) {
    //   if (taskTypeLightingFields.target.value.trim() === '') { alert('debe ingresar un valor en el campo') } else alert('ingreso ' + taskTypeLightingFields.target.value.trim() + ', es correcto!')
    // }

    // validar()

    // pone mis cambios de mis campos vacios de nuevo
    const refreshTable = () => {
      isloading.value = true
      getTaskTypeLighting(data => {
        taskTypeLighting.value = data
        // rows.value = data.length
        if (taskTypeLighting.value.length > 0) {
          isloading.value = false
        } else {
          if (taskTypeLighting.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }
    const addTaskTypeLighting = () => {
      createTaskTypeLighting(taskTypeLightingFields.value, data => {
        refreshTable()
        swal.fire({
          title: '¡Tipo de tarea registrado correctamente!',
          text: 'El tipo de tarea se ha registrado al sistema satisfactoriamente.',
          icon: 'success'
        })
        // $toast.success('Tarea tipo alumbrado registrado correctamente.', {
        //   position: 'top-right',
        //   duration: 1500
        // })
      })
      // resetStreetLightingFields()
      // taskTypeLightingFields.value = JSON.parse(
      //   JSON.stringify(taskTypeLightingFieldsBlank)
      // )
      showModal.value = false
      resetPublicLightingFields()
    }
    const RemoveTaskTypeLighting = TaskTypeLightingId => {
      isloading.value = true
      swal.fire({
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
            deleteTaskTypeLighting(TaskTypeLightingId, (data) => {
              refreshTable()
            })
            swal
              .fire({
                title: '¡Tipo de tarea archivado!',
                text: 'El tipo de tarea ha sido archivado satisfactoriamente .',
                icon: 'success'
              })
          } else {
            isloading.value = false
          }
        })
      // deleteTaskTypeLighting(TaskTypeLightingId, data => {
      //   refreshTable()
      // })
    }
    return {
      taskTypeLighting,
      taskTypeLightingFields,
      resetPublicLightingFields,
      perPage,
      currentPage,
      showModal,
      // rows,
      filter,
      perPageSelect,
      isloading,
      searchValue,
      searchField,
      taskTypeLightingFieldsBlank,
      // lightingFailuresValues,
      fields,
      NameState,
      DescriptionState,
      // option,
      onFiltered,
      addTaskTypeLighting,
      refreshTable,
      RemoveTaskTypeLighting,
      validateName,
      validateDescription
      // resetStreetLightingFields
    }
  }
}
</script>

<style>

</style>
