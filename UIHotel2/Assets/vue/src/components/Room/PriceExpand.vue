<template>
  <table class="table table-striped table-bordered dataTable no-footer dtr-inline">
    <thead>
        <tr role="row">
          <th tabindex="0" rowspan="1" colspan="1">Room Category</th>
          <th tabindex="0" rowspan="1" colspan="1">Room Price</th>
          <th tabindex="0" rowspan="1" colspan="1"></th>
        </tr>
    </thead>
    <tbody>
      <tr class="gradeA odd" role="row" v-for="i in 2" :key="i">
          <td class="sorting_1" tabindex="0">BIG</td>
          <td>Rp 500,000</td>
          <td>
            <div class="btn-group pull-right">
              <button class="btn btn-sm btn-warning"><i class="fa fa-pencil"></i> Edit</button>
              <button class="btn btn-sm btn-danger"><i class="fa fa-trash"></i> Delete</button>
            </div>
          </td>
      </tr>
      <tr class="gradeA even" role="row">
          <td class="sorting_1" tabindex="0">MEDIUM</td>
          <td>Rp 600,000</td>
          <td>
            <div class="btn-group pull-right">
              <button class="btn btn-sm btn-warning"><i class="fa fa-pencil"></i> Edit</button>
              <button class="btn btn-sm btn-danger"><i class="fa fa-trash"></i> Delete</button>
            </div>
          </td>
      </tr>
    </tbody>
  </table>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";

@Component
export default class PriceExpand extends Vue {}
</script>
