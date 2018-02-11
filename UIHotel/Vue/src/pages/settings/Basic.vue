<template>
    <v-card class="mb-4">
        <v-card-title primary-title>
            <h2 class="card-title">General Setting</h2>
        </v-card-title>

        <v-container fluid grid-list-md>
            <v-layout row>
                <v-flex lg12 md12 sm12 xs12>
                    <v-form v-model="valid" ref="form" lazy-validation>
                        <v-layout row>
                            <v-flex md3>
                                <v-subheader>Hotel Name</v-subheader>
                            </v-flex>
                            <v-flex md6>
                                <v-text-field hint="Choose your hotel name" persistent-hint></v-text-field>
                            </v-flex>
                        </v-layout>

                        <v-layout row>
                            <v-flex md3>
                                <v-subheader>Hotel Address</v-subheader>
                            </v-flex>
                            <v-flex md9>
                                <v-text-field hint="Fill with your hotel address" persistent-hint multi-line :rows="2"></v-text-field>
                            </v-flex>
                        </v-layout>

                        <v-layout row>
                            <v-flex md3>
                                <v-subheader>Hotel Logo</v-subheader>
                            </v-flex>
                            <v-flex md3>
                                <img class="img-fluid img-thumbnail rounded" src="http://localhost.com/images/profile-pic.jpg" />
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
                                        :return-value.sync="checkinTime">
                                    <v-text-field slot="activator"
                                                  v-model="checkinTime"
                                                  prepend-icon="access_time"
                                                  readonly></v-text-field>
                                    <v-time-picker v-model="checkinTime" @change="$refs.menu.save(checkinTime)"></v-time-picker>
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
                                        :return-value.sync="checkoutTime">
                                    <v-text-field slot="activator"
                                                  v-model="checkoutTime"
                                                  prepend-icon="access_time"
                                                  readonly></v-text-field>
                                    <v-time-picker v-model="checkoutTime" @change="$refs.menu4.save(checkoutTime)"></v-time-picker>
                                </v-menu>
                            </v-flex>
                        </v-layout>
                        <v-layout row>
                            <v-flex md3>
                                <v-subheader>Late Checkout Charge</v-subheader>
                            </v-flex>
                            <v-flex md3>
                                <v-text-field prefix="Rp" suffix="Per Hours"></v-text-field>
                            </v-flex>
                        </v-layout>
                        <v-layout row>
                            <v-flex md3>
                                <v-subheader>Minimum Deposit</v-subheader>
                            </v-flex>
                            <v-flex md3>
                                <v-text-field prefix="Rp"></v-text-field>
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
    </v-card>
</template>
<script>
    export default {
        data() {
            return {
                valid: true,
                checkoutTime: null,
                checkinTime: null,
                menu2: false,
                menu1: false,
            }
        },
        methods: {
            getDataApi() {
                //
            },
            getData(response) {
                var data = response.data

                if (data.success) {
                    //
                }
            },
            saveDataApi() {
                //
            },
            saveData(response) {
                var data = response.data

                if (data.success) {
                    //
                }
            },
            uploadPhotoCallback(e) {
                var retObj = JSON.parse(e)

                console.log(retObj)
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
