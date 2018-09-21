<template>
  <div class="panel panel-inverse">
    <div class="panel-heading">
      <h4 class="panel-title">Room Category</h4>
    </div>
    <div class="panel-body">
      <button @click="addData" class="btn btn-success"><i class="fa fa-plus"></i> Add Category</button>
      <table class="table table-hover table-striped m-t-10">
        <thead>
          <tr>
            <th>Category</th>
            <th>Description</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="item in items" :key="item.Id">
            <td>{{ item.CategoryName }}</td>
            <td>{{ item.CategoryDescription }}</td>
            <td>
              <div class="btn-group pull-right">
                <button class="btn btn-sm btn-warning" @click="editData(item)"><i class="fa fa-pencil"></i> Edit</button>
                <button class="btn btn-sm btn-danger" @click="deleteData(item)"><i class="fa fa-trash"></i> Delete</button>
              </div>
            </td>
          </tr>
        </tbody>
      </table>

      <uiv-modal v-model="open_add" title="Add Category">
        <div class="form-horizontal">
          <div class="form-group">
            <label class="col-md-4 control-label">Category Name</label>
            <div class="col-md-6">
              <input class="form-control" placeholder="Category Name" v-model="modal_data.CategoryName"/>
            </div>
          </div>
          <div class="form-group">
            <label class="col-md-4 control-label">Category Description</label>
            <div class="col-md-8">
              <textarea class="form-control" placeholder="Category Description" v-model="modal_data.CategoryDescription" rows="5"></textarea>
            </div>
          </div>
        </div>
        
        <template slot="footer">
          <button class="btn btn-success" @click="storeData">Save</button>
          <button class="btn btn-danger" @click="closeAll">Cancel</button>
        </template>
      </uiv-modal>

      <uiv-modal v-model="open_edit" title="Edit Category">
        <div class="form-horizontal">
          <div class="form-group">
            <label class="col-md-4 control-label">Category Name</label>
            <div class="col-md-6">
              <input class="form-control" placeholder="Category Name" v-model="modal_data.CategoryName"/>
            </div>
          </div>
          <div class="form-group">
            <label class="col-md-4 control-label">Category Description</label>
            <div class="col-md-8">
              <textarea class="form-control" placeholder="Category Description" v-model="modal_data.CategoryDescription" rows="5"></textarea>
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
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import { RoomCategory as TRoomCategory } from "@/lib/Model/RoomCategory";
import { squel, ss, su, sd, si, execute, executeScalar } from "@/lib/Test";

interface IModalData {
  CategoryName: string;
  CategoryDescription: string;
  Id: number;
}

@Component
export default class RoomCategory extends Vue {
  private open_add: boolean = false;
  private open_edit: boolean = false;
  private items: Array<any> = new Array<any>();
  private modal_data: IModalData = {
    CategoryName: "",
    CategoryDescription: "",
    Id: 0
  };

  closeAll() {
    this.open_add = false;
    this.open_edit = false;
  }

  deleteData(item: IModalData) {
    let id = item.Id;
    let command = sd()
      .from("roomcategories")
      .where("Id = ?", id);
    var result = executeScalar(command);

    this.getData();
  }

  editData(item: IModalData) {
    this.modal_data.CategoryName = item.CategoryName;
    this.modal_data.CategoryDescription = item.CategoryDescription;
    this.modal_data.Id = item.Id;
    this.open_edit = true;
  }

  addData() {
    this.modal_data.CategoryName = "";
    this.modal_data.CategoryDescription = "";
    this.modal_data.Id = 0;
    this.open_add = true;
  }

  updateData() {
    let command = su()
      .table("roomcategories")
      .set("CategoryName", this.modal_data.CategoryName)
      .set("CategoryDescription", this.modal_data.CategoryDescription)
      .where("Id = ?", this.modal_data.Id);
    let result = executeScalar(command);

    this.getData();
    this.closeAll();
  }

  storeData() {
    let command = si()
      .into("roomcategories")
      .set("CategoryName", this.modal_data.CategoryName)
      .set("CategoryDescription", this.modal_data.CategoryDescription);
    let result = executeScalar(command);

    this.getData();
    this.closeAll();
  }

  getData() {
    var tables = ss().from("roomcategories");
    var result = execute(tables);

    this.items = new Array<any>();
    result.forEach((item: any) => this.items.push(item));
  }

  mounted() {
    this.$store.commit("changeTitle", "Room Category");
    this.$store.commit("changeSubtitle", "");
    this.getData();
  }
}
</script>
