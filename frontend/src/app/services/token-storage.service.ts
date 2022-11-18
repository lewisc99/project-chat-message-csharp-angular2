import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TokenStorageService {

  constructor() { 
    this.isTokenValid =false;
  }
  public isTokenValid:boolean;

  private storageToken:Storage = sessionStorage;

  public saveToken(token:string)
  {
    this.storageToken.removeItem("token");
    this.storageToken.setItem("token",JSON.stringify(token));
    this.isTokenValid = true;
  }

  public getToken():string 
  {
    let getObject  = JSON.parse( this.storageToken.getItem("token")!);

    let token = getObject['Token'];
    if ( token == "")
    {
        return "";
    }
    
    this.isTokenValid = true;
    return token;
  }




  public getUserId():string
  {
  let getObject  = JSON.parse( this.storageToken.getItem("token")!);
  let userId = getObject['UserId'];
  return userId;
  }

  public cleanToken():void
  {
      this.storageToken.removeItem("token");
      this.isTokenValid = false;
  }

}
