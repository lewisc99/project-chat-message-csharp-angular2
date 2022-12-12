using Moq;
using NUnit.Framework;
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

            MessageService messageService = new MessageService(repository.Object);


            Assert.That(messageService.Get(It.IsAny<int>()).Result, Is.EqualTo(message));

        }



    }
}
