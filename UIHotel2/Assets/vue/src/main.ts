import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store/index";
import "./registerGlobalScope";
import "./registerServiceWorker";

Vue.config.productionTip = false;

const bus = new Vue();

Vue.prototype.$bus = bus;

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount("#page-container");
