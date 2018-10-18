import { ss, su, si, sd, execute, executeFirst, executeScalar } from "@/lib/Test";
import { Invoice } from "@/lib/Model/Invoice";
import { Room, RoomStateType } from "@/lib/Model/Room";
import moment, { Moment, Duration } from "moment";
import { isUndefined, isNullOrUndefined } from "util";


export enum BookStatusType {
  Booking,
  ShouldCheckin,
  LateCheckin,
  Checkin,
  ShouldCheckout,
  LateCheckout,
  Checkout
}

export class Booking {
  private static CheckinTime(): moment.Duration {
    let time: string = window.CS.Setting.Time_Checkin;
    return moment.duration(time);
  }

  private static CheckoutTime(): moment.Duration {
    let time: string = window.CS.Setting.Time_Checkout;
    return moment.duration(time);
  }

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

  public static GetStatus(
    arrivalAt: string,
    departureAt: string,
    checkinTime?: string,
    checkoutTime?: string
  ): BookStatusType {
    var timeCheck: Duration = this.CheckinTime();
    var timeOffset: Duration = this.CheckoutTime();
    var today: Moment = moment();

    if (!isNullOrUndefined(checkoutTime)) {
      return BookStatusType.Checkout;
    }

    if (isNullOrUndefined(checkinTime)) {
      var arrivalDate: Moment = moment(arrivalAt, "DD/MM/YYYY");
      var shouldCheckin: Moment = arrivalDate.clone().add(timeCheck);
      var lateCheckin: Moment = arrivalDate.clone().add(timeOffset);

      if (today.isAfter(lateCheckin)) {
        return BookStatusType.LateCheckin;
      } else if (today.isAfter(shouldCheckin)) {
        return BookStatusType.ShouldCheckin;
      } else {
        return BookStatusType.Booking;
      }
    } else {
      var departureDate: Moment = moment(departureAt, "DD/MM/YYYY");
      var shouldCheckout: Moment = departureDate.clone().add(timeCheck);
      var lateCheckout: Moment = departureDate.clone().add(timeOffset);

      if (today.isAfter(lateCheckout)) {
        return BookStatusType.LateCheckout;
      } else if (today.isAfter(shouldCheckout)) {
        return BookStatusType.ShouldCheckout;
      } else {
        return BookStatusType.Checkin;
      }
    }
  }

  public static GetStatusName(status: BookStatusType): string {
    switch (status) {
      case BookStatusType.Booking:
        return "Booking";
      case BookStatusType.ShouldCheckin:
        return "Should Checkin";
      case BookStatusType.LateCheckin:
        return "Late Checkin";
      case BookStatusType.Checkin:
        return "Checkin";
      case BookStatusType.ShouldCheckout:
        return "Should Checkout";
      case BookStatusType.LateCheckout:
        return "Late Checkout";
      case BookStatusType.Checkout:
        return "Checkout";
    }

    return "";
  }
}
