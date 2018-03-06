<template>
    <v-card class="mb-4">
        <v-card-title primary-title>
            <h5>Installation Step 1</h5>
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
            <v-btn color="red darken-1" dark @click.native="cancel">Cancel</v-btn>
            <v-btn color="green" dark @click.native="next">Next</v-btn>
        </v-card-actions>
        <alert></alert>
    </v-card>
</template>
<script>
    import axios from 'axios'
    import alert from '../../components/Notification.vue'
    export default {
        components: {
            alert
        },
        data() {
            return {
                valid: false,
                SQL_Server: '',
                SQL_Port: 0,
                SQL_Database: '',
                SQL_User: '',
                SQL_Password: '',
                rules: {
                    required: [
                        (v) => (!!v && v.length > 0) || 'This field required!'
                    ]
                }
            }
        },
        props: {
            nextStep: { type: String, required: true }
        },
        methods: {
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

                if (data.success) {
                    // Redirect to next page
                    this.nextPage()
                } else {
                    this.$bus.$emit('alert-show', { text: "Save Failed!", color: 'error', timeout: 6000 })
                }
            },
            nextPage() {
                window.location = this.nextStep
            },
            next() {
                window.CS.getObjectParam("SettingModel", "CheckConnection",
                    this.SQL_Server,
                    this.SQL_Port,
                    this.SQL_User,
                    this.SQL_Password,
                    this.SQL_Database
                ).then(e => {
                    var retObj = JSON.parse(e)

                    if (retObj)
                        this.setData()
                    else
                        this.$bus.$emit('alert-show', { text: "Connection Failed! Couldn't save setting", color: 'error', timeout: 6000 })
                }).catch(e => {
                });
            },
            cancel() {
                // Exit setup
                window.CS.close()
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
            }
        },
        mounted() {
            this.getData()
        }
    }
</script>
