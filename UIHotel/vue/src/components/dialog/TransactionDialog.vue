<template>
    <v-dialog v-model="modal" max-width="600px">
        <v-card>
            <v-card-title><strong>New Transaction</strong></v-card-title>
        </v-card>
        <v-card-text>

        </v-card-text>
    </v-dialog>
</template>
<script>
    import moment from 'moment'
    import axios from 'axios'
    export default {
        data() {
            return {
                tdate: null,
                ttime: null,
                modal: false,
                amount: 0,
                categories: []
            }
        },
        computed: {
            categoryExpense() {
                return this.categories.filter(x => x.IsExpense)
            },
            categoryNonExpense() {
                return this.categories.filter(x => !x.IsExpense)
            }
        },
        methods: {
            showDialog() {
                this.amount = 0
                this.modal = true
            },
            saveTransaction() {
                let data = {}

                this.$emit('save', data)
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
        mounted() {
            this.$bus.$on('new-transaction', this.showDialog)
            this.tdate = moment().format('YYYY-MM-DD')
            this.getCategory()
        },
        beforeDestroy() {
            this.$bus.$off('new-transaction', this.showDialog)
        }
    }
</script>
