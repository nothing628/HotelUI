/* eslint-disable */
import Vue from "vue";
import Router from "vue-router";
import RoomList from "./pages/room/List.vue";
import RoomCategory from "./pages/room/Category.vue";
import RoomPrice from "./pages/room/Price.vue";
import RoomCalendar from "./pages/room/Calendar.vue";
import RoomMaintance from "./pages/room/Maintance.vue";
import CreateBooking from "@/pages/data/booking/Create.vue";
import CreateGuest from "@/pages/data/guest/Create.vue";
import EditGuest from "@/pages/data/guest/Edit.vue";
import DataBooking from "./pages/data/Booking.vue";
import DataInvoice from "./pages/data/Invoice.vue";
import DataGuest from "./pages/data/Guest.vue";
import ReportFinance from "./pages/report/Finance.vue";
import ReportHotel from "./pages/report/Hotel.vue";
import SettingApp from "./pages/setting/Application.vue";
import SettingUser from "./pages/setting/User.vue";
import Setup1 from "./pages/setup/Step1DB.vue";
import Setup2 from "./pages/setup/Step2Migrate.vue";
import Setup4 from "./pages/setup/Step4Finish.vue";
import TransactionCategory from "./pages/transaction/Category.vue";
import TransactionList from "./pages/transaction/List.vue";
import Dashboard from "./pages/Dashboard.vue";

Vue.use(Router);

export default new Router({
  routes: [
    { path: "/", name: "dashboard", component: Dashboard },
    { path: "/data/booking", name: "data.booking", component: DataBooking },
    { path: "/data/booking/create", name: "data.booking.create", component: CreateBooking },
    { path: "/data/invoice/:id", name: "data.invoice", component: DataInvoice },
    { path: "/data/guest", name: "data.guest", component: DataGuest },
    { path: "/data/guest/create", name: "data.guest.create", component: CreateGuest },
    { path: "/data/guest/edit/:id", name: "data.guest.edit", component: EditGuest },
    { path: "/room/list", name: "room.list", component: RoomList },
    { path: "/room/category", name: "room.category", component: RoomCategory },
    { path: "/room/price", name: "room.price", component: RoomPrice },
    { path: "/room/calendar", name: "room.calendar", component: RoomCalendar },
    { path: "/room/maintance", name: "room.maintance", component: RoomMaintance },
    { path: "/report/finance", name: "report.finance", component: ReportFinance },
    { path: "/report/hotel", name: "report.hotel", component: ReportHotel },
    { path: "/setting/app", name: "setting.app", component: SettingApp },
    { path: "/setting/user", name: "setting.user", component: SettingUser },
    { path: "/setup/db", name: "setup.db", component: Setup1, meta: { is_setup: true } },
    { path: "/setup/migrate", name: "setup.migrate", component: Setup2, meta: { is_setup: true } },
    { path: "/setup/finish", name: "setup.finish", component: Setup4, meta: { is_setup: true } },
    { path: "/transaction/category", name: "transaction.category", component: TransactionCategory },
    { path: "/transaction/list", name: "transaction.list", component: TransactionList }
  ]
});
