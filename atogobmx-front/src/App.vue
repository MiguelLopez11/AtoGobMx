<template>
  <div>
    <b-card class="m-2" v-if="isLogged">
      <b-navbar sticky>
        <b-navbar-nav>
          <b-nav-item>
            <template v-slot:button-content>
              <b-avatar :src="background" />
            </template>
          </b-nav-item>
          <div>
            <b-avatar :src="background" />
            <label  class="m-2">AtogobMx</label>
          </div>
          <b-nav-item-dropdown
            text="Recursos Humanos"
            dropright
            auto-close="outside"
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
          <!--Alumbrado Publico-->
          <b-nav-item-dropdown
            text="Servicios Publicos"
            dropright
            auto-close="outside"
          >
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
            <!-- v-if="1 < 0" -->
            <b-dropdown-item to="/TareaTipoAlumbrado/list">
              <i class="bi bi-layers-fill"></i>
              Tarea tipo
            </b-dropdown-item>
            <b-dropdown-item to="/ExpedienteAlumbrado/list">
              <i class="bi bi-folder-fill"></i>
              Expediente
            </b-dropdown-item>
          </b-nav-item-dropdown>
          <!--Cementerios-->
          <b-nav-item-dropdown
            text="Cementerios"
            dropright
            auto-close="outside"
          >
            <template v-slot:button-content>
              <!-- <i class="bi bi-globe2"></i> -->
            </template>
            <b-dropdown-item to="/Cementerios/list">
              <!-- <i class="bi bi-lightning-charge-fill"></i> -->
              Cementerios
            </b-dropdown-item>
          </b-nav-item-dropdown>
          <b-nav-item-dropdown text="Patriminio" right auto-close>
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
          </b-nav-item-dropdown>
          <b-nav-item-dropdown text="Administrador" right auto-close>
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
        <b-row align-h="end" cols="1">
          <div>
              <b-button v-if="!isLogged" size="lg" to="/Login" style="background-color: #7367f0" class="mr-3">
                <i class="bi bi-box-arrow-right"></i>
                Login
              </b-button>
              <b-button v-if="isLogged" size="lg" @click="removeLocalStorgare()" style="background-color: #7367f0" class="mr-3">
                <i class="bi bi-box-arrow-right"></i>
                Cerrar Sesión
              </b-button>
          </div>
        </b-row>
      </b-navbar>
    </b-card>
  </div>
  <router-view></router-view>
</template>
<script>
import { ref, inject } from 'vue'
import LogoAtogobMx from '@/Images/Icon.png'
import { useRouter } from 'vue-router'

export default {
  setup () {
    const router = useRouter()
    const swal = inject('$swal')
    const background = ref(LogoAtogobMx)
    const isLogged = window.sessionStorage.getItem('isLogged')
    const removeLocalStorgare = () => {
      window.sessionStorage.removeItem('isLogged')
      swal.fire({
        title: 'Cerrar Sesión!',
        text: 'Se ha cerrado sesion correctamente',
        icon: 'success'
      }).then(result => {
        router.go('/Login')
        // router.push({ name: 'Login' })
      })
    }
    return {
      background,
      isLogged,

      removeLocalStorgare
      // roleUser,
      // DepartamentUser
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
  /* --bs-btn-hover-color: #7367f0;
  --bs-btn-active-color: #7367f0; */
}
</style>
