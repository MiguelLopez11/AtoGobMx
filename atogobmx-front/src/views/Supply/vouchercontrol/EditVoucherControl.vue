<template>
  <b-card class="m-2">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-tabs content-class="mt-3">
      <b-tab title="Editar control de vale" active>
        <b-card>
          <Form @submit="onUpdateAddVoucherControl">
            <b-row cols="4">
              <!--Agregar Fecha emicion-->
              <b-col>
                <b-form-group class="mt-3" label="Fecha emicion">
                  <Field
                    name="DateOfIssueField"
                    :rules="validateDateOfIssue"
                    as=""
                  >
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
              <!--Agregar usuario proveeduria-->
              <b-col>
                <b-form-group class="mt-3" label="Usuario">
                  <Field name="ColorField" :rules="validateEmployee" as="text">
                    <b-form-select
                      v-model="voucherControl.usuario"
                      autofocus
                      :state="employeeState"
                      value-field="nombreCompleto"
                      text-field="nombreCompleto"
                      :options="employeesProvider"
                    >
                    </b-form-select>
                  </Field>
                  <ErrorMessage
                    class="text-danger"
                    name="ColorField"
                  ></ErrorMessage>
                </b-form-group>
              </b-col>
              <!--Agregar usuario autorizado-->
              <b-col>
                <b-form-group class="mt-3" label="Usuario autorizador">
                  <Field
                    name="ColorField"
                    :rules="validateUserAuthoriser"
                    as="text"
                  >
                    <b-form-select
                      v-model="voucherControl.usuarioAutoriza"
                      autofocus
                      :state="employeeAuthoriserState"
                      value-field="nombreCompleto"
                      text-field="nombreCompleto"
                      :options="employeesProvider"
                    >
                    </b-form-select>
                  </Field>
                  <ErrorMessage
                    class="text-danger"
                    name="ColorField"
                  ></ErrorMessage>
                </b-form-group>
              </b-col>
              <!--Agregar dependencia-->
              <b-col>
                <b-form-group class="mt-3" label="Dependencia">
                  <Field
                    name="DependencyField"
                    :rules="validateDependency"
                    as="text"
                  >
                    <b-form-input
                      v-model="voucherControl.dependencia"
                      :state="DependencyState"
                    >
                    </b-form-input>
                  </Field>
                  <ErrorMessage
                    class="text-danger"
                    name="DependencyField"
                  ></ErrorMessage>
                </b-form-group>
              </b-col>
              <!--Agregar subprograma-->
              <b-col>
                <b-form-group class="mt-3" label="Subprograma">
                  <Field name="AppletField" :rules="validateApplet" as="text">
                    <b-form-input
                      v-model="voucherControl.subprograma"
                      :state="AppletState"
                    >
                    </b-form-input>
                  </Field>
                  <ErrorMessage
                    class="text-danger"
                    name="AppletField"
                  ></ErrorMessage>
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
              <!-- <b-col>
                <b-form-group class="mt-3" label="Empleado: ">
                  <Field
                    name="EmployeeField"
                    :rules="validateEmployee"
                    as="text"
                  >
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
              </b-col> -->
              <!--Agregar Proveedor-->
              <b-col>
                <b-form-group class="mt-3" label="Nombre proveedor: ">
                  <Field
                    name="ProviderField"
                    :rules="validateProvider"
                    as="text"
                  >
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
          </Form>
          <b-card class="mb-4">
            <product-voucher-service :ControlValeId="controlValeId" />
          </b-card>
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
        </b-card>
      </b-tab>
      <b-tab title="Vehiculos">
        <SupplyVehicles :ControlValeId="controlValeId" />
      </b-tab>
    </b-tabs>
  </b-card>
</template>

