<template>
  <b-card class="m-2">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-card>
      <div>
        <h3>Expediente Alumbrado</h3>
      </div>
      <b-tabs content-class="mt-3">
        <b-tab title="Datos generales" active>
          <b-card>
            <Form @submit="onUpdatePublicWork">
              <b-row cols="2">
                <!--Agregar nombre de obra-->
                <b-col>
                  <b-form-group class="mt-3" label="Nombre obra">
                    <Field
                      name="NameWorksField"
                      :rules="validateNameWorks"
                      as="text"
                    >
                      <b-form-input
                        v-model="publicWorks.nombre"
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
                      :state="NameWorksState"
                    >
                    </b-form-checkbox>
                  </b-form-group>
                </b-col>
                <!--Agregar encargado de obra-->
                <b-col v-if="isAgency == false">
                  <b-form-group class="mt-3" label="Encargado obra">
                    <b-form-input v-model="publicWorks.encargado">
                    </b-form-input>
                  </b-form-group>
                </b-col>
                <!--Agregar operador de obra-->
                <b-col v-if="isAgency == false">
                  <b-form-group class="mt-3" label="Operador de la obra">
                    <b-form-input v-model="publicWorks.operadorObra">
                    </b-form-input>
                  </b-form-group>
                </b-col>
                <!--Agregar operador de vehiculo-->
                <b-col v-if="isAgency == false">
                  <b-form-group class="mt-3" label="Operador del vehiculo">
                    <b-form-input v-model="publicWorks.operadorVehiculo">
                    </b-form-input>
                  </b-form-group>
                </b-col>
                <!--Agregar nombre agencia-->
                <b-col v-if="isAgency == false">
                  <b-form-group class="mt-3" label="Nombre agencia">
                    <!-- <Field
                      name="AgencyNameField"
                      :rules="validateAgencyName"
                      as="text"
                    > -->
                    <b-form-input v-model="publicWorks.agencia"> </b-form-input>
                    <!-- </Field>
                    <ErrorMessage
                      class="text-danger"
                      name="AgencyNameField"
                    ></ErrorMessage> -->
                  </b-form-group>
                </b-col>
                <!--Agregar representante legal-->
                <b-col v-if="isAgency == false">
                  <b-form-group class="mt-3" label="Representante legal">
                    <b-form-input v-model="publicWorks.representante">
                    </b-form-input>
                  </b-form-group>
                </b-col>
                <!--Agregar RFC-->
                <b-col v-if="isAgency == false">
                  <b-form-group class="mt-3" label="RFC">
                    <b-form-input v-model="publicWorks.rFC">
                    </b-form-input>
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
                        v-model="publicWorks.estatusObraId"
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
                        v-model="publicWorks.descripcion"
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
                <b-alert variant="warning" dismissible show
                  >Arrastra el punto del mapa al lugar donde se encuentra su
                  gabeta.</b-alert
                >
                <b-card>
                  <GMapMap
                    :center="center"
                    map-type-id="satellite"
                    :zoom="20"
                    :options="{
                      zoomControl: true,
                      mapTypeControl: false,
                      scaleControl: false,
                      rotateControl: true,
                      disableDefaultUi: false
                    }"
                    style="width: 100%; height: 500px"
                  >
                    <GMapMarker
                      :zoom="10"
                      :position="center"
                      :draggable="true"
                      @drag="updateCoordinates"
                    />
                  </GMapMap>
                </b-card>
              </b-row>
              <b-row align-h="end">
                <b-button
                  class="w-auto m-2 text-white"
                  variant="primary"
                  to="/ServiciosPublicos/ObrasPublicas/list"
                >
                  Cancelar
                </b-button>
                <b-button class="w-auto m-2" variant="success" type="submit"
                  >Guardar</b-button
                >
              </b-row>
            </Form>
          </b-card>
        </b-tab>
        <b-tab title="Empleados">
          <publicWorksEmployeeService :ObraId="obraId" />
        </b-tab>
        <b-tab title="Vehiculos">
          <PublicWorkVehicle :ObraId="obraId" />
        </b-tab>
        <b-tab title="Documentos">
          <ExpedientDocumentsPublickWorks :ObraId="obraId" />
        </b-tab>
      </b-tabs>
    </b-card>
  </b-card>
</template>

