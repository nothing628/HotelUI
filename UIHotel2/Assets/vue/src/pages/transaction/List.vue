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
            <label>Date Transaction:<input type="date" class="form-control input-sm m-r-5" placeholder="" ></label>
            <label>Search:<input type="text" class="form-control input-sm" placeholder="" ></label>
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
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
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

  mounted() {
    this.$store.commit("changeTitle", "List Transaction");
    this.$store.commit("changeSubtitle", "");
  }
}
</script>
