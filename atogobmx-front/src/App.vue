<template>
    <VueSidebarMenuAkahon
      v-if="isLogged"
      menuTitle="AtoGobMX"
      bgColor="#ffffff"
      secondaryColor="#ffffff"
      :menuLogo="background"
      :isSearch="false"
      :menuItems="menu"
      logoTitleColor="#7b7a81"
      menuItemsTextColor="#807e85"
      menuFooterTextColor="#807e85"
      iconsColor="#807e85"
      menuItemsHoverColor="#9d94f4"
    />
      <!-- :button-exit-clicked="removeLocalStorgare()" -->
    <!-- <b-navbar toggleable="xl">
      <b-navbar-brand to="/">
        <b-avatar :src="background" />
        AtogobMx
      </b-navbar-brand>
      <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>
      <b-collapse id="nav-collapse" is-nav>
        <b-navbar-nav class="ml-auto">
          <b-nav-item-dropdown
            v-if="
              departament === 'Recursos Humanos' || role === 'Administrador'
            "
            text="Recursos Humanos"
            dropright
            no-caret
            offset
          >
            <template v-slot:button-content>
              <i class="bi bi-people-fill"></i>
            </template>
            <b-dropdown-item to="/Empleados/list">
              <i class="bi bi-person-fill"></i>
              Empleados
            </b-dropdown-item>
            <b-dropdown-item to="/ExpedientesDigitales/list">
              <i class="bi bi-folder-fill"></i>
              ExpedienteDigital
            </b-dropdown-item>
            <b-dropdown-item to="/Departamentos/list">
              <i class="bi bi-map-fill"></i>
              Departamentos
            </b-dropdown-item>
            <b-dropdown-item to="/Areas/list">
              <i class="bi bi-bricks"></i>
              Areas
            </b-dropdown-item>
            <b-dropdown-item to="/PuestosTrabajos/list">
              <i class="bi bi-laptop-fill"></i>
              Puestos de trabajo
            </b-dropdown-item>
          </b-nav-item-dropdown>
          <b-nav-item-dropdown
            right
            v-if="
              departament === 'Servicios Publicos' || role === 'Administrador'
            "
            text="Servicios Publicos"
            dropright
            auto-close="outside"
          >
            <template v-slot:button-content>
              <i class="bi bi-globe2"></i>
            </template>
            <b-nav-item-dropdown
              v-if="
                departament === 'Alumbrado publico' || role === 'Administrador'
              "
              text="⚡ Alumbrado publico"
              dropright
              class="btn-group dropend"
            >
              <template v-slot:button-content> </template>
              <b-dropdown-item to="/Alumbrado/list">
                <i class="bi bi-lightning-charge-fill"></i>
                Alumbrado Público
              </b-dropdown-item>
              <b-dropdown-item to="/EstatusAlumbrado/list">
                <i class="bi bi-layers-fill"></i>
                Estatus Alumbrado
              </b-dropdown-item>
              <b-dropdown-item to="/TareaTipoAlumbrado/list">
                <i class="bi bi-layers-fill"></i>
                Tipo de Servicio
              </b-dropdown-item>
              <b-dropdown-item to="/ExpedienteAlumbrado/list">
                <i class="bi bi-folder-fill"></i>
                Expedientes
              </b-dropdown-item>
            </b-nav-item-dropdown>
            <b-nav-item-dropdown
              v-if="departament === 'Cementerios' || role === 'Administrador'"
              text="⚰️ Cementerios"
              dropright
              class="btn-group dropend"
            >
              <template v-slot:button-content> </template>
              <b-dropdown-item to="/Cementerios/list">
                Gabetas
              </b-dropdown-item>
              <b-dropdown-item to="/DireccionCementerios/list">
                Cementerios
              </b-dropdown-item>
            </b-nav-item-dropdown>
            <b-nav-item-dropdown
              v-if="departament === 'Aseo' || role === 'Administrador'"
              text="🧹 Aseo"
              dropright
              class="btn-group dropend"
            >
              <template v-slot:button-content> </template>
              <b-dropdown-item to="/Aseo/list">
                Aseo
              </b-dropdown-item>
              <b-dropdown-item to="/Ruta/list">
                Ruta
              </b-dropdown-item>
              <b-dropdown-item to="/Zona/list">
                Zona
              </b-dropdown-item>
            </b-nav-item-dropdown>
            <b-nav-item-dropdown
              v-if="
                departament === 'Obras Publicas' || role === 'Administrador'
              "
              text="🏗️ Obras publicas"
              dropright
              class="btn-group dropend"
            >
              <template v-slot:button-content> </template>
              <b-dropdown-item to="/ObrasPublicas/list">
                Obras publicas
              </b-dropdown-item>
              <b-dropdown-item to="/EstatusOP/list">
                Estatus de obras publicas
              </b-dropdown-item>
            </b-nav-item-dropdown>
          </b-nav-item-dropdown>
          <b-nav-item-dropdown
            right
            v-if="
              departament === 'Control de vales' || role === 'Administrador'
            "
            text="Control de vales"
            dropright
            auto-close="outside"
          >
            <template v-slot:button-content> </template>
            <b-dropdown-item to="/ControlVale/list">
              Control de vale
            </b-dropdown-item>
            <b-dropdown-item to="/DetalleProducto/list">
              Detalle del producto
            </b-dropdown-item>
            <b-dropdown-item to="/DetalleVale/list">
              Detalle del vale
            </b-dropdown-item>
            <b-dropdown-item to="/EstatusVale/list">
              Estatus del vale
            </b-dropdown-item>
            <b-dropdown-item to="/Producto/list">
              Producto
            </b-dropdown-item>
            <b-dropdown-item to="/Proveedor/list">
              Proveedor
            </b-dropdown-item>
            <b-dropdown-item to="/TipoVale/list">
              Tipo del vale
            </b-dropdown-item>
          </b-nav-item-dropdown>
          <b-nav-item-dropdown
            right
            v-if="departament === 'Patrimonio' || role === 'Administrador'"
            text="Patriminio"
          >
            <template v-slot:button-content>
              <i class="bi bi-bookmark-check-fill"></i>
            </template>
            <b-dropdown-item to="/EquiposComputo/list">
              <i class="bi bi-pc-display"></i>
              Equipos de computo
            </b-dropdown-item>
            <b-dropdown-item to="/EstatusEquipo/list">
              <i class="bi bi-list-check"></i>
              Estatus Equipos Computo
            </b-dropdown-item>
            <b-dropdown-item to="/Vehiculos/list">
              <i class="bi bi-car-front-fill"></i>
              Vehiculos
            </b-dropdown-item>
            <b-dropdown-item to="/EstatusVehiculo/list">
              <i class="bi bi-activity"></i>
              Estatus Vehiculo
            </b-dropdown-item>
            <b-dropdown-item to="/Mobiliarios/list">
              <i class="bi bi-lamp-fill"></i>
              Mobiliarios
            </b-dropdown-item>
            <b-dropdown-item to="/CategoriasMobiliario/list">
              <i class="bi bi-columns-gap"></i>
              Categoria Mobiliario
            </b-dropdown-item>
            <b-dropdown-item to="/Armeria/list">
              <i class="bi bi-award-fill"></i>
              Armeria
            </b-dropdown-item>
          </b-nav-item-dropdown>
          <b-nav-item-dropdown text="Servicios Medicos Municipales">
            <template v-slot:button-content>
              <i class="bi bi-hospital-fill" />
            </template>
            <b-dropdown-item to="/ServiciosMedicos/Agenda">
              <i class="bi bi-file-medical"></i>
              Agenda
            </b-dropdown-item>
            <b-dropdown-item to="/ServiciosMedicos/Cita/list">
              <i class="bi bi-file-medical"></i>
              Citas
            </b-dropdown-item>
          </b-nav-item-dropdown>
          <b-nav-item-dropdown
            text="Administrador"
            right
            auto-close
            style="margin-right: 70px"
          >
            <template v-slot:button-content>
              <i class="bi bi-clipboard2-pulse-fill"></i>
            </template>
            <b-dropdown-item to="Administrador/Usuarios/list">
              <i class="bi bi-person-bounding-box"></i>
              Usuarios
            </b-dropdown-item>
            <b-dropdown-item to="Administrador/Roles/list">
              <i class="bi bi-person-rolodex"></i>
              Roles
            </b-dropdown-item>
          </b-nav-item-dropdown>
          <b-row cols="1">
            <b-button
              v-if="isLogged"
              @click="removeLocalStorgare()"
              style="background-color: #7367f0; font-size: 20px; margin-right:-10px"
            >
              <i class="bi bi-box-arrow-right"></i>
            </b-button>
          </b-row>
        </b-navbar-nav>
      </b-collapse>
    </b-navbar> -->
  <!-- </b-card> -->
  <router-view></router-view>
