<template>
  <div class="form-horizontal">
    <div class="form-group">
      <label class="col-md-3 control-label">Room Number :</label>
      <div class="col-md-3" v-if="IsRoomSelected">
        <div class="widget widget-stats" style="background-color:#00ACAC">
          <div class="stats-info">
            <h4>{{ modalData.RoomCategory }}</h4>
            <p>{{ modalData.RoomNumber }}</p>	
          </div>
          <div class="stats-link">
            <a href="javascript:;" @click="searchRoom">Change Room <i class="fa fa-arrow-circle-o-right"></i></a>
          </div>
        </div>
      </div>
      <div class="col-md-6" v-if="!IsRoomSelected">
        <button class="btn btn-info" @click="searchRoom">
          <i class="fa fa-search"></i>
          Select Room
        </button>
        <span class="text-danger" v-if="!IsRoomSelected && is_invalidate">Please select room</span>
      </div>
    </div>

    <div class="form-group">
      <label class="col-md-3 control-label">Note :</label>
      <div class="col-md-6">
        <textarea class="form-control" v-model="modalData.Note"></textarea>
      </div>
    </div>

    <uiv-modal v-model="is_show" title="Search Room" size="lg">
      <div class="row">
        <div class="col-md-12">
          <div class="form-inline">
            <input class="form-control" placeholder="Search Room" v-model="keyword"/>
          </div>
        </div>
      </div>
      <div class="row m-t-10">
        <div class="col-md-3" v-for="item in pageList" :key="item.Id">
          <div
          class="widget widget-stats selectable"
          :style="{ 'background-color':'#' + item.StateColor }"
          @click="selectRoom(item)">
            <div class="stats-info">
              <h4>{{ item.CategoryName }}</h4>
              <p>{{ item.RoomNumber }}</p>	
            </div>
            <div class="stats-link">
              <a>{{ item.StateName }}</a>
            </div>
          </div>
        </div>
      </div>
      <div class="row">
        <div class="col-md-12 text-center">
          <pagination :total-page.sync="totalPage" nomargin v-model="page"></pagination>
        </div>
      </div>

      <template slot="footer">
        <button class="btn btn-danger" @click="hideModal">Cancel</button>
      </template>
    </uiv-modal>
  </div>
</template>
<script lang="ts">
import Pagination from "@/components/Table/Pagination.vue";
import { Component, Vue, Prop } from "vue-property-decorator";
import { ss, se, execute } from "@/lib/Test";

interface IModalData {
  Id: string;
  RoomNumber: string;
  RoomCategory: string;
  RoomFloor: string;
  Note: string;
}

@Component({
  components: {
    Pagination
  }
})
export default class CreateStep3 extends Vue {
  private is_show: boolean = false;
  private is_invalidate: boolean = false;
  private listRoom: Array<any> = [];
  private page: number = 1;
  private limit: number = 8;
  private keyword: string = "";
  private modalData: IModalData = {
    Id: "",
    RoomNumber: "",
    RoomCategory: "",
    RoomFloor: "",
    Note: ""
  };

  get IsRoomSelected(): boolean {
    return this.modalData.Id != "";
  }

  get listFiltered(): Array<any> {
    if (this.keyword == "") return this.listRoom;

    let keyword = this.keyword;
    let result = this.listRoom.filter((item: any) => {
      return (
        item.RoomNumber.search(keyword) > -1 ||
        item.CategoryName.search(keyword) > -1
      );
    });

    return result;
  }

  get pageList(): Array<any> {
    let start = (this.page - 1) * this.limit;
    let end = start + this.limit;

    return this.listFiltered.slice(start, end);
  }

  get max_item(): number {
    return this.listFiltered.length;
  }

  get totalPage(): number {
    return Math.ceil(this.max_item / this.limit);
  }

  searchRoom() {
    this.is_show = true;
  }

  hideModal() {
    this.is_show = false;
  }

  selectRoom(item: any) {
    this.modalData.Id = item.Id;
    this.modalData.RoomNumber = item.RoomNumber;
    this.modalData.RoomFloor = item.RoomFloor;
    this.modalData.RoomCategory = item.CategoryName;
    this.hideModal();
  }

  getRoom() {
    let qry = ss()
      .from("rooms", "a")
      .join("roomstates", "b", "a.RoomStateId = b.Id")
      .join("roomcategories", "c", "a.RoomCategoryId = c.Id")
      .where(
        se()
          .and("b.Id = ?", 1)
          .or("b.Id = ?", 4)
      )
      .field("a.*")
      .field("b.StateName")
      .field("b.StateColor")
      .field("c.CategoryName");
    let result = execute(qry);

    this.listRoom = [];
    result.forEach((item: any) => this.listRoom.push(item));
  }

  validate() {
    this.is_invalidate = true;
  }

  mounted() {
    this.getRoom();
    window.bus.$on("book-validate", this.validate);
  }

  beforeDestroy() {
    window.bus.$off("book-validate", this.validate);
  }
}
</script>
