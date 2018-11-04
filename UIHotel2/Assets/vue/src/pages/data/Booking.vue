<template>
  <div class="row">
    <div class="panel panel-inverse">
      <div class="panel-heading">
        <h4 class="panel-title">List Booking</h4>
      </div>
      <div class="panel-body">
        <div class="dataTables_wrapper form-inline dt-bootstrap no-footer">
          <div class="dt-buttons btn-group">
            <a class="btn btn-success buttons-copy buttons-html5 btn-sm" @click="add">
              <i class="fa fa-book"></i>
              <span> New Booking</span>
            </a>
          </div>
          <div class="dataTables_filter">
            <label>Search:<input type="search" class="form-control input-sm" placeholder="" ></label>
          </div>
          <table class="table table-striped table-bordered dataTable no-footer dtr-inline" role="grid">
            <thead>
                <tr role="row">
                  <th rowspan="1" colspan="1">Booking ID</th>
                  <th>Booking Time</th>
                  <th>Checkin Time</th>
                  <th rowspan="1" colspan="1">Arrival</th>
                  <th rowspan="1" colspan="1">Departure</th>
                  <th rowspan="1" colspan="1">Status</th>
                  <th rowspan="1" colspan="1"></th>
                </tr>
            </thead>
            <tbody>
              <tr v-for="item in items" :key="item.Id" :class="getStateColor(item)">
                  <td>{{ item.Id }}</td>
                  <td>{{ item.BookingAt | strdate("DD/MM/YYYY HH:mm") }}</td>
                  <td>{{ item.CheckinAt | strdate("DD/MM/YYYY HH:mm") }}</td>
                  <td>{{ item.ArrivalDate | strlongdate }}</td>
                  <td>{{ item.DepartureDate | strlongdate }}</td>
                  <td>{{ getStateName(item) }}</td>
                  <td>
                    <div class="btn-group pull-right">
                      <button class="btn btn-sm btn-success" @click="detail(item)">
                        <i class="fa fa-pencil"></i> Action
                      </button>
                    </div>
                  </td>
              </tr>
            </tbody>
          </table>
          <counter :from.sync="from" :to.sync="to" :total.sync="max_item"></counter>
          <pagination :total-page.sync="totalPage" v-model="currentPage"></pagination>
        </div>

        <booking-detail v-if="show_form" :booking-id="selectedId" @close="show_form = false" @refresh="refresh"></booking-detail>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import { squel, ss, su, sd, si, execute, executeScalar, executeFirst } from "@/lib/Test";
import { Booking, BookStatusType } from "@/lib/Model/ModelCollection";
import Pagination from "@/components/Table/Pagination.vue";
import Counter from "@/components/Table/Counter.vue";
import BookingDetail from "@/pages/data/booking/Detail.vue";
import moment from "moment";
import { isNull, isNullOrUndefined, isUndefined } from "util";

@Component({
  components: {
    Pagination,
    Counter,
    BookingDetail
  }
})
export default class DataBooking extends Vue {
  private show_form: boolean = false;
  private max_item: number = 0;
  private currentPage: number = 1;
  private limit: number = 10;
  private items: Array<any> = new Array();
  private selectedId: string = "";

  get from(): number {
    return this.offset + 1;
  }

  get to(): number {
    let rest = this.from + this.limit - 1;

    if (rest > this.max_item) {
      return this.max_item;
    }

    return rest;
  }

  get offset(): number {
    return (this.currentPage - 1) * this.limit;
  }

  get totalPage(): number {
    return Math.ceil(this.max_item / this.limit);
  }

  get checkoutTime(): string {
    return this.$store.state.Setting.Time_Checkout;
  }

  get checkinTime(): string {
    return this.$store.state.Setting.Time_Checkin;
  }

  getStateName(item: any): string {
    var state: BookStatusType = this.getStateNum(item);

    return Booking.GetStatusName(state);
  }

  getStateNum(item: any): BookStatusType {
    return Booking.GetStatus(
      item.ArrivalDate,
      item.DepartureDate,
      item.CheckinAt,
      item.CheckoutAt
    );
  }

  getStateColor(item: any): Array<string> {
    var state = this.getStateNum(item);

    if (state == 1 || state == 4) {
      return ["bg-success"];
    } else if (state == 2 || state == 5) {
      return ["bg-danger"];
    }

    return [];
  }

  refresh() {
    this.getMaxItem();
    this.getItems();
  }

  getMaxItem() {
    let qry = ss()
      .from("bookings")
      .field("COUNT(*) as cnt");
    let result = execute(qry);
    let first = result[0];

    this.max_item = Number(first.cnt);
  }

  getItems() {
    let qry = ss()
      .from("bookings", "a")
      .field("a.*")
      .limit(this.limit)
      .offset(this.offset);
    let result = execute(qry);

    this.items = [];
    result.forEach((item: any) => this.items.push(item));
  }

  add() {
    this.$router.push({ name: "data.booking.create" });
  }

  detail(item: any) {
    this.selectedId = item.Id;
    this.show_form = true;
  }

  processQuery(query: { [key: string]: string }): void {
    if ("roomId" in query) {
      let roomId = query["roomId"];
      let qry = ss()
        .from("bookings", "a")
        .where("a.roomId = ?", Number(roomId))
        .where("a.CheckoutAt IS NULL")
        .field("a.Id");
      let result = executeFirst(qry);

      if (!isUndefined(result)) {
        this.detail(result);
      }
    }
  }

  mounted() {
    this.$store.commit("changeTitle", "List Booking");
    this.$store.commit("changeSubtitle", "");
    var query: { [key: string]: string } = this.$router.currentRoute.query;
    this.refresh();
    this.processQuery(query);
  }
}
</script>
