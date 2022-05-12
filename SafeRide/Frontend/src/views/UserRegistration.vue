<template>
  <div class="UserRegistration">
    <h1>Registration</h1>
    <p>Enter desired Username, Email, and Password</p>
    <form class="form-group">
      <input v-model="userName" type="text" class="form-control" placeholder="Username" required>
      <input v-model="userEmail" type="text" class="form-control" placeholder="Email Address" required>
      <input v-model="userPassphrase" type="text" class="form-control" placeholder="Passphrase" required>
      <input type="submit" class="btn btn-primary" @click="doRegistration">
    </form>
    <h2>Email Confirmation</h2>
    <form class="form-group">
      <input v-model="otp" type="text" class="form-control" placeholder="One-Time Passphrase" required>
      <input type="submit" class="btn btn-primary" @click="verifyEmail">
    </form>
  </div>
</template>

<script>
import axios from 'axios'
export default {
  el: 'app',
  name: 'Home',
  methods: {
    doRegistration () {
      if (this.userName !== undefined && this.userEmail !== undefined && this.userPassphrase !== undefined) {
        axios.post('https://updatedbackend-apim.azure-api.net/user/createUser', {
          Username: this.userName,
          Email: this.userEmail,
         //Passphrase: this.userPassphrase,
          Role: 'User',
          Valid: true
        },
        {
          withCredentials: false,
          params: { 
            passphrase: this.userPassphrase
            }
        })
          .then(function (response) {
            console.log(response)
            window.alert(JSON.stringify(response.data.message))
          })
          .catch(function (error) {
            console.log(error)
            window.alert(error)
          })
      }
    },
    verifyEmail() {
      if (this.otp) {
        axios.get('https://updatedbackend-apim.azure-api.net/user/verifyEmail', {
          withCredentials: false,
          params: {
            otpPassphrase: this.otp
          }
        })
          .then(function (response) {
            var token = response.data.token
            localStorage.setItem('token', JSON.stringify(token))
            console.log(response)
            window.alert(JSON.stringify(response.data.message))
          })
          .catch(function (error) {
            console.log(error)
            window.alert(error)
          })
      }
    }
  }
}
</script>
