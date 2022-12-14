using Moq;
using NUnit.Framework;
using System;
using TalkToApiStudyTest.V1.Models;
using TalkToApiStudyTest.V1.Repositories.Contracts;
using TalkToApiStudyTest.V1.Services;

namespace TalktoApiTest.TestProject.Mocking.Services
{

    [TestFixture]
    class TokenRepositoryTests
    {

        Mock<ITokenRepository> repository;
        Token newToken;
        TokenService result;

        [SetUp]
        public void SetUp()
        {
            repository = new Mock<ITokenRepository>();
            newToken = new Token("", new ApplicationUser(), false, DateTime.Now.AddHours(2), DateTime.Now.AddHours(3), DateTime.Now);
            repository.Setup(method => method.Get(It.IsAny<string>())).ReturnsAsync(newToken);
            result = new TokenService(repository.Object);
        }

        [Test]
        public  void get_WhenCalled_returnToken()
        {
            Assert.That(result.Get(It.IsAny<string>()).Result, Is.EqualTo(newToken));
        }


        [Test]
        public void get_WhenCalled_NotNull()
        {
            repository.Setup(method => method.Get(It.IsAny<string>())).ReturnsAsync(newToken);
            var result = new TokenService(repository.Object);

            Assert.NotNull(result);

        }

        [Test]
        public void get_WhenCalled_ThrowsException()
        {
            Assert.That(result.Get(It.IsAny<string>()).Result, Is.EqualTo(newToken));

            repository.Setup(method => method.Get(It.IsAny<string>())).Throws(new Exception());
            Assert.ThrowsAsync<Exception>(() =>  result.Get(It.IsAny<string>()) );
        }



        [Test]
        public void get_WhenCalled_ReturnException()
        {
            var expected = repository.Setup(m => m.Get(Guid.NewGuid().ToString())).ReturnsAsync(newToken);
            Assert.AreNotEqual(expected, Is.EqualTo(newToken));
        }

    }
}
