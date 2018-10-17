import { ss, su, si, sd, execute, executeFirst, executeScalar } from "@/lib/Test";
import { Invoice } from "@/lib/Model/Invoice";
import { Room, RoomStateType } from "@/lib/Model/Room";
import moment, { Moment } from "moment";
import { isUndefined } from "util";

export class Booking {
  public static Cancel(bookId: string): any {
    Invoice.Delete(bookId);
    let qry: any = ss()
      .from("bookings")
      .field("RoomId")
      .where("Id = ?", bookId);
    let first: any = executeFirst(qry);

    if (!isUndefined(first)) {
      Room.ChangeState(first.RoomId, RoomStateType.Vacant);
    }

    let qry2: any = sd()
      .from("bookings")
      .where("Id = ?", bookId);
    let result2: any = executeScalar(qry2);
    return result2;
  }

  public static Checkin(bookId: string): any {
    let today: Moment = moment();
    let qry: any = ss()
      .from("bookings")
      .field("RoomId")
      .where("Id = ?", bookId);
    let first: any = executeFirst(qry);

    if (!isUndefined(first)) {
      Room.ChangeState(first.RoomId, RoomStateType.Occupied);
    }

    let qry2: any = su()
      .table("bookings")
      .set("CheckinAt", today.format("YYYY-MM-DD HH:mm:ss"))
      .where("Id = ?", bookId);
    let result2: any = executeScalar(qry2);
    return result2;
  }

  public static Checkout(bookId: string): boolean {
    try {
      let today: string = moment().format("YYYY-MM-DD HH:mm:ss");
      let bookQry: any = ss()
        .from("bookings")
        .where("Id = ?", bookId)
        .field("RoomId");
      let bookFirst: any = executeFirst(bookQry);
      let qry: any = su()
        .table("bookings")
        .set("CheckoutAt", today)
        .where("Id = ?", bookId);

      if (!isUndefined(bookFirst)) {
        Invoice.CloseInvoice(bookId);
        Room.ChangeState(bookFirst.RoomId, RoomStateType.Cleaning);
        executeScalar(qry);

        return true;
      } else {
        return false;
      }
    } catch (e) {
      // need to pay invoice first
      return false;
    }
  }
}
