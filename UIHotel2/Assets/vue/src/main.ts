import "@/App.css";
import Vue from "vue";
import Vuelidate from "vee-validate";
import * as uiv from "uiv";
import App from "./App.vue";
import router from "./router";
import store from "./store/index";
import "./registerGlobalScope";
import "./registerServiceWorker";

Vue.use(uiv, { prefix: "uiv" });
Vue.use(Vuelidate);
Vue.config.productionTip = false;

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount("#page-container");
