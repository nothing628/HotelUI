import { ss, si, su, sd, se, execute, executeScalar, executeFirst } from "@/lib/Test";
import { isUndefined } from "util";
import moment, { Moment } from "moment";

export class Transaction {
  public static CheckoutTransaction(invoiceId: string): boolean {
    let qry: any = ss()
      .from("invoicedetails")
      .where("InvoiceId = ?", invoiceId)
      .where(
        se()
          .or("KindId = ?", 100)
          .or("KindId = ?", 101)
      )
      .field("AmmountIn")
      .field("KindId")
      .field("Description")
      .field("InvoiceId");
    let result: any = executeFirst(qry);

    if (!isUndefined(result)) {
      let description: string = "Booking Payment #" + invoiceId;

      if (result.KindId === 101) {
        description += " With Refno #" + result.Description;
      }

      let today: Moment = moment();
      let today_format: string = today.format("YYYY-MM-DD HH:mm:ss");
      let qryInsert: any = si()
        .into("transactions")
        .set("AmmountIn", result.AmmountIn)
        .set("AmmountOut", 0)
        .set("Subtotal", 0)
        .set("IsClosed", false)
        .set("UserId", 1)
        .set("CategoryId", 2)
        .set("TransactionAt", today_format)
        .set("Description", description);

      executeScalar(qryInsert);
      window.CS.Hotel.CalcTransaction(false, () => {
        //
      });

      return true;
    }

    return false;
  }
}