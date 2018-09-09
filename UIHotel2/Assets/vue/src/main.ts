import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store/index";
import "./registerServiceWorker";
import { Guest } from "@/lib/Model/Guest";
import { ICS } from "@/lib/Interface";

declare global {
  interface Window {
    CS: ICS;
  }
}

Vue.config.productionTip = false;

const bus = new Vue();
const guest = new Guest();

var guest_data = guest.GetBuilder().Where("id", 2).First();
console.log(guest_data);
Vue.prototype.$bus = bus;

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount("#page-container");
