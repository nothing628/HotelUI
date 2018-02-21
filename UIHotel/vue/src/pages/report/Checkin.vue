<template>
    <v-card class="mb-4">
        <v-card-title primary-title>
            <h2 class="card-title mb-0">REPORT CHECKIN & BOOKING</h2>
        </v-card-title>

        <v-container fluid grid-list-md>
            <v-layout row>
                <v-flex md12>
                    <v-menu ref="menu1"
                            lazy
                            :close-on-content-click="false"
                            v-model="modal1"
                            transition="scale-transition"
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
                    s/d
                    <v-menu ref="menu2"
                            lazy
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
                    Range :
                    <v-btn-toggle v-model="range_type">
                        <v-btn flat value="d">
                            Days
                        </v-btn>
                        <v-btn flat value="m">
                            Monthly
                        </v-btn>
                        <v-btn flat value="y">
                            Yearly
                        </v-btn>
                    </v-btn-toggle>
                    <v-btn color="primary" @click="getData">Search</v-btn>
                </v-flex>
            </v-layout>
            <v-divider></v-divider>
            <v-layout row>
                <v-flex md12>
                    Checkin
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
                range_type: 'd',
            }
        },
        computed: {
            allowArr() {
                return moment().format('YYYY-MM-DD')
            }
        },
        methods: {
            getData() {
                let data = {
                    bdate: this.bdate,
                    edate: this.edate,
                    range_type: this.range_type
                }

                axios.post('http://localhost.com/report/post/getReportCheckin', data).then(this.getReportCheckin)
            },
            getDataData(response) {
                let data = response.data

                if (data.success) {

                    console.log(data)
                }
            }
        }
    }
</script>
