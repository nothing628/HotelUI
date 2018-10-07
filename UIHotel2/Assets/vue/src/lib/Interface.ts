export interface IDB {
  Query(qry: string, params: Array<any>): Array<any>;
  QueryScalar: any;
}

export interface IApp {
  OpenDialog(callback: (data: any) => void): void;
  OpenDialog(filter: string, callback: (data: any) => void): void;
  SaveDialog(callback: (data: any) => void): void;
  SaveDialog(filter: string, callback: (data: any) => void): void;
  GetUploadUrl(filehash: string): string;
  GetNewBookingNumber(): string;
  CalcTransaction(callback: () => void): void;
}

export interface ISetting {
  App_Key: string;
  Hotel_Name: string;
  Hotel_Logo: string;
  Hotel_Address: string;
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
  Setting: ISetting;
}
