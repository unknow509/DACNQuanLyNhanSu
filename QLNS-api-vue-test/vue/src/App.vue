<template>
  <div id="app">
    <Navbar />
    <router-view />
  </div>
</template>

<script>
import Navbar from "./components/Navbar";
import { mapActions } from "vuex";
export default {
  components: { Navbar },
  name: "App",
  data() {
    return {};
  },
  methods: {
    ...mapActions(["docheckLogin"])
  },
  created() {
    this.docheckLogin();
  }
};
</script>

<style>
@import url("https://fonts.googleapis.com/css?family=Montserrat:400,700");
#app {
  font-family: "Montserrat", sans-serif;
  background-image: url("./assets/bg.png");
  background-attachment: fixed;
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover;
  height: 90,9vh !important;
}

#nav-menu ul li a {
  color: aliceblue;
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
th,
td {
  color: #fff;
 
}
thead{
   background: #243e59;
}
button.swal2-cancel.btn.btn-danger {
    margin-left: 2.3rem;
}
</style>
