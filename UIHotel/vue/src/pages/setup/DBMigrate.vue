<template>
    <v-card class="mb-4">
        <v-card-title primary-title>
            <h5>Installation Step 2</h5>
        </v-card-title>
        <v-divider></v-divider>
        <v-card-text v-if="loading" style="min-height:300px;">
            <div class="d-flex align-center justify-center my-5">
                <div class="page-loader__spinner">
                    <svg viewBox="25 25 50 50">
                        <circle cx="50" cy="50" r="20" fill="none" stroke-width="2" stroke-miterlimit="10" />
                    </svg>
                </div>
            </div>
            <h5 class="title text-sm-center text--disabled">Installing Database {{ load_txt }}</h5>
        </v-card-text>
        <v-card-text v-if="!loading && success" style="min-height:300px;">
            <div class="d-flex my-4">
                <v-icon color="green" size="150px">done_all</v-icon>
            </div>
            <h5 class="text--disabled text-sm-center">Success Migrate Database</h5>
        </v-card-text>
        <v-card-text v-if="!loading && !success">
            <div v-html="message" style="max-height:272px;overflow-y:auto"></div>
        </v-card-text>
        <v-divider></v-divider>
        <v-card-actions>
            <v-btn color="red darken-1" dark @click.native="prev" :disabled="loading">Previous</v-btn>
            <v-btn color="green" dark @click.native="next" :disabled="loading && !success">Next</v-btn>
        </v-card-actions>
    </v-card>
</template>
<script>
    import axios from 'axios'
    export default {
        data() {
            return {
                loading: true,
                num: 0,
                message: '',
                success: false,
            }
        },
        computed: {
            load_txt() {
                return '.'.repeat(this.num)
            }
        },
        props: {
            nextStep: { type: String, required: true },
            prevStep: { type: String, required: true },
        },
        methods: {
            doMigration() {
                axios.post('http://localhost.com/setup/api/DoMigration').then(this.finishMigration)
            },
            finishMigration(response) {
                let data = response.data

                this.loading = false
                this.message = data.message
                this.success = data.success
            },
            prev() {
                window.location = this.prevStep
            },
            next() {
                window.location = this.nextStep
            },
            tick() {
                this.num++

                if (this.num > 5)
                    this.num = 0
            }
        },
        mounted() {
            setInterval(this.tick, 1000)
            setTimeout(this.doMigration, 1000)
        }
    }
</script>