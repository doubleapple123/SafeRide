<template>
  <MapHeader></MapHeader>
  <MapSearchRectangle id="MapSearchRec"></MapSearchRectangle>
  <div>
   <div id='map'></div>
   <div id="startGeocoder" class="startGeocoder"></div>
  </div>
  <MapFooter @selectedOverlayColor="onOverlayColorChange" @selectedDimFooter="onReceiveOverlay"></MapFooter>
</template>

<script>
import MapSearchRectangle from '@/components/MapSearchRectangle'
import MapHeader from '@/components/MapHeader.vue'
import MapFooter from '@/components/MapFooter'
import 'mapbox-gl/dist/mapbox-gl.css'
import mapboxgl from 'mapbox-gl'
import MapboxGeocoder from '@mapbox/mapbox-gl-geocoder'
import '@mapbox/mapbox-gl-geocoder/dist/mapbox-gl-geocoder.css'
import axios from 'axios'
export default {
  components: {
    MapSearchRectangle,
    MapFooter,
    MapHeader
  },
  data () {
    return {
      start: [0, 0]
    }
  },
  methods: {
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
    }
  },
  props: ['api_key'],
  mounted () {
    mapboxgl.accessToken = this.api_key
    const map = new mapboxgl.Map({
      container: 'map', // container ID
      style: 'mapbox://styles/mapbox/streets-v11', // style URL
      center: [-118.1141, 33.7838], // starting position [lng, lat]
      zoom: 14 // starting zoom
    })
    const startGeocoder = new MapboxGeocoder({
      accessToken: this.api_key,
      mapboxgl: mapboxgl
    })
    console.log(this.start)
    map.addControl(startGeocoder)
    document.getElementById('startGeocoder').appendChild(startGeocoder.onAdd(map))
    startGeocoder.on('result', (event) => {
      const coords = Object.keys(event.lngLat).map((key) => event.lngLat[key])
      this.start = coords
      console.log(this.start)
    })
    async function getRoute (end) {
      const query = await axios.get(
        `https://api.mapbox.com/geocoding/v5/mapbox.places/${this.start[0]},${this.start[1]}.json?access_token=${this.api_key}`
      )
      const json = await query.json()
      const data = json.routes[0]
      const route = data.geometry.coordinates
      const geojson = {
        type: 'Feature',
        properties: {},
        geometry: {
          type: 'LineString',
          coordinates: route
        }
      }
      if (map.getSource('route')) {
        map.getSource('route').setData(geojson)
      } else {
        map.addLayer({
          id: 'route',
          type: 'line',
          source: {
            type: 'geojson',
            data: geojson
          },
          layout: {
            'line-join': 'round',
            'line-cap': 'round'
          },
          paint: {
            'line-color': '#3887be',
            'line-width': 5,
            'line-opacity': 0.75
          }
        })
      }
      // turn instructions here
    }
    map.on('load', () => {
      getRoute(this.start)
      map.addLayer({
        id: 'point',
        type: 'circle',
        source: {
          type: 'geojson',
          data: {
            type: 'FeatureCollection',
            features: [{
              type: 'Feature',
              properties: {},
              geometry: {
                type: 'Point',
                coordinates: this.start
              }
            }]
          }
        },
        paint: {
          'circle-radius': 10,
          'circle-color': '#3887be'
        }
      })
      map.on('click', (event) => {
        const coords = Object.keys(event.lngLat).map((key) => event.lngLat[key])
        const end = {
          type: 'FeatureCollection',
          features: [
            {
              type: 'Feature',
              properties: {},
              geometry: {
                type: 'Point',
                coordinates: coords
              }
            }
          ]
        }
        if (map.getLayer('end')) {
          map.getSource('end').setData(end)
        } else {
          map.addLayer({
            id: 'end',
            type: 'circle',
            source: {
              type: 'geojson',
              data: {
                type: 'FeatureCollection',
                features: [
                  {
                    type: 'Feature',
                    properties: {},
                    geometry: {
                      type: 'Point',
                      coordinates: coords
                    }
                  }
                ]
              }
            },
            paint: {
              'circle-radius': 10,
              'circle-color': '#f30'
            }
          })
        }
        getRoute(coords)
      })
    })
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
    min-width:100%
  }

</style>
