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
              <tr class="gradeA odd" role="row" v-for="i in 20" :key="i">
                  <td class="sorting_1" tabindex="0">201</td>
                  <td>BIG</td>
                  <td>1</td>
                  <td>
                    <div class="btn-group pull-right">
                      <button class="btn btn-sm btn-warning"><i class="fa fa-pencil"></i> Edit</button>
                      <button class="btn btn-sm btn-danger"><i class="fa fa-trash"></i> Delete</button>
                    </div>
                  </td>
              </tr>
              <tr class="gradeA even" role="row">
                  <td class="sorting_1" tabindex="0">202</td>
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
import Pagination from "@/components/Table/Pagination.vue";
import Counter from "@/components/Table/Counter.vue";

@Component({
  components: {
    Pagination,
    Counter
  }
})
export default class RoomMaintance extends Vue {
  private totalPage: number = 12;
  private currentPage: number = 1;

  mounted() {
    this.$store.commit("changeTitle", "Room Maintance");
    this.$store.commit("changeSubtitle", "");
  }

  @Watch("currentPage")
  currentPageChanged(newval: number, oldval: number) {
    console.log(newval);
  }
}
</script>
