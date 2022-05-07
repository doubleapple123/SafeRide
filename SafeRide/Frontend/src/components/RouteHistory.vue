<template>
  <div class="Route History">
    <h1>History Searches</h1>
  </div>
</template>

<script>
import axios from 'axios'
export default {
  components: {
  },
  name: 'HistorySearches',
  data () {
    return {
      startpoint: '',
      endpoint: '',
      geo: [],
      instructions: ''
    }
  },
  methods: {
    async getRouteInfo () {
      let routeInfo = []
      axios.defaults.headers.common.Authorization = localStorage.getItem('token')
      await axios.get('', {
        withCredentials: true
      })
        .then(async function (res) {
          routeInfo = await res
        })
        .catch(function () {
        })
      return routeInfo.data.routeInfo
    }
  }
}
</script>
