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
                <v-flex md12>
                    <v-btn color="teal" dark class="mx-0" href="http://localhost.com/guest/get/create">
                        <v-icon>add</v-icon> Add
                    </v-btn>
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
                                <td class="justify-center layout px-0">
                                    <v-btn icon class="mx-0" :href="props.item.DetailLink">
                                        <v-icon color="primary">visibility</v-icon>
                                    </v-btn>
                                    <v-btn icon class="mx-0" :href="props.item.EditLink">
                                        <v-icon color="teal">edit</v-icon>
                                    </v-btn>
                                    <v-btn icon class="mx-0" @click="confirmDelete(props.item)">
                                        <v-icon color="pink">delete</v-icon>
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

        <v-dialog v-model="dialog" max-width="400">
            <v-card>
                <v-card-title class="headline">Delete Guest?</v-card-title>
                <v-card-text>This will delete report or other information about this guest.</v-card-text>
                <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn color="red darken-1" flat="flat" @click.native="dialog = false"><v-icon dark left>clear</v-icon>Disagree</v-btn>
                    <v-btn color="green darken-1" flat="flat" @click.native="deleteItem"><v-icon dark left>done_all</v-icon>Agree</v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
    </v-card>
</template>
<script>
    import axios from 'axios'

    export default {
        data() {
            return {
                dialog: false,
                selectItem: null,
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
                        { text: 'Actions', sortable: false },
                    ],
                    items: []
                }
            }
        },
        methods: {
            deleteData(response) {
                var data = response.data

                if (data.success) {
                    // Notification
                } else {
                    // Notification
                }
                console.log(data)
                this.getDataApi()
            },
            deleteItem() {
                const guest = this.selectItem

                axios.post('http://localhost.com/guest/post/deleteGuest', guest)
                    .then(this.deleteData)
                    .catch(e => { })

                this.dialog = false
            },
            confirmDelete(item) {
                this.selectItem = item
                this.dialog = true
            },
            getData(response) {
                var data = response.data

                if (data.success) {
                    this.tableData.items = []

                    data.data.forEach(x => this.tableData.items.push(x))
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
