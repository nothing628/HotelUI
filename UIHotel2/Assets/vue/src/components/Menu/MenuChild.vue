<template>
  <li :class="active_class">
    <a @click="linkClicked" href="javascript:;">
      <i v-if="is_icon" :class="icon_class"></i>
      <span>{{ text }}</span>
    </a>
  </li>
</template>
<script>
export default {
  computed: {
    active_class() {
      if (this.isActive) return ["active"];
      return [];
    },
    icon_class() {
      let list_class = this.icon.split(" ");

      return list_class;
    },
    is_icon() {
      return this.icon != "";
    }
  },
  props: {
    isActive: { type: Boolean, required: false, default: false },
    text: { type: String, required: true },
    href: { type: String, required: false, default: "" },
    route: { type: String, required: false, defaultl: "" },
    icon: { type: String, required: false, default: "" }
  },
  methods: {
    linkClicked() {
      if (this.href != "") {
        this.$emit("click", this.href);
        this.$router.push(this.href);
      }

      if (this.route != "") {
        this.$emit("click", this.route);
        this.$router.push({ name: this.route });
      }
    }
  }
};
</script>
