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
              <span>Add Room</span>
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
          <div class="dataTables_info" role="status" aria-live="polite">Showing 1 to 10 of 57 entries</div>
          <div class="dataTables_paginate paging_simple_numbers">
            <ul class="pagination">
              <li class="paginate_button previous disabled">
                <a href="#" aria-controls="data-table" data-dt-idx="0" tabindex="0">Previous</a>
              </li>
              <li class="paginate_button active">
                <a href="#" aria-controls="data-table" data-dt-idx="1" tabindex="0">1</a>
              </li>
              <li class="paginate_button ">
                <a href="#" aria-controls="data-table" data-dt-idx="2" tabindex="0">2</a>
              </li>
              <li class="paginate_button ">
                <a href="#" aria-controls="data-table" data-dt-idx="3" tabindex="0">3</a>
              </li>
              <li class="paginate_button ">
                <a href="#" aria-controls="data-table" data-dt-idx="4" tabindex="0">4</a>
              </li>
              <li class="paginate_button ">
                <a href="#" aria-controls="data-table" data-dt-idx="5" tabindex="0">5</a>
              </li>
              <li class="paginate_button ">
                <a href="#" aria-controls="data-table" data-dt-idx="6" tabindex="0">6</a>
              </li>
              <li class="paginate_button next">
                <a href="#" aria-controls="data-table" data-dt-idx="7" tabindex="0">Next</a>
              </li>
            </ul>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";

@Component
export default class RoomMaintance extends Vue {
  mounted() {
    this.$store.commit("changeTitle", "Room Maintance");
    this.$store.commit("changeSubtitle", "");
  }
}
</script>
