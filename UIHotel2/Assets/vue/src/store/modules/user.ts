import { Module, MutationTree, GetterTree, ActionTree } from "vuex";
import { UserLoginState, RootState } from "../types/types";

const namespaced: boolean = true;
const state: UserLoginState = {
  id: "1",
  fullname: "2",
  username: "2",
  userlevel: 0
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
    state.userlevel = user.UserLevel;
  }
};

const getters: GetterTree<UserLoginState, RootState> = {
  is_login(state): boolean {
    return state.id != "";
  },
  levelname(state): string {
    var level = "";

    switch (state.userlevel) {
      case 0:
        level = "Administrator";
        break;
      case 1:
        level = "Manager";
        break;
      case 2:
        level = "Receiptionist";
        break;
      case 3:
        level = "Cleaning Service";
        break;
      default:
        level = "Undefined";
    }

    return level;
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
