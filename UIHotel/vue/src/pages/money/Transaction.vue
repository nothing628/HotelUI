<template>
    <v-card class="mb-4">
        <v-card-title primary-title>
            <h2 class="card-title mb-0">Pencatatan Transaksi</h2>
        </v-card-title>

        <v-container fluid grid-list-md>
            <v-layout row>
                <v-flex md12>
                    <label>Date :</label>
                    <v-menu ref="menu1"
                            :close-on-content-click="false"
                            v-model="modal1"
                            transition="scale-transition"
                            offset-y
                            :nudge-right="40"
                            min-width="310px"
                            :return-value.sync="mdate">
                        <v-text-field slot="activator"
                                      v-model="mdate"
                                      prepend-icon="event"
                                      readonly></v-text-field>
                        <v-date-picker v-model="mdate"
                                       @change="$refs.menu1.save(mdate)"
                                       :max="allowArr" no-title scrollable>
                        </v-date-picker>
                    </v-menu>
                    <v-btn color="primary" @click="newData">New Transaction</v-btn>
                </v-flex>
            </v-layout>
            <v-divider></v-divider>
            <v-layout row>
                <v-flex md12>

                </v-flex>
            </v-layout>
        </v-container>

        <tdialog></tdialog>
    </v-card>
</template>
<script>
    import moment from 'moment'
    import tdialog from '../../components/dialog/TransactionDialog.vue'
    export default {
        components: {
            tdialog
        },
        data() {
            return {
                mdate: null,
                modal1: false,
            }
        },
        computed: {
            allowArr() {
                return moment().format('YYYY-MM-DD')
            },
        },
        methods: {
            newData() {
                //New Transaction
            }
        },
        mounted() {
            this.mdate = moment().format('YYYY-MM-DD')
        }
    }
</script>
