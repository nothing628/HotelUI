<template>
    <v-app id="inspire">
        <v-card class="mb-4">
            <v-card-title primary-title>
                <h2 class="card-title mb-0">Registration Info</h2>
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
                            <v-btn fab dark icon small color="primary" @click.stop="selectBook">
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
                            <v-menu ref="menu1"
                                    lazy
                                    :close-on-content-click="false"
                                    v-model="modal1"
                                    transition="scale-transition"
                                    offset-y
                                    full-width
                                    :nudge-right="40"
                                    min-width="330px"
                                    :return-value.sync="registration.arr_date">
                                <v-text-field slot="activator"
                                              v-model="registration.arr_date"
                                              prepend-icon="event"
                                              readonly></v-text-field>
                                <v-date-picker v-model="registration.arr_date" :min="allowArr" no-title scrollable>
                                    <v-btn flat color="primary" @click="$refs.menu1.save(registration.arr_date)">OK</v-btn>
                                </v-date-picker>
                            </v-menu>
                        </v-flex>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">Departure Date</v-subheader>
                        </v-flex>
                        <v-flex md4>
                            <v-menu ref="menu2"
                                    lazy
                                    :close-on-content-click="false"
                                    v-model="modal2"
                                    transition="scale-transition"
                                    offset-y
                                    full-width
                                    :nudge-right="40"
                                    min-width="330px"
                                    :return-value.sync="registration.dep_date">
                                <v-text-field slot="activator"
                                              v-model="registration.dep_date"
                                              prepend-icon="event"
                                              readonly></v-text-field>
                                <v-date-picker v-model="registration.dep_date" :min="allowArr" no-title scrollable>
                                    <v-btn flat color="primary" @click="$refs.menu2.save(registration.dep_date)">OK</v-btn>
                                </v-date-picker>
                            </v-menu>
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
                <h2 class="card-title mb-0">Room Select</h2>
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
                <h2 class="card-title mb-0">Guest Info</h2>
            </v-card-title>
            <div class="card-block">
                <v-form v-model="guest.valid" ref="form_guest" lazy-validation>
                    <v-layout row>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">ID Number*</v-subheader>
                        </v-flex>
                        <v-flex md2>
                            <v-text-field type="text"
                                          label="Enter ID Kind"
                                          single-line
                                          :rules="rules.id_number"
                                          v-model="guest.id_kind"></v-text-field>
                        </v-flex>
                        <v-flex md1></v-flex>
                        <v-flex md4>
                            <v-text-field type="text"
                                          label="Enter ID Number"
                                          single-line
                                          :rules="rules.id_number"
                                          v-model="guest.id_number"></v-text-field>
                        </v-flex>
                        <v-flex md2>
                            <v-btn fab dark icon small color="primary" v-if="guest.id == null" @click.stop="selectGuest">
                                <v-icon dark>search</v-icon>
                            </v-btn>
                            <v-btn fab dark icon small color="error" v-else @click.stop="clearGuest">
                                <v-icon dark>clear</v-icon>
                            </v-btn>
                        </v-flex>
                    </v-layout>
                    <v-layout row>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">Fullname*</v-subheader>
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
                            <v-menu ref="menu3"
                                    lazy
                                    :close-on-content-click="false"
                                    v-model="modal3"
                                    transition="scale-transition"
                                    offset-y
                                    full-width
                                    :nudge-right="40"
                                    min-width="330px"
                                    :return-value.sync="guest.birth_day">
                                <v-text-field slot="activator"
                                              v-model="guest.birth_day"
                                              prepend-icon="event"
                                              readonly></v-text-field>
                                <v-date-picker v-model="guest.birth_day" :max="allowBir" no-title scrollable>
                                    <v-btn flat color="primary" @click="$refs.menu3.save(guest.birth_day)">OK</v-btn>
                                </v-date-picker>
                            </v-menu>
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
                    <v-flex md3></v-flex>
                    <v-flex md6>
                        <v-btn color="error" href="http://localhost.com/room/get/index">Cancel</v-btn>
                        <v-btn color="success" dark @click.stop="checkin">Checkin <v-icon dark right>check_circle</v-icon></v-btn>
                    </v-flex>
                </v-layout>
            </div>
        </v-card>
        <book-dialog v-model="dialog_book"></book-dialog>
        <room-dialog v-model="dialog_room"></room-dialog>
        <guest-dialog v-model="dialog_guest"></guest-dialog>
        <alert></alert>
    </v-app>
