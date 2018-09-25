<template>
  <div class="row">
    <div class="panel panel-inverse">
      <div class="panel-heading">
        <h4 class="panel-title">Calendar Price</h4>
      </div>
      <div class="panel-body">
        <div class="vertical-box">
            <div class="vertical-box-column p-15 bg-silver width-200">
              <div id="external-events" class="fc-event-list">
                <h5 class="m-t-0 m-b-10">Day Type</h5>
                <div v-for="item in priceKind" 
                class="fc-event" 
                :key="item.Id"
                :data-id="item.Id"
                :data-title="item.KindName"
                :data-color="item.KindColor">
                  <div class="fc-event-icon">
                    <i class="fa fa-circle-o fa-fw" :style="getStyle(item.KindColor)"></i>
                  </div> {{ item.KindName }}
                </div>
              </div>
              <button class="m-t-15 btn btn-success btn-block" @click="storeData">
                <i class="fa fa-floppy-o"></i>
                Save Changes
              </button>
          </div>
          <div id="calendar" class="vertical-box-column p-15 calendar"></div>
        </div>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import {
  squel,
  ss,
  execute,
  executeScalar,
  calendarGet,
  calendarSet
} from "@/lib/Test";
import moment from "moment";
import $ from "jquery";
import "jqueryui";
import "fullcalendar";
import { EventObjectInput } from "fullcalendar";

@Component
export default class RoomCalendar extends Vue {
  private priceKind: Array<any> = new Array<any>();

  getPriceKind() {
    let qry = ss().from("roompricekinds");
    let result = execute(qry);

    this.priceKind = [];
    result.forEach((item: any) => this.priceKind.push(item));

    this.$nextTick(() => {
      this.initCalendar();
      this.initExternalEvent();
    });
  }

  getStyle(color: string) {
    let style = {
      color: "#" + color
    };

    return style;
  }

  getKindId(kind: string): number {
    var result = this.priceKind.filter((item: any) => {
      return item.KindName == kind;
    });

    if (result.length == 0) return -1;

    return result[0].Id;
  }

  getKindName(id: number): string {
    var result = this.priceKind.filter((item: any) => {
      return item.Id == id;
    });

    if (result.length == 0) return "";

    return result[0].KindName;
  }

  getKindColor(id: number): string {
    var result = this.priceKind.filter((item: any) => {
      return item.Id == id;
    });

    if (result.length == 0) return "";

    return result[0].KindColor;
  }

  getData() {
    calendarGet(2018, this.asyncGetData);
  }

  asyncGetData(items: any) {
    let jquery_o = $("#calendar");

    items.list.forEach((item: any) => {
      jquery_o.fullCalendar("renderEvent", item, true);
    });
  }

  asyncStoreData() {
    return new Promise((resolve, reject) => {
      let jquery_o = $("#calendar");
      let calendar = jquery_o.fullCalendar("clientEvents", null);
      let insert_element: Array<any> = [];

      calendar.forEach((item: any) => {
        let title = item.title;
        let kind_id = this.getKindId(title);
        let start = item.start.format("YYYY-MM-DD");
        let end = item.start.format("YYYY-MM-DD");

        if (item.end != null) {
          end = item.end
            .clone()
            .add(-1, "d")
            .format("YYYY-MM-DD");
        }

        let batch = this.createBatchDate(start, end, kind_id);
        insert_element = insert_element.concat(batch);
      });

      calendarSet(insert_element);
    });
  }

  storeData() {
    this.asyncStoreData();
  }

  createBatchDate(start: string, end: string, kind: number): Array<any> {
    let start_date = moment(start);
    let end_date = moment(end);
    let result = [];

    do {
      result.push({
        DateAt: start_date.format("YYYY-MM-DD"),
        RoomPriceKindId: kind
      });
      start_date.add(1, "d");
    } while (end_date.isSameOrAfter(start_date));

    return result;
  }

  overlapSolve(event: any, test: any): Array<any> {
    let event_start: moment.Moment = moment(event.start);
    let event_end: moment.Moment = moment(event.end);
    let test_start: moment.Moment = moment(test.start);
    let test_end: moment.Moment = moment(test.end);
    let result: Array<any> = [];
    let diff_test = test_start.diff(test_end, "d");
    let diff_event = event_start.diff(event_end, "d");

    if (Math.abs(diff_test) > 0) {
      test_end.add(-1, "d");
    }

    if (Math.abs(diff_event) > 0) {
      event_end.add(-1, "d");
    }

    if (
      test_start.isSameOrAfter(event_start) &&
      test_end.isSameOrBefore(event_end)
    ) {
      // overlap will remove
    } else if (test_end.isBetween(event_start, event_end, undefined, "[]")) {
      //cutoff the end
      let new_end: moment.Moment = event_start.clone();
      result.push({
        title: test.title,
        start: test_start,
        end: new_end,
        color: test.color,
        allDay: true
      });
    } else if (test_start.isBetween(event_start, event_end, undefined, "[]")) {
      //cutoff the start
      let new_start: moment.Moment = event_end.clone().add(1, "d");
      let new_end: moment.Moment = test_end.clone().add(1, "d");
      result.push({
        title: test.title,
        start: new_start,
        end: new_end,
        color: test.color,
        allDay: true
      });
    } else if (
      event_start.isSameOrAfter(test_start) &&
      event_end.isSameOrBefore(test_end)
    ) {
      //overlap will split
      let new_end: moment.Moment = event_start.clone();
      let new_end1: moment.Moment = test_end.clone().add(1, "d");
      let new_start: moment.Moment = event_end.clone().add(1, "d");

      result.push({
        title: test.title,
        start: test_start,
        end: new_end,
        color: test.color,
        allDay: true
      });
      result.push({
        title: test.title,
        start: new_start,
        end: new_end1,
        color: test.color,
        allDay: true
      });
    }

    return result;
  }

