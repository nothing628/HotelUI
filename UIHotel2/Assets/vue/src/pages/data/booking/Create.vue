<template>
  <div class="row">
    <div class="panel panel-inverse">
      <div class="panel-heading">
        <h4 class="panel-title">Create Booking</h4>
      </div>
      <div class="panel-body">
        <step-selector>
          <step-item step-number="1" step-title="Booking Info" step-target="booking" active></step-item>
          <step-item step-number="2" step-title="Guest Info" step-target="guest"></step-item>
          <step-item step-number="3" step-title="Room Info" step-target="room"></step-item>
        </step-selector>
        <step-container name="booking" active>
          <h1>Booking</h1>
        </step-container>
        <step-container name="guest">
          <h1>Guest</h1>
        </step-container>
        <step-container name="room">
          <h1>Room</h1>
        </step-container>
        <button @click="back">Go Back</button>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import StepItem from "@/components/Steps/StepItem.vue";
import StepSelector from "@/components/Steps/StepSelector.vue";
import StepContainer from "@/components/Steps/StepContainer.vue";

@Component({
  components: {
    StepItem,
    StepSelector,
    StepContainer
  }
})
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
