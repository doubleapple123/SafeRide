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
      selectedRouteData: {},
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
    },
    async getSavedRoute(routeName) {
      axios.defaults.headers.common.Authorization = localStorage.getItem('token')
      let routeData = {}
      await axios.get('https://backendsaferideapi.azure-api.net/overlayAPI/api/route/get', {params: {routeName: routeName}})
        .then(async function (res) {
          routeData = await res
        })
        .catch(function () {
        })
      return routeData
    }
  },
  created(){
    this.getSavedRoutes().then((res) => {
      this.savedRoutes = res
    })
  },
  updated(){
    if(this.selectedRoute !== "None") {
      this.getSavedRoute(this.selectedRoute).then((res) => {
        let resObj = JSON.parse(res)
        this.$emit("selectedSavedRoute", resObj)
      })
    }
  }
}
</script>

<style scoped>

</style>
