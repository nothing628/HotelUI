<template>
  <div class="row">
    <div class="panel panel-inverse">
      <div class="panel-heading">
        <h4 class="panel-title">Migrating Database</h4>
      </div>
      <div class="panel-body">
        <div data-scrollbar="true" data-height="350px">
          <div class="form-horizontal" v-if="is_loading">
            <div class="form-group m-t-40">
              <div class="col-md-12">
                <h2 class="text-center"><i class="fa fa-refresh fa-2x animate"></i></h2>
                <h4 class="text-center text-grey">Migrating Database</h4>
								<p class="text-center">Please don't turn off your computer until this process finish.</p>
              </div>
            </div>
          </div>

          <div class="form-horizontal" v-if="!is_loading && !is_success">
            <div class="form-group m-t-40">
              <div class="col-md-12">
                <h2 class="text-center"><i class="fa fa fa-times-circle-o fa-2x"></i></h2>
                <h4 class="text-center text-grey">Failed Migrating Database</h4>
								<p class="text-center">Whoops, this shouldn't be happen. Please ensure the credentials are able to create database.</p>
              </div>
            </div>
          </div>
        </div>

        <div class="row">
          <div class="col-md-2 col-md-offset-10">
            <button class="btn btn-success btn-block" @click="next" v-if="is_loading" disabled>
              <span>Please wait</span>&nbsp;
            </button>

            <button class="btn btn-danger btn-block" @click="next" v-if="!is_loading && !is_success">
              <span>Cancel</span>&nbsp;
              <i class="fa fa-times"></i>
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
export default class Step2 extends Vue {
  is_loading: boolean = true;
  is_success: boolean = false;

  next() {
    this.$router.push({ name: "setup.finish" });
  }

  migrate() {
    let params = this.$router.currentRoute.query;

    let SQL_Database: string = params.SQL_Database; 
    let SQL_Host: string = params.SQL_Host; 
    let SQL_Port: string = params.SQL_Port; 
    let SQL_User: string = params.SQL_User; 
    let SQL_Password: string = params.SQL_Password; 

    window.CS.DB.Migrate(
      SQL_Host,
      Number(SQL_Port),
      SQL_User,
      SQL_Password,
      SQL_Database,
      (data: boolean) => {
        if (data) {
          this.next();
        } else {
          this.is_loading = false;
          this.is_success = false;
        }
      }
    );
  }

  mounted() {
    window.handleSlimScroll();
    this.migrate();
  }
}
</script>
