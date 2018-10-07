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
              <input class="form-control" readonly v-model="App_Key"/>
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
              <input class="form-control" v-model="Hotel_Name"/>
            </div>
          </div>
          <div class="form-group">
            <label class="col-md-3 control-label">Hotel Address</label>
            <div class="col-md-4">
              <textarea class="form-control" v-model="Hotel_Address"></textarea>
            </div>
          </div>
          <div class="form-group">
            <label class="col-md-3 control-label">Hotel Logo</label>
            <div class="col-md-2">
              <span class="navbar-logo"></span>
            </div>
            <div class="col-md-3">
              <button class="btn btn-info" @click="browseImage">Change</button>
            </div>
          </div>
          <div class="form-group">
            <label class="col-md-3 control-label">Deposit</label>
            <div class="col-md-2">
              <input class="form-control" type="number" v-model="Deposit"/>
            </div>
            <label class="col-md-2 control-label">Penalty Per Hour</label>
            <div class="col-md-2">
              <input class="form-control" type="number" v-model="Penalty"/>
            </div>
          </div>
          <div class="form-group">
            <label class="col-md-3 control-label">Time to Checkin</label>
            <div class="col-md-3">
              <time-picker v-model="Time_Checkin"/>
            </div>
            <label class="col-md-2 control-label">Time to Checkout</label>
            <div class="col-md-3">
              <time-picker v-model="Time_Checkout"/>
            </div>
          </div>
          <div class="form-group">
            <label class="col-md-3 control-label">Time Full Charge</label>
            <div class="col-md-3">
              <time-picker v-model="Time_FullCharge"/>
            </div>
          </div>
          <div class="form-group">
            <div class="col-md-3 col-md-offset-3">
              <button class="btn btn-success" @click="saveSetting">
                <i class="fa fa-floppy-o"></i>
                Save Setting
              </button>
            </div>
          </div>
          <div class="form-group">
            <h4>Database Settings</h4>
          </div>
          <div class="form-group">
            <label class="col-md-3 control-label">Database Server</label>
            <div class="col-md-3">
              <input class="form-control" v-model="SQL_Host"/>
            </div>
            <label class="col-md-3 control-label">Database Port</label>
            <div class="col-md-3">
              <input class="form-control" type="number" v-model="SQL_Port"/>
            </div>
          </div>
          <div class="form-group">
            <label class="col-md-3 control-label">Database Username</label>
            <div class="col-md-3">
              <input class="form-control" v-model="SQL_User"/>
            </div>
            <label class="col-md-3 control-label">Database Password</label>
            <div class="col-md-3">
              <input class="form-control" type="password" v-model="SQL_Password"/>
            </div>
          </div>
          <div class="form-group">
            <label class="col-md-3 control-label">Database Name</label>
            <div class="col-md-3">
              <input class="form-control" v-model="SQL_Database"/>
            </div>
          </div>
          <div class="form-group">
            <div class="col-md-3 col-md-offset-3">
              <button class="btn btn-success">
                <i class="fa fa-cube"></i>
                Test & Save Setting
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
import TimePicker from "@/components/Form/TimePicker.vue";

@Component({
  components: {
    TimePicker
  }
})
export default class SettingApplication extends Vue {
  App_Key: string = "";
  Hotel_Name: string = "";
  Hotel_Logo: string = "";
  Hotel_Address: string = "";
  Deposit: number = 0;
  Penalty: number = 0;
  Time_Checkin: string = "";
  Time_Checkout: string = "";
  Time_FullCharge: string = "";
  SQL_Database: string = "";
  SQL_Host: string = "";
  SQL_Port: number = 0;
  SQL_User: string = "";
  SQL_Password: string = "";

  mounted() {
    this.$store.commit("changeTitle", "Application Setting");
    this.$store.commit("changeSubtitle", "");
    this.copySetting();
  }

  browseImage() {
    window.CS.App.OpenDialog("Images|*.png;*.jpg", this.browseCallback);
  }

  browseCallback(data: any) {
    let hashname = data.hashname;

    this.Hotel_Logo = hashname;
  }

  copySetting() {
    this.App_Key = this.$store.state.Setting.App_Key;
    this.Hotel_Name = this.$store.state.Setting.Hotel_Name;
    this.Hotel_Logo = this.$store.state.Setting.Hotel_Logo;
    this.Hotel_Address = this.$store.state.Setting.Hotel_Address;
    this.Deposit = this.$store.state.Setting.Deposit;
    this.Penalty = this.$store.state.Setting.Penalty;
    this.Time_Checkin = this.$store.state.Setting.Time_Checkin;
    this.Time_Checkout = this.$store.state.Setting.Time_Checkout;
    this.Time_FullCharge = this.$store.state.Setting.Time_Fullcharge;
    this.SQL_Database = this.$store.state.Setting.SQL_Database;
    this.SQL_Host = this.$store.state.Setting.SQL_Host;
    this.SQL_Port = this.$store.state.Setting.SQL_Port;
    this.SQL_User = this.$store.state.Setting.SQL_User;
    this.SQL_Password = this.$store.state.Setting.SQL_Password;
  }

  loadSetting() {
    this.$store.dispatch("Setting/LoadSetting");
  }

  saveSetting() {
    
  }
}
</script>
