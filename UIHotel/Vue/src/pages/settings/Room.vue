<template>
    <v-app id="inspire">
        <v-card>
            <v-card-title>
                <v-btn color="primary" dark @click.stop="newRoom">Add Room</v-btn>
                <v-spacer></v-spacer>
                <v-text-field append-icon="search"
                              label="Search"
                              single-line
                              hide-details
                              v-model="search"></v-text-field>
            </v-card-title>
            <v-data-table v-bind:headers="tableData.headers"
                          v-bind:items="tableData.items"
                          v-bind:search="tableData.search"
                          v-bind:pagination.sync="tableData.pagination"
                          v-bind:total-items="tableData.totalItems"
                          v-bind:loading="tableData.loading"
                          class="elevation-1">
                <template slot="items" slot-scope="props">
                    <td>{{ props.item.RoomNumber }}</td>
                    <td class="text-xs-right">{{ props.item.RoomFloor }}</td>
                    <td class="text-xs-right">{{ props.item.RoomCategory }}</td>
                    <td class="text-xs-right">{{ props.item.RoomStatus }}</td>
                    <td class="text-xs-right">
                        <v-btn color="warning" @click.stop="editRoom(props.item)">Edit</v-btn>
                        <v-btn color="error" @click.stop="deleteRoom(props.item)">Remove</v-btn>
                    </td>
                </template>
                <template slot="pageText" slot-scope="{ pageStart, pageStop }">
                    From {{ pageStart }} to {{ pageStop }}
                </template>
            </v-data-table>
        </v-card>

        <v-dialog v-model="newDialog" max-width="500px">
            <v-card>
                <v-card-title>Add Room </v-card-title>
                <v-card-text>
                    <v-form v-model="valid">
                        <v-text-field label="Room Number"
                                      :rules="nameRules"
                                      :counter="10"
                                      v-model="roomData.roomNumber"
                                      required></v-text-field>
                        <v-text-field label="Room Floor"
                                      type="number"
                                      v-model="roomData.roomFloor"
                                      required></v-text-field>
                        <v-select v-bind:items="select"
                                  v-model="roomData.roomCategory"
                                  label="Room Category"
                                  item-value="value"
                                  item-text="text"></v-select>
                    </v-form>
                </v-card-text>
                <v-card-actions>
                    <v-btn color="primary" @click.stop="saveRoom">Submit</v-btn>
                    <v-btn color="primary" flat @click.stop="newDialog=false">Close</v-btn>
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
                        {
                            text: 'Room Number',
                            align: 'left',
                            sortable: false,
                            value: 'RoomNumber'
                        },
                        { text: 'Floor', sortable: false, value: 'RoomFloor' },
                        { text: 'Type', sortable: false, value: 'IdCategory' },
                        { text: 'Status', sortable: false, value: 'Status' },
                        { text: '', sortable: false }
                    ],
                    items: []
                },
                roomData: {
                    id: "",
                    roomNumber: "",
                    roomFloor: 1,
                    roomCategory: ""
                },
                newDialog: false,
                nameRules: [
                    (v) => !!v || 'Name is required',
                    (v) => v && v.length <= 10 || 'Name must be less than 10 characters'
                ],
                select: [],
                snackbar: {
                    color: 'success',
                    text: '',
                    show: false,
                }
            }
        },
        watch: {
            'tableData.pagination': {
                handler() {
                    this.getDataFromApi();
                },
                deep: true
            }
        },
        methods: {
            newRoom() {
                this.roomData.id = ""
                this.newDialog = true
            },
            deleteRoom(room) {
                console.log(room)
            },
            editRoom(room) {
                this.roomData.id = room.Id;
                this.roomData.roomCategory = room.IdCategory
                this.roomData.roomNumber = room.RoomNumber
                this.roomData.roomFloor = room.RoomFloor

                this.newDialog = true
            },
            successSave(response) {
                if (response.data.success) {
                    this.snackbar.color = 'success'
                } else {
                    this.snackbar.color = 'error'
                }

                this.snackbar.text = response.data.message
                this.snackbar.show = true
                this.getDataFromApi()
            },
            saveRoom() {
                this.newDialog = false

                if (this.roomData.id == "") {
                    //Insert Data
                    axios.post('http://localhost.com/room/post/setRoom', this.roomData)
                        .then(this.successSave)
                        .catch(e => { })
                } else {
                    //Update Data
                    axios.post('http://localhost.com/room/post/updateRoom', this.roomData)
                        .then(this.successSave)
                        .catch(e => { })
                }
            },
            getData(response) {
                let items = response.data;

                this.tableData.loading = false
                this.tableData.items = items
                this.tableData.totalItems = items.length
            },
            getDataFromApi() {
                const { sortBy, descending, page, rowsPerPage } = this.tableData.pagination
                this.tableData.loading = true

                axios.get('http://localhost.com/room/get/getRoom').then(this.getData).catch(e => { })
            },
            getCategory() {
                axios.get('http://localhost.com/room/get/getCategory').then(this.getCategoryData).catch(e => { })
            },
            getCategoryData(response) {
                let items = response.data;

                this.select = []

                items.forEach((i, index) => {
                    this.select.push({
                        text: i.Category,
                        value: i.Id,
                    })
                })
            }
        },
        mounted() {
            this.getCategory()
            this.getDataFromApi()
        }
    }
</script>
