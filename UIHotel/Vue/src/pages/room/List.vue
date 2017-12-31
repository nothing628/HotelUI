<template>
    <v-app id="inspire">
        <v-card>
            <v-card-title>
                <v-spacer></v-spacer>
                <v-text-field append-icon="search"
                              label="Search room"
                              single-line
                              hide-details
                              v-model="search"></v-text-field>
            </v-card-title>
            <v-expansion-panel expand>
                <v-expansion-panel-content v-for="(category,i) in categories" :key="i">
                    <div slot="header">
                        {{ category.Category }}
                        <div class="right">
                            <span class="badge badge-success">Vacant (1)</span>
                            <span class="badge badge-danger">Occupied (1)</span>
                            <span class="badge bg-orange">Booked (1)</span>
                            <span class="badge bg-blue">Late (1)</span>
                            <span class="badge purple lighten-1">Cleaning (1)</span>
                            <span class="badge grey darken-1">Maintance (1)</span>
                        </div>
                    </div>
                    <v-card>
                        <div class="row room_status">
                            <div class="col-md-2" v-for="(room,j) in category.Rooms" :key="j" >
                                <div class="quick-stats__item" :style="{'background-color': '#' + room.StatusColor }">
                                    <div class="quick-stats__info">
                                        <h2>{{ room.RoomNumber }}</h2>
                                        <small>{{ room.Status }}</small>
                                    </div>

                                    <div class="actions listview__actions">
                                        <div class="dropdown actions__item">
                                            <i class="zmdi zmdi-more-vert" data-toggle="dropdown"></i>
                                            <div class="dropdown-menu dropdown-menu-right">
                                                <a class="dropdown-item" v-for="(link,k) in room.RoomLinks" :href="link.Href">
                                                    <i :class="link.Icon"></i> {{ link.Name }}
                                                </a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </v-card>
                </v-expansion-panel-content>
            </v-expansion-panel>
        </v-card>
</v-app>
</template>

<script>
    import axios from 'axios';

    export default {
        data() {
            return {
                search: "",
                categories: [],
            }
        },
        watch: {
            search: {
                handler() {
                    this.getDataFromApi()
                }
            }
        },
        methods: {
            getData(response) {
                let items = response.data;

                this.categories = items.data
                console.log(items)
            },
            getDataFromApi() {
                let search = this.search

                axios.post('http://localhost.com/room/post/getRoomList', { search }).then(this.getData).catch(e => { })
            },
        },
        mounted() {
            this.getDataFromApi()
        }
    }
</script>
