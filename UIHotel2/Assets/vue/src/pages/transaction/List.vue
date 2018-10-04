<template>
  <div class="row">
    <div class="panel panel-inverse">
      <div class="panel-heading">
        <h4 class="panel-title">List Transaction</h4>
      </div>
      <div class="panel-body">
        <div class="dataTables_wrapper form-inline dt-bootstrap no-footer">
          <div class="dt-buttons btn-group m-b-10">
            <a class="btn btn-success buttons-copy buttons-html5 btn-sm" @click="addData">
              <i class="fa fa-plus"></i>
              <span> New Transaction</span>
            </a>
          </div>
          <div class="dataTables_filter">
            <label>Date Transaction:<input type="date" class="form-control input-sm m-r-5" v-model="date_at"/></label>
            <label>Search:<input type="text" class="form-control input-sm" v-model="keyword"/></label>
          </div>
          <table class="table table-striped table-bordered dataTable no-footer dtr-inline" role="grid">
            <thead>
                <tr role="row">
                  <th>Date</th>
                  <th>Description</th>
                  <th>Category</th>
                  <th>Ammount In</th>
                  <th>Ammount Out</th>
                  <th>Subtotal</th>
                  <th></th>
                </tr>
            </thead>
            <tbody>
              <tr v-for="item in items" :key="item.Id">
                  <td>{{ item.TransactionAt | strtime }}</td>
                  <td>{{ item.Description }}</td>
                  <td>{{ item.CategoryName }}</td>
                  <td>{{ item.AmmountIn }}</td>
                  <td>{{ item.AmmountOut }}</td>
                  <td>{{ item.Subtotal }}</td>
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

        <uiv-modal title="Add Transaction" v-model="show_add" @hide="closeAll">
          <div class="form form-horizontal">
            <div class="form-group">
              <label class="col-md-3 control-label">Transaction At</label>
              <div class="col-md-4">
                <input
                type="date"
                class="form-control"
                name="date_at"
                v-model="modelData.TransactionDate"
                v-validate="'required'"
                :max="maxDate"/>
                <span class="text-danger">{{ errors.first('date_at') }}</span>
              </div>
              <div class="col-md-5">
                <time-picker v-model="modelData.TransactionTime"></time-picker>
              </div>
            </div>
            <div class="form-group">
              <label class="col-md-3 control-label">Kind</label>
              <div class="col-md-3">
                <select class="form-control" v-model="kind">
                  <option value="1">Income</option>
                  <option value="0">Outcome</option>
                </select>
              </div>
              <label class="col-md-2 control-label">Category</label>
              <div class="col-md-4">
                <select
                class="form-control"
                name="category"
                v-model="modelData.CategoryId"
                v-validate="'required'">
                  <option
                  v-for="item in listFilteredCategory"
                  :key="item.Id"
                  :value="item.Id">
                    {{ item.CategoryName }}
                  </option>
                </select>
                <span class="text-danger">{{ errors.first('category') }}</span>
              </div>
            </div>
            <div class="form-group">
              <label class="col-md-3 control-label">Description</label>
              <div class="col-md-9">
                <textarea
                class="form-control"
                name="description"
                v-model="modelData.Description"
                v-validate="'max:200'"></textarea>
                <span class="text-danger">{{ errors.first('description') }}</span>
              </div>
            </div>
            <div class="form-group">
              <label class="col-md-3 control-label">Ammount</label>
              <div class="col-md-4">
                <input
                type="number"
                class="form-control"
                name="ammount"
                min="0"
                v-model="modelData.Ammount"
                v-validate="'required|min_value:1'"/>
                <span class="text-danger">{{ errors.first('ammount') }}</span>
              </div>
            </div>
          </div>

          <template slot="footer">
            <button class="btn btn-success" @click="storeData">Save</button>
            <button class="btn btn-danger" @click="closeAll">Cancel</button>
          </template>
        </uiv-modal>

        <uiv-modal title="Add Transaction" v-model="show_edit" @hide="closeAll">
          <div class="form form-horizontal">
            <div class="form-group">
              <label class="col-md-3 control-label">Transaction At</label>
              <div class="col-md-4">
                <input
                type="date"
                class="form-control"
                name="date_at"
                v-model="modelData.TransactionDate"
                v-validate="'required'"
                :max="maxDate"/>
                <span class="text-danger">{{ errors.first('date_at') }}</span>
              </div>
              <div class="col-md-5">
                <time-picker v-model="modelData.TransactionTime"></time-picker>
              </div>
            </div>
            <div class="form-group">
              <label class="col-md-3 control-label">Kind</label>
              <div class="col-md-3">
                <select class="form-control" v-model="kind">
                  <option value="1">Income</option>
                  <option value="0">Outcome</option>
                </select>
              </div>
              <label class="col-md-2 control-label">Category</label>
              <div class="col-md-4">
                <select
                class="form-control"
                name="category"
                v-model="modelData.CategoryId"
                v-validate="'required'">
                  <option
                  v-for="item in listFilteredCategory"
                  :key="item.Id"
                  :value="item.Id">
                    {{ item.CategoryName }}
                  </option>
                </select>
                <span class="text-danger">{{ errors.first('category') }}</span>
              </div>
            </div>
            <div class="form-group">
              <label class="col-md-3 control-label">Description</label>
              <div class="col-md-9">
                <textarea
                class="form-control"
                name="description"
                v-model="modelData.Description"
                v-validate="'max:200'"></textarea>
                <span class="text-danger">{{ errors.first('description') }}</span>
              </div>
            </div>
            <div class="form-group">
              <label class="col-md-3 control-label">Ammount</label>
              <div class="col-md-4">
                <input
                type="number"
                class="form-control"
                name="ammount"
                min="0"
                v-model="modelData.Ammount"
                v-validate="'required|min_value:1'"/>
                <span class="text-danger">{{ errors.first('ammount') }}</span>
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
import { ss, si, su, sd, execute, executeScalar } from "@/lib/Test";
import moment from "moment";
import Pagination from "@/components/Table/Pagination.vue";
import Counter from "@/components/Table/Counter.vue";
import TimePicker from "@/components/Form/TimePicker.vue";

