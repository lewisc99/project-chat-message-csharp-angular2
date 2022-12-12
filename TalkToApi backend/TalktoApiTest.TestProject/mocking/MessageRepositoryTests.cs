using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TalkToApiStudyTest.V1.Models;
using TalkToApiStudyTest.V1.Repositories.Contracts;
using TalkToApiStudyTest.V1.Services;

namespace TalktoApiTest.TestProject.mocking
{

    [TestFixture]
   public class MessageRepositoryTests
    {

        [Test]
      public void get_WhenCalled_ReturnNewMessage()
        {

            Mock<IMessageRepository> repository = new Mock<IMessageRepository>();


            Message message = new Message();

            repository.Setup(method => method.Get(It.IsAny<int>())).ReturnsAsync(message);

            MessageService result = new MessageService(repository.Object);


            Assert.That(result.Get(It.IsAny<int>()).Result, Is.EqualTo(message));

        }

        [Test]
        public void get_WhenCalled_ReturnNotNull()
        {

            Mock<IMessageRepository> repository = new Mock<IMessageRepository>();

            Message message = new Message();

            repository.Setup(method => method.Get(It.IsAny<int>())).ReturnsAsync(message);

            MessageService result = new MessageService(repository.Object);


            Assert.IsNotNull(result.Get(It.IsAny<int>()).Result);

        }


        [Test]
        public void get_WhenCalled_ReturnException()
        {
            Mock<IMessageRepository> repository = new Mock<IMessageRepository>();


            Message message = new Message();

            repository.Setup(method => method.Get(It.IsAny<int>())).Throws<Exception>();

            MessageService result = new MessageService(repository.Object);

            Assert.ThrowsAsync<Exception>(() => result.Get(It.IsAny<int>()));
        }



        [Test]
        public void getMessages_WhenCalled_ReturnListOfMessages()
        {
            Mock<IMessageRepository> repository = new Mock<IMessageRepository>();


            Message message = new Message();

            List<Message> messages = new List<Message>();

            repository.Setup(method => method.GetMessages(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(messages);


            MessageService result = new MessageService(repository.Object);

            Assert.That(result.GetMessages(It.IsAny<string>(), It.IsAny<string>()).Result, Is.EqualTo(messages));
     
        }


        [Test]
        public void getMessages_WhenCalled_ReturnListOfMessagesNotEqualtoMessage()
        {
            Mock<IMessageRepository> repository = new Mock<IMessageRepository>();


            Message message = new Message();

            List<Message> messages = new List<Message>();

            repository.Setup(method => method.GetMessages(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(messages);


            MessageService result = new MessageService(repository.Object);

            Assert.That(result.GetMessages(It.IsAny<string>(), It.IsAny<string>()).Result, Is.Not.EqualTo(message));

        }



        [Test]
        public void getMessages_WhenCalled_ReturnException()
        {
            Mock<IMessageRepository> repository = new Mock<IMessageRepository>();

            Message messageOne = new Message() {  Id = 1, FromId = "", ToId = "" , Text =  "Test "};
            Message messageTwo = new Message() {  Id = 2, FromId = "", ToId = "" , Text =  "Test "};


            List<Message> messages = new List<Message>();



            messages.Add( messageOne);
            messages.Add( messageTwo);

            repository.Setup(method => method.GetMessages(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(messages);


            MessageService result = new MessageService(repository.Object);


            Assert.That(result.GetMessages(It.IsAny<string>(), It.IsAny<string>()).Result.Count, Is.EqualTo(2));

        }

        [Test]
        public void getMessages_WhenCalled_ReturnTwoMessages()
        {
            Mock<IMessageRepository> repository = new Mock<IMessageRepository>();



            List<Message> messages = new List<Message>();

            repository.Setup(method => method.GetMessages(It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new Exception());


            MessageService result = new MessageService(repository.Object);


            Assert.ThrowsAsync<Exception>(() => result.GetMessages(It.IsAny<string>(), It.IsAny<string>()));
        }


        [Test]
        public void register_WhenCalled_RegisterMessage()
        {
            Mock<IMessageRepository> repository = new Mock<IMessageRepository>();

            repository.Setup(method => method.Register(It.IsAny<Message>()));


            MessageService result = new MessageService(repository.Object);


            Assert.That(result.Get(It.IsAny<int>()).Result, Is.EqualTo(It.IsAny<Message>()));
        }

        [Test]
        public void update_WhenCalled_UpdateAMessage()
        {
            Mock<IMessageRepository> repository = new Mock<IMessageRepository>();



            repository.Setup(method => method.Update(It.IsAny<Message>()));


            MessageService result = new MessageService(repository.Object);


            Assert.That(result.Get(It.IsAny<int>()).Result, Is.EqualTo(It.IsAny<Message>()));
        }

    }
}
