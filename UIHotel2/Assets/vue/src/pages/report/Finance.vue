<template>
  <div class="row">
    <div class="panel panel-inverse">
      <div class="panel-heading">
        <h4 class="panel-title">Transaction Report</h4>
      </div>
      <div class="panel-body">
        <div class="form-inline m-b-10">
          <div class="form-group">
            <label class="m-r-5">From</label>
            <input type="date" class="form-control m-r-5"/>
          </div>
          <div class="form-group">
            <label class="m-r-5">To</label>
            <input type="date" class="form-control m-r-5"/>
          </div>
          <div class="form-group">
            <div class="btn-group">
              <a class="btn btn-success " >
                <i class="fa fa-search"></i>
                <span> Search</span>
              </a>
              <a class="btn btn-success " >
                <i class="fa fa-download"></i>
                <span> Export</span>
              </a>
            </div>
          </div>
        </div>
        <div id="canvas" style="width: 100%; height: 400px;"></div>
        <div class="dataTables_wrapper form-inline dt-bootstrap no-footer">
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
import { Component, Vue, Prop } from "vue-property-decorator";

@Component
export default class ReportFinance extends Vue {
  getDate() {
    let arr = [];

    for (let i = 1; i < 31; i++) {
      arr.push(i);
    }

    return arr;
  }

  getRandom() {
    let arr = [];

    for (let i = 1; i < 31; i++) {
      let rand = Math.random();
      let num = Math.ceil(rand * 100000) + 20000;
      arr.push(num);
    }

    return arr;
  }

  createChart() {
    var elem = document.getElementById("canvas") as HTMLDivElement;
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
        data: this.getDate()
      },
      yAxis: {},
      series: [{
        name: 'In',
        type: 'bar',
        data: this.getRandom(),
        barGap: '0%'
      }, {
        name: 'Out',
        type: 'bar',
        data: this.getRandom(),
        barGap: '0%'
      }]
    });
  }

  mounted() {
    this.$store.commit("changeTitle", "Transaction Report");
    this.$store.commit("changeSubtitle", "");
    this.createChart();
  }
}
</script>
