<template>
  <div class="panel-group">
    <panel-accordion
    v-for="category in listCategory"
    :key="category.Id"
    :header="getCategoryHeader(category)">
      <div class="row">
        <div class="col-md-2" v-for="item in getCategoryRoom(category)" :key="item.Id">
          <div class="widget widget-stats" :style="{'background-color': '#' + item.StateColor }">
            <div class="stats-info">
              <h4>{{ item.StateName }}</h4>
              <p>{{ item.RoomNumber }}</p>	
            </div>
            <div class="stats-link">
              <a href="javascript:;" @click="detailRoom(item)">More Detail <i class="fa fa-arrow-circle-o-right"></i></a>
            </div>
          </div>
        </div>
      </div>
    </panel-accordion>
    <uiv-modal v-model="open_detail" title="Room Action">
      <div class="form form-horizontal">
        <div class="form-group">
          <label class="col-md-3 control-label">Room Number</label>
          <div class="col-md-6">
            <input class="form-control" readonly/>
          </div>
        </div>
        <div class="form-group">
          <label class="col-md-3 control-label">Room Floor</label>
          <div class="col-md-6">
            <input class="form-control" readonly/>
          </div>
        </div>
        <div class="form-group">
          <label class="col-md-3 control-label">Category</label>
          <div class="col-md-6">
            <input class="form-control" readonly/>
          </div>
        </div>
        <div class="form-group">
          <label class="col-md-3 control-label">Status</label>
          <div class="col-md-6">
            <input class="form-control" readonly/>
          </div>
        </div>
      </div>

      <div class="row m-t-10">
        <div class="col-md-4">
          <button class="btn btn-success btn-block">
            <i class="fa fa-book"></i>
            Booking
          </button>
        </div>
        <div class="col-md-4">
          <button class="btn btn-success btn-block">
            <i class="fa fa-sign-in"></i>
            Check-In
          </button>
        </div>
        <div class="col-md-4">
          <button class="btn btn-success btn-block">
            <i class="fa fa-sign-out"></i>
            Check-Out
          </button>
        </div>
      </div>

      <div class="row m-t-10">
        <div class="col-md-4">
          <button class="btn btn-warning btn-block">
            <i class="fa fa-magic"></i>
            Finish Cleaning
          </button>
        </div>
        <div class="col-md-4">
          <button class="btn btn-warning btn-block">
            <i class="fa fa-wrench"></i>
            Maintance
          </button>
        </div>
      </div>

      <template slot="footer">
        <button class="btn btn-default btn-block" @click="open_detail = false">
          <i class="fa fa-times"></i>
          Close
        </button>
      </template>
    </uiv-modal>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import { ss, execute, executeScalar } from "@/lib/Test";
import PanelAccordion from "@/components/Panel/PanelAccordion.vue";

@Component({
  components: {
    PanelAccordion
  }
})
export default class RoomList extends Vue {
  private open_detail: boolean = false;
  private listCategory: Array<any> = new Array<any>();
  private listRoom: Array<any> = new Array<any>();

  detailRoom(item: any) {
    this.open_detail = true;
  }

  getCategoryHeader(item: any): string {
    return "CATEGORY : " + item.CategoryName.toUpperCase();
  }

  getCategoryRoom(item: any): Array<any> {
    let id = item.Id;
    let result = this.listRoom.filter((item: any) => {
      return item.RoomCategoryId == id;
    });
    return result;
  }

  getCategory() {
    let qry = ss().from("roomcategories");
    let result = execute(qry);

    result.forEach((item: any) => {
      this.listCategory.push(item);
    });
  }

  getRoom() {
    let qry = ss()
      .from("rooms", "a")
      .join("roomstates", "b", "a.RoomStateId = b.Id")
      .field("a.*")
      .field("b.StateName")
      .field("b.StateColor");
    let result = execute(qry);

    result.forEach((item: any) => {
      this.listRoom.push(item);
    });
  }

  mounted() {
    this.$store.commit("changeTitle", "Room List");
    this.$store.commit("changeSubtitle", "");
    this.getRoom();
    this.getCategory();
  }
}
</script>
