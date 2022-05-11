<template>
  <div id="app">
    <h1>Sign In</h1>
    <form class="form-group">
      <input v-model="userLogin" type="text" class="form-control" placeholder="Username" required>
      <input v-model="emailLogin" type="text" class="form-control" placeholder="Email" required>
      <input type="submit" class="btn btn-primary" @click="doLogin">
    </form>
    <h2>Two-Factor Verification</h2>
    <form class="form-group">
      <input v-model="otp" type="text" class="form-control" placeholder="One-Time Passphrase" required>
      <input type="submit" class="btn btn-primary" @click="verifyOTP">
    </form>
  </div>
</template>

<script>
import axios from 'axios'
export default {
  el: '#app',
  name: 'Home',
  methods: {
    doLogin () {
      if (this.userLogin !== undefined && this.emailLogin !== undefined) {
        axios.post('https://updatedbackend-apim.azure-api.net/v1/api/login', {
          UserName: this.userLogin,
          Email: this.emailLogin,
          Role: 'admin',
          Valid: true
        }, {
          withCredentials: false
        })
          .then(function (response) {
            
            console.log(response)
            window.alert('OTP Sent - Please verify your account login by entering the temporary one-time passphrase sent to your email address')
          })
          .catch(function (error) {
            console.log(error)
            window.alert('Login error')
          })
      }
    },
     verifyOTP () {
      if (this.otp) {
        axios.get('https://updatedbackend-apim.azure-api.net/v1/api/verifyOTP', {
          withCredentials: true,
          params: {
            otpPassphrase: this.otp
          }
        })
          .then(function (response) {
            var token = response.data.token
            localStorage.setItem('token', JSON.stringify(token))
            console.log(response)
            window.alert('OTP Verified. User sucessfully logged in with token = ' + localStorage.getItem('token'))
          })
          .catch(function (error) {
            console.log(error)
            window.alert('OTP error')
          })
      }
    }
  }
}

</script>