<script>
import PublicWorksServices from '@/Services/publickworks.Services'
import worksStatusServices from '@/Services/worksstatus.Services'
import publicWorksEmployeeService from '@/views/ServiciosPublicos/publicworks/publicworksemployee/PublicWorksEmployeeList.vue'
import ExpedientDocumentsPublickWorks from '@/views/ServiciosPublicos/publicworks/publicWorks/DocumentsPublicworks.vue'
import PublicWorkVehicle from '@/views/ServiciosPublicos/publicworks/publickworkvehicles/PublickWorksVehiclesList.vue'
import { Field, Form, ErrorMessage } from 'vee-validate'
import { ref, inject } from 'vue'
import { useRoute, useRouter } from 'vue-router'
// import Datepicker from '@vuepic/vue-datepicker'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    // Datepicker,
    Field,
    Form,
    ErrorMessage,
    publicWorksEmployeeService,
    ExpedientDocumentsPublickWorks,
    PublicWorkVehicle
  },
  setup (props) {
    const swal = inject('$swal')
    const { getPublicWorksById, updatePublicWorks } = PublicWorksServices()
    const { getWorksStatus } = worksStatusServices()
    const publicWorks = ref([])
    const worksStatus = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const obraId = ref(parseInt(router.params.ObraId))
    const NameWorksState = ref(false)
    const DescriptionState = ref(false)
    const WorkStatusState = ref(false)
    // const AgencyNameState = ref(false)
    const isAgency = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Obras publicas', to: '/ServiciosPublicos/ObrasPublicas/list' },
      { text: 'Editar-Obras publicas' }
    ])

    getPublicWorksById(router.params.ObraId, data => {
      publicWorks.value = data
      center.value.lat = data.latitud
      center.value.lng = data.longitud
      validateState()
    })

    const onUpdatePublicWork = () => {
      updatePublicWorks(publicWorks.value, data => {
        swal
          .fire({
            title: '¡Obras publicas modificado correctamente!',
            text: 'Las obras publicas se ha modificado  satisfactoriamente.',
            icon: 'success'
          })
          .then(result => {
            redirect.push('/ServiciosPublicos/ObrasPublicas/list')
          })
      })
    }

    getWorksStatus(data => {
      publicWorks.value = data
      if (data.length === 0) {
        swal.fire({
          title: 'No se encuentra un estatus_OP registrado!',
          text: 'No se encuentra estatus_OP registrado en el departamento seleccionado, registre primero un tipo de estatus_OP para continuar',
          icon: 'warning'
        })
      }
    })

    const center = ref({ lat: 0, lng: 0 })

    const updateCoordinates = location => {
      publicWorks.value.latitud = location.latLng.lat()
      publicWorks.value.longitud = location.latLng.lng()
    }

    const validateNameWorks = () => {
      if (!publicWorks.value.nombre) {
        validateState()
        return 'Este campo es requerido'
      }
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ 0-9]+$/i.test(publicWorks.value.nombre)) {
        NameWorksState.value = false
        return 'El nombre de la obra solo puede contener letras'
      }
      if (!publicWorks.value.nombre.trim().length > 0) {
        NameWorksState.value = false
        return 'Este campo no puede contener espacios'
      }
      validateState()
      return true
    }

    const validateWorkStatus = () => {
      if (!publicWorks.value.estatusObraId) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }

    const validateDescription = () => {
      if (!publicWorks.value.descripcion) {
        validateState()
        return 'Este campo es requerido'
      }
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(publicWorks.value.descripcion)) {
        DescriptionState.value = false
        return 'La descripcion solo puede contener letras y numeros'
      }
      if (!publicWorks.value.descripcion.trim().length > 0) {
        DescriptionState.value = false
        return 'Este campo no puede contener espacios'
      }
      validateState()
      return true
    }

    const validateState = () => {
      NameWorksState.value =
        publicWorks.value.nombre !== '' || publicWorks.value.nombre !== null
      DescriptionState.value =
        publicWorks.value.descripcion !== '' ||
        publicWorks.value.descripcion !== null
      WorkStatusState.value =
        publicWorks.value.estatusObraId !== '' ||
        publicWorks.value.estatusObraId !== null
    }

    return {
      publicWorks,
      worksStatus,
      obraId,
      breadcrumbItems,
      router,
      NameWorksState,
      DescriptionState,
      WorkStatusState,
      isAgency,
      center,

      onUpdatePublicWork,
      updateCoordinates,
      validateNameWorks,
      validateWorkStatus,
      validateDescription
    }
  }
}
</script>

<style scoped>
.form-check {
  display: inline-block;
}
</style>
