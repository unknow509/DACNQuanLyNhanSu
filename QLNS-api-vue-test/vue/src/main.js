import Vue from 'vue'
import router from './router'
import jquery from 'jquery'
import BootstrapVue from 'bootstrap-vue'
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import axios from 'axios'
import VueResource from 'vue-resource'
import App from './App.vue'
import moment from 'moment'
import Swal from 'sweetalert2'
import {store} from './store/store'
//declare global jquery
window.$ = jquery;
window.EventBus = new Vue()
//=======================
Vue.use(BootstrapVue)
Vue.use(router)
window.axios = axios
Vue.use(VueResource)
//Sweet Alert 2
window.Swal = Swal;
const toast = Swal.mixin({
    toast: true,
    position: 'top-end',
    showConfirmButton: false,
    timer: 3000
});
window.toast = toast
const swalWithBootstrapButtons = Swal.mixin({
  customClass: {
    confirmButton: "btn btn-success",
    cancelButton: "btn btn-danger"
  },
  buttonsStyling: false
});
window.swalWithBootstrapButtons = swalWithBootstrapButtons
Vue.config.productionTip = false
Vue.filter('formatDate', function(value){
  if (value) {
    return moment(String(value)).format('DD/MM/YYYY')
  }
})
Vue.filter('formatTime', function(value){
  if (value) {
    return moment(String(value)).format(' kk : mm : ss ')
  }
})
Vue.filter('formatYMDT', function(value){
  if (value) {
    return moment(String(value)).format('YYYY/MM/DD kk:mm:ss')
  }
})
Vue.filter('formatDay', function(value){
  if (value) {
    return moment(String(value)).format('YYYY/MM/DD')
  }
})
new Vue({
  router,
  store,
  render: h => h(App),
}).$mount('#app')