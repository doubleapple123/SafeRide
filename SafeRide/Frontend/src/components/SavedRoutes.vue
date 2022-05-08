<template>
  <div id="savedRoutes">
    <select v-model="selectedRoute">
      <option v-for="route in savedRoutes.routeList" v-bind:key="route.value">
        {{route}}
      </option>
      <option>
        None
      </option>
    </select>
    <button @click="shareRoute">share current route</button>
    <input v-model="newRouteName" placeholder="enter route name" />
    <input v-model="newRouteJson" placeholder="enter json string" />
    <button @click="submitNewRoute">submit new route</button>
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
      savedRoutes: [],
      encryptedRoute: '',
      newRouteName: '',
      newRouteJson: ''
    }
  },
  methods:{
    async submitNewRoute(){
      axios.defaults.headers.common.Authorization = localStorage.getItem('token')
      await axios.post('https://backendsaferideapi.azure-api.net/overlayAPI/' + 'api/route/add', JSON.parse(this.newRouteJson), {
        params: {routeName: this.newRouteName}
      })
        .then(async function() {
          this.$forceUpdate()
        })
        .catch(function () {
        })
    },
    async shareRoute(){
/*      let cipher = ''
      axios.defaults.headers.common.Authorization = localStorage.getItem('token')
      await axios.get('https://localhost:5001/' + 'api/route/share', {params: {routeName: this.selectedRoute}})
        .then(async function(res) {
          cipher = await res
        })
        .catch(function () {

        })
      console.log(cipher.data)*/
      /*return this.selectedRouteData*/
      console.log(this.selectedRouteData.json)
    },
    async getSavedRoutes(){
      let routes = []
      axios.defaults.headers.common.Authorization = localStorage.getItem('token')
      await axios.get('https://backendsaferideapi.azure-api.net/overlayAPI/' + 'api/route/all')
        .then(async function(res) {
          routes = await res
        })
        .catch(function () {

        })
      console.log(routes.data)
      return routes.data
    },
    async getSavedRoute(routeName) {
      axios.defaults.headers.common.Authorization = localStorage.getItem('token')
      let routeData = {}
      await axios.get('https://backendsaferideapi.azure-api.net/overlayAPI/' + 'api/route/get', {params: {routeName: routeName}})
        .then(async function (res) {
          routeData = await res
        })
        .catch(function () {
        })
      return routeData.data
    }
  },
  created(){
    this.getSavedRoutes().then((res) => {
      this.savedRoutes = res
    })
  },
  updated(){
    if(this.selectedRoute !== "None" && this.selectedRoute !== '') {
      this.getSavedRoute(this.selectedRoute).then((res) => {
        this.selectedRouteData = res
        let resObj = JSON.parse(res.json)
        this.$emit("selectedSavedRoute", resObj)
      })
    }
    if(this.selectedRoute === "None"){
      this.$emit("selectedSavedRoute", "None")
    }
  }
}
</script>

<style scoped>

</style>
