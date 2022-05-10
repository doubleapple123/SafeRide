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
  </div>
</template>

<script>
export default {
  name: "UserManagement",
  data(){
    return{
      selectedOption: ''
    }
  }
}
</script>

<style scoped>

</style>
