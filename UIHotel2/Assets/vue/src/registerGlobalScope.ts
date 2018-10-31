import { ICS } from "@/lib/Interface";
import $ from "jquery";
import Vue from "vue";

declare global {
  // tslint:disable-next-line:interface-name
  interface Window {
    CS: ICS;
    $: JQueryStatic;
    jQuery: JQueryStatic;
    bus: Vue;
    handleSlimScroll(): void;
  }
}

window.$ = $;
window.jQuery = $;
window.bus = new Vue();
window.handleSlimScroll = () => {
  //
};
