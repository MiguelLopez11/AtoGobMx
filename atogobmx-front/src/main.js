import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import BootstrapVue3 from 'bootstrap-vue-3'
import Vue3EasyDataTable from 'vue3-easy-data-table'
import Datepicker from '@vuepic/vue-datepicker'
import VueToast from 'vue-toast-notification'
import VueSweetalert2 from 'vue-sweetalert2'
import vSelect from 'vue-select'
import VueGoogleMaps from '@fawmi/vue-google-maps'

import 'vue-select/dist/vue-select.css'
import 'sweetalert2/dist/sweetalert2.min.css'
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
app.component('v-select', vSelect)
app.use(router).use(BootstrapVue3).use(VueToast).use(VueSweetalert2).use(VueGoogleMaps, {
  load: {
    key: 'AIzaSyAu9G_tPHiWvYnf-qkessxqcv4pQaJPhfY'
  }
}).mount('#app')
