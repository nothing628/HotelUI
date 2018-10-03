import { Module, MutationTree, GetterTree, ActionTree } from "vuex";
import { UserLoginState, RootState } from "../types/types";

const namespaced: boolean = true;
const state: UserLoginState = {
  Id: 1,
  Fullname: "2",
  Username: "2",
  Level: 0
};

const mutations: MutationTree<UserLoginState> = {
  logout(state) {
    state.Id = 0;
    state.Fullname = "";
    state.Username = "";
    state.Level = -1;
  },
  login(state, user) {
    state.Id = user.Id;
    state.Username = user.Username;
    state.Fullname = user.Fullname;
    state.Level = user.UserLevel;
  }
};

const getters: GetterTree<UserLoginState, RootState> = {
  is_login(state): boolean {
    return state.Id > 0;
  },
  levelname(state): string {
    var level = "";

    switch (state.Level) {
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
  },
  user_id(state): number {
    return state.Id;
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
