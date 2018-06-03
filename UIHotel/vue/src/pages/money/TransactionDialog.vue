<template>
    <v-dialog v-model="modal3" max-width="600px" persistent>
        <v-card>
            <v-card-title><strong>New Transaction</strong></v-card-title>
            <v-card-text>
                <v-container fluid class="px-3">
                    <v-layout row>
                        <v-flex xs5>
                            <v-menu ref="menu1"
                                    lazy
                                    :close-on-content-click="false"
                                    v-model="modal1"
                                    transition="scale-transition"
                                    offset-y
                                    full-width
                                    :nudge-right="40"
                                    min-width="290px">
                                <v-text-field slot="activator"
                                              label="Transaction Date"
                                              v-model="tdate"
                                              append-icon="event"
                                              readonly></v-text-field>
                                <v-date-picker ref="picker"
                                               v-model="tdate"
                                               @change="$refs.menu1.save(tdate)"
                                               min="1900-01-01"
                                               :max="new Date().toISOString().substr(0, 10)"></v-date-picker>
                            </v-menu>
                        </v-flex>
                        <v-flex xs1></v-flex>
                        <v-flex xs5>
                            <v-menu ref="menu2"
                                    lazy
                                    :close-on-content-click="false"
                                    v-model="modal2"
                                    transition="scale-transition"
                                    offset-y
                                    full-width
                                    :nudge-right="0"
                                    max-width="290px"
                                    min-width="290px"
                                    :return-value.sync="ttime">
                                <v-text-field slot="activator"
                                              label="Transaction Time"
                                              v-model="ttime"
                                              append-icon="access_time"
                                              readonly></v-text-field>
                                <v-time-picker v-model="ttime" @change="changeTime"></v-time-picker>
                            </v-menu>
                        </v-flex>
                    </v-layout>
                    <v-layout row>
                        <v-flex xs12>
                            <v-switch label="Outcome Transaction"
                                      v-model="isOutcome"></v-switch>
                        </v-flex>
                        <v-flex xs12>
                            <v-select label="Select"
                                      :items="categoryList"
                                      :rules="rules.category"
                                      ref="category"
                                      v-model="category"
                                      item-text="Description"
                                      item-value="Id"
                                      max-height="auto"
                                      autocomplete>
                                <template slot="item" slot-scope="data">
                                    <v-list-tile-avatar>
                                        <v-icon large :color="data.item.Color">{{ data.item.Icon }}</v-icon>
                                    </v-list-tile-avatar>
                                    <v-list-tile-content>
                                        <v-list-tile-title v-html="data.item.Description"></v-list-tile-title>
                                    </v-list-tile-content>
                                </template>
                            </v-select>
                        </v-flex>
                    </v-layout>
                    <v-layout row>
                        <v-flex xs12>
                            <v-text-field label="Description"
                                          :rules="rules.desc"
                                          :counter="512"
                                          multi-line
                                          rows="2"
                                          ref="description"
                                          v-model="description"></v-text-field>
                        </v-flex>
                    </v-layout>
                    <v-layout row>
                        <v-flex xs12>
                            <v-text-field label="Amount"
                                          type="number"
                                          ref="amount"
                                          prefix="Rp"
                                          :rules="rules.amount"
                                          v-model="amount"></v-text-field>
                        </v-flex>
                    </v-layout>
                </v-container>
            </v-card-text>
            <v-divider></v-divider>
            <v-card-actions>
                <v-btn color="red darken-1" dark @click.native="cancelTransaction">Cancel</v-btn>
                <v-btn color="blue darken-1" dark @click.native="saveTransaction">OK</v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
</template>
<script>
    import moment from 'moment'
    import axios from 'axios'
    export default {
        data() {
            return {
                modal1: false,
                modal2: false,
                modal3: false,
                description: '',
                tdate: null,
                ttime: null,
                category: '',
                amount: 0,
                isOutcome: true,
                categories: [],
                validref: ['amount', 'description', 'category'],
                rules: {
                    desc: [
                        (v) => (!!v && v.length > 0) || 'This field is required',
                        (v) => (!!v && v.length <= 512) || 'Max 512 characters'
                    ],
                    amount: [
                        (v) => parseInt(v) > 0 || 'This amount must be greater than 0'
                    ],
                    category: [
                        (v) => v != '' || 'Please select category'
                    ]
                }
            }
        },
        computed: {
            categoryList() {
                if (this.isOutcome)
                    return this.categoryExpense
                else
                    return this.categoryNonExpense
            },
            categoryExpense() {
                return this.categories.filter(x => x.IsExpense)
            },
            categoryNonExpense() {
                return this.categories.filter(x => !x.IsExpense)
            }
        },
        methods: {
            changeTime(e) {
                this.$refs.menu2.save(this.ttime)
                this.modal3 = true
            },
            validate() {
                let formNoErrors = true
                let keys = this.validref

                keys.forEach(x => {
                    let ref = this.$refs[x]

                    if (!ref.validate(true))
                        formNoErrors = false
                })
                
                return formNoErrors
            },
            resetValidation() {
                let keys = this.validref

                keys.forEach(x => {
                    this.$refs[x].reset()
                })
            },
            resetData() {
                this.tdate = moment().format('YYYY-MM-DD')
                this.ttime = moment().format('HH:mm')
                this.amount = 0
                this.description = ''
                this.category = ''
                this.isOutcome = true
            },
            showDialog() {
                this.resetValidation()
                this.resetData()
                this.modal3 = true
            },
            cancelTransaction() {
                this.modal3 = false
            },
            saveTransaction() {
                let data = {
                    date: this.tdate,
                    time: this.ttime,
                    idCategory: this.category,
                    isOutcome: this.isOutcome,
                    amount: this.amount,
                    description: this.description,
                }

                if (this.validate()) {
                    this.$emit('save', data)
                    this.cancelTransaction()
                }
            },
            getCategory() {
                axios.post('http://localhost.com/money/post/getCategories').then(this.getCategoryData)
            },
            getCategoryData(response) {
                let data = response.data

                if (data.success) {
                    let items = data.data

                    this.categories = []

                    items.forEach(x => this.categories.push(x))
                }
            }
        },
        watch: {
        },
        mounted() {
            this.$bus.$on('new-transaction', this.showDialog)
            this.getCategory()
            this.resetValidation()
            this.resetData()
        },
        beforeDestroy() {
            this.$bus.$off('new-transaction', this.showDialog)
        }
    }
</script>
