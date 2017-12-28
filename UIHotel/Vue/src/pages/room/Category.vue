<template>
    <v-app id="inspire">
        <v-card>
            <v-card-title>
                <v-btn color="primary" dark @click.stop="newCategory">Add Category</v-btn>
                <v-spacer></v-spacer>
                <v-text-field append-icon="search"
                              label="Search"
                              single-line
                              hide-details
                              v-model="tableData.search"></v-text-field>
            </v-card-title>
            <v-data-table v-bind:headers="tableData.headers"
                          v-bind:items="tableData.items"
                          v-bind:search="tableData.search"
                          v-bind:pagination.sync="tableData.pagination"
                          v-bind:total-items="tableData.totalItems"
                          v-bind:loading="tableData.loading"
                          class="elevation-1">
                <template slot="items" slot-scope="props">
                    <td>{{ props.item.Category }}</td>
                    <td class="text-xs-right">{{ props.item.Description }}</td>
                    <td class="text-xs-right">
                        <v-btn color="warning">Edit</v-btn>
                        <v-btn color="error"  >Remove</v-btn>
                    </td>
                </template>
                <template slot="pageText" slot-scope="{ pageStart, pageStop }">
                    From {{ pageStart }} to {{ pageStop }}
                </template>
            </v-data-table>
        </v-card>

        <v-snackbar :timeout="1000"
                    :top="true"
                    :color="snackbar.color"
                    v-model="snackbar.show">
            {{ snackbar.text }}
        </v-snackbar>
    </v-app>
</template>

<script>
    import axios from 'axios';

    export default {
        data() {
            return {
                tableData: {
                    search: '',
                    totalItems: 0,
                    loading: true,
                    pagination: {},
                    headers: [
                        { text: 'Category', sortable: false, value: 'Category' },
                        { text: 'Description', sortable: false, value: 'Description' },
                        { text: '', sortable: false },
                    ],
                    items: []
                },
                snackbar: {
                    color: 'success',
                    text: '',
                    show: false,
                }
            }
        },
        watch: {
            'tableData.search': {
                handler() {
                    this.getDataFromApi()
                }
            },
            'tableData.pagination': {
                handler() {
                    this.getDataFromApi();
                },
                deep: true
            }
        },
        methods: {
            newCategory() {
                //
            },
            getData(response) {
                let items = response.data;

                this.tableData.loading = false
                this.tableData.items = items.data
                this.tableData.totalItems = items.total
            },
            getDataFromApi() {
                const { page, rowsPerPage } = this.tableData.pagination
                const search = this.tableData.search
                this.tableData.loading = true

                axios.post('http://localhost.com/room/post/getCategoryData', { page, rowsPerPage, search }).then(this.getData).catch(e => { })
            }
        },
        mounted() {
            this.tableData.pagination.rowsPerPage = 10
            this.getDataFromApi()
        }
    }
</script>
