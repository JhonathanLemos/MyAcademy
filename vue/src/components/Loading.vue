<template>
  <div :class="contentStyle">
    <v-progress-circular
      v-if="visible"
      indeterminate
      :color="color"
      :size="size"
      :width="width"
    ></v-progress-circular>
  </div>
</template>
<script>
export default {
  props: {
    startContent: {
      type: Boolean,
      default: false,
    },
    size: {
      type: Number,
      default: 20,
    },
    color: {
      type: String,
      default: 'Academia-purple'
    },
    width: {
      type: Number,
      default: 3,
    },
    visible: {
      type: Boolean,
      default: true,
    },
  },
  computed: {
    contentStyle() {
      return this.startContent
        ? 'd-flex align-center justify-start'
        : 'd-flex align-center justify-center'
    },
  },
}
</script>
