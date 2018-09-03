<template>
  <div class="row">
    <div class="col-md-12">
      <div class="row">
        <!-- begin col-3 -->
        <div class="col-md-3 col-sm-6">
          <div class="widget widget-stats bg-green">
            <div class="stats-icon"><i class="fa fa-desktop"></i></div>
            <div class="stats-info">
              <h4>TOTAL VISITORS</h4>
              <p>3,291,922</p>	
            </div>
            <div class="stats-link">
              <a href="javascript:;">View Detail <i class="fa fa-arrow-circle-o-right"></i></a>
            </div>
          </div>
        </div>
        <!-- end col-3 -->
        <!-- begin col-3 -->
        <div class="col-md-3 col-sm-6">
          <div class="widget widget-stats bg-blue">
            <div class="stats-icon"><i class="fa fa-chain-broken"></i></div>
            <div class="stats-info">
              <h4>BOUNCE RATE</h4>
              <p>20.44%</p>	
            </div>
            <div class="stats-link">
              <a href="javascript:;">View Detail <i class="fa fa-arrow-circle-o-right"></i></a>
            </div>
          </div>
        </div>
        <!-- end col-3 -->
        <!-- begin col-3 -->
        <div class="col-md-3 col-sm-6">
          <div class="widget widget-stats bg-purple">
            <div class="stats-icon"><i class="fa fa-users"></i></div>
            <div class="stats-info">
              <h4>UNIQUE VISITORS</h4>
              <p>1,291,922</p>	
            </div>
            <div class="stats-link">
              <a href="javascript:;">View Detail <i class="fa fa-arrow-circle-o-right"></i></a>
            </div>
          </div>
        </div>
        <!-- end col-3 -->
        <!-- begin col-3 -->
        <div class="col-md-3 col-sm-6">
          <div class="widget widget-stats bg-red">
            <div class="stats-icon"><i class="fa fa-clock-o"></i></div>
            <div class="stats-info">
              <h4>AVG TIME ON SITE</h4>
              <p>00:12:23</p>	
            </div>
            <div class="stats-link">
              <a href="javascript:;">View Detail <i class="fa fa-arrow-circle-o-right"></i></a>
            </div>
          </div>
        </div>
        <!-- end col-3 -->
      </div>
      <div class="row">
        <div class="col-md-12">
          <div class="panel panel-inverse">
            <div class="panel-heading">
							<h4 class="panel-title">Dashboard Menu</h4>
						</div>
            <div class="panel-body">
              <a href="javascript:;" class="btn btn-lg btn-success">
                <i class="fa fa-twitter fa-2x pull-left"></i>
                Twitter Bootstrap<br>
                <small>Version 3.0</small>
              </a>
              <a href="javascript:;" class="btn btn-lg btn-success">
                <i class="fa fa-twitter fa-2x pull-left"></i>
                Twitter Bootstrap<br>
                <small>Version 3.0</small>
              </a>
              <a href="javascript:;" class="btn btn-lg btn-success">
                <i class="fa fa-twitter fa-2x pull-left"></i>
                Twitter Bootstrap<br>
                <small>Version 3.0</small>
              </a>
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
export default class Dashboard extends Vue {}
</script>
