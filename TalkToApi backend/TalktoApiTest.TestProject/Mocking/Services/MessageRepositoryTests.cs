using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TalkToApiStudyTest.V1.Models;
using TalkToApiStudyTest.V1.Repositories.Contracts;
using TalkToApiStudyTest.V1.Services;

namespace TalktoApiTest.TestProject.Mocking.Services
{

    [TestFixture]
   public class MessageRepositoryTests
    {

        Mock<IMessageRepository> repository;
        Message message;
        List<Message> messages;

        [SetUp]
        public void SetUp()
        {
            repository = new Mock<IMessageRepository>();
            message = new Message();
            messages = new List<Message>();
        }

        [Test]
      public void get_WhenCalled_ReturnNewMessage()
        {
            repository.Setup(method => method.Get(It.IsAny<int>())).ReturnsAsync(message);
            MessageService result = new MessageService(repository.Object);

            Assert.That(result.Get(It.IsAny<int>()).Result, Is.EqualTo(message));
        }

        [Test]
        public void get_WhenCalled_ReturnNotNull()
        {
            repository.Setup(method => method.Get(It.IsAny<int>())).ReturnsAsync(message);
            MessageService result = new MessageService(repository.Object);

            Assert.IsNotNull(result.Get(It.IsAny<int>()).Result);
        }


        [Test]
        public void get_WhenCalled_ReturnException()
        {
            repository.Setup(method => method.Get(It.IsAny<int>())).Throws<Exception>();
            MessageService result = new MessageService(repository.Object);

            Assert.ThrowsAsync<Exception>(() => result.Get(It.IsAny<int>()));
        }



        [Test]
        public void getMessages_WhenCalled_ReturnListOfMessages()
        {
            repository.Setup(method => method.GetMessages(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(messages);
            MessageService result = new MessageService(repository.Object);

            Assert.That(result.GetMessages(It.IsAny<string>(), It.IsAny<string>()).Result, Is.EqualTo(messages));
        }


        [Test]
        public void getMessages_WhenCalled_ReturnListOfMessagesNotEqualtoMessage()
        {
            repository.Setup(method => method.GetMessages(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(messages);
            MessageService result = new MessageService(repository.Object);

            Assert.That(result.GetMessages(It.IsAny<string>(), It.IsAny<string>()).Result, Is.Not.EqualTo(message));
        }



        [Test]
        public void getMessages_WhenCalled_ReturnException()
        {
            Message messageOne = new Message() {  Id = 1, FromId = "", ToId = "" , Text =  "Test "};
            Message messageTwo = new Message() {  Id = 2, FromId = "", ToId = "" , Text =  "Test "};

            messages.Add( messageOne);
            messages.Add( messageTwo);

            repository.Setup(method => method.GetMessages(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(messages);
            MessageService result = new MessageService(repository.Object);

            Assert.That(result.GetMessages(It.IsAny<string>(), It.IsAny<string>()).Result.Count, Is.EqualTo(2));
        }

        [Test]
        public void getMessages_WhenCalled_ReturnTwoMessages()
        {
            repository.Setup(method => method.GetMessages(It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new Exception());
            MessageService result = new MessageService(repository.Object);

            Assert.ThrowsAsync<Exception>(() => result.GetMessages(It.IsAny<string>(), It.IsAny<string>()));
        }


        [Test]
        public void register_WhenCalled_RegisterMessage()
        {
            repository.Setup(method => method.Register(It.IsAny<Message>()));
            MessageService result = new MessageService(repository.Object);

            Assert.That(result.Get(It.IsAny<int>()).Result, Is.EqualTo(It.IsAny<Message>()));
        }

        [Test]
        public void update_WhenCalled_UpdateAMessage()
        {
            repository.Setup(method => method.Update(It.IsAny<Message>()));
            MessageService result = new MessageService(repository.Object);

            Assert.That(result.Get(It.IsAny<int>()).Result, Is.EqualTo(It.IsAny<Message>()));
        }
    }
}
