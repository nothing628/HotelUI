<template>
  <div class="row">
    <div class="col-md-6 col-md-offset-3">
      <div class="panel panel-inverse">
        <div class="panel-heading">
          <h4 class="panel-title">Change Password</h4>
        </div>
        <div class="panel-body">
          <div class="form-horizontal">
            <div class="form-group">
              <label class="col-md-4 control-label">Current Password</label>
              <div class="col-md-6">
                <input class="form-control" type="password" v-model="CurrentPassword" />
              </div>
            </div>
            <div class="form-group">
              <label class="col-md-4 control-label">New Password</label>
              <div class="col-md-6">
                <input class="form-control" type="password" v-model="NewPassword" />
              </div>
            </div>
            <div class="form-group">
              <label class="col-md-4 control-label">Repeat Password</label>
              <div class="col-md-6">
                <input class="form-control" type="password" v-model="ConfirmPassword" />
              </div>
            </div>

            <div class="form-group">
              <div class="col-md-8 col-md-offset-4">
                <button class="btn btn-success m-r-5">Change Password</button>
                <button class="btn btn-danger m-r-5">Cancel</button>
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
export default class ChangePass extends Vue {
  CurrentPassword: string = "";
  NewPassword: string = "";
  ConfirmPassword: string = "";

  mounted() {
    this.$store.commit("changeTitle", "Change Password");
    this.$store.commit("changeSubtitle", "");
  }
}
</script>
