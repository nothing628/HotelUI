<template>
    <v-app id="inspire">
        <v-card>
            <v-card-title>
                <h2 class="card-title mb-0">Room Detail : Big No. 201</h2>
            </v-card-title>
            <v-card-title class="card-block">
                <v-layout row>
                    <v-flex md3>
                        <img class="img-fluid" src="http://localhost.com/images/room.jpg" />
                    </v-flex>
                    <v-flex md9>
                        <v-form>
                            <v-layout row>
                                <v-flex md2>
                                    <v-subheader class="text--lighten-1">Room Type</v-subheader>
                                </v-flex>
                                <v-flex md4>
                                    <v-text-field readonly value="BIG"></v-text-field>
                                </v-flex>
                            </v-layout>

                            <v-layout row>
                                <v-flex md2>
                                    <v-subheader class="text--lighten-1">Room Number</v-subheader>
                                </v-flex>
                                <v-flex md4>
                                    <v-text-field readonly value="201"></v-text-field>
                                </v-flex>
                            </v-layout>

                            <v-layout row>
                                <v-flex md2>
                                    <v-subheader class="text--lighten-1">Room Status</v-subheader>
                                </v-flex>
                                <v-flex md4>
                                    <v-text-field readonly value="Vacant"></v-text-field>
                                </v-flex>
                            </v-layout>

                            <v-layout row>
                                <v-flex md2>
                                    <v-subheader class="text--lighten-1">Floor Number</v-subheader>
                                </v-flex>
                                <v-flex md4>
                                    <v-text-field readonly value="1"></v-text-field>
                                </v-flex>
                            </v-layout>

                            <v-layout row>
                                <v-flex md10 offset-md2>
                                    <v-btn color="green lighten-1" class="white--text" href="http://localhost.com/home/get/checkin">
                                        Checkin
                                        <v-icon right dark>file_download</v-icon>
                                    </v-btn>
                                    <v-btn color="light-blue darken-1" class="white--text" href="http://localhost.com/home/get/booking">
                                        Booking
                                        <v-icon right dark>bookmark</v-icon>
                                    </v-btn>
                                    <v-btn color="deep-orange darken-1" class="white--text" href="http://localhost.com/home/get/checkout">
                                        Checkout
                                        <v-icon right dark>file_upload</v-icon>
                                    </v-btn>
                                </v-flex>
                            </v-layout>
                        </v-form>
                    </v-flex>
                </v-layout>
            </v-card-title>
        </v-card>
    </v-app>
</template>
<script>
    import axios from 'axios'
    export default {
        data() {
            return {
            }
        }
    }
</script>
