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
      <input type="submit" class="btn btn-primary" @click="verifyOTP">
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
        axios.post('https://updatedbackend-apim.azure-api.net/v1/user/createUser', {
          Username: this.userName,
          Email: this.userEmail,
          Passphrase: this.userPassphrase,
          Role: 'User',
          Valid: true
        },
        {
          withCredentials: false
        })
          .then(function (response) {
            console.log(response)
            window.alert('Email Confirmation Sent - Please enter the unique one-time passphrase sent to your email to complete registration')
          })
          .catch(function (error) {
            console.log(error)
            window.alert('Account creation error. Could not send out OTP to complete registration. Please retry again or contact system administrator')
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
