import { Component, OnInit } from '@angular/core';
import { TokenStorageService } from '../../services/token-storage.service';
import { UserService } from '../../services/user.service';
import { NotificationHubService } from '../../services/notificationhub.service';
import { Router } from '@angular/router';
import { map } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  tokenIsValid:boolean = false;
  constructor(private TokenStorageService: TokenStorageService, private userService: UserService, private notificationHubService:NotificationHubService, private storageToken:TokenStorageService,
    private router:Router) { }
  ngOnInit(): void {
      this.userIsLogIn();
  }

  userIsLogIn()
  {
   
      this.TokenStorageService.isTokenValid.subscribe(
        (token: any) =>
         {
          if (!token)  
          {
            this.tokenIsValid = false;
          }
          else
          {
            this.tokenIsValid = true;
          }
         }
      )

  }


  logout()
  {
    this.userService.logout().subscribe(
    (data:any) =>
    {
      if (data.logout)
      {
        console.log("main-message-logout");
        this.notificationHubService.DownConnectionId();
        this.notificationHubService.downConnection();
        this.storageToken.cleanToken();
      }
      this.tokenIsValid = false;
      
    }, 
    (error:any) =>
    {
      console.log("main-message-logout error");
    }
    )
    
  }

  
}