<script>
import VoucherControlServices from '@/Services/vouchercontrol.Services'
import EmployeeServices from '@/Services/employee.Services'
import DepartamentServices from '@/Services/departament.Services'
import ProviderServices from '@/Services/provider.Services'
import StatusVoucherServices from '@/Services/statusvoucher.Services'
import TypeVoucherServices from '@/Services/typevoucher.Services'
import ProductVoucherService from '@/views/Supply/detailvoucher/DetailVoucherList.vue'
import SupplyVehicles from '@/views/Supply/supplyvehicle/SupplyVehicleList.vue'
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
    ErrorMessage,
    ProductVoucherService,
    SupplyVehicles
  },
  setup () {
    const swal = inject('$swal')
    const { getVoucherControlById, updateVoucherControl } =
      VoucherControlServices()
    const { getEmployees, getEmployeesProvider } = EmployeeServices()
    const { getDepartaments } = DepartamentServices()
    const { getProvider } = ProviderServices()
    const { getStatusVoucher } = StatusVoucherServices()
    const { getTypeVoucher } = TypeVoucherServices()
    const voucherControl = ref([])
    const employees = ref([])
    const departaments = ref([])
    const provider = ref([])
    const statusVoucher = ref([])
    const typeVoucher = ref([])
    const employeesProvider = ref([])
    const router = useRoute()
    const redirect = useRouter()
    const DateOfIssueState = ref(false)
    const ExpirationDateState = ref(false)
    const departamentState = ref(false)
    const employeeState = ref(false)
    const ProviderState = ref(false)
    const ProductState = ref(false)
    const DetailVoucherState = ref(false)
    const StatusVoucherState = ref(false)
    const TypeVoucherState = ref(false)
    const DependencyState = ref(false)
    const AppletState = ref(false)
    const employeeAuthoriserState = ref(false)
    const controlValeId = ref(parseInt(router.params.ControlValeId))
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Control de vale', to: '/ServiciosPublicos/ControlVale/list' },
      { text: 'Editar-Control de vale' }
    ])

    getVoucherControlById(router.params.ControlValeId, data => {
      voucherControl.value = data
      validateState()
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

    getEmployeesProvider(data => {
      employeesProvider.value = data
      if (data.length === 0) {
        swal.fire({
          title: '¡No se encuentran empleados!',
          text: 'Registre un empleado del area de proveeduria para autorizacion',
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

    // const validateEmployee = () => {
    //   if (!voucherControl.value.empleadoId) {
    //     validateState()
    //     return 'Este campo es requerido'
    //   }
    //   validateState()
    //   return true
    // }

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

    const validateEmployee = () => {
      if (!voucherControl.value.usuario) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }

    const validateUserAuthoriser = () => {
      if (!voucherControl.value.usuarioAutoriza) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }

    const validateDependency = () => {
      if (!voucherControl.value.dependencia) {
        validateState()
        return 'Este campo es requerido'
      }
      if (!voucherControl.value.dependencia.trim().length > 0) {
        DependencyState.value = false
        return 'Este campo no puede contener solo espacios'
      }
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(voucherControl.value.dependencia)) {
        DependencyState.value = false
        return 'El nombre solo puede contener letras'
      }
      validateState()
      return true
    }

    const validateApplet = () => {
      if (!voucherControl.value.subprograma) {
        validateState()
        return 'Este campo es requerido'
      }
      if (!voucherControl.value.subprograma.trim().length > 0) {
        AppletState.value = false
        return 'Este campo no puede contener solo espacios'
      }
      if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(voucherControl.value.subprograma)) {
        AppletState.value = false
        return 'El nombre solo puede contener letras'
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
      ProviderState.value = voucherControl.value.proveedorId !== ''
      // eslint-disable-next-line no-unneeded-ternary
      StatusVoucherState.value = voucherControl.value.estatusValeId !== ''
      // eslint-disable-next-line no-unneeded-ternary
      TypeVoucherState.value = voucherControl.value.tipoId !== ''
      // eslint-disable-next-line no-unneeded-ternary
      DependencyState.value = voucherControl.value.dependencia !== ''
      AppletState.value = voucherControl.value.subprograma !== ''
      employeeState.value = voucherControl.value.usuario !== ''
      employeeAuthoriserState.value = voucherControl.value.usuarioAutoriza !== ''
    }

    return {
      voucherControl,
      breadcrumbItems,
      employeesProvider,
      DateOfIssueState,
      employees,
      controlValeId,
      departaments,
      provider,
      statusVoucher,
      typeVoucher,
      ExpirationDateState,
      departamentState,
      employeeState,
      ProviderState,
      ProductState,
      DetailVoucherState,
      StatusVoucherState,
      TypeVoucherState,
      employeeAuthoriserState,
      DependencyState,
      AppletState,
      router,

      onUpdateAddVoucherControl,
      validateDateOfIssue,
      validateExpirationDate,
      validateDepartament,
      validateEmployee,
      validateProvider,
      validateStatusVoucher,
      validateTypeVoucher,
      validateDependency,
      validateApplet,
      validateUserAuthoriser,
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
