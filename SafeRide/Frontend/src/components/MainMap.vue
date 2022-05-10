<template>
  <MapHeader></MapHeader>
  <div>
    <div id='mapControllers'>
      <form @submit.prevent="handleUserRoute">
        <MapSearchRectangle v-model="userStartLocation"  placeholder="Start Location" />
        <MapSearchRectangle v-model="userEndLocation"  placeholder="End Location"/>
        <MapSearchRectangle v-model="excludedHazard"  placeholder="End Location"/>
        <button>Search</button>
        <button>Exclude</button>

      </form>

    </div>

    <div id='map' class="map">
    </div>

    <div id="instructions" class="instructions"></div>
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
  data () {
    return {
      userStartLocation: '',
      userEndLocation: '',
      excludedHazard: ''
    }
    MapSearchRectangle,
    MapFooter,
    MapHeader,
    SavedRoutes
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
    },
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