<template>
  <div class="row">
    <div class="panel panel-inverse">
      <div class="panel-heading">
        <h4 class="panel-title">User Setting</h4>
      </div>
      <div class="panel-body">
        <div class="dataTables_wrapper form-inline dt-bootstrap no-footer">
          <div class="dt-buttons btn-group m-b-10">
            <a class="btn btn-success buttons-copy buttons-html5 btn-sm" @click="addData">
              <i class="fa fa-plus"></i>
              <span> New User</span>
            </a>
          </div>
          <div class="dataTables_filter">
            <label>Search:<input type="text" class="form-control input-sm" v-model="keyword"/></label>
          </div>
          <table class="table table-striped table-bordered dataTable no-footer dtr-inline" role="grid">
            <thead>
                <tr role="row">
                  <th>User</th>
                  <th>Fullname</th>
                  <th>Level</th>
                  <th>Active</th>
                  <th>Action</th>
                </tr>
            </thead>
            <tbody>
              <tr v-for="item in items" :key="item.Id">
                  <td>{{ item.TransactionAt | strdate("DD/MM HH:mm") }}</td>
                  <td>{{ item.Description }}</td>
                  <td>{{ item.CategoryName }}</td>
                  <td>{{ item.AmmountIn | strcurrency }}</td>
                  <td>{{ item.AmmountOut | strcurrency }}</td>
                  <td>{{ item.Subtotal | strcurrency }}</td>
                  <td>
                    <div class="btn-group pull-right">
                      <button class="btn btn-sm btn-warning" :disabled="canEdit(item)" @click="editData(item)"><i class="fa fa-pencil"></i> Edit</button>
                      <button class="btn btn-sm btn-danger" :disabled="canEdit(item)" @click="deleteData(item)"><i class="fa fa-trash"></i> Delete</button>
                    </div>
                  </td>
              </tr>
            </tbody>
          </table>
          <counter :from.sync="from" :to.sync="to" :total.sync="max_item"></counter>
          <pagination :total-page.sync="totalPage" v-model="current_page"></pagination>
        </div>

        <uiv-modal title="Add new user" v-model="show_add" @hide="closeAll">
          <div class="form form-horizontal">
            <div class="form-group">
              <label class="col-md-3 control-label">Fullname</label>
              <div class="col-md-6">
                <input
                type="text"
                class="form-control"
                name="fullname"
                v-model="model_data.Fullname"
                v-validate="'required'"/>
                <span class="text-danger">{{ errors.first('fullname') }}</span>
              </div>
            </div>
            <div class="form-group">
              <label class="col-md-3 control-label">Username</label>
              <div class="col-md-5">
                <input
                type="text"
                class="form-control"
                name="username"
                v-model="model_data.Username"
                v-validate="'required'"/>
                <span class="text-danger">{{ errors.first('username') }}</span>
              </div>
            </div>
            <div class="form-group">
              <label class="col-md-3 control-label">Password</label>
              <div class="col-md-5">
                <input
                type="password"
                class="form-control"
                name="password"
                v-model="model_data.Password"
                v-validate="'required'"/>
                <span class="text-danger">{{ errors.first('password') }}</span>
              </div>
            </div>
            <div class="form-group">
              <label class="col-md-3 control-label">Level</label>
              <div class="col-md-4">
                <select class="form-control" v-model="model_data.Level">
                  <option value="0">Administrator</option>
                  <option value="1">Manager</option>
                  <option value="2">Receptionist</option>
                  <option value="3">Cleaner</option>
                </select>
              </div>
            </div>
          </div>

          <template slot="footer">
            <button class="btn btn-success" @click="storeData">Save</button>
            <button class="btn btn-danger" @click="closeAll">Cancel</button>
          </template>
        </uiv-modal>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import Pagination from "@/components/Table/Pagination.vue";
import Counter from "@/components/Table/Counter.vue";

interface IUserModel {
  Fullname: string;
  Username: string;
  Password: string;
  Level: number;
}

@Component({
  components: {
    Pagination,
    Counter
  }
})
export default class SettingUser extends Vue {
  private show_add: boolean = false;
  private show_edit: boolean = false;
  private keyword: string = "";
  private items: Array<any> = new Array<any>();
  private max_item: number = 0;
  private limit: number = 20;
  private current_page: number = 1;
  private model_data: IUserModel = {
    Fullname: "",
    Username: "",
    Password: "",
    Level: 2
  };

  get from(): number {
    return this.offset + 1;
  }

  get to(): number {
    let value = this.from + this.limit - 1;

    if (value > this.max_item) {
      return this.max_item;
    }

    return value;
  }

  get offset(): number {
    return (this.current_page - 1) * this.limit;
  }

  get totalPage(): number {
    return Math.ceil(this.max_item / this.limit);
  }

  closeAll() {
    this.show_add = false;
    this.show_edit = false;
  }

  editData(item: any) {
    //
  }

  storeData() {
    this.$validator.validateAll().then((is_valid: boolean) => {
      if (is_valid) {
        this.storeDataExe();
      }
    });
  }

  storeDataExe() {
    //
  }

  updateData() {
    //
  }

  addData() {
    this.model_data.Username = "";
    this.model_data.Password = "";
    this.model_data.Level = 2;
    this.model_data.Fullname = "";
    this.show_add = true;
    this.$validator.reset();
  }

  mounted() {
    this.$store.commit("changeTitle", "User Setting");
    this.$store.commit("changeSubtitle", "");
  }
}
</script>
