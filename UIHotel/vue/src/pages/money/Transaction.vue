<template>
    <v-card class="mb-4">
        <v-card-title primary-title>
            <h2 class="card-title mb-0">Pencatatan Transaksi</h2>
        </v-card-title>

        <v-container fluid grid-list-md>
            <v-layout row>
                <v-flex md12>
                    <label>Date :</label>
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
                                      prepend-icon="event"
                                      readonly></v-text-field>
                        <v-date-picker v-model="mdate"
                                       @change="$refs.menu1.save(mdate)"
                                       :max="allowArr" no-title scrollable>
                        </v-date-picker>
                    </v-menu>
                    <v-btn color="primary" @click="newData">New Transaction</v-btn>
                </v-flex>
            </v-layout>
            <v-divider></v-divider>
            <v-layout row>
                <v-flex md12>
                    <v-data-table v-bind:headers="headers"
                                  v-bind:items="items"
                                  class="elevation-1">
                        <template slot="items" slot-scope="props">
                            <tr :class="{'green--text': !props.item.IsExpense, 'red--text': props.item.IsExpense }">
                                <td>{{ props.item.Date | dateString }}</td>
                                <td><strong>{{ props.item.Description }}</strong></td>
                                <td>{{ props.item.Category.Description }}</td>
                                <td>{{ props.item.Debit | Currency }}</td>
                                <td>{{ props.item.Kredit | Currency }}</td>
                            </tr>
                        </template>
                    </v-data-table>
                </v-flex>
            </v-layout>
        </v-container>

        <tdialog @save="saveData"></tdialog>
    </v-card>
</template>
<script>
    import moment from 'moment'
    import axios from 'axios'
    import tdialog from '../../components/dialog/TransactionDialog.vue'

    export default {
        components: {
            tdialog
        },
        data() {
            return {
                mdate: null,
                modal1: false,
                items: [],
                headers: [
                    { text: 'Date', sortable: false, align: 'left', },
                    { text: 'Description', sortable: false, align: 'left' },
                    { text: 'Category', sortable: false, align: 'left' },
                    { text: 'Debit', sortable: false, align: 'left' },
                    { text: 'Kredit', sortable: false, align: 'left' },
                ]
            }
        },
        computed: {
            allowArr() {
                return moment().format('YYYY-MM-DD')
            },
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
            newData() {
                //New Transaction
                this.$bus.$emit('new-transaction')
            },
            saveData(data) {
                //Save Transaction
                axios.post('http://localhost.com/money/post/saveTransaction', data).then(this.saveResponse)
            },
            saveResponse(response) {
                let data = response.data

                if (data.success) {
                    this.getData()
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

                    items.forEach(x => this.items.push(x))
                    console.log(items)
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
