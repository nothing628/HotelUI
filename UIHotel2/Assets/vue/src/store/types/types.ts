export interface UserLoginState {
    Id: number;
    Fullname: string;
    Username: string;
    Level: number;
}

export interface RootState {
    app_title: string;
    app_version: string;
    page_title: string;
    page_subtitle: string;
}