<template>
  <b-card class="m-2">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-card>
      <div>
        <h3>Editar Inmueble</h3>
      </div>
      <Form @submit="onUpdateWorkStation()">
         <b-row cols="3">
          <b-col>
            <b-form-group class="mt-3" label="Tipo de inmueble">
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
              <ErrorMessage class="text-danger" name="typeFurnitureField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Descripción">
              <Field
                name="DescriptionField"
                :rules="validateDescription"
                as="number"
              >
                <b-form-input
                  v-model="furniture.descripción"
                  :state="descriptionState"
                />
              </Field>
              <ErrorMessage class="text-danger" name="DescriptionField"></ErrorMessage>
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
                  @input="getAreas(furnituresFields.departamentoId)"
                >
                </b-form-select>
              </Field>
              <ErrorMessage class="text-danger" name="DepartamentField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Area">
              <Field
                name="AreaField"
                :rules="validateArea"
                as="number"
              >
                <b-form-select
                  v-model="furniture.areaId"
                  autofocus
                  :options="areas"
                  value-field="areaId"
                  text-field="nombre"
                  :state="areaState"
                >
                </b-form-select>
              </Field>
              <ErrorMessage class="text-danger" name="AreaField"></ErrorMessage>
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
    </b-card>
  </b-card>
</template>

<script>
import FurnitureServices from '@/Services/furniture.Services'
import AreasServices from '@/Services/area.Services'
import DepartamentServices from '@/Services/departament.Services'
import { ref, inject } from 'vue'
import { useRoute, useRouter } from 'vue-router'
// import { useToast } from 'vue-toast-notification'
import { Form, Field, ErrorMessage } from 'vee-validate'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    Form,
    Field,
    ErrorMessage
  },
  setup () {
    const swal = inject('$swal')
    const { getTypeFurniture, updateTypeFurniture, getTypeFurnitures } = FurnitureServices()
    const { getAreasByDepartament } = AreasServices()
    const { getDepartaments } = DepartamentServices()
    const areas = ref([])
    const furniture = ref([])
    const typeFurnitures = ref([])
    const departaments = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const nameState = ref(false)
    const areaState = ref(false)
    const departamentState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Puestos de Trabajo', to: '/PuestosTrabajos/list' },
      { text: 'Editar-Puesto de trabajo' }
    ])
    const onUpdateWorkStation = () => {
      updateTypeFurniture(furniture.value, data => {
        swal.fire({
          title: 'Puesto de trabajo modificado correctamente!',
          text: 'El Puesto de trabajo se ha modificado  satisfactoriamente.',
          icon: 'success'
        }).then(result => {
          if (result.isConfirmed) {
            redirect.push('/PuestosTrabajos/list')
          }
        })
      })
    }
    getTypeFurnitures(data => {
      typeFurnitures.value = data
    })
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
    const getAreas = (departamentoId) => {
      getAreasByDepartament(departamentoId, data => {
        areas.value = data
        if (data.length === 0) {
          swal.fire({
            title: 'No se encuentran areas registradas!',
            text: 'No se encuentran areas registradas en el departamento seleccionado, registre primero una area para continuar.',
            icon: 'error'
          })
        }
      })
    }
    getTypeFurniture(router.params.MobiliarioId, data => {
      furniture.value = data
      nameState.value = data.nombre !== null
      departamentState.value = data.departamentoId !== null
      areaState.value = data.areaId !== null
      getAreas(data.departamentoId)
    })
    const validateName = () => {
      if (!furniture.value.nombre) {
        nameState.value = false
        return 'Este campo es requerido'
      }
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(furniture.value.nombre)) {
        nameState.value = false
        return 'El nombre solo puede contener letras'
      }
      nameState.value = true
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
    const validateArea = () => {
      if (!furniture.value.areaId) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateState = () => {
      departamentState.value = furniture.value.departamentoId !== null
      areaState.value = furniture.value.areaId !== null
      return ''
    }
    return {
      furniture,
      typeFurnitures,
      breadcrumbItems,
      areas,
      departaments,
      departamentState,
      areaState,
      nameState,
      //   router

      onUpdateWorkStation,
      validateName,
      validateState,
      validateDepartament,
      validateArea,
      getAreas
    }
  }
}
</script>

<style></style>
