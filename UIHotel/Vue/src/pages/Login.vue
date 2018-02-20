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
                <input class="form-control" type="password" v-model="password">
                <label>Password</label>
                <i class="form-group__bar"></i>
            </div>

            <button class="btn btn--icon login__block__btn waves-effect" @click="postLogin"><i class="zmdi zmdi-long-arrow-right"></i></button>
        </div>

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
                username: '',
                password: '',
            }
        },
        methods: {
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
            }
        }
    }
</script>
