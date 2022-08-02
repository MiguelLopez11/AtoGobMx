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
    component: () => import('../views/Employees/EmployeeList.vue')
  },
  {
    path: '/AlumbradoPublico/list',
    name: 'AlumbradoPublico',
    component: () => import('../views/lighting_failures/LightingFailuresList')
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
