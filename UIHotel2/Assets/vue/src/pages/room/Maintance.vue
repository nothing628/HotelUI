<template>
  <div class="row">
    <div class="panel panel-inverse">
      <div class="panel-heading">
        <h4 class="panel-title">Room Maintance</h4>
      </div>
      <div class="panel-body">
        <div class="dataTables_wrapper form-inline dt-bootstrap no-footer">
          <div class="dt-buttons btn-group">
            <a class="btn btn-success buttons-copy buttons-html5 btn-sm" @click="addData">
              <i class="fa fa-plus"></i>
              <span> Add Room</span>
            </a>
          </div>
          <div class="dataTables_filter">
            <label>Search:<input type="search" class="form-control input-sm" placeholder="" ></label>
          </div>
          <table class="table table-striped table-bordered dataTable no-footer dtr-inline" role="grid">
            <thead>
                <tr role="row">
                  <th rowspan="1" colspan="1">Room Number</th>
                  <th rowspan="1" colspan="1">Room Category</th>
                  <th rowspan="1" colspan="1">Room Floor</th>
                  <th rowspan="1" colspan="1">Status</th>
                  <th rowspan="1" colspan="1"></th>
                </tr>
            </thead>
            <tbody>
              <tr v-for="item in items" :key="item.Id">
                  <td>{{ item.RoomNumber }}</td>
                  <td>{{ item.CategoryName }}</td>
                  <td>{{ item.RoomFloor }}</td>
                  <td>{{ item.StateName }}</td>
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
        
        <uiv-modal v-model="open_add" title="Add Room">
          <div class="form-horizontal">
            <div class="form-group">
              <label class="col-md-4 control-label">Room Number</label>
              <div class="col-md-4">
                <input class="form-control" type="text" v-model="modal_data.RoomNumber" />
              </div>
            </div>
            <div class="form-group">
              <label class="col-md-4 control-label">Room Floor</label>
              <div class="col-md-4">
                <input class="form-control" type="text" v-model="modal_data.RoomFloor" />
              </div>
            </div>
            <div class="form-group">
              <label class="col-md-4 control-label">Category</label>
              <div class="col-md-6">
                <select class="form-control" v-model="modal_data.RoomCategoryId">
                  <option v-for="item in categories" :key="item.Id" :value="item.Id">{{ item.CategoryName }}</option>
                </select>
              </div>
            </div>
          </div>
          
          <template slot="footer">
            <button class="btn btn-success" @click="storeData">Save</button>
            <button class="btn btn-danger" @click="closeAll">Cancel</button>
          </template>
        </uiv-modal>

        <uiv-modal v-model="open_edit" title="Edit Room">
          <div class="form-horizontal">
            <div class="form-group">
              <label class="col-md-4 control-label">Room Number</label>
              <div class="col-md-4">
                <input class="form-control" type="text" v-model="modal_data.RoomNumber" />
              </div>
            </div>
            <div class="form-group">
              <label class="col-md-4 control-label">Room Floor</label>
              <div class="col-md-4">
                <input class="form-control" type="text" v-model="modal_data.RoomFloor" />
              </div>
            </div>
            <div class="form-group">
              <label class="col-md-4 control-label">Category</label>
              <div class="col-md-6">
                <select class="form-control" v-model="modal_data.RoomCategoryId">
                  <option v-for="item in categories" :key="item.Id" :value="item.Id">{{ item.CategoryName }}</option>
                </select>
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
import { squel, ss, su, sd, si, execute, executeScalar } from "@/lib/Test";
import Pagination from "@/components/Table/Pagination.vue";
import Counter from "@/components/Table/Counter.vue";

interface IModalData {
  RoomNumber: string;
  RoomFloor: string;
  RoomStateId: number;
  RoomCategoryId: number;
  Id: number;
}

@Component({
  components: {
    Pagination,
    Counter
  }
})
export default class RoomMaintance extends Vue {
  private open_add: boolean = false;
  private open_edit: boolean = false;
  private max_item: number = 0;
  private limit: number = 10;
  private currentPage: number = 1;
  private categories: Array<any> = new Array();
  private items: Array<any> = new Array();
  private modal_data: IModalData = {
    RoomStateId: 1,
    RoomCategoryId: 0,
    RoomNumber: "",
    RoomFloor: "",
    Id: 0
  };

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

  closeAll() {
    this.open_add = false;
    this.open_edit = false;
  }

  deleteData(item: IModalData) {
    let qry = sd()
      .from("rooms")
      .where("Id = ?", item.Id);
    let result = executeScalar(qry);

    this.getMaxItem();
    this.getItems();
  }

  editData(item: IModalData) {
    this.modal_data.Id = item.Id;
    this.modal_data.RoomNumber = item.RoomNumber;
    this.modal_data.RoomFloor = item.RoomFloor;
    this.modal_data.RoomStateId = item.RoomStateId;
    this.modal_data.RoomCategoryId = item.RoomCategoryId;
    this.open_edit = true;
  }

  addData() {
    this.modal_data.Id = 0;
    this.modal_data.RoomNumber = "";
    this.modal_data.RoomFloor = "";
    this.modal_data.RoomStateId = 1;
    this.modal_data.RoomCategoryId = 0;
    this.open_add = true;
  }

  updateData() {
    let qry = su()
      .table("rooms")
      .set("RoomNumber", this.modal_data.RoomNumber)
      .set("RoomFloor", this.modal_data.RoomFloor)
      .set("RoomCategoryId", this.modal_data.RoomCategoryId)
      .where("Id = ?", this.modal_data.Id);
    let result = executeScalar(qry);

    this.getMaxItem();
    this.getItems();
    this.closeAll();
  }

  storeData() {
    this.modal_data.RoomStateId = this.getStates();

    let qry = si()
      .into("rooms")
      .set("RoomNumber", this.modal_data.RoomNumber)
      .set("RoomFloor", this.modal_data.RoomFloor)
      .set("RoomStateId", this.modal_data.RoomStateId)
      .set("RoomCategoryId", this.modal_data.RoomCategoryId);
    let result = executeScalar(qry);

    this.getMaxItem();
    this.getItems();
    this.closeAll();
  }

  getStates(): number {
    let qry = ss()
      .from("roomstates")
      .where("StateName = ?", "Vacant")
      .field("Id");
    let result = execute(qry);
    let first = result[0];

    return first.Id;
  }

  getCategories() {
    let qry = ss().from("roomcategories");
    let result = execute(qry);

    result.forEach((item: any) => this.categories.push(item));
  }

  getMaxItem() {
    let qry = ss()
      .from("rooms")
      .field("COUNT(*) as cnt");
    let result = execute(qry);
    let first = result[0];

    this.max_item = Number(first.cnt);
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
      .limit(this.limit)
      .offset(this.offset);
    let result = execute(qry);

    this.items = [];
    result.forEach((item: any) => this.items.push(item));
  }

  mounted() {
    this.$store.commit("changeTitle", "Room Maintance");
    this.$store.commit("changeSubtitle", "");
    this.getCategories();
    this.getMaxItem();
    this.getItems();
  }

  @Watch("currentPage")
  currentPageChanged(newval: number, oldval: number) {
    this.getItems();
  }
}
</script>
