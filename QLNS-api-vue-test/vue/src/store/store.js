import Vue from 'vue'
import Vuex from 'vuex'
import router from '../router'
Vue.use(Vuex)

export const store = new Vuex.Store({
    state: {
      isLoggedIn: false,
      isEmployee:false,
      userInfo:{}
    },
    getters:{
      
     },
    actions:{
      docheckLogin({commit}) {
        var checkLocalStored = JSON.parse(localStorage.getItem("user_loggedin"));
        if (checkLocalStored != null) {
          commit('SET_ISLOGIN',true);
          if(this.state.userInfo.maChucVu=='nv'){
            router.push("/homeemp").catch(err => {});
          }
          else{
            router.push("/").catch(err => {});
          }        
        } else {
          commit('SET_ISLOGIN',false)
          router.push("/login").catch(err => {});
        }
      },
      doCheckEmp({commit}){
          if( this.state.userInfo.maChucVu == 'nv'){
            commit('SET_EMPLOYEE',true)
          }
          else{
            commit('SET_EMPLOYEE',false)
          }             
      }
     },
    mutations:{
      SET_ISLOGIN:(state,isLoggedIn)=>{
        state.isLoggedIn=isLoggedIn
      },
      SET_EMPLOYEE:(state,isEmployee)=>{
        state.isEmployee=isEmployee
      },
    },
  });