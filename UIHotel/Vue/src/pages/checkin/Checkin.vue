<template>
    <v-app id="inspire">
        <v-card>
            <v-card-title primary-title>
                <div>
                    <h2 class="card-title mb-0">Registration Info</h2>
                </div>
            </v-card-title>
            <div class="card-block">
                <v-form v-model="registration.valid">
                    <v-layout row>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">Booking Number</v-subheader>
                        </v-flex>
                        <v-flex md4>
                            <v-text-field type="text" readonly disabled v-model="registration.book_no"></v-text-field>
                        </v-flex>
                        <v-flex md2>
                            <v-btn fab dark small color="primary">
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
                <v-form v-model="room.valid">
                    <v-layout row>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">Select Room</v-subheader>
                        </v-flex>
                        <v-flex md6>
                            <v-text-field type="text" readonly disabled v-model="room.room_number"></v-text-field>
                        </v-flex>
                        <v-flex md2>
                            <v-btn fab dark small color="primary">
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
                <v-form v-model="guest.valid">
                    <v-layout row>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">ID Number*</v-subheader>
                        </v-flex>
                        <v-flex md6>
                            <v-text-field type="text" v-model="guest.id_number"></v-text-field>
                        </v-flex>
                        <v-flex md2>
                            <v-btn fab dark small color="primary">
                                <v-icon dark>search</v-icon>
                            </v-btn>
                        </v-flex>
                    </v-layout>
                    <v-layout row>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">Name*</v-subheader>
                        </v-flex>
                        <v-flex md8>
                            <v-text-field type="text" v-model="guest.name"></v-text-field>
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
                                <v-date-picker v-model="guest.birth_day" scrollable actions>
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
                            <v-select v-bind:items="['test']"
                                      v-model="guest.address.state"
                                      label="Select"
                                      single-line
                                      auto
                                      prepend-icon="map"
                                      hide-details></v-select>
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
                            <v-text-field type="text" prepend-icon="phone" v-model="guest.phone.phone1"></v-text-field>
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
                            <v-btn fab dark small color="primary">
                                <v-icon dark>backup</v-icon>
                            </v-btn>
                        </v-flex>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">Photo Guest</v-subheader>
                        </v-flex>
                        <v-flex md4>
                            <v-btn fab dark small color="primary">
                                <v-icon dark>backup</v-icon>
                            </v-btn>
                        </v-flex>
                    </v-layout>

                    <v-layout row>
                        <v-flex md6>
                            <v-btn color="error">Cancel</v-btn>
                            <v-btn color="success" dark>Checkin <v-icon dark right>check_circle</v-icon></v-btn>
                        </v-flex>
                    </v-layout>
                </v-form>
            </div>
        </v-card>
    </v-app>
</template>
<script>
    import axios from 'axios'
    import moment from 'moment'

    export default {
        data() {
            return {
                modal1: false,
                modal2: false,
                modal3: false,
                registration : {
                    valid: true,
                    book_no: "",
                    deposit: 50000,
                    arr_date: null,
                    dep_date: null,
                    adl_count: 1,
                    chl_count: 0
                },
                room: {
                    valid: true,
                    room_number: null,
                    note: null
                },
                guest: {
                    valid: true,
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
                }
            }
        },
        watch: {
        },
        mounted() {
            var momen = moment()

            this.registration.arr_date = momen.format('YYYY-MM-DD')
            this.registration.dep_date = momen.add(1, 'd').format('YYYY-MM-DD')
        }
    }
</script>
