<template>
  <MapHeader></MapHeader>
  
  <div>
    <div id='mapControllers'>
      <form @submit.prevent="handleUserRoute">
        <MapSearchRectangle v-model="userStartLocation"  placeholder="Start Location" />
        <MapSearchRectangle v-model="userEndLocation"  placeholder="End Location"/>
        <button>Search</button> 
      </form>
        <button @click="saveUserRoute">Save This Route</button>
    </div>

    <div id='map' class="map">
    </div>

    <div id="instructions" class="instructions"></div>
  </div>

</template>


<script>
import MapSearchRectangle from '@/components/MapSearchRectangle'
import MapHeader from '@/components/MapHeader.vue'
import 'mapbox-gl/dist/mapbox-gl.css'
import mapboxgl from 'mapbox-gl'
import '@mapbox/mapbox-gl-geocoder/dist/mapbox-gl-geocoder.css'
export default {
  components: {
    MapHeader,
    MapSearchRectangle
  },
  data () {
    return {
      userStartLocation: '',
      userEndLocation: ''
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
    }
    
  },
    props: ['api_key'],
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
      console.log('updated')
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

</style>
