export interface IUserLoginState {
  Id: number;
  Fullname: string;
  Username: string;
  Level: number;
}

export interface ISettingState {
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
}

export interface IRootState {
  app_title: string;
  app_version: string;
  page_title: string;
  page_subtitle: string;
}