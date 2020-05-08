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
                <th>Status checkin</th>            
              </tr>
            </thead>
            <tbody>
              <tr v-for="(field,index)  in fields" :key="index">
              <th>{{index+1}}</th>
              <th>{{field.hoTen}}</th>
              <th>{{field.tenPhongBan}}</th>
              <th>{{field.tenChucVu}}</th>
              <th></th>
              <th>{{field.status}}</th>
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
     fields:[],
     
    };
  },
  methods: {
  },
mounted() {
  axios.get("/api/CheckIn/GetSalary")
  .then(res=>{
    this.fields=res.data;
  })
  .catch(err=>{
    console.log(err)
  })
  
},
  
    
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

th{
  text-align: center;
}

</style>
