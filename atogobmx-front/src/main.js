import { createApp } from 'vue'
import App from './App.vue'
import './registerServiceWorker'
import router from './router'
import BootstrapVue3 from 'bootstrap-vue-3'
import Vue3EasyDataTable from 'vue3-easy-data-table'
import Datepicker from '@vuepic/vue-datepicker'
import VueToast from 'vue-toast-notification'
import VeeValidate from 'vee-validate'

import 'vue-neat-modal/dist/vue-neat-modal.css'
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue-3/dist/bootstrap-vue-3.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import '@vuepic/vue-datepicker/dist/main.css'
import 'vue3-easy-data-table/dist/style.css'
import 'vue-toast-notification/dist/theme-sugar.css'

const app = createApp(App)
app.component('date-picker', Datepicker)
app.component('EasyDataTable', Vue3EasyDataTable)
app.use(router).use(BootstrapVue3).use(VueToast).use(VeeValidate).mount('#app')
