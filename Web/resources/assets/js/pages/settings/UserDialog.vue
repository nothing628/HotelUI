<template>
    <v-dialog v-model="modal" max-width="600px">
        <v-card>
            <v-card-title><strong>{{ title }}</strong></v-card-title>
            <v-divider></v-divider>
            <v-card-text>
                <v-form v-model="valid" ref="form" lazy-validation>
                    <v-container fluid class="px-3">
                        <v-layout row wrap>
                            <v-flex xs12>
                                <v-text-field label="Username"
                                              :rules="rules.username"
                                              :counter="50"
                                              v-model="item_username"></v-text-field>
                            </v-flex>
                        </v-layout>
                        <v-layout row wrap>
                            <v-flex xs12>
                                <v-text-field label="Fullname"
                                              :rules="rules.username"
                                              :counter="50"
                                              v-model="item_fullname"></v-text-field>
                            </v-flex>
                        </v-layout>
                        <v-layout row wrap>
                            <v-flex xs12>
                                <v-text-field label="Password"
                                              :rules="rules.password"
                                              :counter="36"
                                              type="password"
                                              v-model="item_password"></v-text-field>
                            </v-flex>
                        </v-layout>
                        <v-layout row wrap>
                            <v-flex xs12>
                                <v-select v-bind:items="listRoles"
                                          v-model="item_role"
                                          label="Role"
                                          :rules="rules.role"
                                          item-value="Value"
                                          item-text="Role"></v-select>
                            </v-flex>
                        </v-layout>
                    </v-container>
                </v-form>
            </v-card-text>
            <v-divider></v-divider>
            <v-card-actions>
                <v-btn color="red darken-1" dark @click.native="cancel">Cancel</v-btn>
                <v-btn color="blue darken-1" dark @click.native="save">OK</v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
</template>
<script>
    export default {
        data() {
            return {
                modal: false,
                valid: false,
                item_id: '',
                item_username: '',
                item_fullname: '',
                item_password: '',
                item_role: 0,
                allowemptypass: false,
                rules: {
                    username: [
                        (v) => (!!v && v.length > 0) || 'This field is required',
                        (v) => (!!v && v.length <= 50) || 'Max 50 characters'
                    ],
                    password: [
                        (v) => ((!!v && v.length > 0) || this.allowemptypass) || 'This field is required',
                        (v) => ((!!v && v.length <= 36) || this.allowemptypass) || 'Max 36 characters'
                    ],
                    role: [
                        (v) => (v >= 0 && typeof (v) != 'string') || 'This field is required'
                    ],
                }
            }
        },
        props: {
            listRoles: { type: Array, required: true },
            name: { type: String, required: true },
            title: { type: String, default: 'New User' }
        },
        methods: {
            showDialog(param) {
                if (param.name != this.name)
                    return

                this.modal = true
                this.resetValidation()
                this.resetData()

                this.item_id = param.item_id || ''
                this.item_username = param.item_username || ''
                this.item_fullname = param.item_fullname || ''
                this.item_password = param.item_password || ''
                this.item_role = param.item_role || ''
                this.allowemptypass = param.emptypass || false
            },
            resetValidation() {
                this.$refs.form.reset()
            },
            resetData() {
                this.item_id = ''
                this.item_username = ''
                this.item_fullname = ''
                this.item_password = ''
                this.item_role = 0
                this.allowemptypass = false
            },
            save() {
                let data = {
                    item_id: this.item_id,
                    item_username: this.item_username,
                    item_fullname: this.item_fullname,
                    item_password: this.item_password,
                    item_role: this.item_role,
                }

                if (this.$refs.form.validate()) {
                    this.$emit('save', data)
                    this.modal = false
                }
            },
            cancel() {
                this.modal = false
            },
        },
        mounted() {
            this.$bus.$on('user-dialog', this.showDialog)
        },
        beforeDestroy() {
            this.$bus.$off('user-dialog', this.showDialog)
        }
    }
</script>
