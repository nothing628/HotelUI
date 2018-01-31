<template>
    <div>
        <v-tabs color="blue-grey darken-1" slot="extension" v-model="tab" centered dark>
            <v-tabs-slider color="yellow"></v-tabs-slider>
            <v-tab>Detail</v-tab>
            <v-tab>Invoice</v-tab>
        </v-tabs>

        <v-tabs-items v-model="tab">
            <v-tab-item>
                <v-card>
                    <v-card-title>
                        <h2 class="card-title mb-0">Checkin Detail</h2>
                    </v-card-title>
                    <v-container fluid grid-list-md>
                        <v-layout row>
                            <v-flex lg3 md3 sm12 xs12>
                                <img style="max-height: 190px;" class="img-fluid img-thumbnail rounded" :src="PhotoUrl" @error="imageError" />
                            </v-flex>
                            <v-flex lg9 md9 sm12 xs12>
                                <v-layout row>
                                    <v-flex xs3>
                                        <v-subheader>Checkin Number</v-subheader>
                                    </v-flex>
                                    <v-flex xs4>
                                        <v-subheader>{{ checkin.Id }}</v-subheader>
                                    </v-flex>
                                </v-layout>
                                <v-layout row>
                                    <v-flex xs3>
                                        <v-subheader>Invoice Number</v-subheader>
                                    </v-flex>
                                    <v-flex xs4>
                                        <v-subheader>
                                            <a href="#" @click="tab = 1">{{ checkin.InvoiceId }}</a>
                                        </v-subheader>
                                    </v-flex>
                                </v-layout>
                                <v-layout row>
                                    <v-flex xs3>
                                        <v-subheader>Guest Name</v-subheader>
                                    </v-flex>
                                    <v-flex xs4>
                                        <v-subheader>
                                            <a :href="checkin.DetailGuest">{{ checkin.Fullname }}</a>
                                        </v-subheader>
                                    </v-flex>
                                </v-layout>
                                <v-layout row>
                                    <v-flex xs3>
                                        <v-subheader>Room Number</v-subheader>
                                    </v-flex>
                                    <v-flex xs4>
                                        <v-subheader>
                                            <a :href="checkin.DetailRoom">{{ checkin.RoomName }}</a>
                                        </v-subheader>
                                    </v-flex>
                                </v-layout>
                                <v-layout row>
                                    <v-flex xs3>
                                        <v-subheader>Checkin At</v-subheader>
                                    </v-flex>
                                    <v-flex xs4>
                                        <v-subheader>{{ checkin.CheckinAt }}</v-subheader>
                                    </v-flex>
                                </v-layout>
                                <v-layout row>
                                    <v-flex xs3>
                                        <v-subheader>Departure At</v-subheader>
                                    </v-flex>
                                    <v-flex xs4>
                                        <v-subheader>{{ checkin.DepartureAt }}</v-subheader>
                                    </v-flex>
                                </v-layout>
                                <v-layout row>
                                    <v-flex xs3>
                                        <v-subheader>Note</v-subheader>
                                    </v-flex>
                                    <v-flex xs9>
                                        <v-subheader>{{ checkin.Note }}</v-subheader>
                                    </v-flex>
                                </v-layout>
                                <v-layout row>
                                    <v-flex xs9>
                                        <v-btn dark color="success" class="mb-4 ml-0" :href="checkin.InvoiceLink">
                                            <span>Pay Invoice</span>
                                            <v-icon right dark>move_to_inbox</v-icon>
                                        </v-btn>
                                        <v-btn dark color="error" class="mb-4 ml-0" :href="checkin.CheckoutLink">
                                            <span>Checkout</span>
                                            <v-icon right dark>move_to_inbox</v-icon>
                                        </v-btn>
                                        <v-btn dark disabled depressed class="mb-4 ml-0">
                                            <span>Checkout</span>
                                            <v-icon right dark>move_to_inbox</v-icon>
                                        </v-btn>
                                    </v-flex>
                                </v-layout>
                            </v-flex>
                        </v-layout>
                    </v-container>
                </v-card>
            </v-tab-item>
            <v-tab-item>
                <v-card>
                    <v-card-title>
                        <h2 class="card-title mb-0">Invoice</h2>
                    </v-card-title>
                    <v-container fluid grid-list-md>
                        <v-layout row>
                            <v-flex lg12 md12 sm12 xs12>
                                <v-btn dark color="success" class="mb-4 ml-0" :href="checkin.InvoiceLink">
                                    <span>Pay Invoice</span>
                                    <v-icon right dark>move_to_inbox</v-icon>
                                </v-btn>
                                <v-btn dark color="primary" class="mb-4 ml-0" :href="checkin.PayLink">
                                    <span>Print</span>
                                    <v-icon right dark>print</v-icon>
                                </v-btn>
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
                                                <strong>Deposit</strong>
                                            </td>
                                            <td>{{ deposit | currency }}</td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <strong>Total Pay</strong>
                                            </td>
                                            <td>{{ TotalPay | currency }}</td>
                                        </tr>
                                    </template>
                                </v-data-table>
                            </v-flex>
                        </v-layout>
                    </v-container>
                </v-card>
            </v-tab-item>
        </v-tabs-items>
    </div>
