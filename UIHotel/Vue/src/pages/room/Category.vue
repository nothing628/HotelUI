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
                        <v-btn color="warning" @click.stop="edit(props.item)">Edit</v-btn>
                        <v-btn color="error"  @click.stop="remove(props.item)">Remove</v-btn>
                    </td>
                </template>
                <template slot="pageText" slot-scope="{ pageStart, pageStop }">
                    From {{ pageStart }} to {{ pageStop }}
                </template>
            </v-data-table>
        </v-card>

        <v-dialog v-model="newDialog" max-width="500px">
            <v-card>
                <v-card-title>Add Category </v-card-title>
                <v-card-text>
                    <v-form v-model="valid">
                        <v-text-field label="Category Name"
                                      :rules="rules.categoryRules"
                                      :counter="50"
                                      v-model="categoryData.category"
                                      required></v-text-field>
                        <v-text-field label="Description"
                                      :rules="rules.descriptionRules"
                                      :counter="200"
                                      :multi-line="true"
                                      :rows="2"
                                      v-model="categoryData.description"
                                      required></v-text-field>
                    </v-form>
                </v-card-text>
                <v-card-actions>
                    <v-btn color="primary" @click.stop="store">Submit</v-btn>
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
                        { text: 'Category', sortable: false, value: 'Category' },
                        { text: 'Description', sortable: false, value: 'Description' },
                        { text: '', sortable: false },
                    ],
                    items: []
                },
                valid: true,
                newDialog: false,
                rules: {
                    categoryRules: [
                        (v) => !!v || 'Category Name is required',
                        (v) => v && v.length <= 50 || 'Category must be less than 50 characters'
                    ],
                    descriptionRules: [
                        (v) => v && v.length <= 200 || 'Description must be less than 200 characters'
                    ]
                },
                categoryData: {
                    id: "",
                    category: "",
                    description: "",
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
                this.categoryData.id = ""
                this.categoryData.category = ""
                this.categoryData.description = ""
                this.newDialog = true
            },
            remove(category) {
                this.categoryData.id = category.Id

                //Delete Data
                axios.post('http://localhost.com/room/post/deleteCategory', this.categoryData)
                    .then(this.successSave)
                    .catch(e => { })
            },
            edit(category) {
                this.categoryData.id = category.Id
                this.categoryData.category = category.Category
                this.categoryData.description = category.Description
                this.newDialog = true
            },
            store() {
                this.newDialog = false

                if (this.categoryData.id == "") {
                    //Insert Data
                    axios.post('http://localhost.com/room/post/storeCategory', this.categoryData)
                        .then(this.successSave)
                        .catch(e => { })
                } else {
                    //Update Data
                    axios.post('http://localhost.com/room/post/updateCategory', this.categoryData)
                        .then(this.successSave)
                        .catch(e => { })
                }
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
        },
        mounted() {
            this.tableData.pagination.rowsPerPage = 10
            this.getDataFromApi()
        }
    }
</script>
