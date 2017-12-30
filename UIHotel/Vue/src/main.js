// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import '../../node_modules/vuetify/dist/vuetify.min.css'
import Vue from 'vue'
import Vuetify from 'vuetify'
import router from './router'
import RoomCategory from './pages/room/Category'
import RoomList from './pages/room/List'
import RoomMaintain from './pages/settings/Room'

Vue.use(Vuetify)
Vue.config.productionTip = false
Vue.component('room-category', RoomCategory)
Vue.component('room-list', RoomList)
Vue.component("setting-room", RoomMaintain)

/* eslint-disable no-new */
new Vue({
  el: '#app',
  //router,
  //template: '<App/>',
  //components: { App }
})
