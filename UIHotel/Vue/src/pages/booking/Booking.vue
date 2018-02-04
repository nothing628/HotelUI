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
                            <v-subheader class="text--lighten-1">Booking Type</v-subheader>
                        </v-flex>
                        <v-flex md2>
                            <v-select v-bind:items="booktype"
                                      v-model="selectedbook"
                                      label="Select"
                                      single-line
                                      auto
                                      hide-details></v-select>
                        </v-flex>
                        <v-flex md2 v-show="canShow">
                            <v-subheader class="text--lighten-1">Online Type</v-subheader>
                        </v-flex>
                        <v-flex md3 v-show="canShow">
                            <v-select v-bind:items="onlinetype"
                                      label="Select"
                                      single-line
                                      auto
                                      hide-details></v-select>
                        </v-flex>
                    </v-layout>
                    <v-layout row v-show="canShow">
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">Harga</v-subheader>
                        </v-flex>
                        <v-flex md3>
                            <v-text-field type="number" prefix="Rp"></v-text-field>
                        </v-flex>
                    </v-layout>
                    <v-layout row>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">Deposit</v-subheader>
                        </v-flex>
                        <v-flex md3>
                            <v-text-field type="number" prefix="Rp"></v-text-field>
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
                                    :return-value.sync="date1">
                                <v-text-field slot="activator"
                                              v-model="date1"
                                              prepend-icon="event"
                                              readonly></v-text-field>
                                <v-date-picker v-model="date1" :min="allowArr" no-title scrollable>
                                    <v-btn flat color="primary" @click="$refs.menu1.save(date1)">OK</v-btn>
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
                                    :return-value.sync="date2">
                                <v-text-field slot="activator"
                                              v-model="date2"
                                              prepend-icon="event"
                                              readonly></v-text-field>
                                <v-date-picker v-model="date2" :min="allowArr" no-title scrollable>
                                    <v-btn flat color="primary" @click="$refs.menu2.save(date2)">OK</v-btn>
                                </v-date-picker>
                            </v-menu>
                        </v-flex>
                    </v-layout>
                    <v-layout row>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">Jumlah Adult</v-subheader>
                        </v-flex>
                        <v-flex md4>
                            <v-text-field type="number" value="1"></v-text-field>
                        </v-flex>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">Jumlah Child</v-subheader>
                        </v-flex>
                        <v-flex md4>
                            <v-text-field type="number" value="0"></v-text-field>
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
                            <v-select label="Select"
                                      v-bind:items="booktype"
                                      multiple
                                      chips
                                      max-height="400"></v-select>
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
                            <v-text-field type="text" multi-line :rows="3"></v-text-field>
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
                            <v-text-field type="text"></v-text-field>
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
                            <v-text-field type="text"></v-text-field>
                        </v-flex>
                    </v-layout>
                    <v-layout row>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">Guest Type</v-subheader>
                        </v-flex>
                        <v-flex md6>
                            <v-radio-group row>
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
                            <v-text-field type="text" label="Birth Place"></v-text-field>
                        </v-flex>
                        <v-flex md4>
                            <v-dialog persistent
                                      v-model="modal3"
                                      lazy
                                      full-width
                                      width="290px">
                                <v-text-field slot="activator"
                                              v-model="date3"
                                              prepend-icon="event"
                                              readonly></v-text-field>
                                <v-date-picker v-model="date3" scrollable actions>
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
                            <v-text-field type="text" multi-line :rows="2"></v-text-field>
                        </v-flex>
                    </v-layout>
                    <v-layout row>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">State</v-subheader>
                        </v-flex>
                        <v-flex md4>
                            <v-select v-bind:items="['test']"
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
                            <v-text-field type="text"></v-text-field>
                        </v-flex>
                    </v-layout>
                    <v-layout row>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">City</v-subheader>
                        </v-flex>
                        <v-flex md4>
                            <v-text-field type="text"></v-text-field>
                        </v-flex>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">Pos Code</v-subheader>
                        </v-flex>
                        <v-flex md4>
                            <v-text-field type="text"></v-text-field>
                        </v-flex>
                    </v-layout>
                    <v-layout row>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">Email</v-subheader>
                        </v-flex>
                        <v-flex md6>
                            <v-text-field type="text"></v-text-field>
                        </v-flex>
                    </v-layout>
                    <v-layout row>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">Phone Number 1*</v-subheader>
                        </v-flex>
                        <v-flex md4>
                            <v-text-field type="text" prepend-icon="phone"></v-text-field>
                        </v-flex>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">Phone Number 2</v-subheader>
                        </v-flex>
                        <v-flex md4>
                            <v-text-field type="text" prepend-icon="phone"></v-text-field>
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
    export default {
        data() {
            return {
                booktype: [
                    'WALK-IN',
                    'TELEPHONE',
                    'ONLINE',
                ],
                onlinetype: [
                    'TRAVELOKA',
                    'AGODA'
                ],
                selectedbook: null,
                date1: null,
                date2: null,
                date3: null,
                modal1: false,
                modal2: false,
                modal3: false,
                registration: { valid: true },
                room: { valid: true },
                guest: { valid: true }
            }
        },
        computed: {
            canShow() {
                return this.selectedbook == 'ONLINE'
            },
            allowArr() {
                var min = moment()

                return min.format('YYYY-MM-DD')
            }
        }
    }
</script>
