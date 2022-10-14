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
              <b-dropdown-item to="/Empleados/list">Empleados</b-dropdown-item>
              <b-dropdown-item to="/ExpedientesDigitales/list"
                >ExpedienteDigital</b-dropdown-item
              >
              <b-dropdown-item to="/Departamentos/list"
                >Departamentos</b-dropdown-item
              >
              <b-dropdown-item to="/Areas/list">Areas</b-dropdown-item>
              <b-dropdown-item to="/PuestosTrabajos/list"
                >Puestos de trabajo</b-dropdown-item
              >
            </b-nav-item-dropdown>
          </b-navbar-brand>
          <b-navbar-brand>
            <b-nav-item-dropdown text="Servicios Publicos" right auto-close>
              <template v-slot:button-content>
                <i class="bi bi-globe2"></i>
              </template>
              <b-dropdown-item to="/Alumbrado/list"
                >Alumbrado Público</b-dropdown-item
              >
              <b-dropdown-item to="/EstatusAlumbrado/list"
                >Estatus Alumbrado</b-dropdown-item
              >
              <b-dropdown-item to="/TareaTipoAlumbrado/list"
                >Tarea tipo</b-dropdown-item
              >
            </b-nav-item-dropdown>
          </b-navbar-brand>
          <b-navbar-brand>
            <b-nav-item-dropdown text="Administrador" right auto-close>
              <template v-slot:button-content>
                <i class="bi bi-clipboard2-pulse-fill"></i>
              </template>
              <b-dropdown-item to="/Usuarios/list">Usuarios</b-dropdown-item>
              <b-dropdown-item to="/Roles/list">Roles</b-dropdown-item>
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
