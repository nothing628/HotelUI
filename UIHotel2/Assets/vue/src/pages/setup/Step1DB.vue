<template>
  <div class="row">
    <div class="panel panel-inverse">
      <div class="panel-heading">
        <h4 class="panel-title">Database Setup</h4>
      </div>
      <div class="panel-body">
        <div data-scrollbar="true" data-height="350px">
          <div class="form-horizontal">
            <div class="form-group">
              <div class="col-md-12">
                <h2 class="text-center"><i class="fa fa-database fa-2x"></i></h2>
                <h4 class="text-center text-grey">Database Installation</h4>
              </div>
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
          </div>
        </div>

        <div class="row">
          <div class="col-md-2 col-md-offset-8">
            <button class="btn btn-danger btn-block" @click="cancel">
              <i class="fa fa-times"></i>&nbsp;
              <span>Cancel</span>
            </button>
          </div>
          <div class="col-md-2">
            <button class="btn btn-success btn-block" @click="testConnect">
              Next
              <i class="fa fa-chevron-right"></i>
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";

@Component
export default class Step1 extends Vue {
  SQL_Database: string = "hotelx";
  SQL_Host: string = "localhost";
  SQL_Port: number = 3306;
  SQL_User: string = "root";
  SQL_Password: string = "";

  testConnect() {
    var port: number = Number(this.SQL_Port);

    window.CS.DB.TestConnect(
      this.SQL_Host,
      port,
      this.SQL_User,
      this.SQL_Password,
      (e: boolean) => {
        if (e) {
          this.next();
        } else {
          window.bus.$emit("Notify", {
            Title: "Database Test",
            Content: "Failed to connect",
            Type: "error"
          });
        }
      }
    );
  }

  next() {
    let params = {
      SQL_Database: this.SQL_Database,
      SQL_Host: this.SQL_Host,
      SQL_Port: this.SQL_Port.toString(),
      SQL_User: this.SQL_User,
      SQL_Password: this.SQL_Password
    };

    this.$router.push({ name: "setup.migrate", query: params });
  }

  cancel() {
    window.close();
  }

  mounted() {
    window.handleSlimScroll();
  }
}
</script>
