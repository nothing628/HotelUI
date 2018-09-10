import Vue from "vue";
import Vuex, { MutationTree } from "vuex";
import { StoreOptions } from "vuex";
import { User } from "./modules/user";
import { RootState } from "@/store/types/types";

Vue.use(Vuex);

const mutations: MutationTree<RootState> = {
  changeTitle(state, payload) {
    state.page_title = payload;
  },
  changeSubtitle(state, payload) {
    state.page_subtitle = payload;
  }
};

const store: StoreOptions<RootState> = {
  state: {
    app_title: "Hotel Management System",
    app_version: "v1.0.0",
    page_title: "",
    page_subtitle: ""
  },
  mutations,
  modules: {
    User
  }
};

export default new Vuex.Store(store);
