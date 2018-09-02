<template>
    <div class="login bg-black animated fadeInDown">
        <!-- begin brand -->
        <div class="login-header">
            <div class="brand">
                <span class="logo"></span> ConsoleMY
                <small>Welcome to ConsoleMY</small>
            </div>
            <div class="icon">
                <i class="fa fa-sign-in"></i>
            </div>
        </div>
        <!-- end brand -->
        <div class="login-content">
            <form class="margin-bottom-0">
                <div class="form-group m-b-20">
                    <input class="form-control input-lg inverse-mode no-border" v-model="username" placeholder="Username" required="" type="text">
                </div>
                <div class="form-group m-b-20">
                    <input class="form-control input-lg inverse-mode no-border" v-model="password" placeholder="Password" required="" type="password">
                </div>
                <div class="login-buttons">
                    <button type="button" @click="submit" class="btn btn-success btn-block btn-lg">Sign in</button>
                </div>
            </form>
        </div>
    </div>
</template>
<script>
    export default {
        data() {
            return {
                username: '',
                password: '',
            }
        },
        methods: {
            submit() {
                var result = Application.UserValidate(this.username, this.password)

                if (result.success) {
                    let user = result.user

                    this.$store.commit('user/login', user)
                } else {
                    $.gritter.add({
                        title: 'Login Failed',
                        text: 'Please check your username or password',
                    })
                }
            }
        }
    }
</script>