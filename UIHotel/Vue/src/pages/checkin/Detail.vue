<template>
    <div>
        <v-card>
            <v-card-title>
                <h2 class="card-title mb-0">Checkin Detail</h2>
            </v-card-title>

            <v-container fluid grid-list-md>
                <v-layout row>
                    <v-flex lg3 md3 sm12 xs12></v-flex>
                    <v-flex lg9 md9 sm12 xs12></v-flex>
                </v-layout>
            </v-container>
        </v-card>
        <v-card class="mt-2">
            <v-card-title>
                <h2 class="card-title mb-0">Invoice</h2>
            </v-card-title>

            <v-container fluid grid-list-md>
                <v-layout row>
                    <v-flex lg12 md12 sm12 xs12></v-flex>
                </v-layout>
            </v-container>
        </v-card>
    </div>
</template>
<script>
    export default {
        data() {
            return {}
        }
    }
</script>
