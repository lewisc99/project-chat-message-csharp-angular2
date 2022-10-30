import { Component, OnInit } from '@angular/core';
import { Message } from 'src/app/models/message';
import { MessageService } from 'src/app/services/message.service';
import { NotificationHubService } from 'src/app/services/notificationhub.service';
import { TokenStorageService } from 'src/app/services/token-storage.service';

@Component({
  selector: 'app-main-message',
  templateUrl: './main-message.component.html',
  styleUrls: ['./main-message.component.css']
})
export class MainMessageComponent implements OnInit {

  constructor(private storageToken:TokenStorageService, private messageService:MessageService,
    private notificationHubService:NotificationHubService) { }

  public messages:Message[] = [];
  public firstUserId:string = this.storageToken.getUserId();
  public secondUserById:string;

  ngOnInit(): void {

    this.notificationHubService.notificationMessage();
    this.notificationHubService.userNotified();
  
  }
 

  SelectUserMessage(secondUserId:string)
  {
    this.secondUserById = secondUserId;


    
    this.messageService.getUserMessages(this.firstUserId,secondUserId).subscribe(
      (response:any) =>
      {
       console.log(response);
       console.log("main-message messages");
       return this.messages = response;
      
      
      },(error:any) =>
      {
        console.log("user-message error: " + error);
      }
    )

    this.notificationHubService.messages.subscribe(
      (data:any) =>
      {
        console.log("from container main-messages");
       var message:Message = new Message(data.fromId, data.toId,data.text);
       console.log(message);
        this.messages.push(message);
      }
    );
      
  }

  getTextMessage(messagetext:string)
  {
    var message:Message  = new Message(this.firstUserId,this.secondUserById,messagetext);
    this.messageService.sendMessageToUser(message).subscribe(
      (response:any) =>
      {
        console.log("new Message Added");
        console.log(response);
        this.messages.push(response);
      },
      (error:any) =>
      {

      }
    )
  }





}
