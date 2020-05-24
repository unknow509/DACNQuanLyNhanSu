<template>
<div class="outter">
  <b-container  id="info-compo">
    <h2 class="pl-3 info-compo-h2">Account infomation</h2>
      <b-col sm-6>
        <b-row class="my-3">
      <b-col sm="3">
        <label for="ID">Id</label>
      </b-col>
      <b-col sm="9">
        <b-form-input type="text" disabled v-model="form.maNhanVien"></b-form-input>
      </b-col>
    </b-row>
    <b-row class="my-3">
      <b-col sm="3">
        <label for="Họ Tên">Name</label>
      </b-col>
      <b-col sm="9">
        <b-form-input  v-model="form.hoTen" type="text"></b-form-input>
      </b-col>
    </b-row>
    <b-row class="my-3">
      <b-col sm="3">
        <label for="gioiTinh">Gender</label>
      </b-col>
      <b-col sm="9">
       <b-form-select v-model="form.gioiTinh" name="gioiTinh">
         <option value="nam">Nam</option>
          <option value="nu">Nữ</option>
       </b-form-select>
      </b-col>
    </b-row>
    <b-row class="my-3">
      <b-col sm="3">
        <label for="CMND">Identity</label>
      </b-col>
      <b-col sm="9">
        <b-form-input  v-model="form.soCmnd" type="text"></b-form-input>
      </b-col>
    </b-row>
    <b-row class="my-3">
      <b-col sm="3">
        <label for="ngày sinh">DOB</label>
      </b-col>
      <b-col sm="9">
        <b-form-input  v-model="form.ngaySinh" type="text"></b-form-input>
      </b-col>
    </b-row>
    <b-row class="my-3">
      <b-col sm="3">
        <label for="hoKhau">Adress</label>
      </b-col>
      <b-col sm="9">
        <b-form-input  v-model="form.hoKhau" type="text"></b-form-input>
      </b-col>
    </b-row>
    <b-row class="my-3">
      <b-col sm="3">
        <label for="nơi sinh">Place</label>
      </b-col>
      <b-col sm="9">
        <b-form-input  v-model="form.noiSinh" type="text"></b-form-input>
      </b-col>
    </b-row>
    <b-row class="my-3">
      <b-col sm="3">
        <label for="dienThoai">Phone</label>
      </b-col>
      <b-col sm="9">
        <b-form-input v-model="form.dienThoai" type="text"></b-form-input>
      </b-col>
    </b-row>
    
    <div class="text-center">
     <b-btn class="my-3 info-compo-btn">cập nhật</b-btn> 
    </div>  
      </b-col>  
  </b-container>
  <footer ></footer>
</div>
  
</template>

<script>
export default {
data(){
  return {
    form: {
        maNhanVien: 0,
        hoTen: null,
        gioiTinh: null,
        soCmnd: null,
        ngaySinh: null,
        noiSinh: null,
        hoKhau: null,
        dienThoai: null,
        maPhongBan: null,
        maChucVu: null,
        tenDangNhap: null,
        matKhau: null,
        thucLanh:null,
      },
  }
    
},
methods:{

},
mounted(){
  (this.form.maNhanVien=this.$store.state.userInfo.maNhanVien),
  (this.form.hoTen=this.$store.state.userInfo.hoTen),
  (this.form.gioiTinh=this.$store.state.userInfo.gioiTinh),
  (this.form.soCmnd=this.$store.state.userInfo.soCmnd),
  (this.form.ngaySinh=this.$store.state.userInfo.ngaySinh),
  (this.form.noiSinh=this.$store.state.userInfo.noiSinh),
  (this.form.hoKhau=this.$store.state.userInfo.hoKhau),
  (this.form.dienThoai=this.$store.state.userInfo.dienThoai),
  (this.form.maChucVu=this.$store.state.userInfo.maChucVu),
  (this.form.tenDangNhap=this.$store.state.userInfo.tenDangNhap),
  (this.form.matKhau=this.$store.state.userInfo.matKhau),
  (this.form.thucLanh=this.$store.state.userInfo.thucLanh),
  (this.form.maNhanVien=this.$store.state.userInfo.maNhanVien);
},
}
</script>

<style scoped>
.outter{
  background-color:#eee;
  color:black !important;
}
#info-compo{
  font-family: 'Roboto', sans-serif;
  height: 600px;
  padding-top: 2.5em;
  width: 40em;
  background-color: white;
  border-radius: 10%;
}
.info-compo-h2{
  font-size: 14px;
  text-transform: uppercase;
  color: #333;
  font-weight: 700;
  padding-bottom: 10px;
  box-sizing: border-box;
  border-bottom: 2px lightgray solid ;
}
.info-compo-btn{
  width: 120px;
    height: 35px;
  border-radius:13px;
    font-size: 14.5px;;
    text-transform: uppercase;
    background-color: #03b4f5;
    display: inline-block;
}
button:hover{
  background-color: #0287d1;
}
footer{
  padding-bottom:4em;
  background-color:#eee;
}
</style>>
