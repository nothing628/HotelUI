// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import '../../node_modules/vuetify/dist/vuetify.min.css'
import Vue from 'vue'
import Vuetify from 'vuetify'
import router from './router'
import Booking from './pages/booking/Booking'
import Checkin from './pages/checkin/Checkin'
import CheckinList from './pages/checkin/List'
import Checkout from './pages/checkin/Checkout'
import GuestList from './pages/guest/List'
import GuestEdit from './pages/guest/Edit'
import GuestDetail from './pages/guest/Detail'
import RoomDetail from './pages/room/Detail'
import RoomCategory from './pages/room/Category'
import RoomPrice from './pages/room/Price'
import RoomList from './pages/room/List'
import RoomMaintain from './pages/settings/Room'

Vue.use(Vuetify)
Vue.config.productionTip = false
Vue.component('booking', Booking)
Vue.component('checkin', Checkin)
Vue.component('checkin-list', CheckinList)
Vue.component('checkout', Checkout)
Vue.component('guest-list', GuestList)
Vue.component('guest-edit', GuestEdit)
Vue.component('guest-detail', GuestDetail)
Vue.component('room-detail', RoomDetail)
Vue.component('room-category', RoomCategory)
Vue.component('room-list', RoomList)
Vue.component('room-price', RoomPrice)
Vue.component("setting-room", RoomMaintain)

/* eslint-disable no-new */
new Vue({
  el: '#app',
  //router,
  //template: '<App/>',
  //components: { App }
})
