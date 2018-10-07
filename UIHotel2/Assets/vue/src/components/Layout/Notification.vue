<template>
  <div id="gritter-notice-wrapper">
    <item :id="12" title="test" content="Test 12" v-for="notify in ListNotification" :key="notify.Id"></item>
  </div>
</template>
<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import Item from "@/components/Layout/NotificationItem.vue";

interface INotify {
  Title: string;
  Content: string;
  Id: number;
  Type?: string;
}

@Component({
  components: {
    Item
  }
})
export default class Notification extends Vue {
  ListNotification: Array<INotify> = new Array<INotify>();

  add(data: INotify) {
    this.ListNotification.push(data);
  }

  mounted() {
    //
  }
}
</script>
