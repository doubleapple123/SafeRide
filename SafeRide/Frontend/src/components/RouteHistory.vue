<template>
  <div id="app">
    <h1>Search History</h1>
    <textarea v-model="haha" id="haha" name="haha" rows="4" cols="50">
    </textarea>
  </div>
</template>

<script>
import axios from 'axios'
export default {
  el: '#app',
  name: 'Home',
  mounted () {
    axios.get('https://api.coindesk.com/v1/bpi/currentprice.json').then(function (response) {
      console.log(response.data.bpi)
    })
  }
}

</script>
