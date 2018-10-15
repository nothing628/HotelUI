<template>
  <div class="row">
    <div class="invoice">
      <div class="invoice-company">
        <span class="pull-right hidden-print">
          <button class="btn btn-sm btn-success m-b-10 m-r-5"><i class="fa fa-pencil"></i> Pay Invoice</button>
          <button class="btn btn-sm btn-success m-b-10 m-r-5"><i class="fa fa-download m-r-5"></i> Export as PDF</button>
          <button class="btn btn-sm btn-danger m-b-10 m-r-5"><i class="fa fa-times m-r-5"></i> Cancel</button>
        </span>
        {{ Hotel_Name }}
      </div>
      <div class="invoice-header">
        <div class="invoice-from">
          <small>from</small>
          <address class="m-t-5 m-b-5">
            <strong>{{ Hotel_Name }}</strong><br>
            <span v-html="CleanAddress(Hotel_Address)"></span>
            Phone: {{ Hotel_Phone }}<br>
            Email: {{ Hotel_Email }}
          </address>
        </div>
        <div class="invoice-to">
          <small>to</small>
          <address class="m-t-5 m-b-5">
            <strong>{{ Guest_Name }}</strong><br>
            <span v-html="CleanAddress(Guest_Address)"></span>
            Phone: {{Guest_Phone1}}/{{Guest_Phone2}}<br>
            Email: {{Guest_Email}}
          </address>
        </div>
        <div class="invoice-date">
          <small>Invoice Period</small>
          <div class="date m-t-5">August 3,2012</div>
          <div class="invoice-detail">{{invId}}</div>
        </div>
      </div>
      <div class="invoice-content">
        <div class="table-responsive">
          <table class="table table-invoice">
            <thead>
              <tr>
                <th>DATE</th>
                <th>DESCRIPTION</th>
                <th>IN</th>
                <th>OUT</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="item in List_Detail" :key="item.Id">
                <td>{{ item.TransactionAt | strshortdate }}</td>
                <td>
                  {{ item.KindName }}<br>
                  <small>{{ item.Description }}</small>
                </td>
                <td>{{ item.AmmountIn | strcurrency }}</td>
                <td>{{ item.AmmountOut | strcurrency }}</td>
              </tr>
            </tbody>
          </table>
        </div>
        <div class="invoice-price">
          <div class="invoice-price-left">
            <div class="invoice-price-row" v-if="false">
              <div class="sub-price">
                <small>SUBTOTAL</small>
                $4,500.00
              </div>
              <div class="sub-price">
                <i class="fa fa-plus"></i>
              </div>
              <div class="sub-price">
                <small>PAYPAL FEE (5.4%)</small>
                $108.00
              </div>
            </div>
          </div>
          <div class="invoice-price-right">
            <small>TOTAL</small> {{ TotalBalance | strcurrency }}
          </div>
        </div>
        <div class="invoice-price">
          <div class="invoice-price-left">
            <div class="invoice-price-row">
              <div class="sub-price">
                <small>TYPE PAY</small>
                CASH
              </div>
              <div class="sub-price">
                <small>REF NO</small>
                0293851284233
              </div>
            </div>
          </div>
          <div class="invoice-price-right white">
            <small>PAY</small> {{ TotalBalance | strcurrency }}
          </div>
        </div>
        <div class="invoice-price">
          <div class="invoice-price-left">
            <div class="invoice-price-row">
              <div class="sub-price">
                <small>TYPE PAY</small>
                <div class="radio radio-css">
                  <input type="radio" name="cssRadio" value="option1" checked="">
                  <label>CASH</label>
                </div>
                <div class="radio radio-css">
                  <input type="radio" name="cssRadio" value="option1" checked="">
                  <label>ONLINE</label>
                </div>
              </div>
              <div class="sub-price">
                <small>REF NO</small>
                <input/>
              </div>
            </div>
          </div>
          <div class="invoice-price-right white input">
            <div class="input-group">
              <span class="input-group-addon">Rp</span>
              <input class="form-control">
            </div>
          </div>
        </div>
        <div class="invoice-price">
          <div class="invoice-price-left"></div>
          <div class="invoice-price-right white">
            <small>CASHBACK</small> {{ TotalBalance | strcurrency }}
          </div>
        </div>
        <div class="invoice-price">
          <div class="invoice-price-left"></div>
          <div class="invoice-price-right">
            <button class="btn btn-info"><i class="fa fa-check"></i> Finish</button>
          </div>
        </div>
      </div>
      <div class="invoice-note"></div>
      <div class="invoice-footer text-muted">
        <p class="text-center m-b-5">THANK YOU FOR YOUR BUSINESS</p>
        <p class="text-center">
          <span class="m-r-10" v-if="false"><i class="fa fa-globe"></i></span>
          <span class="m-r-10"><i class="fa fa-phone"></i> {{ Hotel_Phone }}</span>
          <span class="m-r-10"><i class="fa fa-envelope"></i> {{ Hotel_Email }}</span>
        </p>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import { ss, execute, executeFirst } from "@/lib/Test";
import { isUndefined, isNull } from "util";

@Component
export default class DataInvoice extends Vue {
  private invId: string = "";
  Hotel_Name: string = "";
  Hotel_Address: string = "";
  Hotel_Phone: string = "";
  Hotel_Email: string = "";
  Guest_Name: string = "";
  Guest_Address: string = "";
  Guest_Phone1: string = "";
  Guest_Phone2: string = "";
  Guest_Email: string = "";
  List_Detail: Array<any> = new Array<any>();

  get TotalBalance(): number {
    let total: number = 0;

    this.List_Detail.forEach((item: any) => {
      console.log(item);
    });

    return total;
  }

  CleanAddress(value: string): string {
    return value.replace("\n", "<br/>") + "<br/>";
  }

  copySetting() {
    this.Hotel_Name = this.$store.state.Setting.Hotel_Name;
    this.Hotel_Address = this.$store.state.Setting.Hotel_Address;
    this.Hotel_Phone = this.$store.state.Setting.Hotel_Phone;
    this.Hotel_Email = this.$store.state.Setting.Hotel_Email;
  }

  getInvoice() {
    let qry = ss()
      .from("invoicedetails", "a")
      .join("invoicedetailkinds", "b", "a.KindId = b.Id")
      .field("a.*")
      .field("b.KindName")
      .where("a.InvoiceId = ?", this.invId);
    let result = execute(qry);

    if (result.length > 0) {
      result.forEach((item: any) => this.List_Detail.push(item));
    }
  }

  getGuest() {
    let qry = ss()
      .from("bookings", "a")
      .join("guests", "b", "a.GuestId = b.Id")
      .field("b.*")
      .where("a.Id = ?", this.invId);
    let result = executeFirst(qry);

    if (!isUndefined(result)) {
      this.Guest_Name = result.Fullname;
      this.Guest_Address = result.Address + "\n";
      this.Guest_Address += result.City + ", ";
      this.Guest_Address += result.Province + ", ";
      this.Guest_Address += isNull(result.PostCode) ? "" : result.PostCode;
      this.Guest_Phone1 = result.Phone1;
      this.Guest_Phone2 = result.Phone2;
      this.Guest_Email = result.Email;
    }
  }

  mounted() {
    this.invId = this.$route.params.id;
    this.$store.commit("changeTitle", "Invoice #" + this.invId);
    this.$store.commit("changeSubtitle", "");
    this.copySetting();
    this.getGuest();
    this.getInvoice();
  }
}
</script>
