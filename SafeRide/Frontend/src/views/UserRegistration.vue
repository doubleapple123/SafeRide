<template>
  <div class="UserRegistration">
    <h1>Registration</h1>
    <p>Enter desired Username, Email, and passphrase</p>
    <form @submit.prevent="doRegistration" class="form-group">
      
      <input v-model="userEmail" type="text" class="form-control" placeholder="Email Address" required>
      <input v-model="userPassphrase" type="text" class="form-control" placeholder="Passphrase" required>
      <input type="submit" class="btn btn-primary">
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
      if (this.userEmail !== undefined && this.userOTP !== undefined) {
        axios.post('https://localhost:5001/user/createUser', {
          
          Email: this.userEmail,
          Passphrase: this.userPassphrase,
          Role: 'User',
          Valid: true
        })
          .then(function (response) {
            console.log(response)
            window.alert('Email confirmation pending')
          })
          .catch(function (error) {
            console.log(error)
            window.alert('Account creation error. Retry again or contact system administrator')
          })
      }
    }
  }
}
</script>
