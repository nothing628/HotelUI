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
import { squel, se, ss, su, sd, si, execute, executeScalar } from "@/lib/Test";
import moment from "moment";
import $ from "jquery";
import "jqueryui";
import "fullcalendar";

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
      "color": "#" + color
    };

    return style;
  }

  storeData() {
    //
  }

  initExternalEvent() {
    $("#external-events .fc-event").each(function() {
      $(this).data("event", {
        title: $(this).attr("data-title") ? $(this).attr("data-title") : "",
        color: $(this).attr("data-color") ? "#" + $(this).attr("data-color") : "",
        allDay: true
      });
      $(this).draggable({
        zIndex: 999,
        revert: !0,
        revertDuration: 0
      });
    });
  }

  initCalendar() {
    $("#calendar").fullCalendar({
      header: {
        left: "",
        center: "title",
        right: "prev,today,next "
      },
      droppable: true,
      drop: function(datex, jsEvent, ui) {
        //$(this).remove();
      },
      selectable: true,
      selectHelper: true,
      select: function(t, e) {
        // var a,
        //   r = prompt("Event Title:");
        // r &&
        //   ((a = {
        //     title: r,
        //     start: t,
        //     end: e
        //   }),
        //   $("#calendar").fullCalendar("renderEvent", a, true)),
        //   $("#calendar").fullCalendar("unselect");
      },
      editable: true,
      eventLimit: true,
      eventOverlap: function(event_no_moving, event_moving) {
        console.log(event_no_moving, event_moving);
        return true;
      },
      events: [
        // {
        //   title: "All Day Event",
        //   start: e + "-" + a + "-01",
        //   color: "#00acac"
        // }
      ]
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
  }

  beforeDestroy() {
    this.destoryExternalEvent();
    this.destoryCalendar();
  }
}
</script>
