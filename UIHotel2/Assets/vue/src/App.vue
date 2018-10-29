<template>
  <div id="page-container" class="fade page-sidebar-fixed page-header-fixed">
    <v-navbar></v-navbar>
    <v-notification></v-notification>
    <v-sidebar v-if="is_login"></v-sidebar>
		<div class="sidebar-bg" v-if="is_login"></div>
    <v-content v-if="is_login || is_setup" :is-setup="is_setup"></v-content>
    <v-login v-if="!is_login && !is_setup"></v-login>
    <v-theme-panel></v-theme-panel>
		
		<!-- begin scroll to top btn -->
		<a class="btn btn-icon btn-circle btn-success btn-scroll-to-top fade" data-click="scroll-top">
      <i class="fa fa-angle-up"></i>
    </a>
		<!-- end scroll to top btn -->
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import VNavbar from "@/components/Header.vue";
import VSidebar from "@/components/Sidebar.vue";
import VThemePanel from "@/components/ThemePanel.vue";
import VContent from "@/components/Content.vue";
import VLogin from "@/components/Auth/Login.vue";
import VNotification from "@/components/Layout/Notification.vue";

@Component({
  components: {
    VLogin,
    VNavbar,
    VThemePanel,
    VSidebar,
    VContent,
    VNotification
  }
})
export default class App extends Vue {
  @Prop() private msg!: string;
  @Prop() private dataSrc!: string;

  is_setup: boolean = false;

  get is_login() {
    return this.$store.getters["User/is_login"];
  }

  created() {
    this.$store.dispatch("Setting/CopySetting");
  }

  mounted() {
    let current = this.$router.currentRoute.meta;

    if ('is_setup' in current && current.is_setup)
      this.is_setup = true;
  }
}
</script>
