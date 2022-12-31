<template>
  <b-card class="m-2">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-card>
      <div>
        <h3>Editar Inmueble</h3>
      </div>
      <b-tabs>
        <b-tab
          title="Datos generales"
        >
          <Form @submit="onUpdateWorkStation()">
            <b-row cols="3">
              <b-col>
                <b-form-group class="mt-3" label="Nomenclatura">
                  <Field name="FolioField" :rules="validateFolio" as="text">
                    <b-form-input
                      v-model="furniture.codigoInventario"
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
                      v-model="furniture.tipoMobiliarioId"
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
                      v-model="furniture.departamentoId"
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
                      v-model="furniture.descripción"
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
                  <Field name="DateField" :rules="validateDate" as="number">
                    <Datepicker
                      v-model="furniture.fechaAdquisición"
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
                  <Field name="CostField" :rules="validateCost" as="number">
                    <b-form-input
                      v-model="furniture.costo"
                      :state="costState"
                      type="number"
                    />
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
                to="/PatrimonioMunicipal/Mobiliarios/list"
              >
                Cancelar
              </b-button>
              <b-button class="w-auto m-2" variant="success" type="submit">
                Guardar
              </b-button>
            </b-row>
          </Form>
        </b-tab>
        <b-tab
          title="Documentos"
        >
          <furnitureDocuments :MobiliarioId="mobiliarioId"/>
        </b-tab>
      </b-tabs>
    </b-card>
  </b-card>
</template>

<script>
import FurnitureServices from '@/Services/furniture.Services'
import DepartamentServices from '@/Services/departament.Services'
import furnitureDocuments from './Documents/FurnitureDocuments.vue'
import { ref, inject } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import Datepicker from '@vuepic/vue-datepicker'
import { Form, Field, ErrorMessage } from 'vee-validate'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    Form,
    Field,
    ErrorMessage,
    Datepicker,
    furnitureDocuments
  },
  setup () {
    const swal = inject('$swal')
    const { getFurniture, updateFurniture, getTypeFurnitures } =
      FurnitureServices()
    const { getDepartaments } = DepartamentServices()
    const furniture = ref([])
    const typeFurnitures = ref([])
    const departaments = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const typeFurnitureState = ref(false)
    const descriptionState = ref(false)
    const folioState = ref(false)
    const dateState = ref(false)
    const costState = ref(false)
    const departamentState = ref(false)
    const mobiliarioId = ref(parseInt(router.params.MobiliarioId))
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Mobiliarios', to: '/PatrimonioMunicipal/Mobiliarios/list' },
      { text: 'Editar mobiliario' }
    ])
    const onUpdateWorkStation = () => {
      updateFurniture(furniture.value, data => {
        swal
          .fire({
            title: 'Inmueble modificado correctamente!',
            text: 'El inmueble se ha modificado  satisfactoriamente.',
            icon: 'success'
          })
          .then(result => {
            if (result.isConfirmed) {
              redirect.push('/PatrimonioMunicipal/Mobiliarios/list')
            }
          })
      })
    }
    getDepartaments(data => {
      departaments.value = data
      if (data.length === 0) {
        swal.fire({
          title: 'No se encuentran departamentos registrados!',
          text: 'No se encuentran departamentos registrados en el sistema, registre primero un departamento para continuar.',
          icon: 'error'
        })
      }
    })
    getFurniture(router.params.MobiliarioId, data => {
      furniture.value = data
      validateState()
    })
    getTypeFurnitures(data => {
      typeFurnitures.value = data
    })
    // VALIDATIONS
    const validateFolio = () => {
      if (!furniture.value.codigoInventario) {
        validateState()
        return 'Este campo es requerido'
      }
      // eslint-disable-next-line no-useless-escape
      if (
        !/^(?=.*\d)(?=.*[a-zA-Z])([A-ZñÑáéíóúÁÉÍÓÚ])[A-Z0-9]+$/i.test(
          furniture.value.codigoInventario
        )
      ) {
        validateState()
        return 'El nombre del area solo puede contener letras'
      }
      validateState()
      return true
    }
    const validateTypeFurniture = () => {
      if (!furniture.value.tipoMobiliarioId) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateDepartament = () => {
      if (!furniture.value.departamentoId) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateDescription = () => {
      if (!furniture.value.descripción) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateDate = () => {
      if (!furniture.value.fechaAdquisición) {
        dateState.value = false
        return 'Este campo es requerido'
      }
      dateState.value = true
      return true
    }
    const validateCost = () => {
      if (!furniture.value.costo) {
        costState.value = false
        return 'Este campo es requerido'
      }
      costState.value = true
      return true
    }
    const validateState = () => {
      departamentState.value = furniture.value.departamentoId !== null
      typeFurnitureState.value = furniture.value.tipoMobiliarioId !== null
      descriptionState.value =
        furniture.value.descripción !== null &&
        furniture.value.descripción !== ''
      folioState.value =
        furniture.value.codigoInventario !== null &&
        /^(?=.*\d)(?=.*[a-zA-Z])([A-ZñÑáéíóúÁÉÍÓÚ])[A-Z0-9]+$/i.test(
          furniture.value.codigoInventario
        )
      dateState.value = furniture.value.fechaAdquisición !== null
      costState.value =
        furniture.value.costo !== null && furniture.value.costo !== ''
      return ''
    }
    return {
      furniture,
      typeFurnitures,
      breadcrumbItems,
      departaments,
      folioState,
      departamentState,
      typeFurnitureState,
      descriptionState,
      dateState,
      costState,
      mobiliarioId,
      //   router

      onUpdateWorkStation,
      validateDepartament,
      validateTypeFurniture,
      validateDescription,
      validateState,
      validateFolio,
      validateDate,
      validateCost
    }
  }
}
</script>

<style>
:root {
  --vs-search-input-color: #eeeeee;
  --vs-controls-color: #664cc3;
  --vs-dropdown-bg: #ffffff;
  --vs-selected-bg: #7367f0;
  --vs-selected-color: #eeeeee;
  --vs-dropdown-bg: #ffffff;
  --vs-dropdown-color: #7367f0;
  --vs-dropdown-option-color: #7367f0;
}
</style>
