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

        <uiv-modal title="Add Transaction" v-model="show_add" @hide="closeAll">
          <div class="form">
            <div class="form-group"></div>
          </div>

          <template slot="footer">
            <button class="btn btn-success" @click="storeData">Save</button>
            <button class="btn btn-danger" @click="closeAll">Cancel</button>
          </template>
        </uiv-modal>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import { ss, execute, executeScalar } from "@/lib/Test";
import moment from "moment";
import Pagination from "@/components/Table/Pagination.vue";
import Counter from "@/components/Table/Counter.vue";

@Component({
  components: {
    Pagination,
    Counter
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
  private show_add: boolean = false;
  private show_edit: boolean = false;
  
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
  
  closeAll() {
    this.show_add = false;
    this.show_edit = false;
  }

  addData() {
    //
  }

  editData(item: any) {
    //
  }

  storeData() {
    //
  }

  updateData() {
    //
  }

  deleteData(item: any) {
    //
  }

  bindFilter(qry: any): any {
    if (this.date_at != "") {
      let date_start = moment(this.date_at);
      let date_end = date_start.add(1, "d");

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
    //
  }

  mounted() {
    this.$store.commit("changeTitle", "List Transaction");
    this.$store.commit("changeSubtitle", "");
    this.getListCategory();
    this.getMaxItem();
    this.getItems();
  }
}
</script>
