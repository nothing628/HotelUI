import { ICS } from "@/lib/Interface";
import $ from "jquery";

declare global {
  interface Window {
    CS: ICS;
    $: JQueryStatic;
    jQuery: JQueryStatic;
  }
}

window["$"] = $;
window["jQuery"] = $;
