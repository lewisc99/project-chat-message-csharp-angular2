import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, map, Observable, throwError } from 'rxjs';
import { Login } from '../models/login';
import { MessageService } from './message.service';
import { getToken, getUsers } from '../interfaces/interfaces';
import { User } from '../models/User';
import { Token } from '../models/token';
import { TokenStorageService } from './token-storage.service';


@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpclient:HttpClient, private messageService: MessageService, private tokenStorage:TokenStorageService)
  {}

  private fullUrl = "https://localhost:44334/api/user/";
  
  
  

  private  headers = new HttpHeaders({
    'Content-Type': 'application/json',
    "Accept":"application/json",
    'Access-Control-Allow-Headers': 'Content-Type',
    'crossDomain': 'true',
    'Access-Control-Allow-Origin':'*',
    'Authorization':''
   
    });

  
  loginUser(loginForm:Login):Observable<Token>
  {
    let baseUrl = this.fullUrl + "login";

    var options =  {
        headers: this.headers
    };
    

    return  this.httpclient.post<Login>(baseUrl,loginForm,options).pipe(
      map(
        (response:any) =>
        {
      
         this.tokenStorage.saveToken(response);

         console.log("service response  Token: "  + response.Token);
         return response;
        }),catchError(
          (error:any) =>
          {
            console.log("service error: " + error.message);
            return throwError(error);
          })

    );
  }

 public getUsers():Observable<User[]> //the return 
  {
    

    
    var headerGetUser = new HttpHeaders({
      'Content-Type': 'application/json',
      "Accept":"application/json",
      'Access-Control-Allow-Headers': 'Content-Type',
      'crossDomain': 'true',
      'Access-Control-Allow-Origin':'*',
      'Authorization': 'Bearer ' + this.tokenStorage.getToken()
      });


    var options = {
      headers:headerGetUser
    };

    return this.httpclient.get<getUsers>(this.fullUrl,options).pipe(
      map(
        (response:any) =>
        {
         console.log(response);
          return response;
        }),
        catchError(
          (error:any) => {
            console.log("error Get Users: " + error.message);
            return throwError(error);
          }
        )
    )

   
  }

}
