<template>
  <b-card class="m-2">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-card>
      <div>
        <h3>Editar control de vale</h3>
      </div>
      <Form @submit="onUpdateAddVoucherControl">
        <b-row cols="3">
          <!--Agregar Fecha emicion-->
          <b-col>
            <b-form-group class="mt-3" label="Fecha emicion">
              <Field name="DateOfIssueField" :rules="validateDateOfIssue" as="">
                <Datepicker
                  locale="es"
                  name="date"
                  text-input
                  v-model="voucherControl.fechaEmicion"
                  :state="DateOfIssueState"
                ></Datepicker>
              </Field>
              <ErrorMessage name="DateOfIssueField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--Agregar Fecha vigencia-->
          <b-col>
            <b-form-group class="mt-3" label="Fecha vigencia">
              <Field
                name="ExpirationDateField"
                :rules="validateExpirationDate"
                as=""
              >
                <Datepicker
                  locale="es"
                  name="date"
                  text-input
                  v-model="voucherControl.fechaVigencia"
                  :state="ExpirationDateState"
                ></Datepicker>
                <!-- @input="getAreas(voucherControlFields.departamentoId)" -->
              </Field>
              <ErrorMessage name="ExpirationDateField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--Agregar area-->
          <b-col>
            <b-form-group class="mt-3" label="Area">
              <Field name="AreaField" :rules="validateArea" as="number">
                <b-form-select
                  v-model="voucherControl.areaId"
                  autofocus
                  :options="areas"
                  value-field="areaId"
                  text-field="nombre"
                  :state="areaState"
                >
                  <!-- @input="getWorkStation(voucherControlFields.areaId)" -->
                </b-form-select>
              </Field>
              <ErrorMessage class="text-danger" name="AreaField"></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--Agregar Departamento-->
          <b-col>
            <b-form-group class="mt-3" label="Departamento: ">
              <Field
                name="DepartamentField"
                :rules="validateDepartament"
                as="number"
              >
                <b-form-select
                  v-model="voucherControl.departamentoId"
                  autofocus
                  :options="departaments"
                  value-field="departamentoId"
                  text-field="nombre"
                  :state="departamentState"
                  @input="getAreas(EmployeesFields.departamentoId)"
                >
                </b-form-select>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="DepartamentField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--Agregar Empleado-->
          <b-col>
            <b-form-group class="mt-3" label="Empleado: ">
              <Field name="EmployeeField" :rules="validateEmployee" as="text">
                <b-form-select
                  v-model="voucherControl.empleadoId"
                  autofocus
                  :options="employees"
                  value-field="empleadoId"
                  text-field="nombreCompleto"
                  :state="EmployeeState"
                >
                </b-form-select>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="EmployeeField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--Agregar Proveedor-->
          <b-col>
            <b-form-group class="mt-3" label="Nombre proveedor: ">
              <Field name="ProviderField" :rules="validateProvider" as="text">
                <b-form-select
                  v-model="voucherControl.proveedorId"
                  autofocus
                  :options="provider"
                  value-field="proveedorId"
                  text-field="nombre"
                  :state="ProviderState"
                >
                </b-form-select>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="ProviderField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--Agregar Estatus vale-->
          <b-col>
            <b-form-group class="mt-3" label="Estatus vale: ">
              <Field
                name="StatusVoucherField"
                :rules="validateStatusVoucher"
                as="text"
              >
                <b-form-select
                  v-model="voucherControl.estatusValeId"
                  autofocus
                  :options="statusVoucher"
                  value-field="estatusValeId"
                  text-field="nombre"
                  :state="StatusVoucherState"
                >
                </b-form-select>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="StatusVoucherField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--Agregar Tipo-->
          <b-col>
            <b-form-group class="mt-3" label="Tipo vale: ">
              <Field
                name="TypeVoucherField"
                :rules="validateTypeVoucher"
                as="text"
              >
                <b-form-select
                  v-model="voucherControl.tipoId"
                  autofocus
                  :options="typeVoucher"
                  value-field="tipoId"
                  text-field="nombre"
                  :state="TypeVoucherState"
                >
                </b-form-select>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="TypeVoucherField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
        </b-row>
        <!-- <ProductPrescription  /> -->

        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            to="/ServiciosPublicos/ControlVale/list"
          >
            Cancelar
          </b-button>
          <b-button class="w-auto m-2" variant="success" type="submit"
            >Guardar</b-button
          >
        </b-row>
      </Form>
    </b-card>
  </b-card>
</template>

