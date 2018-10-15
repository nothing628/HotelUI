import { ss, si, sd, se, execute, executeScalar } from "@/lib/Test";
import moment from "moment";

export enum PaymentType {
  CASH,
  ONLINE,
}

export interface IPaymentModel {
  Ammount: number;
  TRefNo: string;
  Type?: PaymentType;
}

export class Invoice {
  public static Delete(invoiceId: string): void {
    this.DeleteDetail(invoiceId);
    this.DeleteInvoice(invoiceId);
  }

  public static DeleteInvoice(invoiceId: string): void {
    let qry: any = sd()
      .from("invoices")
      .where("Id = ?", invoiceId);
    executeScalar(qry);
  }

  public static DeleteDetail(invoiceId: string): void {
    let qry: any = sd()
      .from("invoicedetails")
      .where("InvoiceId = ?", invoiceId);
    executeScalar(qry);
  }

  private static DropCurrentPayment(invoiceId: string): void {
    let qry: any = sd()
      .from("invoicedetails")
      .where("InvoiceId = ?", invoiceId)
      .where(se()
        .or("KindId = ?", 100)
        .or("KindId = ?", 101)
      );
    executeScalar(qry);
  }

  public static PaymentProcess(invoiceId: string, paymentDetail: IPaymentModel): void {
    this.DropCurrentPayment(invoiceId);
    let is_cash: boolean = paymentDetail.Type === PaymentType.CASH;
    let today: string = moment().format("YYYY-MM-DD HH:mm:ss");
    let qry: any = si()
      .into("invoicedetails")
      .set("InvoiceId", invoiceId)
      .set("AmmountIn", paymentDetail.Ammount)
      .set("AmmountOut", 0)
      .set("TransactionAt", today)
      .set("IsSystem", false)
      .set("KindId", is_cash ? 100 : 101);

    if (!is_cash) {
      qry.set("Description", paymentDetail.TRefNo);
    }

    executeScalar(qry);
  }
}