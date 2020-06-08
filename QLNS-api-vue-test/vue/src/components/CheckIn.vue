!<template>
  <div id="checkin-compo" style="color:aliceblue">
    <h1 class="checkin-table-header">timesheets</h1>
    <b-form class="form-group">
      <div class="box">
        <label for>Employee Id</label>
        <div class="timesheets-input">{{this.form.MaNhanVien}}</div>
      </div>
      <div class="box">
        <label for>Time start</label>
        <div class="timesheets-input">{{dateNow | formatTime}}</div>
        <div class="check"></div>
      </div>
      <div class="box">
        <label for>Hours work</label>
        <input maxlength="1" type="number" class="timesheets-input" min="0" max="8" v-model="NumberOfHourEnd" />
      </div>
      <div class="box">
        <label for>Status</label>
        <input
          type="text"
          class="timesheets-input"
          placeholder="Pending"
          v-show="!ShowStatus"
          disabled
        />
        <input
          type="text"
          class="timesheets-input"
          placeholder="Accepted"
          v-show="ShowStatus"
          disabled
        />
        <div class="check">
          <b-button variant="info" @click.prevent="Submit" class="btn">Submit</b-button>
        </div>
      </div>
    </b-form>
  </div>
</template>

<script>
export default {
  name: "CheckIn",
  data() {
    return {
     
      ShowStatus: false,
      dateNow: null,
      NumberOfHourEnd: null,
      form: {
        MaNhanVien: this.$store.state.userInfo.maNhanVien,
        Day: null,
        GioBatDau: null,
        GioKetThuc: null,
        Status: null,
      }
    };
  },
  methods: {
    getDate() {
      this.dateNow = new Date();
    },
    showStatus() {
      if (this.$store.state.userInfo.maChucVu === "nv") {
        this.ShowStatus = false;
       // console.log("nhân viên");
      } else {
        this.ShowStatus = true;
        //console.log("đéo phải nhân viên");
      }
    },
    addHoursEnd() {
      var temp = new Date();
      Date.prototype.addHours = function(h) {
        this.setTime(this.getTime() + h * 60 * 60 * 1000);
        return this;
      };
      temp.addHours(this.NumberOfHourEnd);
      var tempEndTime = this.$options.filters.formatYMDT(temp);
      this.form.GioKetThuc=tempEndTime
    },
    fillInForm() {
      var tempDay = this.$options.filters.formatDay(this.dateNow);
      this.form.Day = tempDay;
      var tempTimeStart = this.$options.filters.formatYMDT(this.dateNow);
      this.form.GioBatDau = tempTimeStart;
      this.form.Status= 'Pending'
    },
   Submit(){
      swalWithBootstrapButtons
        .fire({
          title: "Are you sure ?",
          text: "You won't be able to revert this!",
          icon: "warning",
          showCancelButton: true,
          confirmButtonText: "Yes, Submit it!",
          cancelButtonText: "No, cancel!",
          reverseButtons: false
        })
        .then(result => {
          if (result.value) {
            axios.post('http://localhost:61447/api/CheckIn/CheckIn',this.form).then(res => {
              swalWithBootstrapButtons.fire(
                "Done!",
                "Checkin thành công rồi nhé.",
                "success"
              );
            })
            .catch(e=>{
              (e.dismiss === Swal.DismissReason.cancel) 
            swalWithBootstrapButtons.fire(
              "Cancelled",
              "Bạn đã check in hôm nay rồi nhé",
              "error"
            );
          
            })
          } else if (result.dismiss === Swal.DismissReason.cancel) {
            swalWithBootstrapButtons.fire(
              "Cancelled",
              "Bạn chưa checkin hôm nay ",
              "error"
            );
          }
        });
    }
  },
  mounted() {
    this.getDate();
    this.fillInForm();
  },
  created() {
    this.showStatus();
  },
  updated() {
    this.addHoursEnd();
  }
};
</script>


<style scoped>
#checkin-compo{
  height: 600px;
}
.checkin-table-header {
  font-size: xx-large;
  text-transform: uppercase;
  text-align: center;
  margin-top: 2em;
  font-weight: bold;
}
.box {
  width: 30em;

  margin-top: 2em;
}

.form-group {
  margin: 0px 535px 0 517px;
}
.timesheets-input {
  display: block;
  width: 100%;
  height: calc(1.5em + 0.75rem + 2px);
  padding: 0.375rem 0.75rem;
  font-size: 1rem;
  font-weight: 400;
  line-height: 1.5;
  color: #495057;
  background-color: #fff;
  background-clip: padding-box;
  border: 1px solid #ced4da;
  border-radius: 0.25rem;
}
.check {
  text-align: center;
}
.btn {
  text-align: center;
  /* align-items: center; */
  margin: 0.5em;
  width: 10em;
}
#btn-submit {
  text-align: center;
  margin-top: 2em;
}
#submit {
  text-align: center;
  width: 115px;
  height: 55px;
  color: white;
  background-color: #073c5d;
  border: solid;
  border-radius: 33px;
  opacity: 0.6;
}
#submit:hover {
  opacity: 1;
}
</style>