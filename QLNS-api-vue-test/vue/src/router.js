import Vue from 'vue'
import Router from 'vue-router'
import Employee from './components/Employee.vue'
import Department from './components/Department.vue'
import Login from './components/Login.vue'
import CheckIn from './components/CheckIn.vue'
import Pending from './components/Pending.vue'
import HomeEmp from './components/HomeEmp.vue'
import PersonalInfo from './components/PersonalInfo.vue'

Vue.use(Router)

export default new Router({
    routes: [

    {
      path: '/',
      name: 'employee',
      component: Employee
    },
    {
      path: '/department',
      name: 'department',  
      component: Department
    },
    {
        path: '/login',
        name: 'login',
        component: Login
    },
    {
      path: '/checkin',
      name: 'checkin',
      component: CheckIn
  },
  {
    path: '/pending',
    name: 'pending',
    component: Pending
  },
  {
    path:'/homeemp',
    name: 'homeemp',
    component: HomeEmp
  },
  {
    path:'/personalinfo',
    name: 'personalinfo',
    component: PersonalInfo
  },
  
  
    ],
     mode: 'history'
})