</template>
<script>
    import axios from 'axios'
    export default {
        data() {
            return {
                deposit: 50000,
                tab: null,
                checkin: {
                    Id: null,
                    Fullname: null,
                    PhotoGuest: null,
                    DetailGuest: null,
                    DetailRoom: null,
                    RoomName: null,
                    CheckinAt: null,
                    DepartureAt: null,
                    InvoiceId: null,
                    InvoiceLink: null,
                    PayLink: null,
                    CheckoutLink: null,
                    Note: null,
                },
                tableData: {
                    totalItems: 0,
                    loading: false,
                    pagination: {},
                    headers: [
                        { text: '#', sortable: false, align: 'left' },
                        { text: 'Date', sortable: false, align: 'left'  },
                        { text: 'Description', sortable: false, align: 'left' },
                        { text: 'In', sortable: false, align: 'left' },
                        { text: 'Out', sortable: false, align: 'left' },
                    ],
                    items: []
                }
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
        props: {
            checkid: { type: String, required: true }
        },
        watch: {
            tab: {
                handler() {}
            }
        },
        computed: {
            TotalBalance() {
                var inVal = 0
                var outVal = 0

                this.tableData.items.forEach((item) => {
                    inVal += item.AmmountIn
                    outVal += item.AmmountOut
                })

                return (inVal - outVal)
            },
            TotalPay() {
                return 0 - (this.TotalBalance) - this.deposit
            },
            PhotoUrl() {
                if (this.checkin.PhotoGuest)
                    return 'http://localhost.com/Upload/' + this.checkin.PhotoGuest
                else
                    return 'http://localhost.com/images/users.png'
            }
        },
        methods: {
            imageError() {
                this.PhotoHash = ''
            },
            getData(response) {
                var data = response.data

                if (data.success) {
                    var check = data.data

                    this.checkin.Id = check.Id
                    this.checkin.CheckinAt = check.CheckinAt
                    this.checkin.DepartureAt = check.DepartureAt
                    this.checkin.Note = check.Note
                    this.checkin.InvoiceId = check.Invoice.Id
                    this.checkin.Fullname = check.Guest.Fullname
                    this.checkin.PhotoGuest = check.Guest.PhotoGuest
                    this.checkin.DetailGuest = check.Guest.DetailLink
                    this.checkin.DetailRoom = check.Room.DetailLink
                    this.checkin.InvoiceLink = check.Invoice.InvoiceLink
                    this.checkin.PayLink = check.Invoice.PayLink
                    this.checkin.CheckoutLink = check.CheckoutLink
                    this.checkin.RoomName = check.Room.RoomName
                }
            },
            getDataApi() {
                const id = this.checkid

                axios.post('http://localhost.com/checkin/post/getCheckinDetail', { id })
                    .then(this.getData)
                    .catch(e => { })
            },
            getInvoice(response) {
                var data = response.data

                if (data.success) {
                    var realdata = data.data

                    this.tableData.items = []

                    realdata.Details.forEach(x => this.tableData.items.push(x))
                }
            },
            getInvoiceApi() {
                const id = this.checkid

                axios.post('http://localhost.com/checkin/post/getCheckinInvoice', { id })
                    .then(this.getInvoice)
                    .catch(e => { })
            }
        },
        mounted() {
            this.getDataApi()
            this.getInvoiceApi()
        }
    }
</script>
