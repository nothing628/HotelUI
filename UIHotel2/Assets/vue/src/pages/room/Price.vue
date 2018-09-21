<template>
  <div class="row">
    <div class="panel panel-inverse">
      <div class="panel-heading">
        <h4 class="panel-title">Room Price</h4>
      </div>
      <div class="panel-body">
        <div class="dataTables_wrapper form-inline dt-bootstrap no-footer">
          <div class="dt-buttons btn-group">
            <a class="btn btn-success buttons-copy buttons-html5 btn-sm" tabindex="0">
              <i class="fa fa-plus"></i>
              <span>Add Day Type</span>
            </a>
          </div>
          <div class="dataTables_filter">
            <label>Search:<input type="search" class="form-control input-sm" placeholder="" aria-controls="data-table"></label>
          </div>
          <table class="table table-striped table-bordered dataTable no-footer dtr-inline">
            <thead>
                <tr role="row">
                  <th tabindex="0" rowspan="1" colspan="1">Day Type</th>
                  <th tabindex="0" rowspan="1" colspan="1">Description</th>
                  <th tabindex="0" rowspan="1" colspan="1">Color</th>
                  <th tabindex="0" rowspan="1" colspan="1"></th>
                </tr>
            </thead>
            <tbody>
              <tr class="gradeA odd" role="row" v-for="i in 2" :key="i">
                  <td class="sorting_1" tabindex="0">Holiday</td>
                  <td>National Holiday / Religion Holiday</td>
                  <td style="background-color:#00acac"></td>
                  <td>
                    <div class="btn-group pull-right">
                      <button class="btn btn-sm btn-success"><i class="fa fa-plus"></i> Expand</button>
                      <button class="btn btn-sm btn-warning"><i class="fa fa-pencil"></i> Edit</button>
                      <button class="btn btn-sm btn-danger"><i class="fa fa-trash"></i> Delete</button>
                    </div>
                  </td>
              </tr>
              <tr class="gradeA even" role="row">
                  <td class="sorting_1" tabindex="0">Weekday</td>
                  <td>Weekday</td>
                  <td style="background-color: #f59c1a"></td>
                  <td>
                    <div class="btn-group pull-right">
                      <button class="btn btn-sm btn-success"><i class="fa fa-plus"></i> Expand</button>
                      <button class="btn btn-sm btn-warning"><i class="fa fa-pencil"></i> Edit</button>
                      <button class="btn btn-sm btn-danger"><i class="fa fa-trash"></i> Delete</button>
                    </div>
                  </td>
              </tr>
              <tr>
                <td colspan="4">
                  <price-expand :price-id="3"></price-expand>
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
import { squel, ss, su, sd, si, execute, executeScalar } from "@/lib/Test";
import PriceExpand from "@/components/Room/PriceExpand.vue";
import Pagination from "@/components/Table/Pagination.vue";
import Counter from "@/components/Table/Counter.vue";

@Component({
  components: {
    PriceExpand,
    Pagination,
    Counter
  }
})
export default class RoomPrice extends Vue {
  private open_add: boolean = false;
  private open_edit: boolean = false;
  private max_item: number = 0;
  private limit: number = 10;
  private currentPage: number = 1;

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

  getMaxItem() {
    let qry = ss()
      .from("roompricekinds")
      .field("COUNT(*) as cnt");
    let result = execute(qry);
    let first = result[0];

    this.max_item = Number(first.cnt);
  }

  mounted() {
    this.$store.commit("changeTitle", "Room Price");
    this.$store.commit("changeSubtitle", "");
    this.getMaxItem();
  }
}
</script>
