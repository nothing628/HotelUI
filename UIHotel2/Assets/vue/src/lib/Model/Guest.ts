import { ss, si, execute, executeScalar } from "@/lib/Test";
import moment from "moment";

export interface IGuest {
  IdNumber: string;
  Fullname: string;
  Email: string;
  IsVIP: boolean;
  BirthPlace: string;
  BirthDay: string;
  Phone1: string;
  Phone2: string;
  Address: string;
  City: string;
  Province: string;
  State: string;
  PhotoDoc: string;
  PhotoGuest: string;
}

export class Guest {
  public static Store(user: IGuest): number {
    var new_moment = moment();
    var qry = si()
      .into("guests")
      .set("IdKind", "KTP")
      .set("IdNumber", user.IdNumber)
      .set("Fullname", user.Fullname)
      .set("Email", user.Email)
      .set("IsVIP", user.IsVIP ? 1 : 0)
      .set("BirthPlace", user.BirthPlace)
      .set("BirthDay", user.BirthDay)
      .set("Phone1", user.Phone1)
      .set("Phone2", user.Phone2)
      .set("Address", user.Address)
      .set("City", user.City)
      .set("Province", user.Province)
      .set("State", user.State)
      .set("PhotoDoc", user.PhotoDoc)
      .set("PhotoGuest", user.PhotoGuest)
      .set("CreateAt", new_moment.format("YYYY-MM-DD"))
      .set("UpdateAt", new_moment.format("YYYY-MM-DD"));
    var result = executeScalar(qry);

    return Number(result);
  }
}
