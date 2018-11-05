import { Module, MutationTree, GetterTree, ActionTree, ActionContext } from "vuex";
import { IMenuState, IRootState, IMenuBase } from "../types/types";

const namespaced: boolean = true;
const state: IMenuState = {
  listMenu: new Array<IMenuBase>()
};

const mutations: MutationTree<IMenuState> = {};
const getters: GetterTree<IMenuState, IRootState> = {
  MenuList(state: IMenuState): IMenuBase[] {
    return state.listMenu;
  }
};
const actions: ActionTree<IMenuState, IRootState> = {
  RegisterMenu(context: ActionContext<IMenuState, IRootState>): void {
    let state: IMenuState = context.state;
    let dashboard: any = {
      text: "Dashboard",
      route: "dashboard",
      show_rule: [ true, true, true, true ],
      icon: "fa fa-laptop"
    };
    let room: IMenuBase = {
      text: "Room",
      icon: "fa fa-codepen",
      show_rule: [ true, true, true, true ],
      submenu: [
        { show_rule: [ true, true, true, true ], text:"Room List", route: "room.list" },
        { show_rule: [ true, true, false, false ], text:"Room Maintance", route: "room.maintance" },
        { show_rule: [ true, true, false, false ], text:"Room Category", route: "room.category" },
        { show_rule: [ true, true, false, false ], text:"Room Price", route: "room.price" },
        { show_rule: [ true, true, false, false ], text:"Calendar Price", route: "room.calendar" }
      ]
    };
    let datam: IMenuBase = {
      text: "Data Maintain",
      icon: "fa fa-book",
      show_rule: [ true, true, true, false ],
      submenu: [
        { show_rule: [ true, true, true, false ], text: "Booking / Checkin", route: "data.booking" },
        { show_rule: [ true, true, true, false ], text: "Guest", route: "data.guest" }
      ]
    };
    let transaction: IMenuBase = {
      text: "Transaction",
      icon: "fa fa-diamond",
      show_rule: [ true, true, true, false ],
      submenu: [
        { show_rule: [ true, true, true, false ], text: "List Transaction", route: "transaction.list" },
        { show_rule: [ true, true, false, false ], text: "Transaction Category", route: "transaction.category" }
      ]
    };
    let report: IMenuBase = {
      text: "Report",
      icon: "fa fa-line-chart",
      show_rule: [ true, true, false, false ],
      submenu: [
        { show_rule: [ true, true, false, false ], text: "Booking Report", route: "report.hotel" },
        { show_rule: [ true, true, false, false ], text: "Transaction Report", route: "report.finance" }
      ]
    };
    let settings: IMenuBase = {
      text: "Settings",
      icon: "fa fa-wrench",
      show_rule: [ true, true, false, false ],
      submenu: [
        { show_rule: [ true, true, false, false ], text: "Application", route: "setting.app" },
        { show_rule: [ true, true, false, false ], text: "Users", route: "setting.user" }
      ]
    };

    state.listMenu.push(dashboard);
    state.listMenu.push(room);
    state.listMenu.push(datam);
    state.listMenu.push(transaction);
    state.listMenu.push(report);
    state.listMenu.push(settings);
  }
};
export const Menu: Module<IMenuState, IRootState> = {
  namespaced,
  state,
  getters,
  actions,
  mutations
};
