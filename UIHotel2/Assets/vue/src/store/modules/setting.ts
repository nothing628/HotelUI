import { Module, MutationTree, GetterTree, ActionTree, ActionContext } from "vuex";
import { ISettingState, IRootState } from "../types/types";

const namespaced: boolean = true;
const state: ISettingState = {
  App_Key: "",
  Hotel_Address: "",
  Hotel_Name: "",
  Hotel_Logo: "",
  Deposit: 0,
  Penalty: 0,
  Time_Checkin: "",
  Time_Checkout: "",
  Time_Fullcharge: "",
  SQL_Database: "",
  SQL_Host: "",
  SQL_Port: 0,
  SQL_Password: "",
  SQL_User: ""
};

const mutations: MutationTree<ISettingState> = {};
const getters: GetterTree<ISettingState, IRootState> = {};
const actions: ActionTree<ISettingState, IRootState> = {
  CopySetting(context: ActionContext<ISettingState, IRootState>): any {
    context.state.App_Key = window.CS.Setting.App_Key;
    context.state.Hotel_Name = window.CS.Setting.Hotel_Name;
    context.state.Hotel_Logo = window.CS.Setting.Hotel_Logo;
    context.state.Hotel_Address = window.CS.Setting.Hotel_Address;
    context.state.Deposit = window.CS.Setting.Deposit;
    context.state.Penalty = window.CS.Setting.Penalty;
    context.state.Time_Checkin = window.CS.Setting.Time_Checkin;
    context.state.Time_Checkout = window.CS.Setting.Time_Checkout;
    context.state.Time_Fullcharge = window.CS.Setting.Time_Fullcharge;
    context.state.SQL_Database = window.CS.Setting.SQL_Database;
    context.state.SQL_Host = window.CS.Setting.SQL_Host;
    context.state.SQL_Port = window.CS.Setting.SQL_Port;
    context.state.SQL_User = window.CS.Setting.SQL_User;
    context.state.SQL_Password = window.CS.Setting.SQL_Password;
  },
  LoadSetting(context: ActionContext<ISettingState, IRootState>): any {
    window.CS.Setting.Load();
    context.dispatch("CopySetting");
  },
  SaveAppSetting(context: ActionContext<ISettingState, IRootState>): any {
    window.CS.Setting.Hotel_Name = context.state.Hotel_Name;
    window.CS.Setting.Hotel_Logo = context.state.Hotel_Logo;
    window.CS.Setting.Hotel_Address = context.state.Hotel_Address;
    window.CS.Setting.Deposit = context.state.Deposit;
    window.CS.Setting.Penalty = context.state.Penalty;
    window.CS.Setting.Time_Checkin = context.state.Time_Checkin;
    window.CS.Setting.Time_Checkout = context.state.Time_Checkout;
    window.CS.Setting.Time_Fullcharge = context.state.Time_Fullcharge;
    window.CS.Setting.Save();
  },
  SaveDBSetting(context: ActionContext<ISettingState, IRootState>): any {
    //
  }
};

export const Setting: Module<ISettingState, IRootState> = {
  namespaced,
  state,
  getters,
  actions,
  mutations
};