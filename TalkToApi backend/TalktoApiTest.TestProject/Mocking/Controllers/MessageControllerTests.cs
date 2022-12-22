using Microsoft.AspNetCore.Mvc;
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

            List<Message> messages = new List<Message>();

            var idOne = Guid.NewGuid();
            var idTwo = Guid.NewGuid();

         



            MessageController messageController = new MessageController(repository.Object);

            var result = messageController.GetMessages("1", "2", "").Result.Result as OkObjectResult;

            
            Assert.AreEqual(result.StatusCode, 200);


            
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
    }
}
