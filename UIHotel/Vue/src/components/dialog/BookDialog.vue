<template>
    <v-dialog v-model="dataValue.show" scrollable max-width="300px">
        <v-card>
            <v-card-title>Select Country</v-card-title>
            <v-divider></v-divider>
            <v-card-text style="height: 300px;">
                <v-text-field type="text" v-model="dataValue.text" @input="updateValue"></v-text-field>
            </v-card-text>
            <v-divider></v-divider>
            <v-card-actions>
                <v-btn color="blue darken-1" flat @click.native="dataValue.show = false">Close</v-btn>
                <v-btn color="blue darken-1" flat @click.native="dataValue.show = false">Save</v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
</template>
<script>
    export default {
        data() {
            return {
                dataValue: {
                    text: null,
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

                    if ('text' in this.value)
                        this.dataValue.text = this.value.text
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
