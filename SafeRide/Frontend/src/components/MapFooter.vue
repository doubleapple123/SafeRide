<template>
  <div id="mapFooter">
    <button id="routeAnalysisButton" @click="doRouteAnalysisButtonClick">Route Analysis</button>
    <button id="hazardButton" @click="doHazardButtonClick">Report Hazard</button>
    <input type="submit" class="btn btn-primary" @click="doGetHazardClick">
    <MapOverlay @selectedColor="onOverlayColorChange" @selectedDim="onOverlaySelect"></MapOverlay>
  </div>
  <div>
    <h1>Hazard JSON data from database</h1>
  </div>
</template>

<script>
import MapOverlay from './MapOverlay'
import axios from 'axios'
export default { // OVERLAYS NEEDS TO BE PASSED DYNAMICALLY, cant do rn
  components: {
    MapOverlay
  },
  name: 'MapFooter',
  methods: {
    doRouteAnalysisButtonClick () {
    },
    doHazardButtonClick () {
    },
    onOverlaySelect (value) {
      this.$emit('selectedDimFooter', value)
    },
    onOverlayColorChange (value) {
      this.$emit('selectedOverlayColor', value)
    },
    doGetHazardButtonClick() {
      console.log("gethazard")
      axios
        .get('https://backend20220418173746.azurewebsites.net/api/hazards/getHazards')
        .then(response => (this.hazards = response.data))
    }
  }
}
</script>

<style scoped>
  #routeAnalysisButton{
    position: relative;
    height: 40px;
    width: 100px;
    background-color: greenyellow;
    right: 20%;
  }
  #hazardButton{
    position: relative;
    height: 40px;
    width: 100px;
    background-color: greenyellow;
    left: 20%;
  }
  #getHazardButton {
    position: relative;
    height: 40px;
    width: 100px;
    background-color: greenyellow;
    left: 20%;
  }

  #mapFooter{
    height: 50px;
    background-color: gray;
  }
</style>
