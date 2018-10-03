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
                <input
                class="form-control"
                name="category_name"
                v-model="modelData.CategoryName"
                v-validate="'required|max:40'"/>
                <span class="text-danger">{{ errors.first('category_name') }}</span>
              </div>
            </div>
            <div class="form-group">
              <label class="col-md-3 control-label">Icon</label>
              <div class="col-md-3">
                <icon-picker v-model="modelData.CategoryIcon"></icon-picker>
              </div>
              <label class="col-md-3 control-label">Color</label>
              <div class="col-md-3">
                <color-picker v-model="modelData.CategoryColor"></color-picker>
              </div>
            </div>
            <div class="form-group">
              <label class="col-md-3 control-label">Type</label>
              <div class="col-md-3">
                <select class="form-control" v-model="is_income">
                  <option value="1">Income</option>
                  <option value="0">Outcome</option>
                </select>
              </div>
            </div>
          </div>
          <template slot="footer">
            <button class="btn btn-success" @click="storeData">Save</button>
            <button class="btn btn-danger" @click="closeAll">Cancel</button>
          </template>
        </uiv-modal>

        <uiv-modal title="Edit Category" v-model="show_edit">
          <div class="form-horizontal">
            <div class="form-group">
              <label class="col-md-3 control-label">Category Name</label>
              <div class="col-md-6">
                <input
                class="form-control"
                name="category_name"
                v-model="modelData.CategoryName"
                v-validate="'required|max:40'"/>
                <span class="text-danger">{{ errors.first('category_name') }}</span>
              </div>
            </div>
            <div class="form-group">
              <label class="col-md-3 control-label">Icon</label>
              <div class="col-md-3">
                <icon-picker v-model="modelData.CategoryIcon"></icon-picker>
              </div>
              <label class="col-md-3 control-label">Color</label>
              <div class="col-md-3">
                <color-picker v-model="modelData.CategoryColor"></color-picker>
              </div>
            </div>
            <div class="form-group">
              <label class="col-md-3 control-label">Type</label>
              <div class="col-md-3">
                <select class="form-control" v-model="is_income">
                  <option value="1">Income</option>
                  <option value="0">Outcome</option>
                </select>
              </div>
            </div>
          </div>
          <template slot="footer">
            <button class="btn btn-success" @click="updateData">Save</button>
            <button class="btn btn-danger" @click="closeAll">Cancel</button>
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

interface IModelData {
  CategoryName: string;
  CategoryIcon: string;
  CategoryColor: string;
  IsIncome: boolean;
  Id: number;
}

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
  private show_add: boolean = false;
  private show_edit: boolean = false;
  private is_income: string = "0";
  private max_item: number = 0;
  private limit: number = 10;
  private currentPage: number = 1;
  private items: Array<any> = new Array();
  private modelData: IModelData = {
    CategoryName: "",
    CategoryIcon: "",
    CategoryColor: "",
    IsIncome: false,
    Id: 0
  };

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
    let arr_class: string[] = [icon];
    arr_class.push("fa-2x");
    arr_class.push("fa");

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
    this.modelData.Id = 0;
    this.modelData.CategoryName = "";
    this.modelData.CategoryIcon = "";
    this.modelData.CategoryColor = "";
    this.is_income = "1";
    this.show_add = true;
    this.$validator.reset();
  }

  storeData() {
    this.$validator.validateAll().then((is_valid: any) => {
      if (is_valid) {
        this.storeDataExe();
      }
    });
  }

  storeDataExe() {
    let qry = si()
      .into("transactioncategories")
      .set("CategoryName", this.modelData.CategoryName)
      .set("CategoryIcon", this.modelData.CategoryIcon)
      .set("CategoryColor", this.modelData.CategoryColor)
      .set("IsIncome", this.modelData.IsIncome);
    let result = executeScalar(qry);

    this.getMaxItem();
    this.getItems();
    this.closeAll();
  }

  editData(item: any) {
    this.modelData.Id = item.Id;
    this.modelData.CategoryName = item.CategoryName;
    this.modelData.CategoryColor = item.CategoryColor;
    this.modelData.CategoryIcon = item.CategoryIcon;
    this.is_income = item.IsIncome ? "1" : "0";
    this.show_edit = true;
  }

  updateData() {
    this.$validator.validateAll().then((is_valid: any) => {
      if (is_valid) {
        this.updateDataExe();
      }
    });
  }

  updateDataExe() {
    let qry = su()
      .table("transactioncategories")
      .set("CategoryName", this.modelData.CategoryName)
      .set("CategoryIcon", this.modelData.CategoryIcon)
      .set("CategoryColor", this.modelData.CategoryColor)
      .set("IsIncome", this.modelData.IsIncome)
      .where("Id = ?", this.modelData.Id);
    let result = executeScalar(qry);

    this.getMaxItem();
    this.getItems();
    this.closeAll();
  }

  deleteData(item: any) {
    let qry = sd()
      .from("transactioncategories")
      .where("Id = ?", item.Id);
    let result = executeScalar(qry);

    this.getMaxItem();
    this.getItems();
  }

  closeAll() {
    this.show_add = false;
    this.show_edit = false;
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

  @Watch("is_income")
  isIncomeChanged(newval: string, oldval: string) {
    this.modelData.IsIncome = this.is_income == "1";
  }
}
</script>
