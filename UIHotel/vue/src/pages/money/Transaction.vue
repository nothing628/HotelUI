<template>
    <v-card class="mb-4">
        <v-card-title primary-title>
            <h2 class="card-title mb-0">Pencatatan Transaksi</h2>
        </v-card-title>

        <v-container fluid grid-list-md>
            <v-layout row>
                <v-flex md6>
                    <v-menu ref="menu1"
                            :close-on-content-click="false"
                            v-model="modal1"
                            transition="scale-transition"
                            offset-y
                            :nudge-right="40"
                            min-width="310px"
                            :return-value.sync="mdate">
                        <v-text-field slot="activator"
                                      v-model="mdate"
                                      label="Transaction Date"
                                      prepend-icon="event"
                                      readonly></v-text-field>
                        <v-date-picker v-model="mdate"
                                       @change="$refs.menu1.save(mdate)"
                                       :max="allowArr" no-title scrollable>
                        </v-date-picker>
                    </v-menu>
                    <v-btn color="primary" style="vertical-align: top;" @click="newData">New Transaction</v-btn>
                </v-flex>
                <v-flex md6>
                    <h2 class="text-xl-right grey--text display-1">Balance : {{ balance | Currency }}</h2>
                </v-flex>
            </v-layout>
            <v-divider></v-divider>
            <v-layout row>
                <v-flex md12>
                    <v-data-table v-bind:headers="headers"
                                  v-bind:items="items"
                                  hide-actions
                                  class="elevation-1">
                        <template slot="items" slot-scope="props">
                            <tr :class="{'green--text': !props.item.IsExpense, 'red--text': props.item.IsExpense }">
                                <td>{{ props.item.Date | dateString }}</td>
                                <td>{{ props.item.Description }}</td>
                                <td><v-icon small>{{ props.item.Category.Icon }}</v-icon> {{ props.item.Category.Description }}</td>
                                <td v-if="!props.item.IsExpense"><strong>{{ props.item.Debit | Currency }}</strong></td>
                                <td v-if="props.item.IsExpense"><strong>{{ -props.item.Kredit | Currency }}</strong></td>
                                <td class="text-center" style="width: 7%;">
                                    <v-btn fab flat small @click="deleteData(props.item)">
                                        <v-icon color="error">clear</v-icon>
                                    </v-btn>
                                </td>
                            </tr>
                        </template>
                        <template slot="footer">
                            <td colspan="3">
                                <strong>Total</strong>
                            </td>
                            <td><strong>{{ balance | Currency }}</strong></td>
                            <td></td>
                        </template>
                    </v-data-table>
                </v-flex>
            </v-layout>
        </v-container>

        <tdialog @save="saveData"></tdialog>
        <alert></alert>
    </v-card>
</template>
<script>
    import moment from 'moment'
    import axios from 'axios'
    import tdialog from './TransactionDialog.vue'
    import alert from '../../components/Notification.vue'

    export default {
        components: {
            tdialog,
            alert,
        },
        data() {
            return {
                mdate: null,
                modal1: false,
                balance: 0,
                items: [],
                headers: [
                    { text: 'Date', sortable: false, align: 'left', },
                    { text: 'Description', sortable: false, align: 'left' },
                    { text: 'Category', sortable: false, align: 'left' },
                    { text: 'Amount', sortable: false, align: 'left' },
                    { text: 'Action', sortable: false, align: 'center' },
                ]
            }
        },
        computed: {
            allowArr() {
                return moment().format('YYYY-MM-DD')
            },
            totalDebit() {
                let amount = 0

                this.items.forEach(x => amount += x.Debit)

                return amount
            },
            totalKredit() {
                let amount = 0

                this.items.forEach(x => amount += x.Kredit)

                return amount
            }
        },
        filters: {
            dateString(val) {
                let momen = moment(val)

                return momen.format('YYYY-MM-DD HH:mm')
            },
            Currency(val) {
                return 'Rp' + parseFloat(val).toLocaleString()
            }
        },
        methods: {
            newData() {
                //New Transaction
                this.$bus.$emit('new-transaction')
            },
            deleteData(data) {
                axios.post('http://localhost.com/money/post/deleteTransaction', data).then(this.saveResponse)
            },
            saveData(data) {
                //Save Transaction
                axios.post('http://localhost.com/money/post/saveTransaction', data).then(this.saveResponse)
            },
            saveResponse(response) {
                let data = response.data

                if (data.success) {
                    this.getData()
                } else {
                    this.$bus.$emit('alert-show', { text: data.message, color: 'error', timeout: 6000 })
                }
            },
            getData() {
                let data = {
                    item_date: this.mdate
                }

                axios.post('http://localhost.com/money/post/getTransaction', data).then(this.getDataData)
            },
            getDataData(response) {
                let data = response.data

                if (data.success) {
                    let items = data.data

                    this.items = []
                    this.balance = data.balance
                    
                    items.forEach(x => this.items.push(x))
                }
            }
        },
        mounted() {
            this.mdate = moment().format('YYYY-MM-DD')
            this.getData()
        },
        watch: {
            mdate: {
                handler() {
                    this.getData()
                }
            }
        }
    }
</script>
