import { Module, MutationTree, GetterTree, ActionTree } from "vuex";
import { IUserLoginState, IRootState } from "../types/types";

const namespaced: boolean = true;
const state: IUserLoginState = {
  Id: 0,
  Fullname: "",
  Username: "",
  Level: -1
};

const mutations: MutationTree<IUserLoginState> = {
  logout(state: IUserLoginState): void {
    state.Id = 0;
    state.Fullname = "";
    state.Username = "";
    state.Level = -1;
  },
  login(state: IUserLoginState, user: any): void {
    state.Id = user.Id;
    state.Username = user.Username;
    state.Fullname = user.Fullname;
    state.Level = user.UserLevel;
  }
};

const getters: GetterTree<IUserLoginState, IRootState> = {
  is_login(state: IUserLoginState): boolean {
    return state.Id > 0;
  },
  levelname(state: IUserLoginState): string {
    let level: string = "";

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
  level(state: IUserLoginState): number {
    return state.Level;
  },
  user_id(state: IUserLoginState): number {
    return state.Id;
  }
};

const actions: ActionTree<IUserLoginState, IRootState> = {};

export const User: Module<IUserLoginState, IRootState> = {
  namespaced,
  state,
  getters,
  actions,
  mutations
};
