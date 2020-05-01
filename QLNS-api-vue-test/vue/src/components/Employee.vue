<template>
  <div >
    
      <b-col md="2" offset-md="10">
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
                v-model="form.HoTen"
                type="text"
                name="HoTen"
                placeholder="Họ tên"
                class="form-control"
              />
            </div>
            <div class="form-group">
              <input
                v-model="form.SoCMND"
                type="text"
                name="SoCMND"
                placeholder="Số CMND"
                class="form-control"
                maxlength="10"
              />
            </div>
            <div class="form-group">
              <input
                v-model="form.GioiTinh"
                type="text"
                name="GioiTinh"
                placeholder="Giới tính"
                class="form-control"
              />
            </div>
            <div class="form-group">
              <input
                v-model="form.NgaySinh"
                type="text"
                name="NgaySinh"
                placeholder="Ngày sinh"
                class="form-control"
              />
            </div>
            <div class="form-group">
              <input
                v-model="form.NoiSinh"
                type="text"
                name="NoiSinh"
                placeholder="Nơi sinh"
                class="form-control"
              />
            </div>
            <div class="form-group">
              <input
                v-model="form.HoKhau"
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
                v-model="form.DienThoai"
                type="text"
                name="DienThoai"
                placeholder="Số điện thoại"
                class="form-control"
                maxlength="10"
              />
            </div>
                  <div class="form-group">
              <input
                v-model="form.MaPhongBan"
                type="number"
                name="MaPhongBan"
                placeholder="Mã phòng ban"
                class="form-control"
                max="4"
                min="1"
              />
            </div>
            <div class="form-group">
              <select
                name="MaChucVu"
                v-model="form.MaChucVu"
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
                v-model="form.TenDangNhap"
                type="text"
                name="TenDangNhap"
                placeholder="Tên đăng nhập"
                class="form-control"
              />
            </div>
            <div class="form-group">
              <input
                v-model="form.MatKhau"
                type="text"
                name="MatKhau"
                placeholder="Mật khẩu"
                class="form-control"
              />
            </div>
            <div v-show="editMode" class="form-group">
              <input
                v-model="form.ThucLanh"
                type="text"
                name="Luong"
                placeholder="Lương"
                class="form-control"
              />
            </div>
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
        MaNhanVien: 0,
        HoTen: null,
        GioiTinh: null,
        SoCMND: null,
        NgaySinh: null,
        NoiSinh: null,
        HoKhau: null,
        DienThoai: null,
        MaPhongBan: null,
        MaChucVu: null,
        TenDangNhap: null,
        MatKhau: null,
        ThucLanh: null
      }
    };
  },
  methods: {
    resetForm() {
      (this.form.HoTen = ""),
        (this.form.GioiTinh = ""),
        (this.form.SoCMND = ""),
        (this.form.NgaySinh = ""),
        (this.form.NoiSinh = ""),
        (this.form.HoKhau = ""),
        (this.form.DienThoai = ""),
        (this.form.MaPhongBan = ""),
        (this.form.MaChucVu = ""),
        (this.form.TenDangNhap = ""),
        (this.form.MatKhau = "");
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
      (this.form.MaNhanVien = nhanvien.maNhanVien),
        (this.form.HoTen = nhanvien.hoTen),
        (this.form.GioiTinh = nhanvien.gioiTinh),
        (this.form.SoCMND = nhanvien.soCmnd),
        (this.form.NgaySinh = nhanvien.ngaySinh),
        (this.form.NoiSinh = nhanvien.noiSinh),
        (this.form.HoKhau = nhanvien.hoKhau),
        (this.form.DienThoai = nhanvien.dienThoai),
        (this.form.MaPhongBan = nhanvien.maPhongBan),
        (this.form.MaChucVu = nhanvien.maChucVu),
        (this.form.TenDangNhap = nhanvien.tenDangNhap),
        (this.form.MatKhau = nhanvien.matKhau),
        (this.form.ThucLanh = nhanvien.thucLanh);
    },
    updateSubmit(event) {
       if (event) event.preventDefault()
      axios.put("/api/SampleData/update", this.form).then(res => {
        this.$bvModal.hide('modalFormAdmin')
         this.posts[this.tempId]=res.data;
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
          } else if (
            result.dismiss === Swal.DismissReason.cancel
          ) {
            swalWithBootstrapButtons.fire(
              "Cancelled",
              "Your imaginary file is safe :",
              "error"
            );
          }
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
            this.posts.push(response.data);
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
@import url("https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css");

#skills {
  margin-top: 5em;
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
