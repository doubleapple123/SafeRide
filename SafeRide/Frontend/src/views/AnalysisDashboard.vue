<template>
  <div>
    <h1>Retrieve Data</h1>
    <form class="form-group">
      <input type="submit" class="btn btn-primary" @click="getData">
    </form>
  </div>
  <div>
    <h1>Get JSON data for non-expired hazard in database</h1>
    {{topViews}}
    <h1> Top 5 Longest Views of All Time</h1>
    {{topDurations}}
    <h1> Number of Logins daily for the last three months</h1>
    {{lastThreeMonthLogins}}
    <h1>Number of Registrations daily for last three months</h1>
    {{lastThreeMonthRegistrations}}
  </div>
</template>

<script>
import axios from 'axios'
export default {
  el: '#app',
  name: 'Analytics',
  methods: {
    getData () {
      axios
        .get('https://backend20220418173746.azurewebsites.net/api/hazards/getHazards')
        .then(response => (this.topViews = response.data))
      axios
        .get('https://backend20220418173746.azurewebsites.net/api/analytics/topDurations')
        .then(response => (this.topDurations = response.data))
      axios
        .get('https://backend20220418173746.azurewebsites.net/api/analytics/lastThreeMonthLogins')
        .then(response => (this.lastThreeMonthLogins = response.data))
      axios
        .get('https://backend20220418173746.azurewebsites.net/api/analytics/lastThreeMonthRegistrations')
        .then(response => (this.lastThreeMonthRegistrations = response.data))
    }
  }
}
</script>
