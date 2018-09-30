<template>
  <div class="form-horizontal">
    <div class="form-group">
      <label class="col-md-3 control-label">Room Number :</label>
      <div class="col-md-6">
        <button class="btn btn-info" @click="searchRoom">
          <i class="fa fa-search"></i>
          Select Room
        </button>
      </div>
    </div>

    <div class="form-group">
      <label class="col-md-3 control-label">Note :</label>
      <div class="col-md-6">
        <textarea class="form-control"></textarea>
      </div>
    </div>

    <uiv-modal v-model="is_show" title="Search Room">

    </uiv-modal>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";

interface IModalData {
  Id: string;
}

@Component
export default class CreateStep3 extends Vue {
  private is_show: boolean = false;
  private modalData: IModalData = {
    Id: ""
  };

  searchRoom() {
    this.is_show = true;
  }

  mounted() {
    //
  }
}
</script>
