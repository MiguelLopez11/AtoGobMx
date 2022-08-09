import { createRouter, createMemoryHistory } from 'vue-router'

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
  }
]

const router = createRouter({
  history: createMemoryHistory(),
  routes
})

export default router
