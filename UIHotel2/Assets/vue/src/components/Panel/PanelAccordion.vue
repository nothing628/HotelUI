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
  data() {
    return {
      is_show: true
    };
  },
  props: {
    header: { type: String, required: false, default: "" },
    value: { type: Boolean, required: false, default: false }
  },
  methods: {
    toggleBody() {
      this.is_show = !this.is_show;
      this.changeState();
      this.$emit("input", this.is_show);

      if (this.is_show) {
        this.$emit("show");
      } else {
        this.$emit("hide");
      }
    },
    changeState() {
      var elem = this.$refs.panelbody;
      var jq_obj = $(elem);

      if (this.is_show) {
        jq_obj.slideDown();
      } else {
        jq_obj.slideUp();
      }
    }
  },
  watch: {
    value: {
      handler(newvalue, oldvalue) {
        this.is_show = newvalue;
        this.changeState();
      }
    }
  }
});
</script>
