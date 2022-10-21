import { createRouter, createWebHistory } from 'vue-router'

const routes = [
  {
    path: '/Login',
    name: 'Login',
    component: () => import('../views/LogIn/LogIn.vue'),
    meta: {
      requiresAuth: false
    }
  },
  {
    path: '/PageNotAllowed',
    name: 'PageNotPermission',
    component: () => import('../views/PageNotPermission/PageNotPermission.vue'),
    meta: {
      requiresAuth: false
    }
  },
  {
    path: '/',
    name: 'Home',
    component: () => import('../views/Home/HomePage.vue'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/Areas/List',
    name: 'Areas',
    component: () => import('../views/RecursosHumanos/areas/AreaList.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Recursos Humanos'

    }
  },
  {
    path: '/Areas/:AreaId',
    name: 'Area-Edit',
    component: () => import('../views/RecursosHumanos/areas/AreaEdit.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Recursos Humanos'
    }
  },
  {
    path: '/Empleados/list',
    name: 'Empleados',
    component: () => import('@/views/RecursosHumanos/Employees/EmployeeList.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Recursos Humanos'
    }
  },
  {
    path: '/Empleados/:EmpleadoId',
    name: 'Empleados-Edit',
    component: () => import('@/views/RecursosHumanos/Employees/EmployeeEdit.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Recursos Humanos'
    }
  },
  {
    path: '/ExpedientesDigitales/list',
    name: 'ExpedientesDigitales',
    component: () => import('@/views/RecursosHumanos/ExpedienteDigital/ExpedientDigitalList.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Recursos Humanos'
    }
  },
  {
    path: '/ExpedientesDigitales/:ExpedienteDigitalId',
    name: 'ExpedienteDigital-edit',
    component: () => import('@/views/RecursosHumanos/ExpedienteDigital/ExpedientDigitalEdit.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Recursos Humanos'
    }
  },
  {
    path: '/Alumbrado/list',
    name: 'Alumbrado',
    component: () => import('@/views/ServiciosPublicos/streetLighting/publiclighting/PublicLightingList.vue'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/Alumbrado/:AlumbradoId',
    name: 'Alumbrado-Edit',
    component: () => import('@/views/ServiciosPublicos/streetLighting/publiclighting/EditLighting.vue'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/EstatusAlumbrado/list',
    name: 'EstatusAlumbradorado',
    component: () => import('@/views/ServiciosPublicos/streetLighting/statuslighting/StatusLightingList.vue'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/EstatusAlumbrado/:EstatusId',
    name: 'EstatusAlumbrado-Edit',
    component: () => import('@/views/ServiciosPublicos/streetLighting/statuslighting/EditStatusLighting.vue'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/ExpedienteAlumbrado/list',
    name: 'ExpedienteAlumbradorado',
    component: () => import('@/views/ServiciosPublicos/streetLighting/expedientlighting/ExpedientLightingList.vue'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/ExpedienteAlumbrado/:ExpedienteAlumbradoId',
    name: 'ExpedienteAlumbrado-Edit',
    component: () => import('@/views/ServiciosPublicos/streetLighting/expedientlighting/EditExpedientLighting.vue'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/InventarioAlumbrado/list',
    name: 'InventarioAlumbradorado',
    component: () => import('@/views/ServiciosPublicos/streetLighting/inventorylighting/InventoryLightingList.vue'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/InventarioAlumbrado/:InventarioAlumbradoId',
    name: 'InventarioAlumbrado-Edit',
    component: () => import('@/views/ServiciosPublicos/streetLighting/inventorylighting/EditInventoryLighting.vue'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/TareaTipoAlumbrado/list',
    name: 'TareaTipoAlumbradorado',
    component: () => import('@/views/ServiciosPublicos/streetLighting/typetasklighting/TaskTypeLightingList.vue'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/TareaTipoAlumbrado/:TareaTipoAlumbradoId',
    name: 'TareaTipoAlumbrado-Edit',
    component: () => import('@/views/ServiciosPublicos/streetLighting/typetasklighting/EditTaskTypeLighting.vue'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/Cementerios/list',
    name: 'Cementerios',
    component: () => import('@/views/ServiciosPublicos/cementery/Cementery/CementeryList.vue'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/Cementerios/:CementeriosId',
    name: 'Cementerios-Edit',
    component: () => import('@/views/ServiciosPublicos/cementery/Cementery/EditCementery.vue'),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/Usuarios/list',
    name: 'Usuarios',
    component: () => import('@/views/Users/UserList.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Dirección de sistemas'
    }
  },
  {
    path: '/Usuarios/:usuarioId',
    name: 'Usuarios-Edit',
    component: () => import('@/views/Users/UserEdit.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Dirección de sistemas'
    }
  },
  {
    path: '/Roles/list',
    name: 'Roles',
    component: () => import('@/views/Roles/RoleList.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Dirección de sistemas'
    }
  },
  {
    path: '/Roles/:RoleId',
    name: 'Roles-Edit',
    component: () => import('@/views/Roles/RoleEdit.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Dirección de sistemas'
    }
  },
  {
    path: '/Departamentos/list',
    name: 'Departamentos',
    component: () => import('@/views/RecursosHumanos/Departaments/DepartamentList.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Recursos Humanos'
    }
  },
  {
    path: '/Departamentos/:DepartamentoId',
    name: 'Departamentos-Edit',
    component: () => import('@/views/RecursosHumanos/Departaments/DepartamentEdit.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Recursos Humanos'
    }
  },
  {
    path: '/PuestosTrabajos/list',
    name: 'PuestosTrabajos',
    component: () => import('@/views/RecursosHumanos/WorkStation/WorkStationList.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Recursos Humanos'
    }
  },
  {
    path: '/PuestosTrabajos/:puestoTrabajoId',
    name: 'PuestoTrabajo-Edit',
    component: () => import('@/views/RecursosHumanos/WorkStation/WorkStationEdit.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Recursos Humanos'
    }
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})
router.beforeEach(async (to, from, next) => {
  const isLoggedIn = window.sessionStorage.getItem('isLogged')
  const role = window.sessionStorage.getItem('Role')
  const departamento = window.sessionStorage.getItem('Departamento')
  if (['Login'].includes(to.name) && isLoggedIn) {
    next({ name: 'Home' })
  } else if (to.meta.requiresAuth && !isLoggedIn) {
    next({
      path: '/Login',
      query: { redirect: to.fullPath }
    })
  } else if (to.meta.rol && to.meta.departamento && departamento !== to.meta.departamento && role !== 'Administrador') {
    next({ name: 'PageNotPermission' })
  } else {
    next()
  }
})
export default router
