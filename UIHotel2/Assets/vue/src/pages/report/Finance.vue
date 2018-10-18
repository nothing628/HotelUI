<template>
  <div class="row">
    <div class="panel panel-inverse">
      <div class="panel-heading">
        <h4 class="panel-title">Transaction Report</h4>
      </div>
      <div class="panel-body">
        <h1>Transaction Report</h1>
        <div id="canvas" style="width: 800px; height: 400px;"></div>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import echart from "echarts";
import { Component, Vue, Prop } from "vue-property-decorator";

@Component
export default class ReportFinance extends Vue {
  createChart() {
    var elem = document.getElementById("canvas") as HTMLDivElement;
    var chart = echart.init(elem);

    chart.setOption({
      title: {
        text: 'ECharts entry example'
      },
      tooltip: {},
      legend: {
        data:['Sales']
      },
      xAxis: {
        data: ["shirt","cardign","chiffon shirt","pants","heels","socks"]
      },
      yAxis: {},
      series: [{
        name: 'Sales',
        type: 'bar',
        data: [5, 20, 36, 10, 10, 20]
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
