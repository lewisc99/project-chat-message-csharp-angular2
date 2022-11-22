import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { HubConnection } from '@aspnet/signalr';
import { BehaviorSubject, Subject} from 'rxjs';
import { Message } from '../models/message';

@Injectable({
  providedIn: "root"
})
export class NotificationHubService {

  constructor(  ) { 
    this.initConnection();
    }

  private connectionId:string;
  private hubConnection!: HubConnection;
  public messages: Subject<Message> = new BehaviorSubject<Message>({});
  public brodcast: Subject<string> = new BehaviorSubject<string>("");

  private initConnection(): void 
  {
    try {

  
    let hubConnectionUrl = "https://localhost:44334/notify";
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(hubConnectionUrl)
      .build();
    }catch(error)
    {
      console.log("error");
    }

    this.hubConnection.start().then(() => console.log("Hub connection started")).catch((error) => console.error(error + " connection refused"));
  }

  public downConnection()
  {
    this.hubConnection.stop().then( () => console.log("Hub connectin off"));
  }
  
  
  notificationMessage():void 
{
  this.hubConnection.on("brodcastConnectionId", (data) =>
  {
    this.getConnectionId();
    console.log("User: " + data + "connection Id : " + this.connectionId);
  })
}

  userNotified():void
  {
    this.hubConnection.on("brodcastNotification", (message:Message)=>
    {
      console.log("User notified");
      this.messages.next(message);
    })
  }

getConnectionId():void 
{
  this.hubConnection.invoke('getConnectionId')
    .then( (connectionId)  => {
      this.DownConnectionId();
      this.connectionId = connectionId;
    });
}

  public getConnection():string 
  {
    return this.connectionId;
  }
  
  public DownConnectionId():void 
  {
    this.connectionId = "";
  }
}
