<template>
    <v-card>
        <v-card-title>
            <h2 class="card-title mb-0">Change Room</h2>
            <v-spacer></v-spacer>
            <v-text-field append-icon="search"
                              label="Search"
                              single-line
                              hide-details
                              v-model="tableData.search"></v-text-field>
        </v-card-title>
        
        <v-container fluid grid-list-md>
            <v-layout row>
                <v-flex md12>
                    <v-btn dark color="primary" :href="detailLink">
                        <span>Back</span>
                    </v-btn>
                    <v-data-table
                        :items="tableData.items"
                        :headers="tableData.headers">
                        <template slot="items" slot-scope="props">
                            <tr>
                                <td>{{ props.item.RoomNumber }}</td>
                                <td>{{ props.item.RoomFloor }}</td>
                                <td>{{ props.item.RoomCategory }}</td>
                                <td class="text-xs-right">
                                    <v-btn color="success" @click.stop="move(props.item)">Move</v-btn>
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
                tableData: {
                    search: '',
                    items: [],
                    headers: [
                        {
                            text: 'Room Number',
                            align: 'left',
                            sortable: false,
                            value: 'RoomNumber'
                        },
                        { text: 'Floor', sortable: false, value: 'RoomFloor' },
                        { text: 'Type', sortable: false, value: 'IdCategory' },
                        { text: 'Actions', sortable: false, align: 'right' },
                    ],
                }
            }
        },
        computed: {
            detailLink() {
                return 'http://localhost.com/checkin/get/detail?id=' + this.checkid
            }
        },
        props: {
            checkid: { type: String, required: true }
        },
        methods: {
            getDataApi() {
                const search = this.tableData.search
                const book_id = ''

                axios.post('http://localhost.com/checkin/post/getRooms', { search, book_id })
                    .then(this.getData)
                    .catch(e => { })
            },
            getData(response) {
                var data = response.data

                this.tableData.items = []

                data.data.forEach(x => this.tableData.items.push(x))
            },
            move(item) {
                let checkid = this.checkid
                let roomid = item.Id

                axios.post('http://localhost.com/room/post/changeRoom', { checkid, roomid })
                    .then(this.moveResponse)
            },
            moveResponse(response) {
                let data = response.data

                if (data.success) {
                    window.location = data.redirect_uri
                }
            }
        },
        mounted() {
            this.getDataApi()
        }
    }
</script>