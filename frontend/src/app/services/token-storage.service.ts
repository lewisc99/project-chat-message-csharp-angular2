import { Token } from '@angular/compiler';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TokenStorageService {

  constructor() { }

  private storageToken:Storage = sessionStorage;

  public saveToken(token:string)
  {
    this.storageToken.removeItem("token");
    this.storageToken.setItem("token",JSON.stringify(token));
  }

  public getToken():string 
  {
    let getObject  = JSON.parse( this.storageToken.getItem("token")!);
    let token = getObject['Token'];
    return token;
  }

  public getUserId():string
  {
  let getObject  = JSON.parse( this.storageToken.getItem("token")!);
  let userId = getObject['UserId'];
  return userId;
  }


}
