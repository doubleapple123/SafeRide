<template>
  <div id="nav">
    <router-link to="/">Home</router-link>
    <br />
    <router-link to="/login">Login</router-link>
    <br />
    <router-link to="/UserRegistration">UserRegistration</router-link>
    <br />
    <router-link to="/User">User</router-link>
    <br />
    <router-link v-if="isAuthorized" to="/Map">DefaultMap</router-link>
    <br />
    <router-link to="/Analytics">AnalyticsDashboard</router-link>
    <br />
    <router-link to="/UserManagement">Manage Users</router-link>
  </div>
  <router-view/>
</template>

<script>
import axios from 'axios'
export default {
  data(){
    return {
      isAuthorized: false
    }
  },
  methods:{
    async checkToken () {
      let ret = false
      axios.defaults.headers.common.Authorization = localStorage.getItem('token')
      await axios.post('https://localhost:5001/api/verifyToken')
        .then(async function (){
          ret = true
        })
      return ret
    }
  },
  created(){
    this.checkToken().then((res) => {
      this.isAuthorized = res
    })
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

#nav {
  padding: 30px;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #42b983;
}
</style>
