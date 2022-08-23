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
    path: '/Empleados/list',
    name: 'Empleados',
    component: () => import('@/views/Employees/EmployeeList.vue')
  },
  {
    path: '/Empleados/:empleadoId',
    name: 'ExpedienteEmpleados',
    component: () => import('@/views/Employees/EmployeeExpedient.vue')
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router
