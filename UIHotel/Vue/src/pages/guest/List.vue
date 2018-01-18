<template>
    <v-card>
        <v-card-title>
            <h2 class="card-title mb-0">Guest List</h2>
        </v-card-title>

        <v-container fluid grid-list-md>
            <v-layout row>
                <v-flex lg3 md3 sm12 xs12>
                    <v-select v-model="filter" v-bind:items="filters" single-line auto></v-select>
                </v-flex>
                <v-flex lg9 md9 sm12 xs12>
                    <v-text-field placeholder="Search" v-model="search"></v-text-field>
                </v-flex>
            </v-layout>
            <v-layout row>
                <v-flex lg12>
                    <v-data-table v-bind:headers="tableData.headers"
                                  v-bind:items="tableData.items"
                                  v-bind:pagination.sync="tableData.pagination"
                                  v-bind:total-items="tableData.totalItems"
                                  v-bind:loading="tableData.loading"
                                  class="elevation-1">
                        <template slot="items" slot-scope="props">
                            <tr>
                                <td>{{ props.item.IdNumber }}</td>
                                <td>{{ props.item.Fullname }}</td>
                                <td>{{ props.item.Phone1 }}/{{ props.item.Phone2 }}</td>
                                <td>{{ props.item.Email }}</td>
                                <td>{{ props.item.FullAddress }}</td>
                                <td></td>
                                <td>
                                    <v-btn color="success" :href="props.item.DetailLink">Detail</v-btn>
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

    export default {
        data() {
            return {
                filter: 'All',
                filters: ['All', 'Checkin', 'Not Checkin'],
                search: '',
                tableData: {
                    totalItems: 0,
                    loading: false,
                    pagination: {},
                    headers: [
                        { text: 'ID', sortable: false, align: 'left', value: 'IdNumber' },
                        { text: 'Fullname', sortable: false, align: 'left', value: 'Fullname' },
                        { text: 'Phone', sortable: false, align: 'left' },
                        { text: 'Email', sortable: false, align: 'left' },
                        { text: 'Address', sortable: false, align: 'left' },
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
                    console.log(data.data)
                }

                this.tableData.loading = false
            },
            getDataApi() {
                const { page, rowsPerPage } = this.tableData.pagination
                const search = this.search
                const filter = this.filter
                this.tableData.loading = true

                axios.post('http://localhost.com/guest/post/getGuestList', { page, rowsPerPage, search, filter })
                    .then(this.getData)
                    .catch(e => { })
            }
        },
        mounted() {
            this.getDataApi()
        }
    }
</script>
