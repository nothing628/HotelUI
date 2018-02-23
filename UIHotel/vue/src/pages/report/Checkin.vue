<template>
    <v-card class="mb-4">
        <v-card-title primary-title>
            <h2 class="card-title mb-0">REPORT CHECKIN & BOOKING</h2>
        </v-card-title>

        <v-container fluid grid-list-md>
            <v-layout row>
                <v-flex md12>
                    <label>Range :</label>
                    <v-btn-toggle v-model="range_type">
                        <v-btn flat value="c">
                            Custom
                        </v-btn>
                        <v-btn flat value="d">
                            Daily
                        </v-btn>
                        <v-btn flat value="m">
                            Monthly
                        </v-btn>
                        <v-btn flat value="y">
                            Yearly
                        </v-btn>
                    </v-btn-toggle>
                    <v-menu ref="menu1"
                            :close-on-content-click="false"
                            v-model="modal1"
                            transition="scale-transition"
                            offset-y
                            :nudge-right="40"
                            min-width="310px"
                            :return-value.sync="bdate">
                        <v-text-field slot="activator"
                                      v-model="bdate"
                                      prepend-icon="event"
                                      readonly></v-text-field>
                        <v-date-picker v-model="bdate" :max="allowArr" no-title scrollable>
                            <v-btn flat color="primary" @click="$refs.menu1.save(bdate)">OK</v-btn>
                        </v-date-picker>
                    </v-menu>
                    <label v-show="range_type == 'c'">s/d</label>
                    <v-menu v-show="range_type == 'c'"
                            ref="menu2"
                            :close-on-content-click="false"
                            v-model="modal2"
                            transition="scale-transition"
                            offset-y
                            :nudge-right="40"
                            min-width="310px"
                            :return-value.sync="edate">
                        <v-text-field slot="activator"
                                      v-model="edate"
                                      prepend-icon="event"
                                      readonly></v-text-field>
                        <v-date-picker v-model="edate" :max="allowArr" :min="bdate" no-title scrollable>
                            <v-btn flat color="primary" @click="$refs.menu2.save(edate)">OK</v-btn>
                        </v-date-picker>
                    </v-menu>
                    <v-btn color="primary" @click="getData">Search</v-btn>
                </v-flex>
            </v-layout>
            <v-divider></v-divider>
            <v-layout row>
                <v-flex md12>
                    <v-data-table v-bind:headers="headers"
                                  v-bind:items="items"
                                  class="elevation-1">
                        <template slot="items" slot-scope="props">
                            <td>{{ props.item.Date | dateString }}</td>
                            <td>{{ props.item.BookingCount }}</td>
                            <td>{{ props.item.CheckinCount }}</td>
                            <td>{{ props.item.CheckoutCount }}</td>
                        </template>
                    </v-data-table>
                </v-flex>
            </v-layout>
        </v-container>
    </v-card>
</template>
<script>
    import axios from 'axios'
    import moment from 'moment'
    
    export default {
        data() {
            return {
                modal1: false,
                modal2: false,
                bdate: null,
                edate: null,
                range_type: 'c',
                items: [],
                headers: [
                    { text: 'Date', sortable: false, align: 'left', },
                    { text: 'Booking', sortable: false, align: 'left' },
                    { text: 'Checkin', sortable: false, align: 'left' },
                    { text: 'Chekout', sortable: false, align: 'left' },
                ]
            }
        },
        computed: {
            allowArr() {
                return moment().format('YYYY-MM-DD')
            },
            calcEdate() {
                let momen = moment(this.bdate)

                switch (this.range_type) {
                    case 'c':
                        return this.edate
                        break
                    case 'd':
                        return this.bdate
                        break
                    case 'm':
                        return momen.format('YYYY-MM-DD')
                        break
                    case 'y':
                        return momen.format('YYYY-MM-DD')
                        break
                }
            }
        },
        filters: {
            dateString(val) {
                let momen = moment(val)

                return momen.format('YYYY-MM-DD')
            }
        },
        methods: {
            getData() {
                let data = {
                    bdate: this.bdate,
                    edate: this.calcEdate,
                    range_type: this.range_type
                }

                axios.post('http://localhost.com/report/post/getReportCheckin', data).then(this.getDataData)
            },
            getDataData(response) {
                let data = response.data

                if (data.success) {
                    let items = data.data

                    this.items = []

                    items.forEach(x => this.items.push(x))
                    console.log(data)
                }
            }
        }
    }
</script>
