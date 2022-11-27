<template>
    <VueSidebarMenuAkahon
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
      { link: '/ObrasPublicas', name: 'Obras Publicas', icon: 'icono-arg-camion-construccion' },
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
