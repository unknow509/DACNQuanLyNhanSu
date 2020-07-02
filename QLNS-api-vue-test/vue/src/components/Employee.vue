<template>
  <div class="emp-compo"> 
      <b-col md="2" offset-md="10" >
        <b-button @click="createEmp" variant="outline-info"><b-icon icon="person-plus"></b-icon>Create employee</b-button>
      </b-col>
      <b-col md="12">
        <div class="table-responsive">
           <b-table id="table" striped hover :items="posts" :fields="postFields" :per-page="perPage" :current-page="currentPage">
             <template v-slot:cell(ngaySinh)="data"> 
                 {{ data.item.ngaySinh | formatDate }}
             </template>
             <template v-slot:cell(actionUpdate)="data"> 
                  <b-button variant="success" @click="updateEmp(data.item)"><b-icon icon="pencil"></b-icon>Update</b-button>
             </template> 
                    <template v-slot:cell(actionDelete)="data"> 
                  <b-button variant="danger" @click="deleteEmp(data.item.maNhanVien)"><b-icon icon="x-circle"></b-icon>Delete</b-button>
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
          <h4 v-show="!editMode" class="modal-title text-center">CREATE</h4>
          <h4 v-show="editMode" class="modal-title">MODIFY</h4>
        </div>
        <form>
          <!-------------------------------------------- Modal body ------------------------------>
          <div class="modal-body row">
            <div class="col-6">
              <div class="form-group">
              <input
                v-model="form.hoTen"
                type="text"
                name="HoTen"
                placeholder="Họ tên"
                class="form-control"
              />
            </div>
            <div class="form-group">
              <input
                v-model="form.soCMND"
                type="text"
                name="SoCMND"
                placeholder="Số CMND"
                class="form-control"
                maxlength="10"
              />
            </div>
            <div class="form-group">
              <b-form-select v-model="form.gioiTinh" name="gioiTinh" >
                 <option disabled value="">Giới tính</option>
                <option value="nam">Nam</option>
                <option value="nu">Nữ</option>
              </b-form-select>
            </div>
            <div class="form-group">
              <input
                v-model="form.ngaySinh"
                type="text"
                name="NgaySinh"
                placeholder="Ngày sinh (yyyy/mm/dd)"
                class="form-control"
              />
            </div>
            <div class="form-group">
              <input
                v-model="form.noiSinh"
                type="text"
                name="NoiSinh"
                placeholder="Nơi sinh"
                class="form-control"
              />
            </div>
            <div class="form-group">
              <input
                v-model="form.hoKhau"
                type="text"
                name="HoKhau"
                placeholder="Hộ khẩu"
                class="form-control"
              />
            </div>
            
            </div>
            <div class="col-6">
              <div class="form-group">
              <input
                v-model="form.dienThoai"
                type="text"
                name="DienThoai"
                placeholder="Số điện thoại"
                class="form-control"
                maxlength="10"
              />
            </div>
               <div class="form-group">
                 <select  placeholder="Phòng ban" class="form-control" v-model="form.maPhongBan" name="maPhongBan" id="maPhongBan">
                   <option value disabled>chọn phòng ban</option>
                    <option value="1">Ban quản lý</option>
                    <option value="2">Team Developer</option>
                    <option value="3">Tạp vụ</option>
                    <option value="4">Team Designer</option>
                 </select>
              
            </div>
            <div class="form-group">
              <select
                name="MaChucVu"
                v-model="form.maChucVu"
                id="MaChucVu"
                placeholder="Mã chức vụ"
                class="form-control"
              >
                <option value disabled>Chọn chức vụ</option>
                <option
                  v-for="(id, index) in positionId"
                  :key="index"
                  :value="id.maChucVu"
                >{{ id.maChucVu }}</option>
              </select>
            </div>
            <div class="form-group">
              <input
                v-model="form.tenDangNhap"
                type="text"
                name="TenDangNhap"
                placeholder="Tên đăng nhập"
                class="form-control"
              />
            </div>
            <div class="form-group">
              <input
                v-model="form.matKhau"
                type="password"
                name="MatKhau"
                placeholder="Mật khẩu"
                class="form-control"
              />
            </div>
            <!-- <div v-show="editMode" class="form-group">
              <input
                v-model="form.thucLanh"
                type="text"
                name="Luong"
                placeholder="Lương"
                class="form-control"
              />
            </div> -->
            </div>
            
            
          </div>
          <!--------------------------------------------Modal footer ---------------------------->
          <div class="modal-footer">
            
            <b-button v-show="!editMode" @click="resetForm" variant="warning">Reset</b-button>
            <button v-show="!editMode" @click="createbtn($event)" type="submit" class="btn btn-primary">Create</button>
            <button v-show="editMode" @click="updateSubmit($event)" type="submit" class="btn btn-success">Modify</button>
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
        // {
        //   key:'tenChucVu',
        // label:'Position',
        // sortable:true
        // },
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
          key:'actionUpdate',
        label:'Update',
        sortable:false
        },
        {
          key:'actionDelete',
        label:'Delete',
        sortable:false
        },
        
      ],
      originPosts:[],
      positionId: [],   
      showModal: false,
      editMode: true,
      deleteMode: false,
      form: {
        maNhanVien: 0,
        hoTen: null,
        gioiTinh: null,
        soCMND: null,
        ngaySinh: null,
        noiSinh: null,
        hoKhau: null,
        dienThoai: null,
        maPhongBan: null,
        maChucVu: null,
        tenDangNhap: null,
        matKhau: null,
        thucLanh:null,
        tenPhongBan:null
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
      (this.form.hoTen = ""),
        (this.form.gioiTinh = ""),
        (this.form.soCMND = ""),
        (this.form.ngaySinh = ""),
        (this.form.noiSinh = ""),
        (this.form.hoKhau = ""),
        (this.form.dienThoai = ""),
        (this.form.maPhongBan = ""),
        (this.form.maChucVu = ""),
        (this.form.tenDangNhap = ""),
        (this.form.maNhanVien=0),
        (this.form.matKhau = "");
    },
    callPosition() {
      axios
        .get("http://localhost:61447/api/SampleData/CallPosition")
        .then(res => {
          this.positionId = res.data;
        })
        .catch(e => {
          console.log(e + "call chức vụ thất bại");
        });
    },
    createEmp() {
      this.editMode = false;
      this.showModal = true;
      this.resetForm();
    },
    updateEmp(nhanvien) {
      this.editMode = true;
      this.showModal = true;
   
      (this.form.maNhanVien = nhanvien.maNhanVien),
        (this.form.hoTen = nhanvien.hoTen),
        (this.form.gioiTinh = nhanvien.gioiTinh),
        (this.form.soCMND = nhanvien.soCmnd),
        (this.form.ngaySinh = nhanvien.ngaySinh),
        (this.form.noiSinh = nhanvien.noiSinh),
        (this.form.hoKhau = nhanvien.hoKhau),
        (this.form.dienThoai = nhanvien.dienThoai),
        (this.form.maPhongBan = nhanvien.maPhongBan),
        (this.form.maChucVu = nhanvien.maChucVu),
        (this.form.tenDangNhap = nhanvien.tenDangNhap),
        (this.form.matKhau = nhanvien.matKhau);
        (this.form.thucLanh = nhanvien.thucLanh),
        (this.form.tenPhongBan=nhanvien.tenPhongBan);
    },
    updateSubmit(event) {
       if (event) event.preventDefault()
      axios.put("/api/SampleData/update", this.form).then(res => {
        this.$bvModal.hide('modalFormAdmin')
        //console.log(res)
        const {data} =res;
        const post = this.posts.find(post=>post.maNhanVien==data.maNhanVien);
        //console.log(post);
        post.hoTen = data.hoTen;
        post.gioiTinh = data.gioiTinh;
        post.ngaySinh = data.ngaySinh;
        post.dienThoai = data.dienThoai;
        post.hoKhau = data.hoKhau;
        post.tenPhongBan = data.tenPhongBan;
        post.tenDangNhap = data.tenDangNhap;
        post.thucLanh = data.thucLanh;
         this.resetForm();         
      });
    },
    deleteEmp(id) {
      //console.log(id);
      swalWithBootstrapButtons
        .fire({
          title: "Are you sure?",
          text: "You won't be able to revert this!",
          icon: "warning",
          showCancelButton: true,
           cancelButtonText: "No, cancel!",
          confirmButtonText: "Yes, delete it!",
          reverseButtons: false
        })
        .then(result => {
          if (result.value) {
            axios.delete("/api/SampleData/delete/" + id).then(() => {
              swalWithBootstrapButtons.fire(
                "Deleted!",
                "Your file has been deleted.",
                "success"
              );
              this.posts = this.posts.filter(post => post.maNhanVien !=id);   
            });
          }
        });
    },
    createbtn(event) {
      if(event) event.preventDefault();
      axios
        .post("http://localhost:61447/api/SampleData/create", this.form)
        .then(response => {
          //console.log(response);
          this.$bvModal.hide('modalFormAdmin');                   
          this.posts.push(response.data[0]);
        })
        .catch(e => {          
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
    this.callPosition();
    EventBus.$on('user-searched', data => {
     this.getQuery(data)
    });
  },
  mounted() {
    this.$http
      .get("http://localhost:61447/api/SampleData/findall")
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
.emp-compo{
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
