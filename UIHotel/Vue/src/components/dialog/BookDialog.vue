<template>
    <v-dialog v-model="dataValue.show" scrollable max-width="800px">
        <v-card>
            <v-card-title>Select Booking</v-card-title>
            <v-divider></v-divider>
            <v-card-text style="height: 500px;">
                <v-container grid-list-md>
                    <v-layout wrap>
                        <v-flex md12>
                            <v-text-field label="Search books" v-model="tableData.search" hide-details></v-text-field>
                        </v-flex>
                    </v-layout>
                    <v-data-table v-bind:headers="tableData.headers"
                                  v-bind:items="tableData.items"
                                  v-bind:pagination.sync="tableData.pagination"
                                  v-bind:total-items="tableData.totalItems"
                                  class="elevation-1">
                        <template slot="items" slot-scope="props">
                            <tr style="cursor:pointer;" @click="setValue(props.item)">
                                <td class="text-xs-center">{{ props.item.Id }}</td>
                                <td class="text-xs-center">{{ props.item.Guest.Fullname }}</td>
                                <td class="text-xs-center">{{ props.item.ArriveAt | dateformat }}</td>
                                <td class="text-xs-center">{{ props.item.DepartureAt | dateformat }}</td>
                                <td class="text-xs-center">{{ props.item.DepartureAt }}</td>
                            </tr>
                        </template>
                        <template slot="pageText" slot-scope="{ pageStart, pageStop }">
                            From {{ pageStart }} to {{ pageStop }}
                        </template>
                    </v-data-table>
                </v-container>
            </v-card-text>
            <v-divider></v-divider>
            <v-card-actions>
                <v-btn color="blue darken-1" flat @click.native="dataValue.show = false">Close</v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
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
                    pagination: {
                        rowsPerPage: 20
                    },
                    headers: [
                        { text: 'ID', sortable: false },
                        { text: 'Name', sortable: false },
                        { text: 'Arrive At', sortable: false },
                        { text: 'Departure At', sortable: false },
                        { text: '', sortable: false },
                    ],
                    items: []
                },
                dataValue: {
                    show: false,
                }
            }
        },
        watch: {
            'tableData.search': {
                handler() {
                    this.getDataApi()
                }
            },
            dataValue: {
                handler() {
                    this.updateValue()
                },
                deep: true
            },
            value: {
                handler() {
                    if ('show' in this.value)
                        this.dataValue.show = this.value.show
                    if ('book' in this.value)
                        this.dataValue.book = this.value.book
                },
                deep: true
            }
        },
        filters: {
            dateformat(val) {
                var momen = moment(val)

                return momen.format('YYYY-MM-DD');
            }
        },
        props: {
            value: { type: Object, default() { return {}; } }
        },
        methods: {
            updateValue() {
                // Emit the number value through the input event
                this.$emit('input', this.dataValue)
            },
            setValue(item) {
                this.dataValue.book = item
                this.dataValue.show = false
            },
            getData(response) {
                var data = response.data

                this.tableData.items = []

                data.data.forEach(x => this.tableData.items.push(x))
            },
            getDataApi() {
                const search = this.tableData.search
                const { page, rowsPerPage } = this.tableData.pagination

                axios.post('http://localhost.com/checkin/post/getBookingList', { page, rowsPerPage, search })
                    .then(this.getData)
                    .catch(e => { })
            }
        },
        mounted() {
            this.getDataApi()
        }
    }
</script>
