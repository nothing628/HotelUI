<template>
  <div class="row">
    <div class="panel panel-inverse">
      <div class="panel-heading">
        <h4 class="panel-title">List Booking</h4>
      </div>
      <div class="panel-body">
        <div class="dataTables_wrapper form-inline dt-bootstrap no-footer">
          <div class="dt-buttons btn-group">
            <a class="btn btn-success buttons-copy buttons-html5 btn-sm" @click="add">
              <i class="fa fa-book"></i>
              <span> New Booking</span>
            </a>
          </div>
          <div class="dataTables_filter">
            <label>Search:<input type="search" class="form-control input-sm" placeholder="" ></label>
          </div>
          <table class="table table-striped table-bordered dataTable no-footer dtr-inline" role="grid">
            <thead>
                <tr role="row">
                  <th rowspan="1" colspan="1">Booking ID</th>
                  <th>Booking Time</th>
                  <th>Checkin Time</th>
                  <th rowspan="1" colspan="1">Arrival</th>
                  <th rowspan="1" colspan="1">Departure</th>
                  <th rowspan="1" colspan="1">Status</th>
                  <th rowspan="1" colspan="1"></th>
                </tr>
            </thead>
            <tbody>
              <tr v-for="item in items" :key="item.Id" :class="getStateColor(item)">
                  <td>{{ item.Id }}</td>
                  <td>{{ item.BookingAt | date_time_filt }}</td>
                  <td>{{ item.CheckinAt | date_time_filt }}</td>
                  <td>{{ item.ArrivalDate | date_filt }}</td>
                  <td>{{ item.DepartureDate | date_filt }}</td>
                  <td>{{ getStateName(item) }}</td>
                  <td>
                    <div class="btn-group pull-right">
                      <button class="btn btn-sm btn-success" @click="deleteData(item)"><i class="fa fa-pencil"></i> Action</button>
                    </div>
                  </td>
              </tr>
            </tbody>
          </table>
          <counter :from.sync="from" :to.sync="to" :total.sync="max_item"></counter>
          <pagination :total-page.sync="totalPage" v-model="currentPage"></pagination>
        </div>

        <uiv-modal v-model="show_form" title="Action" size="lg">
          <step-selector>
            <step-item step-number="" step-title="Booking Info" step-target="booking" active></step-item>
            <step-item step-number="" step-title="Guest Info" step-target="guest"></step-item>
          </step-selector>
          <step-container name="booking" active>
            <div class="form-horizontal">
              <div class="form-group">
                <label class="col-md-3 control-label">Booking ID :</label>
                <div class="col-md-6">
                  <input class="form-control" readonly />
                </div>
              </div>
              <div class="form-group">
                <label class="col-md-3 control-label">Room Number :</label>
                <div class="col-md-6">
                  <a class="btn btn-link">201 Big</a>
                </div>
              </div>
              <div class="form-group">
                <label class="col-md-3 control-label">Status :</label>
                <div class="col-md-3">
                  <input class="form-control" readonly />
                </div>
              </div>
              <div class="form-group">
                <label class="col-md-3 control-label">Arrival Date :</label>
                <div class="col-md-3">
                  <input class="form-control" readonly />
                </div>
                <label class="col-md-3 control-label">Departure Date :</label>
                <div class="col-md-3">
                  <input class="form-control" readonly />
                </div>
              </div>
              <div class="form-group">
                <label class="col-md-3 control-label">Checkin Time :</label>
                <div class="col-md-3">
                  <input class="form-control" readonly />
                </div>
                <label class="col-md-3 control-label">Checkout Time :</label>
                <div class="col-md-3">
                  <input class="form-control" readonly />
                </div>
              </div>
              <div class="form-group">
                <label class="col-md-3 control-label">Count Adult :</label>
                <div class="col-md-2">
                  <input class="form-control" readonly />
                </div>
                <label class="col-md-2 control-label">Count Child :</label>
                <div class="col-md-2">
                  <input class="form-control" readonly />
                </div>
              </div>
              <div class="form-group">
                <label class="col-md-3 control-label">Note :</label>
                <div class="col-md-6">
                  <textarea class="form-control" readonly></textarea>
                </div>
              </div>
            </div>
          </step-container>
          <step-container name="guest">
            <div class="form-horizontal">
              <div class="form-group">
                <label class="control-label col-md-3">ID Number :</label>
                <div class="col-md-6">
                  <input class="form-control" readonly />
                </div>
                <div class="col-md-3">
                  <img class="img-responsive img-float" src=""/>
                </div>
              </div>
              <div class="form-group">
                <label class="control-label col-md-3">Fullname :</label>
                <div class="col-md-5">
                  <input class="form-control" readonly />
                </div>
              </div>
              <div class="form-group">
                <label class="control-label col-md-3">Email :</label>
                <div class="col-md-5">
                  <input class="form-control" readonly />
                </div>
              </div>
              <div class="form-group">
                <label class="control-label col-md-3">Address :</label>
                <div class="col-md-6">
                  <textarea class="form-control" readonly></textarea>
                </div>
              </div>
              <div class="form-group">
                <label class="control-label col-md-3">Phone Number :</label>
                <div class="col-md-3">
                  <input class="form-control" readonly />
                </div>
                <div class="col-md-3">
                  <input class="form-control" readonly />
                </div>
              </div>
              <div class="form-group">
                <div class="col-md-3 col-md-offset-3">
                  <button class="btn btn-link">Detail Guest</button>
                </div>
              </div>
            </div>
          </step-container>
          
          <div class="row">
            <div class="col-md-4">
              <button class="btn btn-block btn-info">Invoice</button>
            </div>
            <div class="col-md-4">
              <button class="btn btn-block btn-success">Checkin</button>
            </div>
            <div class="col-md-4">
              <button class="btn btn-block btn-danger">Checkout</button>
            </div>
          </div>
          <div slot="footer">
            <button class="btn btn-danger">Close</button>
          </div>
        </uiv-modal>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import { squel, ss, su, sd, si, execute, executeScalar } from "@/lib/Test";
