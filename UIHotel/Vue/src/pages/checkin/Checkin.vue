<template>
    <v-app id="inspire">
        <v-card class="mb-4">
            <v-card-title primary-title>
                <div>
                    <h2 class="card-title mb-0">Registration Info</h2>
                </div>
            </v-card-title>
            <div class="card-block">
                <v-form v-model="registration.valid"  ref="form_registration" lazy-validation>
                    <v-layout row>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">Booking Number</v-subheader>
                        </v-flex>
                        <v-flex md4>
                            <v-text-field type="text" readonly disabled v-model="registration.book_no"></v-text-field>
                        </v-flex>
                        <v-flex md2>
                            <v-btn fab dark icon small color="primary">
                                <v-icon dark>search</v-icon>
                            </v-btn>
                        </v-flex>
                    </v-layout>

                    <v-layout row>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">Deposit</v-subheader>
                        </v-flex>
                        <v-flex md3>
                            <v-text-field type="number" prefix="Rp" v-model="registration.deposit"></v-text-field>
                        </v-flex>
                    </v-layout>

                    <v-layout row>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">Arrival Date</v-subheader>
                        </v-flex>
                        <v-flex md4>
                            <v-dialog persistent
                                      v-model="modal1"
                                      lazy
                                      full-width
                                      width="290px">
                                <v-text-field slot="activator"
                                              v-model="registration.arr_date"
                                              prepend-icon="event"
                                              readonly></v-text-field>
                                <v-date-picker v-model="registration.arr_date" scrollable actions>
                                    <template slot-scope="{ save, cancel }">
                                        <v-card-actions>
                                            <v-spacer></v-spacer>
                                            <v-btn flat color="primary" @click="cancel">Cancel</v-btn>
                                            <v-btn flat color="primary" @click="save">OK</v-btn>
                                        </v-card-actions>
                                    </template>
                                </v-date-picker>
                            </v-dialog>
                        </v-flex>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">Departure Date</v-subheader>
                        </v-flex>
                        <v-flex md4>
                            <v-dialog persistent
                                      v-model="modal2"
                                      lazy
                                      full-width
                                      width="290px">
                                <v-text-field slot="activator"
                                              v-model="registration.dep_date"
                                              prepend-icon="event"
                                              readonly></v-text-field>
                                <v-date-picker v-model="registration.dep_date" scrollable actions>
                                    <template slot-scope="{ save, cancel }">
                                        <v-card-actions>
                                            <v-spacer></v-spacer>
                                            <v-btn flat color="primary" @click="cancel">Cancel</v-btn>
                                            <v-btn flat color="primary" @click="save">OK</v-btn>
                                        </v-card-actions>
                                    </template>
                                </v-date-picker>
                            </v-dialog>
                        </v-flex>
                    </v-layout>

                    <v-layout row>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">Jumlah Adult</v-subheader>
                        </v-flex>
                        <v-flex md4>
                            <v-text-field type="number" v-model="registration.adl_count"></v-text-field>
                        </v-flex>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">Jumlah Child</v-subheader>
                        </v-flex>
                        <v-flex md4>
                            <v-text-field type="number" v-model="registration.chl_count"></v-text-field>
                        </v-flex>
                    </v-layout>
                </v-form>
            </div>
            <v-card-title primary-title>
                <div>
                    <h2 class="card-title mb-0">Room Select</h2>
                </div>
            </v-card-title>
            <div class="card-block">
                <v-form v-model="room.valid" ref="form_room" lazy-validation>
                    <v-layout row>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">Select Room</v-subheader>
                        </v-flex>
                        <v-flex md6>
                            <v-text-field type="text" readonly disabled :rules="rules.room_num" v-model="room.room_number"></v-text-field>
                        </v-flex>
                        <v-flex md2>
                            <v-btn fab dark icon small color="primary" @click.stop="selectRoom">
                                <v-icon dark>search</v-icon>
                            </v-btn>
                        </v-flex>
                    </v-layout>

                    <v-layout row>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">Note</v-subheader>
                        </v-flex>
                        <v-flex md6>
                            <v-text-field type="text" multi-line :rows="3" v-model="room.note"></v-text-field>
                        </v-flex>
                    </v-layout>
                </v-form>
            </div>
            <v-card-title primary-title>
                <div>
                    <h2 class="card-title mb-0">Guest Info</h2>
                </div>
            </v-card-title>
            <div class="card-block">
                <v-form v-model="guest.valid" ref="form_guest" lazy-validation>
                    <v-layout row>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">ID Number*</v-subheader>
                        </v-flex>
                        <v-flex md6>
                            <v-text-field type="text"
                                          label="Enter ID Number"
                                          single-line
                                          :rules="rules.id_number"
                                          v-model="guest.id_number"></v-text-field>
                        </v-flex>
                        <v-flex md2>
                            <v-btn fab dark icon small color="primary">
                                <v-icon dark>search</v-icon>
                            </v-btn>
                        </v-flex>
                    </v-layout>
                    <v-layout row>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">Name*</v-subheader>
                        </v-flex>
                        <v-flex md8>
                            <v-text-field type="text"
                                          label="Enter user name"
                                          single-line
                                          :rules="rules.name"
                                          v-model="guest.name"></v-text-field>
                        </v-flex>
                    </v-layout>
                    <v-layout row>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">Guest Type</v-subheader>
                        </v-flex>
                        <v-flex md6>
                            <v-radio-group row v-model="guest.type">
                                <v-radio label="Regular" value="Regular"></v-radio>
                                <v-radio label="VIP" value="VIP"></v-radio>
                            </v-radio-group>
                        </v-flex>
                    </v-layout>
                    <v-layout row>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">Birth Day</v-subheader>
                        </v-flex>
                        <v-flex md4>
                            <v-text-field type="text" label="Birth Place" v-model="guest.birth_place"></v-text-field>
                        </v-flex>
                        <v-flex md4>
                            <v-dialog persistent
                                      v-model="modal3"
                                      lazy
                                      full-width
                                      width="290px">
                                <v-text-field slot="activator"
                                              v-model="guest.birth_day"
                                              prepend-icon="event"
                                              readonly></v-text-field>
                                <v-date-picker v-model="guest.birth_day" :allowed-dates="allowedDates" scrollable actions>
                                    <template slot-scope="{ save, cancel }">
                                        <v-card-actions>
                                            <v-spacer></v-spacer>
                                            <v-btn flat color="primary" @click="cancel">Cancel</v-btn>
                                            <v-btn flat color="primary" @click="save">OK</v-btn>
                                        </v-card-actions>
                                    </template>
                                </v-date-picker>
                            </v-dialog>
                        </v-flex>
                    </v-layout>
                    <v-layout row>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">Address</v-subheader>
                        </v-flex>
                        <v-flex md6>
                            <v-text-field type="text" multi-line :rows="2" v-model="guest.address.note"></v-text-field>
                        </v-flex>
                    </v-layout>
                    <v-layout row>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">State</v-subheader>
                        </v-flex>
                        <v-flex md4>
                            <v-select v-bind:items="country_list"
                                      v-model="guest.address.state"
                                      label="Select Country"
                                      single-line
                                      auto
                                      autocomplete
                                      prepend-icon="map"></v-select>
                        </v-flex>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">Province</v-subheader>
                        </v-flex>
                        <v-flex md4>
                            <v-text-field type="text" v-model="guest.address.province"></v-text-field>
                        </v-flex>
                    </v-layout>
                    <v-layout row>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">City</v-subheader>
                        </v-flex>
                        <v-flex md4>
                            <v-text-field type="text" v-model="guest.address.city"></v-text-field>
                        </v-flex>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">Pos Code</v-subheader>
                        </v-flex>
                        <v-flex md4>
                            <v-text-field type="text" v-model="guest.address.postcode"></v-text-field>
                        </v-flex>
                    </v-layout>
                    <v-layout row>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">Email</v-subheader>
                        </v-flex>
                        <v-flex md6>
                            <v-text-field type="text" v-model="guest.email"></v-text-field>
                        </v-flex>
                    </v-layout>
                    <v-layout row>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">Phone Number 1*</v-subheader>
                        </v-flex>
                        <v-flex md4>
                            <v-text-field type="text" prepend-icon="phone" :rules="rules.phone_num" v-model="guest.phone.phone1"></v-text-field>
                        </v-flex>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">Phone Number 2</v-subheader>
                        </v-flex>
                        <v-flex md4>
                            <v-text-field type="text" prepend-icon="phone" v-model="guest.phone.phone2"></v-text-field>
                        </v-flex>
                    </v-layout>
                    <v-layout row>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">Photo Document*</v-subheader>
                        </v-flex>
                        <v-flex md4>
                            <v-text-field type="text" readonly disabled :rules="rules.photo_doc" v-model="guest.photo_doc"></v-text-field>
                            <v-btn fab dark icon small color="primary" @click.stop="uploadDoc">
                                <v-icon dark>backup</v-icon>
                            </v-btn>
                        </v-flex>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">Photo Guest</v-subheader>
                        </v-flex>
                        <v-flex md4>
                            <v-text-field type="text" readonly disabled v-model="guest.photo_guest"></v-text-field>
                            <v-btn fab dark icon small color="primary" @click.stop="uploadPhoto">
                                <v-icon dark>backup</v-icon>
                            </v-btn>
                        </v-flex>
                    </v-layout>
                </v-form>

                <v-layout row class="mb-3">
                    <v-flex md6>
                        <v-btn color="error">Cancel</v-btn>
                        <v-btn color="success" dark @click.stop="checkin">Checkin <v-icon dark right>check_circle</v-icon></v-btn>
                    </v-flex>
                </v-layout>
            </div>
        </v-card>
        <book-dialog v-model="dialog_book"></book-dialog>
        <room-dialog v-model="dialog_room"></room-dialog>
        <guest-dialog v-model="dialog_guest"></guest-dialog>
    </v-app>
