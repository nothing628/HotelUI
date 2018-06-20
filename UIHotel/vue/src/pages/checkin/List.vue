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
                            <tr :class="getClass(props.item)">
                                <td>{{ props.item.IdCheckin }}</td>
                                <td>{{ props.item.RoomCategory }} : {{ props.item.RoomNumber }}</td>
                                <td>{{ props.item.GuestName }}</td>
                                <td>{{ formatDate(props.item.ArrivalDate) }}</td>
                                <td>{{ formatDate(props.item.DepartureDate) }}</td>
                                <td>{{ formatDateTime(props.item.CheckinDate) }}</td>
                                <td>
                                    <v-btn icon class="mx-0" :href="props.item.DetailLink">
                                        <v-icon color="teal">visibility</v-icon>
                                    </v-btn>
                                </td>
                            </tr>
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
    import moment from 'moment'
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
                        { text: 'ID Checkin', sortable: false, align: 'left', value: 'IdCheckin' },
                        { text: 'Room Number', sortable: false, align: 'left', value: 'RoomNumber' },
                        { text: 'Guest', sortable: false, align: 'left', value: 'GuestName' },
                        { text: 'Arrival Date', sortable: false, align: 'left' },
                        { text: 'Departure Date', sortable: false, align: 'left' },
                        { text: 'Checkin Date', sortable: false, align: 'left' },
                        { text: '', sortable: false },
                    ],
                    items: []
                }
            }
        },
        methods: {
            getData(response) {
                var data = response.data

                if (data.success) {
                    this.tableData.items = []

                    data.data.forEach(x => this.tableData.items.push(x))
                }
            },
            getDataApi() {
                axios.post('http://localhost.com/checkin/post/getCheckinList', {})
                    .then(this.getData)
                    .catch(e => { })
            },
            formatDate(strDate) {
                var momen = moment(strDate)
                
                return momen.format("DD-MM-YYYY")
            },
            formatDateTime(strDate) {
                var momen = moment(strDate)

                return momen.format("DD-MM-YYYY")
            },
            getClass(item) {
                if (item.IsCheckout)
                    return ['grey', 'lighten-1']
                if (item.IsLate)
                    return ['red', 'lighten-3']

                return []
            }
        },
        mounted() {
            this.getDataApi()
        }
    }
</script>
