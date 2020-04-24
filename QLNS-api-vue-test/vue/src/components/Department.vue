<template >
  <div>
    <b-row>
      <b-col md="2" offset-md="10" >
        <b-button class @click="createDep" variant="outline-info">Create department</b-button>
      </b-col>
    </b-row>
    <b-row>
      <b-col md="12">
        <div class="table-responsive">
          <table class="table table-hover">
            <thead>
              <tr>
                <th>STT</th>
                <th>Name </th>
                <th>Update</th>
                <th>Delete</th>
                
              </tr>
            </thead>
            <tbody>
              <tr v-for="(phongban,index) in phongbans" :key="phongban.maPhongban">
                <td>{{ ++index }}</td>
                <td>{{ phongban.tenPhongBan }}</td>
                <td>
                  <b-button variant="success" @click="updateDep(phongban)">Update</b-button>
                </td>
                <td>
                  <b-button variant="danger" @click="deleteDep(phongban.maPhongBan)">Delete</b-button>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </b-col>
    </b-row>
     <!--------------------------------------- Modal Start ---------------------------------->
    <div>
      <b-modal id="modalFormAdmin" centered title="BootstrapVue" v-model="showModal" hide-footer hide-header>
        <!-- Modal Header -->
        <div class="modal-header" >
          <h4 v-show="!editMode" class="modal-title">Tạo mới</h4>
          <h4 v-show="editMode" class="modal-title">Sửa</h4>
        </div>
        <form>
          <!---------------------------------- Modal body ---------------------------------->
          <div class="modal-body">
            
            <div class="form-group">
              <input
                v-model="form.TenPhongBan"
                type="text"
                name="TenPhongBan"
                placeholder="Tên phòng ban"
                class="form-control"
              />
            </div>

          </div>
          <!----------------------------------Modal footer ---------------------------------->
          <div class="modal-footer">
            <button v-show="!editMode" @click="createSubmit" type="submit" class="btn btn-primary">Tạo</button>
            <button v-show="editMode" @click="updateSubmit" type="submit" class="btn btn-success">Sửa</button>
            <button type="button" class="btn btn-danger" data-dismiss="modal" @click="$bvModal.hide('modalFormAdmin')">Close</button>
          </div>
        </form>
      </b-modal>

    </div>
    <!---------------------------------- Modal End ---------------------------------->
    
  </div>
</template>
<script>
export default {
  data(){
    return{
      phongbans:[],
      showModal: false,
      editMode: true,
      deleteMode:false,
      form:{
        MaPhongBan:'',
        TenPhongBan:''
      }
    };
  },
  methods: {
    resetForm(){
      this.TenPhongBan=''
    },
    createDep() {
      this.editMode = false
      this.showModal = true
      this.resetForm()

    },
    updateDep(name){
      this.editMode = true
      this.showModal = true
      this.form.TenPhongBan = name.tenPhongBan
      this.form.MaPhongBan = name.maPhongBan
    },
    updateSubmit(){
      axios.put("/api/SampleData/UpdateDeparment" , this.form)
    },
    // deleteDep(){
    //   this.deleteMode = true
    // },
    createSubmit(){
      axios.post("/api/SampleData/CreateDeparment",this.form).then(()=>{
       this.$router.push('/department');
      })
     
    },   
    deleteDep(idDep) {
     console.log(idDep)
      swalWithBootstrapButtons
        .fire({
          title: "Are you sure?",
          text: "You won't be able to revert this!",
          icon: "warning",
          showCancelButton: true,
          confirmButtonText: "Yes, delete it!",
          cancelButtonText: "No, cancel!",
          reverseButtons: true
        })
        .then(result => {
          if (result.value) {
            axios.delete("/api/SampleData/DeleteDeparment/" + idDep).then(()=>{

                swalWithBootstrapButtons.fire(
                "Deleted!",
                "Your file has been deleted.",
                "success"
                );
                setTimeout(function(){this.$router.push('/department')}, 1000);    //location.reload().then(()=>{this.$router.push('/department')})            
                
            })
          } 
          else if (
            /* Read more about handling dismissals below */
            result.dismiss === Swal.DismissReason.cancel
          ) {
            swalWithBootstrapButtons.fire(
              "Cancelled",
              "Your imaginary file is safe :)",
              "error"
            );
          }
        });
    }
  },
   mounted() {
      this.$http
      .get("http://localhost:61447/api/SampleData/findPhongBan")
      .then(function(resPhongBan) {
        this.phongbans = resPhongBan.body;
        //console.log(resPhongBan);
      })
      .catch(function(err) {
        console.log("Error: ", err);
      });
  }
  
}
</script>
<style  scoped>
.col-md-12 {
    height: 100vh;
}
</style>