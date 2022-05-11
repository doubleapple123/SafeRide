<template>
  <h1 v-if=loggedIn>
    <a href="#">Home </a>
    <a href="#">AddRoute </a>
    <button v-on:click="logout">Logout></button>
  </h1>
  <h1 v-else>
    not logged in, {{ loggedIn }}
  </h1>
</template>

<script>
import axios from 'axios'

async function getUserInfo () {
  let res = false
  axios.defaults.headers.common.Authorization = localStorage.getItem('token')
  await axios.post('https://updatedbackend-apim.azure-api.net/v1/api/getToken')
    .then(function () { // do something with res here
      res = true
    })
    .catch(function () {
    })
  return res
}

export default {
  data: () => ({
    loggedIn: false
  }),
  created () {
    getUserInfo().then((res) => {
      this.loggedIn = res
    })
  },
  name: 'UserProtected',
  methods: {
    logout () {
      localStorage.setItem('token', "")
      this.$router.push({ name: 'Login' })
    }
  }
}
</script>
