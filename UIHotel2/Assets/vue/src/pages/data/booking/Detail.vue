<template>
  <uiv-modal v-model="show" title="Action" size="lg">
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
          <div class="col-md-6">
            <a class="btn btn-link">{{ booking_model.roomNumber }} {{ booking_model.roomCategory }}</a>
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
        <button class="btn btn-block btn-primary">Invoice</button>
      </div>
      <div class="col-md-3">
        <button class="btn btn-block btn-success">Checkin</button>
      </div>
      <div class="col-md-3">
        <button class="btn btn-block btn-success">Checkout</button>
      </div>
      <div class="col-md-3">
        <button class="btn btn-block btn-danger" @click="closeAll">Close</button>
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
import { isNullOrUndefined } from "util";

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

  @Prop({ required: true })
  public bookingId?: string;

  getStateName(item: any): string {
    var state: number = this.getStateNum(item);

    switch (state) {
      case 0:
        return "Booking";
      case 1:
        return "Should Checkin";
      case 2:
        return "Late Checkin";
      case 3:
        return "Checkin";
      case 4:
        return "Should Checkout";
      case 5:
        return "Late Checkout";
    }

    return "";
  }

  getStateNum(item: any): number {
    var checkout_time = this.checkoutTime;
    var checkin_time = this.checkinTime;
    var timeCheck = moment.duration(checkin_time);
    var timeOffset = moment.duration(checkout_time);
    var today = moment();

    if (isNullOrUndefined(item.checkinTime)) {
      var arrivalDate = moment(item.arrivalDate);
      var shouldCheckin = arrivalDate.clone().add(timeCheck);
      var lateCheckin = arrivalDate.clone().add(timeOffset);

      if (today.isAfter(lateCheckin)) {
        return 2;
      } else if (today.isAfter(shouldCheckin)) {
        return 1;
      } else {
        return 0;
      }
    } else {
      var departureDate = moment(item.departureDate);
      var shouldCheckout = departureDate.clone().add(timeCheck);
      var lateCheckout = departureDate.clone().add(timeOffset);

      if (today.isAfter(lateCheckout)) {
        return 5;
      } else if (today.isAfter(shouldCheckout)) {
        return 4;
      } else {
        return 3;
      }
    }
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
    let arrival = moment(first.ArrivalDate);
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
    let fulladdress = first.Address + ", " + first.City + ", " + first.Province + ", " + first.State;

    this.guest_model.idNumber = first.IdNumber;
    this.guest_model.fullname = first.Fullname;
    this.guest_model.email = first.Email;
    this.guest_model.phone1 = first.Phone1;
    this.guest_model.phone2 = first.Phone2;
    this.guest_model.address = fulladdress;
    this.guest_model.photoGuest = first.PhotoGuest;
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
