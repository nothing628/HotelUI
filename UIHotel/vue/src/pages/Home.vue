<template>
    <v-layout row class="quick-stats">
        <v-flex md3 sm6>
            <div class="quick-stats__item bg-light-blue">
                <div class="quick-stats__info">
                    <h2>{{guest}}</h2>
                    <small>Total Tamu</small>
                </div>

                <div class="quick-stats__chart sparkline-bar-stats">
                    <v-icon dark size="50px">person</v-icon>
                </div>
            </div>
        </v-flex>

        <v-flex md3 sm6>
            <div class="quick-stats__item bg-amber">
                <div class="quick-stats__info">
                    <h2>{{free_room}}</h2>
                    <small>Total Ruang Tersedia</small>
                </div>

                <div class="quick-stats__chart sparkline-bar-stats">
                    <v-icon dark size="50px">event_available</v-icon>
                </div>
            </div>
        </v-flex>

        <v-flex md3 sm6>
            <div class="quick-stats__item bg-red">
                <div class="quick-stats__info">
                    <h2>{{used_room}}</h2>
                    <small>Total Ruang Terpakai</small>
                </div>

                <div class="quick-stats__chart sparkline-bar-stats">
                    <v-icon dark size="50px">event_busy</v-icon>
                </div>
            </div>
        </v-flex>

        <v-flex md3 sm6>
            <div class="quick-stats__item bg-purple">
                <div class="quick-stats__info">
                    <h2>{{ balance | currency }}</h2>
                    <small>Total Pemasukan</small>
                </div>

                <div class="quick-stats__chart sparkline-bar-stats">
                    <v-icon dark size="50px">attach_money</v-icon>
                </div>
            </div>
        </v-flex>
    </v-layout>
</template>
<script>
    import axios from 'axios'
    export default {
        data() {
            return {
                balance: 0,
                free_room: 0,
                used_room: 0,
                guest: 0,
            }
        },
        filters: {
            currency(val) {
                let b = parseFloat(val)

                return "Rp." + b.toLocaleString()
            }
        },
        methods: {
            getData() {
                axios.post('http://localhost.com/home/post/getDashboardData').then(this.getDataResponse)
            },
            getDataResponse(response) {
                let data = response.data

                if (data.success) {
                    let items = data.data

                    this.guest = items.guest
                    this.free_room = items.free_room
                    this.used_room = items.used_room
                    this.balance = items.balance
                }
            }
        },
        mounted() {
            this.getData()
        }
    }
</script>