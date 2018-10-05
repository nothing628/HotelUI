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

export interface ICS {
  DB: IDB;
  App: IApp;
}
