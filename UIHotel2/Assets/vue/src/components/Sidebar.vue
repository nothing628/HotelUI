<template>
  <div id="sidebar" class="sidebar">
    <!-- begin sidebar scrollbar -->
    <div data-scrollbar="true" data-height="100%">
      <!-- begin sidebar user -->
      <ul class="nav">
        <li class="nav-profile">
          <div class="image">
            <a href="javascript:;"><img src="assets/img/user-13.jpg" alt="" /></a>
          </div>
          <div class="info">
            Sean Ngu
            <small>Front end developer</small>
          </div>
        </li>
      </ul>
      <!-- end sidebar user -->
      <!-- begin sidebar nav -->
      <ul class="nav">
        <li class="nav-header">Navigation</li>
        <menu-child text="Dashboard" is-active icon="fa fa-laptop"></menu-child>
        <menu-parent
          text="Room"
          icon="fa fa-codepen">
          <menu-child text="Room List"></menu-child>
          <menu-child text="Room Category"></menu-child>
          <menu-child text="Room Price"></menu-child>
          <menu-child text="Calendar Price"></menu-child>
        </menu-parent>
        <menu-parent
          text="Data Maintain"
          icon="fa fa-book">
          <menu-child text="Checkin/Checkout" href="/foo"></menu-child>
          <menu-child text="Booking" href="/"></menu-child>
          <menu-child text="Guest"></menu-child>
        </menu-parent>
        <menu-parent
          text="Transaction"
          icon="fa fa-diamond">
          <menu-child text="New Transaction" href="/foo"></menu-child>
          <menu-child text="Transaction Category" href="/"></menu-child>
        </menu-parent>
        <menu-parent
          text="Report"
          icon="fa fa-line-chart">
          <menu-child text="Finance Report" href="/foo"></menu-child>
          <menu-child text="Hotel Report" href="/"></menu-child>
        </menu-parent>
        <menu-parent
          text="Settings"
          icon="fa fa-wrench">
          <menu-child text="Application" href="/foo"></menu-child>
          <menu-child text="Database" href="/"></menu-child>
          <menu-child text="Users"></menu-child>
        </menu-parent>
        <menu-child icon="fa fa-laptop" text="Calendar"></menu-child>
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
import MenuParent from './Menu/MenuParent.vue'
import MenuChild from './Menu/MenuChild.vue'

@Component({
  components: {
    MenuParent,
    MenuChild
  }
})
export default class Sidebar extends Vue {
  
};
</script>
