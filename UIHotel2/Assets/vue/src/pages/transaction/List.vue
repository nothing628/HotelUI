<template>
  <div class="row">
    <div class="panel panel-inverse">
      <div class="panel-heading">
        <h4 class="panel-title">List Transaction</h4>
      </div>
      <div class="panel-body">
        <h1>List Transaction</h1>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";

@Component
export default class TransactionList extends Vue {
  mounted() {
    this.$store.commit("changeTitle", "List Transaction");
    this.$store.commit("changeSubtitle", "");
  }
}
</script>
