<template>
    <v-card class="mb-4">
        <v-card-title primary-title>
            <h5>Installation Finish</h5>
        </v-card-title>
        <v-divider></v-divider>
        <v-card-text>
            <div class="d-flex my-4">
                <v-icon color="green" size="150px">done_all</v-icon>
            </div>
            <h5 class="text--disabled text-sm-center">Installation Finish</h5>
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
                window.CS.close()
            }
        }
    }
</script>
