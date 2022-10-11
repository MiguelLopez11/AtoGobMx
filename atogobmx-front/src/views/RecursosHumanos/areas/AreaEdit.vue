<template>
  <b-card class="m-3">
    <b-card class="mb-4">
      <b-breadcrumb
        class="p-0"
        :items="breadcrumbItems"
      >
      </b-breadcrumb>
    </b-card>
    <b-card>
      <div>
        <h3>Editar Area de trabajo</h3>
      </div>
      <Form @submit="onUpdateArea()">
        <b-row cols="3">
          <b-col>
            <b-form-group class="mt-3" label="Nombre de area">
              <Field name="NameField" :rules="validateArea" as="text">
                <b-form-input v-model="area.nombre" :state="nameState">
                </b-form-input>
              </Field>
              <ErrorMessage class="text-danger" name="NameField"></ErrorMessage>
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
                  v-model="area.departamentoId"
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
              <b-form-input v-model="area.descripcion" />
            </b-form-group>
          </b-col>
        </b-row>
        <b-row align-h="end">
          <b-button
            class="col-1 m-2 text-white"
            variant="primary"
            to="/areas/list"
            type="reset"
            >Cancelar</b-button
          >
          <b-button class="col-1 m-2" type="suceess" variant="success"
            >Guardar</b-button
          >
        </b-row>
      </Form>
    </b-card>
  </b-card>
</template>

<script>
import AreaServices from '@/Services/area.Services'
import { ref, inject } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { Form, Field, ErrorMessage } from 'vee-validate'
import DepartamentServices from '@/Services/departament.Services'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    Form,
    Field,
    ErrorMessage
  },
  setup () {
    const swal = inject('$swal')
    const { getArea, updateArea } = AreaServices()
    const { getDepartaments } = DepartamentServices()
    const redirect = useRouter()
    const departaments = ref([])
    const area = ref([])
    const router = useRoute()
    const nameState = ref(false)
    const departamentState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Areas', to: '/Areas/list' },
      { text: 'Editar-Area' }
    ])
    const onUpdateArea = () => {
      updateArea(area.value, data => {
        swal.fire({
          title: '¡Area de trabajo modificada correctamente!',
          text: 'El area de trabajo se ha modificado  satisfactoriamente.',
          icon: 'success'
        }).then(result => {
          if (result.isConfirmed) {
            redirect.push('/Areas/list')
          }
        })
      })
    }
    getDepartaments(data => {
      departaments.value = data
    })
    getArea(router.params.AreaId, data => {
      area.value = data
    })
    const validateArea = () => {
      if (!area.value.nombre) {
        validateState()
        return 'Este campo es requerido'
      }
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(area.value.nombre)) {
        nameState.value = false
        return 'El nombre solo puede contener letras'
      }
      validateState()
      return true
    }
    const validateDepartament = () => {
      if (!area.value.departamentoId) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }
    const validateState = () => {
      nameState.value = area.value.nombre !== ''
      departamentState.value = area.value.departamentoId !== 0
      return ''
    }
    return {
      departaments,
      area,
      breadcrumbItems,
      //   router
      departamentState,

      validateDepartament,
      onUpdateArea,
      validateArea,
      validateState,
      nameState
    }
  }
}
</script>

<style></style>
