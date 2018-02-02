<template>
    <v-card>
        <v-card-title>
            <h2 class="card-title mb-0">Invoice Payment</h2>
        </v-card-title>

        <v-container fluid grid-list-md>
            <v-layout row>
                <v-flex lg4 md4 sm12 xs12>
                    <v-list three-line>
                        <v-list-tile>
                            <v-list-tile-content>
                                <v-list-tile-title>Issued To :</v-list-tile-title>
                                <v-list-tile-sub-title class="text--primary">Yogy Phang</v-list-tile-sub-title>
                                <v-list-tile-sub-title>Jl Tengku Umar, Tangerang Banten 15122</v-list-tile-sub-title>
                            </v-list-tile-content>
                        </v-list-tile>
                    </v-list>
                </v-flex>
                <v-flex lg4 md4 sm12 xs12>
                    <v-list three-line>
                        <v-list-tile>
                            <v-list-tile-content>
                                <v-list-tile-title>Invoice No :</v-list-tile-title>
                                <v-list-tile-sub-title class="text--primary"><a href="#">INV201802020001</a></v-list-tile-sub-title>
                                <v-list-tile-title class="mt-4">Issued Date :</v-list-tile-title>
                                <v-list-tile-sub-title class="text--primary">12/12/2018</v-list-tile-sub-title>
                            </v-list-tile-content>
                        </v-list-tile>
                    </v-list>
                </v-flex>
                <v-flex lg4 md4 sm12 xs12>
                    <v-btn dark color="primary" class="mb-4 ml-0 float-right">
                        <span>Print</span>
                        <v-icon right dark>print</v-icon>
                    </v-btn>
                </v-flex>
            </v-layout>
            <v-layout row>
                <v-flex lg12 md12 sm12 xs12>
                    <v-data-table v-bind:headers="tableData.headers"
                                  v-bind:items="tableData.items"
                                  v-bind:pagination.sync="tableData.pagination"
                                  v-bind:total-items="tableData.totalItems"
                                  v-bind:loading="tableData.loading"
                                  hide-actions
                                  class="elevation-1">
                        <template slot="items" slot-scope="props">
                            <tr>
                                <td>{{ (props.index + 1) }}</td>
                                <td>{{ props.item.TransactionDate | dateformat }}</td>
                                <td v-html="props.item.Description"></td>
                                <td>{{ props.item.AmmountIn | currency }}</td>
                                <td>{{ props.item.AmmountOut | currency }}</td>
                            </tr>
                        </template>
                        <template slot="footer">
                            <tr>
                                <td colspan="4">
                                    <strong>Total Balance</strong>
                                </td>
                                <td>{{ TotalBalance | currency }}</td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <strong>Tax (5%)</strong>
                                </td>
                                <td>{{ Tax | currency }}</td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <strong>Total Pay</strong>
                                </td>
                                <td>{{ TotalPay | currency }}</td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <strong>Deposit</strong>
                                </td>
                                <td>{{ Deposit | currency }}</td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <strong>Cash</strong>
                                </td>
                                <td><v-text-field prefix="Rp" type="number" v-model="Cash" label="Cash"></v-text-field></td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <strong>Cash Back</strong>
                                </td>
                                <td>{{ CashBack | currency }}</td>
                            </tr>
                            <tr>
                                <td colspan="4"></td>
                                <td>
                                    <v-btn dark color="success" class="mb-4 ml-0">
                                        <span>Close Invoice</span>
                                        <v-icon right dark>move_to_inbox</v-icon>
                                    </v-btn>
                                </td>
                            </tr>
                        </template>
                    </v-data-table>
                </v-flex>
            </v-layout>
        </v-container>
    </v-card>
</template>
<script>
    import axios from 'axios'
    export default {
        data() {
            return {
                Deposit: 50000,
                Cash: 0,
                tableData: {
                    totalItems: 0,
                    loading: false,
                    pagination: {},
                    headers: [
                        { text: '#', sortable: false, align: 'left' },
                        { text: 'Date', sortable: false, align: 'left' },
                        { text: 'Description', sortable: false, align: 'left' },
                        { text: 'Debit', sortable: false, align: 'left' },
                        { text: 'Kredit', sortable: false, align: 'left' },
                    ],
                    items: []
                }
            }
        },
        props: {
            id: { type: String, required: true }
        },
        computed: {
            Tax() {
                return this.TotalBalance * .05
            },
            TotalBalance() {
                var inVal = 0
                var outVal = 0

                this.tableData.items.forEach((item) => {
                    inVal += item.AmmountIn
                    outVal += item.AmmountOut
                })

                return Math.abs(inVal - outVal) + this.Deposit
            },
            TotalPay() {
                return this.TotalBalance + this.Tax
            },
            CashBack() {
                return this.Cash - this.TotalPay + this.Deposit
            }
        },
        filters: {
            currency(val) {
                var Nform = new Intl.NumberFormat('id-ID', { style: 'currency', currency: 'IDR' })

                return Nform.format(val)
            },
            dateformat(val) {
                var momen = moment(val)

                return momen.format('YYYY-MM-DD');
            }
        },
        methods: {
            getDataApi() {
                const id = this.id

                axios.post('http://localhost.com/checkin/post/getInvoiceDetail', { id })
                    .then(this.getData)
                    .catch(e => { })
            },
            getData(response) {
                var data = response.data

                if (data.success) {
                    var invoice = data.data

                    this.tableData.items = []

                    invoice.Details.forEach(x => this.tableData.items.push(x))

                    console.log(invoice)
                }
            }
        },
        mounted() {
            this.getDataApi()
        }
    }
</script>
