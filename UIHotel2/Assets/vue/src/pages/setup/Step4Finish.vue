<template>
   <div class="row">
    <div class="panel panel-inverse">
      <div class="panel-heading">
        <h4 class="panel-title">Finish Installation</h4>
      </div>
      <div class="panel-body">
        <div data-scrollbar="true" data-height="350px">
          <div class="form-horizontal">
            <div class="form-group m-t-40">
              <div class="col-md-12">
                <h2 class="text-center"><i class="fa fa-flag-checkered fa-2x"></i></h2>
                <h4 class="text-center text-grey">Finish Installation</h4>
								<p class="text-center">This installation is finish, the application is ready for use.<br> Here the default user and password for login:</p>
                <p class="text-center">Username : <code>admin</code></p>
                <p class="text-center">Password : <code>admin</code></p>
              </div>
            </div>
          </div>
        </div>

        <div class="row">
          <div class="col-md-2 col-md-offset-10">
            <button class="btn btn-success btn-block" @click="finish">
              <span>Finish</span>&nbsp;
              <i class="fa fa-flag-checkered"></i>
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
export default class Step4 extends Vue {
  finish() {
    this.$router.push({ name: "dashboard" });
    this.$nextTick(() => window.bus.$emit("refresh-meta"));
  }

  mounted() {
    window.handleSlimScroll();
  }
}
</script>
