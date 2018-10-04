<template>
  <div class="bootstrap-timepicker-widget dropdown-menu timepicker-orient-left timepicker-orient-top open">
    <table>
      <tbody>
        <tr>
          <td>
            <a href="javascript:;" @click="hourUp">
              <span class="glyphicon glyphicon-chevron-up"></span>
            </a>
          </td>
          <td class="separator">&nbsp;</td>
          <td>
            <a href="javascript:;" @click="minuteUp">
              <span class="glyphicon glyphicon-chevron-up"></span>
            </a>
          </td>
          <td class="separator">&nbsp;</td>
          <td class="meridian-column">
            <a href="javascript:;" @click="meridianToggle">
              <span class="glyphicon glyphicon-chevron-up"></span>
            </a>
          </td>
        </tr>
        <tr>
          <td>
            <input type="text" class="bootstrap-timepicker-hour form-control" readonly v-model="hourFormat">
          </td>
          <td class="separator">:</td>
          <td>
            <input type="text" class="bootstrap-timepicker-minute form-control" readonly v-model="minuteFormat"></td>
          <td class="separator">&nbsp;</td>
          <td>
            <input type="text" class="bootstrap-timepicker-meridian form-control" readonly v-model="meridian">
          </td>
        </tr>
        <tr>
          <td>
            <a href="javascript:;" @click="hourDown">
              <span class="glyphicon glyphicon-chevron-down"></span>
            </a>
          </td>
          <td class="separator"></td>
          <td>
            <a href="javascript:;" @click="minuteDown">
              <span class="glyphicon glyphicon-chevron-down"></span>
            </a>
          </td>
          <td class="separator">&nbsp;</td>
          <td>
            <a href="javascript:;" @click="meridianToggle">
              <span class="glyphicon glyphicon-chevron-down"></span>
            </a>
          </td>
        </tr>
        <tr>
          <td>
            <a class="btn btn-success" @click="close">
              <i class="fa fa-check"></i>
            </a>
          </td>
          <td class="separator">&nbsp;</td>
          <td></td>
          <td class="separator">&nbsp;</td>
          <td></td>
        </tr>
      </tbody>
    </table>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop, Watch } from "vue-property-decorator";
import moment from "moment";

@Component
export default class TimePickerFloat extends Vue {
  private hour: number = 12;
  private minute: number = 0;
  private meridian: string = "AM";

  @Prop({ type: String, required: false })
  private value?: string;

  get convValue(): string {
    let fullStr = this.hourFormat + ":" + this.minuteFormat + " " + this.meridian;
    let fullMoment = moment(fullStr, "hh:mm A");

    return fullMoment.format("HH:mm:ss");
  }

  get hourFormat(): string {
    let hour_str = this.hour.toString();

    if (hour_str.length == 1) {
      return "0" + hour_str;
    }

    return hour_str;
  }

  get minuteFormat(): string {
    let minute_str = this.minute.toString();

    if (minute_str.length == 1) {
      return "0" + minute_str;
    }

    return minute_str;
  }

  close() {
    this.$emit("close");
  }

  parseValue() {
    let parse = moment(this.value, "HH:mm:ss");
    let hour = parse.hour();
    let minute = parse.minute();

    this.meridian = hour >= 12 ? "PM" : "AM";
    this.hour = hour > 12 ? hour - 12 : hour;
    this.minute = minute;

    if (hour == 0) {
      this.hour = 12;
    }
  }

  @Watch("value")
  changeValue() {
    this.parseValue();
  }

  @Watch("meridian")
  changeMeridian() {
    this.$emit("input", this.convValue);
  }

  @Watch("minute")
  changeMinute() {
    this.$emit("input", this.convValue);
  }

  @Watch("hour")
  changeHour() {
    this.$emit("input", this.convValue);
  }

  hourUp() {
    this.hour++;

    if (this.hour > 12) {
      this.hour = 1;
    }
  }

  hourDown() {
    this.hour--;

    if (this.hour < 1) {
      this.hour = 12;
    }
  }

  minuteDown() {
    this.minute--;

    if (this.minute < 0) {
      this.minute = 59;
      this.hourDown();
    }
  }

  minuteUp() {
    this.minute++;

    if (this.minute > 59) {
      this.minute = 0;
      this.hourUp();
    }
  }

  meridianToggle() {
    this.meridian = this.meridian == "AM" ? "PM" : "AM";
  }

  mounted() {
    this.parseValue();
  }
}
</script>
