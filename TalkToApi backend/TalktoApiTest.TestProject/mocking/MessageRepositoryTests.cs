using Moq;
using NUnit.Framework;
using System;
using TalkToApiStudyTest.V1.Models;
using TalkToApiStudyTest.V1.Repositories;
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



    }
}
