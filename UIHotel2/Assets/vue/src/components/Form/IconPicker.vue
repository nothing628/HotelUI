<template>
  <div class="color-picker">
    <button class="btn btn-default" @click="openModal">
      <i class="fa fa-pencil" :class="getClass(value_icon)"></i>
    </button>
    <uiv-modal v-model="open" @hide="closeModal" title="Icon Picker" append-to-body>
      <div class="column-flex" v-for="(col, idx) in icon_array" :key="idx">
        <div v-for="(row, idy) in col"
        :key="idy"
        :class="getClass(row)"
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
import { IconArray } from "@/lib/IconList";

@Component
export default class IconPicker extends Vue {
  private open: boolean = false;
  private icon_array: Array<string> = new Array<string>();
  private value_icon: string = "";

  @Prop({ type: String, required: false })
  private value?: string;

  emitSelect() {
    //
  }

  setIcon(item: any) {
    //
  }

  getClass(item: any) {
    //
  }

  openModal() {
    this.open = true;
  }

  closeModal() {
    this.open = false;
  }

  mounted() {
    IconArray.forEach((item: string) => {
      this.icon_array.push(item);
    });
  }
}
</script>
