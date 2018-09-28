<template>
  <div class="form-horizontal">
    <div class="form-group">
      <label class="col-md-3 control-label">Booking Type</label>
      <div class="col-md-3">
        <input class="form-control"/>
      </div>
      <label class="col-md-2 control-label">Online Provider</label>
      <div class="col-md-3">
        <input class="form-control"/>
      </div>
    </div>

    <div class="form-group">
      <label class="col-md-3 control-label">Booking Number</label>
      <div class="col-md-4">
        <input class="form-control"/>
      </div>
    </div>

    <div class="form-group">
      <label class="col-md-3 control-label">Arrival Date</label>
      <div class="col-md-3">
        <input type="date" class="form-control"/>
      </div>
      <label class="col-md-2 control-label">Departure Date</label>
      <div class="col-md-3">
        <input type="date" class="form-control"/>
      </div>
    </div>

    <div class="form-group">
      <label class="col-md-3 control-label">Adult Count</label>
      <div class="col-md-3">
        <input class="form-control" type="number"/>
      </div>
      <label class="col-md-2 control-label">Child Count</label>
      <div class="col-md-3">
        <input class="form-control" type="number"/>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";

@Component
export default class CreateStep1 extends Vue {
  mounted() {
    //
  }
}
</script>
