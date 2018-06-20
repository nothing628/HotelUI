<template>
    <v-card>
        <v-container fluid grid-list-md>
            <v-layout row>
                <v-flex lg4 md4 sm12 xs12>
                    <v-list three-line>
                        <v-list-tile>
                            <v-list-tile-content class="top">
                                <v-list-tile-title>Issued To :</v-list-tile-title>
                                <v-list-tile-sub-title class="text--primary"><a href="#">{{ guest.fullname }}</a></v-list-tile-sub-title>
                                <v-list-tile-sub-title>{{ guest.address }}</v-list-tile-sub-title>
                                <v-list-tile-title>Status :</v-list-tile-title>
                                <v-list-tile-sub-title></v-list-tile-sub-title>
                            </v-list-tile-content>
                        </v-list-tile>
                    </v-list>
                </v-flex>
                <v-flex lg4 md4 sm12 xs12>
                    <v-list three-line>
                        <v-list-tile>
                            <v-list-tile-content>
                                <v-list-tile-title>Invoice No :</v-list-tile-title>
                                <v-list-tile-sub-title class="text--primary"><a href="#">{{ invoice.invoice_no }}</a></v-list-tile-sub-title>
                                <v-list-tile-title class="mt-4">Issued Date :</v-list-tile-title>
                                <v-list-tile-sub-title class="text--primary">{{ IssuedDate }}</v-list-tile-sub-title>
                            </v-list-tile-content>
                        </v-list-tile>
                    </v-list>
                </v-flex>
            </v-layout>
            <v-layout row>
                <v-flex md12>
                    <v-btn dark color="primary" v-if="AllowClose" @click="closeInvoice" class="mb-4 ml-0 float-right no-print">
                        <span>Close</span>
                        <v-icon right dark>done_all</v-icon>
                    </v-btn>
                    <v-btn dark color="success" v-if="AllowPay" @click="pay" class="mb-4 ml-0 float-right no-print">
                        <span>Pay</span>
                        <v-icon right dark>move_to_inbox</v-icon>
                    </v-btn>
                    <v-btn dark color="primary" class="mb-4 ml-0 float-right no-print" @click.stop="print">
                        <span>Print</span>
                        <v-icon right dark>print</v-icon>
                    </v-btn>
                    <v-btn dark color="warning" :href="CheckinLink" @click="detail" class="mb-4 ml-0 float-right no-print">
                        <span>Back</span>
                        <v-icon right dark>reply</v-icon>
                    </v-btn>
                </v-flex>
            </v-layout>
            <v-layout row>
                <v-flex lg12 md12 sm12 xs12>
                    <v-data-table v-bind:headers="tableData.headers"
                                  v-bind:items="AllowedItems"
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
                                <td>{{ -(TotalPay) | currency }}</td>
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
                        </template>
                    </v-data-table>
                </v-flex>
            </v-layout>
        </v-container>
    </v-card>
</template>
<style>
    @media screen {
        .no-print {
        }
    }

    @media print {
        table.table tbody td,
        table.table tfoot td,
        table.table thead th {
            border: 1px solid black;
        }

        .no-print {
            display: none;
        }
    }
</style>
<script>
    import axios from 'axios'
    import moment from 'moment'
    export default {
        data() {
            return {
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
                },
                invoice: {
                    invoice_no: null,
                    invoice_close: false,
                    checkin_id: null,
                    issued_date: null,
                },
                guest: {
                    fullname: null,
                    address: null,
                }
            }
        },
        props: {
            id: { type: String, required: true }
        },
        computed: {
            AllowedItems() {
                var items = this.tableData.items.filter(item => {
                    let kind = item.IdKind

                    return (kind != 98 && kind != 99)
                })

                return items
            },
            Tax() {
                let data = this.tableData.items.filter(x => x.IdKind == 99)
                
                if (data.length > 0)
                    return -(data[0].AmmountOut)
                
                return 0
            },
            TotalKredit() {
                var outVal = 0

                this.tableData.items.forEach((item) => {
                    outVal += item.AmmountOut
                })

                return outVal
            },
            TotalDebit() {
                var inVal = 0

                this.tableData.items.forEach((item) => {
                    inVal += item.AmmountIn
                })

                return inVal
            },
            TotalBalance() {
                var inVal = 0
                var outVal = 0

                this.tableData.items.forEach((item) => {
                    let kind = item.IdKind

                    if (kind != 99) {
                        inVal += item.AmmountIn
                        outVal += item.AmmountOut
                    }
                })

                return (inVal - outVal)
            },
            TotalPay() {
                return this.TotalBalance + this.Tax
            },
            CashBack() {
                let data = this.tableData.items.filter(x => x.IdKind == 98)
                let cashback = parseInt(this.Cash) + this.TotalPay

                if (data.length > 0)
                    cashback += data[0].AmmountOut

                return cashback
            },
            IssuedDate() {
                var momen = moment(this.invoice.issued_date)

                return momen.format('DD/MM/YYYY');
            },
            AllowClose() {
                return this.TotalPay < 0 && !this.invoice.invoice_close
            },
            AllowPay() {
                return this.Cash > 0 && !this.invoice.invoice_close
            },
            CheckinLink() {
                return "http://localhost.com/checkin/get/detail?id=" + this.invoice.checkin_id
            }
        },
        filters: {
            currency(val) {
                var Nform = new Intl.NumberFormat('id-ID', { style: 'currency', currency: 'IDR' })

                return Nform.format(val)
            },
            dateformat(val) {
                var momen = moment(val)

                return momen.format('DD/MM/YYYY');
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
                    var guest = data.guest

                    this.invoice.invoice_no = invoice.Id
                    this.invoice.checkin_id = invoice.IdCheckin
                    this.invoice.invoice_close = invoice.IsClosed
                    this.invoice.issued_date = invoice.UpdateAt
                    this.guest.fullname = guest.Fullname
                    this.guest.address = guest.FullAddress
                    this.tableData.items = []

                    invoice.Details.forEach(x => this.tableData.items.push(x))

                    this.tableData.totalItems = this.tableData.items.length
                }
            },
            pay() {
                const invoiceId = this.invoice.invoice_no
                const countPay = this.Cash

                axios.post('http://localhost.com/checkin/post/payInvoice', { id: invoiceId, pay: countPay })
                    .then(this.payData)
                    .catch(() => { })
            },
            payData(response) {
                var data = response.data

                if (data.success) {
                    this.Cash = 0
                    this.getDataApi()
                }
            },
            closeInvoice() {
                let data = {
                    id: this.invoice.invoice_no,
                    cashback: this.CashBack,
                }

                axios.post('http://localhost.com/checkin/post/closeInvoice', data)
                    .then(this.closeInvoiceData)
                    .catch(() => { })
            },
            closeInvoiceData(response) {
                var data = response.data

                if (data.success) {
                    this.getDataApi()
                }
            },
            detail() {
                //
            },
            print() {
                // Print dialog
                window.CS.print().then((e) => { })
            }
        },
        watch: {
            Cash: {
                handler() {
                    if (this.Cash < 0)
                        this.Cash = -(this.Cash)
                }
            }
        },
        mounted() {
            this.getDataApi()
        }
    }
</script>
