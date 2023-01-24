<template>
  <div>
    <VueSidebarMenuAkahon
      style="height: 98%; margin-top: 8px;"
      v-if="Token"
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
      :profileImg="`http://localhost:5000/api/Archivos/FotoPerfil/Empleado/${empleadoId}`"
      :profileName="employee"
      :profileRole="departament + ', ' + puestoTrabajo"
      @button-exit-clicked="removeLocalStorgare()"
    />
  </div>
  <router-view></router-view>
</template>
<script>
import { ref, inject } from 'vue'
import LogoAtogobMx from '@/Images/Icon.png'
import AtogobMx from '@/Images/AtoGobMx-removebg-preview.png'
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
    const ImageProfile = ref(AtogobMx)
    const empleadoId = window.sessionStorage.getItem('EmpleadoId')
    const Token = window.sessionStorage.getItem('Token')
    const departament = window.sessionStorage.getItem('Departamento')
    const puestoTrabajo = window.sessionStorage.getItem('PuestoTrabajo')
    const employee = window.sessionStorage.getItem('Nombre')
    const menu = ref([
      { link: '/', name: 'Inicio', icon: 'bi bi-house' },
      {
        link: '/RecursosHumanos',
        name: 'Recursos Humanos',
        icon: 'bi bi-people-fill'
      },
      {
        link: '/ServiciosPublicos',
        name: 'Servicios Publicos',
        icon: 'bi bi-globe2'
      },
      { link: '/Obras', name: 'Obras Publicas', icon: 'bi bi-cone-striped' },
      {
        link: '/Proveeduria',
        name: 'Proveeduria',
        icon: 'bi bi-clipboard2-data-fill'
      },
      {
        link: '/PatrimonioMunicipal',
        name: 'Patrimonio Publico',
        icon: 'bi bi-bookmark-check-fill'
      },
      {
        link: '/ServiciosMedicosMunicipales',
        name: 'Servicios Medicos',
        icon: 'bi bi-hospital-fill'
      },
      {
        link: '/Administrador',
        name: 'Administrador',
        icon: 'bi bi-clipboard2-pulse-fill'
      }
    ])
    const removeLocalStorgare = () => {
      window.sessionStorage.removeItem('Token')
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
      ImageProfile,
      Token,
      departament,
      puestoTrabajo,
      employee,
      menu,
      empleadoId,

      removeLocalStorgare
    }
  }
}
</script>

<style>
@import '@/css/table-easy-documents.css';
.sidebar .nav-list {
  margin-left: -30px;
}
.sidebar div.profile {
  height: 90px;
}
.sidebar div.profile .name {
  font-size: 12px;
  white-space: pre-line;
}
.sidebar div.profile .job {
  font-size: 9px;
  white-space: pre-line;
}
.sidebar div.profile #log_out {
  height: 90px;
}
.sidebar div img {
  height: 60px;
  width: 60px;
}
body {
  background-color: #f8f7fa !important;
  /* padding-left: 350px !important; */
}
.btn:hover {
  color: #ffff;
  background-color: #9d94f4 !important;
}
</style>
