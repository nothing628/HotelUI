<template>
  <div class="sidebar sidebar-grid">
    <!-- begin sidebar scrollbar -->
    <div data-scrollbar="true" data-height="100%">
      <!-- begin sidebar user -->
      <ul class="nav">
        <li class="nav-profile">
          <div class="image">
            <a href="javascript:;"><img src="assets/img/user-13.jpg" alt="" /></a>
          </div>
          <div class="info">
            {{ fullname }}
            <small>{{ levelname }}</small>
          </div>
        </li>
      </ul>
      <!-- end sidebar user -->
      <!-- begin sidebar nav -->
      <ul class="nav">
        <li class="nav-header">Navigation</li>
        <menu-child text="Dashboard" route="dashboard" icon="fa fa-laptop"></menu-child>
        <menu-parent
          text="Room"
          icon="fa fa-codepen">
          <menu-child text="Room List" route="room.list"></menu-child>
          <menu-child text="Room Category" route="room.category"></menu-child>
          <menu-child text="Room Price" route="room.price"></menu-child>
          <menu-child text="Calendar Price" route="room.calendar"></menu-child>
        </menu-parent>
        <menu-parent
          text="Data Maintain"
          icon="fa fa-book">
          <menu-child text="Booking" route="data.booking"></menu-child>
          <menu-child text="Checkin/Checkout" route="data.checkin"></menu-child>
          <menu-child text="Guest" route="data.guest"></menu-child>
        </menu-parent>
        <menu-parent
          text="Transaction"
          icon="fa fa-diamond">
          <menu-child text="New Transaction" route="transaction.list"></menu-child>
          <menu-child text="Transaction Category" route="transaction.category"></menu-child>
        </menu-parent>
        <menu-parent
          text="Report"
          icon="fa fa-line-chart">
          <menu-child text="Finance Report" route="report.finance"></menu-child>
          <menu-child text="Hotel Report" route="report.hotel"></menu-child>
        </menu-parent>
        <menu-parent
          text="Settings"
          icon="fa fa-wrench">
          <menu-child text="Application" route="setting.app"></menu-child>
          <menu-child text="Database" route="setting.db"></menu-child>
          <menu-child text="Users" route="setting.user"></menu-child>
        </menu-parent>
        <!-- begin sidebar minify button -->
        <li><a href="javascript:;" class="sidebar-minify-btn" data-click="sidebar-minify"><i class="fa fa-angle-double-left"></i></a></li>
        <!-- end sidebar minify button -->
      </ul>
      <!-- end sidebar nav -->
    </div>
    <!-- end sidebar scrollbar -->
  </div>
</template>
<script>
import { Component, Prop, Vue } from "vue-property-decorator";
import MenuParent from "./Menu/MenuParent.vue";
import MenuChild from "./Menu/MenuChild.vue";

@Component({
  components: {
    MenuParent,
    MenuChild
  }
})
export default class Sidebar extends Vue {
  get fullname() {
    return this.$store.state.User.fullname;
  }

  get levelname() {
    return this.$store.getters["User/levelname"];
  }
}
</script>
