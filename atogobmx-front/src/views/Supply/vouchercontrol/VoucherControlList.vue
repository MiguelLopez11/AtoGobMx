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
        placeholder="Buscar Vale..."
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
        Consulta de vales
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
      :items="voucherControl"
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
            @click="RemoveVoucherControl(items.controlValeId)"
            class="m-1"
            variant="outline-danger"
            ><i class="bi bi-trash3"> Archivar</i></b-dropdown-item
          >
          <b-dropdown-item
            class="m-1"
            variant="outline-warning"
            :to="{
              name: 'ControlVale-Edit',
              params: { ControlValeId: items.controlValeId }
            }"
            ><i class="bi bi-pencil-square" /> Editar</b-dropdown-item
          >
        </b-dropdown>
      </template>
    </EasyDataTable>
    <b-modal
      id="modal-voucherControl"
      title="Agregar Vale"
      v-model="showModal"
      size="xl"
      hide-footer
      button-size="lg"
      lazy
    >
      <Form @submit="addVoucherControl">
        <b-row cols="2">
          <!--Agregar Fecha emicion-->
          <b-col>
            <b-form-group class="mt-3" label="Fecha emicion">
              <Field name="DateOfIssueField" :rules="validateDateOfIssue" as="">
                <Datepicker
                  locale="es"
                  name="date"
                  text-input
                  v-model="voucherControlFields.fechaEmicion"
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
                  v-model="voucherControlFields.fechaVigencia"
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
                  v-model="voucherControlFields.areaId"
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
                  v-model="voucherControlFields.departamentoId"
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
          <b-col>
            <b-form-group class="mt-3" label="Empleado: ">
              <Field name="EmployeeField" :rules="validateEmployee" as="text">
                <b-form-select
                  v-model="voucherControlFields.empleadoId"
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
                  v-model="voucherControlFields.proveedorId"
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
          <!-- <b-col>
            <b-form-group class="mt-3" label="Producto: ">
              <Field name="ProductField" :rules="validateProduct" as="text">
                <b-form-select
                  v-model="voucherControlFields.productoId"
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
          </b-col> -->
          <!--Agregar Detalle vale-->
          <!-- <b-col>
            <b-form-group class="mt-3" label="Detalle vale: ">
              <Field
                name="DetailVoucherField"
                :rules="validateDetailVoucher"
                as="text"
              >
                <b-form-select
                  v-model="voucherControlFields.detalleValeId"
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
          </b-col> -->
          <!--Agregar Estatus vale-->
          <b-col>
            <b-form-group class="mt-3" label="Estatus vale: ">
              <Field
                name="StatusVoucherField"
                :rules="validateStatusVoucher"
                as="text"
              >
                <b-form-select
                  v-model="voucherControlFields.estatusValeId"
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
                  v-model="voucherControlFields.tipoId"
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
        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            @click="resetVoucherControlFields"
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
import VoucherControlServices from '@/Services/vouchercontrol.Services'
import EmployeeServices from '@/Services/employee.Services'
import AreaServices from '@/Services/area.Services'
import DepartamentServices from '@/Services/departament.Services'
import ProviderServices from '@/Services/provider.Services'
// import ProductVoucherServices from '@/Services/productvoucher.Services'
// import DetailVoucherServices from '@/Services/detailvoucher.Services'
import StatusVoucherServices from '@/Services/statusvoucher.Services'
import TypeVoucherServices from '@/Services/typevoucher.Services'
import { Form, Field, ErrorMessage } from 'vee-validate'
import { ref, inject } from 'vue'
import Datepicker from '@vuepic/vue-datepicker'
import '@vuepic/vue-datepicker/dist/main.css'
export default {
  components: {
    EasyDataTable: window['vue3-easy-data-table'],
    Datepicker,
    Form,
    Field,
    ErrorMessage
  },
  setup () {
    const swal = inject('$swal')
    const showModal = ref(false)
    const { getVoucherControl, createVoucherControl, deleteVoucherControl } =
      VoucherControlServices()
    const { getAreas } = AreaServices()
    const { getDepartaments } = DepartamentServices()
    const { getEmployees } = EmployeeServices()
    const { getProvider } = ProviderServices()
    // const { getProductVoucher } = ProductVoucherServices()
    // const { getDetailVoucher } = DetailVoucherServices()
    const { getStatusVoucher } = StatusVoucherServices()
    const { getTypeVoucher } = TypeVoucherServices()
    const voucherControl = ref([])
    const employees = ref([])
    const departaments = ref([])
    const areas = ref([])
    const provider = ref([])
    // const productVoucher = ref([])
    // const detailVoucher = ref([])
    const statusVoucher = ref([])
    const typeVoucher = ref([])
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('empleadoId')
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
      { text: 'Proveeduria', to: '/Proveeduria' },
      { text: 'Control de vale' }
    ])

    const voucherControlFields = ref({
      controlValeId: 0,
      fechaEmicion: null,
      fechaVigencia: null,
      departamentoId: 0,
      areaId: null,
      empleadoId: null,
      proveedorId: null,
      estatusValeId: null,
      tipoId: null,
      archivado: false
      // productoId: null,
      // detalleValeId: null,
    })

    const voucherControlFieldsBlank = ref(
      JSON.parse(JSON.stringify(voucherControlFields))
    )

    // const getAreas = departamentoId => {
    //   getAreasByDepartament(departamentoId, data => {
    //     areas.value = data
    //     if (data.length === 0) {
    //       swal.fire({
    //         title: 'No se encuentran areas registradas!',
    //         text: 'No se encuentran areas registradas en el departamento seleccionado, registre primero una area para continuar.',
    //         icon: 'warning'
    //       })
    //     }
    //   })
    // }

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

    // getProductVoucher(data => {
    //   productVoucher.value = data
    //   if (data.length === 0) {
    //     swal.fire({
    //       title: 'No se encuentran productos registrados!',
    //       text: 'No se encuentran  productos registrados en el sistema, registre primero un producto para continuar.',
    //       icon: 'warning'
    //     })
    //   }
    // })

    // getDetailVoucher(data => {
    //   detailVoucher.value = data
    //   if (data.length === 0) {
    //     swal.fire({
    //       title: 'No se encuentra el detalle del vale registrado!',
    //       text: 'No se encuentra el detalle del vale registrado en el sistema, registre primero un detalle de vale para continuar.',
    //       icon: 'warning'
    //     })
    //   }
    // })

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

    const fields = ref([
      // { value: 'controlValeId', text: 'ID', sortable: true },
      { value: 'fechaEmicion', text: 'Fecha emicion' },
      { value: 'fechaVigencia', text: 'Fecha vigencia' },
      { value: 'departamentos.nombre', text: 'Departamento' },
      { value: 'areaId', text: 'Area' },
      { value: 'empleadoId', text: 'empleado' },
      { value: 'proveedorId', text: 'Proveedor' },
      { value: 'estatusValeId', text: 'Proveedor' },
      { value: 'tipoId', text: 'Tipo vale' },
      { value: 'actions', text: 'Acciones' }
    ])

    const resetVoucherControlFields = () => {
      showModal.value = false
      voucherControlFields.value = JSON.parse(
        JSON.stringify(voucherControlFieldsBlank)
      )
      DateOfIssueState.value = false
      ExpirationDateState.value = false
      departamentState.value = false
      areaState.value = false
      EmployeeState.value = false
      ProviderState.value = false
      ProductState.value = false
      DetailVoucherState.value = false
      StatusVoucherState.value = false
      TypeVoucherState.value = false
    }

    getVoucherControl(data => {
      voucherControl.value = data
      if (voucherControl.value.length > 0) {
        isloading.value = false
      } else {
        if (voucherControl.value.length <= 0) {
          isloading.value = false
        }
      }
    })

    const onFiltered = filteredItems => {
      currentPage.value = 1
    }

    const validateDateOfIssue = () => {
      if (!voucherControlFields.value.fechaEmicion) {
        DateOfIssueState.value = false
        return 'Este campo es requerido'
      }
      DateOfIssueState.value = false
      return true
    }

    const validateExpirationDate = () => {
      if (!voucherControlFields.value.fechaVigencia) {
        ExpirationDateState.value = false
        return 'Este campo es requerido'
      }
      ExpirationDateState.value = false
      return true
    }

    const validateDepartament = () => {
      if (!voucherControlFields.value.departamentoId) {
        departamentState.value = false
        return 'Este campo es requerido'
      }
      departamentState.value = true
      return true
    }

    const validateArea = () => {
      if (!voucherControlFields.value.areaId) {
        areaState.value = false
        return 'Este campo es requerido'
      }
      areaState.value = true
      return true
    }

    const validateEmployee = () => {
      if (!voucherControlFields.value.empleadoId) {
        EmployeeState.value = false
        return 'Este campo es requerido'
      }
      EmployeeState.value = false
      return true
    }

    const validateProvider = () => {
      if (!voucherControlFields.value.proveedorId) {
        ProviderState.value = false
        return 'Este campo es requerido'
      }
      ProviderState.value = false
      return true
    }

    const validateProduct = () => {
      if (!voucherControlFields.value.productoId) {
        ProductState.value = false
        return 'Este campo es requerido'
      }
      ProductState.value = false
      return true
    }

    const validateDetailVoucher = () => {
      if (!voucherControlFields.value.detalleValeId) {
        DetailVoucherState.value = false
        return 'Este campo es requerido'
      }
      DetailVoucherState.value = false
      return true
    }

    const validateStatusVoucher = () => {
      if (!voucherControlFields.value.estatusValeId) {
        StatusVoucherState.value = false
        return 'Este campo es requerido'
      }
      StatusVoucherState.value = false
      return true
    }

    const validateTypeVoucher = () => {
      if (!voucherControlFields.value.tipoId) {
        TypeVoucherState.value = false
        return 'Este campo es requerido'
      }
      TypeVoucherState.value = false
      return true
    }

    // pone mis cambios de mis campos vacios de nuevo
    const refreshTable = () => {
      isloading.value = true
      getVoucherControl(data => {
        voucherControl.value = data
        if (voucherControl.value.length > 0) {
          isloading.value = false
        } else {
          if (voucherControl.value.length <= 0) {
            isloading.value = false
          }
        }
      })
      return 'datos recargados'
    }
    const addVoucherControl = () => {
      createVoucherControl(voucherControlFields.value, data => {
        refreshTable()
        swal.fire({
          title: '¡Control de vales registrado correctamente!',
          text: 'El control de vales se ha registrado al sistema satisfactoriamente.',
          icon: 'success'
        })
      })
      showModal.value = false
      resetVoucherControlFields()
    }
    const RemoveVoucherControl = VoucherControlId => {
      isloading.value = true
      swal
        .fire({
          title: '¿Estas seguro?',
          text: 'No podrás revertir esto!',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Si, Archivar control de vales!',
          cancelButtonText: 'Cancelar'
        })
        .then(result => {
          if (result.isConfirmed) {
            deleteVoucherControl(VoucherControlId, data => {
              refreshTable()
            })
            swal.fire({
              title: '¡Control de vales archivado!',
              text: 'El control de vales ha sido archivado satisfactoriamente .',
              icon: 'success'
            })
          } else {
            isloading.value = false
          }
        })
    }
    return {
      voucherControl,
      employees,
      breadcrumbItems,
      departaments,
      areas,
      provider,
      statusVoucher,
      typeVoucher,
      voucherControlFields,
      showModal,
      perPage,
      currentPage,
      filter,
      perPageSelect,
      isloading,
      searchValue,
      searchField,
      voucherControlFieldsBlank,
      fields,
      DateOfIssueState,
      ExpirationDateState,
      departamentState,
      areaState,
      EmployeeState,
      ProviderState,
      ProductState,
      DetailVoucherState,
      StatusVoucherState,
      TypeVoucherState,

      onFiltered,
      addVoucherControl,
      // getAreas,
      refreshTable,
      RemoveVoucherControl,
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
      resetVoucherControlFields
    }
  }
}
</script>

<style></style>
