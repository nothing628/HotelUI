<template>
    <v-card class="mb-4">
        <v-card-title primary-title>
            <h2 class="card-title mb-0">REPORT TRANSACTION</h2>
        </v-card-title>

        <v-container fluid grid-list-md>
            <v-layout row>
                <v-flex md12>
                    <label>Range :</label>
                    <v-btn-toggle v-model="range_type">
                        <v-btn flat value="c">Custom</v-btn>
                        <v-btn flat value="d">Daily</v-btn>
                        <v-btn flat value="m">Monthly</v-btn>
                        <v-btn flat value="y">Yearly</v-btn>
                    </v-btn-toggle>
                    <v-menu v-show="range_type == 'm'"
                            ref="menu1"
                            :close-on-content-click="false"
                            v-model="modal1"
                            transition="scale-transition"
                            offset-y
                            :nudge-right="40"
                            min-width="310px"
                            :return-value.sync="mdate">
                        <v-text-field slot="activator"
                                      v-model="mdate"
                                      prepend-icon="event"
                                      readonly></v-text-field>
                        <v-date-picker v-model="mdate"
                                       type="month"
                                       @change="$refs.menu1.save(mdate)"
                                       :max="allowArr" no-title scrollable>
                        </v-date-picker>
                    </v-menu>
                    <v-menu v-show="range_type == 'c' || range_type == 'd'"
                            ref="menu2"
                            :close-on-content-click="false"
                            v-model="modal2"
                            transition="scale-transition"
                            offset-y
                            :nudge-right="40"
                            min-width="310px"
                            :return-value.sync="bdate">
                        <v-text-field slot="activator"
                                      v-model="bdate"
                                      prepend-icon="event"
                                      readonly></v-text-field>
                        <v-date-picker v-model="bdate"
                                       :max="allowArr"
                                       @change="$refs.menu2.save(bdate)"
                                       no-title scrollable>
                        </v-date-picker>
                    </v-menu>
                    <label v-show="range_type == 'c'">s/d</label>
                    <v-menu v-show="range_type == 'c'"
                            ref="menu3"
                            :close-on-content-click="false"
                            v-model="modal3"
                            transition="scale-transition"
                            offset-y
                            :nudge-right="40"
                            min-width="310px"
                            :return-value.sync="edate">
                        <v-text-field slot="activator"
                                      v-model="edate"
                                      prepend-icon="event"
                                      readonly></v-text-field>
                        <v-date-picker v-model="edate"
                                       :max="allowArr"
                                       :min="bdate"
                                       @change="$refs.menu3.save(edate)"
                                       no-title scrollable>
                        </v-date-picker>
                    </v-menu>
                    <v-select v-bind:items="year_list"
                              v-show="range_type == 'y'"
                              class="ml-3"
                              style="max-width: 100px; display: inline-flex;"
                              v-model="year"></v-select>
                    <v-btn color="primary" @click="getData">Search</v-btn>
                    <v-btn color="primary" @click="download">Export</v-btn>
                </v-flex>
            </v-layout>
            <v-divider></v-divider>
            <v-layout row v-if="items.length > 0">
                <v-flex md12>
                    <bar-chart :width="900" :height="200" :chart-data.sync="datacollection" ></bar-chart>
                </v-flex>
            </v-layout>
            <v-layout row>
                <v-flex md12>
                    <v-data-table v-bind:headers="headers"
                                  v-bind:items="items"
                                  hide-actions
                                  class="elevation-1">
                        <template slot="items" slot-scope="props">
                            <td>{{ props.item.Date | dateString }}</td>
                            <td><strong>{{ props.item.Debit | Currency }}</strong></td>
                            <td><strong>{{ props.item.Kredit | Currency }}</strong></td>
                        </template>
                        <template slot="footer">
                            <td><strong>Total</strong></td>
                            <td><strong>{{ totalDebit | Currency }}</strong></td>
                            <td><strong>{{ totalKredit | Currency }}</strong></td>
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
    import BarChart from '../../components/BarChart'

    export default {
        components: {
            BarChart
        },
        data() {
            return {
                modal1: false,
                modal2: false,
                modal3: false,
                year: null,
                bdate: null,
                edate: null,
                mdate: null,
                range_type: 'c',
                items: [],
                headers: [
                    { text: 'Date', sortable: false, align: 'left' },
                    { text: 'Debit', sortable: false, align: 'left' },
                    { text: 'Kredit', sortable: false, align: 'left' },
                ],
                datacollection: {
                    labels: [],
                    datasets: [
                        {
                            label: 'Debit',
                            borderColor: '#59C859',
                            fill: false,
                            data: []
                        },
                        {
                            label: 'Kredit',
                            borderColor: '#f83900',
                            fill: false,
                            data: []
                        }
                    ]
                },
            }
        },
        computed: {
            allowArr() {
                return moment().format('YYYY-MM-DD')
            },
            year_list() {
                return ['2018']
            },
            maxYear() {
                return parseInt(moment().format('YYYY'))
            },
            calcBdate() {
                let momen = moment()

                switch (this.range_type) {
                    case 'y':
                        momen.year(this.year)
                        momen.dayOfYear(1)
                        return momen.format('YYYY-MM-DD')
                        break
                    case 'c':
                    case 'd':
                        return this.bdate
                        break
                    case 'm':
                        let mbdate = moment(this.mdate, "YYYY-MM");
                        return mbdate.format('YYYY-MM-DD')
                        break
                }
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
                        let mbdate = moment(this.calcBdate);
                        let eod = mbdate.endOf('M')
                        return eod.format('YYYY-MM-DD')
                        break
                    case 'y':
                        return moment(this.calcBdate).endOf('Y').format('YYYY-MM-DD')
                        break
                }
            },
            totalDebit() {
                let total = 0

                this.items.forEach(x => total += x.Debit)

                return total
            },
            totalKredit() {
                let total = 0

                this.items.forEach(x => total += x.Kredit)

                return total
            }
        },
        filters: {
            dateString(val) {
                let momen = moment(val)

                return momen.format('YYYY-MM-DD')
            },
            Currency(val) {
                return 'Rp' + parseFloat(val).toLocaleString()
            }
        },
        methods: {
            download() {
                // TODO: Export report
                if (this.items.length == 0)
                    return

                let row = 5
                let data = []

                data.push({ row: 4, col: 1, value: 'Date', border: 1 })
                data.push({ row: 4, col: 2, value: 'Debit', border: 1 })
                data.push({ row: 4, col: 3, value: 'Kredit', border: 1 })

                this.items.forEach(x => {
                    let date = moment(x.Date)

                    data.push({ row: row, col: 1, value: date.format('YYYY-MM-DD'), border: 1 })
                    data.push({ row: row, col: 2, value: x.Debit, border: 1 })
                    data.push({ row: row, col: 3, value: x.Kredit, border: 1 })
                    
                    row++
                })

                window.CS.export({
                    items: data,
                    options: [
                        { col: 0, width: 10 }
                    ]
                });
            },
            getData() {
                let data = {
                    bdate: this.calcBdate,
                    edate: this.calcEdate,
                }

                axios.post('http://localhost.com/report/post/getReportMoney', data).then(this.getDataData)
            },
            getDataData(response) {
                let data = response.data

                if (data.success) {
                    let items = data.data
                    let collect = {
                        labels: [],
                            datasets: [
                                {
                                label: 'Debit',
                                    borderColor: '#59C859',
                                    fill: false,
                                    data: []
                            },
                            {
                                label: 'Kredit',
                                borderColor: '#f83900',
                                fill: false,
                                data: []
                            }
                        ]
                    }

                    this.items = []

                    items.forEach(x => {
                        let momen = moment(x.Date)

                        collect.labels.push(momen.format('DD/MM'))
                        collect.datasets[0].data.push(x.Debit)
                        collect.datasets[1].data.push(x.Kredit)
                        
                        this.items.push(x)
                    })

                    this.datacollection = collect
                }
            }
        },
        mounted() {
            this.year = moment().format('YYYY')
        }
    }
</script>
