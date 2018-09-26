<template>
  <div class="row">
    <div class="panel panel-inverse">
      <div class="panel-heading">
        <h4 class="panel-title">Create Guest</h4>
      </div>

      <div class="panel-body">
        <div class="form-horizontal">
          <div class="form-group">
            <label class="col-md-3 control-label">Id Number</label>
            <div class="col-md-3">
              <input class="form-control" />
            </div>
          </div>

          <div class="form-group">
            <label class="col-md-3 control-label">Fullname</label>
            <div class="col-md-4">
              <input class="form-control" />
            </div>
          </div>

          <div class="form-group">
            <label class="col-md-3 control-label">Email</label>
            <div class="col-md-4">
              <input class="form-control" />
            </div>
          </div>

          <div class="form-group">
            <label class="col-md-3 control-label">Birth Day</label>
            <div class="col-md-3">
              <input class="form-control" type="date" />
            </div>
            <label class="col-md-2 control-label">Birth Place</label>
            <div class="col-md-3">
              <input class="form-control" />
            </div>
          </div>

          <div class="form-group">
            <label class="col-md-3 control-label">Is VIP</label>
            <div class="col-md-5">
              <div class="checkbox checkbox-css checkbox-success">
                  <input type="checkbox" id="checkbox_css_2" value="">
                  <label for="checkbox_css_2">VIP</label>
              </div>
            </div>
          </div>

          <div class="form-group">
            <label class="col-md-3 control-label">Address</label>
            <div class="col-md-9">
              <textarea class="form-control"></textarea>
            </div>
          </div>

          <div class="form-group">
            <label class="col-md-3 control-label"></label>
            <div class="col-md-3">
              <input class="form-control" />
            </div>
            <div class="col-md-3">
              <input class="form-control" />
            </div>
            <div class="col-md-3">
              <input class="form-control" />
            </div>
          </div>

          <div class="form-group">
            <label class="col-md-3 control-label">Phone Number</label>
            <div class="col-md-2">
              <input class="form-control" />
            </div>
            <div class="col-md-2">
              <input class="form-control" />
            </div>
          </div>

          <div class="form-group">
            <label class="col-md-3 control-label">Document</label>
            <div class="col-md-3">
              <button class="btn btn-success btn-block">
                <i class="fa fa-upload"></i>
                Upload Photo
              </button>
            </div>
            <div class="col-md-3">
              <button class="btn btn-success btn-block">
                <i class="fa fa-upload"></i>
                Upload Document
              </button>
            </div>
          </div>
        </div>
      </div>

      <div class="panel-footer">
        <div class="row">
          <div class="col-md-2 col-md-offset-3">
            <button class="btn btn-success btn-block">
              <i class="fa fa-floppy-o"></i>
              Save
            </button>
          </div>
          <div class="col-md-2">
            <button class="btn btn-danger btn-block" @click="cancel">Cancel</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";

@Component
export default class CreateGuest extends Vue {
  cancel() {
    this.$router.push({ name: "data.guest" });
  }

  mounted() {
    this.$store.commit("changeTitle", "Create Guest");
    this.$store.commit("changeSubtitle", "");
  }
}
</script>

