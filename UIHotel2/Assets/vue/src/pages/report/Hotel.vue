<template>
  <div class="row">
    <div class="panel panel-inverse">
      <div class="panel-heading">
        <h4 class="panel-title">Booking Report</h4>
      </div>
      <div class="panel-body">
        <div class="form-inline m-b-10">
          <div class="form-group">
            <div class="btn-group m-r-5">
              <button class="btn btn-success" :class="{active: type == 0}" @click="setDay(0)">Today</button>
              <button class="btn btn-success" :class="{active: type == 1}" @click="setDay(1)">This Month</button>
              <button class="btn btn-success" :class="{active: type == 2}" @click="setDay(2)">This Year</button>
              <button class="btn btn-success" :class="{active: type == 3}" @click="setDay(3)">Custom</button>
            </div>
          </div>
          <div class="form-group" v-if="isCustom">
            <label class="m-r-5">From</label>
            <input type="date" class="form-control m-r-5" v-model="startDate"/>
          </div>
          <div class="form-group" v-if="isCustom">
            <label class="m-r-5">To</label>
            <input type="date" class="form-control m-r-5" v-model="endDate"/>
          </div>
          <div class="form-group">
            <div class="btn-group">
              <button class="btn btn-success" @click="downloadReport">
                <i class="fa fa-search"></i>
                <span> Search</span>
              </button>
              <button class="btn btn-success" @click="exportReport">
                <i class="fa fa-download"></i>
                <span> Export</span>
              </button>
            </div>
          </div>
        </div>
        <div class="dataTables_wrapper form-inline dt-bootstrap no-footer" v-if="isDownload">
          <table class="table table-striped table-bordered dataTable no-footer dtr-inline" role="grid">
            <thead>
                <tr role="row">
                  <th>ID</th>
                  <th>Booking</th>
                  <th>Type</th>
                  <th>Arrival</th>
                  <th>Departure</th>
                  <th>Checkin</th>
                  <th>Checkout</th>
                  <th>Room Number</th>
                  <th>Guest ID</th>
                  <th>Guest Name</th>
                  <th>Total Pay</th>
                </tr>
            </thead>
            <tbody>
              <tr v-for="item in listData" :key="item.Id">
                <td>{{ item.Id }}</td>
                <td>{{ item.BookingAt | strdate("DD/MM/YYYY") }}</td>
                <td>{{ item.TypeName }}</td>
                <td>{{ item.ArrivalDate | strdate("DD/MM/YYYY") }}</td>
                <td>{{ item.DepartureDate | strdate("DD/MM/YYYY") }}</td>
                <td>{{ item.CheckinAt | strdate("DD/MM/YYYY") }}</td>
                <td>{{ item.CheckoutAt | strdate("DD/MM/YYYY") }}</td>
                <td>{{ item.RoomNumber }}</td>
                <td>{{ item.IdNumber }}</td>
                <td>{{ item.Fullname }}</td>
                <td>{{ item.AmmountIn | strcurrency }}</td>
              </tr>
            </tbody>
            <tfoot>
              <tr>
                <td colspan="2"><strong>Booking Count</strong></td>
                <td>{{ BookingCount }}</td>
                <td colspan="7"><strong>Total</strong></td>
                <td>{{ TotalPay | strcurrency }}</td>
              </tr>
            </tfoot>
          </table>
        </div>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import { ss, se, execute } from "@/lib/Test";
import moment, { Moment } from "moment";

enum DownloadType {
  Dialy,
  Monthly,
  Yearly,
  Custom
}

interface ITimeRange {
  start: Moment;
  end: Moment;
}

@Component
export default class ReportHotel extends Vue {
  isDownload: boolean = false;
  type: DownloadType = DownloadType.Dialy;
  startDate: string = "";
  endDate: string = "";
  listData: Array<any> = new Array<any>();

  get isCustom(): boolean {
    return this.type == DownloadType.Custom;
  }

  get BookingCount(): number {
    return this.listData.length;
  }

  get TotalPay(): number {
    let result: number = 0;

    this.listData.forEach((item: any) => result += item.AmmountIn);

    return result;
  }

  getTimeRange(type: DownloadType): ITimeRange {
    var timeRange: ITimeRange = {
      start: moment().startOf("d"),
      end: moment().endOf("d")
    };

    switch (type) {
      case DownloadType.Monthly:
        timeRange.start = moment().startOf("M");
        timeRange.end = moment().endOf("M");
        break;
      case DownloadType.Yearly:
        timeRange.start = moment().startOf("y");
        timeRange.end = moment().endOf("y");
        break;
      case DownloadType.Custom:
        timeRange.start = moment(this.startDate).startOf("d");
        timeRange.end = moment(this.endDate).endOf("d");
        break;
    }

    return timeRange;
  }

  getReport(dateStart: Moment, dateEnd: Moment) {
    let qry = ss()
      .from("bookings", "a")
      .join("bookingtypes", "b", "a.TypeId = b.Id")
      .join("rooms", "c", "a.RoomId = c.Id")
      .join("guests", "d", "a.GuestId = d.Id")
      .join("invoicedetails", "e", "a.Id =  e.InvoiceId")
      .where(
        se()
          .and("e.KindId = ?", 100)
          .or("e.KindId = ?", 101)
      )
      .field("a.Id")
      .field("a.BookingAt")
      .field("a.ArrivalDate")
      .field("a.DepartureDate")
      .field("a.CheckinAt")
      .field("a.CheckoutAt")
      .field("b.TypeName")
      .field("c.RoomNumber")
      .field("d.IdNumber")
      .field("d.Fullname")
      .field("e.AmmountIn")
      .where("a.CheckoutAt IS NOT NULL");
    let result = execute(qry);

    this.listData = [];
    this.isDownload = true;
    result.forEach((item: any) => this.listData.push(item));
  }

  exportReport() {
    let range = this.getTimeRange(this.type);

    window.CS.Hotel.BookingReportDownload(range.start.toDate(), range.end.toDate());
  }

  downloadReport() {
    let range = this.getTimeRange(this.type);

    this.getReport(range.start, range.end);
  }

  setDay(num: number) {
    this.type = num;
  }

  mounted() {
    this.$store.commit("changeTitle", "Booking Report");
    this.$store.commit("changeSubtitle", "");
  }
}
</script>
