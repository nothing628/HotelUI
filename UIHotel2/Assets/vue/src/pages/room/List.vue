<template>
  <div class="panel-group">
    <panel-accordion header="CATEGORY : BIG">
      <div class="row">
        <div class="col-md-2">
          <div class="widget widget-stats bg-green">
            <div class="stats-info">
              <h4>READY</h4>
              <p>201</p>	
            </div>
            <div class="stats-link">
              <a href="javascript:;">View Detail <i class="fa fa-arrow-circle-o-right"></i></a>
            </div>
          </div>
        </div>
        <div class="col-md-2">
          <div class="widget widget-stats bg-orange">
            <div class="stats-info">
              <h4>BOOKED</h4>
              <p>202</p>	
            </div>
            <div class="stats-link">
              <a href="javascript:;">View Detail <i class="fa fa-arrow-circle-o-right"></i></a>
            </div>
          </div>
        </div>
        <div class="col-md-2">
          <div class="widget widget-stats bg-red">
            <div class="stats-info">
              <h4>OCCUPIED</h4>
              <p>203</p>	
            </div>
            <div class="stats-link">
              <a href="javascript:;">View Detail <i class="fa fa-arrow-circle-o-right"></i></a>
            </div>
          </div>
        </div>
        <div class="col-md-2">
          <div class="widget widget-stats bg-blue">
            <div class="stats-info">
              <h4>LATE CHECKOUT</h4>
              <p>204</p>	
            </div>
            <div class="stats-link">
              <a href="javascript:;">View Detail <i class="fa fa-arrow-circle-o-right"></i></a>
            </div>
          </div>
        </div>
        <div class="col-md-2">
          <div class="widget widget-stats bg-purple">
            <div class="stats-info">
              <h4>CLEANING</h4>
              <p>205</p>	
            </div>
            <div class="stats-link">
              <a href="javascript:;">View Detail <i class="fa fa-arrow-circle-o-right"></i></a>
            </div>
          </div>
        </div>
        <div class="col-md-2">
          <div class="widget widget-stats bg-grey-darker">
            <div class="stats-info">
              <h4>MAINTANCE</h4>
              <p>206</p>	
            </div>
            <div class="stats-link">
              <a href="javascript:;">View Detail <i class="fa fa-arrow-circle-o-right"></i></a>
            </div>
          </div>
        </div>
      </div>
    </panel-accordion>
    <panel-accordion header="CATEGORY : MEDIUM">
      <div class="row">
        <div class="col-md-2">
          <div class="widget widget-stats bg-green">
            <div class="stats-info">
              <h4>READY</h4>
              <p>201</p>	
            </div>
            <div class="stats-link">
              <a href="javascript:;">View Detail <i class="fa fa-arrow-circle-o-right"></i></a>
            </div>
          </div>
        </div>
      </div>
    </panel-accordion>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import PanelAccordion from "@/components/Panel/PanelAccordion.vue";

@Component({
  components: {
    PanelAccordion
  }
})
export default class RoomList extends Vue {
  private is_show: boolean = false;

  mounted() {
    this.$store.commit("changeTitle", "Room List");
    this.$store.commit("changeSubtitle", "");
  }
}
</script>
