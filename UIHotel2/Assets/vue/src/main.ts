import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store/index";
import "./registerServiceWorker";
import { User } from "@/lib/Model/User";
import { ICS } from "@/lib/Interface";

declare global {
  interface Window {
    CS: ICS;
  }
}

Vue.config.productionTip = false;

const bus = new Vue();

var static_data = User.Get();
console.log(static_data);
Vue.prototype.$bus = bus;

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount("#page-container");
