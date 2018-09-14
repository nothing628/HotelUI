<template>
  <div class="row">
    <div class="panel panel-inverse">
      <div class="panel-heading">
        <h4 class="panel-title">List Guest</h4>
      </div>
      <div class="panel-body">
        <h1>List Guest</h1>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";

@Component
export default class DataGuest extends Vue {
  mounted() {
    this.$store.commit("changeTitle", "List Guest");
    this.$store.commit("changeSubtitle", "");
  }
}
</script>
