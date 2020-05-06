<template>
  <div>
    <br />
   
      <b-col md="12">
        <div class="table-responsive">
          <table class="table table-hover">
            <thead>
              <tr>
                <th>STT</th>
                <th>Name</th>
                <th>Department</th>
                <th>Position</th>
                <th>Salary</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(post, index) in posts" :key="post.maNhanVien">
                <td>{{ ++index }}</td>
                <td>{{ post.hoTen }}</td>
                <td>{{post.gioiTinh}}</td>
                <td>{{ post.dienThoai }}</td>
                <td >{{ post.hoKhau }}</td>
                <td>{{ post.ngaySinh | formatDate }}</td>
                <td>{{post.maPhongBan}}</td>
                <td>{{post.maChucVu}}</td>
              </tr>
            </tbody>
          </table>
        </div>
      </b-col>
    
</div>
</template>

<script>

export default {
  data() {
    return {
     
    };
  },
  methods: {

  }
    
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

th{
  text-align: center;
}

</style>
