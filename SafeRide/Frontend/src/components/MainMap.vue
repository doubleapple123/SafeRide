<template>
  <MapHeader></MapHeader>
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
export default {
  components: {
    MapSearchRectangle,
    MapFooter,
    MapHeader,
    SavedRoutes
  },
  props: ['api_key', ' startLocation', 'endLocation', 'api_key'],
  methods: {
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
    addOverlays (value) {
      const coords = []
      value.overlayStructure.forEach(function (coord) {
        coords.push([coord.longPoint, coord.latPoint])
      })
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
  mounted () {
    mapboxgl.accessToken = this.api_key
    this.map = new mapboxgl.Map({
      container: 'map', // container ID
      style: 'mapbox://styles/mapbox/streets-v11', // style URL
      center: [-118.1109043, 33.7827241], // starting position [lng, lat]
      zoom: 14 // starting zoom
    })
  }
}
</script>

<style scoped>
#map{
  margin: auto;
  width: 70%;
  height: 600px;
}
#MapSearchRec{
  position:fixed;
  left:50px;
}
</style>
