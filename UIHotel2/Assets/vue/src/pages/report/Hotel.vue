<template>
  <div class="row">
    <div class="panel panel-inverse">
      <div class="panel-heading">
        <h4 class="panel-title">Hotel Report</h4>
      </div>
      <div class="panel-body">
        <h1>Hotel Report</h1>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";

@Component
export default class ReportHotel extends Vue {
  mounted() {
    this.$store.commit("changeTitle", "Hotel Report");
    this.$store.commit("changeSubtitle", "");
  }
}
</script>
