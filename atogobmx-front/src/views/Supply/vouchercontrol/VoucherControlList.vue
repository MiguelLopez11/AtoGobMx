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
        placeholder="Buscar vale..."
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
        Crear nuevo vale
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
          <b-dropdown-item
            class="m-1"
            variant="outline-warning"
            @click="onDownloadFile(items)"
          >
            <i class="bi bi-download"></i>
            Generar vale
          </b-dropdown-item>
          <b-dropdown-item
            v-if="
              items.proV_EstatusVale.nombre === 'Pendiente' && !items.autorizado
            "
            @click="onAutorized(items.controlValeId)"
            class="m-1"
            variant="outline-danger"
            ><i class="bi bi-trash3">Validar</i></b-dropdown-item
          >
          <b-dropdown-item
            v-if="
              items.proV_EstatusVale.nombre === 'Validado' && items.autorizado
            "
            @click="onDeliver(items.controlValeId)"
            class="m-1"
            variant="outline-danger"
            ><i class="bi bi-trash3">Entregar vale</i></b-dropdown-item
          >
          <b-dropdown-item
            v-if="
              items.proV_EstatusVale.nombre === 'Entregado' && items.autorizado
            "
            @click="onReceiver(items.controlValeId)"
            class="m-1"
            variant="outline-danger"
            ><i class="bi bi-trash3">Recibir vale</i></b-dropdown-item
          >
        </b-dropdown>
      </template>
      <template #item-estatus="items">
        <b-badge
          :variant="
            items.proV_EstatusVale.nombre === 'Recibido'
              ? 'success'
              : '' || items.proV_EstatusVale.nombre === 'Pendiente'
              ? 'warning'
              : '' || items.proV_EstatusVale.nombre === 'Validado'
              ? 'primary'
              : '' || items.proV_EstatusVale.nombre === 'Entregado'
              ? 'primary'
              : ''
          "
        >
          {{ items.proV_EstatusVale.nombre }}
        </b-badge>
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
        <b-row cols="3">
          <!--Agregar Fecha emicion-->
          <b-col>
            <b-form-group class="mt-3" label="Fecha emicion">
              <Field
                name="DateOfIssueField"
                :rules="validateDateOfIssue"
                as="text"
              >
                <Datepicker
                  v-model="voucherControlFields.fechaEmicion"
                  locale="es"
                  show-now-button
                  :enableTimePicker="false"
                  :state="DateOfIssueState"
                  cancel-text="Cancelar"
                  select-text="Seleccionar"
                >
                  <template #now-button="{ selectCurrentDate }">
                    <b-button
                      @click="selectCurrentDate()"
                      title="Seleccionar fecha actual"
                    >
                      Hoy
                    </b-button>
                  </template>
                </Datepicker>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="DateOfIssueField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <!--Agregar usuario proveeduria-->
          <b-col>
            <b-form-group class="mt-3" label="Usuario">
              <Field name="ColorField" :rules="validateEmployee" as="text">
                <b-form-select
                  v-model="voucherControlFields.usuario"
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
                  v-model="voucherControlFields.usuarioAutoriza"
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
          <!--Agregar departamento-->
          <b-col>
            <b-form-group class="mt-3" label="Dependencia: ">
              <Field
                name="DepartamentField"
                :rules="validateDepartament"
                as="text"
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
          <!--Agregar subprograma-->
          <b-col>
            <b-form-group class="mt-3" label="Subprograma">
              <Field name="AppletField" :rules="validateApplet" as="text">
                <b-form-input
                  v-model="voucherControlFields.subprograma"
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
                as="text"
              >
                <Datepicker
                  v-model="voucherControlFields.fechaVigencia"
                  locale="es"
                  autoApply
                  :enableTimePicker="false"
                  :state="ExpirationDateState"
                >
                </Datepicker>
              </Field>
              <ErrorMessage
                class="text-danger"
                name="ExpirationDateField"
              ></ErrorMessage>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group class="mt-3" label="Recibio">
              <Field name="ReceivedField" :rules="validateReceived" as="text">
                <b-form-input
                  v-model="voucherControlFields.recibio"
                  :state="ReceivedState"
                />
              </Field>
              <ErrorMessage
                class="text-danger"
                name="ReceivedField"
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
import DepartamentServices from '@/Services/departament.Services'
import ProviderServices from '@/Services/provider.Services'
import StatusVoucherServices from '@/Services/statusvoucher.Services'
import TypeVoucherServices from '@/Services/typevoucher.Services'
import { Form, Field, ErrorMessage } from 'vee-validate'
import { ref, inject } from 'vue'
import { axiosPrivate } from '@/common/axiosPrivate.js'
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
    const {
      getVoucherControl,
      createVoucherControl,
      deleteVoucherControl,
      autorizeVoucherControl,
      receiveVoucherControl,
      DeliveryVoucherControl
    } = VoucherControlServices()
    const { getDepartaments } = DepartamentServices()
    const { getEmployees, getEmployeesProvider } = EmployeeServices()
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
    const perPage = ref(5)
    const currentPage = ref(1)
    const filter = ref(null)
    const perPageSelect = ref([5, 10, 25, 50, 100])
    const isloading = ref(true)
    const searchValue = ref('')
    const searchField = ref('fechaEmicion')
    const DateOfIssueState = ref(false)
    const ExpirationDateState = ref(false)
    const departamentState = ref(false)
    const employeeState = ref(false)
    const employeeAuthoriserState = ref(false)
    const ProviderState = ref(false)
    const ReceivedState = ref(false)
    const ProductState = ref(false)
    const DetailVoucherState = ref(false)
    const StatusVoucherState = ref(false)
    const TypeVoucherState = ref(false)
    const disableButtonDownload = ref(false)
    const DependencyState = ref(false)
    const AppletState = ref(false)
    const breadcrumbItems = ref([
      { text: 'Inicio', to: '/' },
      { text: 'Proveeduria', to: '/Proveeduria' },
      { text: 'Control de vale' }
    ])

    const voucherControlFields = ref({
      controlValeId: 0,
      fechaEmicion: null,
      fechaVigencia: null,
      recibio: null,
      usuario: null,
      dependencia: null,
      subprograma: null,
      usuarioAutoriza: null,
      departamentoId: null,
      proveedorId: null,
      estatusValeId: null,
      tipoId: null,
      folio: '-',
      archivado: false
    })

    const voucherControlFieldsBlank = ref(
      JSON.parse(JSON.stringify(voucherControlFields))
    )

    getDepartaments(data => {
      departaments.value = data
    })

    const onDownloadFile = ControlVale => {
      isloading.value = true
      disableButtonDownload.value = true
      axiosPrivate({
        url: `/ControlDeVales/ControlDeVale/Download/${ControlVale.controlValeId}`,
        method: 'GET',
        responseType: 'blob' // important
      })
        .then(response => {
          const url = window.URL.createObjectURL(new Blob([response.data]))
          const link = document.createElement('a')
          link.href = url
          link.setAttribute('download', `Expediente_${ControlVale.recibio}.pdf`)
          document.body.appendChild(link)
          link.click()
        })
        .then(result => {
          isloading.value = false
          disableButtonDownload.value = false
        })
    }

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

    const fields = ref([
      // { value: 'controlValeId', text: 'ID', sortable: true },
      { value: 'fechaEmicion', text: 'Fecha emision' },
      { value: 'fechaVigencia', text: 'Fecha vigencia' },
      { value: 'usuario', text: 'Usuario' },
      { value: 'usuarioAutoriza', text: 'Autorizado por:' },
      { value: 'departamentos.nombre', text: 'Departamento' },
      { value: 'proV_Proveedor.nombre', text: 'Proveedor' },
      { value: 'estatus', text: 'Proveedor' },
      { value: 'tipoVales.nombre', text: 'Tipo vale' },
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
      employeeState.value = false
      employeeAuthoriserState.value = false
      ProviderState.value = false
      ProductState.value = false
      DetailVoucherState.value = false
      StatusVoucherState.value = false
      TypeVoucherState.value = false
      DependencyState.value = false
      AppletState.value = false
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

    const validateEmployee = () => {
      if (!voucherControlFields.value.usuario) {
        employeeState.value = false
        return 'Este campo es requerido'
      }
      employeeState.value = true
      return true
    }

    const validateReceived = () => {
      if (!voucherControlFields.value.recibio) {
        ReceivedState.value = false
        return 'Este campo es requerido'
      }
      if (!voucherControlFields.value.recibio.trim().length > 0) {
        ReceivedState.value = false
        return 'Este campo no puede contener solo espacios'
      }
      if (
        !/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(voucherControlFields.value.recibio)
      ) {
        ReceivedState.value = false
        return 'El nombre solo puede contener letras'
      }
      ReceivedState.value = true
      return true
    }

    const validateDependency = () => {
      if (!voucherControlFields.value.dependencia) {
        DependencyState.value = false
        return 'Este campo es requerido'
      }
      if (!voucherControlFields.value.dependencia.trim().length > 0) {
        DependencyState.value = false
        return 'Este campo no puede contener solo espacios'
      }
      if (
        !/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(
          voucherControlFields.value.dependencia
        )
      ) {
        DependencyState.value = false
        return 'El nombre solo puede contener letras'
      }
      DependencyState.value = true
      return true
    }

    const validateApplet = () => {
      if (!voucherControlFields.value.subprograma) {
        AppletState.value = false
        return 'Este campo es requerido'
      }
      if (!voucherControlFields.value.subprograma.trim().length > 0) {
        AppletState.value = false
        return 'Este campo no puede contener solo espacios'
      }
      if (
        !/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/i.test(
          voucherControlFields.value.subprograma
        )
      ) {
        AppletState.value = false
        return 'El nombre solo puede contener letras'
      }
      AppletState.value = true
      return true
    }

    const validateUserAuthoriser = () => {
      if (!voucherControlFields.value.usuarioAutoriza) {
        employeeAuthoriserState.value = false
        return 'Este campo es requerido'
      }
      employeeAuthoriserState.value = true
      return true
    }

    const validateProvider = () => {
      if (!voucherControlFields.value.proveedorId) {
        ProviderState.value = false
        return 'Este campo es requerido'
      }
      ProviderState.value = true
      return true
    }

    const validateProduct = () => {
      if (!voucherControlFields.value.productoId) {
        ProductState.value = false
        return 'Este campo es requerido'
      }
      ProductState.value = true
      return true
    }

    const validateDetailVoucher = () => {
      if (!voucherControlFields.value.detalleValeId) {
        DetailVoucherState.value = false
        return 'Este campo es requerido'
      }
      DetailVoucherState.value = true
      return true
    }

    const validateStatusVoucher = () => {
      if (!voucherControlFields.value.estatusValeId) {
        StatusVoucherState.value = false
        return 'Este campo es requerido'
      }
      StatusVoucherState.value = true
      return true
    }

    const validateTypeVoucher = () => {
      if (!voucherControlFields.value.tipoId) {
        TypeVoucherState.value = false
        return 'Este campo es requerido'
      }
      TypeVoucherState.value = true
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
    const onAutorized = controlValeId => {
      autorizeVoucherControl(controlValeId, data => {
        swal.fire({
          title: '¡Control de vales autorizado correctamente!',
          text: 'El control de vales se ha autorizado al sistema satisfactoriamente.',
          icon: 'success'
        })
        refreshTable()
      })
    }
    const onDeliver = controlValeId => {
      DeliveryVoucherControl(controlValeId, data => {
        swal.fire({
          title: '¡Control de vales entregado correctamente!',
          text: 'El control de vales se ha entregado al solicitante satisfactoriamente.',
          icon: 'success'
        })
        refreshTable()
      })
    }
    const onReceiver = controlValeId => {
      receiveVoucherControl(controlValeId, data => {
        swal.fire({
          title: '¡Control de vales entregado correctamente!',
          text: 'El control de vales se ha entregado al solicitante satisfactoriamente.',
          icon: 'success'
        })
        refreshTable()
      })
    }
    return {
      voucherControl,
      employees,
      employeesProvider,
      breadcrumbItems,
      departaments,
      ReceivedState,
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
      employeeState,
      employeeAuthoriserState,
      ProviderState,
      ProductState,
      DetailVoucherState,
      StatusVoucherState,
      TypeVoucherState,
      DependencyState,
      AppletState,
      disableButtonDownload,

      onFiltered,
      addVoucherControl,
      // getAreas,
      refreshTable,
      RemoveVoucherControl,
      validateDateOfIssue,
      validateExpirationDate,
      validateDepartament,
      validateEmployee,
      validateReceived,
      validateUserAuthoriser,
      validateProvider,
      validateProduct,
      validateDetailVoucher,
      validateStatusVoucher,
      validateTypeVoucher,
      validateDependency,
      validateApplet,
      onDownloadFile,
      resetVoucherControlFields,
      onAutorized,
      onDeliver,
      onReceiver
    }
  }
}
</script>

<style></style>
