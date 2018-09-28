<template>
  <div :class="activeClass">
    <slot></slot>
  </div>
</template>
<script>
export default {
  data() {
    return {
      is_active: false
    };
  },
  computed: {
    activeClass() {
      if (this.is_active) {
        return ["bwizard-activated"];
      } else {
        return ["hide"];
      }
    }
  },
  props: {
    name: { type: String, required: true },
    active: { type: Boolean, default: false }
  },
  methods: {
    containerActive(targetname) {
      this.is_active = this.name == targetname;
    }
  },
  mounted() {
    this.is_active = this.active;
    this.$bus.$on("wizard-toggle", this.containerActive);
  },
  beforeDestroy() {
    this.$bus.$off("wizard-toggle", this.containerActive);
  }
};
</script>
