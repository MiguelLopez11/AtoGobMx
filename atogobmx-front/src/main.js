import { createApp } from 'vue'
import App from './App.vue'
import './registerServiceWorker'
import router from './router'
import BootstrapVue3 from 'bootstrap-vue-3'
import Vue3EasyDataTable from 'vue3-easy-data-table'
import Datepicker from '@vuepic/vue-datepicker'
import { vfmPlugin } from 'vue-final-modal'

import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue-3/dist/bootstrap-vue-3.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import '@vuepic/vue-datepicker/dist/main.css'
import 'vue3-easy-data-table/dist/style.css'

const app = createApp(App)
app.component('date-picker', Datepicker)
app.component('EasyDataTable', Vue3EasyDataTable)
app.use(vfmPlugin({
  key: '$vfm',
  componentName: 'VueFinalModal',
  dynamicContainerName: 'ModalsContainer'
}))
app.use(router).use(BootstrapVue3).mount('#app')
