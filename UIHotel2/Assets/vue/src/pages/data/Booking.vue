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
              <tr v-for="item in items" :key="item.Id">
                  <td>{{ item.Id }}</td>
                  <td>{{ item.BookingAt | date_time_filt }}</td>
                  <td>{{ item.CheckinAt | date_time_filt }}</td>
                  <td>{{ item.ArrivalDate | date_filt }}</td>
                  <td>{{ item.DepartureDate | date_filt }}</td>
                  <td>{{ item | status_filt }}</td>
                  <td>
                    <div class="btn-group pull-right">
                      <button class="btn btn-sm btn-success" @click="editData(item)"><i class="fa fa-sign-in"></i> Check In</button>
                      <button class="btn btn-sm btn-warning" @click="editData(item)"><i class="fa fa-trash"></i> Cancel Booking</button>
                      <button class="btn btn-sm btn-danger" @click="deleteData(item)"><i class="fa fa-sign-out"></i> Check Out</button>
                    </div>
                  </td>
              </tr>
            </tbody>
          </table>
          <counter :from.sync="from" :to.sync="to" :total.sync="max_item"></counter>
          <pagination :total-page.sync="totalPage" v-model="currentPage"></pagination>
        </div>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import { squel, ss, su, sd, si, execute, executeScalar } from "@/lib/Test";
import Pagination from "@/components/Table/Pagination.vue";
import Counter from "@/components/Table/Counter.vue";
import moment from "moment";
import { isNull, isNullOrUndefined } from 'util';

@Component({
  components: {
    Pagination,
    Counter
  },
  filters: {
    date_time_filt(val: any): string {
      let momentf = moment(val);

      if (!momentf.isValid()) {
        return "-";
      }

      return momentf.format("DD/MM/YYYY HH:mm")
    },
    date_filt(val: any): string {
      let momentf = moment(val);

      if (!momentf.isValid()) {
        return "-";
      }

      return momentf.format("DD/MM/YYYY");
    },
    status_filt(val: any): string {
      if (isNullOrUndefined(val.CheckinAt)) {
        return "Booking";
      }
      console.log(val);
      return "";
    }
  }
})
export default class DataBooking extends Vue {
  private max_item: number = 0;
  private currentPage: number = 1;
  private limit: number = 10;
  private items: Array<any> = new Array();

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

  mounted() {
    this.$store.commit("changeTitle", "List Booking");
    this.$store.commit("changeSubtitle", "");
    this.getMaxItem();
    this.getItems();
  }
}
</script>
