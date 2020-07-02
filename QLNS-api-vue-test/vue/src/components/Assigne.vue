<template>
  <div class="ass-compo"> 
      
      <b-col md="12">
        <div class="table-responsive">
           <b-table id="table" striped hover :items="posts" :fields="postFields" :per-page="perPage" :current-page="currentPage">
             <template v-slot:cell(ngaySinh)="data"> 
                 {{ data.item.ngaySinh | formatDate }}
             </template>
             <template v-slot:cell(actionassigne)="data"> 
                  <b-button variant="success" @click="assigneEmp(data.item)"><b-icon icon="pencil"></b-icon>Assigne</b-button>
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
    <!-------------------------------------------- Modal Start -------------------------------->
    <div>
      <b-modal
        id="modalFormAdmin"
        centered
        title="BootstrapVue"
        v-model="showModal"
        hide-footer
        hide-header
      >
        <!----------------------------------------------- Modal Header ------------------------------------->
        <div class="modal-header">
          <h4 class="modal-title">Description</h4>
        </div>
        <form>
          <!-------------------------------------------- Modal body ------------------------------>
          <div class="modal-body ">
           
              <div class="form-group">
                 <label class="text-center " ><small>Employee Id</small></label>
              <input
                v-model="form.maNhanVien"
                type="text"
                name="maNhanVien"
                disabled
                class="form-control"
              />
            </div>
              <div class="form-group">
                 <label class="text-center " ><small>Task description</small></label>
              <input
                v-model="form.noiDungCongViec"
                type="text"
                name="noiDungCongViec"
                placeholder=""
                class="form-control"
              />
            </div>
              <div class="form-group">
                <label class="text-center " ><small>Time start</small></label>
              <input
                v-model="form.thoiGianBatDau"
                type="text"
                name="thoiGianBatDau"
                disabled
                class="form-control"
              />
            </div>
            <div class="form-group">
              <label class="text-center " ><small>Deadline</small></label>
            <input
                v-model="form.thoiGianKetThuc"
                type="text"
                name="thoiGianKetThuc"
                 placeholder="yyyy/MM/dd"
                class="form-control"
              />
            </div>
            <div class="form-group">
              <label class="text-center " ><small>Process</small></label>
            <input
                v-model="form.tienDo"
                type="text"
                name="tienDo"
                disabled
                class="form-control"
              />
            </div>
            
          </div>
          <!--------------------------------------------Modal footer ---------------------------->
          <div class="modal-footer">
            
            <b-button  @click="resetForm" variant="warning">Reset</b-button>
            <button  @click="assigneSubmit($event)" type="submit" class="btn btn-success">Assigne</button>
            <button
              type="button"
              class="btn btn-danger"
              data-dismiss="modal"
              @click="$bvModal.hide('modalFormAdmin')"
            >Close</button>
          </div>
        </form>
      </b-modal>
    </div>
    <!---------------------------------------- Modal End -------------------------------------------------->
  </div>
</template>

<script>

export default {
  data() {
    return {
      currentPage:1,
      perPage: 7,
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
          key:'dienThoai',
        label:'Phone',
        sortable:false
        },
        {
          key:'hoKhau',
        label:'Address',
        sortable:false
        },
        {
          key:'ngaySinh',
        label:'DOB',
        sortable:true
        },
        {
          key:'tenPhongBan',
        label:'Department',
        sortable:true
        },
        {
          key:'tenDangNhap',
        label:'Account',
        sortable:false
        },
        {
          key:'thucLanh',
        label:'Salary$',
        sortable:true
        },
        {
          key:'actionAssigne',
        label:'Assigne',
        sortable:false
        },
        
        
      ],
      originPosts:[],
      positionId: [],   
      showModal: false,
      form: {
        noiDungCongViec:null,
        maNhanVien: 0,
        thoiGianBatDau:null,
        thoiGianKetThuc:null,
        tienDo:"Pending"
      }
    };
  },
  computed:{
    rows(){
      return this.posts.length
    },

  },
  methods: {
    resetForm() {
      (this.form.noiDungCongViec = ""),
        (this.form.thoiGianBatDau = ""),
        (this.form.thoiKetThuc = "")       
    },
    fillInForm() {
      var dateNow = new Date();
      var tempTimeStart = this.$options.filters.formatYMDT(dateNow);
      this.form.thoiGianBatDau = tempTimeStart;
     
    },

    assigneEmp(nhanvien) {
      this.showModal = true;  
      (this.form.maNhanVien = nhanvien.maNhanVien);
        this.fillInForm();
    },
    assigneSubmit(event) {
        if (event) event.preventDefault()
      axios.post("http://localhost:61447/api/Assigne/assigne", this.form).then(res => {
        this.$bvModal.hide('modalFormAdmin')
         this.resetForm();         
      });
    },
    getQuery(data){
     this.posts = this.originPosts.filter((item)=>{
       return item.hoTen.toLowerCase().includes(data.toLowerCase());
     });
        //console.log(this.posts)     
    }
  },
  created() {
    EventBus.$on('user-searched', data => {
     this.getQuery(data)
    });
    
  },
  mounted() {
    this.$http
      .get("http://localhost:61447/api/Assigne/findallassigne/"+this.$store.state.userInfo.maPhongBan)
      .then(function(res) {
        this.posts = res.body;
        this.originPosts= res.body;
        // console.log(res);
      })
      .catch(function(err) {
        console.log("Error: ", err);
      });
      
  },

};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.ass-compo{
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
td{
  text-align: left;
}
th{
  text-align: center;
}
.modal-header {
  text-align: left;
}
button.swal2-cancel.btn.btn-danger {
  margin-right: 6em;
}
.modal{
  width: 60%;
}
.paging{
      position: absolute;
    top: 32em;
    left: 37em;
}
.modal-footer button:first-child {
    margin-right: 14em;
}
</style>
