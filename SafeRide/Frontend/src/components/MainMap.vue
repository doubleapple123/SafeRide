<template>
  <MapHeader></MapHeader>
  <div>
    <div id='mapControllers'>
      <p hidden>{{allHazards}}</p>
      <form @submit.prevent="handleUserRoute">
        <MapSearchRectangle v-model="userStartLocation" placeholder="Start Location" />
        <MapSearchRectangle v-model="userEndLocation" placeholder="End Location" />
        <MapSearchRectangle v-model="excludedHazard" placeholder="End Location" />
        <button>Search</button>
        <button>Exclude</button>

      </form>

    </div>

    <div id='map' class="map">
    </div>

    <div id="instructions" class="instructions"></div>
  </div>
  <div id="markerReport">
    <h1>Report Hazard</h1>
    <select v-model="selectedHazard">
      <option v-for="hazardType in possibleHazards" v-bind:key="hazardType.value">
        {{hazardType}}
      </option>
    </select>
  </div>
  <MapSearchRectangle id="MapSearchRec"></MapSearchRectangle>
  <div id='map'></div>
  <MapFooter @selectedOverlayColor="onOverlayColorChange" @selectedDimFooter="onReceiveOverlay"></MapFooter>
  <SavedRoutes @selectedSavedRoute="onSavedRouteChange"></SavedRoutes>

</template>

