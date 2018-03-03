<template>
    <v-card class="mb-4">
        <v-card-title primary-title>
            <h2 class="card-title">Application Setting</h2>
        </v-card-title>

        <v-container fluid grid-list-md>
            <v-layout row>
                <v-flex md12>
                    <v-form v-model="valid" ref="form" lazy-validation>
                        <v-layout row>
                            <v-flex md3>
                                <v-subheader>Database Host</v-subheader>
                            </v-flex>
                            <v-flex md6>
                                <v-text-field hint="Database Host" persistent-hint v-model="SQL_Server"></v-text-field>
                            </v-flex>
                            <v-flex md3>
                                <v-text-field hint="Database Port" type="number" persistent-hint v-model="SQL_Port"></v-text-field>
                            </v-flex>
                        </v-layout>
                        <v-layout row>
                            <v-flex md3>
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
                            <v-flex md3>
                                <v-subheader>Database Name</v-subheader>
                            </v-flex>
                            <v-flex md4>
                                <v-text-field hint="Database Name" persistent-hint v-model="SQL_Database"></v-text-field>
                            </v-flex>
                        </v-layout>

                        <v-btn @click.stop="testConnect">
                            <span>Test Connection</span>
                        </v-btn>
                        <v-btn color="success" @click.stop="setData">
                            <span>Save Setting</span>
                            <v-icon right>done_all</v-icon>
                        </v-btn>
                    </v-form>
                </v-flex>
            </v-layout>
        </v-container>

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
                SQL_Database: '',
                SQL_Server: '',
                SQL_Port: 3600,
                SQL_User: '',
                SQL_Password: '',
            }
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

                axios.post('http://localhost.com/setting/post/SaveSettingApp', data).then(this.setDataResponse);
            },
            setDataResponse(response) {
                let data = response.data

                if (data.success) {
                    //
                } else {
                    //
                }
            },
            testConnect() {
                //
            }
        },
        mounted() {
            this.getData()
        }
    }
</script>
