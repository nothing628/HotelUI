<template>
    <v-dialog v-model="dataValue.show" scrollable max-width="600px">
        <v-card>
            <v-card-title>Select Room</v-card-title>
            <v-divider></v-divider>
            <v-card-text style="height: 400px;">
                <v-container grid-list-md>
                    <v-layout wrap>
                        <v-flex md12>
                            <v-text-field label="Search room" v-model="tableData.search" hide-details></v-text-field>
                        </v-flex>
                    </v-layout>
                </v-container>
                <v-data-table v-bind:headers="tableData.headers"
                              v-bind:items="tableData.items"
                              v-bind:pagination.sync="tableData.pagination"
                              v-bind:total-items="tableData.totalItems"
                              v-bind:hide-actions="true"
                              class="elevation-1">
                    <template slot="items" slot-scope="props">
                        <tr style="cursor:pointer;" @click="setValue(props.item)">
                            <td class="text-xs-center" :style="getStyle(props.item)">{{ props.item.RoomCategory }}</td>
                            <td class="text-xs-center" :style="getStyle(props.item)">{{ props.item.RoomNumber }}</td>
                            <td class="text-xs-center" :style="getStyle(props.item)">{{ props.item.RoomFloor }}</td>
                            <td class="text-xs-center" :style="getStyle(props.item)">{{ props.item.Status }}</td>
                        </tr>
                    </template>
                    <template slot="pageText" slot-scope="{ pageStart, pageStop }">
                        From {{ pageStart }} to {{ pageStop }}
                    </template>
                </v-data-table>
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

    export default {
        data() {
            return {
                tableData: {
                    search: '',
                    totalItems: 0,
                    pagination: {
                        rowsPerPage: -1
                    },
                    headers: [
                        { text: 'TYPE', sortable: false, align: 'center' },
                        { text: 'NUMBER', sortable: false, align: 'center' },
                        { text: 'FLOOR', sortable: false, align: 'center' },
                        { text: 'Status', sortable: false, align: 'center' },
                    ],
                    items: []
                },
                dataValue: {
                    book_id: '',
                    room: {},
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
            'dataValue.book_id': {
                handler() {
                    this.getDataApi()
                },
                deep: false,
            },
            dataValue: {
                handler() {
                    this.updateValue()
                },
                deep: true
            },
            value: {
                handler() {
                    if ('book_id' in this.value)
                        this.dataValue.book_id = this.value.book_id
                    if ('show' in this.value)
                        this.dataValue.show = this.value.show
                    if ('room' in this.value)
                        this.dataValue.room = this.value.room
                },
                deep: true
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
                this.dataValue.room = item
                this.dataValue.show = false
            },
            getData(response) {
                var data = response.data

                this.tableData.items = []
                
                data.data.forEach(x => this.tableData.items.push(x))
            },
            getDataApi() {
                const search = this.tableData.search
                const book_id = this.dataValue.book_id

                axios.post('http://localhost.com/checkin/post/getRooms', { search, book_id })
                    .then(this.getData)
                    .catch(e => { })
            },
            getStyle(item) {
                var backColor = '#' + item.StatusColor
                var textColor = '#FFFFFF'

                return { 'background-color': backColor, color: textColor }
            }
        },
        mounted() {
            this.getDataApi()
        }
    }
</script>
