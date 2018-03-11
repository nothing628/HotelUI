<template>
    <div></div>
</template>
<script>
    import axios from 'axios'
    export default {
        data() {
            return {
                //
            }
        },
        methods: {
            getData() {
                //
            },
            getDataResponse(response) {
                let data = response.data

                if (data.success) {
                    let items = data.data

                    console.log(items)
                }
            }
        },
        mounted() {
            //
        }
    }
</script>