interface IModelData {
  TransactionDate: string;
  TransactionTime: string;
  CategoryId: string;
  Description: string;
  Ammount: number;
  Id: number;
}

@Component({
  components: {
    Pagination,
    Counter,
    TimePicker
  },
  filters: {
    strtime(item: string): string {
      let m = moment(item);

      return m.format("DD/MM HH:mm");
    }
  }
})
export default class TransactionList extends Vue {
  private listCategory: Array<any> = new Array<any>();
  private items: Array<any> = new Array<any>();
  private max_item: number = 0;
  private limit: number = 20;
  private currentPage: number = 1;
  private date_at: string = "";
  private keyword: string = "";
  private kind: string = "1";
  private show_add: boolean = false;
  private show_edit: boolean = false;
  private user_level: number = -1;
  private user_id: number = -1;
  private modelData: IModelData = {
    TransactionDate: "",
    TransactionTime: "",
    CategoryId: "",
    Description: "",
    Ammount: 0,
    Id: 0
  };

  @Watch("date_at")
  @Watch("keyword")
  changeSearch() {
    this.getMaxItem();
    this.getItems();
  }

  get maxDate(): string {
    return moment().format("YYYY-MM-DD");
  }

  get listFilteredCategory(): Array<any> {
    return this.listCategory.filter((item: any) => {
      if (this.kind == "1") {
        return item.IsIncome;
      } else {
        return !item.IsIncome;
      }
    });
  }

  get from(): number {
    return (this.currentPage - 1) * this.limit + 1;
  }

  get to(): number {
    let value = this.from + this.limit - 1;

    if (value > this.max_item) {
      return this.max_item;
    }

    return value;
  }

  get offset(): number {
    return (this.currentPage - 1) * this.limit;
  }

  get totalPage(): number {
    return Math.ceil(this.max_item / this.limit);
  }

  getCategoryKind(id: any): boolean | undefined {
    let result = this.listCategory.filter((item: any) => {
      return item.Id == id;
    });

    if (result.length > 0) {
      return result[0].IsIncome;
    }

    return;
  }

  closeAll() {
    this.show_add = false;
    this.show_edit = false;
  }

  addData() {
    let today = moment();

    this.modelData.TransactionDate = "";
    this.modelData.TransactionTime = today.format("HH:mm:ss");
    this.modelData.CategoryId = "";
    this.modelData.Description = "";
    this.modelData.Ammount = 0;
    this.show_add = true;
    this.$validator.reset();
  }

  editData(item: any) {
    let time_str = moment(item.TransactionAt);
    let time_exp = moment().add(-7, "d");

    if (item.IsClosed) {
      // Cannot change, only administrator can change
      // It's should 7 days before expired to change
      let is_valid_time = time_exp.isBefore(time_str);
      let is_valid_user = this.user_level == 1 || this.user_level == 0;

      if (is_valid_time && is_valid_user) {
        this.editDataExe(item);
      }
    } else {
      this.editDataExe(item);
    }
  }

