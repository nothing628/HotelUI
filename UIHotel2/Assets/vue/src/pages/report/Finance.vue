<template>
  <div class="row">
    <div class="panel panel-inverse">
      <div class="panel-heading">
        <h4 class="panel-title">Transaction Report</h4>
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
        <div style="width: 100%;position: relative;" v-show="isDownload">
          <div ref="kanvas"></div>
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
import echart from "echarts";
import moment, { Moment } from "moment";
import { ss, execute } from "@/lib/Test";
import { Component, Vue, Prop, Watch } from "vue-property-decorator";

enum DownloadType {
  Dialy,
  Monthly,
  Yearly,
  Custom
}

enum KindType {
  In,
  Out
}

interface ITimeRange {
  start: Moment;
  end: Moment;
}

@Component
export default class ReportFinance extends Vue {
  isDownload: boolean = false;
  startDate: string = "";
  endDate: string = "";
  type: DownloadType = DownloadType.Dialy;
  listData: Array<any> = new Array<any>();

  @Watch("isDownload")
  isDownloadChange(newval: boolean, oldval: boolean): void {
    if (!newval) {
      this.$nextTick(() => this.destroyChart());
    }
  }

  get isCustom(): boolean {
    return this.type == DownloadType.Custom;
  }

  get totalIn(): number {
    let result: number = 0;

    this.listData.forEach((item: any) => result += item.AmmountIn);

    return result;
  }

  get totalOut(): number {
    let result: number = 0;

    this.listData.forEach((item: any) => result += item.AmmountOut);

    return result;
  }

  getReport(dateStart: Moment, dateEnd: Moment) {
    let qry = ss()
      .from("transactions")
      .where("TransactionAt >= ?", dateStart.format("YYYY-MM-DD HH:mm:ss"))
      .where("TransactionAt <= ?", dateEnd.format("YYYY-MM-DD HH:mm:ss"));
    let data = execute(qry);

    this.listData = [];
    this.isDownload = true;

    data.forEach((item: any) => {
      this.listData.push(item);
    });

    this.$nextTick(() => this.createChart());
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

  getXAxisData(): Array<string> {
    let result: Array<string> = new Array<string>();
    let range: ITimeRange = this.getTimeRange(this.type);
    let start = range.start.clone();
    let end = range.end;

    switch(this.type) {
      case DownloadType.Dialy:
        result.push(range.start.format("DD/MM/YYYY"));
      break;
      case DownloadType.Monthly:
        while (start.isBefore(end)) {
          result.push(start.format("DD"));
          start.add(1, "day");
        }
      break;
      case DownloadType.Yearly:
        while (start.isBefore(end)) {
          result.push(start.format("MMM YYYY"));
          start.add(1, "month");
        }
      break;
      case DownloadType.Custom:
        while (start.isBefore(end)) {
          result.push(start.format("DD/MM/YYYY"));
          start.add(1, "day");
        }
      break;
    }

    return result;
  }

  sumAmmountIn(items: Array<any>): number {
    if (items.length == 0) return 0;

    let result = 0;

    items.forEach((item: any) => result += item.AmmountIn);

    return result;
  }

  sumAmmountOut(items: Array<any>): number {
    if (items.length == 0) return 0;

    let result = 0;

    items.forEach((item: any) => result += item.AmmountOut);

    return result;
  }

  getYAxisData(kind: KindType): Array<number> {
    let result: Array<number> = new Array<number>();
    let range: ITimeRange = this.getTimeRange(this.type);
    let start = range.start.clone();
    let end = range.end;

    switch (this.type) {
      case DownloadType.Dialy:
        let sumNum = 0;

        if (kind == KindType.In)
          sumNum = this.sumAmmountIn(this.listData);
        else
          sumNum = this.sumAmmountOut(this.listData);

        result.push(sumNum);
      break;
      case DownloadType.Custom:
      case DownloadType.Monthly:
        while (start.isBefore(end)) {
          let sumNum = 0;
          let startTime = start.clone().startOf("date");
          let endTime = start.clone().endOf("date");
          let filterDay = this.listData.filter((item: any) => {
            let itemDate = moment(item.TransactionAt);
            let isAfter = itemDate.isAfter(startTime);
            let isBefore = itemDate.isBefore(endTime);

            return isBefore && isAfter;
          });

          if (kind == KindType.In)
            sumNum = this.sumAmmountIn(filterDay);
          else
            sumNum = this.sumAmmountOut(filterDay);
          
          result.push(sumNum);
          start.add(1, "day");
        }
      break;
      case DownloadType.Yearly:
        while (start.isBefore(end)) {
          let sumNum = 0;
          let startTime = start.clone().startOf("months");
          let endTime = start.clone().endOf("months");
          let filterDay = this.listData.filter((item: any) => {
            let itemDate = moment(item.TransactionAt);
            let isAfter = itemDate.isAfter(startTime);
            let isBefore = itemDate.isBefore(endTime);

            return isBefore && isAfter;
          });

          if (kind == KindType.In)
            sumNum = this.sumAmmountIn(filterDay);
          else
            sumNum = this.sumAmmountOut(filterDay);
          
          result.push(sumNum);
          start.add(1, "months");
        }
      break;
    }

    return result;
  }

  destroyChart() {
    var kanvas = this.$refs.kanvas as HTMLDivElement;
    echart.dispose(kanvas);
  }

  createChart() {
    var kanvas = this.$refs.kanvas as HTMLDivElement;
    $(kanvas).width(kanvas.clientWidth);
    $(kanvas).height(400);
    var chart = echart.init(kanvas);

    chart.setOption({
      color: ['#00acac', '#ff5b57'],
      title: {
        text: 'Transaction Chart'
      },
      tooltip: {},
      legend: {
        data:['In', 'Out']
      },
      xAxis: {
        data: this.getXAxisData()
      },
      yAxis: {},
      series: [{
        name: 'In',
        type: 'bar',
        data: this.getYAxisData(KindType.In),
        barGap: '0%'
      }, {
        name: 'Out',
        type: 'bar',
        data: this.getYAxisData(KindType.Out),
        barGap: '0%'
      }]
    });
  }

  setDay(num: number) {
    this.type = num;
  }

  exportReport() {
    //
  }

  downloadReport() {
    let range = this.getTimeRange(this.type);

    this.getReport(range.start, range.end);
  }

  mounted() {
    this.$store.commit("changeTitle", "Transaction Report");
    this.$store.commit("changeSubtitle", "");
  }
}
</script>
