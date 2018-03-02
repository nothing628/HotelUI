<template>
    <div></div>
</template>
<script>
    export default {
        data() {
            return {
                SQL_Database: ''
            }
        },
        methods: {
            getData() {
                //
            },
            getDataData(response) {
                let data = response.data

                if (data.success) {
                    //
                }
            }
        },
        mounted() {
            this.getData()
        }
    }
</script>
