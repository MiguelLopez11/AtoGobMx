<template>
  <div>
    <b-card align="center" class="m-3">
      <b-navbar container="fluid" toggleable="lg">
        <b-navbar-nav>
          <b-nav-item>
            <template v-slot:button-content>
              <b-avatar :src="background" />
            </template>
          </b-nav-item>
          <b-navbar-brand href="/" class="mt-1">
            <b-avatar :src="background" />
            AtoGobMx
          </b-navbar-brand>
          <b-navbar-brand>
            <b-nav-item-dropdown text="Recursos Humanos" dropright auto-close>
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
              </b-dropdown-item
              >
              <b-dropdown-item to="/Areas/list">
                <i class="bi bi-bricks"></i>
                Areas
              </b-dropdown-item>
              <b-dropdown-item to="/PuestosTrabajos/list">
                <i class="bi bi-laptop-fill"></i>
                Puestos de trabajo
              </b-dropdown-item>
            </b-nav-item-dropdown>
          </b-navbar-brand>
          <b-navbar-brand>
            <b-nav-item-dropdown text="Servicios Publicos" right auto-close>
              <template v-slot:button-content>
                <i class="bi bi-globe2"></i>
              </template>
              <b-dropdown-item to="/Alumbrado/list">
                <i class="bi bi-lightning-charge-fill"></i>
                Alumbrado Público
              </b-dropdown-item
              >
              <b-dropdown-item to="/EstatusAlumbrado/list">
                <i class="bi bi-layers-fill"></i>
                Estatus Alumbrado
              </b-dropdown-item
              >
              <b-dropdown-item v-if="1 < 0" to="/TareaTipoAlumbrado/list">
                <i class="bi bi-layers-fill"></i>
                Tarea tipo
              </b-dropdown-item
              >
            </b-nav-item-dropdown>
          </b-navbar-brand>
          <b-navbar-brand>
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
          </b-navbar-brand>
        </b-navbar-nav>
        <b-row align-h="end">
          <b-navbar-nav>
            <b-navbar-brand>
              <b-nav-item to="/Login">
                <i class="bi bi-box-arrow-right"></i>
                Login
              </b-nav-item>
            </b-navbar-brand>
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
.nav-pills .nav-link {
  color: black;
}
body {
  background-color: #e5e8e8 !important;
}
</style>
