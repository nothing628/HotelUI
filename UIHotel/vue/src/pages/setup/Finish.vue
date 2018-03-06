<template>
    <v-card class="mb-4">
        <v-card-title primary-title>
            <h5>Installation Step 4</h5>
        </v-card-title>
        <v-divider></v-divider>
        <v-card-text>
            <h5 class="title text-sm-center grey--text">Finish</h5>
        </v-card-text>
        <v-divider></v-divider>
        <v-card-actions>
            <v-btn color="green" dark @click.native="close">Finish</v-btn>
        </v-card-actions>
    </v-card>
</template>
<script>
    export default {
        data() {
            return {}
        },
        methods: {
            close() {
                //
            }
        },
        mounted() {
            //
        }
    }
</script>
