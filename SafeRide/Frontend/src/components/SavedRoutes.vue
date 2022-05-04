<template>
  <div id="savedRoutes">
    <select v-model="selectedRoute">
      <option v-for="route in savedRoutes" v-bind:key="route.value">
        {{route}}
      </option>
      <option>
        None
      </option>
    </select>
  </div>
</template>

<script>
import axios from "axios";

export default {
  name: "SavedRoutes",
  data(){
    return {
      selectedRoute: '',
      savedRoutes: []
    }
  },
  methods:{
    async getSavedRoutes(){
      let routes = []
      axios.defaults.headers.common.Authorization = localStorage.getItem('token')
      await axios.get('https://backendsaferideapi.azure-api.net/overlayAPI/api/route/all')
        .then(async function(res) {
          routes = await res
        })
        .catch(function () {

        })
      return routes.data
    }
  },
  created(){
    this.getSavedRoutes().then((res) => {
      this.savedRoutes = res
    })
  }
}
</script>

<style scoped>

</style>
