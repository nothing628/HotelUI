<template>
  <div id="header" class="header navbar navbar-default navbar-fixed-top navbar-inverse">
  <div class="container-fluid">
    <div class="navbar-header">
      <a href="index.html" class="navbar-brand"><span class="navbar-logo"></span> {{ app_name }}</a>
      <button type="button" class="navbar-toggle" data-click="sidebar-toggled">
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
      </button>
    </div>

    <ul class="nav navbar-nav navbar-right">
      <li v-if="is_login" class="dropdown navbar-user">
        <a href="javascript:;" class="dropdown-toggle" data-toggle="dropdown">
          <img src="assets/img/user-13.jpg" alt="" /> 
          <span class="hidden-xs">{{ fullname }}</span> <b class="caret"></b>
        </a>
        <ul class="dropdown-menu animated fadeInLeft">
          <li class="arrow"></li>
          <li><a href="javascript:;">Change Password</a></li>
          <li class="divider"></li>
          <li><a @click="logout">Log Out</a></li>
        </ul>
      </li>
      <li onclick="windowMinimize();">
        <a class="f-s-14">
          <i class="fa fa-window-minimize"></i>
        </a>
      </li>
      <li onclick="windowMaximize();">
        <a class="f-s-14">
          <i class="fa fa-window-maximize"></i>
        </a>
      </li>
      <li onclick="windowClose();">
        <a class="f-s-14">
          <i class="fa fa-close"></i>
        </a>
      </li>
    </ul>
  </div>
  </div>
</template>
<style>
.navbar-brand {
  width: 300px;
}
</style>
<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";

@Component
export default class Header extends Vue {
  get is_login() {
    return this.$store.getters["User/is_login"];
  }

  get fullname() {
    return this.$store.state.User.fullname;
  }

  get app_name() {
    return this.$store.state.app_title;
  }

  logout() {
    this.$store.commit("user/logout");
  }
}
</script>
