<!-- <template>
<section>
  <div id="mapHazards">
      <button @click="excludeHazards">Exclude</button>
      </div>
      <div class="wrap">
        <div class="left">
        <h3>Hazards:</h3>
        <ul id="hazardTypes">
            <li v-for="hazard in hazards" :key="hazard.name">
                <input type="checkbox" :id="hazard.name" :value="hazard" v-model="selectedHazards">
                <label :for="hazard.name">{{hazard.name}}</label>
            </li>
        </ul>
        </div>
        <div class="right">
        <div v-for="hazard in selectedHazards" :key="hazard.name">{{hazard.name}}</div>
        </div>
    </div>
</section>
</template>

<script>
export default {
    name: 'MapHazard',
    methods: {
    onSearchEnter: function () {
    }
    },
    data() {
        return {
        hazards: [
            { name: 'Accident' },
            { name: 'Obstruction' },
            { name: 'BikeLane' },
            { name: 'Vehicle' },
            { name: 'Closure' }
        ],
        selectedHazards: []
        }
    }
} 
</script>

<style scoped>
</style> -->
