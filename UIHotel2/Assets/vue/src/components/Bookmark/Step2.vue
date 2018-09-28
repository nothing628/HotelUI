<template>
  <div class="form">
    <guest-form @search="show_search"></guest-form>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import GuestForm from "@/components/Bookmark/GuestForm.vue";


@Component({
  components: {
    GuestForm
  }
})
export default class CreateStep2 extends Vue {
  show_search() {
    console.log(false);
  }

  mounted() {
    //
  }
}
</script>
