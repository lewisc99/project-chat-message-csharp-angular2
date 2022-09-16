import { User } from "src/app/models/User";


 export interface getToken 
{
    Token:string;
    userId:string;
    Expiration:string;
    RefreshToken:string;
    ExpirationRefreshToken:string;
}


export interface getUsers
{
    result: User[];
}