<template>
  <div class="input-group bootstrap-timepicker timepicker">
    <input class="form-control" type="text" v-model="valuex" maxlength="8">
    <span class="input-group-addon" @click="toggleFloat">
      <i class="glyphicon glyphicon-time"></i>
    </span>
    <float-control ref="test" @close="closeAll" v-model="valuex" v-if="show"></float-control>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop, Watch } from "vue-property-decorator";
import FloatControl from "./TimePickerFloat.vue";
import moment from "moment";

@Component({
  components: {
    FloatControl
  }
})
export default class TimePicker extends Vue {
  private valuex: string = "";
  private show: boolean = false;

  @Prop({ type: String, required: false })
  private value?: string;

  closeAll() {
    this.show = false;
  }

  toggleFloat() {
    this.show = !this.show;
  }

  parseValue() {
    this.valuex = "00:00:00";

    if (this.value != undefined && this.value != "") {
      let parse = moment(this.value, "HH:mm:ss");

      this.valuex = parse.format("HH:mm:ss");
    }

    this.$nextTick(() => {
      this.$emit("input", this.valuex);
    });
  }

  @Watch("show")
  changeShow(newval: any, oldval: any) {
    this.$emit("input", this.valuex);
  }

  @Watch("value")
  changeValue() {
    this.parseValue();
  }

  mounted() {
    this.parseValue();
  }
}
</script>
