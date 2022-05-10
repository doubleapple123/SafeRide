<template>
  <div id="manageUsers">
    <select v-model="selectedOption">
      <option>
        Create
      </option>
      <option>
        Update
      </option>
      <option>
        Delete
      </option>
      <option>
        Disable
      </option>
      <option>
        Enable
      </option>
    </select>
    <input v-model="enteredUsername" placeholder="username"/>
    <button @click="performUM">perform operation</button>
    <input v-model="newUsername" placeholder="new username"/>
    <input v-model="newEmail" placeholder="new email"/>
  </div>
</template>

<script>
import axios from "axios";

export default {
  name: "UserManagement",
  data(){
    return{
      selectedOption: '',
      enteredUsername: '',
      newUsername: '',
      newEmail: ''
    }
  },
  methods:{
    async performUM(){
      let postType = ""
      switch(this.selectedOption){
        case "Create":{
          postType = "user/createUser"
          break;
        }
        case "Update":{
          postType = "user/updateUser"
          break;
        }
        case "Delete": {
          postType = "user/deleteUser"
          break;
        }
        case "Disable":{
          postType = "user/disableAccount"

          break;
        }
        case "Enable":{
          postType = "user/enableAccount"

          break;
        }
      }
      //https://backendsaferideapi.azure-api.net/overlayAPI/
      axios.defaults.headers.common.Authorization = localStorage.getItem('token')
      if (this.selectedOption === "Update") {
        await axios.post('https://localhost:5001/' + postType, {
          UserName: this.newUsername,
          Email: this.newEmail,
          Role: "user",
          Valid: true
        }, {
          params: {username: this.enteredUsername}})
          .catch(function () {
            window.alert("Not logged in or not admin")
          })

      }
      else if(this.selectedOption === "Create"){
        await axios.post('https://localhost:5001/' + postType, {
          UserName: this.newUsername,
          Email: this.newEmail,
          Role: "user",
          Valid: true
        })
          .catch(function () {
            window.alert("Not logged in or not admin")
          })
      }
      else{
        await axios.post('https://localhost:5001/' + postType, {
          params: {username: this.enteredUsername}})
          .catch(function () {
            window.alert("Not logged in or not admin")
          })
      }
    }
  }

}
</script>

<style scoped>

</style>
