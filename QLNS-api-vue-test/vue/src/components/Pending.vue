<template>
  <div>
    <table class="table table-hover">
      <thead>
        <tr>
          <th>Họ Tên</th>
          <th>Giờ bắt đầu</th>
          <th>Giờ kết thúc</th>
          <th>Ngày</th>
          <th>Status</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(field,index) in fields" :key="index">
          <td>{{ field.hoTen}}</td>
          <td>{{field.gioBatDau | formatTime}}</td>
          <td>{{field.gioKetThuc | formatTime }}</td>
          <td>{{field.day | formatDate}}</td>
          <td>{{ field.status }}</td>
          <td>
            <b-button @click="UpdateChiTietChamCong(field)" variant="success">Check</b-button>
          </td>
        </tr>
      </tbody>
    </table>
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
          <h4 class="modal-title text-center">Check</h4>
        </div>
        <form>
          <!-------------------------------------------- Modal body ------------------------------>
          <div class="modal-body">
            <div class="form-group">
              <input v-model="form.MaNhanVien" type="text" disabled class="form-control" />
            </div>
             <div class="form-group">
              <input v-model="form.GioBatDau" type="text"  disabled class="form-control" />
            </div>
             <div class="form-group">
              <input v-model="form.GioKetThuc" type="text"  disabled class="form-control" />
            </div>
             <div class="form-group">
              <input v-model="form.Day" type="text"  disabled class="form-control" />
            </div>
            
             <div class="form-group">
               <select
                name="Status"
                v-model="form.Status"
                id="Status"
                placeholder="Status"
                class="form-control"
              >
                <option selected disabled>Pending</option>
                <option value="Acecpt">Acecpt</option>
                <option value="Delete">Delete</option>
              </select>
             
            </div>
            
          </div>
          <!--------------------------------------------Modal footer ---------------------------->
          <div class="modal-footer">
            <button @click="updateSubmit" type="submit" class="btn btn-success">Check</button>
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
      fields: [],
      showModal: false,
      form: {
        MaNhanVien: null,
        GioBatDau: null,
        GioKetThuc: null,
        Day: null,
        Status:"pending"
      }
    };
  },
  methods: {
    getAllPending() {
      axios
        .get("http://localhost:61447/api/CheckIn/GetAllPending")
        .then(res => {
          this.fields = res.data;
          //console.log(res.data);
        })
        .catch(err => {
          console.log("error get pending", err);
        });
    },
    UpdateChiTietChamCong(ctcc) {
       this.showModal = true;
        (this.form.MaNhanVien = ctcc.maNhanVien),
        (this.form.GioBatDau = ctcc.gioBatDau),
        (this.form.GioKetThuc = ctcc.gioKetThuc),
        (this.form.Day = ctcc.day),
        (this.form.Status = null);
    },
    updateSubmit() {
      axios.put('http://localhost:61447/api/CheckIn/Update',this.form)
      .then(res=>{
        alert('success')
      })
    }
  },
  mounted() {
    this.getAllPending();
  }
};
</script>

<style scoped>
</style>