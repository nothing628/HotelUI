<template>
  <uiv-modal v-model="show" title="Action" size="lg" @hide="closeAll">
    <step-selector>
      <step-item step-number="" step-title="Booking Info" step-target="booking" active></step-item>
      <step-item step-number="" step-title="Guest Info" step-target="guest"></step-item>
    </step-selector>
    <step-container name="booking" active>
      <div class="form-horizontal">
        <div class="form-group">
          <label class="col-md-3 control-label">Booking ID :</label>
          <div class="col-md-6">
            <input class="form-control" readonly v-model="booking_model.bookingId"/>
          </div>
        </div>
        <div class="form-group">
          <label class="col-md-3 control-label">Room Number :</label>
          <div class="col-md-3">
            <input class="form-control" readonly v-model="booking_model.roomNumber"/>
          </div>
          <div class="col-md-3">
            <input class="form-control" readonly v-model="booking_model.roomCategory"/>
          </div>
        </div>
        <div class="form-group">
          <label class="col-md-3 control-label">Status :</label>
          <div class="col-md-3">
            <input class="form-control" readonly v-model="status"/>
          </div>
        </div>
        <div class="form-group">
          <label class="col-md-3 control-label">Arrival Date :</label>
          <div class="col-md-3">
            <input class="form-control" readonly v-model="booking_model.arrivalDate" />
          </div>
          <label class="col-md-3 control-label">Departure Date :</label>
          <div class="col-md-3">
            <input class="form-control" readonly v-model="booking_model.departureDate" />
          </div>
        </div>
        <div class="form-group">
          <label class="col-md-3 control-label">Checkin Time :</label>
          <div class="col-md-3">
            <input class="form-control" readonly v-model="booking_model.checkinTime" />
          </div>
          <label class="col-md-3 control-label">Checkout Time :</label>
          <div class="col-md-3">
            <input class="form-control" readonly v-model="booking_model.checkoutTime" />
          </div>
        </div>
        <div class="form-group">
          <label class="col-md-3 control-label">Count Adult :</label>
          <div class="col-md-2">
            <input class="form-control" readonly v-model="booking_model.countAdult" />
          </div>
          <label class="col-md-2 control-label">Count Child :</label>
          <div class="col-md-2">
            <input class="form-control" readonly v-model="booking_model.countChild" />
          </div>
        </div>
        <div class="form-group">
          <label class="col-md-3 control-label">Note :</label>
          <div class="col-md-6">
            <textarea class="form-control" readonly v-model="booking_model.note"></textarea>
          </div>
        </div>
      </div>
    </step-container>
    <step-container name="guest">
      <div class="form-horizontal">
        <div class="form-group">
          <label class="control-label col-md-3">ID Number :</label>
          <div class="col-md-6">
            <input class="form-control" readonly v-model="guest_model.idNumber" />
          </div>
          <div class="col-md-3">
            <img class="img-responsive img-float" :src="UrlPhoto"/>
          </div>
        </div>
        <div class="form-group">
          <label class="control-label col-md-3">Fullname :</label>
          <div class="col-md-5">
            <input class="form-control" readonly v-model="guest_model.fullname" />
          </div>
        </div>
        <div class="form-group">
          <label class="control-label col-md-3">Email :</label>
          <div class="col-md-5">
            <input class="form-control" readonly v-model="guest_model.email" />
          </div>
        </div>
        <div class="form-group">
          <label class="control-label col-md-3">Address :</label>
          <div class="col-md-6">
            <textarea class="form-control" readonly v-model="guest_model.address"></textarea>
          </div>
        </div>
        <div class="form-group">
          <label class="control-label col-md-3">Phone Number :</label>
          <div class="col-md-3">
            <input class="form-control" readonly v-model="guest_model.phone1" />
          </div>
          <div class="col-md-3">
            <input class="form-control" readonly v-model="guest_model.phone2" />
          </div>
        </div>
        <div class="form-group">
          <div class="col-md-3 col-md-offset-3">
            <button class="btn btn-link">Detail Guest</button>
          </div>
        </div>
      </div>
    </step-container>

    <div class="row" slot="footer">
      <div class="col-md-3">
        <button class="btn btn-block btn-warning" @click="Invoice">
          <i class="fa fa-book"></i> Invoice
        </button>
      </div>
      <div class="col-md-3">
        <button class="btn btn-block btn-success" @click="Checkin" :disabled="disableCheckin">
          <i class="fa fa-sign-in"></i> Checkin
        </button>
      </div>
      <div class="col-md-3">
        <button class="btn btn-block btn-success" @click="Checkout" :disabled="disableCheckout">
          <i class="fa fa-sign-out"></i> Checkout
        </button>
      </div>
      <div class="col-md-3">
        <button class="btn btn-block btn-danger" @click="cancelBooking" :disabled="disableCancel">
          <i class="fa fa-times"></i> Cancel Booking
        </button>
      </div>
    </div>
  </uiv-modal>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import { ss, execute } from "@/lib/Test";
