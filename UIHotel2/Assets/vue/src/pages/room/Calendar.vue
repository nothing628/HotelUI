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
                <h5 class="m-t-0 m-b-10">Draggable Events</h5>
                <div class="fc-event" data-color="#00acac"><div class="fc-event-icon"><i class="fa fa-circle-o fa-fw text-success"></i></div> Meeting with Client</div>
                <div class="fc-event" data-color="#348fe2"><div class="fc-event-icon"><i class="fa fa-circle-o fa-fw text-primary"></i></div> IOS App Development</div>
                <div class="fc-event" data-color="#f59c1a"><div class="fc-event-icon"><i class="fa fa-circle-o fa-fw text-warning"></i></div> Group Discussion</div>
                <div class="fc-event" data-color="#ff5b57"><div class="fc-event-icon"><i class="fa fa-circle-o fa-fw text-danger"></i></div> New System Briefing</div>
                <div class="fc-event"><div class="fc-event-icon"><i class="fa fa-circle-o fa-fw text-inverse"></i></div> Brainstorming</div>
                <h5 class="m-t-20 m-b-10">Other Events</h5>
                <div class="fc-event" data-color="#b6c2c9"><div class="fc-event-icon"><i class="fa fa-circle-o fa-fw text-muted"></i></div> Other Event 1</div>
                <div class="fc-event" data-color="#b6c2c9"><div class="fc-event-icon"><i class="fa fa-circle-o fa-fw text-muted"></i></div> Other Event 2</div>
                <div class="fc-event" data-color="#b6c2c9"><div class="fc-event-icon"><i class="fa fa-circle-o fa-fw text-muted"></i></div> Other Event 3</div>
                <div class="fc-event" data-color="#b6c2c9"><div class="fc-event-icon"><i class="fa fa-circle-o fa-fw text-muted"></i></div> Other Event 4</div>
                <div class="fc-event" data-color="#b6c2c9"><div class="fc-event-icon"><i class="fa fa-circle-o fa-fw text-muted"></i></div> Other Event 5</div>
              </div>
          </div>
          <div id="calendar" class="vertical-box-column p-15 calendar"></div>
        </div>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import $ from "jquery";
import "jqueryui";
import "fullcalendar";

@Component
export default class RoomCalendar extends Vue {
  initExternalEvent() {
    $("#external-events .fc-event").each(function() {
      $(this).data("event", {
        title: $.trim($(this).text()),
        stick: !0,
        color: $(this).attr("data-color") ? $(this).attr("data-color") : ""
      });
      $(this).draggable({
        zIndex: 999,
        revert: !0,
        revertDuration: 0
      });
    });
  }

  initCalendar() {
    var t = new Date();
    var e = t.getFullYear();
    var a = t.getMonth() + 1;
    a = 10 > a ? parseInt("0" + a) : a;
    $("#calendar").fullCalendar({
      header: {
        left: "month,agendaWeek,agendaDay",
        center: "title",
        right: "prev,today,next "
      },
      droppable: true,
      drop: function() {
        //$(this).remove();
      },
      selectable: true,
      selectHelper: true,
      select: function(t, e) {
        var a,
          r = prompt("Event Title:");
        r &&
          ((a = {
            title: r,
            start: t,
            end: e
          }),
          $("#calendar").fullCalendar("renderEvent", a, true)),
          $("#calendar").fullCalendar("unselect");
      },
      editable: true,
      eventLimit: true,
      events: [
        {
          title: "All Day Event",
          start: e + "-" + a + "-01",
          color: "#00acac"
        }
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
    this.initCalendar();
    this.initExternalEvent();
  }

  beforeDestroy() {
    this.destoryExternalEvent();
    this.destoryCalendar();
  }
}
</script>
