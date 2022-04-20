<template>
  <MapHeader></MapHeader>
  <MapSearchRectangle id="MapSearchRec"></MapSearchRectangle>
  <div id='map'></div>
  <MapFooter @selectedOverlayColor="onOverlayColorChange" @selectedDimFooter="onReceiveOverlay"></MapFooter>
</template>

<script>
import MapSearchRectangle from '@/components/MapSearchRectangle'
import MapHeader from '@/components/MapHeader.vue'
import MapFooter from '@/components/MapFooter'
import 'mapbox-gl/dist/mapbox-gl.css'
import mapboxgl from 'mapbox-gl'
export default {
  components: {
    MapSearchRectangle,
    MapFooter,
    MapHeader
  },
  props: ['api_key'],
  mounted () {
    mapboxgl.accessToken = 'pk.eyJ1IjoiY2FudGRyaW5rbWlsayIsImEiOiJjbDAwZnFiOHkwM3kyM3FwaG1qcmFhazh6In0.ytVFjAsRLDJra61yH0ZT-w'
    this.map = new mapboxgl.Map({
      container: 'map', // container ID
      style: 'mapbox://styles/mapbox/streets-v11', // style URL
      center: [-118.1109043, 33.7827241], // starting position [lng, lat]
      zoom: 14 // starting zoom
    })
    //    const start = [-118.1109043, 33.7827241]
    //    async function getRoute (end) {
    //    const query = await fetch(`https://api.mapbox.com/directions/v5/mapbox/cycling/${start[0]},${start[1]};${end[0]},${end[1]}?steps=true&geometries=geojson&access_token=${mapboxgl.accessToken}`,
    //      { method: 'GET' })

  //    const json = await query.json()
  //    const data = json.routes[0]
  //    const route = data.geometry.coordinates
  //    const geojson = { type: 'Feature', properties: {}, geometry: { type: 'LineString', coordinates: route } }
  //    if (map.getSource('route')) {
  //      map.getSource('route').setData(geojson)
  //    } else {
  //      map.addLayer({
  //        id: 'route',
  //        type: 'line',
  //        source: {
  //          type: 'geojson', data: geojson
  //        },
  //        layout: { 'line-join': 'round', 'line-cap': 'round' },
  //        paint: { 'line-color': '#3887be', 'line-width': 5, 'line-opacity': 0.75 }
  //      })
  //    }
  //  }
  //  map.on('load', () => {
  //    getRoute(start)
  //    map.addLayer({
  //      id: 'point',
  //      type: 'circle',
  //      source: {
  //        type: 'geojson',
  //        data: {
  //          type: 'FeatureCollection',
  //          features: [{
  //            type: 'Feature',
  //            properties: {},
  //            geometry: {
  //              type: 'Point',
  //              coordinates: start
  //            }
  //          }]
  //        }
  //      },
  //      paint: {
  //        'circle-radius': 10,
  //        'circle-color': '#3887be'
  //      }
  //    })
  //  }
  //  )
  //  map.on('click', (event) => {
  //    const coords = Object.keys(event.lngLat).map((key) => event.lngLat[key])
  //    const end = {
  //      type: 'FeatureCollection',
  //      features: [{
  //        type: 'Feature',
  //        properties: {},
  //        geometry: {
  //          type: 'Point',
  //          coordinates: coords
  //        }
  //      }]
  //    }
  //    if (map.getLayer('end')) {
  //      map.getSource('end').setData(end)
  //    } else {
  //      map.addLayer({
  //        id: 'end',
  //        type: 'circle',
  //        source: {
  //          type: 'geojson',
  //          data: {
  //            type: 'FeatureCollection',
  //            features: [
  //              {
  //                type: 'Feature',
  //                properties: {},
  //                geometry: {
  //                  type: 'Point',
  //                  coordinates: coords
  //                }
  //              }
  //            ]
  //          }
  //        },
  //        paint: {
  //          'circle-radius': 10,
  //          'circle-color': '#f30'
  //        }
  //      })
  //    }
  //    getRoute(coords)
  //  })
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
