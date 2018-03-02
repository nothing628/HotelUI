<template>
    <v-card class="mb-4">
        <v-card-title primary-title>
            <h2 class="card-title">General Setting</h2>
        </v-card-title>

        <v-container fluid grid-list-md>
            <v-layout row>
                <v-flex md12>
                    <v-form v-model="valid" ref="form" lazy-validation>
                        <v-layout row>
                            <v-flex md3>
                                <v-subheader>Hotel Name</v-subheader>
                            </v-flex>
                            <v-flex md6>
                                <v-text-field hint="Choose your hotel name" persistent-hint v-model="HotelName"></v-text-field>
                            </v-flex>
                        </v-layout>

                        <v-layout row>
                            <v-flex md3>
                                <v-subheader>Hotel Address</v-subheader>
                            </v-flex>
                            <v-flex md9>
                                <v-text-field hint="Fill with your hotel address" persistent-hint multi-line :rows="2" v-model="HotelAddress"></v-text-field>
                            </v-flex>
                        </v-layout>

                        <v-layout row>
                            <v-flex md3>
                                <v-subheader>Hotel Logo</v-subheader>
                            </v-flex>
                            <v-flex md3>
                                <img class="img-fluid img-thumbnail rounded" :src="image_url" @error="imageError"/>
                                <v-btn color="success" block @click="uploadPhoto">
                                    <span>Upload Logo</span>
                                    <v-icon right>cloud_upload</v-icon>
                                </v-btn>
                            </v-flex>
                        </v-layout>

                        <v-layout row>
                            <v-flex md3>
                                <v-subheader>Time Checkin</v-subheader>
                            </v-flex>
                            <v-flex md3>
                                <v-menu ref="menu"
                                        lazy
                                        :close-on-content-click="false"
                                        v-model="menu2"
                                        transition="scale-transition"
                                        offset-y
                                        full-width
                                        :nudge-right="40"
                                        min-width="290px"
                                        :return-value.sync="CheckinTime">
                                    <v-text-field slot="activator"
                                                  v-model="CheckinTime"
                                                  prepend-icon="access_time"
                                                  readonly></v-text-field>
                                    <v-time-picker v-model="CheckinTime" @change="$refs.menu.save(CheckinTime)"></v-time-picker>
                                </v-menu>
                            </v-flex>
                            <v-flex md3>
                                <v-subheader>Time Checkout</v-subheader>
                            </v-flex>
                            <v-flex md3>
                                <v-menu ref="menu4"
                                        lazy
                                        :close-on-content-click="false"
                                        v-model="menu1"
                                        transition="scale-transition"
                                        offset-y
                                        full-width
                                        :nudge-right="40"
                                        min-width="290px"
                                        :return-value.sync="CheckoutTime">
                                    <v-text-field slot="activator"
                                                  v-model="CheckoutTime"
                                                  prepend-icon="access_time"
                                                  readonly></v-text-field>
                                    <v-time-picker v-model="CheckoutTime" @change="$refs.menu4.save(CheckoutTime)"></v-time-picker>
                                </v-menu>
                            </v-flex>
                        </v-layout>
                        <v-layout row>
                            <v-flex md3>
                                <v-subheader>Late Checkout Charge</v-subheader>
                            </v-flex>
                            <v-flex md3>
                                <v-text-field prefix="Rp" suffix="Per Hours" type="number" v-model="Pinalty"></v-text-field>
                            </v-flex>
                        </v-layout>
                        <v-layout row>
                            <v-flex md3>
                                <v-subheader>Minimum Deposit</v-subheader>
                            </v-flex>
                            <v-flex md3>
                                <v-text-field prefix="Rp" type="number" v-model="Deposit"></v-text-field>
                            </v-flex>
                        </v-layout>

                        <v-btn color="success" @click.stop="saveDataApi">
                            <span>Save Setting</span>
                            <v-icon right>done_all</v-icon>
                        </v-btn>
                    </v-form>
                </v-flex>
            </v-layout>
        </v-container>

        <alert></alert>
    </v-card>
</template>
<script>
    import axios from 'axios'
    import alert from '../../components/Notification.vue'

    export default {
        components: {
            alert
        },
        data() {
            return {
                valid: true,
                CheckoutTime: null,
                CheckinTime: null,
                menu2: false,
                menu1: false,
                Pinalty: 0,
                Deposit: 0,
                HotelName: null,
                HotelAddress: null,
                HotelLogo: '',
            }
        },
        computed: {
            image_url() {
                if (this.HotelLogo == '')
                    return 'http://localhost.com/images/empty.png'
                return 'http://localhost.com/Upload/' + this.HotelLogo
            }
        },
        methods: {
            getDataApi() {
                axios.post('http://localhost.com/setting/post/GetSettings').then(this.getData)
            },
            getData(response) {
                var data = response.data

                if (data.success) {
                    this.Deposit = data.Deposit
                    this.Pinalty = data.Pinalty
                    this.CheckinTime = data.CheckinTime
                    this.CheckoutTime = data.CheckoutTime
                    this.HotelName = data.HotelName
                    this.HotelAddress = data.HotelAddress
                    this.HotelLogo = data.HotelLogo
                }
            },
            saveDataApi() {
                let data = {
                    Pinalty: this.Pinalty,
                    Deposit: this.Deposit,
                    CheckinTime: this.CheckinTime,
                    CheckoutTime: this.CheckoutTime,
                    HotelLogo: this.HotelLogo,
                    HotelName: this.HotelName,
                    HotelAddress: this.HotelAddress,
                }

                axios.post('http://localhost.com/setting/post/SetSettings', data).then(this.saveData)
            },
            saveData(response) {
                let data = response.data

                if (data.success) {
                    this.$bus.$emit('alert-show', { text: data.message, color: 'success', timeout: 6000 })
                } else {
                    this.$bus.$emit('alert-show', { text: 'Error saving setting', color: 'error', timeout: 6000 })
                }
            },
            uploadPhotoCallback(e) {
                var retObj = JSON.parse(e)

                this.HotelLogo = retObj.hashname
            },
            uploadPhoto() {
                window.CS.getObjectParam("CheckinModel", "OpenDialog", "Dialog|*.jpg;*.png;*.jpeg").then(this.uploadPhotoCallback).catch(e => { });
            },
            imageError() {
                this.HotelLogo = ''
            }
        },
        mounted() {
            this.getDataApi()
        }
    }
</script>
