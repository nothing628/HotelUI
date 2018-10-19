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
              <a class="btn btn-success" :class="{active: type == 0}" @click="setDay(0)">Today</a>
              <a class="btn btn-success" :class="{active: type == 1}" @click="setDay(1)">This Month</a>
              <a class="btn btn-success" :class="{active: type == 2}" @click="setDay(2)">This Year</a>
              <a class="btn btn-success" :class="{active: type == 3}" @click="setDay(3)">Custom</a>
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
              <a class="btn btn-success" @click="downloadReport">
                <i class="fa fa-search"></i>
                <span> Search</span>
              </a>
              <a class="btn btn-success" @click="exportReport">
                <i class="fa fa-download"></i>
                <span> Export</span>
              </a>
            </div>
          </div>
        </div>
        <div style="width: 100%;position: relative;" v-if="isDownload">
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
              <tr>
                <td>09/01</td>
                <td>Description</td>
                <td>Rp100.000</td>
                <td>Rp0</td>
              </tr>
            </tbody>
            <tfoot>
              <tr>
                <td colspan="2">Total</td>
                <td>Rp785.000</td>
                <td>Rp200.000</td>
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
import { Component, Vue, Prop } from "vue-property-decorator";

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

  get isCustom(): boolean {
    return this.type == DownloadType.Custom;
  }

  getReport(dateStart: string, dateEnd: string) {
    //
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
    return [];
  }

  getYAxisData(kind: KindType): Array<number> {
    return [];
  }

  createChart() {
    var kanvas = this.$refs.kanvas as Element;
    $(kanvas).width(kanvas.clientWidth);
    $(kanvas).height(400);
    var elem = kanvas as HTMLDivElement;
    var chart = echart.init(elem);

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
    console.log(range);
  }

  mounted() {
    this.$store.commit("changeTitle", "Transaction Report");
    this.$store.commit("changeSubtitle", "");
    this.createChart();
  }
}
</script>