</template>
<script>
    import axios from 'axios'
    import moment from 'moment'
    import country from '../../components/CountryList'
    import DialogBook from '../../components/dialog/BookDialog'
    import DialogRoom from '../../components/dialog/RoomDialog'
    import DialogGuest from '../../components/dialog/GuestDialog'

    export default {
        components: {
            'book-dialog': DialogBook,
            'room-dialog': DialogRoom,
            'guest-dialog': DialogGuest,
        },
        data() {
            return {
                allowedDates: {
                    min: null,
                    max: null
                },
                modal1: false,
                modal2: false,
                modal3: false,
                dialog_book: { show: false },
                dialog_room: { show: false },
                dialog_guest: { show: false },
                registration : {
                    valid: false,
                    book_no: "",
                    deposit: 0,
                    arr_date: null,
                    dep_date: null,
                    adl_count: 1,
                    chl_count: 0
                },
                room: {
                    valid: false,
                    room_id: null,
                    room_number: null,
                    note: null
                },
                guest: {
                    valid: false,
                    id_number: null,
                    name: null,
                    type: 'Regular',
                    birth_place: null,
                    birth_day: null,
                    address: {
                        state: null,
                        province: null,
                        city: null,
                        postcode: null,
                        note: null
                    },
                    email: null,
                    phone: {
                        phone1: null,
                        phone2: null
                    },
                    photo_doc: null,
                    photo_guest: null
                },
                rules: {
                    id_number: [
                        (v) => !!v || 'ID Number is required'
                    ],
                    name: [
                        (v) => !!v || 'Name is required'
                    ],
                    phone_num: [
                        (v) => !!v || 'Phone Number is required'
                    ],
                    photo_doc: [
                        (v) => !!v || 'Document is required'
                    ],
                    room_num: [
                        (v) => !!v || 'Room Number is required'
                    ],
                }
            }
        },
        props: {
            min_deposit: { type: Number, default: 50000 }
        },
        computed: {
            country_list() {
                return country.country_list
            }
        },
        watch: {
            'registration.deposit': {
                handler() {
                    if (this.registration.deposit < this.min_deposit)
                        this.registration.deposit = this.min_deposit

                    console.log(this.registration.deposit)
                }
            },
            dialog_room: {
                handler() {
                    if ("room" in this.dialog_room) {
                        var room = this.dialog_room.room
                        var roomNumber = ""

                        if ("Id" in room) {
                            this.room.room_id = room.Id
                        }

                        if ("RoomCategory" in room) {
                            roomNumber += room.RoomCategory + ": "
                        }

                        if ("RoomNumber" in room) {
                            roomNumber += room.RoomNumber + ", Floor "
                        }

                        if ("RoomFloor" in room) {
                            roomNumber += room.RoomFloor
                        }

                        this.room.room_number = roomNumber
                    }
                },
                deep: true
            }
        },
        methods: {
            selectRoom() {
                this.dialog_room.show = !this.dialog_room.show
            },
            selectBook() {
                this.dialog_book.show = !this.dialog_book.show
            },
            checkinData(response) {
                var data = response.data

                console.log(data)
            },
            checkin() {
                this.$refs.form_registration.validate()
                this.$refs.form_room.validate()
                this.$refs.form_guest.validate()

                const registration = this.registration
                const room = this.room
                const guest = this.guest
                const data = { registration, room, guest }

                console.log(data)

                if (registration.valid && room.valid && guest.valid) {
                    axios.post('http://localhost.com/checkin/post/postCheckin', data)
                        .then(this.checkinData)
                        .catch(e => { })
                }
            },
            uploadDoc() {
                window.CS.getObject("CheckinModel", "Return").then(e => this.guest.photo_doc = e).catch(e => { });
            },
            uploadPhoto() {
                window.CS.getObject("CheckinModel", "Return").then(e => this.guest.photo_guest = e).catch(e => { });
            }
        },
        mounted() {
            var momen = moment()

            this.registration.deposit = this.min_deposit
            this.registration.arr_date = momen.format('YYYY-MM-DD')
            this.registration.dep_date = momen.add(1, 'd').format('YYYY-MM-DD')
            this.allowedDates.max = moment().subtract(18, "y").toISOString().substr(0, 10)
            this.guest.birth_day = moment().subtract(18, "y").format('YYYY-MM-DD')
        }
    }
</script>
