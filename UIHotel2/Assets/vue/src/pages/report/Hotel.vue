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
                  <th rowspan="1" colspan="1">Date</th>
                  <th rowspan="1" colspan="1">Description</th>
                  <th rowspan="1" colspan="1">In</th>
                  <th rowspan="1" colspan="1">Out</th>
                </tr>
            </thead>
            <tbody>
              <tr v-for="item in listData" :key="item.Id">
                <td>{{ item.TransactionAt | strdate("DD/MM/YYYY HH:mm") }}</td>
                <td>{{ item.Description }}</td>
                <td>{{ item.AmmountIn | strcurrency }}</td>
                <td>{{ item.AmmountOut | strcurrency }}</td>
              </tr>
            </tbody>
            <tfoot>
              <tr>
                <td colspan="2"><strong>Total</strong></td>
                <td>{{ totalIn | strcurrency }}</td>
                <td>{{ totalOut | strcurrency }}</td>
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
import { ss, execute } from "@/lib/Test";
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

  get isCustom(): boolean {
    return this.type == DownloadType.Custom;
  }

  getTimeRange(type: DownloadType): ITimeRange {
    var timeRange: ITimeRange = {
      start: moment().startOf("d"),
      end: moment().endOf("d")
    };

    switch(type) {
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
    //
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
