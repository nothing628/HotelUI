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
                <v-expansion-panel-content v-for="(item,i) in 5" :key="i" v-bind:value="item === 2">
                    <div slot="header">
                        ALL
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
                            <div class="col-md-2" v-for="(room,j) in 5" :key="j" >
                                <div class="quick-stats__item bg-green">
                                    <div class="quick-stats__info">
                                        <h2>201</h2>
                                        <small>Vacant</small>
                                    </div>

                                    <div class="actions listview__actions">
                                        <div class="dropdown actions__item">
                                            <i class="zmdi zmdi-more-vert" data-toggle="dropdown"></i>
                                            <div class="dropdown-menu dropdown-menu-right">
                                                <a class="dropdown-item" href="http://localhost.com/room/get/detail"><i class="zmdi zmdi-apps"></i> Detail</a>
                                                <a class="dropdown-item" href="http://localhost.com/home/get/checkin"><i class="zmdi zmdi-download"></i> Check In</a>
                                                <a class="dropdown-item" href="http://localhost.com/home/get/booking"><i class="zmdi zmdi-bookmark"></i> Booking</a>
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
    export default {
        data() {
            return {
                search: ""
            }
        },
        methods: {
            //
        },
        mounted() {

        }
    }
</script>
