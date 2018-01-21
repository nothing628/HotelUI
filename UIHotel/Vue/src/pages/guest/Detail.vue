<template>
    <v-card>
        <v-card-title>
            <h2 class="card-title mb-0">Guest Detail</h2>
        </v-card-title>

        <v-container fluid grid-list-md>
            <v-layout row>
                <v-flex lg3 md3 sm12 xs12>
                    <img class="img-fluid img-thumbnail rounded" :src="PhotoUrl"/>
                </v-flex>
                <v-flex lg9 md9 sm12 xs12>
                    <v-form>
                        <v-layout row>
                            <v-flex xs3>
                                <v-subheader>ID Number</v-subheader>
                            </v-flex>
                            <v-flex xs2>
                                <v-text-field readonly v-model="IdKind"></v-text-field>
                            </v-flex>
                            <v-flex xs4>
                                <v-text-field readonly v-model="ID"></v-text-field>
                            </v-flex>
                        </v-layout>
                        <v-layout row>
                            <v-flex xs3>
                                <v-subheader>Fullname</v-subheader>
                            </v-flex>
                            <v-flex xs6>
                                <v-text-field readonly v-model="Fullname"></v-text-field>
                            </v-flex>
                        </v-layout>
                        <v-layout row>
                            <v-flex xs3>
                                <v-subheader>Email</v-subheader>
                            </v-flex>
                            <v-flex xs6>
                                <v-text-field readonly v-model="Email" append-icon="email"></v-text-field>
                            </v-flex>
                        </v-layout>
                        <v-layout row>
                            <v-flex xs3>
                                <v-subheader>Address</v-subheader>
                            </v-flex>
                            <v-flex xs9>
                                <v-text-field readonly multi-line rows="3" v-model="Address"></v-text-field>
                            </v-flex>
                        </v-layout>
                        <v-layout row>
                            <v-flex xs3></v-flex>
                            <v-flex xs3>
                                <v-text-field readonly label="Province" v-model="Province"></v-text-field>
                            </v-flex>
                            <v-flex xs3>
                                <v-text-field readonly label="City" v-model="City"></v-text-field>
                            </v-flex>
                            <v-flex xs3>
                                <v-text-field readonly label="State" v-model="State"></v-text-field>
                            </v-flex>
                        </v-layout>
                        <v-layout row>
                            <v-flex xs3>
                                <v-subheader>Post Code</v-subheader>
                            </v-flex>
                            <v-flex xs3>
                                <v-text-field readonly v-model="PostCode" append-icon="email"></v-text-field>
                            </v-flex>
                        </v-layout>
                        <v-layout row>
                            <v-flex xs3>
                                <v-subheader>Phone Number</v-subheader>
                            </v-flex>
                            <v-flex xs3>
                                <v-text-field readonly v-model="Phone1" append-icon="phone"></v-text-field>
                            </v-flex>
                            <v-flex xs3>
                                <v-text-field readonly v-model="Phone2" append-icon="phone"></v-text-field>
                            </v-flex>
                        </v-layout>
                        <v-layout row>
                            <v-flex xs12>
                                <div>
                                    <v-btn color="yellow darken-3" href="http://localhost.com/guest/get/list" class="white--text">
                                        <v-icon>chevron_left</v-icon>
                                        <span>Back</span>
                                    </v-btn>
                                    <v-btn color="success">
                                        <span>Edit</span>
                                        <v-icon right>edit</v-icon>
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

    export default {
        data() {
            return {
                ID: null,
                IdKind: null,
                Fullname: null,
                Email: null,
                Phone1: null,
                Phone2: null,
                PostCode: null,
                City: null,
                Province: null,
                State: null,
                Address: null,
                Birthday: null,
                Birthplace: null,
                PhotoUrl: null,
            }
        },
        props: {
            IdNumber: { required: true, type: String }
        },
        methods: {
            getData(response) {
                var data = response.data

                if (data.success) {
                    this.ID = data.data.IdNumber
                    this.IdKind = data.data.IdKind
                    this.Fullname = data.data.Fullname
                    this.Email = data.data.Email
                    this.Phone1 = data.data.Phone1
                    this.Phone2 = data.data.Phone2
                    this.PostCode = data.data.PostCode
                    this.City = data.data.City
                    this.Province = data.data.Province
                    this.State = data.data.State
                    this.Address = data.data.Address
                    this.Birthday = data.data.BirthDay
                    this.PhotoUrl = data.data.PhotoUrl
                }
            },
            getDataApi() {
                axios.post('http://localhost.com/guest/post/getGuestDetail', { id: this.IdNumber })
                    .then(this.getData)
                    .catch(e => { })
            }
        },
        mounted() {
            this.getDataApi()
        }
    }
</script>
