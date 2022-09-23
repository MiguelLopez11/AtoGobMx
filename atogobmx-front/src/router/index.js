import { createRouter, createWebHistory } from 'vue-router'

const routes = [
  {
    path: '/Login',
    name: 'LogIn',
    component: () => import('../views/LogIn/LogIn.vue')
  },
  {
    path: '/',
    name: 'home',
    component: () => import('../views/Home/HomePage.vue')
  },
  {
    path: '/Areas/List',
    name: 'Areas',
    component: () => import('../views/RecursosHumanos/areas/AreaList.vue')
  },
  {
    path: '/Areas/:AreaId',
    name: 'Area-Edit',
    component: () => import('../views/RecursosHumanos/areas/AreaEdit.vue')
  },
  {
    path: '/Empleados/list',
    name: 'Empleados',
    component: () => import('@/views/RecursosHumanos/Employees/EmployeeList.vue')
  },
  {
    path: '/Empleados/:EmpleadoId',
    name: 'Empleados-Edit',
    component: () => import('@/views/RecursosHumanos/Employees/EmployeeEdit.vue')
  },
  {
    path: '/ExpedientesDigitales/list',
    name: 'ExpedientesDigitales',
    component: () => import('@/views/RecursosHumanos/ExpedienteDigital/ExpedientDigitalList.vue')
  },
  {
    path: '/ExpedientesDigitales/:ExpedienteDigitalId',
    name: 'ExpedienteDigital-edit',
    component: () => import('@/views/RecursosHumanos/ExpedienteDigital/ExpedientDigitalEdit.vue')
  },
  {
    path: '/FallasAlumbrado/list',
    name: 'FallasAlumbrado',
    component: () => import('@/views/LightingFailures/LightingFailuresList.vue')
  },
  {
    path: '/FallasAlumbrado/:FallaId',
    name: 'FallasAlumbrado-Edit',
    component: () => import('@/views/LightingFailures/EditGlitches.vue')
  },
  {
    path: '/Usuarios/list',
    name: 'Usuarios',
    component: () => import('@/views/Users/UserList.vue')
  },
  {
    path: '/Usuarios/:usuarioId',
    name: 'Usuarios-Edit',
    component: () => import('@/views/Users/UserEdit.vue')
  },
  {
    path: '/Roles/list',
    name: 'Roles',
    component: () => import('@/views/Roles/RoleList.vue')
  },
  {
    path: '/Departamentos/list',
    name: 'Departamentos',
    component: () => import('@/views/RecursosHumanos/Departaments/DepartamentList.vue')
  },
  {
    path: '/Departamentos/:DepartamentoId',
    name: 'Departamentos-Edit',
    component: () => import('@/views/RecursosHumanos/Departaments/DepartamentEdit.vue')
  },
  {
    path: '/PuestosTrabajos/list',
    name: 'PuestosTrabajos',
    component: () => import('@/views/RecursosHumanos/WorkStation/WorkStationList.vue')
  },
  {
    path: '/PuestosTrabajos/:puestoTrabajoId',
    name: 'PuestoTrabajo-Edit',
    component: () => import('@/views/RecursosHumanos/WorkStation/WorkStationEdit.vue')
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router
