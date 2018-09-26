<template>
  <div class="row">
    <div class="panel panel-inverse">
      <div class="panel-heading">
        <h4 class="panel-title">Create Booking</h4>
      </div>
      <div class="panel-body">
        <h1>Create Booking</h1>
        <h2>{{ $route.query }}</h2>
        <button @click="back">Go Back</button>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";

@Component
export default class DataBooking extends Vue {
  back() {
    this.$router.push({ name: "data.booking" });
  }

  mounted() {
    this.$store.commit("changeTitle", "Create Booking");
    this.$store.commit("changeSubtitle", "");
  }
}
</script>
