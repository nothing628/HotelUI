<template>
    <v-card class="mb-4">
        <v-card-title primary-title>
            <h2 class="card-title">User List</h2>
        </v-card-title>

        <v-container fluid grid-list-md>
            <v-layout row>
                <v-flex md12>
                    <v-spacer></v-spacer>
                    <v-btn color="primary" @click.stop="getData">
                        <v-icon>search</v-icon>
                        <span>Search</span>
                    </v-btn>
                    <v-btn color="success" @click.stop="newUser">
                        <v-icon>create</v-icon>
                        <span>New User</span>
                    </v-btn>
                </v-flex>
            </v-layout>
            <v-layout row>
                <v-flex md12>
                    <v-data-table v-bind:headers="headers"
                                  v-bind:items="list_user"
                                  class="elevation-1">
                        <template slot="items" slot-scope="props">
                            <td>{{ props.item.Username }}</td>
                            <td>{{ props.item.Fullname }}</td>
                            <td><strong>{{ getRoleStr(props.item.Permission) }}</strong></td>
                            <td class="text-right">
                                <v-btn class="mx-0" icon fab color="teal" small flat>
                                    <v-icon>create</v-icon>
                                </v-btn>
                                <v-btn class="mx-0" icon fab color="yellow darken-2" small flat>
                                    <v-icon>refresh</v-icon>
                                </v-btn>
                                <v-btn class="mx-0" icon fab color="red" small flat>
                                    <v-icon>clear</v-icon>
                                </v-btn>
                            </td>
                        </template>
                    </v-data-table>
                </v-flex>
            </v-layout>
        </v-container>

        <alert></alert>
        <user name="newUser" :list-roles.sync="list_role"></user>
        <user name="editUser" :list-roles.sync="list_role"></user>
    </v-card>
</template>
<script>
    import axios from 'axios'
    import user from './UserDialog.vue'
    import alert from '../../components/Notification.vue'

    export default {
        components: {
            alert,
            user,
        },
        data() {
            return {
                item_username: '',
                list_user: [],
                list_role: [],
                headers: [
                    { text: 'Username', sortable: false, align: 'left', },
                    { text: 'Fullname', sortable: false, align: 'left', },
                    { text: 'Role', sortable: false, align: 'left', },
                    { text: 'Actions', sortable: false, align: 'right', },
                ],
            }
        },
        methods: {
            getRoleStr(val) {
                let role = this.list_role.filter(x => x.Value == val)
                
                if (role.length > 0)
                    return role[0].Role

                return ''
            },
            getData() {
                let data = {
                    item_username: this.item_username
                }

                axios.post('http://localhost.com/setting/post/GetUsers', data).then(this.getDataResponse)
            },
            getDataResponse(response) {
                let data = response.data

                if (data.success) {
                    let items = data.data
                    let roles = data.roles

                    this.list_user = []
                    this.list_role = []

                    roles.forEach(x => this.list_role.push(x))
                    items.forEach(x => this.list_user.push(x))
                }
            },
            newUser() {
                //
            }
        },
        mounted() {
            this.getData()
        }
    }
</script>
