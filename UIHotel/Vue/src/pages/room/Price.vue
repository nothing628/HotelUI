<template>
    <v-container fluid grid-list-md>
        <v-layout row>
            <v-flex md12>
                <v-card>
                    <v-card-title>
                        <h2 class="card-title mb-0">Price Maintain</h2>
                        <v-spacer></v-spacer>
                        <v-select v-bind:items="typeList"
                                  v-model="type"
                                  label="Date Type"
                                  item-value="text"
                                  item-text="text"></v-select>
                    </v-card-title>
                    <v-data-table v-bind:headers="tableData.headers"
                                  v-bind:items="tableData.items"
                                  v-bind:search="tableData.search"
                                  v-bind:pagination.sync="tableData.pagination"
                                  v-bind:total-items="tableData.totalItems"
                                  v-bind:loading="tableData.loading"
                                  v-bind:hide-actions="true"
                                  class="elevation-1">
                        <template slot="items" slot-scope="props">
                            <td>{{ props.item.Category }}</td>
                            <td class="text-xs-right">
                                <v-edit-dialog :return-value.sync="props.item.Price"
                                               lazy>
                                    <div>Rp. {{ props.item.Price }}</div>
                                    <div slot="input" class="mt-3 title">Update Price</div>
                                    <v-text-field slot="input"
                                                  label="Edit"
                                                  v-model="props.item.Price"
                                                  prefix="Rp."
                                                  single-line
                                                  counter></v-text-field>
                                </v-edit-dialog>
                            </td>
                        </template>
                        <template slot="pageText" slot-scope="{ pageStart, pageStop }">
                            From {{ pageStart }} to {{ pageStop }}
                        </template>
                    </v-data-table>
                    <v-layout row>
                        <v-flex md12>
                            <v-btn color="primary" v-if="tableData.items.length > 0" @click="storeDate">
                                <v-icon>done_all</v-icon>
                                <span>Save</span>
                            </v-btn>
                        </v-flex>
                    </v-layout>
                    <v-snackbar :timeout="1000"
                                :top="true"
                                :color="snackbar.color"
                                v-model="snackbar.show">
                        {{ snackbar.text }}
                    </v-snackbar>
                </v-card>
            </v-flex>
        </v-layout>
    </v-container>
</template>
<script>
    import axios from 'axios'
    import colors from 'vuetify/es5/util/colors'

    export default {
        data() {
            return {
                type: null,
                tableData: {
                    search: '',
                    totalItems: 0,
                    loading: false,
                    pagination: {
                        rowsPerPage: -1
                    },
                    headers: [
                        { text: 'ROOM TYPE', sortable: false },
                        { text: 'PRICE', sortable: false },
                    ],
                    items: []
                },
                snackbar: {
                    color: 'success',
                    text: '',
                    show: false,
                },
                colors: {},
            }
        },
        watch: {
            type: {
                handler() {
                    this.getPriceApi()
                }
            }
        },
        computed: {
            typeList() {
                var keys = Object.keys(this.colors)
                var ret = keys.map(x => {
                    return { text: x }
                })

                return ret
            }
        },
        methods: {
            getColorsApi() {
                axios.post('http://localhost.com/room/post/getDayEffect').then(this.getColorsData).catch(e => { })
            },
            getColorsData(response) {
                var data = response.data

                this.colors = data.colors
            },
            getPriceData(response) {
                var data = response.data

                if (data.success) {
                    //process
                    this.tableData.items = data.data
                } else {
                    this.snackbar.color = 'error'
                    this.snackbar.text = data.message
                    this.snackbar.show = true
                }

                this.tableData.loading = false
            },
            getPriceApi() {
                var type = this.type

                this.tableData.items = []
                this.tableData.loading = true

                axios.post('http://localhost.com/room/post/getPrice', { type: type }).then(this.getPriceData)
            },
            storeDate() {
                let items = this.tableData.items

                this.tableData.loading = true
                axios.post('http://localhost.com/room/post/setPrice', items).then(this.storeResponse).catch(e => { })
            },
            storeResponse(response) {
                var data = response.data

                if (data.success) {
                    this.snackbar.color = 'success'
                } else {
                    this.snackbar.color = 'error'
                }

                this.snackbar.text = data.message
                this.snackbar.show = true

                this.getPriceApi()
            },
        },
        mounted() {
            this.getColorsApi()
        }
    }
</script>
