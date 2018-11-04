import { su, executeScalar } from "@/lib/Test";

export enum RoomStateType {
  Vacant = 1,
  Booked,
  Occupied,
  Cleaning,
  Maintance,
  LateCheckout
}

export class Room {
  public static ChangeState(roomId: number, state: RoomStateType): void {
    let qry: any = su()
      .table("rooms")
      .set("RoomStateId", state)
      .where("Id = ?", roomId);
    executeScalar(qry);
  }
}