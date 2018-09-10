<template>
  <div class="panel-group" id="accordion">
    <div class="panel panel-inverse overflow-hidden">
      <div class="panel-heading">
        <h3 class="panel-title">
          <a class="accordion-toggle accordion-toggle-styled" data-toggle="collapse" data-parent="#accordion" href="#collapseOne">
            <i class="fa fa-plus-circle pull-right"></i>CATEGORY : BIG
          </a>
        </h3>
      </div>
      <div id="collapseOne" class="panel-collapse collapse">
        <div class="panel-body">
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
        </div>
      </div>
    </div>
    <div class="panel panel-inverse overflow-hidden">
      <div class="panel-heading">
        <h3 class="panel-title">
          <a class="accordion-toggle accordion-toggle-styled" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo">
            <i class="fa fa-plus-circle pull-right"></i>CATEGORY : MEDIUM
          </a>
        </h3>
      </div>
      <div id="collapseTwo" class="panel-collapse collapse">
        <div class="panel-body">
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
        </div>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";

@Component
export default class RoomList extends Vue {
  mounted() {
    this.$store.commit("changeTitle", "Room List");
    this.$store.commit("changeSubtitle", "");
  }
}
</script>
