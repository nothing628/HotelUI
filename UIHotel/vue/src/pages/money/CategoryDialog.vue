<template>
    <v-dialog v-model="modal" max-width="600px">
        <v-card>
            <v-card-title><strong>{{ title }}</strong></v-card-title>
            <v-card-text></v-card-text>
            <v-divider></v-divider>
            <v-card-actions>
                <v-btn color="red darken-1" dark @click.native="cancel">Cancel</v-btn>
                <v-btn color="blue darken-1" dark @click.native="save">OK</v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
</template>
<script>
    export default {
        data() {
            return {
                modal: false,
                title: 'New Category',
            }
        },
        methods: {
            showDialog() {
                this.title = 'New Category'
                this.modal = true
            },
            editDialog(item) {
                this.title = 'Edit Category'
                this.modal = true
            },
            resetData() {
                //
            },
            save() {
                //
            },
            cancel() {
                this.modal = false
            }
        },
        mounted() {
            this.$bus.$on('new-category', this.showDialog)
            this.$bus.$on('edit-category', this.editDialog)
            this.resetData()
        },
        beforeDestroy() {
            this.$bus.$off('new-category', this.showDialog)
            this.$bus.$off('edit-category', this.editDialog)
        }
    }
</script>
