<template>
  <div class="homeEmp-compo">
    <br />
  <b-col class="table-center" md="12">
        <div class="table-responsive">
           <b-table id="table" striped hover :items="posts" :fields="postFields" :per-page="perPage" :current-page="currentPage">
             <template v-slot:cell(ngaySinh)="data"> 
                 {{ data.item.ngaySinh | formatDate }}
             </template>
           </b-table>
           <b-pagination
                class="paging"
                v-model="currentPage"
                :per-page="perPage"
                :total-rows="rows"
              >
                <template v-slot:first-text><span class="text-success">First</span></template>
                <template v-slot:prev-text><span class="text-danger">Prev</span></template>
                <template v-slot:next-text><span class="text-warning">Next</span></template>
                <template v-slot:last-text><span class="text-info">Last</span></template>              
                <template v-slot:page="{ page, active }">
                  <b v-if="active">{{ page }}</b>
                  <i v-else>{{ page }}</i>
                </template>
            </b-pagination>        
        </div>
  </b-col>
     
    
</div>
</template>

<script>

export default {
  data() {
    return {
      currentPage:1,
      perPage: 8,
      posts: [],
       postFields:[
        {
          key:'hoTen',
        label:'Name',
        sortable:true
        },
        {
          key:'gioiTinh',
        label:'Gender',
        sortable:true
        },
        {
          key:'ngaySinh',
        label:'DOB',
        sortable:true
        },
        {
          key:'noiSinh',
        label:'Place',
        sortable:true
        },
        {
          key:'tenPhongBan',
        label:'Department',
        sortable:true
        },{
          key:'tenChucVu',
        label:'Position',
        sortable:true
        }
        ]
    };
  },
  methods: {

  },
  computed:{
    rows(){
      return this.posts.length
    },
  },
  mounted() {
    this.$http
      .get("http://localhost:61447/api/SampleData/findall")
      .then(function(res) {
        this.posts = res.body;
       // console.log(res);
      })
      .catch(function(err) {
        console.log("Error: ", err);
      });
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.homeEmp-compo{
 font-family: "Montserrat", sans-serif;
  background-image: url("/src/assets/bg.png"); 
  background-attachment: fixed;
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover;
  min-height: 91%;
  min-width: 100%;
  position: absolute;
  top: 66px;
  left: 0px;
}
.paging{
      position: absolute;
    top: 32em;
    left: 37em;
}
.table-center{
  width: 98%;
  margin:auto;
}
th{
  text-align: center;
}
td:not(:nth-child(5)){
  text-align: center;
}
.modal-header {
  text-align: center;
}
button.swal2-cancel.btn.btn-danger {
  margin-right: 6em;
}
</style>
