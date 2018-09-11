import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store/index";
import "./registerServiceWorker";
import { ICS } from "@/lib/Interface";

declare global {
  interface Window {
    CS: ICS;
  }
}

Vue.config.productionTip = false;

const bus = new Vue();

Vue.prototype.$bus = bus;

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount("#page-container");
