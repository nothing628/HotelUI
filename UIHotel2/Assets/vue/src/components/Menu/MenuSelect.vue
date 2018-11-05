<template>
  <menu-parent v-if="is_parent && is_show(menu_data.show_rule)" :text="menu_data.text" :icon="menu_data.icon">
    <menu-child v-for="(sub, i) in menu_data.submenu" v-if="is_show(sub.show_rule)" :key="i" :text="sub.text" :route="sub.route"></menu-child>
  </menu-parent>
  <menu-child v-else v-show="!is_parent && is_show(menu_data.show_rule)" :text="menu_data.text" :route="menu_data.route" :icon="menu_data.icon"></menu-child>
</template>
<script>
import MenuChild from "@/components/Menu/MenuChild.vue";
import MenuParent from "@/components/Menu/MenuParent.vue";

export default {
  components: {
    MenuChild,
    MenuParent
  },
  data() {
    return {
      //
    }
  },
  props: {
    menu_data: { type: Object, required: true },
    level: { type: Number, required: true }
  },
  computed: {
    is_parent() {
      return ("submenu" in this.menu_data);
    }
  },
  methods: {
    is_show(arr_perm) {
      return arr_perm[this.level];
    }
  }
}
</script>
