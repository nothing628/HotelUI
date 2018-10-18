import currency from "currency.js";
import moment, { Moment } from "moment";
import Vue from "vue";

function StrDate(item: any, format: string): string {
  var date: Moment = moment(item);

  if (date.isValid()) {
    return date.format(format);
  }

  return "";
}

function StrShortdate(item: any): string {
  return StrDate(item, "MM/DD");
}

function StrLongdate(item: any): string {
  return StrDate(item, "DD/MM/YYYY");
}

function StrCurrency(item: string): string {
  const IDR: any = (value: any) => {
    var options: currency.Options = {
      symbol: "Rp",
      decimal: ",",
      separator: ".",
      precision: 0
    };
    return currency(value, options);
  };

  return IDR(item).format(true);
}

export function Register(): void {
  Vue.filter("strcurrency", StrCurrency);
  Vue.filter("strshortdate", StrShortdate);
  Vue.filter("strlongdate", StrLongdate);
  Vue.filter("strdate", StrDate);
}
