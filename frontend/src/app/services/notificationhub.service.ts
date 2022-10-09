import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { HubConnection } from '@aspnet/signalr';

@Injectable({
  providedIn: 'root'
})
export class NotificationHubService {

  constructor() { 
    
    this.initConnection();
  }

  
  private hubConnection!: HubConnection;


  private initConnection(): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl( "https://localhost:44334/"+ "notify")
      .build();
    this.hubConnection.start().then(() => console.log("Hub connection started"))
      
  }
  

  notificationMessage():void 
{
  this.hubConnection.on("brodcastNotification", () =>
  {
    console.log("Hey, User  has Been notified");
  })
}

}
