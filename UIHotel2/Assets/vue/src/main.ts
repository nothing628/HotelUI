import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store/index";
import "./registerServiceWorker";
import { Guest } from "@/lib/Model/Guest";

Vue.config.productionTip = false;

const bus = new Vue();
const guest = new Guest();

guest.Test();

Vue.prototype.$bus = bus;

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount("#page-container");
