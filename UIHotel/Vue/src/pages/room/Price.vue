<template>
    <v-app id="inspire">
        <v-container fluid grid-list-md>
            <v-layout row>
                <v-flex md4>
                    <v-card>
                        <v-card-title>
                            <h2 class="card-title mb-0">Calendar</h2>
                        </v-card-title>
                        <v-card-title>
                            <v-date-control @dateclick="dateClicked" :items="items" :colors="colors"></v-date-control>
                        </v-card-title>
                    </v-card>
                </v-flex>
                <v-flex md8>
                    <v-card>
                        <v-card-title>
                            <h2 class="card-title mb-0">Price List</h2>
                        </v-card-title>
                        <v-card-title>
                            <v-data-table v-bind:headers="tableData.headers"
                                          v-bind:items="tableData.items"
                                          v-bind:search="tableData.search"
                                          v-bind:pagination.sync="tableData.pagination"
                                          v-bind:total-items="tableData.totalItems"
                                          v-bind:loading="tableData.loading"
                                          class="elevation-1">
                                <template slot="items" slot-scope="props">
                                    <td>12</td>
                                    <td class="text-xs-right">12000</td>
                                </template>
                                <template slot="pageText" slot-scope="{ pageStart, pageStop }">
                                    From {{ pageStart }} to {{ pageStop }}
                                </template>
                            </v-data-table>
                        </v-card-title>
                    </v-card>
                </v-flex>
            </v-layout>
        </v-container>
        <v-dialog v-model="changeDialog" max-width="500px">
            <v-card>
                <v-card-title>Change Date Type </v-card-title>
                <v-card-text>
                    <v-form>
                        <v-text-field label="Date"
                                      v-model="dateForm.date"
                                      readonly></v-text-field>
                        <v-select v-bind:items="typeList"
                                  v-model="dateForm.type"
                                  label="Date Type"
                                  item-value="text"
                                  item-text="text"></v-select>
                    </v-form>
                </v-card-text>
                <v-card-actions>
                    <v-btn color="primary" @click.stop="store">Submit</v-btn>
                    <v-btn color="primary" flat @click.stop="changeDialog=false">Close</v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
        <v-snackbar :timeout="1000"
                    :top="true"
                    :color="snackbar.color"
                    v-model="snackbar.show">
            {{ snackbar.text }}
        </v-snackbar>
    </v-app>
</template>
<script>
    import axios from 'axios'

    export default {
        data() {
            return {
                changeDialog: false,
                dateForm: {
                    type: null,
                    date: null,
                },
                tableData: {
                    search: '',
                    totalItems: 0,
                    loading: false,
                    pagination: {},
                    headers: [
                        { text: '', sortable: false },
                        { text: 'WEEKDAY', sortable: false },
                        { text: 'WEEKEND', sortable: false },
                    ],
                    items: [{ day: 'A' }, { day: 'B' }]
                },
                colors: {},
                items: {},
                snackbar: {
                    color: 'success',
                    text: '',
                    show: false,
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
            dateClicked(str) {
                this.dateForm.date = str
                this.dateForm.type = this.items[str]
                this.changeDialog = true
            },
            getColorsData(response) {
                var data = response.data

                this.colors = data.colors
                this.items = data.items
            },
            getColorsApi() {
                axios.post('http://localhost.com/room/post/getDayEffect').then(this.getColorsData).catch(e => { })
            },
            store() {
                axios.post('http://localhost.com/room/post/setDayEffect', this.dateForm).then(this.onSuccess).catch(e => { })

                this.changeDialog = false
            },
            onSuccess(response) {
                var data = response.data

                if (data.success) {
                    this.snackbar.color = 'success'
                } else {
                    this.snackbar.color = 'error'
                }

                this.snackbar.text = data.message
                this.snackbar.show = true
                this.getColorsApi()
            }
        },
        mounted() {
            this.getColorsApi()
        }
    }
</script>
