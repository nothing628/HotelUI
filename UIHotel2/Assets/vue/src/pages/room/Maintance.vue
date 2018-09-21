<template>
  <div class="row">
    <div class="panel panel-inverse">
      <div class="panel-heading">
        <h4 class="panel-title">Room Maintance</h4>
      </div>
      <div class="panel-body">
        <div class="dataTables_wrapper form-inline dt-bootstrap no-footer">
          <div class="dt-buttons btn-group">
            <a class="btn btn-success buttons-copy buttons-html5 btn-sm" tabindex="0">
              <i class="fa fa-plus"></i>
              <span> Add Room</span>
            </a>
          </div>
          <div class="dataTables_filter">
            <label>Search:<input type="search" class="form-control input-sm" placeholder="" aria-controls="data-table"></label>
          </div>
          <table class="table table-striped table-bordered dataTable no-footer dtr-inline" role="grid" aria-describedby="data-table_info">
            <thead>
                <tr role="row">
                  <th tabindex="0" rowspan="1" colspan="1">Room Number</th>
                  <th tabindex="0" rowspan="1" colspan="1">Room Category</th>
                  <th tabindex="0" rowspan="1" colspan="1">Room Floor</th>
                  <th tabindex="0" rowspan="1" colspan="1"></th>
                </tr>
            </thead>
            <tbody>
              <tr>
                  <td>202</td>
                  <td>MEDIUM</td>
                  <td>1</td>
                  <td>
                    <div class="btn-group pull-right">
                      <button class="btn btn-sm btn-warning"><i class="fa fa-pencil"></i> Edit</button>
                      <button class="btn btn-sm btn-danger"><i class="fa fa-trash"></i> Delete</button>
                    </div>
                  </td>
              </tr>
            </tbody>
          </table>
          <counter></counter>
          <pagination :total-page="totalPage" v-model="currentPage"></pagination>
        </div>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop, Watch } from "vue-property-decorator";
import { squel, ss, su, sd, si, execute, executeScalar } from "@/lib/Test";
import Pagination from "@/components/Table/Pagination.vue";
import Counter from "@/components/Table/Counter.vue";

@Component({
  components: {
    Pagination,
    Counter
  }
})
export default class RoomMaintance extends Vue {
  private max_item: number = 0;
  private totalPage: number = 12;
  private currentPage: number = 1;
  private categories: Array<any> = new Array();
  private items: Array<any> = new Array();

  getCategories() {
    let qry = ss().from("roomcategories");
    let result = execute(qry);

    result.forEach((item: any) => this.categories.push(item));
  }

  getMaxItem(): number {
    let qry = ss()
      .from("rooms")
      .field("COUNT(*)");
    let result = execute(qry);
    let first = result[0];

    return Number(Object.values(first)[0]);
  }

  getItems() {
    let qry = ss()
      .from("rooms", "a")
      .join("roomcategories", "b", "a.RoomCategoryId = b.Id")
      .join("roomstates", "c", "a.RoomStateId = c.Id")
      .field("a.*")
      .field("b.CategoryName")
      .field("c.StateName")
      .field("c.StateColor")
      .limit(10)
      .offset(0);
    let result = execute(qry);

    console.log(result);
  }

  mounted() {
    this.$store.commit("changeTitle", "Room Maintance");
    this.$store.commit("changeSubtitle", "");
    this.max_item = this.getMaxItem();
    this.getCategories();
    this.getItems();
  }

  @Watch("currentPage")
  currentPageChanged(newval: number, oldval: number) {
    console.log(newval);
  }
}
</script>
