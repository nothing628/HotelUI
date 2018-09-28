<template>
  <div class="form-horizontal">
    <div class="form-group">
      <label class="col-md-3 control-label">Room Number :</label>
      <div class="col-md-6">
        <button class="btn btn-info">Select Room</button>
      </div>
    </div>

    <div class="form-group">
      <label class="col-md-3 control-label">Note :</label>
      <div class="col-md-6">
        <textarea class="form-control"></textarea>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";

@Component
export default class CreateStep3 extends Vue {
  mounted() {
    //
  }
}
</script>