import Pagination from "@/components/Table/Pagination.vue";
import Counter from "@/components/Table/Counter.vue";
import StepItem from "@/components/Steps/StepItem.vue";
import StepSelector from "@/components/Steps/StepSelector.vue";
import StepContainer from "@/components/Steps/StepContainer.vue";
import moment from "moment";
import { isNull, isNullOrUndefined } from 'util';

@Component({
  components: {
    Pagination,
    Counter,
    StepItem,
    StepSelector,
    StepContainer
  },
  filters: {
    date_time_filt(val: any): string {
      let momentf = moment(val);

      if (!momentf.isValid()) {
        return "-";
      }

      return momentf.format("DD/MM/YYYY HH:mm")
    },
    date_filt(val: any): string {
      let momentf = moment(val);

      if (!momentf.isValid()) {
        return "-";
      }

      return momentf.format("DD/MM/YYYY");
    }
  }
})
export default class DataBooking extends Vue {
  private show_form: boolean = true;
  private max_item: number = 0;
  private currentPage: number = 1;
  private limit: number = 10;
  private items: Array<any> = new Array();

  get from(): number {
    return this.offset + 1;
  }

  get to(): number {
    let rest = this.from + this.limit - 1;

    if (rest > this.max_item) {
      return this.max_item;
    }

    return rest;
  }

  get offset(): number {
    return (this.currentPage - 1) * this.limit;
  }

  get totalPage(): number {
    return Math.ceil(this.max_item / this.limit);
  }

  get checkoutTime(): string {
    return this.$store.state.Setting.Time_Checkout;
  }

  get checkinTime(): string {
    return this.$store.state.Setting.Time_Checkin;
  }

  getStateName(item: any): string {
    var state: number = this.getStateNum(item);

    switch(state) {
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

    if (isNullOrUndefined(item.CheckinAt)) {
      var arrivalDate = moment(item.ArrivalDate);
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
      var departureDate = moment(item.DepartureDate);
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

  getStateColor(item: any): Array<string> {
    var state = this.getStateNum(item);

    if (state == 1 || state == 4) {
      return ["bg-success"];
    } else if (state == 2 || state == 5) {
      return ["bg-danger"];
    }

    return [];
  }

  getMaxItem() {
    let qry = ss()
      .from("bookings")
      .field("COUNT(*) as cnt");
    let result = execute(qry);
    let first = result[0];

    this.max_item = Number(first.cnt);
  }

  getItems() {
    let qry = ss()
      .from("bookings", "a")
      .field("a.*")
      .limit(this.limit)
      .offset(this.offset);
    let result = execute(qry);

    this.items = [];
    result.forEach((item: any) => this.items.push(item));
  }

  add() {
    this.$router.push({ name: "data.booking.create" });
  }

  mounted() {
    this.$store.commit("changeTitle", "List Booking");
    this.$store.commit("changeSubtitle", "");
    this.getMaxItem();
    this.getItems();
  }
}
</script>
