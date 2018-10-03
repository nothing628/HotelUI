<template>
  <div class="color-picker">
    <button class="btn" @click="openModal">
      <i :class="getClass(value_icon)"></i>
    </button>
    <uiv-modal v-model="open" @hide="closeModal" title="Icon Picker" append-to-body>
      <div class="column-flex" v-for="(col, idx) in listPage" :key="idx">
        <button @click="setIcon(col)" class="row-flex btn" :style="getStyle(col)">
          <i :class="getClass(col)"></i>
        </button>
      </div>
      <div class="clearfix"></div>
      <pagination :total-page.sync="totalPage" v-model="currentPage"></pagination>

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
import Pagination from "@/components/Table/Pagination.vue";

@Component({
  components: {
    Pagination
  }
})
export default class IconPicker extends Vue {
  private open: boolean = false;
  private icon_array: Array<string> = new Array<string>();
  private value_icon: string = "";
  private select_icon: string = "";
  private currentPage: number = 1;
  private limit: number = 112;

  get totalItem(): number {
    return this.icon_array.length;
  }

  get totalPage(): number {
    return Math.ceil(this.totalItem / this.limit);
  }

  get listPage() {
    let start = (this.currentPage - 1) * this.limit;
    let end = start + this.limit;

    return this.icon_array.slice(start, end);
  }

  @Prop({ type: String, required: false })
  private value?: string;

  emitSelect() {
    this.value_icon = this.select_icon;
    this.$emit("input", this.select_icon);
    this.open = false;
  }

  setIcon(item: any) {
    this.select_icon = item;
  }

  getStyle(item: any) {
    if (this.select_icon == item) {
      return {
        "background-color": "#868686",
        color: "#FFFFFF"
      };
    }

    return {};
  }

  getClass(item: any) {
    return ["fa", item];
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

    if (this.value_icon == "") {
      this.value_icon = "fa-ellipsis-h";
    }
  }

  @Watch("value")
  changeValue(newval: string, oldval: string) {
    if (newval.length == 0) {
      this.value_icon = "fa-ellipsis-h";
      this.select_icon = "fa-ellipsis-h";
      this.$emit("input", "fa-ellipsis-h");
    } else {
      this.select_icon = newval;
      this.value_icon = newval;
    }
  }
}
</script>
