<template>
  <div>
    <b-row>
      <b-col md="2" offset-md="10">
        <b-button @click="createEmp" variant="outline-info">Create employee</b-button>
      </b-col>
    </b-row>
    <br />
    <b-row>
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
                <td>{{ ++index }}</td>
                <td>{{ post.hoTen }}</td>
                <td>{{post.gioiTinh}}</td>
                <td>{{ post.dienThoai }}</td>
                <td>{{ post.hoKhau }}</td>
                <td>{{ post.ngaySinh | formatDate }}</td>
                <td>{{post.maPhongBan}}</td>
                <td>{{ post.tenDangNhap }}</td>
                <td>{{ post.matKhau }}</td>
                <td>{{ post.thucLanh }}</td>
                <td>
                  <b-button variant="success" @click="updateEmp(post)">Update</b-button>
                </td>
                <td>
                  <b-button variant="danger" @click="deleteEmp(post.maNhanVien)">Delete</b-button>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </b-col>
    </b-row>

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
            </div>
            
            
          </div>
          <!--------------------------------------------Modal footer ---------------------------->
          <div class="modal-footer">
            <button v-show="!editMode" @click="createbtn" type="submit" class="btn btn-primary">Tạo</button>
            <button v-show="editMode" @click="updateSubmit" type="submit" class="btn btn-success">Sửa</button>
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
      positionId: [],
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

    updateEmp(nhanvien) {
      this.editMode = true;
      this.showModal = true;
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
    updateSubmit() {
      axios.put("/api/SampleData/update", this.form).then(res => {
        console.log("udpate thành công");
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
          confirmButtonText: "Yes, delete it!",
          cancelButtonText: "No, cancel!",
          reverseButtons: true
        })
        .then(result => {
          if (result.value) {
            axios.delete("/api/SampleData/delete/" + id).then(() => {
              swalWithBootstrapButtons.fire(
                "Deleted!",
                "Your file has been deleted.",
                "success"
              );
              location.reload();
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
    createbtn() {
      axios
        .post("http://localhost:61447/api/SampleData/create", this.form)
        .then(response => {
          //console.log(response);
        })
        .catch(e => {
          alert(e + "\n  Vui lòng nhập mã chức vụ ");
        });
    }
  },
  created() {
    this.callPosition();
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
@import url("https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css");

#skills {
  margin-top: 5em;
}

td,
th {
  text-align: center;
}

.modal-header {
  text-align: center;
}
button.swal2-cancel.btn.btn-danger {
  margin-right: 6em;
}
.modal{
  width: 60%;
}
</style>
