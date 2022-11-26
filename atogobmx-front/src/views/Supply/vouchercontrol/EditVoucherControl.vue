<template>
  <b-card class="m-2">
    <b-card class="mb-4">
      <b-breadcrumb class="p-0" :items="breadcrumbItems"> </b-breadcrumb>
    </b-card>
    <b-card>
      <div>
        <h3>Editar control de vale</h3>
      </div>
      <Form @submit="onUpdateVoucherControl">
        <b-row cols="2">
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
                  @input="getAreas(voucherControl.departamentoId)"
                ></Datepicker>
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
                  @input="getWorkStation(voucherControl.areaId)"
                >
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
          </b-col>
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
          <!--Agregar Producto-->
          <b-col>
            <b-form-group class="mt-3" label="Producto: ">
              <Field
                name="ProductField"
                :rules="validateProduct"
                as="text"
              >
                <b-form-select
                  v-model="voucherControl.productoId"
                  autofocus
                  :options="productVoucher"
                  value-field="productoId"
                  text-field="nombre"
                  :state="ProductState"
                >
                </b-form-select>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="ProductField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--Agregar Detalle vale-->
          <b-col>
            <b-form-group class="mt-3" label="Detalle vale: ">
              <Field
                name="DetailVoucherField"
                :rules="validateDetailVoucher"
                as="text"
              >
                <b-form-select
                  v-model="voucherControl.detalleValeId"
                  autofocus
                  :options="detailVoucher"
                  value-field="detalleValeId"
                  text-field="cantidad"
                  :state="DetailVoucherState"
                >
                </b-form-select>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="DetailVoucherField"
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
                  text-field="estatusVale"
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
                  text-field="nombreVale"
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
        <b-row align-h="end">
          <b-button
            class="col-1 m-2 text-white"
            variant="primary"
            to="/ServiciosPublicos/ControlVale/list"
            type="reset"
          >
            Cancelar
          </b-button>
          <b-button type="success" class="col-1 m-2" variant="success">
            Guardar
          </b-button>
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
import ProductVoucherServices from '@/Services/productvoucher.Services'
import DetailVoucherServices from '@/Services/detailvoucher.Services'
import StatusVoucherServices from '@/Services/statusvoucher.Services'
import TypeVoucherServices from '@/Services/typevoucher.Services'
import { ref, inject } from 'vue'
import { useRoute, useRouter } from 'vue-router'
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
    const { getVoucherControlById, updateVoucherControl } = VoucherControlServices()
    const { getAreasByDepartament } = AreaServices()
    const { getDepartaments } = DepartamentServices()
    const { getEmployees } = EmployeeServices()
    const { getProvider } = ProviderServices()
    const { getProductVoucher } = ProductVoucherServices()
    const { getDetailVoucher } = DetailVoucherServices()
    const { getStatusVoucher } = StatusVoucherServices()
    const { getTypeVoucher } = TypeVoucherServices()
    const voucherControl = ref([])
    const employees = ref([])
    const departaments = ref([])
    const areas = ref([])
    const provider = ref([])
    const productVoucher = ref([])
    const detailVoucher = ref([])
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
      { text: 'Editar-Control de Vale' }
    ])

    const getAreas = departamentoId => {
      getAreasByDepartament(departamentoId, data => {
        areas.value = data
        if (data.length === 0) {
          swal.fire({
            title: 'No se encuentran areas registradas!',
            text: 'No se encuentran areas registradas en el departamento seleccionado, registre primero una area para continuar.',
            icon: 'warning'
          })
        }
      })
    }

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

    getProductVoucher(data => {
      productVoucher.value = data
      if (data.length === 0) {
        swal.fire({
          title: 'No se encuentran productos registrados!',
          text: 'No se encuentran  productos registrados en el sistema, registre primero un producto para continuar.',
          icon: 'warning'
        })
      }
    })

    getDetailVoucher(data => {
      detailVoucher.value = data
      if (data.length === 0) {
        swal.fire({
          title: 'No se encuentra el detalle del vale registrados!',
          text: 'No se encuentra el detalle del vale registrados en el sistema, registre primero un detalle de vale para continuar.',
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

    const onUpdateVoucherControl = () => {
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

    getVoucherControlById(router.params.ControlValeId, data => {
      voucherControl.value = data
    })

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

    const validateProduct = () => {
      if (!voucherControl.value.productoId) {
        validateState()
        return 'Este campo es requerido'
      }
      validateState()
      return true
    }

    const validateDetailVoucher = () => {
      if (!voucherControl.value.detalleValeId) {
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
      DateOfIssueState.value = voucherControl.value.fechaEmicion === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      ExpirationDateState.value = voucherControl.value.fechaVigencia === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      departamentState.value = voucherControl.value.departamentoId === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      areaState.value = voucherControl.value.areaId === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      EmployeeState.value = voucherControl.value.empleadoId === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      ProviderState.value = voucherControl.value.proveedorId === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      ProductState.value = voucherControl.value.productoId === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      DetailVoucherState.value = voucherControl.value.detalleValeId === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      StatusVoucherState.value = voucherControl.value.estatusValeId === '' ? false : true
      // eslint-disable-next-line no-unneeded-ternary
      TypeVoucherState.value = voucherControl.value.tipoId === '' ? false : true
    }

    return {
      voucherControl,
      breadcrumbItems,
      DateOfIssueState,
      areas,
      ExpirationDateState,
      departamentState,
      areaState,
      EmployeeState,
      ProviderState,
      ProductState,
      DetailVoucherState,
      StatusVoucherState,
      TypeVoucherState,

      onUpdateVoucherControl,
      getAreas,
      validateDateOfIssue,
      validateExpirationDate,
      validateDepartament,
      validateArea,
      validateEmployee,
      validateProvider,
      validateProduct,
      validateDetailVoucher,
      validateStatusVoucher,
      validateTypeVoucher,
      validateState
    }
  }
}
</script>

<style></style>
