﻿using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TalkToApiStudyTest.V1.Controllers;
using TalkToApiStudyTest.V1.Models;
using TalkToApiStudyTest.V1.Services.Contracts;

namespace TalktoApiTest.TestProject.Mocking.Controllers
{


    [TestFixture]
   public class MessageControllerTests
    {

        [Test]
      public   void getMessages_WhenCalled_ReturnListOfMessages()
        {
            Mock<IMessageService> repository = new  Mock<IMessageService>();

            Message message = new Message() { Id = 1, FromId = "1", ToId = "2", Text = "Hello Man!" };
            Message message2 = new Message() { Id = 2, FromId = "1", ToId = "2", Text = "Hello Man 2!" };
            List<Message> messages = new List<Message>();

            messages.Add( message);
            messages.Add(message2);

            repository.Setup(method => method.GetMessages(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(messages);
            MessageController messageController = new MessageController(repository.Object);

            var messageOkObject = messageController.GetMessages("1", "2", "").Result.Result as OkObjectResult;
            var result = (List<Message>) messageOkObject.Value;

            Assert.AreEqual(result.Count, 2);
            
        }

        [Test]
        public void get_WhenCalled_ReturnHttpStatus200()
        {
            Mock<IMessageService> repository = new Mock<IMessageService>();

            Message message = new Message() { Id = 1, FromId = "1", ToId = "2" , Text = "" };

            repository.Setup(method => method.Register(message));
            repository.Setup(method => method.Get(1)).ReturnsAsync(message);
            MessageController messageController = new MessageController(repository.Object);

            var result = messageController.Get(1).Result.Result as OkObjectResult;


            Assert.AreEqual(result.StatusCode, 200);

        }

        [Test]
        public  void get_WhenCalled_ReturnTheMessage()
        {
            Mock<IMessageService> repository = new Mock<IMessageService>();


            Message message = new Message() { Id = 1, FromId = "1", ToId = "2", Text = "Hello Man!" };

            repository.Setup(method => method.Register(message));
            repository.Setup(method => method.Get(1)).ReturnsAsync(message);
            MessageController messageController = new MessageController(repository.Object);


            var messageObject =  messageController.Get(1).Result.Result as OkObjectResult;
            Message result2 = (Message) messageObject.Value;

            Assert.AreEqual(result2.Text, "Hello Man!");
          
        }
    }
}
