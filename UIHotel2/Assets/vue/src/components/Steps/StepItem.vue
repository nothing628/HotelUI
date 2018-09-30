<template>
  <li :class="activeClass" @click="toggleActive">
    <span class="label">{{ stepNumber }}</span>
    <a href="javascript:;" class="hidden-phone">{{ stepTitle }}</a>
    <a href="javascript:;" class="hidden-phone">
      <small>{{ stepSubtitle }}</small>
    </a>
  </li>
</template>
<script>
export default {
  data() {
    return {
      is_active: false,
      uid: null
    };
  },
  props: {
    stepNumber: { type: String },
    stepTitle: { type: String },
    stepSubtitle: { type: String },
    stepTarget: { type: String, default: "" },
    active: { type: Boolean }
  },
  computed: {
    activeClass() {
      if (this.is_active) {
        return ["active"];
      }

      return [];
    }
  },
  methods: {
    toggleActive() {
      window.bus.$emit("wizard-active", this.uid);

      if (this.stepTarget != "") {
        window.bus.$emit("wizard-toggle", this.stepTarget);
      }
    },
    activeWizard(uidname) {
      this.is_active = this.uid == uidname;
    }
  },
  mounted() {
    this.uid = Math.ceil(Math.random() * 10000000);
    this.is_active = this.active;
    window.bus.$on("wizard-active", this.activeWizard);
  },
  beforeDestroy() {
    window.bus.$off("wizard-active", this.activeWizard);
  }
};
</script>
