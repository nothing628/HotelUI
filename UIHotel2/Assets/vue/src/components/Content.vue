<template>
  <div id="content" class="content">
    <h1 class="page-header">{{ page_title }} <small>{{ page_subtitle }}</small></h1>
    <router-view></router-view>
  </div>
</template>
<script>
import { Component, Vue, Prop } from "vue-property-decorator";

@Component
export default class Content extends Vue {
  get page_title() {
    return this.$store.state.page_title;
  }
  get page_subtitle() {
    return this.$store.state.page_subtitle;
  }
}
</script>
