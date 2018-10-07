<template>
  <div :class="GritterClass">
    <div class="gritter-top"></div>
    <div class="gritter-item">
      <a class="gritter-close" href="javascript:;">Close Notification</a>
      <div>
        <span class="gritter-title" v-html="Title"></span>
        <p v-html="Content"></p>
      </div>
      <div style="clear:both"></div>
    </div>
    <div class="gritter-bottom"></div>
  </div>
</template>
<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";

@Component
export default class NotificationItem extends Vue {
  @Prop({ required: true })
  public Id?: number;

  @Prop({ required: false, default: "" })
  public Title?: string;

  @Prop({ required: false, default: "" })
  public Content?: string;

  @Prop({ required: false, default: ""})
  public Type?: string;

  get GritterClass() {
    let default_class = ["gritter-item-wrapper"];

    switch(this.Type) {
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

  mounted() {
    //
  }
}
</script>
