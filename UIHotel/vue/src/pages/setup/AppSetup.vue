<template>
    <v-card class="mb-4">
        <v-card-title primary-title>
            <h5>Installation Step 3</h5>
        </v-card-title>
        <v-divider></v-divider>
        <v-card-text style="min-height:300px;">
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
                        <img class="img-fluid img-thumbnail rounded" :src="image_url" @error="imageError" />
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
                                          append-icon="access_time"
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
                                          append-icon="access_time"
                                          readonly></v-text-field>
                            <v-time-picker v-model="CheckoutTime" @change="$refs.menu4.save(CheckoutTime)"></v-time-picker>
                        </v-menu>
                    </v-flex>
                </v-layout>
                <v-layout row>
                    <v-flex md3>
                        <v-subheader>Late Checkout</v-subheader>
                    </v-flex>
                    <v-flex md5>
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
                    <v-flex md3>
                        <v-subheader>Tax Charge</v-subheader>
                    </v-flex>
                    <v-flex md2>
                        <v-text-field suffix="%" type="number" v-model="Tax"></v-text-field>
                    </v-flex>
                </v-layout>
            </v-form>
        </v-card-text>
        <v-divider></v-divider>
        <v-card-actions>
            <v-btn color="red darken-1" dark @click.native="prev">Previous</v-btn>
            <v-btn color="green" dark @click.native="next">Next</v-btn>
        </v-card-actions>
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
                valid: false,
                HotelName: '',
                HotelAddress: '',
                HotelLogo: '',
                CheckoutTime: null,
                CheckinTime: null,
                menu2: false,
                menu1: false,
                Pinalty: 0,
                Deposit: 0,
                Tax: 0,
            }
        },
        computed: {
            image_url() {
                if (this.HotelLogo == '')
                    return 'http://localhost.com/images/empty.png'
                return 'http://localhost.com/Upload/' + this.HotelLogo
            }
        },
        props: {
            nextStep: { type: String, required: true },
            prevStep: { type: String, required: true },
        },
        methods: {
            setData() {
                let data = {
                    Pinalty: this.Pinalty,
                    Deposit: this.Deposit,
                    Tax: this.Tax,
                    CheckinTime: this.CheckinTime,
                    CheckoutTime: this.CheckoutTime,
                    HotelLogo: this.HotelLogo,
                    HotelName: this.HotelName,
                    HotelAddress: this.HotelAddress,
                }

                axios.post('http://localhost.com/setup/api/SaveSettingApp', data).then(this.setDataResponse)
            },
            setDataResponse(response) {
                let data = response.data

                if (data.success) {
                    window.location = this.nextStep
                } else {
                    this.$bus.$emit('alert-show', { text: "Save Failed!", color: 'error', timeout: 6000 })
                }
            },
            getData() {
                axios.post('http://localhost.com/setting/post/GetSettings').then(this.getDataResponse)
            },
            getDataResponse(response) {
                var data = response.data

                if (data.success) {
                    this.Tax = data.TaxPercent
                    this.Deposit = data.Deposit
                    this.Pinalty = data.Pinalty
                    this.CheckinTime = data.CheckinTime
                    this.CheckoutTime = data.CheckoutTime
                    this.HotelName = data.HotelName
                    this.HotelAddress = data.HotelAddress
                    this.HotelLogo = data.HotelLogo
                }
            },
            imageError() {
                this.HotelLogo = ''
            },
            uploadPhoto() {
                window.CS.getObjectParam("CheckinModel", "OpenDialog", "Dialog|*.jpg;*.png;*.jpeg").then(this.uploadPhotoCallback);
            },
            uploadPhotoCallback(e) {
                var retObj = JSON.parse(e)

                this.HotelLogo = retObj.hashname
            },
            next() {
                this.setData()
            },
            prev() {
                window.location = this.prevStep
            }
        },
        mounted() {
            this.getData()
        }
    }
</script>