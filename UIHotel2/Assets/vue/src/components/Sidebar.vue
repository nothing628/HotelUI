<template>
  <div class="sidebar sidebar-grid">
    <!-- begin sidebar scrollbar -->
    <div data-scrollbar="true" data-height="100%">
      <!-- begin sidebar user -->
      <ul class="nav">
        <li class="nav-profile">
          <div class="image">
            <a href="javascript:;">
              <i class="fa fa-user-circle-o fa-3x" :class="class_image"></i>
            </a>
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
        <menu-select v-for="(item, i) in listmenu" :key="i" :menu_data="item" :level="level"></menu-select>
        <!-- begin sidebar minify button -->
        <li><a href="javascript:;" class="sidebar-minify-btn" data-click="sidebar-minify"><i class="fa fa-angle-double-left"></i></a></li>
        <!-- end sidebar minify button -->
      </ul>
      <!-- end sidebar nav -->
    </div>
    <!-- end sidebar scrollbar -->
  </div>
</template>
<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import MenuParent from "./Menu/MenuParent.vue";
import MenuChild from "./Menu/MenuChild.vue";
import MenuSelect from "@/components/Menu/MenuSelect.vue";

@Component({
  components: {
    MenuParent,
    MenuChild,
    MenuSelect
  }
})
export default class Sidebar extends Vue {
  get level() {
    return this.$store.state.User.Level;
  }

  get fullname() {
    return this.$store.state.User.Fullname;
  }

  get class_image() {
    let result = new Array<string>();

    switch (this.level) {
      case 0: result.push("text-danger"); break;
      case 1: result.push("text-warning"); break;
      case 2: result.push("text-success"); break;
      case 3: result.push("text-gray"); break;
    }

    return result;
  }

  get levelname() {
    return this.$store.getters["User/levelname"];
  }

  get listmenu() {
    return this.$store.getters["Menu/MenuList"];
  }

  mounted() {
    window.handleSlimScroll();
  }
}
</script>