  isOverlap(
    event_start: string,
    event_end: string,
    event_test_start: string,
    event_test_end: string
  ): boolean {
    let mevent_start: moment.Moment = moment(event_start);
    let mevent_end: moment.Moment = moment(event_end);
    let mtest_start: moment.Moment = moment(event_test_start);
    let mtest_end: moment.Moment = moment(event_test_end);
    let result: boolean = false;
    let diff_event = mevent_start.diff(mevent_end, "d");
    let diff_test = mtest_start.diff(mtest_end, "d");

    if (Math.abs(diff_event) > 0) {
      mevent_end.add(-1, "d");
    }

    if (Math.abs(diff_test) > 0) {
      mtest_end.add(-1, "d");
    }

    if (mtest_start.isBetween(mevent_start, mevent_end, undefined, "[]")) {
      return true;
    } else if (mtest_end.isBetween(mevent_start, mevent_end, undefined, "[]")) {
      return true;
    } else {
      result =
        mevent_start.isBetween(mtest_start, mtest_end, undefined, "[]") &&
        mevent_end.isBetween(mtest_start, mtest_end, undefined, "[]");
    }

    return result;
  }

  arrangeEvent(event?: EventObjectInput) {
    let jquery_o = $("#calendar");
    let calendar = jquery_o.fullCalendar("clientEvents", null);
    let event_id = event!._id;
    let event_time_start: any = event!.start;
    let event_time_end: any = event!.end;
    let event_start = event_time_start.format("YYYY-MM-DD");
    let event_end = event_start;

    if (event!.end != null) {
      event_end = event_time_end.format("YYYY-MM-DD");
    }

    let filtered_remove = calendar.filter((item: any) => {
      let test_start = item.start.format("YYYY-MM-DD");
      let test_end = test_start;

      if (item.end != null) {
        test_end = item.end.format("YYYY-MM-DD");
      }

      if (item._id == event_id) return false;

      return this.isOverlap(event_start, event_end, test_start, test_end);
    });

    filtered_remove.forEach((item: any) => {
      let test_start = item.start.format("YYYY-MM-DD");
      let test_item: EventObjectInput = {
        title: item.title,
        start: test_start,
        end: test_start,
        color: item.color,
        allDay: true
      };
      let event_item: EventObjectInput = {
        title: event!.title,
        start: event_start,
        end: event_end,
        color: event!.color,
        allDay: true
      };

      if (item.end != null) {
        test_item.end = item.end.format("YYYY-MM-DD");
      }

      let solveEvent = this.overlapSolve(event_item, test_item);
      jquery_o.fullCalendar("removeEvents", item._id);
      solveEvent.forEach((item: any) => {
        jquery_o.fullCalendar("renderEvent", item, true);
      });
    });
  }

  initExternalEvent() {
    $("#external-events .fc-event").each(function() {
      $(this).data("event", {
        title: $(this).attr("data-title") ? $(this).attr("data-title") : "",
        color: $(this).attr("data-color")
          ? "#" + $(this).attr("data-color")
          : "",
        stick: true,
        allDay: true
      });
      $(this).draggable({
        zIndex: 999,
        revert: true,
        revertDuration: 0
      });
    });
  }

  initCalendar() {
    let self = this;
    $("#calendar").fullCalendar({
      header: {
        left: "",
        center: "title",
        right: "prev,today,next "
      },
      droppable: true,
      drop: function(datex, jsEvent, ui) {},
      selectable: true,
      selectHelper: true,
      select: function(t, e) {},
      editable: true,
      eventLimit: true,
      eventOverlap: true,
      eventClick: function(calEvent, jsEvent, view) {
        console.log(jsEvent);
      },
      eventDrop: function(event) {
        self.arrangeEvent(event);
      },
      eventResize: function(event) {
        self.arrangeEvent(event);
      },
      eventReceive: function(event?: EventObjectInput) {
        self.arrangeEvent(event);
      },
      events: []
    });
  }

  destoryCalendar() {
    $("#calendar").fullCalendar("destroy");
  }

  destoryExternalEvent() {
    $("#external-events .fc-event").each(function() {
      $(this).removeData("event");
      $(this).draggable("destroy");
    });
  }

  mounted() {
    this.$store.commit("changeTitle", "Calendar Price");
    this.$store.commit("changeSubtitle", "");
    this.getPriceKind();

    this.$nextTick(this.getData);
  }

  beforeDestroy() {
    this.destoryExternalEvent();
    this.destoryCalendar();
  }
}
</script>
