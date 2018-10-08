<template>
  <div id="gritter-notice-wrapper">
    <item
    :id="notify.Id"
    :title="notify.Title"
    :content="notify.Content"
    :type="notify.Type"
    :interval="notify.Timeout"
    @timeout="removeAt(notify.Id)"
    v-for="notify in ListNotification"
    :key="notify.Id"></item>
  </div>
</template>
<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import Item from "@/components/Layout/NotificationItem.vue";
import { setInterval, clearInterval } from 'timers';

interface INotify {
  Title: string;
  Content: string;
  Id: number;
  Type?: string;
  Timeout?: number;
}

@Component({
  components: {
    Item
  }
})
export default class Notification extends Vue {
  ListNotification: Array<INotify> = new Array<INotify>();
  LastId: number = 1;

  add(data: INotify) {
    this.ListNotification.push(data);
  }

  addNotify(data: any) {
    var newNotify: INotify = {
      Id: this.LastId,
      Content: "",
      Title: "",
      Type: "",
      Timeout: 5000
    };

    if ("Content" in data) {
      newNotify.Content = data.Content;
    }

    if ("Title" in data) {
      newNotify.Title = data.Title;
    }

    if ("Type" in data) {
      newNotify.Type = data.Type;
    }

    if ("Timeout" in data) {
      newNotify.Timeout = data.Timeout;
    }
    
    this.add(newNotify);
    this.LastId++;
  }

  removeAt(id: number) {
    this.ListNotification.forEach((value: INotify, index: number) => {
      if (value.Id == id) {
        this.ListNotification.splice(index, 1);
      }
    });
  }

  removeFirst() {
    this.ListNotification.shift();
  }

  mounted() {
    window.bus.$on("Notify", this.addNotify);
  }

  beforeDestroy() {
    window.bus.$off("Notify", this.addNotify);
  }
}
</script>
