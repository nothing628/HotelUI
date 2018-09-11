/* eslint-disable */
import Vue from "vue";
import Router from "vue-router";
import RoomList from "./pages/room/List.vue";
import RoomCategory from "./pages/room/Category.vue";
import RoomPrice from "./pages/room/Price.vue";
import RoomCalendar from "./pages/room/Calendar.vue";
import RoomMaintance from "./pages/room/Maintance.vue"
import DataBooking from "./pages/data/Booking.vue";
import DataCheckin from "./pages/data/Checkin.vue";
import DataGuest from "./pages/data/Guest.vue";
import ReportFinance from "./pages/report/Finance.vue";
import ReportHotel from "./pages/report/Hotel.vue";
import SettingApp from "./pages/setting/Application.vue";
import SettingDB from "./pages/setting/Database.vue";
import SettingUser from "./pages/setting/User.vue";
import Setup1 from "./pages/setup/Step1DB.vue";
import Setup2 from "./pages/setup/Step2Migrate.vue";
import Setup3 from "./pages/setup/Step3Setting.vue";
import Setup4 from "./pages/setup/Step4Finish.vue";
import TransactionCategory from "./pages/transaction/Category.vue";
import TransactionList from "./pages/transaction/List.vue";
import Dashboard from "./pages/Dashboard.vue";

Vue.use(Router);

export default new Router({
  routes: [
    { path: "/", name: "dashboard", component: Dashboard },
    { path: "/data/booking", name: "data.booking", component: DataBooking },
    { path: "/data/checkin", name: "data.checkin", component: DataCheckin },
    { path: "/data/guest", name: "data.guest", component: DataGuest },
    { path: "/room/list", name: "room.list", component: RoomList },
    { path: "/room/category", name: "room.category", component: RoomCategory },
    { path: "/room/price", name: "room.price", component: RoomPrice },
    { path: "/room/calendar", name: "room.calendar", component: RoomCalendar },
    { path: "/room/maintance", name: "room.maintance", component: RoomMaintance },
    { path: "/report/finance", name: "report.finance", component: ReportFinance },
    { path: "/report/hotel", name: "report.hotel", component: ReportHotel },
    { path: "/setting/app", name: "setting.app", component: SettingApp },
    { path: "/setting/db", name: "setting.db", component: SettingDB },
    { path: "/setting/user", name: "setting.user", component: SettingUser },
    { path: "/setup/db", name: "setup.db", component: Setup1 },
    { path: "/setup/migrate", name: "setup.migrate", component: Setup2 },
    { path: "/setup/basic", name: "setup.basic", component: Setup3 },
    { path: "/setup/finish", name: "setup.finish", component: Setup4 },
    { path: "/transaction/category", name: "transaction.category", component: TransactionCategory },
    { path: "/transaction/list", name: "transaction.list", component: TransactionList }
  ]
});
