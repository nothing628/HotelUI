<template>
  <div class="row">
    <div class="col-md-12">
      <div class="row">
        <!-- begin col-3 -->
        <div class="col-md-3 col-sm-6">
          <div class="widget widget-stats bg-green">
            <div class="stats-icon"><i class="fa fa-users"></i></div>
            <div class="stats-info">
              <h4>TOTAL VISITORS</h4>
              <p>{{ TotalVisitor }}</p>	
            </div>
            <div class="stats-link">
              <a href="javascript:;" @click="goto('data.booking')">View Detail <i class="fa fa-arrow-circle-o-right"></i></a>
            </div>
          </div>
        </div>
        <!-- end col-3 -->
        <!-- begin col-3 -->
        <div class="col-md-3 col-sm-6">
          <div class="widget widget-stats bg-blue">
            <div class="stats-icon"><i class="fa fa-sign-in"></i></div>
            <div class="stats-info">
              <h4>TOTAL USED ROOM</h4>
              <p>{{ RoomUsed.Used }}/{{ RoomUsed.Total }}</p>	
            </div>
            <div class="stats-link">
              <a href="javascript:;" @click="goto('room.list')">View Detail <i class="fa fa-arrow-circle-o-right"></i></a>
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
              <p>{{ UniqueVistor }}</p>	
            </div>
            <div class="stats-link">
              <a href="javascript:;" @click="goto('data.booking')">View Detail <i class="fa fa-arrow-circle-o-right"></i></a>
            </div>
          </div>
        </div>
        <!-- end col-3 -->
        <!-- begin col-3 -->
        <div class="col-md-3 col-sm-6">
          <div class="widget widget-stats bg-red">
            <div class="stats-icon"><i class="fa fa-clock-o"></i></div>
            <div class="stats-info">
              <h4>BALANCE TODAY</h4>
              <p>{{ Balance | strcurrency }}</p>	
            </div>
            <div class="stats-link">
              <a href="javascript:;" @click="goto('report.finance')">View Detail <i class="fa fa-arrow-circle-o-right"></i></a>
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
              <div class="row">
                <div class="col-md-4">
                  <a href="javascript:;" class="btn btn-lg btn-block btn-success" @click="goto('data.booking')">
                    <i class="fa fa-sign-in pull-left"></i>
                    Check In
                  </a>
                </div>
                <div class="col-md-4">
                  <a href="javascript:;" class="btn btn-lg btn-block btn-success" @click="goto('data.booking.create')">
                    <i class="fa fa-bookmark pull-left"></i>
                    Booking
                  </a>
                </div>
                <div class="col-md-4">
                  <a href="javascript:;" class="btn btn-lg btn-block btn-success" @click="goto('data.guest')">
                    <i class="fa fa-user pull-left"></i>
                    Guest
                  </a>
                </div>
              </div>
              <div class="row m-t-10">
                <div class="col-md-4">
                  <a href="javascript:;" class="btn btn-lg btn-block btn-success" @click="goto('transaction.list')">
                    <i class="fa fa-retweet pull-left"></i>
                    Transaction
                  </a>
                </div>
                <div class="col-md-4">
                  <a href="javascript:;" class="btn btn-lg btn-block btn-success" @click="goto('report.finance')">
                    <i class="fa fa-line-chart pull-left"></i>
                    Report
                  </a>
                </div>
                <div class="col-md-4">
                  <a href="javascript:;" class="btn btn-lg btn-block btn-success" @click="goto('setting.app')">
                    <i class="fa fa-gear pull-left"></i>
                    Setting
                  </a>
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
import { IRoomUsed } from "@/lib/Interface";

@Component
export default class Dashboard extends Vue {
  get RoomUsed(): object {
    let room: IRoomUsed = window.CS.Hotel.Room;
    return {
      Used: room.Used,
      Total: room.Total
    };
  }

  get TotalVisitor(): number {
    return window.CS.Hotel.Visitor;
  }

  get UniqueVistor(): number {
    return window.CS.Hotel.UniqueVisitor;
  }

  get Balance(): number {
    return window.CS.Hotel.Balance;
  }

  goto(location: string) {
    this.$router.push({ name: location });
  }

  mounted() {
    this.$store.commit("changeTitle", "Dashboard");
    this.$store.commit("changeSubtitle", "");
  }
}
</script>
