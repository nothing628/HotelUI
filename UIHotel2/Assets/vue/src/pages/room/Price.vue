<template>
  <div class="row">
    <div class="panel panel-inverse">
      <div class="panel-heading">
        <h4 class="panel-title">Room Price</h4>
      </div>
      <div class="panel-body">
        <div class="dataTables_wrapper form-inline dt-bootstrap no-footer">
          <div class="dt-buttons btn-group">
            <a class="btn btn-success buttons-copy buttons-html5 btn-sm" @click="addData">
              <i class="fa fa-plus"></i>
              <span> Add Day Type</span>
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
              <template v-for="item in items">
                <tr :key="item.Id">
                  <td>{{ item.KindName }}</td>
                  <td>{{ item.KindDescription }}</td>
                  <td :style="getStyle(item.KindColor)"></td>
                  <td>
                    <div class="btn-group pull-right">
                      <button class="btn btn-sm btn-success" @click="expandData(item)"><i class="fa fa-plus"></i> Expand</button>
                      <button class="btn btn-sm btn-warning" @click="editData(item)"><i class="fa fa-pencil"></i> Edit</button>
                      <button class="btn btn-sm btn-danger" @click="deleteData(item)"><i class="fa fa-trash"></i> Delete</button>
                    </div> 
                  </td>
                </tr>
                <tr v-if="item.is_expand" :key="item.KindName">
                  <td colspan="4">
                    <price-expand :price-id="3"></price-expand>
                  </td>
                </tr>
              </template>
            </tbody>
          </table>
          <counter :from.sync="from" :to.sync="to" :total.sync="max_item"></counter>
          <pagination :total-page.sync="totalPage" v-model="currentPage"></pagination>
        </div>

        <uiv-modal v-model="open_add" title="Edit Room">
          <div class="form-horizontal">
            <div class="form-group">
              <label class="col-md-3 control-label">Day Type</label>
              <div class="col-md-5">
                <input class="form-control" type="text" v-model="modal_data.KindName" />
              </div>
            </div>
            <div class="form-group">
              <label class="col-md-3 control-label">Description</label>
              <div class="col-md-9">
                <textarea class="form-control" v-model="modal_data.KindDescription"></textarea>
              </div>
            </div>
            <div class="form-group">
              <label class="col-md-3 control-label">Color Mark</label>
              <div class="col-md-4">
                <color-picker v-model="modal_data.KindColor"></color-picker>
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
              <label class="col-md-3 control-label">Day Type</label>
              <div class="col-md-5">
                <input class="form-control" type="text" v-model="modal_data.KindName" />
              </div>
            </div>
            <div class="form-group">
              <label class="col-md-3 control-label">Description</label>
              <div class="col-md-9">
                <textarea class="form-control" v-model="modal_data.KindDescription"></textarea>
              </div>
            </div>
            <div class="form-group">
              <label class="col-md-3 control-label">Color Mark</label>
              <div class="col-md-4">
                <color-picker v-model="modal_data.KindColor"></color-picker>
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
import { Component, Vue, Prop } from "vue-property-decorator";
import { squel, ss, su, sd, si, execute, executeScalar } from "@/lib/Test";
import PriceExpand from "@/components/Room/PriceExpand.vue";
import Pagination from "@/components/Table/Pagination.vue";
import Counter from "@/components/Table/Counter.vue";
import ColorPicker from "@/components/Form/ColorPicker.vue";

interface IModalData {
  KindColor: string;
  KindDescription: string;
  KindName: string;
  is_expand: boolean;
  Id: number;
}

@Component({
  components: {
    ColorPicker,
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
  private items: Array<IModalData> = new Array<IModalData>();
  private modal_data: IModalData = {
    KindColor: "",
    KindDescription: "",
    KindName: "",
    Id: 0,
    is_expand: false
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
  
  addData() {
    this.modal_data.KindColor = "EEEEEE";
    this.modal_data.KindDescription = "";
    this.modal_data.KindName = "";
    this.modal_data.Id = 0;
    this.modal_data.is_expand = false;
    this.open_add = true;
  }

  deleteData(item: IModalData) {
    let qry = sd()
      .from("roompricekinds")
      .where("Id = ?", item.Id)
    let result = executeScalar(qry);

    this.getMaxItem();
    this.getData();
  }

  editData(item: IModalData) {
    this.modal_data.KindColor = item.KindColor;
    this.modal_data.KindDescription = item.KindDescription;
    this.modal_data.KindName = item.KindName;
    this.modal_data.Id = item.Id;
    this.open_edit = true;
  }

  updateData() {
    let qry = su()
      .table("roompricekinds")
      .set("KindName", this.modal_data.KindName)
      .set("KindDescription", this.modal_data.KindDescription)
      .set("KindColor", this.modal_data.KindColor)
      .where("Id = ?", this.modal_data.Id);
    let result = executeScalar(qry);

    this.getData();
    this.closeAll();
  }

  storeData() {
    let qry = si()
      .into("roompricekinds")
      .set("KindName", this.modal_data.KindName)
      .set("KindDescription", this.modal_data.KindDescription)
      .set("KindColor", this.modal_data.KindColor)
    let result = executeScalar(qry);

    this.getMaxItem();
    this.getData();
    this.closeAll();
  }

  expandData(item: IModalData) {
    item.is_expand = !item.is_expand;
  }

  getMaxItem() {
    let qry = ss()
      .from("roompricekinds")
      .field("COUNT(*) as cnt");
    let result = execute(qry);
    let first = result[0];

    this.max_item = Number(first.cnt);
  }

  getData() {
    let qry = ss()
      .from("roompricekinds")
      .limit(this.limit)
      .offset(this.offset);
    let result = execute(qry);

    this.items = [];
    result.forEach((item: any) => {
      let data = Object.assign({}, item);
      data.is_expand = false;

      this.items.push(data);
    });
  }

  getStyle(color: string) {
    let result = {
      "background-color": "#" + color
    };

    return result;
  }

  mounted() {
    this.$store.commit("changeTitle", "Room Price");
    this.$store.commit("changeSubtitle", "");
    this.getMaxItem();
    this.getData();
  }
}
</script>
