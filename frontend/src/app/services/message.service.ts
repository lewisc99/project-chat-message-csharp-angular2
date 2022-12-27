import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {  catchError, map, Observable, throwError } from 'rxjs';
import { Message } from '../models/message';
import { NotificationHubService } from './notificationhub.service';
import { TokenStorageService } from './token-storage.service';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  constructor(private httpCient:HttpClient, private tokenStorage:TokenStorageService, private notificationHub:NotificationHubService) { }

  public urlDefaultMessage = "https://localhost:44334/api/message/";

    public getUserMessages(userIdOne:string,userIdTwo:string):Observable<Message[]>
    {
      let urlWithUserIds = this.urlDefaultMessage + userIdOne + "/" + userIdTwo;
      let  headers = new HttpHeaders({
        'Content-Type': 'application/json',
        "Accept":"application/vnd.talkto.hateoas+json",
        'Access-Control-Allow-Headers': 'Content-Type',
        'crossDomain': 'true',
        'Access-Control-Allow-Origin':'*',
        'Authorization':"Bearer " + this.tokenStorage.getToken()
        });

      var options =  {
        headers: headers
      };

      return this.httpCient.get<getMessages>(urlWithUserIds,options).pipe(
        map(
          (response:any) =>
          {
            console.log(response.Result);
            return response.Result;
          }
        ),catchError(
          (error:any) =>
          {
            console.log("Message Service error: " );
            console.log(error);
            return throwError(error);
          }
        )
       )
    }

    public  sendMessageToUser(message:Message):Observable<Message>
    {
      let  headers = new HttpHeaders({
        'Content-Type': 'application/json',
        "Accept":"application/vnd.talkto.hateoas+json",
        'Access-Control-Allow-Headers': 'Content-Type',
        'crossDomain': 'true',
        'Access-Control-Allow-Origin':'*',
        'Authorization':"Bearer " + this.tokenStorage.getToken()
        });

        var options =  {
          headers: headers
        };

        message.ToConnectionId = this.notificationHub.getConnection();

        return this.httpCient.post(this.urlDefaultMessage,message,options).pipe(
          map(
            (response:any) =>
            {
                return response;
            }
          ), 
          catchError(
            (error:any) =>
            {
              return throwError(error);
            }
          )
        )
    }
}
interface getMessages
{
  Result:Message[]
}