<template>
    <v-app id="inspire">
        <v-card>
            <v-card-title>
                Room Maintain
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
                    <td class="text-xs-right">{{ props.item.IdCategory }}</td>
                    <td class="text-xs-right">{{ props.item.Status }}</td>
                </template>
                <template slot="pageText" slot-scope="{ pageStart, pageStop }">
                    From {{ pageStart }} to {{ pageStop }}
                </template>
            </v-data-table>
        </v-card>
    </v-app>
</template>

<script>
    import axios from 'axios';

    export default {
        data() {
            return {
                search: '',
                totalItems: 0,
                loading: true,
                pagination: {},
                headers: [
                    {
                        text: 'Room Number',
                        align: 'left',
                        sortable: false,
                        value: 'RoomNumber'
                    },
                    { text: 'Floor', sortable: false, value: 'RoomFloor' },
                    { text: 'Type', sortable: false, value: 'IdCategory' },
                    { text: 'Status', sortable: false, value: 'Status' }
                ],
                items: []
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
            }
        },
        mounted() {
            this.getDataFromApi();
        }
    }
</script>
