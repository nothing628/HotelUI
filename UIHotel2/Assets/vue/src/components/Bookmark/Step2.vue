<template>
  <div class="form">
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
        <div class="col-md-3" v-if="!isSelected">
          <button class="btn btn-info" @click="Search">
            <i class="fa fa-search"></i>
            Search Guest
          </button>
        </div>
        <div class="col-md-3" v-if="isSelected">
          <button class="btn btn-warning" @click="unselectGuest">
            <i class="fa fa-times"></i>
            Unselect
          </button>
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

      <uiv-modal v-model="isShow" title="Search Guest" size="lg">
        <div class="row">
          <div class="col-md-12">
            <div class="form-inline">
              <input class="form-control" placeholder="Search Guest" v-model="keyword"/>
            </div>
          </div>
        </div>
        <div class="row m-t-10">
          <div class="col-md-12">
            <table class="table table-bordered">
              <thead>
                <tr>
                  <td>ID Number</td>
                  <td>Fullname</td>
                  <td>Email</td>
                  <td>Phone</td>
                </tr>
              </thead>
              <tbody>
                <tr class="selectable" v-for="item in items" :key="item.Id" @click="selectGuest(item)">
                  <td>{{ item.IdNumber }}</td>
                  <td>{{ item.Fullname }}</td>
                  <td>{{ item.Email }}</td>
                  <td>{{ item.Phone1 }} / {{ item.Phone2 }}</td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
        <div class="row">
          <div class="col-md-12 text-center">
            <pagination :total-page.sync="totalPage" nomargin v-model="page"></pagination>
          </div>
        </div>

        <template slot="footer">
          <button class="btn btn-danger" @click="hideModal">Cancel</button>
        </template>
      </uiv-modal>
    </div>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop, Watch } from "vue-property-decorator";
import { ss, se, execute } from "@/lib/Test";
import { IGuest, Guest } from "@/lib/Model/Guest";
import Pagination from "@/components/Table/Pagination.vue";
import moment from "moment";

interface IModalData {
  Id: string;
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

@Component({
  components: {
    Pagination
  }
})
export default class CreateStep2 extends Vue {
  private isShow: boolean = false;
  private keyword: string = "";
  private docName: string = "";
  private page: number = 1;
  private max_item: number = 0;
  private limit: number = 10;
  private items: Array<any> = new Array<any>();
  private modalData: IModalData = {
    Id: "",
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
  };

  get cleanKeyword(): string {
    return "%" + this.keyword + "%";
  }

  get UrlPhoto(): string {
    if (this.modalData.PhotoGuest != "") {
      return window.CS.App.GetUploadUrl(this.modalData.PhotoGuest);
    }

    return "";
  }

  get totalPage(): number {
    return Math.ceil(this.max_item / this.limit);
  }

  get offset(): number {
    return (this.page - 1) * this.limit;
  }

  get isSelected(): boolean {
    return this.modalData.Id != "";
  }

  hideModal() {
    this.isShow = false;
  }

  unselectGuest() {
    this.modalData.Id = "";
    this.modalData.Address = "";
    this.modalData.BirthPlace = "";
    this.modalData.BirthDay = "";
    this.modalData.City = "";
    this.modalData.Email = "";
    this.modalData.Fullname = "";
    this.modalData.IdNumber = "";
    this.modalData.IsVIP = false;
    this.modalData.Phone1 = "";
    this.modalData.Phone2 = "";
    this.modalData.PhotoDoc = "";
    this.modalData.PhotoGuest = "";
    this.modalData.Province = "";
    this.modalData.State = "";
  }

  selectGuest(item: any) {
    let birth = moment(item.BirthDay);

    this.modalData.Id = item.Id;
    this.modalData.Address = item.Address;
    this.modalData.BirthPlace = item.BirthPlace;
    this.modalData.BirthDay = birth.format("YYYY-MM-DD");
    this.modalData.City = item.City;
    this.modalData.Email = item.Email;
    this.modalData.Fullname = item.Fullname;
    this.modalData.IdNumber = item.IdNumber;
    this.modalData.IsVIP = item.IsVIP;
    this.modalData.Phone1 = item.Phone1;
    this.modalData.Phone2 = item.Phone2;
    this.modalData.PhotoDoc = item.PhotoDoc;
    this.modalData.PhotoGuest = item.PhotoGuest;
    this.modalData.Province = item.Province;
    this.modalData.State = item.State;
    this.isShow = false;
  }

  getMaxItem() {
    let qry = ss()
      .from("guests")
      .field("COUNT(*) as cnt");

    if (this.keyword != "") {
      qry.where(
        se()
          .and("Fullname LIKE ?", this.cleanKeyword)
          .or("Email LIKE ?", this.cleanKeyword)
          .or("IdNumber LIKE ?", this.cleanKeyword)
      );
    }

    let result = execute(qry);
    let first = result[0];

    this.max_item = Number(first.cnt);
  }

  getData() {
    let qry = ss()
      .from("guests")
      .limit(this.limit)
      .offset(this.offset);

    if (this.keyword != "") {
      qry.where(
        se()
          .and("Fullname LIKE ?", this.cleanKeyword)
          .or("Email LIKE ?", this.cleanKeyword)
          .or("IdNumber LIKE ?", this.cleanKeyword)
      );
    }

    let result = execute(qry);

    this.items = [];
    result.forEach((item: any) => {
      this.items.push(item);
    });
  }

  Search() {
    this.isShow = true;
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

  findOrSave() {
    this.$validator.validateAll().then((result: any) => {
      if (result) {
        if (this.isSelected) {
          //No need to save
        } else {
          //Save here
          Guest.Store(this.modalData);
        }
      }
    });
  }

  @Watch("keyword")
  onKeywordChange(newvalue: string, oldvalue: string) {
    this.getMaxItem();
    this.getData();
  }

  mounted() {
    this.getMaxItem();
    this.getData();
    window.bus.$on("book-validate", this.findOrSave);
  }

  beforeDestroy() {
    window.bus.$off("book-validate", this.findOrSave);
  }
}
</script>
