<template>
  <div class="row">
    <div class="invoice">
      <div class="invoice-company">
        <span class="pull-right hidden-print">
          <button class="btn btn-sm btn-success m-b-10 m-r-5" @click="ProcessPayment"><i class="fa fa-pencil"></i> Pay Invoice</button>
          <button class="btn btn-sm btn-success m-b-10 m-r-5" @click="ShowNewDetail"><i class="fa fa-plus"></i> Add Item</button>
          <!-- <button class="btn btn-sm btn-success m-b-10 m-r-5"><i class="fa fa-download m-r-5"></i> Export as PDF</button> -->
          <button class="btn btn-sm btn-danger m-b-10 m-r-5" @click="BackToList"><i class="fa fa-reply m-r-5"></i> Back</button>
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
            Phone: {{Guest_Phone1}} / {{Guest_Phone2}}<br>
            Email: {{Guest_Email}}
          </address>
        </div>
        <div class="invoice-date">
          <small>Invoice Period</small>
          <div class="date m-t-5">{{ InvoicePeriod }}</div>
          <div class="invoice-detail">{{invId}}</div>
        </div>
      </div>
      <div class="invoice-content">
        <div class="table-responsive">
          <table class="table table-invoice">
            <thead>
              <tr>
                <th style="width: 1px"></th>
                <th>DATE</th>
                <th>DESCRIPTION</th>
                <th>IN</th>
                <th>OUT</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="item in CleanList" :key="item.Id">
                <td style="width: 1px">
                  <button v-if="!item.IsSystem" @click="RemoveDetail(item.Id)" class="btn btn-danger btn-xs"><i class="fa fa-times"></i></button>
                </td>
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
        <div class="invoice-price" v-if="!is_pay">
          <div class="invoice-price-left">
            <div class="invoice-price-row">
              <div class="sub-price">
                <small>TYPE PAY</small>
                {{ PaymentType }}
              </div>
              <div class="sub-price">
                <small>REF NO</small>
                {{ PaymentRefno }}
              </div>
            </div>
          </div>
          <div class="invoice-price-right white">
            <small>PAY</small> {{ PaymentModel.Ammount | strcurrency }}
          </div>
        </div>
        <div class="invoice-price" v-if="is_pay">
          <div class="invoice-price-left">
            <div class="invoice-price-row">
              <div class="sub-price">
                <small>TYPE PAY</small>
                <div class="radio radio-css">
                  <input type="radio" v-model="TypeModel" id="option1" value="CASH">
                  <label for="option1">CASH</label>
                </div>
                <div class="radio radio-css">
                  <input type="radio" v-model="TypeModel" id="option2" value="DEBIT">
                  <label for="option2">DEBIT</label>
                </div>
              </div>
              <div class="sub-price" v-if="TypeModel == 'DEBIT'">
                <small>REF NO</small>
                <input type="text" v-model="TypeRefno"/>
              </div>
            </div>
          </div>
          <div class="invoice-price-right white input">
            <div class="input-group">
              <span class="input-group-addon">Rp</span>
              <input class="form-control" type="number" v-model="PaymentModel.Ammount">
            </div>
          </div>
        </div>
        <div class="invoice-price">
          <div class="invoice-price-left"></div>
          <div class="invoice-price-right white">
            <small>CASHBACK</small> {{ Cashback | strcurrency }}
          </div>
        </div>
        <div class="invoice-price" v-if="is_pay">
          <div class="invoice-price-left"></div>
          <div class="invoice-price-right">
            <button class="btn btn-info btn-block" @click="PaymentSubmit"><i class="fa fa-check"></i> Finish</button>
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

    <uiv-modal v-model="is_add" title="Add Transaction" @hide="CloseAll">
      <div class="form-horizontal">
        <div class="form-group">
          <label class="col-md-3 control-label">Description</label>
          <div class="col-md-6">
            <textarea class="form-control" v-model="DetailModel.Description"></textarea>
          </div>
        </div>
        <div class="form-group">
          <label class="col-md-3 control-label">Type</label>
          <div class="col-md-3">
            <div class="radio radio-css">
              <input type="radio" v-model="DetailType" id="option3" value="200">
              <label for="option3">IN</label>
            </div>
          </div>
          <div class="col-md-3">
            <div class="radio radio-css">
              <input type="radio" v-model="DetailType" id="option4" value="201">
              <label for="option4">OUT</label>
            </div>
          </div>
        </div>
        <div class="form-group" v-if="DetailType == '200'">
          <label class="col-md-3 control-label">Ammount In</label>
          <div class="col-md-5">
            <div class="input-group">
              <span class="input-group-addon">Rp</span>
              <input class="form-control" type="number" v-model="DetailModel.AmmountIn">
            </div>
          </div>
        </div>
        <div class="form-group" v-if="DetailType == '201'">
          <label class="col-md-3 control-label">Ammount Out</label>
          <div class="col-md-5">
            <div class="input-group">
              <span class="input-group-addon">Rp</span>
              <input class="form-control" type="number" v-model="DetailModel.AmmountOut">
            </div>
          </div>
        </div>
      </div>

      <div slot="footer">
        <button class="btn btn-success" @click="StoreDetail">Save</button>
        <button class="btn btn-danger" @click="CloseAll">Cancel</button>
      </div>
    </uiv-modal>
  </div>
