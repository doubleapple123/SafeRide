<template>
  <div id="mapSearchBox">
    <label>
      <input type='text' v-model='inputValue'/>
    </label>
  </div>
</template>

<script>
export default {
    name: 'MapSearchRectangle',
    props: ['modelValue'],
    computed: {
      inputValue: {
        get() {
          return this.modelValue
        },
        set(value) {
          this.$emit('update:modelValue', value)
        }
      }
    }
  }
</script>
<style scoped>
</style>