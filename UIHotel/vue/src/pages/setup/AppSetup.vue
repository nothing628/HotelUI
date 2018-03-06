<template>
    <v-card class="mb-4">
        <v-card-title primary-title>
            <h5>Installation Step 3</h5>
        </v-card-title>
        <v-divider></v-divider>
        <v-divider></v-divider>
        <v-card-actions>
            <v-btn color="red darken-1" dark @click.native="prev">Previous</v-btn>
            <v-btn color="green" dark @click.native="next">Next</v-btn>
        </v-card-actions>
    </v-card>
</template>
<script>
    export default {
        data() {
            return {}
        },
        props: {
            nextStep: { type: String, required: true },
            prevStep: { type: String, required: true },
        },
        methods: {
            next() {
                //
            },
            prev() {
                //
            }
        },
        mounted() {
            //
        }
    }
</script>