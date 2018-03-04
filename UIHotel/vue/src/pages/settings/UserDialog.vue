<template>
    <div></div>
</template>
<script>
    export default {
        data() {
            return {
                modal: false,
                item_username: '',
                item_fullname: '',
                item_password: '',
                item_role: 0,
            }
        },
        props: {
            listRoles: { type: Array, required: true },
            name: { type: String, required: true },
        },
        methods: {
            cancelDialog() {
                this.modal = false
            },
            showDialog(param) {
                //
            },
            resetValidation() {
                //
            },
            resetData() {
                //
            },
            save() {
                //
            }
        },
        mounted() {
            //
        }
    }
</script>
