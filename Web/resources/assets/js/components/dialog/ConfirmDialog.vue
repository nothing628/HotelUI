<template>
    <v-dialog v-model="modal" max-width="500px">
        <v-card>
            <v-card-title><strong><v-icon color="success">info_outline</v-icon> {{ title }}</strong></v-card-title>
            <v-card-text>
                <div>{{ text }}</div>
            </v-card-text>
            <v-card-actions>
                <v-btn color="success" flat @click.stop="onYes">Yes</v-btn>
                <v-btn color="error" flat @click.stop="onNo">No</v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
</template>
<script>
    export default {
        data() {
            return {
                modal: false,
                title: null,
                text: null,
                onActionYes: () => { },
                onActionNo: () => { },
            }
        },
        methods: {
            onYes() {
                this.modal = false

                this.onActionYes()
            },
            onNo() {
                this.modal = false

                this.onActionNo()
            },
            showDialog(param) {
                this.title = param.title
                this.text = param.message
                this.onActionNo = param.onNo
                this.onActionYes = param.onYes
                this.modal = true
            }
        },
        mounted() {
            this.$bus.$on('dialog-show', this.showDialog)
        },
        beforeDestroy() {
            this.$bus.$off('dialog-show', this.showDialog)
        }
    }
</script>
