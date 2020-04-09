<template>
  <div id="app">   
    <Navbar/>
    <router-view />
  </div>
</template>

<script>
import Navbar from './components/Navbar'
import {mapActions} from 'vuex';
export default {
  components:{Navbar},
  name: "App",
  data() {
    return {
      
    };
  },
  methods: {
  ...mapActions(['docheckLogin']),
  },
  created(){
    this.docheckLogin();
  },
};
</script>

<style>
@import url("https://fonts.googleapis.com/css?family=Montserrat:400,700");

body {
  background-color: #eeeeee;
  font-family: "Montserrat", sans-serif;
}
body,
html {
  margin: 0;
  height: 100%;
}

#nav-menu ul li a {
  color: #313131;
  font-size: larger;
  text-transform: uppercase;
}
#nav-menu ul li a:hover {
  color: orangered !important ;
  text-decoration: none;
}
#logout {
  margin: 0px 0.5em;
}
</style>
