!<template>
     <div id="task-compo">
    <h1 class="task-table-header">Task</h1>
    <b-form class="form-group">
      <div class="box">
        <label for>Employee Id</label>
        <div class="timesheets-input">{{form.maNhanVien}}</div>
      </div>
      <div class="box">
        <label for>Time start</label>
        <div class="timesheets-input">{{form.thoiGianBatDau | formatDate}}</div>
        <div class="check"></div>
      </div>
      <div class="box">
        <label for>Deadline</label>
        <div class="timesheets-input">{{form.thoiGianKetThuc | formatDate}}</div>
        <div class="check"></div>
      </div>
      <div class="box">
        <label for>Description</label>
       <textarea disabled rows="4" cols="50" name="Description" v-model="form.noiDungCongViec"></textarea>
      </div>
      <div class="box">
        <label for>Status</label>
              <b-form-select v-model="form.tienDo" name="gioiTinh" >
                 <option disabled value="Pending">Pending</option>
                <option value="Processing">Processing</option>
              </b-form-select>
        <div class="check">
          <b-button variant="info" @click.prevent="Submit" class="btn"> <b-icon icon="cloud-upload" aria-hidden="true"></b-icon> Acecpt task</b-button>
        </div>
      </div>
    </b-form>
  </div>
</template>
<script>
export default {
    data() {
    return {
        form: {
        maPhanCong:null,
        noiDungCongViec:null,
        maNhanVien: 0,
        thoiGianBatDau:null,
        thoiGianKetThuc:null,
        tienDo:null
      },
    };
  },
  methods:{
      getData(){
          axios.get("http://localhost:61447/api/Assigne/getdesassigne/108").then(res=>{
              console.log(res)
              this.form=res.data
          })
      },
      Submit(){
          axios.put("http://localhost:61447/api/Assigne/modifystatus",this.form).then(res=>{
              alert("done");
          })
      },
  },
  created(){
      this.getData();
  }
};

</script>


<style scoped>
#task-compo{
    color:aliceblue;
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
.task-table-header {
  font-size: xx-large;
  text-transform: uppercase;
  text-align: center;
  margin-top: 0.7em;
  font-weight: bold;
}
.box {
  width: 30em;
  margin-top: 0.9em;
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
  margin-top: 1em;
}
.btn {
  text-align: center;
  /* align-items: center; */
  margin: 0.5em;
  width: 10em;
}
#btn-submit {
  text-align: center;
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
textarea {
    width: 30em;
    border-radius: 15px ;
    background-color: azure !important;
}
</style>