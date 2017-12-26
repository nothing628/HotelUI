<template>
    <v-app id="inspire">
        <v-card>
            <v-card-title>
                <v-btn color="primary" dark @click.stop="dialog2 = true">Add Room</v-btn>
                <v-spacer></v-spacer>
                <v-text-field append-icon="search"
                              label="Search"
                              single-line
                              hide-details
                              v-model="search"></v-text-field>
            </v-card-title>
            <v-data-table v-bind:headers="headers"
                          v-bind:items="items"
                          v-bind:search="search"
                          v-bind:pagination.sync="pagination"
                          v-bind:total-items="totalItems"
                          v-bind:loading="loading"
                          class="elevation-1">
                <template slot="items" slot-scope="props">
                    <td>{{ props.item.RoomNumber }}</td>
                    <td class="text-xs-right">{{ props.item.RoomFloor }}</td>
                    <td class="text-xs-right">{{ props.item.RoomCategory }}</td>
                    <td class="text-xs-right">{{ props.item.RoomStatus }}</td>
                    <td class="text-xs-right">
                        <v-btn color="primary">Remove</v-btn>
                    </td>
                </template>
                <template slot="pageText" slot-scope="{ pageStart, pageStop }">
                    From {{ pageStart }} to {{ pageStop }}
                </template>
            </v-data-table>
        </v-card>

        <v-dialog v-model="dialog2" max-width="500px">
            <v-card>
                <v-card-title>Add Room </v-card-title>
                <v-card-text>
                    <v-form v-model="valid">
                        <v-text-field label="Room Number"
                                      :rules="nameRules"
                                      :counter="10"
                                      required></v-text-field>
                        <v-text-field label="Room Floor"
                                      required></v-text-field>
                        <v-select v-bind:items="select"
                                  label="Room Category"
                                  item-value="value"
                                  item-text="text"></v-select>
                    </v-form>
                </v-card-text>
                <v-card-actions>
                    <v-btn color="primary" @click.stop="dialog2=false">Submit</v-btn>
                    <v-btn color="primary" flat @click.stop="dialog2=false">Close</v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
    </v-app>
</template>

<script>
    import axios from 'axios';

    export default {
        data() {
            return {
                search: '',
                dialog2: false,
                totalItems: 0,
                loading: true,
                pagination: {},
                nameRules: [
                    (v) => !!v || 'Name is required',
                    (v) => v && v.length <= 10 || 'Name must be less than 10 characters'
                ],
                headers: [
                    {
                        text: 'Room Number',
                        align: 'left',
                        sortable: false,
                        value: 'RoomNumber'
                    },
                    { text: 'Floor', sortable: false, value: 'RoomFloor' },
                    { text: 'Type', sortable: false, value: 'IdCategory' },
                    { text: 'Status', sortable: false, value: 'Status' },
                    { text: '', sortable: false}
                ],
                items: [],
                select: []
            }
        },
        watch: {
            pagination: {
                handler() {
                    this.getDataFromApi();
                },
                deep: true
            }
        },
        methods: {
            getData(response) {
                let items = response.data;

                this.loading = false
                this.items = items
                this.totalItems = items.length
            },
            getDataFromApi() {
                const { sortBy, descending, page, rowsPerPage } = this.pagination
                this.loading = true

                axios.get('http://localhost.com/room/get/getRoom').then(this.getData).catch(e => { })
            },
            getCategory() {
                axios.get('http://localhost.com/room/get/getCategory').then(this.getCategoryData).catch(e => { })
            },
            getCategoryData(response) {
                let items = response.data;

                this.select = []

                items.forEach((i, index) => {
                    this.select.push({
                        text: i.Category,
                        value: i.Id,
                    })
                })
            }
        },
        mounted() {
            this.getCategory()
            this.getDataFromApi()
        }
    }
</script>