</template>
<script>
    import axios from 'axios'
    import moment from 'moment'
    import country from '../../components/CountryList'
    import DialogBook from '../../components/dialog/BookDialog'
    import DialogRoom from '../../components/dialog/RoomDialog'
    import DialogGuest from '../../components/dialog/GuestDialog'
    import Alert from '../../components/Notification'

    export default {
        components: {
            'book-dialog': DialogBook,
            'room-dialog': DialogRoom,
            'guest-dialog': DialogGuest,
            'alert': Alert,
        },
        data() {
            return {
                modal1: false,
                modal2: false,
                modal3: false,
                dialog_book: {
                    show: false,
                    book: {}
                },
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
                    id: null,
                    id_kind: 'KTP',
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
            min_deposit: { type: Number, default: 50000 },
            bookId: { type: String, default: null },
            roomId: { type: String, default: null },
            roomName: { type: String, default: null }
        },
        filters: {
            dateformat(val) {
                var momen = moment(val)

                return momen.format('YYYY-MM-DD');
            }
        },
        computed: {
            country_list() {
                return country.country_list
            },
            allowBir() {
                var max = moment().subtract(18, "y")

                return max.format('YYYY-MM-DD')
            },
            allowArr() {
                var min = moment()

                return min.format('YYYY-MM-DD')
            }
        },
        watch: {
            'registration.deposit': {
                handler() {
                    if (this.registration.deposit < this.min_deposit)
                        this.registration.deposit = this.min_deposit
                }
            },
            dialog_book: {
                handler() {
                    if ("book" in this.dialog_book) {
                        let book = this.dialog_book.book

                        this.registration.book_no = book.Id
                        this.registration.arr_date = this.$options.filters.dateformat(book.ArriveAt)
                        this.registration.dep_date = this.$options.filters.dateformat(book.DepartureAt)
                        this.registration.adl_count = book.CountAdult
                        this.registration.chl_count = book.CountChild
                        this.dialog_room.book_id = book.Id
                    }
                },
                deep: true,
            },
            dialog_room: {
                handler() {
                    if ("room" in this.dialog_room) {
                        var room = this.dialog_room.room
                        var roomNumber = ""

                        this.room.room_id = room.Id || this.room.room_id

                        if ("RoomCategory" in room)
                            roomNumber += room.RoomCategory + ": "

                        if ("RoomNumber" in room)
                            roomNumber += room.RoomNumber + ", Floor "

                        if ("RoomFloor" in room)
                            roomNumber += room.RoomFloor

                        if (roomNumber != "")
                            this.room.room_number = roomNumber
                    }
                },
                deep: true
            },
            dialog_guest: {
                handler() {
                    if ('guest' in this.dialog_guest) {
                        var guest = this.dialog_guest.guest
                        var birth = moment(guest.BirthDay)

                        this.guest.id = guest.Id
                        this.guest.id_kind = guest.IdKind
                        this.guest.id_number = guest.IdNumber
                        this.guest.name = guest.Fullname
                        this.guest.type = (guest.IsVIP) ? 'VIP' : 'Regular'
                        this.guest.birth_place = guest.BirthPlace
                        this.guest.birth_day = birth.format("YYYY-MM-DD")
                        this.guest.address.state = guest.State
                        this.guest.address.province = guest.Province
                        this.guest.address.city = guest.City
                        this.guest.address.postcode = guest.PostCode
                        this.guest.address.note = guest.Address
                        this.guest.email = guest.Email
                        this.guest.phone.phone1 = guest.Phone1
                        this.guest.phone.phone2 = guest.Phone2
                        this.guest.photo_doc = guest.PhotoDoc
                        this.guest.photo_guest = guest.PhotoGuest
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
            selectGuest() {
                this.dialog_guest.show = !this.dialog_guest.show
            },
            clearGuest() {
                this.guest.id = null
                this.guest.id_kind = 'KTP'
                this.guest.id_number = null
                this.guest.name = null
                this.guest.type = 'Regular'
                this.guest.birth_place = null
                this.guest.birth_day = this.allowBir
                this.guest.address.state = null
                this.guest.address.province = null
                this.guest.address.city = null
                this.guest.address.postcode = null
                this.guest.address.note = null
                this.guest.email = null
                this.guest.phone.phone1 = null
                this.guest.phone.phone2 = null
                this.guest.photo_doc = null
                this.guest.photo_guest = null
            },
            checkinData(response) {
                var data = response.data

                if (data.success) {
                    // redirect here
                    window.location = data.redirect_url
                } else {
                    // show errors
                    this.$bus.$emit('alert-show', { text: data.message, color: 'error' })
                }
            },
            checkin() {
                this.$refs.form_registration.validate()
                this.$refs.form_room.validate()
                this.$refs.form_guest.validate()

                const registration = this.registration
                const room = this.room
                const guest = this.guest
                const data = { registration, room, guest }
                
                if (registration.valid && room.valid && guest.valid) {
                    axios.post('http://localhost.com/checkin/post/postCheckin', data)
                        .then(this.checkinData)
                        .catch(e => { })
                }
            },
            uploadDocCallback(e) {
                var objRet = JSON.parse(e)
                //Big: 201, Floor 1
                this.guest.photo_doc = objRet.hashname
            },
            uploadPhotoCallback(e) {
                var objRet = JSON.parse(e)

                this.guest.photo_guest = objRet.hashname
            },
            uploadDoc() {
                window.CS.getObject("CheckinModel", "OpenDialog").then(this.uploadDocCallback).catch(e => { });
            },
            uploadPhoto() {
                window.CS.getObject("CheckinModel", "OpenDialog").then(this.uploadPhotoCallback).catch(e => { });
            }
        },
        mounted() {
            var momen = moment()

            this.registration.deposit = this.min_deposit
            this.registration.arr_date = momen.format('YYYY-MM-DD')
            this.registration.dep_date = momen.add(1, 'd').format('YYYY-MM-DD')
            this.guest.birth_day = this.allowBir

            this.room.room_id = this.roomId || ""
            this.room.room_number = this.roomName || ""
            this.registration.book_no = this.bookId || ""

            if (this.bookId != "") {
                setTimeout(() => {
                    this.$bus.$emit('refresh-book', { id: this.bookId })
                }, 1000)
            } 
        }
    }
</script>
