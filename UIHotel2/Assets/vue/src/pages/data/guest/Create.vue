<template>
  <div class="row">
    <div class="panel panel-inverse">
      <div class="panel-heading">
        <h4 class="panel-title">Create Guest</h4>
      </div>

      <div class="panel-body">
        <div class="form-horizontal">
          <div class="form-group">
            <label class="col-md-3 control-label">(*) ID Number</label>
            <div class="col-md-3">
              <input class="form-control" v-model="modalData.IdNumber" maxlength="30" placeholder="ID Number"/>
            </div>
          </div>

          <div class="form-group">
            <label class="col-md-3 control-label">(*) Fullname</label>
            <div class="col-md-4">
              <input class="form-control" v-model="modalData.Fullname" maxlength="60" placeholder="Fullname"/>
            </div>
          </div>

          <div class="form-group">
            <label class="col-md-3 control-label">Email</label>
            <div class="col-md-4">
              <input class="form-control" v-model="modalData.Email" maxlength="100" placeholder="Email"/>
            </div>
          </div>

          <div class="form-group">
            <label class="col-md-3 control-label">(*) Birth Day</label>
            <div class="col-md-3">
              <input class="form-control" type="date" v-model="modalData.BirthDay" />
            </div>
            <label class="col-md-2 control-label">Birth Place</label>
            <div class="col-md-3">
              <input class="form-control" v-model="modalData.BirthPlace" maxlength="50" placeholder="Birth Place"/>
            </div>
          </div>

          <div class="form-group">
            <label class="col-md-3 control-label">Is VIP</label>
            <div class="col-md-5">
              <div class="checkbox checkbox-css checkbox-success">
                  <input type="checkbox" id="checkbox_css_2" value="VIP" v-model="modalData.IsVIP">
                  <label for="checkbox_css_2">VIP</label>
              </div>
            </div>
          </div>

          <div class="form-group">
            <label class="col-md-3 control-label">Address</label>
            <div class="col-md-9">
              <textarea class="form-control" v-model="modalData.Address" maxlength="255"></textarea>
            </div>
          </div>

          <div class="form-group">
            <label class="col-md-3 control-label"></label>
            <div class="col-md-3">
              <input class="form-control" v-model="modalData.City" maxlength="50" placeholder="City"/>
            </div>
            <div class="col-md-3">
              <input class="form-control" v-model="modalData.Province" maxlength="50" placeholder="Province"/>
            </div>
            <div class="col-md-3">
              <input class="form-control" v-model="modalData.State" maxlength="50" placeholder="State"/>
            </div>
          </div>

          <div class="form-group">
            <label class="col-md-3 control-label">Phone Number</label>
            <div class="col-md-2">
              <input class="form-control" v-model="modalData.Phone1" maxlength="15" placeholder="Phone 1 (*)"/>
            </div>
            <div class="col-md-2">
              <input class="form-control" v-model="modalData.Phone2" maxlength="15" placeholder="Phone 2"/>
            </div>
          </div>

          <div class="form-group">
            <label class="col-md-3 control-label">Document</label>
            <div class="col-md-3">
              <img class="img-responsive" :src="UrlPhoto" v-if="modalData.PhotoGuest != ''"/>
              <button class="btn btn-success btn-block" @click="UploadImg">
                <i class="fa fa-upload"></i>
                Upload Photo
              </button>
            </div>
            <div class="col-md-3">
              <label v-if="modalData.PhotoDoc != ''">{{ docName }}</label>
              <button class="btn btn-success btn-block" @click="UploadDoc">
                <i class="fa fa-upload"></i>
                Upload Document (*)
              </button>
            </div>
          </div>
        </div>
      </div>

      <div class="panel-footer">
        <div class="row">
          <div class="col-md-2 col-md-offset-3">
            <button class="btn btn-success btn-block" @click="save">
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

interface IModalData {
  IdNumber: string;
  Fullname: string;
  Email: string;
  IsVIP: boolean;
  BirthPlace: string;
  BirthDay: string;
  Phone1: string;
  Phone2: string;
  Address: string;
  City: string;
  Province: string;
  State: string;
  PhotoDoc: string;
  PhotoGuest: string;
}

@Component
export default class CreateGuest extends Vue {
  private docName: string = "";
  private modalData: IModalData = {
    IdNumber: "",
    Fullname: "",
    Email: "",
    IsVIP: false,
    BirthPlace: "",
    BirthDay: "",
    Phone1: "",
    Phone2: "",
    Address: "",
    City: "",
    Province: "",
    State: "",
    PhotoDoc: "",
    PhotoGuest: ""
  }

  get UrlPhoto(): string {
    if (this.modalData.PhotoGuest != "") {
      return window.CS.App.GetUploadUrl(this.modalData.PhotoGuest);
    }

    return "";
  }

  UploadImg() {
    window.CS.App.OpenDialog("Image File|*.png;*.jpg", this.handleImg);
  }

  UploadDoc() {
    window.CS.App.OpenDialog("PDF File |*.pdf", this.handleDoc);
  }

  handleImg(e: any): void {
    var filehash = e.hashname;

    this.modalData.PhotoGuest = filehash;
  }

  handleDoc(e: any): void {
    var filehash = e.hashname;
    var filename = e.filename;

    this.docName = filename;
    this.modalData.PhotoDoc = filehash;
  }

  save() {
    console.log(this.modalData);
  }

  cancel() {
    this.$router.push({ name: "data.guest" });
  }

  mounted() {
    this.$store.commit("changeTitle", "Create Guest");
    this.$store.commit("changeSubtitle", "");
  }
}
</script>

