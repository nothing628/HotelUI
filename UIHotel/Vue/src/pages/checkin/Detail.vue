<template>
    <div>
        <v-toolbar color="cyan" dark tabs>
            <v-tabs color="cyan" slot="extension" v-model="tab" grow>
                <v-tabs-slider color="yellow"></v-tabs-slider>
                <v-tab>Detail</v-tab>
                <v-tab>Invoice</v-tab>
            </v-tabs>
        </v-toolbar>
        
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
                                        <v-subheader>{{ checkin.InvoiceId }}</v-subheader>
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
                            <v-flex lg12 md12 sm12 xs12></v-flex>
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
                    Note: null,
                }
            }
        },
        props: {
            checkid: { type: String, required: true }
        },
        computed: {
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

                    console.log(realdata)
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