</template>
<script lang="ts">
import moment from "moment";
import { Component, Vue, Prop } from "vue-property-decorator";
import { ss, execute, executeFirst } from "@/lib/Test";
import { isUndefined, isNull } from "util";
import { PaymentType, IPaymentModel, UncategorizedDetailType, Invoice, IInvoiceDetailModel } from "@/lib/Model/Invoice";

interface IDetailModel {
  AmmountIn: number;
  AmmountOut: number;
  Type: UncategorizedDetailType;
  Description: string;
}

@Component
export default class DataInvoice extends Vue {
  private is_pay: boolean = false;
  private is_add: boolean = false;
  private invId: string = "";
  private DetailType: string = "200";
  private DetailModel: IDetailModel = {
    AmmountIn: 0,
    AmmountOut: 0,
    Type: UncategorizedDetailType.IN,
    Description: ""
  };
  private PaymentModel: IPaymentModel = {
    Ammount: 0,
    TRefNo: "",
    Type: undefined
  };
  private TypeModel: string = "CASH";
  private TypeRefno: string = "";
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

  get PaymentType(): string {
    if (isUndefined(this.PaymentModel.Type)) {
      return "-";
    } else {
      if (this.PaymentModel.Type == PaymentType.CASH) {
        return "CASH";
      } else {
        return "DEBIT";
      }
    }
  }

  get PaymentRefno(): string {
    if (this.PaymentModel.TRefNo == "" || isNull(this.PaymentModel.TRefNo)) {
      return "-";
    }

    return this.PaymentModel.TRefNo;
  }

  get Cashback(): number {
    return this.PaymentModel.Ammount - this.TotalBalance;
  }

  get TotalBalance(): number {
    let total: number = 0;

    this.CleanList.forEach((item: any) => {
      total += item.AmmountOut - item.AmmountIn;
    });

    return total;
  }

  get CleanList(): Array<any> {
    let filtered: Array<any> = this.List_Detail.filter((item) => {
      return item.KindId != 100 && item.KindId != 101;
    });

    return filtered;
  }

  get InvoicePeriod(): string {
    return moment().format("MMMM DD, YYYY");
  }

  BackToList() {
    this.$router.push({ name: "data.booking" });
  }

  StoreDetail() {
    this.DetailModel.Type = this.DetailType == "200" ? UncategorizedDetailType.IN : UncategorizedDetailType.OUT;

    let newDetail: IInvoiceDetailModel = {
      AmmountIn: this.DetailModel.AmmountIn,
      AmmountOut: this.DetailModel.AmmountOut,
      Description: this.DetailModel.Description,
      Type: this.DetailModel.Type
    };

    Invoice.NewDetail(this.invId, newDetail);
    this.getInvoice();
    this.is_add = false;
  }

  ShowNewDetail() {
    this.DetailModel.AmmountIn = 0;
    this.DetailModel.AmmountOut = 0;
    this.DetailModel.Description = "";
    this.DetailType = "201";
    this.is_add = true;
  }

  RemoveDetail(idnum: number) {
    Invoice.DeleteDetail(this.invId, idnum);
    this.getInvoice();
  }

  CloseAll() {
    this.is_add = false;
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

    this.List_Detail = [];
    this.is_pay = false;

    if (result.length > 0) {
      result.forEach((item: any) => this.List_Detail.push(item));
      this.getPayment();
    }
  }

  getPayment() {
    let dataPayment = this.List_Detail.filter((item: any) => {
      return item.KindId == 100 || item.KindId == 101;
    });

    if (dataPayment.length == 0) {
      this.PaymentModel.Type = undefined;
      this.PaymentModel.Ammount = 0;
      this.PaymentModel.TRefNo = "";
    } else {
      let first = dataPayment[0];
      this.PaymentModel.Type = first.KindId == 100 ? PaymentType.CASH : PaymentType.DEBIT;
      this.PaymentModel.Ammount = first.AmmountIn;
      this.PaymentModel.TRefNo = first.Description;
    }
  }

  ProcessPayment() {
    this.TypeRefno = this.PaymentModel.TRefNo;
    
    if (isUndefined(this.PaymentModel.Type) || this.PaymentModel.Type === PaymentType.CASH) {
      this.TypeModel = "CASH";
    } else {
      this.TypeModel = "DEBIT";
    }

    this.is_pay = true;
    window.scrollTo(0, document.body.scrollHeight);
  }

  PaymentSubmit() {
    let paymentModel: IPaymentModel = {
      Ammount: this.PaymentModel.Ammount,
      TRefNo: this.TypeRefno,
      Type: this.TypeModel == "CASH" ? PaymentType.CASH : PaymentType.DEBIT
    };

    Invoice.PaymentProcess(this.invId, paymentModel);
    this.getInvoice();
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
