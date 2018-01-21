<template>
    <div></div>
</template>
<script>
    import axios from 'axios'
    import moment from 'moment'
    import country from '../../components/CountryList'

    export default {
        data() {
            return {
                guest: {}
            }
        },
        props: {
            IdNumber: { type: String, required: true }
        },
        computed: {
            country_list() {
                return country.country_list
            },
            PhotoUrl() {
                return ''
            }
        },
        methods: {
            getData(response) {
                var data = response.data
            },
            getDataApi() {
                //
            },
            updateData(response) {
                var data = response.data

                this.getDataApi()
            },
            updateDataApi() {
                //
            }
        },
        mounted() {
            this.getDataApi()
        }
    }
</script>
