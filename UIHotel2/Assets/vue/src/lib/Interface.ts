export interface IDB {
  Query(qry: string, params: Array<any>): Array<any>;
  QueryScalar: any;
  Test(
    host: string,
    port: number,
    user: string,
    password: string,
    database: string,
    callback: (is_okay: boolean) => void
  ): void;
  TestConnect(
    host: string,
    port: number,
    user: string,
    password: string,
    callback: (is_okay: boolean) => void
  ): void;
  Migrate(
    host: string,
    port: number,
    user: string,
    password: string,
    database: string,
    callback: (is_okay: boolean) => void
  ): void;
}

export interface IApp {
  OpenDialog(callback: (data: any) => void): void;
  OpenDialog(filter: string, callback: (data: any) => void): void;
  SaveDialog(callback: (data: any) => void): void;
  SaveDialog(filter: string, callback: (data: any) => void): void;
  GetUploadUrl(filehash: string): string;
  GetNewBookingNumber(): string;
}

export enum UserLevel {
  Administrator,
  Manager,
  Receptionist,
  Cleaner
}

export interface IUserModel {
  Username: string;
  Fullname: string;
  Password: string;
  Level: UserLevel;
  IsActive: boolean;
}

export interface IAuth {
  List(): Array<any>;
  Get(id: number): any;
  Create(user: IUserModel): any;
  Update(id: number, user: IUserModel): any;
  Delete(id: number): boolean;
  ResetPassword(id: number, newpassword: string): boolean;
  ChangePassword(id: number, oldpassword: string, newpassword: string): boolean;
  Validate(username: string, password: string): any | undefined;
}

export interface IRoomUsed {
  Used: number;
  Total: number;
}

export interface IHotel {
  TransactionReportDownload(start: Date, end: Date): void;
  BookingReportDownload(start: Date, end: Date): void;
  CalcTransaction(is_normalize: boolean, callback: () => void): void;
  CalcBooking(callback: () => void, invoiceId?: string): void;
  Visitor: number;
  Room: IRoomUsed;
  Balance: number;
  UniqueVisitor: number;
}

export interface ISetting {
  App_Name: string;
  App_Key: string;
  Hotel_Name: string;
  Hotel_Logo: string;
  Hotel_Address: string;
  Hotel_Email: string;
  Hotel_Phone: string;
  Deposit: number;
  Penalty: number;
  Time_Checkin: string;
  Time_Checkout: string;
  Time_Fullcharge: string;
  SQL_Database: string;
  SQL_Host: string;
  SQL_Port: number;
  SQL_User: string;
  SQL_Password: string;
  Save(): void;
  Load(): void;
}

export interface ICS {
  DB: IDB;
  App: IApp;
  Auth: IAuth;
  Hotel: IHotel;
  Setting: ISetting;
}
