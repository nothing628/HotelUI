<template>
    <v-snackbar :timeout="timeout"
                top
                :color="color"
                v-model="snackbar">
        {{ text }}
        <v-btn dark flat @click.native="snackbar = false">Close</v-btn>
    </v-snackbar>
</template>
<script>
    export default {
        data() {
            return {
                color: null,
                snackbar: false,
                timeout: 5000,
                text: null,
            }
        },
        computed: {
        },
        methods: {
            showAlert(param) {
                if ('text' in param) {
                    this.color = (param.color) || null
                    this.timeout = (param.timeout) || 5000

                    this.text = param.text
                    this.snackbar = true
                }
            }
        },
        mounted() {
            this.$bus.$on('alert-show', this.showAlert)
        },
        beforeDestroy() {
            this.$bus.$off('alert-show', this.showAlert)
        }
    }
</script>
