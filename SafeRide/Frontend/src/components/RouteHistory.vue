<template>
  <div id="app">
    <h1>Search History</h1>
  </div>
</template>

<script>
import axios from 'axios'
export default {
  el: '#app',
  name: 'Home',
  mounted () {
    axios.get('https://api.coindesk.com/v1/bpi/currentprice.json').then(function (response) {
      window.alert(response.data)
    })
  }
}

</script>
