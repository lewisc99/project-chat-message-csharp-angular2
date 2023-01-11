using Microsoft.AspNetCore.JsonPatch;
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


        private Mock<IMessageService> repository;

        [SetUp]
        public void beforeEach()
        {
            repository = new Mock<IMessageService>();

        }

        [Test]
      public   void getMessages_WhenCalled_ReturnListOfMessages()
        {

            Message message = new Message() { Id = 1, FromId = "1", ToId = "2", Text = "Hello Man!" };
            Message message2 = new Message() { Id = 2, FromId = "1", ToId = "2", Text = "Hello Man 2!" };
            List<Message> messages = new List<Message>();

            messages.Add( message);
            messages.Add(message2);

            repository.Setup(method => method.GetMessages(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(messages);
            MessageController messageController = new MessageController(repository.Object, null, null);

            var messageOkObject = messageController.GetMessages("1", "2", "").Result.Result as OkObjectResult;
            var result = (List<Message>) messageOkObject.Value;

            Assert.AreEqual(result.Count, 2);
            
        }


        [Test]
        public void getMessages_WhenCalled_ThrowException()
        {

            repository.Setup(method => method.GetMessages(It.IsAny<string>(), It.IsAny<string>())).Throws<NullReferenceException>();
            MessageController messageController = new MessageController(repository.Object, null, null);

            var result = messageController.GetMessages("1", "2", "").Result.Result as UnprocessableEntityObjectResult;

           Assert.That(result.StatusCode == 422);

        }

      

        [Test]
        public void getMessages_WhenCalled_ReturnUnprocessableEntity()
        {


            MessageController messageController = new MessageController(repository.Object, null, null);

            var result = messageController.GetMessages("1", "1", "").Result.Result as UnprocessableEntityResult;

            Assert.AreEqual(result.StatusCode, 422);
        }


        [Test]
        public void get_WhenCalled_ReturnHttpStatus200()
        {

            Message message = new Message() { Id = 1, FromId = "1", ToId = "2" , Text = "" };

            repository.Setup(method => method.Register(message));
            repository.Setup(method => method.Get(1)).ReturnsAsync(message);
            MessageController messageController = new MessageController(repository.Object, null, null);

            var result = messageController.Get(1).Result.Result as OkObjectResult;

            Assert.AreEqual(result.StatusCode, 200);

        }


      [Test]
        public  void get_WhenCalled_ReturnTheMessageText()
        {


            Message message = new Message() { Id = 1, FromId = "1", ToId = "2", Text = "Hello Man!" };

            repository.Setup(method => method.Register(message));
            repository.Setup(method => method.Get(1)).ReturnsAsync(message);
            MessageController messageController = new MessageController(repository.Object, null, null);


            var messageObject =  messageController.Get(1).Result.Result as OkObjectResult;
            Message result2 = (Message) messageObject.Value;

            Assert.AreEqual(result2.Text, "Hello Man!");
          
        }



        [Test]
        public void patch_WhenCalled_PartialUpdateMessage()
        {

            //create a mock messageConnectionId user
            MessageConnectionId message = new MessageConnectionId() { Id = 1, FromId = "1", ToId = "2", Text = "Hello Man!" };

            //create a mock of message
            Message messageEntity = new Message() { Id = 1, FromId = "1", ToId = "2", Text = "Hello Man!" };


            //register a new message
            repository.Setup(method => method.Register(messageEntity));
            //get the message registry
            repository.Setup(method => method.Get(1)).ReturnsAsync(messageEntity);

            MessageController messageController = new MessageController(repository.Object, null, null);

            //call the http method get to check if the messages is register (because of method.Register(messageEntity);
            var messageObject = messageController.Get(1).Result.Result as OkObjectResult;

            //will get the json Ok() and convert to message
            Message result = (Message)messageObject.Value;

            //check if the text message saved in the database is "Hello man!"
            Assert.AreEqual(result.Text, "Hello Man!");


            //will mock a JsonPatchDocument or partial update
            //only the text wil be updated
            JsonPatchDocument<Message> json = new JsonPatchDocument<Message>();
            json.Add( s => s.Text, "ola mundo");

            //will mock a http request to partialUpdate
            var messageUpdated = messageController.PartialUpdate(1, json, "").Result.Result as OkObjectResult;
            result = (Message)messageUpdated.Value;

            //will check if the object result was updated.
            Assert.AreEqual(result.Text, "ola mundo");
        }




        [Test]
        public void patch_whenCalled_ReturnBadRequest()
        {
            MessageConnectionId message = new MessageConnectionId() { Id = 1, FromId = "1", ToId = "2", Text = "Hello Man!" };
            Message messageEntity = new Message() { Id = 1, FromId = "1", ToId = "2", Text = "Hello Man!" };
            repository.Setup(method => method.Register(messageEntity));
            repository.Setup(method => method.Get(1)).ReturnsAsync(messageEntity);
            MessageController messageController = new MessageController(repository.Object, null, null);
            JsonPatchDocument<Message> json = new JsonPatchDocument<Message>();

            var result = messageController.PartialUpdate(1, json, "").Result.Result as StatusCodeResult;
            Assert.AreEqual(result.StatusCode, 400);
        }

    }
}
