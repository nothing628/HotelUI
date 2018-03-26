<template>
    <v-card class="mb-4">
        <v-card-title primary-title>
            <h2 class="card-title">User List</h2>
        </v-card-title>

        <v-container fluid grid-list-sm>
            <v-layout row wrap>
                <v-flex md3>
                    <v-text-field append-icon="search"
                                  label="Search user"
                                  single-line
                                  hide-details
                                  @keyup="getData"
                                  v-model="item_username"></v-text-field>
                </v-flex>
                <v-flex md6>
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
                                <v-btn class="mx-0" icon fab color="teal" @click="editUser(props.item)" small flat>
                                    <v-icon>create</v-icon>
                                </v-btn>
                                <v-btn class="mx-0" icon fab color="red" @click="deleteUser(props.item)" small flat>
                                    <v-icon>clear</v-icon>
                                </v-btn>
                            </td>
                        </template>
                    </v-data-table>
                </v-flex>
            </v-layout>
        </v-container>

        <alert></alert>
        <user name="newUser" title="New User" @save="saveUser" :list-roles.sync="list_role"></user>
        <user name="editUser" title="Edit User" @save="updateUser" :list-roles.sync="list_role"></user>
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
                this.$bus.$emit('user-dialog', {name: 'newUser'})
            },
            editUser(data) {
                let newData = {
                    name: 'editUser',
                    item_id: data.Id,
                    item_username: data.Username,
                    item_fullname: data.Fullname,
                    item_password: '',
                    item_role: data.Permission,
                    emptypass: true,
                }

                this.$bus.$emit('user-dialog', newData)
            },
            saveUser(data) {
                axios.post('http://localhost.com/setting/post/SaveUser', data).then(this.dataResponse)
            },
            updateUser(data) {
                axios.post('http://localhost.com/setting/post/UpdateUser', data).then(this.dataResponse)
            },
            deleteUser(data) {
                let newData = {
                    item_id: data.Id
                }

                axios.post('http://localhost.com/setting/post/DeleteUser', newData).then(this.dataResponse)
            },
            dataResponse(response) {
                let data = response.data

                if (data.success) {
                    this.getData()
                    this.$bus.$emit('alert-show', { text: "Data Update Success", color: 'success', timeout: 6000 })
                } else {
                    this.$bus.$emit('alert-show', { text: "Data Update Failed!", color: 'error', timeout: 6000 })
                }
            }
        },
        mounted() {
            this.getData()
        }
    }
</script>
