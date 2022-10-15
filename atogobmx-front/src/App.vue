<template>
  <div>
    <b-card class="m-3">
      <b-navbar sticky>
        <b-navbar-nav>
          <b-nav-item>
            <template v-slot:button-content>
              <b-avatar :src="background" />
            </template>
          </b-nav-item>
          <div>
            <b-avatar :src="background" />
            <label class="m-2">AtogobMx</label>
          </div>
            <b-nav-item-dropdown
              text="Recursos Humanos"
              dropright
              auto-close="outside"
            >
              <template v-slot:button-content>
                <i class="bi bi-people-fill"></i>
              </template>
              <b-dropdown-item to="/Empleados/list" >
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
              text="Servicios Publicos"
              right auto-close
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
              <b-dropdown-item v-if="1 < 0" to="/TareaTipoAlumbrado/list">
                <i class="bi bi-layers-fill"></i>
                Tarea tipo
              </b-dropdown-item>
            </b-nav-item-dropdown>
            <b-nav-item-dropdown
              text="Administrador"
              right
              auto-close
            >
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
        <b-row align-h="end">
          <b-navbar-nav>
              <b-button size="lg" to="/Login" style="background-color: #7367f0">
                <i class="bi bi-box-arrow-right"></i>
                Login
              </b-button>
          </b-navbar-nav>
        </b-row>
      </b-navbar>
    </b-card>
  </div>
  <router-view></router-view>
</template>
<script>
import { ref } from 'vue'
import LogoAtogobMx from '@/Images/Icon.png'

export default {
  setup () {
    const background = ref(LogoAtogobMx)
    return {
      background
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
.navbar-nav .nav-link
{
      border-width: 2px;
}
body {
  background-color: rgb(236,237,238) !important;
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
  background-color: #7367f0 !important
  /* --bs-btn-hover-color: #7367f0;
  --bs-btn-active-color: #7367f0; */
}
</style>