<script>
import VoucherControlServices from '@/Services/vouchercontrol.Services'
import EmployeeServices from '@/Services/employee.Services'
import AreaServices from '@/Services/area.Services'
import DepartamentServices from '@/Services/departament.Services'
import ProviderServices from '@/Services/provider.Services'
import StatusVoucherServices from '@/Services/statusvoucher.Services'
import TypeVoucherServices from '@/Services/typevoucher.Services'
import { Field, Form, ErrorMessage } from 'vee-validate'
import { ref, inject } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import Datepicker from '@vuepic/vue-datepicker'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    Datepicker,
    Field,
    Form,
    ErrorMessage
  },
  setup () {
    const swal = inject('$swal')
    const { getVoucherControlById, updateVoucherControl } = VoucherControlServices()
    const { getEmployees } = EmployeeServices()
    const { getAreas } = AreaServices()
    const { getDepartaments } = DepartamentServices()
    const { getProvider } = ProviderServices()
    const { getStatusVoucher } = StatusVoucherServices()
    const { getTypeVoucher } = TypeVoucherServices()
    const voucherControl = ref([])
    const employees = ref([])
    const areas = ref([])
    const departaments = ref([])
    const provider = ref([])
    const statusVoucher = ref([])
    const typeVoucher = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const DateOfIssueState = ref(false)
    const ExpirationDateState = ref(false)
    const departamentState = ref(false)
    const areaState = ref(false)
    const EmployeeState = ref(false)
    const ProviderState = ref(false)
    const ProductState = ref(false)
    const DetailVoucherState = ref(false)
    const StatusVoucherState = ref(false)
    const TypeVoucherState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Control de vale', to: '/ServiciosPublicos/ControlVale/list' },
      { text: 'Editar-Control de vale' }
    ])

    getVoucherControlById(router.params.ControlValeId, data => {
      voucherControl.value = data
      validateState()
    })

    getAreas(data => {
      areas.value = data
      if (data.length === 0) {
        swal.fire({
          title: 'No se encuentran areas registradas!',
          text: 'No se encuentran areas registradas en el sistema, registre primero un departamento para continuar.',
          icon: 'warning'
        })
      }
    })

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

    getEmployees(data => {
      employees.value = data
      if (data.length === 0) {
        swal.fire({
          title: 'No se encuentran empleados registrados!',
          text: 'No se encuentran  empleados registrados en el sistema, registre primero un empleado para continuar.',
          icon: 'warning'
        })
      }
    })

    getProvider(data => {
      provider.value = data
      if (data.length === 0) {
        swal.fire({
          title: 'No se encuentran proveedores registrados!',
          text: 'No se encuentran  proveedores registrados en el sistema, registre primero un proveedor para continuar.',
          icon: 'warning'
        })
      }
    })

    getStatusVoucher(data => {
      statusVoucher.value = data
      if (data.length === 0) {
        swal.fire({
          title: 'No se encuentra el estatus del vale registrado!',
          text: 'No se encuentran estatus de vales registrados en el sistema, registre primero un estatus de vale para continuar.',
          icon: 'warning'
        })
      }
    })

    getTypeVoucher(data => {
      typeVoucher.value = data
      if (data.length === 0) {
        swal.fire({
          title: 'No se encuentra el tipo de vale registrado!',
          text: 'No se encuentran el tipo de vale registrado en el sistema, registre primero un tipo de vale para continuar.',
          icon: 'warning'
        })
      }
    })

    const onUpdateAddVoucherControl = () => {
      updateVoucherControl(voucherControl.value, data => {})
      swal
        .fire({
          title: '¡Control de vale modificado correctamente!',
          text: 'El control de vale se ha modificado  satisfactoriamente.',
          icon: 'success'
        })
        .then(result => {
          if (result.isConfirmed) {
            redirect.push('/ServiciosPublicos/ControlVale/list')
          }
        })
    }

    const validateDateOfIssue = () => {
      if (!voucherControl.value.fechaEmicion) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }

    const validateExpirationDate = () => {
      if (!voucherControl.value.fechaVigencia) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }

    const validateDepartament = () => {
      if (!voucherControl.value.departamentoId) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }

    const validateArea = () => {
      if (!voucherControl.value.areaId) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }

    const validateEmployee = () => {
      if (!voucherControl.value.empleadoId) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }

    const validateProvider = () => {
      if (!voucherControl.value.proveedorId) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }

    const validateStatusVoucher = () => {
      if (!voucherControl.value.estatusValeId) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }

    const validateTypeVoucher = () => {
      if (!voucherControl.value.tipoId) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }

    const validateState = () => {
      // eslint-disable-next-line no-unneeded-ternary
      DateOfIssueState.value = voucherControl.value.fechaEmicion !== ''
      // eslint-disable-next-line no-unneeded-ternary
      ExpirationDateState.value = voucherControl.value.fechaVigencia !== ''
      // eslint-disable-next-line no-unneeded-ternary
      departamentState.value = voucherControl.value.departamentoId !== ''
      // eslint-disable-next-line no-unneeded-ternary
      areaState.value = voucherControl.value.areaId !== ''
      // eslint-disable-next-line no-unneeded-ternary
      EmployeeState.value = voucherControl.value.empleadoId !== ''
      // eslint-disable-next-line no-unneeded-ternary
      ProviderState.value = voucherControl.value.proveedorId !== ''
      // eslint-disable-next-line no-unneeded-ternary
      StatusVoucherState.value = voucherControl.value.estatusValeId !== ''
      // eslint-disable-next-line no-unneeded-ternary
      TypeVoucherState.value = voucherControl.value.tipoId !== ''
    }

    return {
      voucherControl,
      breadcrumbItems,
      DateOfIssueState,
      areas,
      employees,
      departaments,
      provider,
      statusVoucher,
      typeVoucher,
      ExpirationDateState,
      departamentState,
      areaState,
      EmployeeState,
      ProviderState,
      ProductState,
      DetailVoucherState,
      StatusVoucherState,
      TypeVoucherState,
      router,

      onUpdateAddVoucherControl,
      validateDateOfIssue,
      validateExpirationDate,
      validateDepartament,
      validateArea,
      validateEmployee,
      validateProvider,
      validateStatusVoucher,
      validateTypeVoucher,
      validateState
      // validateTask,
      // validateProblem,
      // validateNameWork,
      // validateAddresdescription,
      // validateStatus,
      // validateDomicile
    }
  }
}
</script>

<style></style>
