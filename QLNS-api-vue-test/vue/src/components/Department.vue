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
                <td>{{ index+1 }}</td>
                <td>{{ phongban.tenPhongBan }}</td>
                <td>
                  <b-button variant="success" @click="updateDep(phongban,index)">Update</b-button>
                </td>
                <td>
                  <b-button variant="danger" @click="deleteDep(phongban.maPhongBan,index)">Delete</b-button>
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
                v-model="form.tenPhongBan"
                type="text"
                name="tenPhongBan"
                placeholder="Tên phòng ban"
                class="form-control"
              />
            </div>

          </div>
          <!----------------------------------Modal footer ---------------------------------->
          <div class="modal-footer">
            <button v-show="!editMode" @click="createSubmit($event)" type="submit" class="btn btn-primary">Tạo</button>
            <button v-show="editMode" @click="updateSubmit($event)" type="submit" class="btn btn-success">Sửa</button>
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
      tempId:null,
      showModal: false,
      editMode: true,
      deleteMode:false,
      form:{
        maPhongBan:'',
        tenPhongBan:''
      },
    };
  },
  methods: {
    resetForm(){
      this.tenPhongBan=''
    },
    createDep() {
      this.editMode = false
      this.showModal = true
      this.resetForm();

    },
    updateDep(name,index){
      this.editMode = true
      this.showModal = true
      this.tempId=index;
      this.form.tenPhongBan = name.tenPhongBan
      this.form.maPhongBan = name.maPhongBan
    },
    updateSubmit(event){
      if(event) event.preventDefault();
      axios.put("/api/SampleData/UpdateDeparment" , this.form).then(res=>{
        this.$bvModal.hide('modalFormAdmin')
         this.phongbans[this.tempId]=res.data;
      })
    },
    createSubmit(event){
      if(event) event.preventDefault();
      axios.post("/api/SampleData/CreateDeparment",this.form).then(res=>{
        this.$bvModal.hide('modalFormAdmin');
        this.phongbans.push(res.data)
        })
     
    },   
    deleteDep(idDep,index){
     this.tempId=index;
      swalWithBootstrapButtons
        .fire({
          title: "Are you sure?",
          text: "You won't be able to revert this!",
          icon: "warning",
          showCancelButton: true,
          confirmButtonText: "Yes, delete it!",
          cancelButtonText: "No, cancel!",
        })
        .then(result => {
          if (result.value) {
            axios.delete("/api/SampleData/DeleteDeparment/" + idDep).then((res)=>{
                swalWithBootstrapButtons.fire(
                "Deleted!",
                "Your file has been deleted.",
                "success"
                );
                this.phongbans.splice(this.tempId, 1);                       
            })
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