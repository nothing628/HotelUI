<template>
  <div class="row">
    <div class="panel panel-inverse">
      <div class="panel-heading">
        <h4 class="panel-title">Migrating Database</h4>
      </div>
      <div class="panel-body">
        <div data-scrollbar="true" data-height="350px">
          <div class="form-horizontal">
            <div class="form-group m-t-40">
              <div class="col-md-12">
                <h2 class="text-center"><i class="fa fa-refresh fa-2x animate"></i></h2>
                <h4 class="text-center text-grey">Migrating Database</h4>
								<p class="text-center">Please don't turn off your computer until this process finish.</p>
              </div>
            </div>
          </div>
        </div>

        <div class="row">
          <div class="col-md-2 col-md-offset-10">
            <button class="btn btn-success btn-block" @click="next">
              <span>Next</span>&nbsp;
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
export default class Step2 extends Vue {
  next() {
    this.$router.push({ name: "setup.finish" });
  }

  mounted() {
    window.handleSlimScroll();
  }
}
</script>
