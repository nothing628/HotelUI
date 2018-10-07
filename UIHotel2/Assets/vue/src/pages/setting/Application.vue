<template>
  <div class="row">
    <div class="panel panel-inverse">
      <div class="panel-heading">
        <h4 class="panel-title">Application Setting</h4>
      </div>
      <div class="panel-body">
        <div class="form-horizontal form-bordered">
          <div class="form-group">
            <h4>App Settings</h4>
          </div>
          <div class="form-group">
            <label class="col-md-3 control-label">App Key</label>
            <div class="col-md-4">
              <input class="form-control" readonly/>
            </div>
          </div>
          <div class="form-group">
            <label class="col-md-3 control-label">App Logo</label>
            <div class="col-md-4">
              <span class="navbar-logo"></span>
            </div>
          </div>
          <div class="form-group">
            <h4>Hotel Settings</h4>
          </div>
          <div class="form-group">
            <label class="col-md-3 control-label">Hotel Name</label>
            <div class="col-md-4">
              <input class="form-control"/>
            </div>
          </div>
          <div class="form-group">
            <label class="col-md-3 control-label">Hotel Address</label>
            <div class="col-md-4">
              <textarea class="form-control"></textarea>
            </div>
          </div>
          <div class="form-group">
            <label class="col-md-3 control-label">Hotel Logo</label>
            <div class="col-md-4">
              <input class="form-control"/>
            </div>
          </div>
          <div class="form-group">
            <label class="col-md-3 control-label">Deposit</label>
            <div class="col-md-2">
              <input class="form-control" type="number"/>
            </div>
            <label class="col-md-2 control-label">Penalty Per Hour</label>
            <div class="col-md-2">
              <input class="form-control" type="number"/>
            </div>
          </div>
          <div class="form-group">
            <label class="col-md-3 control-label">Time to Checkin</label>
            <div class="col-md-2">
              <input class="form-control"/>
            </div>
            <label class="col-md-2 control-label">Time to Checkout</label>
            <div class="col-md-2">
              <input class="form-control"/>
            </div>
          </div>
          <div class="form-group">
            <label class="col-md-3 control-label">Time Fullcharge</label>
            <div class="col-md-2">
              <input class="form-control"/>
            </div>
          </div>
          <div class="form-group">
            <div class="col-md-3 col-md-offset-3">
              <button class="btn btn-success">
                <i class="fa fa-floppy-o"></i>
                Save App Setting
              </button>
            </div>
          </div>
          <div class="form-group">
            <h4>Database Settings</h4>
          </div>
          <div class="form-group">
            <label class="col-md-3 control-label">Database Server</label>
            <div class="col-md-3">
              <input class="form-control"/>
            </div>
          </div>
          <div class="form-group">
            <label class="col-md-3 control-label">Database Username</label>
            <div class="col-md-3">
              <input class="form-control"/>
            </div>
            <label class="col-md-3 control-label">Database Password</label>
            <div class="col-md-3">
              <input class="form-control" type="password"/>
            </div>
          </div>
          <div class="form-group">
            <label class="col-md-3 control-label">Database Server</label>
            <div class="col-md-3">
              <input class="form-control"/>
            </div>
          </div>
          <div class="form-group">
            <div class="col-md-3 col-md-offset-3">
              <button class="btn btn-success">
                <i class="fa fa-cube"></i>
                Save Database Setting
              </button>
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
export default class SettingApplication extends Vue {
  mounted() {
    this.$store.commit("changeTitle", "Application Setting");
    this.$store.commit("changeSubtitle", "");
  }
}
</script>
