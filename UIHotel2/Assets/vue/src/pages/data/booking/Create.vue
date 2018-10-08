<template>
  <div class="row">
    <div class="panel panel-inverse">
      <div class="panel-heading">
        <h4 class="panel-title">Create Booking</h4>
      </div>
      <div class="panel-body">
        <step-selector>
          <step-item step-number="1" step-title="Booking Info" step-target="booking" active></step-item>
          <step-item step-number="2" step-title="Guest Info" step-target="guest"></step-item>
          <step-item step-number="3" step-title="Room Info" step-target="room"></step-item>
        </step-selector>
        <step-container name="booking" active>
          <create1 v-model="bookInfo"></create1>
        </step-container>
        <step-container name="guest">
          <create2 v-model="guestInfo"></create2>
        </step-container>
        <step-container name="room">
          <create3 v-model="roomInfo"></create3>
        </step-container>

        <div class="row">
          <div class="col-md-6 col-md-offset-3">
            <button class="btn btn-success m-r-5" @click="booking"><i></i> Booking</button>
            <button class="btn btn-success m-r-5" @click="bookingCheckin"><i></i> Booking & Checkin</button>
            <button class="btn btn-danger" @click="back"><i></i> Cancel</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop, Watch } from "vue-property-decorator";
import { ss, si, su, sd, execute, executeScalar } from "@/lib/Test";
import moment from "moment";
import StepItem from "@/components/Steps/StepItem.vue";
import StepSelector from "@/components/Steps/StepSelector.vue";
import StepContainer from "@/components/Steps/StepContainer.vue";
import Create1 from "@/components/Bookmark/Step1.vue";
import Create2 from "@/components/Bookmark/Step2.vue";
import Create3 from "@/components/Bookmark/Step3.vue";

@Component({
  components: {
    Create1,
    Create2,
    Create3,
    StepItem,
    StepSelector,
    StepContainer
  }
})
export default class CreateBooking extends Vue {
  private bookInfo: any = {};
  private guestInfo: any = {};
  private roomInfo: any = {};

  get isValid(): boolean {
    var book_val = Object.keys(this.bookInfo);
    var guest_val = Object.keys(this.guestInfo);
    var room_val = Object.keys(this.roomInfo);

    return !(
      book_val.length == 0 ||
      guest_val.length == 0 ||
      room_val.length == 0
    );
  }

  // Step 1 Create in booking table
  createBooking() {
    let today = moment();
    let qryBook = si()
      .into("bookings")
      .set("Id", this.bookInfo.BookNumber)
      .set("BookingAt", today.format("YYYY-MM-DD HH:mm:ss"))
      .set("CheckinAt", null)
      .set("CheckoutAt", null)
      .set("ArrivalDate", this.bookInfo.ArrivalDate)
      .set("DepartureDate", this.bookInfo.DepartureDate)
      .set("CountAdult", this.bookInfo.CountAdult)
      .set("CountChild", this.bookInfo.CountChild)
      .set("Note", this.roomInfo.Note)
      .set("GuestId", this.guestInfo.Id)
      .set("RoomId", this.roomInfo.Id)
      .set("TypeId", this.bookInfo.BookType);
    let qryRoom = su()
      .table("rooms")
      .set("RoomStateId", 2)
      .where("Id = ?", this.roomInfo.Id);
    
    executeScalar(qryBook);
    executeScalar(qryRoom);
  }

  // Step 2 Create in invoice table and detail invoice
  createInvoice()
  {
    let qry = si()
      .into("invoices")
      .set("Id", this.bookInfo.BookNumber)
      .set("BookingId", this.bookInfo.BookNumber)
    let result = executeScalar(qry);
    this.createInvoiceDetail();
  }

  createInvoiceDetail()
  {
    let today = moment();
    let qryType = ss()
      .from("bookingtypes")
      .where("Id = ?", this.bookInfo.BookType)
      .field("IsLocal");
    let resultType = execute(qryType);

    if (resultType.length > 0) {
      if (resultType[0].IsLocal) {
        //is local, add detail deposit on it
        let qryInv = si()
          .into("invoicedetails")
          .set("InvoiceId", this.bookInfo.BookNumber)
          .set("KindId", 97)
          .set("AmmountIn", this.bookInfo.Deposit)
          .set("AmmountOut", 0)
          .set("TransactionAt", today.format("YYYY-MM-DD HH:mm:ss"))
          .set("IsSystem", true)
          .set("Description", "Deposit");
        executeScalar(qryInv);
      } else {
        //is online, add deposit same as room price
        let qryInv = si()
          .into("invoicedetails")
          .set("InvoiceId", this.bookInfo.BookNumber)
          .set("KindId", 4)
          .set("AmmountIn", 0)
          .set("AmmountOut", this.bookInfo.Price)
          .set("TransactionAt", today.format("YYYY-MM-DD HH:mm:ss"))
          .set("IsSystem", true)
          .set("Description", "Online Price");
        executeScalar(qryInv);
      }
    }
  }

  // Step 3 Change state to checkin after process complete
  createCheckin()
  {
    let today = moment();
    let qryBook = su()
      .table("bookings")
      .set("CheckinAt", today.format("YYYY-MM-DD HH:mm:ss"))
      .where("Id = ?", this.bookInfo.BookNumber);
    let qryRoom = su()
      .table("rooms")
      .set("RoomStateId", 3)
      .where("Id = ?", this.roomInfo.Id);
    executeScalar(qryBook);
    executeScalar(qryRoom);
  }

  // Step 4 Finish step and get back to index
  redirectBack()
  {
    //
  }

  booking() {
    this.validate();
    this.$nextTick(() => {
      if (this.isValid) {
        // Create only booking
        this.createBooking();
        this.createInvoice();
        this.redirectBack();
      }
    });
  }

  bookingCheckin() {
    this.validate();
    this.$nextTick(() => {
      if (this.isValid) {
        // Create booking and checkin
        this.createBooking();
        this.createInvoice();
        this.createCheckin();
        this.redirectBack();
      }
    });
  }

  validate() {
    window.bus.$emit("book-validate");
  }

  back() {
    this.$router.push({ name: "data.booking" });
  }

  mounted() {
    this.$store.commit("changeTitle", "Create Booking");
    this.$store.commit("changeSubtitle", "");
  }
}
</script>