</template>
<script>
import { ref, inject } from 'vue'
import LogoAtogobMx from '@/Images/Icon.png'
import { useRouter } from 'vue-router'
import VueSidebarMenuAkahon from 'vue-sidebar-menu-akahon'

export default {
  components: {
    VueSidebarMenuAkahon
  },
  setup () {
    const router = useRouter()
    const swal = inject('$swal')
    const background = ref(LogoAtogobMx)
    const isLogged = window.sessionStorage.getItem('isLogged')
    const departament = window.sessionStorage.getItem('Departamento')
    const role = window.sessionStorage.getItem('Role')
    const area = window.sessionStorage.getItem('Area')
    const menu = ref([
      { link: '/', name: 'Inicio', icon: 'bi bi-house' },
      { link: '/RecursosHumanos', name: 'Recursos Humanos', icon: 'bi bi-people-fill' },
      { link: '/ServiciosPublicos', name: 'Servicios Publicos', icon: 'bi bi-globe2' },
      { link: '/Proveeduria', name: 'Proveeduria', icon: 'bi bi-clipboard2-data-fill' },
      { link: '/PatrimonioMunicipal', name: 'Patrimonio Publico', icon: 'bi bi-bookmark-check-fill' },
      { link: '/ServiciosMedicosMunicipales', name: 'Servicios Medicos Municipales', icon: 'bi bi-hospital-fill' },
      { link: '/Administrador', name: 'Administrador', icon: 'bi bi-clipboard2-pulse-fill' }
    ])
    const removeLocalStorgare = () => {
      window.sessionStorage.removeItem('isLogged')
      swal
        .fire({
          title: 'Cerrar Sesión!',
          text: 'Se ha cerrado sesion correctamente',
          icon: 'success'
        })
        .then(result => {
          router.go('/Login')
        })
    }
    return {
      background,
      isLogged,
      departament,
      role,
      area,
      menu,

      removeLocalStorgare
    }
  }
}
</script>

<style>
@import '@/css/table-easy-documents.css';
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
.sidebar .nav-list {
margin-left: -30px;
}
.navbar-nav .nav-link {
  border-width: 2px;
  width: auto;
}
body {
  background-color: #f8f7fa !important;
}
.card-body {
  /* background-color: #f8f9fa; */
  border-radius: 5px;
}
.btn-link:hover {
  background-color: #7367f0 !important;
  color: rgb(255, 255, 255) !important;
}
.dropdown-item.active {
  background-color: #7367f0 !important;
}
a.router-link-active.router-link-exact-active {
  color: #7f8996;
}
a.nav-link {
  color: #7f8996;
}
</style>
