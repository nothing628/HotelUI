<template>
  <div class="row">
    <div class="panel panel-inverse">
      <div class="panel-heading">
        <h4 class="panel-title">Calendar Price</h4>
      </div>
      <div class="panel-body">
        <h1></h1>
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
      }),
      $(this).draggable({
        zIndex: 999,
        revert: !0,
        revertDuration: 0
      })
    });
  }

  initCalendar() {
    var t = new Date
    var e = t.getFullYear()
    var a = t.getMonth() + 1;
    a = 10 > a ? parseInt("0" + a) : a;
    $("#calendar").fullCalendar({
      header: {
        left: "month,agendaWeek,agendaDay",
        center: "title",
        right: "prev,today,next "
      },
      droppable: !0,
      drop: function() {
        $(this).remove()
      },
      selectable: !0,
      selectHelper: !0,
      select: function(t, e) {
        var a, r = prompt("Event Title:");
        r && (a = {
          title: r,
          start: t,
          end: e
        },
        $("#calendar").fullCalendar("renderEvent", a, !0)),
        $("#calendar").fullCalendar("unselect")
      },
      editable: !0,
      eventLimit: !0,
      events: [{
        title: "All Day Event",
        start: e + "-" + a + "-01",
        color: "#00acac"
      }, {
        title: "Long Event",
        start: e + "-" + a + "-07",
        end: e + "-" + a + "-10"
      }]
    })
  }

  mounted() {
    this.$store.commit("changeTitle", "Calendar Price");
    this.$store.commit("changeSubtitle", "");
  }
}
</script>
