<template>
  <div id="mapHeader">
    <button id="homeButton" @click="doHomeButtonClick()">Home Button</button>
    <button id="searchRouteButton" @click="doSearchRoute()">Search</button>
    <button id="savedRoutesButton" @click="doSavedRoutesButtonClick()">History</button>
    <button id="accountButton" @click="doAccountButtonClick">Account</button>
  </div>
</template>

<script>
export default {
  name: 'MapHeader',
  methods: { // put your logic here :)
    doHomeButtonClick () {
      this.$router.push('/')
    },
    doSearchRoute () {
      this.$router.push('/SearchRoute')
    },
    doSavedRoutesButtonClick () {
      this.$router.push('/RouteHistory')
    },
    doAccountButtonClick () {
    }
  }
}
</script>

<style scoped>
  #homeButton{
    position: relative;
    right: 25%;
    width: 100px;
    height: 40px;
    background-color: greenyellow;
  }
  #searchRouteButton {
    position: relative;
    right: 25%;
    width: 100px;
    height: 40px;
    background-color: greenyellow;
  }
  #savedRoutesButton{
    position: relative;
    left: 25%;
    width: 100px;
    height: 40px;
    background-color: greenyellow;
  }
  #accountButton{
    position: relative;
    left: 25%;
    width: 100px;
    height: 40px;
    background-color: greenyellow;
  }
  #mapHeader{
    height: 50px;
    background: gray;
  }

</style>
