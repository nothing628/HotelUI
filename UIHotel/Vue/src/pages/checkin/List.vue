<template>
    <v-card>
        <v-card-title>
            <h2 class="card-title mb-0">Check-in List</h2>
        </v-card-title>

        <v-container fluid grid-list-md>
            <v-layout row>
                <v-flex lg3 md3 sm12 xs12>
                    <v-select v-model="filter" v-bind:items="filters" single-line auto></v-select>
                </v-flex>
                <v-flex lg9 md9 sm12 xs12>
                    <v-text-field placeholder="Search" ></v-text-field>
                </v-flex>
            </v-layout>
            <v-layout row>
                <v-flex lg12>
                    <v-data-table v-bind:headers="tableData.headers"
                                  v-bind:items="tableData.items"
                                  v-bind:search="tableData.search"
                                  v-bind:pagination.sync="tableData.pagination"
                                  v-bind:total-items="tableData.totalItems"
                                  v-bind:loading="tableData.loading"
                                  class="elevation-1">
                        <template slot="items" slot-scope="props">
                            <td>CHK2012012100011</td>
                            <td>Big : 201</td>
                            <td>SSS Name</td>
                            <td>2017-2-2</td>
                            <td>2017-2-3</td>
                            <td>2017-2-2 14:00</td>
                            <td>
                                <button class="btn success">Edit</button>
                                <button class="btn warning">Change Room</button>
                                <button class="btn error">Check Out</button>
                            </td>
                        </template>
                        <template slot="pageText" slot-scope="{ pageStart, pageStop }">
                            From {{ pageStart }} to {{ pageStop }}
                        </template>
                    </v-data-table>
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
                filter: 'All',
                filters: ['All', 'Late Checkout', 'Not Late Checkout'],
                tableData: {
                    search: '',
                    totalItems: 0,
                    loading: false,
                    pagination: {},
                    headers: [
                        { text: 'ID Checkin', sortable: false, value: 'Category' },
                        { text: 'Room Number', sortable: false, value: 'Description' },
                        { text: 'Guest', sortable: false, value: 'Description' },
                        { text: 'Arrival Date', sortable: false, value: 'Description' },
                        { text: 'Departure Date', sortable: false, value: 'Description' },
                        { text: 'Checkin Date', sortable: false, value: 'Description' },
                        { text: '', sortable: false },
                    ],
                    items: []
                }
            }
        },
        methods: {
            getData(response) {
                var data = response.data
            },
            getDataApi() {
                //
            }
        },
        mounted() {
            this.getDataApi()
        }
    }
</script>
