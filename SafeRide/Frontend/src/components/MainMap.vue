<template>
  <MapHeader></MapHeader>

  <div>
    <div id='mapControllers'>
      <p hidden>{{allHazards}}</p>
      <form @submit.prevent="handleUserRoute">
        <MapSearchRectangle v-model="userStartLocation" placeholder="Start Location" />
        <MapSearchRectangle v-model="userEndLocation" placeholder="End Location" />
        <button>Search</button>
      </form>
      <button @click="saveUserRoute">Save This Route</button>
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

  <MapFooter></MapFooter>
</template>


<script>
import MapSearchRectangle from '@/components/MapSearchRectangle'
import MapHeader from '@/components/MapHeader.vue'
import MapFooter from '@/components/MapFooter'
import 'mapbox-gl/dist/mapbox-gl.css'
import mapboxgl from 'mapbox-gl'
import axios from 'axios'

export default {
  components: {
    MapSearchRectangle,
    MapFooter,
    MapHeader
    },
  methods: {
      MapHeader,
      MapSearchRectangle,
      MapFooter
    },
    data() {
      return {
        userStartLocation: '',
        userEndLocation: '',
        allHazards: [],
        possibleHazards: ['Accident', 'Obstruction', 'Bike Lane', 'Vehicle', 'Closure'],
        selectedHazard: ''
      }
    },
    // summary
    /*
      Method grabs user coordinate, coordinates are used to map the route.
      This route is then saved into backend.
    */
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

      saveUserRoute() {
        console.log(this.userStartLocation + ' ' + this.userEndLocation)
      },

      buildRoute() {
      },

      showHazards() {
        console.log("woof", this.allHazards)
        for (var obj of this.allHazards) {
          var typeString
          switch (obj.type) {
            case 0: typeString = "Accident"
              break
            case 1: typeString = "Obstruction"
              break
            case 2: typeString = "Bike Lane"
              break
            case 3: typeString = "Vehicle"
              break
            case 4: typeString = "Closure"
            default: typeString = "Default Hazard"
              break
          }

          const myLatlng = new mapboxgl.LngLat(obj.longitude, obj.latitude)
          const marker = new mapboxgl.Marker()
            .setLngLat(myLatlng)
            .setPopup(new mapboxgl.Popup({ offset: 25 })
              .setHTML('<h3>Type: ' + typeString + '</h3><p>Time reported: ' + obj.timeReported + '</p>'))
            .addTo(this.map)
        }
      },

      reportHazard() {
        
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
