import { ss, si, su, sd, se, execute, executeScalar } from "@/lib/Test";
import moment from "moment";
import { isUndefined } from "util";

export enum PaymentType {
  CASH,
  ONLINE,
}

export enum UncategorizedDetailType {
  IN = 200,
  OUT = 201,
}

export interface IInvoiceDetailModel {
  AmmountIn: number;
  AmmountOut: number;
  Type: UncategorizedDetailType;
  Description: string;
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

  public static DeleteDetail(invoiceId: string, detailId?: number): void {
    let qry: any = sd()
      .from("invoicedetails")
      .where("InvoiceId = ?", invoiceId);

    if (!isUndefined(detailId)) {
      qry.where("Id = ?", detailId);
    }

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

  public static NewDetail(invoiceId: string, invoiceDetail: IInvoiceDetailModel): void {
    let AmmountIn: number = 0;
    let AmmountOut: number = 0;
    let today: string = moment().format("YYYY-MM-DD HH:mm:ss");

    if (invoiceDetail.Type === UncategorizedDetailType.IN) {
      AmmountIn = invoiceDetail.AmmountIn;
    } else {
      AmmountOut = invoiceDetail.AmmountOut;
    }

    let qry: any = si()
      .into("invoicedetails")
      .set("InvoiceId", invoiceId)
      .set("AmmountIn", AmmountIn)
      .set("AmmountOut", AmmountOut)
      .set("TransactionAt", today)
      .set("Description", invoiceDetail.Description)
      .set("IsSystem", false)
      .set("KindId", invoiceDetail.Type);
    executeScalar(qry);
  }

  public static IsBalance(invoiceId: string): boolean {
    let qry: any = ss()
      .from("invoicedetails")
      .where("InvoiceId = ?", invoiceId)
      .field("AmmountIn")
      .field("AmmountOut");
    let details: any = execute(qry);
    let total: number = 0;

    if (details.length > 0) {
      details.forEach((item: any) => {
        total += item.AmmountIn - item.AmmountOut;
      });

      return total === 0;
    }

    return false;
  }

  public static CloseInvoice(invoiceId: string): void {
    let is_balance: boolean = this.IsBalance(invoiceId);
    let today: string = moment().format("YYYY-MM-DD HH:mm:ss");
    let qry: any = su()
      .table("invoices")
      .set("CloseAt", today)
      .where("Id = ?", invoiceId);

    if (!is_balance) {
      throw new Error("Need to pay invoice before closing the invoice!");
    }

    executeScalar(qry);
  }
}