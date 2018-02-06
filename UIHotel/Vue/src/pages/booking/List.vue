<template>
    <v-card>
        <v-card-title>
            <h2 class="card-title mb-0">Booking List</h2>
        </v-card-title>

        <v-container fluid grid-list-md>
            <v-layout row>
                <v-flex lg3 md3 sm12 xs12></v-flex>
                <v-flex lg9 md9 sm12 xs12></v-flex>
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
                            <tr>
                                <td>{{ props.item.Id }}</td>
                                <td>
                                    <v-chip v-for="room in props.item.Rooms" :key="room.Id">
                                        <strong>{{ room.RoomNumber }}</strong>&nbsp;
                                        <span>({{ room.RoomCategory }})</span>
                                    </v-chip>
                                </td>
                                <td>{{ props.item.Guest.Fullname }}</td>
                                <td>{{ props.item.ArrivalDate | dateformat }}</td>
                                <td>{{ props.item.DepartureDate | dateformat }}</td>
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
                tableData: {
                    search: '',
                    totalItems: 0,
                    loading: false,
                    pagination: {},
                    headers: [
                        { text: 'ID Booking', sortable: false, align: 'left' },
                        { text: 'Room Number', sortable: false, align: 'left' },
                        { text: 'Guest', sortable: false, align: 'left' },
                        { text: 'Arrival Date', sortable: false, align: 'left' },
                        { text: 'Departure Date', sortable: false, align: 'left' },
                        { text: '', sortable: false },
                    ],
                    items: []
                }
            }
        },
        filters: {
            dateformat(val) {
                var momen = moment(val)

                return momen.format('DD/MM/YYYY');
            }
        },
        methods: {
            getApi() {
                const { page, rowsPerPage } = this.tableData.pagination
                const search = this.tableData.search
                this.tableData.loading = true

                axios.post('http://localhost.com/checkin/post/getBookingList', { page, rowsPerPage, search })
                    .then(this.getApiData)
                    .catch(() => { })
            },
            getApiData(response) {
                var data = response.data

                if (data.success) {
                    console.log(data)
                    this.tableData.items = []
                    this.tableData.totalItems = data.total

                    data.data.forEach((x) => {
                        this.tableData.items.push(x)
                    })
                }

                this.tableData.loading = false
            }
        },
        mounted() {
            this.getApi()
        }
    }
</script>
