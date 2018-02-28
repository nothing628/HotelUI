<template>
    <v-card class="mb-4">
        <v-card-title primary-title>
            <h2 class="card-title mb-0">Category Transaction</h2>
        </v-card-title>

        <v-container fluid grid-list-md>
            <v-layout row>
                <v-flex md12>
                    <v-btn color="success" xsmall @click="newCategory"><v-icon>add</v-icon> New Category</v-btn>
                </v-flex>
            </v-layout>
            <v-layout row>
                <v-flex md12>
                    <v-data-table v-bind:headers="headers"
                                  v-bind:items="items"
                                  hide-actions
                                  class="elevation-1">
                        <template slot="items" slot-scope="props">
                            <td>{{ props.item.Icon }}</td>
                            <td>{{ props.item.Description }}</td>
                            <td class="text-center">
                                <v-btn small fab flat outline color="warning" @click="editCategory(props.item)">
                                    <v-icon dark>create</v-icon>
                                </v-btn>
                                <v-btn small fab flat outline color="error" @click="confCategory(props.item)">
                                    <v-icon dark>clear</v-icon>
                                </v-btn>
                            </td>
                        </template>
                    </v-data-table>
                </v-flex>
            </v-layout>
        </v-container>
        <cdialog></cdialog>
    </v-card>
</template>
<script>
    import axios from 'axios'
    import cdialog from './CategoryDialog.vue'

    export default {
        components: {
            cdialog
        },
        data() {
            return {
                items: [],
                headers: [
                    { text: 'Icon', sortable: false, align: 'left', },
                    { text: 'Category', sortable: false, align: 'left' },
                    { text: 'Action', sortable: false, align: 'center' },
                ],
            }
        },
        methods: {
            newCategory() {
                this.$bus.$emit('new-category')
            },
            editCategory(data) {
                //
            },
            confCategory(data) {
                //
            },
            saveCategory(data) {
                //
            },
            updateCategory(data) {
                //
            },
            deleteCategory(data) {
                //
            },
            getData() {
                axios.post('http://localhost.com/money/post/getCategory').then(this.getDataData)
            },
            getDataData(response) {
                let data = response.data

                if (data.success) {
                    let items = data.data

                    this.items = []
                    console.log(data)
                    items.forEach(x => this.items.push(x))
                    
                }
            }
        },
        mounted() {
            this.getData()
        }
    }
</script>
