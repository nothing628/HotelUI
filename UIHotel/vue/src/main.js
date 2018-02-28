import '../../node_modules/vuetify/dist/vuetify.min.css'
import Vue from 'vue'
import Vuetify from 'vuetify'
import Login from './pages/Login.vue'
import Booking from './pages/booking/Booking.vue'
import BookingList from './pages/booking/List.vue'
import Checkin from './pages/checkin/Checkin.vue'
import CheckinList from './pages/checkin/List.vue'
import CheckinDetail from './pages/checkin/Detail.vue'
import Checkout from './pages/checkin/Checkout.vue'
import InvoiceDetail from './pages/guest/Invoice.vue'
import InvoicePay from './pages/guest/Pay.vue'
import GuestCreate from './pages/guest/Create.vue'
import GuestList from './pages/guest/List.vue'
import GuestEdit from './pages/guest/Edit.vue'
import GuestDetail from './pages/guest/Detail.vue'
import MoneyTransaction from './pages/money/Transaction.vue'
import MoneyCategory from './pages/money/Category.vue'
import RoomDetail from './pages/room/Detail.vue'
import RoomCategory from './pages/room/Category.vue'
import RoomCalendar from './pages/room/Calendar.vue'
import RoomPrice from './pages/room/Price.vue'
import RoomList from './pages/room/List.vue'
import RoomMaintain from './pages/room/Setting.vue'
import SettingBasic from './pages/settings/Basic.vue'
import SettingUser from './pages/settings/User.vue'
import ReportCheckin from './pages/report/Checkin.vue'
import ReportMoney from './pages/report/Money.vue'

Vue.use(Vuetify)
Vue.config.productionTip = false
Vue.component('login', Login)
Vue.component('booking', Booking)
Vue.component('booking-list', BookingList)
Vue.component('checkin', Checkin)
Vue.component('checkin-list', CheckinList)
Vue.component('checkin-detail', CheckinDetail)
Vue.component('checkout', Checkout)
Vue.component('invoice-detail', InvoiceDetail)
Vue.component('invoice-pay', InvoicePay)
Vue.component('guest-create', GuestCreate)
Vue.component('guest-list', GuestList)
Vue.component('guest-edit', GuestEdit)
Vue.component('guest-detail', GuestDetail)
Vue.component('money-transaction', MoneyTransaction)
Vue.component('money-category', MoneyCategory)
Vue.component('room-detail', RoomDetail)
Vue.component('room-category', RoomCategory)
Vue.component('room-calendar', RoomCalendar)
Vue.component('room-list', RoomList)
Vue.component('room-price', RoomPrice)
Vue.component('room-setting', RoomMaintain)
Vue.component('report-checkin', ReportCheckin)
Vue.component('report-money', ReportMoney)
Vue.component('setting-basic', SettingBasic)
Vue.component('setting-user', SettingUser)

/* eslint-disable no-new */
Vue.prototype.$bus = new Vue()
new Vue({
    el: '#app',
    //router,
    //template: '<App/>',
    //components: { App }
})
