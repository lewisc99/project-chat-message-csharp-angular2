import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { User } from 'src/app/models/User';
import { MessageService } from 'src/app/services/message.service';
import { TokenStorageService } from 'src/app/services/token-storage.service';
@Component({
  selector: 'app-users-messages',
  templateUrl: './users-messages.component.html',
  styleUrls: ['./users-messages.component.css']
})
export class UsersMessagesComponent implements OnInit {

  constructor(private userService:UserService, private messageService:MessageService, private tokenStorageService:TokenStorageService) { }
  @Output() secondUserId = new EventEmitter();

  users:User[];

  ngOnInit(): void {
    this.getUsers();
  }

  private getUsers()
  {
      this.userService.getUsers().subscribe(
      (response:any) =>
      {
        this.users = response;
      },
      (error:any) =>
      {
        console.log(error);
      })
  }

  SelectUserMessage(secondUserId:string, username:string)
  {
    console.log(secondUserId);
    this.secondUserId.emit({secondUserId,username});
  }

}
