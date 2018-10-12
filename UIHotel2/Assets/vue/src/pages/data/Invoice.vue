<template>
  <div class="row">
    <div class="invoice">
      <div class="invoice-company">
        <span class="pull-right hidden-print">
          <a href="javascript:;" class="btn btn-sm btn-success m-b-10"><i class="fa fa-download m-r-5"></i> Export as PDF</a>
          <a href="javascript:;" onclick="window.print()" class="btn btn-sm btn-success m-b-10"><i class="fa fa-print m-r-5"></i> Print</a>
        </span>
        Company Name, Inc
      </div>
      <div class="invoice-header">
        <div class="invoice-from">
          <small>from</small>
          <address class="m-t-5 m-b-5">
            <strong>Hotel</strong><br>
            Jl. Kurang Mampu<br>
            Tangerang, 14000<br>
            Phone: (123) 456-7890<br>
            Fax: (123) 456-7890
          </address>
        </div>
        <div class="invoice-to">
          <small>to</small>
          <address class="m-t-5 m-b-5">
            <strong>Company Name</strong><br>
            Street Address<br>
            City, Zip Code<br>
            Phone: (123) 456-7890<br>
            Fax: (123) 456-7890
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
              <tr>
                <td>09/01</td>
                <td>
                  Redesign Service<br>
                  <small>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed id sagittis arcu.</small>
                </td>
                <td>$50.00</td>
                <td>$2,500.00</td>
              </tr>
            </tbody>
          </table>
        </div>
        <div class="invoice-price">
          <div class="invoice-price-left">
            <div class="invoice-price-row" v-show="false">
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
            <small>TOTAL</small> Rp4,508,000
          </div>
        </div>
      </div>
      <div class="invoice-note"></div>
      <div class="invoice-footer text-muted">
        <p class="text-center m-b-5">THANK YOU FOR YOUR BUSINESS</p>
        <p class="text-center">
          <span class="m-r-10"><i class="fa fa-globe"></i> matiasgallipoli.com</span>
          <span class="m-r-10"><i class="fa fa-phone"></i> T:016-18192302</span>
          <span class="m-r-10"><i class="fa fa-envelope"></i> rtiemps@gmail.com</span>
        </p>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";

@Component
export default class DataInvoice extends Vue {
  private invId: string = "";

  mounted() {
    this.invId = this.$route.params.id;
    this.$store.commit("changeTitle", "Invoice #" + this.invId);
    this.$store.commit("changeSubtitle", "");
  }
}
</script>
