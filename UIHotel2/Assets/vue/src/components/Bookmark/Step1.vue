<template>
  <div class="form-horizontal">
    <div class="form-group">
      <label class="col-md-3 control-label">Booking Type</label>
      <div class="col-md-3">
        <select class="form-control" v-model="bookingProv">
          <option value="1">Local</option>
          <option value="0">Online</option>
        </select>
      </div>
      <label class="col-md-2 control-label" v-if="bookingProv == 1">Type</label>
      <label class="col-md-2 control-label" v-if="bookingProv == 0">Online Provider</label>
      <div class="col-md-3">
        <select class="form-control" v-model="modalData.BookType">
          <option v-for="item in listBookingType" :key="item.Id" :value="item.Id">
            {{ item.TypeName }}
          </option>
        </select>
      </div>
    </div>

    <div class="form-group">
      <label class="col-md-3 control-label">Booking Number</label>
      <div class="col-md-4">
        <input
        class="form-control"
        :readonly="isReadonly"
        name="book_number"
        v-model="modalData.BookNumber"
        v-validate="'required'"/>
        <span class="text-danger">{{ errors.first('book_number') }}</span>
      </div>
    </div>

    <div class="form-group">
      <label class="col-md-3 control-label">Arrival Date</label>
      <div class="col-md-3">
        <input
        type="date"
        class="form-control"
        name="arrival_date"
        v-validate="'required'"
        v-model="modalData.ArrivalDate"/>
        <span class="text-danger">{{ errors.first('arrival_date') }}</span>
      </div>
      <label class="col-md-2 control-label">Departure Date</label>
      <div class="col-md-3">
        <input
        type="date"
        class="form-control"
        name="departure_date"
        v-validate="'required'"
        v-model="modalData.DepartureDate"/>
        <span class="text-danger">{{ errors.first('departure_date') }}</span>
      </div>
    </div>

    <div class="form-group">
      <label class="col-md-3 control-label">Adult Count</label>
      <div class="col-md-3">
        <input
        class="form-control"
        type="number"
        name="count_adult"
        v-model="modalData.CountAdult"
        v-validate="'required|min_value:1|max_value:20'"/>
        <span class="text-danger">{{ errors.first('count_adult') }}</span>
      </div>
      <label class="col-md-2 control-label">Child Count</label>
      <div class="col-md-3">
        <input
        class="form-control"
        type="number"
        name="count_child"
        v-model="modalData.CountChild"
        v-validate="'required|min_value:0|max_value:20'"/>
        <span class="text-danger">{{ errors.first('count_child') }}</span>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { ss, execute } from "@/lib/Test";
import { Component, Vue, Prop, Watch } from "vue-property-decorator";

interface IModalData {
  BookNumber: string;
  BookType: number;
  ArrivalDate: string;
  DepartureDate: string;
  CountAdult: number;
  CountChild: number;
}

@Component
export default class CreateStep1 extends Vue {
  private bookingTypes: Array<any> = new Array<any>();
  private bookingProv: number = 0;
  private modalData: IModalData = {
    BookType: 0,
    BookNumber: "",
    ArrivalDate: "",
    DepartureDate: "",
    CountAdult: 1,
    CountChild: 0
  };

  get isReadonly() {
    return this.bookingProv == 1;
  }

  get listBookingType() {
    if (this.bookingProv == 0) {
      return this.bookingTypes.filter((item: any) => !item.IsLocal);
    } else {
      return this.bookingTypes.filter((item: any) => item.IsLocal);
    }
  }

  getBookingType() {
    let qry = ss().from("bookingtypes");
    let result = execute(qry);

    this.bookingTypes = [];
    result.forEach((item: any) => {
      this.bookingTypes.push(item);
    });
  }

  validate() {
    this.$validator.validateAll().then((is_valid: any) => {
      console.log(is_valid);
    });
  }

  @Watch("bookingProv")
  bookProvChange() {
    if (this.isReadonly) {
      this.modalData.BookNumber = window.CS.App.GetNewBookingNumber();
    } else {
      this.modalData.BookNumber = "";
    }

    this.modalData.BookType = this.listBookingType[0].Id;
  }

  mounted() {
    this.getBookingType();
    this.bookingProv = 1;
    window.bus.$on("book-validate", this.validate);
  }

  beforeDestroy() {
    window.bus.$off("book-validate", this.validate);
  }
}
</script>
