using Moq;
using NUnit.Framework;
using System;
using TalkToApiStudyTest.V1.Models;
using TalkToApiStudyTest.V1.Repositories.Contracts;
using TalkToApiStudyTest.V1.Services;
using TalkToApiStudyTest.V1.Services.Contracts;

namespace TalktoApiTest.TestProject.mocking
{

    [TestFixture]
    class TokenRepositoryTests
    {

 

        [SetUp]
        public void SetUp()
        {


        }


        [Test]
        public  void get_WhenCalled_returnToken()
        {

             var repository = new Mock<ITokenRepository>();
        
             Token newToken = new Token("", new ApplicationUser(), false, DateTime.Now.AddHours(2), DateTime.Now.AddHours(3), DateTime.Now);

             repository.Setup(method => method.Get(It.IsAny<string>())).ReturnsAsync(newToken);

            var result = new TokenService(repository.Object);

            Assert.That(result.Get(It.IsAny<string>()).Result, Is.EqualTo(newToken));


        }


        [Test]
        public void get_WhenCalled_NotNull()
        {

            var repository = new Mock<ITokenRepository>();

            Token newToken = new Token("", new ApplicationUser(), false, DateTime.Now.AddHours(2), DateTime.Now.AddHours(3), DateTime.Now);

            repository.Setup(method => method.Get(It.IsAny<string>())).ReturnsAsync(newToken);

            var result = new TokenService(repository.Object);

            Assert.NotNull(result);

        }

        [Test]
        public void get_WhenCalled_ThrowsException()
        {
            var repository = new Mock<ITokenRepository>();

            Token newToken = new Token("", new ApplicationUser(), false, DateTime.Now.AddHours(2), DateTime.Now.AddHours(3), DateTime.Now);

            repository.Setup(method => method.Get(It.IsAny<string>())).Throws(new Exception());

            var result = new TokenService(repository.Object);

            Assert.ThrowsAsync<Exception>(() =>  result.Get(It.IsAny<string>()) );
        }



        [Test]
        public void get_WhenCalled_ReturnException()
        {
            Mock<ITokenService> mockRepo = new Mock<ITokenService>();


            Token newToken = new Token("", new ApplicationUser(), false, DateTime.Now.AddHours(2), DateTime.Now.AddHours(3), DateTime.Now);

            var expected = mockRepo.Setup(m => m.Get(Guid.NewGuid().ToString())).ReturnsAsync(newToken);

            Assert.AreNotEqual(expected, Is.EqualTo(newToken));


        }


        [Test]
        public void register_WhenCalled_ReturnNewToken()
        {

            Mock<ITokenRepository>  MockRepository = new Mock<ITokenRepository>();

            Token newToken = new Token("", new ApplicationUser(), false, DateTime.Now.AddHours(2), DateTime.Now.AddHours(3), DateTime.Now);

            MockRepository.

        }

    }
}
