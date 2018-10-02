<template>
  <div class="row">
    <div class="panel panel-inverse">
      <div class="panel-heading">
        <h4 class="panel-title">Transaction Category</h4>
      </div>
      <div class="panel-body">
        <div class="dataTables_wrapper form-inline dt-bootstrap no-footer">
          <div class="dt-buttons btn-group m-b-10">
            <a class="btn btn-success buttons-copy buttons-html5 btn-sm" @click="addData">
              <i class="fa fa-plus"></i>
              <span> Add Category</span>
            </a>
          </div>
          <table class="table table-striped table-bordered dataTable no-footer dtr-inline" role="grid">
            <thead>
                <tr role="row">
                  <th rowspan="1" colspan="1">Category</th>
                  <th rowspan="1" colspan="1">Icon</th>
                  <th rowspan="1" colspan="1">Type</th>
                  <th rowspan="1" colspan="1"></th>
                </tr>
            </thead>
            <tbody>
              <tr v-for="item in items" :key="item.Id">
                  <td>{{ item.CategoryName }}</td>
                  <td><i :class="getIconClass(item)" :style="getIconStyle(item)"></i></td>
                  <td>{{ item.IsIncome | strincome }}</td>
                  <td>
                    <div class="btn-group pull-right">
                      <button class="btn btn-sm btn-warning" @click="editData(item)"><i class="fa fa-pencil"></i> Edit</button>
                      <button class="btn btn-sm btn-danger" @click="deleteData(item)"><i class="fa fa-trash"></i> Delete</button>
                    </div>
                  </td>
              </tr>
            </tbody>
          </table>
          <counter :from.sync="from" :to.sync="to" :total.sync="max_item"></counter>
          <pagination :total-page.sync="totalPage" v-model="currentPage"></pagination>
        </div>

        <uiv-modal title="Add Category" v-model="show_add">
          <div class="form-horizontal">
            <div class="form-group">
              <label class="col-md-3 control-label">Category Name</label>
              <div class="col-md-6">
                <input class="form-control"/>
              </div>
            </div>
            <div class="form-group">
              <label class="col-md-3 control-label">Icon</label>
              <div class="col-md-3">
                <icon-picker></icon-picker>
              </div>
              <label class="col-md-3 control-label">Color</label>
              <div class="col-md-3">
                <color-picker></color-picker>
              </div>
            </div>
            <div class="form-group">
              <label class="col-md-3 control-label">Type</label>
              <div class="col-md-3">
                <input class="form-control"/>
              </div>
            </div>
          </div>
          <template slot="footer">
            <button class="btn btn-danger">Cancel</button>
          </template>
        </uiv-modal>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop, Watch } from "vue-property-decorator";
import { squel, ss, su, sd, si, execute, executeScalar } from "@/lib/Test";
import Pagination from "@/components/Table/Pagination.vue";
import Counter from "@/components/Table/Counter.vue";
import ColorPicker from "@/components/Form/ColorPicker.vue";
import IconPicker from "@/components/Form/IconPicker.vue";

@Component({
  components: {
    Pagination,
    Counter,
    ColorPicker,
    IconPicker
  },
  filters: {
    strincome(item: boolean): string {
      if (item) {
        return "Income";
      }
      return "Outcome";
    }
  }
})
export default class TransactionCategory extends Vue {
  private show_add: boolean = true;
  private show_edit: boolean = false;
  private max_item: number = 0;
  private limit: number = 10;
  private currentPage: number = 1;
  private items: Array<any> = new Array();

  get totalPage(): number {
    return Math.ceil(this.max_item / this.limit);
  }

  get from(): number {
    return (this.currentPage - 1) * this.limit + 1;
  }

  get to(): number {
    let rest = this.from + 9;

    if (rest > this.max_item) return this.max_item;
    return this.from + 9;
  }

  get offset(): number {
    return (this.currentPage - 1) * this.limit;
  }

  getIconClass(item: any) {
    let icon: string = item.CategoryIcon;
    let arr_class = icon.split(" ");
    arr_class.push("fa-2x");

    return arr_class;
  }

  getIconStyle(item: any) {
    return {
      color: "#" + item.CategoryColor
    };
  }

  getMaxItem() {
    let qry = ss()
      .from("transactioncategories")
      .field("COUNT(*) as cnt");
    let result = execute(qry);
    let first = result[0];

    this.max_item = Number(first.cnt);
  }

  getItems() {
    let qry = ss()
      .from("transactioncategories", "a")
      .field("a.*")
      .limit(this.limit)
      .offset(this.offset);
    let result = execute(qry);

    this.items = [];
    result.forEach((item: any) => this.items.push(item));
  }

  addData() {
    this.show_add = true;
  }

  mounted() {
    this.$store.commit("changeTitle", "Transaction Category");
    this.$store.commit("changeSubtitle", "");
    this.getMaxItem();
    this.getItems();
  }

  @Watch("currentPage")
  currentPageChanged(newval: number, oldval: number) {
    this.getItems();
  }
}
</script>
