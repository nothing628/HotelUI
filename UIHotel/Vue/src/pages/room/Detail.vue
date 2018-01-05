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
                                        <v-text-field readonly v-model="roomType"></v-text-field>
                                    </v-flex>
                                </v-layout>

                                <v-layout row>
                                    <v-flex md3>
                                        <v-subheader class="text--lighten-1">Room Number</v-subheader>
                                    </v-flex>
                                    <v-flex md4>
                                        <v-text-field readonly v-model="roomNumber"></v-text-field>
                                    </v-flex>
                                </v-layout>

                                <v-layout row>
                                    <v-flex md3>
                                        <v-subheader class="text--lighten-1">Room Status</v-subheader>
                                    </v-flex>
                                    <v-flex md4>
                                        <v-text-field readonly v-model="roomStatus"></v-text-field>
                                    </v-flex>
                                </v-layout>

                                <v-layout row>
                                    <v-flex md3>
                                        <v-subheader class="text--lighten-1">Floor Number</v-subheader>
                                    </v-flex>
                                    <v-flex md4>
                                        <v-text-field readonly v-model="roomFloor"></v-text-field>
                                    </v-flex>
                                </v-layout>

                                <v-layout row>
                                    <v-flex md10 offset-md3>
                                        <a class="white--text btn waves-effect" v-if="link.Name != 'Detail'" v-for="(link,k) in roomLinks" :style="styleColor(link.Color)" :href="link.Href">
                                            <div class="btn__content">
                                                {{ link.Name }}
                                                <i :class="link.Icon" class="icon icon--right theme--dark"></i>
                                            </div>
                                        </a>
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
                roomNumber: null,
                roomFloor: null,
                roomStatus: null,
                roomType: null,
                roomLinks: [],
            }
        },
        props: {
            roomId: { type: String, required: true },
        },
        methods: {
            styleColor(color) {
                var hexColor = '#' + color

                return { 'background-color': hexColor }
            },
            getApiData(response) {
                var data = response.data

                if (data.success) {
                    this.roomType = data.data.RoomCategory
                    this.roomFloor = data.data.RoomFloor
                    this.roomStatus = data.data.Status
                    this.roomNumber = data.data.RoomNumber
                    this.roomLinks = []

                    data.data.RoomLinks.forEach(x => this.roomLinks.push(x))
                    console.log(data.data)
                }
            },
            getApi() {
                axios.post('http://localhost.com/room/post/getRoomDetail', { id: this.roomId }).then(this.getApiData).catch(e => { })
            }
        },
        mounted() {
            this.getApi()
        }
    }
</script>
