<template>
    <div class="login__block active" id="l-login">
        <div class="login__block__header">
            <i class="zmdi zmdi-account-circle"></i>
            Please Sign in
        </div>

        <div class="login__block__body">
            <div class="form-group form-group--float form-group--centered">
                <input class="form-control" type="text" v-model="username">
                <label>User Account</label>
                <i class="form-group__bar"></i>
            </div>

            <div class="form-group form-group--float form-group--centered">
                <input class="form-control" type="password" v-model="password" @keyup="keyup">
                <label>Password</label>
                <i class="form-group__bar"></i>
            </div>

            <button class="btn btn--icon green login__block__btn waves-effect" @click="postLogin">
                <v-icon>send</v-icon>
            </button>
            <button class="btn btn--icon blue login__block__btn waves-effect" @click="showSetting">
                <v-icon>settings</v-icon>
            </button>
        </div>

        <v-dialog v-model="dialog"
                  fullscreen
                  transition="dialog-bottom-transition"
                  :overlay="false"
                  scrollable>
            <v-container fluid grid-list-md>
                <v-layout row justify-center>
                    <v-flex lg6 md6 sm12 xs12>
                        <v-card class="mb-4">
                            <v-card-title primary-title>
                                <h5>Database Setting</h5>
                            </v-card-title>
                            <v-divider></v-divider>
                            <v-card-text>
                                <v-form v-model="valid" ref="form" lazy-validation>
                                    <v-layout row>
                                        <v-flex md4>
                                            <v-subheader>Database Host</v-subheader>
                                        </v-flex>
                                        <v-flex md5>
                                            <v-text-field hint="Database Host" persistent-hint v-model="SQL_Server"></v-text-field>
                                        </v-flex>
                                        <v-flex md3>
                                            <v-text-field hint="Database Port" type="number" persistent-hint v-model="SQL_Port"></v-text-field>
                                        </v-flex>
                                    </v-layout>
                                    <v-layout row>
                                        <v-flex md4>
                                            <v-subheader>Database Credential</v-subheader>
                                        </v-flex>
                                        <v-flex md4>
                                            <v-text-field hint="Database Username" persistent-hint v-model="SQL_User"></v-text-field>
                                        </v-flex>
                                        <v-flex md4>
                                            <v-text-field hint="Database Password" type="password" persistent-hint v-model="SQL_Password"></v-text-field>
                                        </v-flex>
                                    </v-layout>
                                    <v-layout row>
                                        <v-flex md4>
                                            <v-subheader>Database Name</v-subheader>
                                        </v-flex>
                                        <v-flex md4>
                                            <v-text-field hint="Database Name" persistent-hint v-model="SQL_Database"></v-text-field>
                                        </v-flex>
                                    </v-layout>
                                    <v-layout row>
                                        <v-flex md4 offset-md4>
                                            <v-btn @click="test" color="green" dark>
                                                <span>Test Connection</span>
                                            </v-btn>
                                        </v-flex>
                                    </v-layout>
                                </v-form>
                            </v-card-text>
                            <v-divider></v-divider>
                            <v-card-actions>
                                <v-btn color="red darken-1" dark @click.native="dialog = false" class="text--black">Cancel</v-btn>
                                <v-btn color="green" dark @click.native="setData">Save</v-btn>
                            </v-card-actions>
                        </v-card>
                    </v-flex>
                </v-layout>
            </v-container>
        </v-dialog>
        <alert></alert>
    </div>
</template>
<script>
    import axios from 'axios'
    import alert from '../components/Notification.vue'

    export default {
        components: {
            alert
        },
        data() {
            return {
                dialog: false,
                username: '',
                password: '',
                SQL_Server: '',
                SQL_Port: 0,
                SQL_Database: '',
                SQL_User: '',
                SQL_Password: '',
            }
        },
        methods: {
            showSetting() {
                this.dialog = true
            },
            getData() {
                axios.post('http://localhost.com/setting/post/GetSettingApp').then(this.getDataResponse)
            },
            getDataResponse(response) {
                let data = response.data

                if (data.success) {
                    this.SQL_Database = data.SQL_Database
                    this.SQL_Server = data.SQL_Server
                    this.SQL_Port = data.SQL_Port
                    this.SQL_User = data.SQL_User
                    this.SQL_Password = data.SQL_Password
                }
            },
            setData() {
                let data = {
                    SQL_Database: this.SQL_Database,
                    SQL_Server: this.SQL_Server,
                    SQL_Port: this.SQL_Port,
                    SQL_User: this.SQL_User,
                    SQL_Password: this.SQL_Password,
                }

                axios.post('http://localhost.com/setup/api/SaveSettingDB', data).then(this.setDataResponse);
            },
            setDataResponse(response) {
                let data = response.data

                if (!data.success)
                    this.$bus.$emit('alert-show', { text: "Save Failed!", color: 'error', timeout: 6000 })

                this.dialog = false
            },
            test() {
                window.CS.getObjectParam("SettingModel", "CheckConnection",
                    this.SQL_Server,
                    this.SQL_Port,
                    this.SQL_User,
                    this.SQL_Password,
                    this.SQL_Database
                ).then(this.testResult).catch(e => { });
            },
            testResult(e) {
                var retObj = JSON.parse(e)

                if (retObj)
                    this.$bus.$emit('alert-show', { text: "Connection Success!", color: 'success', timeout: 6000 })
                else
                    this.$bus.$emit('alert-show', { text: "Connection Failed!", color: 'error', timeout: 6000 })
            },
            postLogin() {
                let data = {
                    username: this.username,
                    password: this.password,
                }

                if (data.username != '' && data.password != '')
                    axios.post('http://localhost.com/home/post/postLogin', data).then(this.postLoginData)
                else
                    this.$bus.$emit('alert-show', { text: 'Please fill username and password', color: 'red', timeout: 6000 })
            },
            postLoginData(response) {
                let data = response.data
                
                if (data.success) {
                    //redirect
                    window.location = 'http://localhost.com/home/get/index'
                } else {
                    //show warning
                    this.$bus.$emit('alert-show', { text: 'Your username or password wrong!', color: 'red', timeout: 6000 })
                }
            },
            keyup(e) {
                if (e.keyCode == 13)
                    this.postLogin()
            }
        },
        mounted() {
            this.getData()
        }
    }
</script>
