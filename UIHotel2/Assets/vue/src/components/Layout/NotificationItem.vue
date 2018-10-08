<template>
  <div :class="GritterClass">
    <div class="gritter-top"></div>
    <div class="gritter-item">
      <a class="gritter-close" href="javascript:;" @click="notifyTimeout">Close Notification</a>
      <div>
        <span class="gritter-title" v-html="title"></span>
        <p v-html="content"></p>
      </div>
      <div style="clear:both"></div>
    </div>
    <div class="gritter-bottom"></div>
  </div>
</template>
<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import { setTimeout, clearTimeout } from 'timers';

@Component
export default class NotificationItem extends Vue {
  @Prop({ required: true })
  public id?: number;

  @Prop({ required: false, default: "" })
  public title?: string;

  @Prop({ required: false, default: "" })
  public content?: string;

  @Prop({ required: false, default: ""})
  public type?: string;

  @Prop({ required: false, default: 5000 })
  public interval?: number;

  timeoutId: any;

  get GritterClass() {
    let default_class = ["gritter-item-wrapper"];

    switch(this.type) {
      case "success":
        default_class.push("gritter-success");
        break;
      case "warning":
        default_class.push("gritter-warning");
        break;
      case "error":
        default_class.push("gritter-error");
        break;
      case "info":
        default_class.push("gritter-info");
        break;
    }
    return default_class;
  }

  notifyTimeout() {
    clearTimeout(this.timeoutId);
    this.$emit("timeout");
  }

  mounted() {
    var interval: number = this.interval || 5000;
    this.timeoutId = setTimeout(this.notifyTimeout, interval);
  }
}
</script>
