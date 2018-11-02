<template>
  <div class="login bg-black animated fadeInDown">
    <!-- begin brand -->
    <div class="login-header">
      <div class="brand">
        <span class="logo"></span> {{ app_title }}
        <small>Welcome to HMS</small>
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
          <input class="form-control input-lg inverse-mode no-border" v-model="password" placeholder="Password" required="" type="password" @keypress="listenKey">
        </div>
        <div class="login-buttons">
          <button type="button" @click="submit" class="btn btn-success btn-block btn-lg">Sign in</button>
        </div>
      </form>
    </div>
  </div>
</template>
<style>
.login .login-header {
  width: 480px;
  margin-left: -240px;
}
</style>
<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import { isNullOrUndefined } from 'util';

@Component
export default class Login extends Vue {
  private username: string = "";
  private password: string = "";

  get app_title() {
    return this.$store.state.app_title;
  }

  listenKey(evt: KeyboardEvent) {
    if (evt.keyCode == 13) {
      this.submit();
    }
  }

  submit() {
    //logic login here
    var user = window.CS.Auth.Validate(this.username, this.password);

    if (isNullOrUndefined(user)) {
      window.bus.$emit("Notify", {
        Title: "Sign In",
        Content: "Username or Password wrong!",
        Type: "error"
      });
    } else {
      let fullname = user.Fullname;

      window.bus.$emit("Notify", {
        Title: "Sign In",
        Content: "Welcome back " + fullname,
        Type: "success"
      });

      this.$store.commit("User/login", {
        Id: user.Id,
        Username: user.Username,
        Fullname: user.Fullname,
        UserLevel: user.Level
      });
    }
  }
}
</script>
