<template>
    <v-app id="inspire">
        <v-layout row>
            <v-flex md4>
                <v-card>
                    <v-card-title>
                        <h2 class="card-title mb-0">Calendar</h2>
                    </v-card-title>
                    <v-card-title>
                        <v-date-picker color="green lighten-1"></v-date-picker>
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
    </v-app>
</template>
<script>
    import axios from 'axios'

    export default {
        data() {
            return {
                tableData: {
                    search: '',
                    totalItems: 0,
                    loading: true,
                    pagination: {},
                    headers: [
                        { text: '', sortable: false },
                        { text: 'WEEKDAY', sortable: false },
                        { text: 'WEEKEND', sortable: false },
                    ],
                    items: [{ day: 'A' }, { day: 'B' }]
                }
            }
        }
    }
</script>
