<template>
  <div>
    <table class="table table-striped table-bordered dataTable no-footer dtr-inline">
      <thead>
          <tr role="row">
            <th tabindex="0" rowspan="1" colspan="1">Room Category</th>
            <th tabindex="0" rowspan="1" colspan="1">Room Price</th>
            <th tabindex="0" rowspan="1" colspan="1"></th>
          </tr>
      </thead>
      <tbody>
        <tr v-for="item in items" :key="item.Id">
            <td>{{ item.CategoryName }}</td>
            <td>{{ item.Price | currency_price}}</td>
            <td>
              <div class="btn-group pull-right">
                <button class="btn btn-sm btn-warning" @click="editData(item)"><i class="fa fa-pencil"></i> Edit</button>
              </div>
            </td>
        </tr>
      </tbody>
    </table>

    <uiv-modal v-model="open" @hide="closeAll" title="Update Price">
      <div class="form-horizontal">
        <div class="form-group">
          <label class="col-md-3 control-label">Category</label>
          <div class="col-md-5">
            <input class="form-control" readonly type="text" v-model="modal_data.CategoryName" />
          </div>
        </div>
        <div class="form-group">
          <label class="col-md-3 control-label">Price</label>
          <div class="col-md-5">
            <input class="form-control" type="number" v-model="modal_data.Price" />
          </div>
        </div>
      </div>

      <template slot="footer">
        <button class="btn btn-success" @click="updateData">Save</button>
        <button class="btn btn-danger" @click="closeAll">Cancel</button>
      </template>
    </uiv-modal>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import { squel, se, ss, su, sd, si, execute, executeScalar } from "@/lib/Test";

interface IModalData {
  CategoryName: string;
  Price: number;
  RoomCategoryId: number;
  RoomPriceId?: number;
}

@Component({
  filters: {
    currency_price(val: any) {
      if (val == null) return "Rp0";

      let formater = new Intl.NumberFormat("id-ID", { style: 'currency', currency: 'IDR' });

      return formater.format(val);
    }
  }
})
export default class PriceExpand extends Vue {
  private open: boolean = false;
  private items: Array<any> = new Array<any>();
  private modal_data: IModalData = {
    CategoryName: "",
    Price: 0,
    RoomCategoryId: 0,
    RoomPriceId: undefined
  };

  @Prop({ required: true })
  public PriceId?: number;

  closeAll() {
    this.open = false;
  }

  editData(item: any) {
    this.modal_data.CategoryName = item.CategoryName;
    this.modal_data.Price = item.Price || 0;
    this.modal_data.RoomCategoryId = item.Id;
    this.modal_data.RoomPriceId = item.RoomPriceId;
    this.open = true;
  }

  updateData() {
    if (this.modal_data.RoomPriceId === null) {
      this.storeData();
    } else {
      let qry = su()
        .table("roomprices")
        .set("Price", this.modal_data.Price)
        .where("Id = ?", this.modal_data.RoomPriceId);
      let result = executeScalar(qry);

      this.getData();
    }

    this.closeAll();
  }

  storeData() {
    let qry = si()
      .into("roomprices")
      .set("Price", this.modal_data.Price)
      .set("RoomCategoryId", this.modal_data.RoomCategoryId)
      .set("RoomPriceKindId", this.PriceId);
    let result = executeScalar(qry);

    this.getData();
  }

  getData() {
    this.createDummy();
    let qry = ss()
      .from("roomcategories", "a")
      .join("roomprices", "b", "a.Id = b.RoomCategoryId")
      .field("a.Id")
      .field("a.CategoryName")
      .field("b.Id as RoomPriceId")
      .field("b.Price")
      .where("b.RoomPriceKindId = ?", this.PriceId);
    let result = execute(qry);

    this.items.forEach((val, idx) => {
      var data = result.filter((x: any) => x.Id == val.Id);

      if (data.length > 0) {
        this.items[idx].RoomPriceId = data[0].RoomPriceId;
        this.items[idx].Price = data[0].Price;
      }
    });
  }

  createDummy() {
    let qry = ss()
      .from("roomcategories", "a")
      .field("a.Id")
      .field("a.CategoryName")
      .field("NULL as RoomPriceId")
      .field("0 as Price");
    let result = execute(qry);
    this.items = [];
    result.forEach((item: any) => {
      this.items.push(item);
    });
  }

  mounted() {
    this.getData();
  }
}
</script>
