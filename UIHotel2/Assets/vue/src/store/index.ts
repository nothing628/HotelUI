import Vue from "vue";
import Vuex from "vuex";
import { StoreOptions } from 'vuex';
import { User } from './modules/user';
import { RootState } from '@/store/types/types';

Vue.use(Vuex);

const store: StoreOptions<RootState> = {
    state: {
        app_title: "Hotel Management System",
        app_version: "v1.0.0"
    },
    modules: {
        User
    }
}

export default new Vuex.Store(store);
