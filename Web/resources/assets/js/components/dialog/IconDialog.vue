<template>
    <v-dialog v-model="modal" scrollable max-width="600px">
        <v-card>
            <v-card-title><strong>Choose Icon</strong></v-card-title>
            <v-divider></v-divider>
            <v-card-text>
                <v-btn @click="chooseIcon(icon)" icon v-for="icon in page_icon">
                    <v-icon>{{ icon }}</v-icon>
                </v-btn>
            </v-card-text>
            <v-divider></v-divider>
            <v-card-actions>
                <v-btn color="red darken-1" dark @click.native="cancelDialog">Cancel</v-btn>
                <v-btn color="primary" dark @click="prevPage">Prev</v-btn>
                <v-btn color="primary" dark @click="nextPage">Next</v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
</template>
<script>
    import listIcon from '../IconList'

    export default {
        computed: {
            page_icon() {
                // 20 items per page
                let page_size = 50
                let start = this.page * page_size
                let end = start + page_size
                let data = this.list_icon.slice(start, end)

                return data
            },
            maxPage() {
                return Math.ceil(this.list_icon.length / 50)
            }
        },
        data() {
            return {
                list_icon: listIcon.icons,
                modal: false,
                page: 0
            }
        },
        props: {
            showModal: { type: Boolean, default: false }
        },
        methods: {
            showDialog() {
                this.modal = true
            },
            cancelDialog() {
                this.modal = false
                this.$emit('cancel')
            },
            chooseIcon(icon) {
                this.$emit('save', icon)
                this.modal = false
            },
            nextPage() {
                if (this.page < this.maxPage - 1)
                    this.page++;
            },
            prevPage() {
                if (this.page > 0)
                    this.page--;
            }
        },
        watch: {
            showModal: {
                handler() {
                    this.modal = this.showModal
                }
            }
        },
        mounted() {
            this.$bus.$on('choose-icon', this.showDialog)
        },
        beforeDestroy() {
            this.$bus.$off('choose-icon', this.showDialog)
        }
    }
</script>
