<template>
  <div class="dataTables_paginate paging_simple_numbers">
    <ul :class="get_class">
      <li class="paginate_button previous" :class="{ disabled: !is_prev }" @click="pagePrev">
        <a href="javascript:;" tabindex="0">Previous</a>
      </li>
      <li :class="classActive(page)" @click="pageClick(page)" v-for="page in arr_page" :key="page">
        <a href="javascript:;" tabindex="0">{{ page }}</a>
      </li>
      <li class="paginate_button next" :class="{ disabled: !is_next }" @click="pageNext">
        <a href="javascript:;" tabindex="0">Next</a>
      </li>
    </ul>
  </div>
</template>
<script lang="ts">
import Vue from "vue";

export default Vue.extend({
  data() {
    return {
      currentPage: 1
    };
  },
  computed: {
    get_class(): Array<string> {
      let default_class = ["pagination"];
      
      if (this.nomargin) {
        default_class.push("m-0");
      }

      return default_class;
    },
    arr_page(): Array<number> {
      let i = 0;
      let res: Array<number> = [];

      for (i = 1; i <= this.totalPage; i++) {
        res.push(i);
      }

      return res;
    },
    is_next(): boolean {
      return this.currentPage < this.totalPage;
    },
    is_prev(): boolean {
      return this.currentPage > 1;
    }
  },
  props: {
    totalPage: { type: Number, required: true },
    maxSize: { type: Number, required: false, default: 5 },
    boundaryLinks: { type: Boolean, required: false, default: false },
    directionLinks: { type: Boolean, required: false, default: true },
    nomargin: { type: Boolean, required: false, default: false },
    value: { type: Number, required: false, default: 1 }
  },
  methods: {
    pageClick(page: number) {
      this.$emit("input", page);
    },
    pagePrev() {
      let prev_page = this.currentPage - 1;

      if (prev_page > 0) this.pageClick(prev_page);
    },
    pageNext() {
      let next_page = this.currentPage + 1;

      if (next_page <= this.totalPage) this.pageClick(next_page);
    },
    classActive(page: number) {
      let default_page = ["paginate_button"];

      if (this.currentPage == page) default_page.push("active");

      return default_page;
    }
  },
  watch: {
    value: {
      handler(newval, oldval) {
        this.currentPage = newval;
      }
    }
  }
});
</script>
