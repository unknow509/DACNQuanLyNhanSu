<template>
  <div class="emp-compo"> 
      <b-col md="2" offset-md="10" >
        <b-button @click="createEmp" variant="outline-info">Create employee</b-button>
      </b-col>
    
    <br />
    
      <b-col md="12">
        <div class="table-responsive">
          <table class="table table-hover">
            <thead>
              <tr>
                <th>STT</th>
                <th>Name</th>
                <th>Sex</th>
                <th>Phone</th>
                <th>Address</th>
                <th>DOB</th>
                <th>Department</th>
                <th>Account</th>
                <th>PW</th>
                <th>Salary($)</th>
                <th>Update</th>
                <th>Delete</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(post, index) in posts" :key="post.maNhanVien">
                <td>{{ index+1 }}</td>
                <td>{{ post.hoTen }}</td>
                <td>{{post.gioiTinh}}</td>
                <td>{{ post.dienThoai }}</td>
                <td >{{ post.hoKhau }}</td>
                <td>{{ post.ngaySinh | formatDate }}</td>
                <td class="text-center">{{post.tenPhongBan}}</td>
                <td class="text-center">{{ post.tenDangNhap }}</td>
                <td  class="text-center">{{ post.matKhau }}</td>
                <td class="text-center">{{ post.thucLanh }}</td>
                <td>
                  <b-button variant="success" @click="updateEmp(post,index)">Update</b-button>
                </td>
                <td>
                  <b-button variant="danger" @click="deleteEmp(post.maNhanVien,index)">Delete</b-button>
                </td>
              </tr>
            </tbody>
          </table>
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
          <h4 v-show="!editMode" class="modal-title text-center">Tạo mới</h4>
          <h4 v-show="editMode" class="modal-title">Sửa</h4>
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
              <input
                v-model="form.gioiTinh"
                type="text"
                name="GioiTinh"
                placeholder="Giới tính"
                class="form-control"
              />
            </div>
            <div class="form-group">
              <input
                v-model="form.ngaySinh"
                type="text"
                name="NgaySinh"
                placeholder="Ngày sinh"
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
                type="text"
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
            <button v-show="!editMode" @click="createbtn($event)" type="submit" class="btn btn-primary">Tạo</button>
            <button v-show="editMode" @click="updateSubmit($event)" type="submit" class="btn btn-success">Sửa</button>
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
      posts: [],
      originPosts:[],
      positionId: [],
      tempId:null,
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

    updateEmp(nhanvien,index) {
      this.editMode = true;
      this.showModal = true;
      this.tempId=index;
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
        // this.posts[this.tempId]=this.form;  //cách 1
         this.posts[this.tempId]=res.data;
         this.posts[this.tempId].tenPhongBan=this.form.tenPhongBan;
         this.resetForm();
          //cách 2
      });
    },
    deleteEmp(id,index) {
      //console.log(id);
      this.tempId=index;
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
              this.posts.splice(this.tempId, 1);   
            });
          }
          //  
        });
    },
    //
    createbtn(event) {
      if(event) event.preventDefault();
      axios
        .post("http://localhost:61447/api/SampleData/create", this.form)
        .then(response => {
         // console.log(response);
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
        //console.log(res);
      })
      .catch(function(err) {
        console.log("Error: ", err);
      });
  }
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
  min-height: 100%;
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

</style>