import StepItem from "@/components/Steps/StepItem.vue";
import StepSelector from "@/components/Steps/StepSelector.vue";
import StepContainer from "@/components/Steps/StepContainer.vue";
import moment from "moment";
import swal, { SweetAlertResult, SweetAlertOptions } from "sweetalert2";
import { Invoice, Booking, BookStatusType } from "@/lib/Model/ModelCollection";
import { isNullOrUndefined } from "util";
import { RawLocation } from "vue-router";

interface IBookingModel {
  bookingId: string;
  guestId: number;
  roomId: number;
  roomNumber: string;
  roomCategory: string;
  arrivalDate: string;
  departureDate: string;
  checkinTime?: string;
  checkoutTime?: string;
  countAdult: number;
  countChild: number;
  note: string;
}

interface IGuestModel {
  idNumber: string;
  fullname: string;
  email: string;
  address: string;
  phone1: string;
  phone2: string;
  photoGuest: string;
}

@Component({
  components: {
    StepItem,
    StepSelector,
    StepContainer
  }
})
export default class BookingDetail extends Vue {
  private show: boolean = true;
  private booking_model: IBookingModel = {
    bookingId: "",
    guestId: 0,
    roomId: 0,
    roomNumber: "",
    roomCategory: "",
    arrivalDate: "",
    departureDate: "",
    checkinTime: undefined,
    checkoutTime: undefined,
    countAdult: 0,
    countChild: 0,
    note: ""
  };

  private guest_model: IGuestModel = {
    idNumber: "",
    fullname: "",
    email: "",
    phone1: "",
    phone2: "",
    address: "",
    photoGuest: ""
  };

  get UrlPhoto(): string {
    if (this.guest_model.photoGuest != "") {
      return window.CS.App.GetUploadUrl(this.guest_model.photoGuest);
    }

    return "";
  }

  get checkoutTime(): string {
    return this.$store.state.Setting.Time_Checkout;
  }

  get checkinTime(): string {
    return this.$store.state.Setting.Time_Checkin;
  }

  get status(): string {
    return this.getStateName(this.booking_model);
  }

  get disableCheckin(): boolean {
    var state: number = this.getStateNum(this.booking_model);
    return state >= 2;
  }

  get disableCheckout(): boolean {
    var state: number = this.getStateNum(this.booking_model);
    return state <= 2 || state == 6;
  }

  get disableCancel(): boolean {
    var state: number = this.getStateNum(this.booking_model);
    return state >= 3;
  }

  @Prop({ required: true })
  public bookingId?: string;

  getStateName(item: any): string {
    var state: BookStatusType = this.getStateNum(item);

    return Booking.GetStatusName(state);
  }

  getStateNum(item: any): BookStatusType {
    return Booking.GetStatus(
      item.arrivalDate,
      item.departureDate,
      item.checkinTime,
      item.checkoutTime
    );
  }

