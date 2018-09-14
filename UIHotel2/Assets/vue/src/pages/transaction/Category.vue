<template>
  <div class="row">
    <div class="panel panel-inverse">
      <div class="panel-heading">
        <h4 class="panel-title">Transaction Category</h4>
      </div>
      <div class="panel-body">
        <h1>Transaction Category</h1>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";

@Component
export default class TransactionCategory extends Vue {
  mounted() {
    this.$store.commit("changeTitle", "Transaction Category");
    this.$store.commit("changeSubtitle", "");
  }
}
</script>
