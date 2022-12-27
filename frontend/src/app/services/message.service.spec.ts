import { TestBed } from '@angular/core/testing';

import { HttpClientTestingModule , HttpTestingController} from '@angular/common/http/testing';
import { HttpErrorResponse } from '@angular/common/http';
import { MessageService } from './message.service';
import { TokenStorageService } from './token-storage.service';
import { NotificationHubService } from './notificationhub.service';
import { Message } from '../models/message';


describe('Message Service', () =>
{
    let service: MessageService;
    let httpMock: HttpTestingController
    let tokenStorage: TokenStorageService;
    let notificationHub: NotificationHubService;
    
    
    const messagesMock:Message[] = [
        {Id:"1",FromId:"1", ToId:"2", Text:"Hello World", ToConnectionId: ""},
        {Id:"2",FromId:"2", ToId:"1", Text:"Hello World 2", ToConnectionId: ""}
    ]

   

    beforeEach(async() => {
        TestBed.configureTestingModule({
          imports:[HttpClientTestingModule],
          providers:[MessageService, TokenStorageService, NotificationHubService]
    
        }).compileComponents();

        service = TestBed.get(MessageService);
        tokenStorage = TestBed.get(TokenStorageService);
        notificationHub = TestBed.get(NotificationHubService);
        httpMock = TestBed.get(HttpTestingController);
      });

      

    it('should return expected messages when getUserMessages()', () =>
    {
        

        service.getUserMessages("","").subscribe(
            messages =>
            {
                expect(messages.length).toBe(2);
                expect(messages).toEqual(messagesMock);
            }
        )
        
    });

    it('should retrieve posts from the API via GET using mockController', () => {

          
    const messagesMock:Message[] = [
        {Id:"1",FromId:"1", ToId:"2", Text:"Hello World", ToConnectionId: ""},
        {Id:"2",FromId:"2", ToId:"1", Text:"Hello World 2", ToConnectionId: ""}
    ]
    
        service.getUserMessages("1","2").subscribe(messages =>
          {
            expect(messages.length).toBe(2);
            expect(messages).toEqual(messagesMock);
          });
    
          const request = httpMock.expectOne(`${service.urlDefaultMessage}/posts`);
    
      });
    
})