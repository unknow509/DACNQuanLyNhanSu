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
                <th>Sex</th>
                <th>Phone</th>
                <th>Address</th>
                <th>DOB</th>
                <th>Department</th>
                <th>Position</th>
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
      posts: [],
    };
  },
  methods: {

  }
    ,
  mounted() {
    this.$http
      .get("http://localhost:61447/api/SampleData/findall")
      .then(function(res) {
        this.posts = res.body;
        console.log(res);
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
th{
  text-align: center;
}
td:not(:nth-child(5)){
  text-align: center;
}
.modal-header {
  text-align: center;
}
button.swal2-cancel.btn.btn-danger {
  margin-right: 6em;
}
</style>
