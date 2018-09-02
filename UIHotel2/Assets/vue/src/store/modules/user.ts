import { Module, MutationTree, GetterTree, ActionTree } from "vuex";
import { UserLoginState, RootState } from "../types/types";

const namespaced: boolean = true;
const state: UserLoginState = {
  id: "",
  fullname: "",
  username: ""
};

const mutations: MutationTree<UserLoginState> = {
  logout(state) {
    state.id = "";
    state.fullname = "";
    state.username = "";
  },
  login(state, user) {
    state.id = user.Id;
    state.username = user.Username;
    state.fullname = user.Fullname;
  }
};

const getters: GetterTree<UserLoginState, RootState> = {
  is_login(state): boolean {
    return state.id != "";
  }
};

const actions: ActionTree<UserLoginState, RootState> = {};

export const User: Module<UserLoginState, RootState> = {
  namespaced,
  state,
  getters,
  actions,
  mutations
};
