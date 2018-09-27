<template>
  <div class="row">
    <div class="panel panel-inverse">
      <div class="panel-heading">
        <h4 class="panel-title">Edit Guest</h4>
      </div>

      <div class="panel-body">
        <div class="form-horizontal">
          <div class="form-group">
            <label class="col-md-3 control-label">(*) ID Number</label>
            <div class="col-md-3">
              <input
              class="form-control"
              name="id_number"
              v-validate="'required|max:30'"
              v-model="modalData.IdNumber"
              placeholder="ID Number"/>
              <span class="text-danger">{{ errors.first('id_number') }}</span>
            </div>
          </div>

          <div class="form-group">
            <label class="col-md-3 control-label">(*) Fullname</label>
            <div class="col-md-4">
              <input
              class="form-control"
              name="fullname"
              v-validate="'required|max:60'"
              v-model="modalData.Fullname"
              placeholder="Fullname"/>
              <span class="text-danger">{{ errors.first('fullname') }}</span>
            </div>
          </div>

          <div class="form-group">
            <label class="col-md-3 control-label">Email</label>
            <div class="col-md-4">
              <input
              class="form-control"
              name="email"
              v-validate="'email|max:100'"
              v-model="modalData.Email"
              placeholder="Email"/>
              <span class="text-danger">{{ errors.first('email') }}</span>
            </div>
          </div>

          <div class="form-group">
            <label class="col-md-3 control-label">(*) Birth Day</label>
            <div class="col-md-3">
              <input
              class="form-control"
              name="birth_day"
              type="date"
              v-validate="'required'"
              v-model="modalData.BirthDay" />
              <span class="text-danger">{{ errors.first('birth_day') }}</span>
            </div>
            <label class="col-md-2 control-label">Birth Place</label>
            <div class="col-md-3">
              <input
              class="form-control"
              name="birth_place"
              v-validate="'max:50'"
              v-model="modalData.BirthPlace"
              placeholder="Birth Place"/>
              <span class="text-danger">{{ errors.first('birth_place') }}</span>
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
              <textarea
              class="form-control"
              name="address"
              v-validate="'max:255'"
              v-model="modalData.Address"></textarea>
              <span class="text-danger">{{ errors.first('address') }}</span>
            </div>
          </div>

          <div class="form-group">
            <label class="col-md-3 control-label"></label>
            <div class="col-md-3">
              <input
              class="form-control"
              name="city"
              v-validate="'max:50'"
              v-model="modalData.City"
              placeholder="City"/>
              <span class="text-danger">{{ errors.first('city') }}</span>
            </div>
            <div class="col-md-3">
              <input
              class="form-control"
              name="province"
              v-validate="'max:50'"
              v-model="modalData.Province"
              placeholder="Province"/>
              <span class="text-danger">{{ errors.first('province') }}</span>
            </div>
            <div class="col-md-3">
              <input
              class="form-control"
              name="state"
              v-validate="'max:50'"
              v-model="modalData.State"
              placeholder="State"/>
              <span class="text-danger">{{ errors.first('state') }}</span>
            </div>
          </div>

          <div class="form-group">
            <label class="col-md-3 control-label">Phone Number</label>
            <div class="col-md-2">
              <input 
              class="form-control" 
              name="phone_1"
              v-validate="'required|max:15'"
              v-model="modalData.Phone1"
              placeholder="Phone 1 (*)"/>
              <span class="text-danger">{{ errors.first('phone_1') }}</span>
            </div>
            <div class="col-md-2">
              <input
              class="form-control"
              name="phone_2"
              v-validate="'max:15'"
              v-model="modalData.Phone2"
              placeholder="Phone 2"/>
              <span class="text-danger">{{ errors.first('phone_2') }}</span>
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
              <input type="hidden" v-model="modalData.PhotoDoc" name="document" v-validate="'required'"/>
              <button class="btn btn-success btn-block" @click="UploadDoc">
                <i class="fa fa-upload"></i>
                Upload Document (*)
              </button>
              <span class="text-danger">{{ errors.first('document') }}</span>
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
import { ss, su, execute, executeScalar } from "@/lib/Test";
import moment from "moment";

interface IModalData {
  Id: number;
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
    PhotoGuest: "",
    Id: 0
  };

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
    this.$validator.validateAll().then((result: any) => {
      if (result) this.saveData();
    });
  }

  saveData() {
    var new_moment = moment();
    var qry = su()
      .table("guests")
      .set("IdNumber", this.modalData.IdNumber)
      .set("Fullname", this.modalData.Fullname)
      .set("Email", this.modalData.Email)
      .set("IsVIP", this.modalData.IsVIP ? 1 : 0)
      .set("BirthPlace", this.modalData.BirthPlace)
      .set("BirthDay", this.modalData.BirthDay)
      .set("Phone1", this.modalData.Phone1)
      .set("Phone2", this.modalData.Phone2)
      .set("Address", this.modalData.Address)
      .set("City", this.modalData.City)
      .set("Province", this.modalData.Province)
      .set("State", this.modalData.State)
      .set("PhotoDoc", this.modalData.PhotoDoc)
      .set("PhotoGuest", this.modalData.PhotoGuest)
      .set("CreateAt", new_moment.format("YYYY-MM-DD"))
      .set("UpdateAt", new_moment.format("YYYY-MM-DD"))
      .where("Id = ?", this.modalData.Id);
    var result = executeScalar(qry);

    this.$router.push({ name: "data.guest" });
  }

  cancel() {
    this.$router.push({ name: "data.guest" });
  }

  getData() {
    let id = this.$route.params.id;
    let qry = ss()
      .from("guests")
      .where("Id = ?", id)
      .limit(1);
    let result = execute(qry);

    if (result.length > 0) {
      let item = result[0];
      let birth_day = item.BirthDay;

      this.modalData.Id = item.Id;
      this.modalData.IdNumber = item.IdNumber;
      this.modalData.Fullname = item.Fullname;
      this.modalData.Email = item.Email;
      this.modalData.BirthPlace = item.BirthPlace;
      this.modalData.IsVIP = item.IsVIP;
      this.modalData.Address = item.Address;
      this.modalData.City = item.City;
      this.modalData.Province = item.Province;
      this.modalData.State = item.State;
      this.modalData.Phone1 = item.Phone1;
      this.modalData.Phone2 = item.Phone2;
      this.modalData.PhotoDoc = item.PhotoDoc;
      this.modalData.PhotoGuest = item.PhotoGuest;
      this.docName = item.PhotoDoc;

      if (typeof birth_day == "object") {
        this.modalData.BirthDay = moment(birth_day).format("YYYY-MM-DD");
      }
    }
  }

  mounted() {
    this.$store.commit("changeTitle", "Edit Guest");
    this.$store.commit("changeSubtitle", "");
    this.getData();
  }
}
</script>
