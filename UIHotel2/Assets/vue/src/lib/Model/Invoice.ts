import { ss, si, sd, execute, executeScalar } from "@/lib/Test";

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
}