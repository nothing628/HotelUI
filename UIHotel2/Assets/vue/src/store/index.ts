import Vue from "vue";
import Vuex, { MutationTree } from "vuex";
import { StoreOptions } from "vuex";
import { User } from "@/store/modules/user";
import { Setting } from "@/store/modules/setting";
import { IRootState } from "@/store/types/types";

Vue.use(Vuex);

const mutations: MutationTree<IRootState> = {
  changeTitle(state: IRootState, payload: any): void {
    state.page_title = payload;
  },
  changeSubtitle(state: IRootState, payload: any): void {
    state.page_subtitle = payload;
  }
};

const store: StoreOptions<IRootState> = {
  state: {
    app_title: "Hotel Management System",
    app_version: "v1.0.0",
    page_title: "",
    page_subtitle: ""
  },
  mutations,
  modules: {
    User,
    Setting
  }
};

export default new Vuex.Store(store);
