<template>
    <v-card class="mb-4">
        <v-card-title primary-title>
            <h2 class="card-title mb-0">New Guest</h2>
        </v-card-title>

        <v-container fluid grid-list-md>
            <v-layout row>
                <v-flex lg3 md3 sm12 xs12>
                    <img class="img-fluid img-thumbnail rounded" :src="PhotoUrl" />
                    <v-btn @click.native="uploadPhoto" color="primary" dark>
                        Upload
                        <v-icon right dark>cloud_upload</v-icon>
                    </v-btn>
                </v-flex>
                <v-flex lg9 md10 sm12 xs12>
                    <v-form>
                        <v-layout row>
                            <v-flex xs3>
                                <v-subheader>ID Number</v-subheader>
                            </v-flex>
                            <v-flex xs2>
                                <v-text-field v-model="IdKind" label="Kind"></v-text-field>
                            </v-flex>
                            <v-flex xs4>
                                <v-text-field v-model="ID" label="ID Number"></v-text-field>
                            </v-flex>
                        </v-layout>
                        <v-layout row>
                            <v-flex xs3>
                                <v-subheader>Fullname</v-subheader>
                            </v-flex>
                            <v-flex xs6>
                                <v-text-field v-model="Fullname" label="Fullname"></v-text-field>
                            </v-flex>
                        </v-layout>
                        <v-layout row>
                            <v-flex xs3>
                                <v-subheader>Email</v-subheader>
                            </v-flex>
                            <v-flex xs6>
                                <v-text-field v-model="Email" append-icon="email" label="Email"></v-text-field>
                            </v-flex>
                        </v-layout>
                        <v-layout row>
                            <v-flex xs3>
                                <v-subheader>Birth Day</v-subheader>
                            </v-flex>
                            <v-flex xs4>
                                <v-text-field v-model="BirthPlace" label="Birth Place"></v-text-field>
                            </v-flex>
                            <v-flex md4>
                                <v-dialog persistent
                                          v-model="modal3"
                                          lazy
                                          full-width
                                          width="290px">
                                    <v-text-field slot="activator"
                                                  v-model="BirthDay"
                                                  prepend-icon="event"
                                                  readonly></v-text-field>
                                    <v-date-picker v-model="BirthDay" :allowed-dates="allowedDates" scrollable actions>
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
                            <v-flex xs3>
                                <v-subheader>Guest Type</v-subheader>
                            </v-flex>
                            <v-flex xs6>
                                <v-radio-group row v-model="Type">
                                    <v-radio label="Regular" value="Regular"></v-radio>
                                    <v-radio label="VIP" value="VIP"></v-radio>
                                </v-radio-group>
                            </v-flex>
                        </v-layout>
                        <v-layout row>
                            <v-flex xs3>
                                <v-subheader>Address</v-subheader>
                            </v-flex>
                            <v-flex xs9>
                                <v-text-field rows="3" multi-line label="Address" v-model="Address"></v-text-field>
                            </v-flex>
                        </v-layout>
                        <v-layout row>
                            <v-flex xs3></v-flex>
                            <v-flex xs3>
                                <v-text-field label="Province" v-model="Province"></v-text-field>
                            </v-flex>
                            <v-flex xs3>
                                <v-text-field label="City" v-model="City"></v-text-field>
                            </v-flex>
                            <v-flex xs3>
                                <v-select v-bind:items="country_list"
                                          v-model="State"
                                          label="State"
                                          single-line
                                          auto
                                          autocomplete
                                          append-icon="map"></v-select>
                            </v-flex>
                        </v-layout>
                        <v-layout row>
                            <v-flex xs3>
                                <v-subheader>Post Code</v-subheader>
                            </v-flex>
                            <v-flex xs3>
                                <v-text-field v-model="PostCode" append-icon="email"></v-text-field>
                            </v-flex>
                        </v-layout>
                        <v-layout row>
                            <v-flex xs3>
                                <v-subheader>Phone Number</v-subheader>
                            </v-flex>
                            <v-flex xs3>
                                <v-text-field v-model="Phone1" label="Phone Number 1*" append-icon="phone"></v-text-field>
                            </v-flex>
                            <v-flex xs3>
                                <v-text-field v-model="Phone2" label="Phone Number 2" append-icon="phone"></v-text-field>
                            </v-flex>
                        </v-layout>
                        <v-layout row>
                            <v-flex xs3>
                                <v-subheader>Guest Document</v-subheader>
                            </v-flex>
                            <v-flex xs2>
                                <v-btn fab dark icon small color="primary" @click.stop="uploadDoc">
                                    <v-icon dark>cloud_upload</v-icon>
                                </v-btn>
                            </v-flex>
                            <v-flex xs6>
                                <v-text-field v-model="DocName" append-icon="attachment"></v-text-field>
                            </v-flex>
                        </v-layout>
                        <v-layout row>
                            <v-flex xs12>
                                <div>
                                    <v-btn color="yellow darken-3" href="http://localhost.com/guest/get/list" class="white--text">
                                        <v-icon>chevron_left</v-icon>
                                        <span>Cancel</span>
                                    </v-btn>
                                    <v-btn color="success">
                                        <span>Save</span>
                                        <v-icon right>done_all</v-icon>
                                    </v-btn>
                                </div>
                            </v-flex>
                        </v-layout>
                    </v-form>
                </v-flex>
            </v-layout>
        </v-container>
    </v-card>
</template>
<script>
    import axios from 'axios'
    import moment from 'moment'
    import country from '../../components/CountryList'
    
    export default {
        data() {
            return {
                modal3: false,
                ID: null,
                IdKind: 'KTP',
                Fullname: null,
                Email: null,
                BirthPlace: null,
                BirthDay: null,
                Type: 'Regular',
                Address: null,
                Province: null,
                City: null,
                State: null,
                PostCode: null,
                Phone1: null,
                Phone2: null,
                PhotoHash: null,
                DocHash: null,
                DocName: null,
            }
        },
        computed: {
            allowedDates() {
                var max = moment().subtract(18, "y").toISOString().substr(0, 10)

                return {
                    max: max,
                    min: null,
                }
            },
            country_list() {
                return country.country_list
            },
            PhotoUrl() {
                if (this.PhotoHash)
                    return 'http://localhost.com/Upload/' + this.PhotoHash
                else
                    return 'http://localhost.com/images/users.png'
            }
        },
        methods: {
            uploadPhotoCallback(e) {
                var retObj = JSON.parse(e)

                this.PhotoHash = retObj.hashname
            },
            uploadDocCallback(e) {
                var retObj = JSON.parse(e)

                this.DocHash = retObj.hashname
                this.DocName = retObj.filename
            },
            uploadDoc() {
                window.CS.getObjectParam("CheckinModel", "OpenDialog", "Dialog|*.pdf").then(this.uploadDocCallback).catch(e => { });
            },
            uploadPhoto() {
                window.CS.getObjectParam("CheckinModel", "OpenDialog", "Dialog|*.jpg;*.png;*.jpeg").then(this.uploadPhotoCallback).catch(e => { });
            }
        },
        mounted() {
            this.BirthDay = this.allowedDates.max
        }
    }
</script>
