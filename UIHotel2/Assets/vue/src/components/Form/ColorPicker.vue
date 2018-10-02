<template>
  <div class="color-picker">
    <div class="form-control form-control-color" :style="getStyle(value_color)" @click="openModal"></div>
    <uiv-modal v-model="open" @hide="closeModal" title="Color Picker" append-to-body>
      <div class="column-flex" v-for="(col, idx) in color_array" :key="idx">
        <div v-for="(row, idy) in col"
        :key="idy"
        :class="getClass(row)"
        :style="getStyle(row)"
        @click="setColor(row)"></div>
      </div>
      <div class="clearfix"></div>

      <template slot="footer">
        <button class="btn btn-success" @click="emitSelect">Save</button>
        <button class="btn btn-danger" @click="closeModal">Cancel</button>
      </template>
    </uiv-modal>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop, Watch } from "vue-property-decorator";
import { ColorArray } from "@/lib/ColorList";

@Component
export default class ColorPicker extends Vue {
  private open: boolean = false;
  private color_array: Array<Array<string>> = new Array<Array<string>>();
  private select_color: string = "";
  private value_color: string = "";

  @Prop({ type: String, required: false })
  private value?: string;

  getStyle(color: string): any {
    let style = {
      "background-color": "#" + color
    };
    return style;
  }

  getClass(color: string): any {
    let default_class = ["row-flex"];

    if (color === this.select_color) default_class.push("selected");

    return default_class;
  }

  openModal() {
    this.open = true;
  }

  closeModal() {
    this.open = false;
  }

  setColor(color: string) {
    this.select_color = color;
  }

  emitSelect() {
    this.value_color = this.select_color;
    this.$emit("input", this.select_color);
    this.open = false;
  }

  mounted() {
    ColorArray.forEach((item: string[]) => {
      this.color_array.push(item);
    });
  }

  @Watch("value")
  changeValue(newval: string, oldval: string) {
    this.select_color = newval;
    this.value_color = newval;
  }
}
</script>
