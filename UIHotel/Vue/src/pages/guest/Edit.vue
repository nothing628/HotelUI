<template>
    <v-card class="mb-4">
        <v-card-title primary-title>
            <h2 class="card-title mb-0">Edit Guest</h2>
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
                    <v-form v-model="valid" ref="form" lazy-validation>
                        <v-layout row>
                            <v-flex xs3>
                                <v-subheader>ID Number</v-subheader>
                            </v-flex>
                            <v-flex xs2>
                                <v-text-field v-model="model.IdKind" label="Kind" :rules="Rules.id_number"></v-text-field>
                            </v-flex>
                            <v-flex xs4>
                                <v-text-field v-model="model.IdNumber" label="ID Number" :rules="Rules.id_number"></v-text-field>
                            </v-flex>
                        </v-layout>
                        <v-layout row>
                            <v-flex xs3>
                                <v-subheader>Fullname</v-subheader>
                            </v-flex>
                            <v-flex xs6>
                                <v-text-field v-model="model.Fullname" label="Fullname" :rules="Rules.name"></v-text-field>
                            </v-flex>
                        </v-layout>
                        <v-layout row>
                            <v-flex xs3>
                                <v-subheader>Email</v-subheader>
                            </v-flex>
                            <v-flex xs6>
                                <v-text-field v-model="model.Email" append-icon="email" label="Email"></v-text-field>
                            </v-flex>
                        </v-layout>
                        <v-layout row>
                            <v-flex xs3>
                                <v-subheader>Birth Day</v-subheader>
                            </v-flex>
                            <v-flex xs4>
                                <v-text-field v-model="model.BirthPlace" label="Birth Place"></v-text-field>
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
                                        :return-value.sync="model.BirthDay">
                                    <v-text-field slot="activator"
                                                  v-model="model.BirthDay"
                                                  prepend-icon="event"
                                                  readonly></v-text-field>
                                    <v-date-picker v-model="model.BirthDay" :max="allowBir" no-title scrollable>
                                        <v-btn flat color="primary" @click="$refs.menu3.save(model.BirthDay)">OK</v-btn>
                                    </v-date-picker>
                                </v-menu>
                            </v-flex>
                        </v-layout>
                        <v-layout row>
                            <v-flex xs3>
                                <v-subheader>Guest Type</v-subheader>
                            </v-flex>
                            <v-flex xs6>
                                <v-radio-group row v-model="model.Type">
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
                                <v-text-field rows="3" multi-line label="Address" v-model="model.Address"></v-text-field>
                            </v-flex>
                        </v-layout>
                        <v-layout row>
                            <v-flex xs3></v-flex>
                            <v-flex xs3>
                                <v-text-field label="Province" v-model="model.Province"></v-text-field>
                            </v-flex>
                            <v-flex xs3>
                                <v-text-field label="City" v-model="model.City"></v-text-field>
                            </v-flex>
                            <v-flex xs3>
                                <v-select v-bind:items="country_list"
                                          v-model="model.State"
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
                                <v-text-field v-model="model.PostCode" append-icon="email"></v-text-field>
                            </v-flex>
                        </v-layout>
                        <v-layout row>
                            <v-flex xs3>
                                <v-subheader>Phone Number</v-subheader>
                            </v-flex>
                            <v-flex xs3>
                                <v-text-field v-model="model.Phone1" label="Phone Number 1*" append-icon="phone" :rules="Rules.phone_num"></v-text-field>
                            </v-flex>
                            <v-flex xs3>
                                <v-text-field v-model="model.Phone2" label="Phone Number 2" append-icon="phone"></v-text-field>
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
                                <v-text-field v-model="model.DocName" readonly append-icon="attachment" :rules="Rules.photo_doc"></v-text-field>
                            </v-flex>
                        </v-layout>
                        <v-layout row>
                            <v-flex xs12>
                                <div>
                                    <v-btn color="yellow darken-3" :href="model.DetailLink" class="white--text">
                                        <v-icon>chevron_left</v-icon>
                                        <span>Cancel</span>
                                    </v-btn>
                                    <v-btn color="success" @click.stop="updateDataApi">
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
                valid: true,
                model: {
                    IdNumber: null,
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
                    DetailLink: null,
                },
            }
        },
        props: {
            id: { type: String, required: true }
        },
        computed: {
            allowBir() {
                var max = moment().subtract(18, "y")

                return max.format('YYYY-MM-DD')
            },
            country_list() {
                return country.country_list
            },
            PhotoUrl() {
                if (this.model.PhotoHash)
                    return 'http://localhost.com/Upload/' + this.model.PhotoHash
                else
                    return 'http://localhost.com/images/users.png'
            },
            Rules() {
                return {
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
                }
            }
        },
        methods: {
            getData(response) {
                var data = response.data

                if (data.success) {
                    var guest = data.data
                    var birth = moment(guest.BirthDay)
                    
                    this.model.IdNumber = guest.IdNumber
                    this.model.IdKind = guest.IdKind
                    this.model.Fullname = guest.Fullname
                    this.model.Email = guest.Email
                    this.model.BirthPlace = guest.BirthPlace
                    this.model.BirthDay = birth.format('YYYY-MM-DD')
                    this.model.Type = (guest.IsVIP) ? 'VIP' : 'Regular'
                    this.model.Address = guest.Address
                    this.model.Province = guest.Province
                    this.model.City = guest.City
                    this.model.State = guest.State
                    this.model.PostCode = guest.PostCode
                    this.model.Phone1 = guest.Phone1
                    this.model.Phone2 = guest.Phone2
                    this.model.PhotoHash = guest.PhotoGuest
                    this.model.DocHash = guest.PhotoDoc
                    this.model.DocName = guest.PhotoDoc
                    this.model.DetailLink = guest.DetailLink
                }
            },
            getDataApi() {
                axios.post('http://localhost.com/guest/post/getGuestDetail', { id: this.id })
                    .then(this.getData)
                    .catch(e => { })
            },
            updateData(response) {
                var data = response.data

                if (data.success) {
                    window.location = this.model.DetailLink
                }
            },
            updateDataApi() {
                const model = this.model
                const data = Object.assign({}, model, { Id: this.id })

                if (this.$refs.form.validate()) {
                    axios.post('http://localhost.com/guest/post/udpateGuest', data)
                        .then(this.updateData)
                        .catch(e => { })
                }
            },
            uploadPhotoCallback(e) {
                var retObj = JSON.parse(e)

                this.model.PhotoHash = retObj.hashname
            },
            uploadDocCallback(e) {
                var retObj = JSON.parse(e)

                this.model.DocHash = retObj.hashname
                this.model.DocName = retObj.filename
            },
            uploadDoc() {
                window.CS.getObjectParam("CheckinModel", "OpenDialog", "Dialog|*.pdf").then(this.uploadDocCallback).catch(e => { });
            },
            uploadPhoto() {
                window.CS.getObjectParam("CheckinModel", "OpenDialog", "Dialog|*.jpg;*.png;*.jpeg").then(this.uploadPhotoCallback).catch(e => { });
            }
        },
        mounted() {
            this.getDataApi()
        }
    }
</script>
