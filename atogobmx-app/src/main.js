import Vue from "vue";
import App from "./App.vue";
import "./registerServiceWorker";
import router from "./router";
import { BootstrapVue, IconsPlugin } from "bootstrap-vue";
import vSelect from "vue-select";
import { FormSelectPlugin } from "bootstrap-vue";

Vue.component("v-select", vSelect);
Vue.use(FormSelectPlugin);
Vue.config.productionTip = false;

Vue.use(BootstrapVue);
Vue.use(IconsPlugin);

import "bootstrap/dist/css/bootstrap.css";
import "bootstrap-vue/dist/bootstrap-vue.css";

new Vue({
  router,
  render: (h) => h(App),
}).$mount("#app");
