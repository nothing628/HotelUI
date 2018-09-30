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
          <create1 v-model="bookInfo"></create1>
        </step-container>
        <step-container name="guest">
          <create2 v-model="guestInfo"></create2>
        </step-container>
        <step-container name="room">
          <create3 v-model="roomInfo"></create3>
        </step-container>

        <div class="row">
          <div class="col-md-6 col-md-offset-3">
            <button class="btn btn-success m-r-5" @click="booking"><i></i> Booking</button>
            <button class="btn btn-success m-r-5" @click="bookingCheckin"><i></i> Booking & Checkin</button>
            <button class="btn btn-danger" @click="back"><i></i> Cancel</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop, Watch } from "vue-property-decorator";
import StepItem from "@/components/Steps/StepItem.vue";
import StepSelector from "@/components/Steps/StepSelector.vue";
import StepContainer from "@/components/Steps/StepContainer.vue";
import Create1 from "@/components/Bookmark/Step1.vue";
import Create2 from "@/components/Bookmark/Step2.vue";
import Create3 from "@/components/Bookmark/Step3.vue";

@Component({
  components: {
    Create1,
    Create2,
    Create3,
    StepItem,
    StepSelector,
    StepContainer
  }
})
export default class CreateBooking extends Vue {
  private bookInfo: object = {};
  private guestInfo: object = {};
  private roomInfo: object = {};

  get isValid(): boolean {
    var book_val = Object.keys(this.bookInfo);
    var guest_val = Object.keys(this.guestInfo);
    var room_val = Object.keys(this.roomInfo);

    return !(
      book_val.length == 0 ||
      guest_val.length == 0 ||
      room_val.length == 0
    );
  }

  booking() {
    this.validate();
    this.$nextTick(() => {
      console.log(this.isValid);
    });
  }

  bookingCheckin() {
    this.validate();
    this.$nextTick(() => {
      console.log(this.isValid);
    });
  }

  validate() {
    window.bus.$emit("book-validate");
  }

  back() {
    this.$router.push({ name: "data.booking" });
  }

  mounted() {
    this.$store.commit("changeTitle", "Create Booking");
    this.$store.commit("changeSubtitle", "");
  }
}
</script>
