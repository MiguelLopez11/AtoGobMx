import Vue from "vue";
import VueRouter from "vue-router";

Vue.use(VueRouter);

const routes = [
  {
    path: "/Login",
    name: "LogIn",
    component: () => import("../views/LogIn/LogIn.vue"),
  },
  {
    path: "/",
    name: "home",
    component: () => import("../views/Home/HomePage.vue"),
  },
  {
    path: "/Empleados/list",
    name: "Empleados",
    component: () => import("@/views/Employees/EmployeeList.vue"),
  },
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes,
});

export default router;
