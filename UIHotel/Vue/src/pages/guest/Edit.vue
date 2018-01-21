<template>
    <div></div>
</template>
<script>
    export default {
        data() {
            return {
                guest: {}
            }
        },
        props: {
            IdNumber: { type: String, required: true }
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