  editDataExe(item: any) {
    let ammount = item.AmmountIn - item.AmmountOut;
    let is_income = this.getCategoryKind(item.CategoryId);
    let time_str = moment(item.TransactionAt);

    this.modelData.Id = item.Id;
    this.modelData.Description = item.Description;
    this.modelData.Ammount = Math.abs(ammount);
    this.modelData.CategoryId = item.CategoryId;
    this.modelData.TransactionDate = time_str.format("YYYY-MM-DD");
    this.modelData.TransactionTime = time_str.format("HH:mm:ss");
    this.kind = is_income ? "1" : "0";
    this.show_edit = true;
  }

  storeData() {
    this.$validator.validateAll().then((is_valid: boolean) => {
      if (is_valid) {
        this.storeDataExe();
      }
    });
  }

  storeDataExe() {
    let ammountIn = this.kind == "1" ? this.modelData.Ammount : 0;
    let ammountOut = this.kind == "0" ? this.modelData.Ammount : 0;
    let subtotal = ammountIn - ammountOut;
    let date = this.modelData.TransactionDate;
    let time = this.modelData.TransactionTime;
    let datetime = date + " " + time;
    let qry = si()
      .into("transactions")
      .set("IsClosed", false)
      .set("TransactionAt", datetime)
      .set("AmmountIn", ammountIn)
      .set("AmmountOut", ammountOut)
      .set("Subtotal", subtotal)
      .set("UserId", this.user_id)
      .set("CategoryId", this.modelData.CategoryId)
      .set("Description", this.modelData.Description);
    let result = executeScalar(qry);

    this.getMaxItem();
    this.getItems();
    this.closeAll();
  }

  updateData() {
    this.$validator.validateAll().then((is_valid: boolean) => {
      if (is_valid) {
        this.updateDataExe();
      }
    });
  }

  updateDataExe() {
    let ammountIn = this.kind == "1" ? this.modelData.Ammount : 0;
    let ammountOut = this.kind == "0" ? this.modelData.Ammount : 0;
    let subtotal = ammountIn - ammountOut;
    let date = this.modelData.TransactionDate;
    let time = this.modelData.TransactionTime;
    let datetime = date + " " + time;
    let qry = su()
      .table("transactions")
      .set("TransactionAt", datetime)
      .set("AmmountIn", ammountIn)
      .set("AmmountOut", ammountOut)
      .set("Subtotal", subtotal)
      .set("UserId", this.user_id)
      .set("CategoryId", this.modelData.CategoryId)
      .set("Description", this.modelData.Description)
      .where("Id = ?", this.modelData.Id);
    let result = executeScalar(qry);

    this.getMaxItem();
    this.getItems();
    this.closeAll();
  }

  deleteData(item: any) {
    let qry = sd()
      .from("transactions")
      .where("Id = ?", item.Id);
    let result = executeScalar(qry);

    this.getMaxItem();
    this.getItems();
  }

  bindFilter(qry: any): any {
    qry.order("TransactionAt", false);

    if (this.date_at != "") {
      let date_start = moment(this.date_at);
      let date_end = date_start.clone().add(1, "d");

      qry.where("TransactionAt > ?", date_start.format("YYYY-MM-DD"));
      qry.where("TransactionAt < ?", date_end.format("YYYY-MM-DD"));
    }

    if (this.keyword != "") {
      qry.where("Description LIKE ?", "%" + this.keyword + "%");
    }

    return qry;
  }

  getListCategory() {
    let qry = ss()
      .from("transactioncategories")
      .limit(this.limit)
      .offset(this.offset);
    let result = execute(qry);

    this.listCategory = [];
    result.forEach((item: any) => this.listCategory.push(item));
  }

  getMaxItem() {
    let qry = ss()
      .from("transactions")
      .field("COUNT(*) as cnt");

    qry = this.bindFilter(qry);

    let result = execute(qry);
    let first = result[0];

    this.max_item = Number(first.cnt);
  }

  getItems() {
    let qry = ss().from("transactions", "a");

    qry = this.bindFilter(qry);
    qry
      .join("transactioncategories", "b", "a.CategoryId = b.Id")
      .field("a.*")
      .field("b.CategoryName");

    let result = execute(qry);

    this.items = [];
    result.forEach((item: any) => this.items.push(item));
  }

  mounted() {
    this.user_id = this.$store.getters["User/user_id"];
    this.user_level = this.$store.getters["User/level"];
    this.$store.commit("changeTitle", "List Transaction");
    this.$store.commit("changeSubtitle", "");
    this.getListCategory();
    this.getMaxItem();
    this.getItems();
  }
}
</script>
