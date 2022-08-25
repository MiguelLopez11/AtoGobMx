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
    path: '/Empleados/:EmpleadoId',
    name: 'Empleados-Edit',
    component: () => import('@/views/Employees/EmployeeEdit.vue')
  },
  {
    path: '/FallasAlumbrado/list',
    name: 'FallasAlumbrado',
    component: () => import('@/views/LightingFailures/LightingFailuresList.vue')
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router
