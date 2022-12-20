using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TalkToApiStudyTest.V1.Controllers;
using TalkToApiStudyTest.V1.Models;
using TalkToApiStudyTest.V1.Models.dto;
using TalkToApiStudyTest.V1.Services.Contracts;

namespace TalktoApiTest.TestProject.Mocking.Controllers
{


    [TestFixture]
   public class MessageControllerTests
    {

        [Test]
      public  void getMessages_WhenCalled_ReturnListOfMessages()
        {
            Mock<IMessageService> repository = new  Mock<IMessageService>();
          MessageController messageController = new MessageController(repository.Object);

            List<Message> messages = new List<Message>();

            Message message1 = new Message() { Created = DateTime.UtcNow, FromId = "", ToId = "", Text = "", Id = 1 };
            Message message2 = new Message() { Created = DateTime.UtcNow, FromId = "", ToId = "", Text = "", Id = 2 };

            messages.Add(message1);
            messages.Add(message2);

            repository.Setup(method => method.GetMessages(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(messages);
           
            Assert.AreEqual( messageController.GetMessages(It.IsAny<string>(), It.IsAny<string>()  , It.IsAny<string>()).Result, messages  as UnprocessableEntityResult);
           ;
        }
    }
}
