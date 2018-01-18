<template>
    <v-card>
        <v-card-title>
            <h2 class="card-title mb-0">Guest Detail</h2>
        </v-card-title>

        <v-container fluid grid-list-md>
            <v-layout row>
                <v-flex lg3 md3 sm12 xs12></v-flex>
                <v-flex lg9 md9 sm12 xs12></v-flex>
            </v-layout>
        </v-container>
    </v-card>
</template>
<script>
    import axios from 'axios'

    export default {
        data() {
            return {}
        },
        props: {
            IdNumber: { required: true, type: String }
        },
        methods: {
            getData(response) {
                var data = response.data

                if (data.success) {
                    console.log(data.data)
                }
            },
            getDataApi() {
                axios.post('http://localhost.com/guest/post/getGuestDetail', { id: this.IdNumber })
                    .then(this.getData)
                    .catch(e => { })
            }
        },
        mounted() {
            this.getDataApi()
        }
    }
</script>