  public Invoice(): void {
    let invoiceLocation: RawLocation = {
      name: "data.invoice",
      params: {
        id: this.booking_model.bookingId
      }
    };

    this.closeAll();
    this.$nextTick(() => this.$router.push(invoiceLocation));
  }

  public Checkout(): void {
    let options: SweetAlertOptions = {
      title: "Checkout",
      text: "Are you sure to checkout this booking ?",
      type: "question",
      showConfirmButton: true,
      showCancelButton: true,
      cancelButtonClass: "bg-red darker-1"
    };

    swal(options).then((value: SweetAlertResult) => {
      if (value.value) {
        Invoice.Recalculate(this.booking_model.bookingId, () => {
          Booking.Checkout(this.booking_model.bookingId);
        });
      }
    });
  }

  public Checkin(): void {
    Booking.Checkin(this.booking_model.bookingId);
    this.refreshAndClose();
  }

  public cancelBooking(): void {
    Booking.Cancel(this.booking_model.bookingId);
    this.refreshAndClose();
  }

  public getDataBooking(): void {
    let qry = ss()
      .from("bookings", "a")
      .join("rooms", "b", "a.RoomId = b.Id")
      .join("roomcategories", "c", "b.RoomCategoryId = c.Id")
      .field("a.Id")
      .field("a.ArrivalDate")
      .field("a.DepartureDate")
      .field("a.CheckinAt")
      .field("a.CheckoutAt")
      .field("a.CountAdult")
      .field("a.CountChild")
      .field("a.Note")
      .field("a.GuestId")
      .field("b.Id", "RoomId")
      .field("b.RoomNumber")
      .field("c.CategoryName", "RoomCategory")
      .where("a.Id = ?", this.bookingId);
    let result = execute(qry);

    if (result.length == 0) {
      return;
    }

    let first = result[0];
    let arrival = moment(first.ArrivalDate.toISOString());
    let depature = moment(first.DepartureDate);
    let checkin = moment(first.CheckinAt);
    let checkout = moment(first.CheckoutAt);

    this.booking_model.bookingId = first.Id;
    this.booking_model.guestId = Number(first.GuestId);
    this.booking_model.roomId = Number(first.RoomId);
    this.booking_model.roomNumber = first.RoomNumber;
    this.booking_model.roomCategory = first.RoomCategory;
    this.booking_model.arrivalDate = arrival.format("DD/MM/YYYY");
    this.booking_model.departureDate = depature.format("DD/MM/YYYY");
    this.booking_model.checkinTime = checkin.isValid() ? checkin.format("DD/MM/YYYY HH:mm"): undefined;
    this.booking_model.checkoutTime = checkout.isValid() ? checkout.format("DD/MM/YYYY HH:mm"): undefined;
    this.booking_model.countAdult = Number(first.CountAdult);
    this.booking_model.countChild = Number(first.CountChild);
    this.booking_model.note = first.Note;
    this.getDataGuest();
  }

  public getDataGuest(): void {
    let qry = ss()
      .from("guests")
      .field("IdNumber")
      .field("Fullname")
      .field("Email")
      .field("Phone1")
      .field("Phone2")
      .field("PhotoGuest")
      .field("City")
      .field("Province")
      .field("State")
      .field("Address")
      .where("Id = ?", this.booking_model.guestId);
    let result = execute(qry);
    let first = result[0];
    let fulladdress = first.Address + ", ";
    fulladdress += first.City + ", ";
    fulladdress += first.Province + ", ";
    fulladdress += first.State;

    this.guest_model.idNumber = first.IdNumber;
    this.guest_model.fullname = first.Fullname;
    this.guest_model.email = first.Email;
    this.guest_model.phone1 = first.Phone1;
    this.guest_model.phone2 = first.Phone2;
    this.guest_model.address = fulladdress;
    this.guest_model.photoGuest = first.PhotoGuest;
  }

  public refreshAndClose() {
    this.$emit("refresh");
    this.closeAll();
  }

  public closeAll(): void {
    this.$emit("close");
  }

  mounted() {
    // refresh detail
    this.getDataBooking();
  }
}
</script>