<script>
import MapSearchRectangle from '@/components/MapSearchRectangle'
import MapHeader from '@/components/MapHeader.vue'
import MapFooter from '@/components/MapFooter'
import SavedRoutes from '@/components/SavedRoutes'
import 'mapbox-gl/dist/mapbox-gl.css'
import mapboxgl from 'mapbox-gl'
import '@mapbox/mapbox-gl-geocoder/dist/mapbox-gl-geocoder.css'
import axios from 'axios'
export default {
  components: {
    MapSearchRectangle,
    MapFooter,
    MapHeader,
    SavedRoutes
  },
  data () {
    return {
      userStartLocation: '',
      userEndLocation: '',
      excludedHazard: '',
      allHazards: [],
      possibleHazards: ['None', 'Accident', 'Obstruction', 'Bike Lane', 'Vehicle', 'Closure'],
      selectedHazard: '',
      markerToReport: new mapboxgl.Marker({ draggable: "true" })
      }
    },
    methods: {
    handleUserRoute() {
        const startLocation = this.userStartLocation.split(', ')
        const endLocation = this.userEndLocation.split(', ')
        const startMarker = new mapboxgl.Marker()
          .setLngLat([startLocation[0], startLocation[1]])
          .addTo(this.map)
        const endMarker = new mapboxgl.Marker()
          .setLngLat([endLocation[0], endLocation[1]])
          .addTo(this.map)
    },
    addSavedRoute (data) {
      let geoFeature = {
        type: 'Feature',
        properties: {},
        geometry:{
          type: 'LineString',
          coordinates: data.routes[0].geometry.coordinates
        }
      }
      if(this.map.getSource("route")){
        this.map.getSource("route").setData(geoFeature)
      }
      else{
        this.map.addLayer({
          id: "route",
          type: "line",
          source: {
            type: 'geojson',
            data: geoFeature
          },
          layout: {
            'line-join': 'round',
            'line-cap': 'round'
          },
          paint: {
            'line-color': 'blue',
            'line-width': 5,
            'line-opacity': 0.75
          }
        })
      }
    },
    removeRoutes(){
      if (this.map.getLayer('route')){
        this.map.removeLayer('route')
      }
      if(this.map.getSource('route')){
        this.map.removeSource('route')
      }
    },
    excludeHazard () {
      if (this.excludeHazard != undefined) {
      axios.post('https://backendsaferideapi.azure-api.net/overlayAPI/api/hazard/simpleHazard', {
        Hazard: this.excludedHazard
      }, {
        withCredentials: false
      })
       .then(function (response) {
            var coordinates = response.data.results
            localStorage.setItem('results', JSON.stringify(results))
            console.log(response)
            window.alert('Excluding hazard was a success with the resuts: = ' + localStorage.getItem('results'))
          })
          .catch(function (error) {
            console.log(error)
            window.alert('Hazard error')
          })
      }
    },
    removeOverlays () {
      if (this.map.getLayer('userLayer')) {
        this.map.removeLayer('userLayer')
      }
      if (this.map.getLayer('outline')) {
        this.map.removeLayer('outline')
      }
      if (this.map.getSource('userLayer')) {
        this.map.removeSource('userLayer')
      }
    },
    changeOverlayColor (value) {
      this.map.getLayer('userLayer').paint = { 'fill-color': value }
      this.map.addSource('userLayer', {
        type: 'geojson',
        data: {
          type: 'Feature',
          geometry: {
            type: 'Polygon',
            coordinates: [
              coords
            ]
          }
        }
      })
      this.map.addLayer({
        id: 'userLayer',
        type: 'fill',
        source: 'userLayer', // reference the data source
        layout: {},
        paint: {
          'fill-color': value.overlayColor, // blue color fill
          'fill-opacity': 0.5
        }
      })
      this.map.addLayer({
        id: 'outline',
        type: 'line',
        source: 'userLayer',
        layout: {},
        paint: {
          'line-color': '#000',
          'line-width': 3
        }
      })
    },

    onReceiveOverlay (value) {
      if (value !== 'None') {
        this.removeOverlays()
        this.addOverlays(value)
      } else {
        this.removeOverlays()
      }
    },
    
    onOverlayColorChange (value) {
      if (value !== 'Default') {
        this.changeOverlayColor(value)
      }
    },
    onSavedRouteChange(value){
      if(value === "None"){
        this.removeRoutes()
      }else {
        this.addSavedRoute(value)
      }
      },

      showHazards() {
        console.log("woof", this.allHazards)
        for (var obj of this.allHazards) {
          var typeString
          var color
          //switch case sets color and type string for user info
          switch (obj.type) {
            case 0: typeString = "Accident"
              color = "#FFC300"
              break
            case 1: typeString = "Obstruction"
              color = "#FF5733"
              break
            case 2: typeString = "Bike Lane"
              color = "#C70039"
              break
            case 3: typeString = "Vehicle"
              color = "#900C3F"
              break
            case 4: typeString = "Closure"
              color = "#581845"
              break
            default: typeString = "Default Hazard"
              break
          }

          //add marker to map and allow user to view popup on click
          const myLatlng = new mapboxgl.LngLat(obj.longitude, obj.latitude)
          const marker = new mapboxgl.Marker({ color: color })
            .setLngLat(myLatlng)
            .setPopup(new mapboxgl.Popup({ offset: 25 })
              .setHTML('<h3>Type: ' + typeString + '</h3><p>Time reported: ' + obj.timeReported + '</p>'))
            .addTo(this.map)
        }
      },

      onDragEnd() {
        const lngLat = this.markerToReport.getLngLat()
        if (confirm("Report hazard here?") == true) {
          //posts hazard to backend, resets markerToReport and alerts user of successful report. 
          var type = 0
          switch (this.selectedHazard) {
            case "Accident": type = 0
              break
            case "Obstruction": type = 1
              break
            case "Bike Lane": type = 2
              break
            case "Vehicle": type = 3
              break
            case "Closure": type = 4
              break
            default: type = 0
              break
          }
          console.log(lngLat.lat, lngLat.lng, type)

          axios.post('https://backend20220418173746.azurewebsites.net/api/hazards/report?' + 'lat=' + lngLat.lat + '&lon=' + lngLat.lng + '&type=' + type)
            .then(async function () {
              location.reload()
            })
            .catch(function () {
            })

          this.selectedHazard = "None"
          this.markerToReport.remove()
          alert("Hazard reported.")
          //HTTP POST TO BACKEND AND UPDATE MAP WITH NEW HAZARDS 
        } else {
          //reset selected hazard to none, removes hazard because it was not reported, and alert the user that report was cancelled.
          this.selectedHazard = "None"
          alert("Report cancelled.")
          this.markerToReport.remove()
        }
      },

      reportHazard() {
        var color = "#FFFFFF"
        this.markerToReport
          .setLngLat(this.map.getCenter())
          .addTo(this.map)
        this.markerToReport.on('dragend', this.onDragEnd)
      }
  },
    props: ['api_key'],
    created() {
      axios
        .get('https://backend20220418173746.azurewebsites.net/api/hazards/getHazards')
        .then(response => (this.allHazards = response.data))
    },
    mounted() {
      mapboxgl.accessToken = this.api_key
       this.map = new mapboxgl.Map({
        container: 'map', // container ID
        style: 'mapbox://styles/mapbox/streets-v11', // style URL
        center: [-118.1141, 33.7838], // starting position [lng, lat]
        zoom: 14 // starting zoom
       })

      
  },
    updated() {
      this.showHazards()
      if (this.selectedHazard !== 'None' && this.selectedHazard !== '') {
        console.log(this.selectedHazard)
        this.reportHazard()
      }
    }
}
</script>

<style scoped>
  #map {
    margin: auto;
    width: 100%;
    height: 600px;
  }

  #MapSearchRec {
    position: fixed;
    left: 50px;
  }
  .startGeocoder {
    position: absolute;
    z-index: 1;
    width: 50%;
    right: 50%;
    margin-left: -25%;
    top: 35%;
  }
  .mapboxgl-ctrl-geocoder {
    min-width: 100%
  }

  #instructions {
    position: absolute;
    margin: 20px;
    width: 20%;
    bottom: 20%;
    left: 78%;
    background-color: #fff;
    overflow-y: scroll;
    font-family: sans-serif;
  }

  #markerReport {
    position: relative;
    width: 100px;
    bottom: 300px;
    left: 90%
  }

</style>
