export interface UserLoginState {
    id: string;
    fullname: string;
    username: string;
    userlevel: number;
}

export interface RootState {
    app_title: string;
    app_version: string;
}