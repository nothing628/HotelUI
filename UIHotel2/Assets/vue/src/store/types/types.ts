export interface IUserLoginState {
    Id: number;
    Fullname: string;
    Username: string;
    Level: number;
}

export interface IRootState {
    app_title: string;
    app_version: string;
    page_title: string;
    page_subtitle: string;
}