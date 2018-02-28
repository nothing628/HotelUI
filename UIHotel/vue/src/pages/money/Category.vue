<template>
    <v-card class="mb-4">
        <v-card-title primary-title>
            <h2 class="card-title mb-0">Category Transaction</h2>
        </v-card-title>

        <v-container fluid grid-list-md>
            <v-layout row>
                <v-flex md12>
                    <v-data-table v-bind:headers="headers"
                                  v-bind:items="items"
                                  hide-actions
                                  class="elevation-1">
                        <template slot="items" slot-scope="props">
                            <td>{{ props.item.Date | dateString }}</td>
                            <td>{{ props.item.Description }}</td>
                            <td>{{ props.item.Category.Description }}</td>
                            <td v-if="!props.item.IsExpense"><strong>{{ props.item.Debit | Currency }}</strong></td>
                            <td v-if="props.item.IsExpense"><strong>{{ -props.item.Kredit | Currency }}</strong></td>
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
                items: [],
                headers: [
                    { text: 'Icon', sortable: false, align: 'left', },
                    { text: 'Category', sortable: false, align: 'left' },
                    { text: 'Action', sortable: false, align: 'center' },
                ],
            }
        },
        methods: {
            getData() {
                //
            },
            getDataData(response) {
                let data = response.data

                if (data.success) {
                    //
                }
            }
        },
        mounted() {
            this.getData()
        }
    }
</script>
