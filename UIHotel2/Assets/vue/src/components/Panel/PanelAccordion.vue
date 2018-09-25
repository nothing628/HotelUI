<template>
  <div class="panel panel-inverse overflow-hidden">
    <div class="panel-heading">
      <h3 class="panel-title">
        <a href="javascript:;" class="accordion-toggle accordion-toggle-styled" @click="toggleBody">
          <i class="fa fa-plus-circle pull-right"></i>{{ header }}
        </a>
      </h3>
    </div>
    <div class="panel-collapse collapse in">
      <div class="panel-body" ref="panelbody">
        <slot></slot>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import Vue from "vue";

export default Vue.extend({
  props: {
    header: { type: String, required: false, default: "" }
  },
  methods: {
    toggleBody() {
      var elem = this.$refs.panelbody;
      var jq_obj = $(elem);
      jq_obj.slideToggle();
    }
  }
});
</script>
