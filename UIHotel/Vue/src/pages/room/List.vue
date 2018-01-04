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
                            <span v-for="(c,s) in category.StatusCount" :style="getColorStatus(s)" class="badge mr-1">{{ s }} ({{ c }})</span>
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
                status: [],
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
                this.status = items.status
            },
            getDataFromApi() {
                let search = this.search

                axios.post('http://localhost.com/room/post/getRoomList', { search }).then(this.getData).catch(e => { })
            },
            getColorStatus(status) {
                var stat = this.status.find(x => x.Status == status);

                return {
                    'background-color': '#' + stat.StatusColor
                }
            },
        },
        mounted() {
            this.getDataFromApi()
        }
    }
</script>
