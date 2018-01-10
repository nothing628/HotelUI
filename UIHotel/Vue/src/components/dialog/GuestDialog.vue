<template>
    <v-dialog v-model="dataValue.show" scrollable max-width="600px">
        <v-card>
            <v-card-title>Select Guest</v-card-title>
            <v-divider></v-divider>
            <v-card-text style="height: 400px;"></v-card-text>
            <v-divider></v-divider>
            <v-card-actions>
                <v-btn color="blue darken-1" flat @click.native="dataValue.show = false">Close</v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
</template>
<script>
    import axios from 'axios'

    export default {
        data() {
            return {
                dataValue: {
                    show: false,
                }
            }
        },
        watch: {
            dataValue: {
                handler() {
                    this.updateValue()
                },
                deep: true
            },
            value: {
                handler() {
                    if ('show' in this.value)
                        this.dataValue.show = this.value.show
                },
                deep: true
            }
        },
        props: {
            value: { type: Object, default() { return {}; } }
        },
        methods: {
            updateValue() {
                // Emit the number value through the input event
                this.$emit('input', this.dataValue)
            }
        }
    }
</script>
