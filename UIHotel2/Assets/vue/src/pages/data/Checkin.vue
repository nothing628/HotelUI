<template>
  <div class="row">
    <div class="panel panel-inverse">
      <div class="panel-heading">
        <h4 class="panel-title">List Checkin</h4>
      </div>
      <div class="panel-body">
        <h1>List Checkin</h1>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";

@Component
export default class DataCheckin extends Vue {
  mounted() {
    this.$store.commit("changeTitle", "List Checkin");
    this.$store.commit("changeSubtitle", "");
  }
}
</script>
