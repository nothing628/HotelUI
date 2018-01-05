<template>
    <v-app id="inspire">
        <v-card>
            <v-card-title>
                <h2 class="card-title mb-0">Room Detail</h2>
            </v-card-title>
            <v-card-title class="card-block">
                <v-container fluid grid-list-md>
                    <v-layout row>
                        <v-flex md3>
                            <img class="img-fluid" src="http://localhost.com/images/room.jpg" />
                        </v-flex>
                        <v-flex md9>
                            <v-form>
                                <v-layout row>
                                    <v-flex md3>
                                        <v-subheader class="text--lighten-1">Room Type</v-subheader>
                                    </v-flex>
                                    <v-flex md4>
                                        <v-text-field readonly v-model="RoomType"></v-text-field>
                                    </v-flex>
                                </v-layout>

                                <v-layout row>
                                    <v-flex md3>
                                        <v-subheader class="text--lighten-1">Room Number</v-subheader>
                                    </v-flex>
                                    <v-flex md4>
                                        <v-text-field readonly v-model="RoomNumber"></v-text-field>
                                    </v-flex>
                                </v-layout>

                                <v-layout row>
                                    <v-flex md3>
                                        <v-subheader class="text--lighten-1">Room Status</v-subheader>
                                    </v-flex>
                                    <v-flex md4>
                                        <v-text-field readonly v-model="RoomStatus"></v-text-field>
                                    </v-flex>
                                </v-layout>

                                <v-layout row>
                                    <v-flex md3>
                                        <v-subheader class="text--lighten-1">Floor Number</v-subheader>
                                    </v-flex>
                                    <v-flex md4>
                                        <v-text-field readonly v-model="RoomFloor"></v-text-field>
                                    </v-flex>
                                </v-layout>

                                <v-layout row>
                                    <v-flex md10 offset-md3>
                                        <a :href="link.Href" v-if="link.Name != 'Detail'" class="white--text btn waves-effect" v-for="link in RoomLinks" :style="getStyle(link.Color)" data-ripple="true">
                                            <div class="btn__content">
                                                {{ link.Name }}
                                                <i :class="link.Icon" class="icon icon--right theme--dark"></i>
                                            </div>
                                        </a>

                                        <v-btn dark color="grey darken-1" href="http://localhost.com/room/get/index">
                                            Back <v-icon dark right>remove_circle</v-icon>
                                        </v-btn>
                                    </v-flex>
                                </v-layout>
                            </v-form>
                        </v-flex>
                    </v-layout>
                </v-container>
            </v-card-title>
        </v-card>
    </v-app>
</template>
<script>
    import axios from 'axios'
    export default {
        data() {
            return {
                RoomLinks: [],
                RoomNumber: null,
                RoomFloor: null,
                RoomType: null,
                RoomStatus: null,
            }
        },
        props: {
            roomId: { type: String, required: true }
        },
        methods: {
            getDataApi() {
                axios.post('http://localhost.com/room/post/getRoomDetail', { roomId: this.roomId })
                    .then(this.getData)
                    .catch(x => { });
            },
            getData(response) {
                var data = response.data

                if (data.success) {
                    var room = data.data

                    this.RoomType = room.RoomCategory
                    this.RoomNumber = room.RoomNumber
                    this.RoomFloor = room.RoomFloor
                    this.RoomStatus = room.Status
                    this.RoomLinks = []

                    room.RoomLinks.forEach(x => this.RoomLinks.push(x))
                }
            },
            getStyle(color) {
                var hColor = '#' + color

                return {'background-color': hColor }
            }
        },
        mounted() {
            this.getDataApi()
        }
    }
</script>
