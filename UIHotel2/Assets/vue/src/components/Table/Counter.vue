<template>
  <div class="dataTables_info">Showing {{ from }} to {{ to }} of {{ total }} entries</div>
</template>
<script lang="ts">
import Vue from "vue";

export default Vue.extend({
  props: {
    from: { type: Number, required: false, default: 0 },
    to: { type: Number, required: false, default: 0 },
    total: { type: Number, required: false, default: 0 }
  }
});
</script>
