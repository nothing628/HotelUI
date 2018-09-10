<template>
  <div class="panel panel-inverse">
    <div class="panel-heading">
      <h4 class="panel-title">Room Category</h4>
    </div>
    <div class="panel-body">
      <button class="btn btn-success"><i class="fa fa-plus"></i> Add Category</button>
      <table class="table table-hover table-striped m-t-10">
        <thead>
          <tr>
            <th>Category</th>
            <th>Description</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>BIG</td>
            <td></td>
            <td>
              <div class="btn-group pull-right">
                <button class="btn btn-sm btn-warning"><i class="fa fa-pencil"></i> Edit</button>
                <button class="btn btn-sm btn-danger"><i class="fa fa-trash"></i> Delete</button>
              </div>
            </td>
          </tr>
          <tr>
            <td>MEDIUM</td>
            <td></td>
            <td>
              <div class="btn-group pull-right">
                <button class="btn btn-sm btn-warning"><i class="fa fa-pencil"></i> Edit</button>
                <button class="btn btn-sm btn-danger"><i class="fa fa-trash"></i> Delete</button>
              </div>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";

@Component
export default class RoomCategory extends Vue {
  mounted() {
    this.$store.commit("changeTitle", "Room Category");
    this.$store.commit("changeSubtitle", "");
  }
}
</script>
