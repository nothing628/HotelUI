import Vue from "vue";
import * as uiv from "uiv";
import App from "./App.vue";
import router from "./router";
import store from "./store/index";
import "./registerGlobalScope";
import "./registerServiceWorker";

Vue.use(uiv, { prefix: "uiv" });
Vue.config.productionTip = false;

const bus = new Vue();

Vue.prototype.$bus = bus;

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount("#page-container");
