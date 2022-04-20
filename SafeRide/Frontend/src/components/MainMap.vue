<template>
  <div id="mapSearchBox">
    <form>
      <input v-model="startLocation" type="text" id="startLocation" placeholder="Start Location"><br>
      <input v-model="destinationLocation" v-on:keyup.enter="getLocation" type="text" id="destinationLocation" placeholder="Destination Location"><br>
      <input type="submit" hidden />
    </form>
  </div>
</template>

<script>
import axios from 'axios'
import mapboxgl from 'mapbox-gl'
import MapboxGeocoder from '@mapbox/mapbox-gl-geocoder'
import '@mapbox/mapbox-gl-geocoder/dist/mapbox-gl-geocoder.css'
export default {
  data () {
    return {
      loading: false,
      location: '',
      center: [-118.1109043, 33.7827241],
      map: {}
    }
  },
  name: 'MapSearchRectangle',
  methods: {
    async getLocation () {
      this.loading = true
      const resp = await axios.get(
          `https://api.mapbox.com/geocoding/v5/mapbox.places/${this.center[0]},${this.center[1]}.json?access_token=${this.api_key}`
      )
      this.loading = false
      this.location = resp.data.features[0].place_name
    },
    onSearchEnter: async function () {
    }
  },
  props: ['api_key'],
  mounted () {
    mapboxgl.accessToken = this.api_key
    this.map = new mapboxgl.Map({
      container: 'map', // container ID
      style: 'mapbox://styles/mapbox/satellite-v9', // style URL
      center: [-118.1109043, 33.7827241], // starting position [lng, lat]
      zoom: 14 // starting zoom
    })
    const geocoder = new MapboxGeocoder({
      accessToken: this.api_key,
      mapboxgl: mapboxgl,
      marker: false
    })
    this.map.addControl(geocoder)

    geocoder.on('result', (e) => {
      const marker = new mapboxgl.Marker({
        draggable: true,
        color: '#D80739'
      })
        .setLngLat(e.result.center)
        .addTo(this.map)
      this.center = e.result.center
      marker.on('dragend', (e) => {
        this.center = Object.values(e.target.getLngLat())
      })
    })
  }
}
</script>

<style scoped>
</style>
