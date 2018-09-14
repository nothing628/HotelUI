<template>
  <div class="row">
    <div class="panel panel-inverse">
      <div class="panel-heading">
        <h4 class="panel-title">Database Setting</h4>
      </div>
      <div class="panel-body">
        <h1>Database Setting</h1>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";

@Component
export default class SettingDatabase extends Vue {
  mounted() {
    this.$store.commit("changeTitle", "Database Setting");
    this.$store.commit("changeSubtitle", "");
  }
}
</script>
