import { ss, si, sd, execute, executeScalar } from "@/lib/Test";
import { Invoice } from "@/lib/Model/Invoice";
import moment, { Moment } from "moment";

export class Booking {
  public static CancelBooking(bookId: string): any {
    Invoice.Delete(bookId);
    let qry: any = sd()
      .from("bookings")
      .where("Id = ?", bookId);
    let result: any = executeScalar(qry);
    return result;
  }
}
