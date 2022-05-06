<template>
  <MapHeader></MapHeader>
  <MapSearchRectangle id="MapSearchRec"></MapSearchRectangle>
  <div>
   <div id='map' class="map"></div>
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
    MapSearchRectangle,
    MapHeader
  },
  data () {
    return {
    }
  },
  methods: {
   
  },
  props: ['api_key', 'startLocation', 'endLocation'],
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
      console.log('updated from map')
    }
}
</script>

<style scoped>
  #map {
    margin: auto;
    width: 60%;
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
