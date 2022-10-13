<template>
  <div>
    <b-card align="center" class="m-3">
      <b-nav tabs>
        <b-avatar :src="background" style="cursor: pointer; margin-right: 15px;" />
        <b-nav-item active to="/">AtoGobMx</b-nav-item>
        <b-nav-item-dropdown
          text="Recursos Humanos"
          right
        >
          <b-dropdown-item to="/Empleados/list">Empleados</b-dropdown-item>
          <b-dropdown-item to="/ExpedientesDigitales/list">ExpedienteDigital</b-dropdown-item>
          <b-dropdown-item to="/Departamentos/list">Departamentos</b-dropdown-item>
          <b-dropdown-item to="/Areas/list">Areas</b-dropdown-item>
          <b-dropdown-item to="/PuestosTrabajos/list">Puestos de trabajo</b-dropdown-item>
        </b-nav-item-dropdown>
        <b-nav-item-dropdown
          text="Servicios Publicos"
          right
        >
          <b-dropdown-item to="/Alumbrado/list">Alumbrado Público</b-dropdown-item>
          <b-dropdown-item to="/EstatusAlumbrado/list">Estatus Alumbrado</b-dropdown-item>
          <b-dropdown-item to="/TareaTipoAlumbrado/list">Tarea tipo</b-dropdown-item>
        </b-nav-item-dropdown>
        <b-nav-item-dropdown
          text="Administrador"
          right
        >
          <b-dropdown-item to="/Usuarios/list">Usuarios</b-dropdown-item>
          <b-dropdown-item to="/Roles/list">Roles</b-dropdown-item>
          <!-- <b-dropdown-item to="/TareaTipoAlumbrado/list">Tarea tipo</b-dropdown-item> -->
        </b-nav-item-dropdown>
        <b-nav-item  dir="auto">Login</b-nav-item>
      </b-nav>
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
body {
  background-color: #e5e8e8 !important;
}
nav {
  padding: 30px;
}

nav a {
  font-weight: bold;
  color: #2c3e50;
}

nav a.router-link-exact-active {
  color: #42b983;
}
</style>
