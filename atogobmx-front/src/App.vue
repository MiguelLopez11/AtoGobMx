<template>
  <b-card align="center" class="m-2" v-if="isLogged">
    <b-navbar toggleable="xl">
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
            <!--Alumbrado Publico-->
            <b-nav-item-dropdown
              v-if="departament === 'Alumbrado publico' || role === 'Administrador'"
              text="⚡ Alumbrado publico"
              dropright
              class="btn-group dropend"
            >
              <template v-slot:button-content>
                <!-- <i class="bi bi-globe2"></i> -->
              </template>
              <b-dropdown-item to="/Alumbrado/list">
                <i class="bi bi-lightning-charge-fill"></i>
                Alumbrado Público
              </b-dropdown-item>
              <b-dropdown-item to="/EstatusAlumbrado/list">
                <i class="bi bi-layers-fill"></i>
                Estatus Alumbrado
              </b-dropdown-item>
              <!-- v-if="1 < 0" -->
              <b-dropdown-item to="/TareaTipoAlumbrado/list">
                <i class="bi bi-layers-fill"></i>
                Tarea tipo
              </b-dropdown-item>
              <b-dropdown-item to="/ExpedienteAlumbrado/list">
                <i class="bi bi-folder-fill"></i>
                Expediente
              </b-dropdown-item>
              <b-dropdown-item to="/AlumbradoEmpleado/list">
                <!-- <i class="bi bi-folder-fill"></i> -->
                Empleado Alumbrado
              </b-dropdown-item>
            </b-nav-item-dropdown>
            <!--Cementerios-->
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
            <!--Aseo-->
            <b-nav-item-dropdown
              v-if="departament === 'Aseo' || role === 'Administrador'"
              text="🧹 Aseo"
              dropright
              class="btn-group dropend"
            >
              <template v-slot:button-content>
                <!-- <i class="bi bi-globe2"></i> -->
              </template>
              <b-dropdown-item to="/Aseo/list">
                <!-- <i class="bi bi-lightning-charge-fill"></i> -->
                Aseo
              </b-dropdown-item>
              <b-dropdown-item to="/Ruta/list">
                <!-- <i class="bi bi-lightning-charge-fill"></i> -->
                Ruta
              </b-dropdown-item>
              <b-dropdown-item to="/Zona/list">
                <!-- <i class="bi bi-lightning-charge-fill"></i> -->
                Zona
              </b-dropdown-item>
            </b-nav-item-dropdown>
            <!--Obras Publicas-->
            <b-nav-item-dropdown
              v-if="departament === 'Obras Publicas' || role === 'Administrador'"
              text="🏗️ Obras publicas"
              dropright
              class="btn-group dropend"
            >
              <template v-slot:button-content>
                <!-- <i class="bi bi-globe2"></i> -->
              </template>
              <b-dropdown-item to="/ObrasPublicas/list">
                <!-- <i class="bi bi-lightning-charge-fill"></i> -->
                Obras publicas
              </b-dropdown-item>
              <b-dropdown-item to="/EstatusOP/list">
                <!-- <i class="bi bi-lightning-charge-fill"></i> -->
                Estatus de obras publicas
              </b-dropdown-item>
            </b-nav-item-dropdown>
          </b-nav-item-dropdown>
          <!--Cementerios-->
          <!-- <b-nav-item-dropdown
            v-if="departament === 'Cementerios' || role === 'Administrador'"
            text="Cementerios"
            dropright
            auto-close="outside"
          >
            <template v-slot:button-content> </template>
            <b-dropdown-item to="/Cementerios/list">
              Gabetas
            </b-dropdown-item>
            <b-dropdown-item to="/DireccionCementerios/list">
              Cementerios
            </b-dropdown-item>
          </b-nav-item-dropdown> -->
          <!--Aseo-->
          <!-- <b-nav-item-dropdown
            v-if="departament === 'Aseo' || role === 'Administrador'"
            text="Aseo"
            dropright
            auto-close="outside"
          >
            <template v-slot:button-content>
            </template>
            <b-dropdown-item to="/Aseo/list">
              Aseo
            </b-dropdown-item>
            <b-dropdown-item to="/Ruta/list">
              Ruta
            </b-dropdown-item>
            <b-dropdown-item to="/Zona/list">
              Zona
            </b-dropdown-item>
          </b-nav-item-dropdown> -->
          <!--Obras publicas-->
          <!-- <b-nav-item-dropdown
            v-if="departament === 'Obras publicas' || role === 'Administrador'"
            text="ObrasPublicas"
            dropright
            auto-close="outside"
          >
            <template v-slot:button-content>
            </template>
            <b-dropdown-item to="/ObrasPublicas/list">
              Obras publicas
            </b-dropdown-item>
            <b-dropdown-item to="/EstatusOP/list">
              Estatus de obras publicas
            </b-dropdown-item>
            <b-dropdown-item to="//list">
              <i class="bi bi-lightning-charge-fill"></i>
              nohay nada
            </b-dropdown-item>
          </b-nav-item-dropdown> -->
          <!--Proveeduria-->
          <b-nav-item-dropdown
            v-if="
              departament === 'Control de vales' || role === 'Administrador'
            "
            text="Control de vales"
            dropright
            auto-close="outside"
          >
            <template v-slot:button-content>
              <!-- <i class="bi bi-globe2"></i> -->
            </template>
            <b-dropdown-item to="/ControlVale/list">
              <!-- <i class="bi bi-lightning-charge-fill"></i> -->
              Control de vale
            </b-dropdown-item>
            <b-dropdown-item to="/DetalleProducto/list">
              <!-- <i class="bi bi-lightning-charge-fill"></i> -->
              Detalle del producto
            </b-dropdown-item>
            <b-dropdown-item to="/DetalleVale/list">
              <!-- <i class="bi bi-lightning-charge-fill"></i> -->
              Detalle del vale
            </b-dropdown-item>
            <b-dropdown-item to="/EstatusVale/list">
              <!-- <i class="bi bi-lightning-charge-fill"></i> -->
              Estatus del vale
            </b-dropdown-item>
            <b-dropdown-item to="/Producto/list">
              <!-- <i class="bi bi-lightning-charge-fill"></i> -->
              Producto
            </b-dropdown-item>
            <b-dropdown-item to="/Proveedor/list">
              <!-- <i class="bi bi-lightning-charge-fill"></i> -->
              Proveedor
            </b-dropdown-item>
            <b-dropdown-item to="/TipoVale/list">
              <!-- <i class="bi bi-lightning-charge-fill"></i> -->
              Tipo del vale
            </b-dropdown-item>
            <template v-slot:button-content>
              <i class="bi bi-globe2"></i>
            </template>
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
              Tarea tipo
            </b-dropdown-item>
            <b-dropdown-item to="/ExpedienteAlumbrado/list">
              <i class="bi bi-folder-fill"></i>
              Expediente
            </b-dropdown-item>
            <b-dropdown-item to="/AlumbradoEmpleado/list">
              Empleado Alumbrado
            </b-dropdown-item>
          </b-nav-item-dropdown>
          <b-nav-item-dropdown
            v-if="departament === 'Cementerios' || role === 'Administrador'"
            text="Cementerios"
            dropright
            auto-close="outside"
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
            v-if="
              departament === 'Servicios Publicos' || role === 'Administrador'
            "
            text="Aseo"
            dropright
            auto-close="outside"
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
          <b-nav-item-dropdown text="Administrador" right auto-close style="margin-right: 70px">
            <template v-slot:button-content>
              <i class="bi bi-clipboard2-pulse-fill"></i>
            </template>
            <b-dropdown-item to="/Usuarios/list">
              <i class="bi bi-person-bounding-box"></i>
              Usuarios
            </b-dropdown-item>
            <b-dropdown-item to="/Roles/list">
              <i class="bi bi-person-rolodex"></i>
              Roles
            </b-dropdown-item>
          </b-nav-item-dropdown>
        </b-navbar-nav>
        <b-row align-h="end" style="margin-right:0">
          <!-- <b-button
            v-if="!isLogged"
            size="lg"
            to="/Login"
            style="background-color: #7367f0"
            class="mr-3 ml-3"
          >
            <i class="bi bi-box-arrow-right"></i>
            Login
          </b-button> -->
          <b-button
            v-if="isLogged"
            size="lg"
            @click="removeLocalStorgare()"
            style="background-color: #7367f0; font-size: 18px"
          >
            <i class="bi bi-box-arrow-right"></i>
            Cerrar Sesión
          </b-button>
        </b-row>
      </b-collapse>
    </b-navbar>
    <!-- <b-navbar>
      <div class="col-lg-8 col-xs-12 col-centered">
        <b-navbar-nav justified>
        </b-navbar-nav>
      </div>
    </b-navbar> -->
  </b-card>
  <router-view></router-view>
</template>
<script>
import { ref, inject } from 'vue'
import LogoAtogobMx from '@/Images/Icon.png'
import { useRouter } from 'vue-router'
import 'vue-sidebar-menu/dist/vue-sidebar-menu.css'

export default {
  components: {},
  setup () {
    const router = useRouter()
    const swal = inject('$swal')
    const background = ref(LogoAtogobMx)
    const isLogged = window.sessionStorage.getItem('isLogged')
    const departament = window.sessionStorage.getItem('Departamento')
    const role = window.sessionStorage.getItem('Role')
    const area = window.sessionStorage.getItem('Area')
    const menu = ref([
      {
        header: 'AtoGobMX',
        hiddenOnCollapse: true
      }
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
.navbar-nav .nav-link {
  border-width: 2px;
  width: auto;
}
body {
  background-color: rgb(236, 237, 238) !important;
}
.card-body {
  background-color: #f8f9fa;
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
