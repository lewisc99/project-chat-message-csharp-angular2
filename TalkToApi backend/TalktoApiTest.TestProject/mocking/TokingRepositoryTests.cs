using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using TalkToApiStudyTest.Database;
using TalkToApiStudyTest.V1.Models;
using TalkToApiStudyTest.V1.Repositories;
using TalkToApiStudyTest.V1.Repositories.Contracts;

namespace TalktoApiTest.TestProject.mocking
{

    [TestFixture]
    class TokingRepositoryTests
    {

        [SetUp]
        public void SetUp()
        {
        // _tokenRepository = new TokenRepository(_talkContext.Object);

        }

        [Test]
        public  void get_WhenCalled_returnToken()
        {

            var mockContext = new TalkToContext(new DbContextOptions<TalkToContext>());

            // Inject the mock DbContext into the repository
            var repository = new Mock<ITokenService>();
        

            Token newToken = new Token("", new ApplicationUser(), false, DateTime.Now.AddHours(2), DateTime.Now.AddHours(3), DateTime.Now);

            repository.Setup(method => method.Get(It.IsAny<string>())).ReturnsAsync(newToken);

            var _repository = new TokenRepository(mockContext);

           var result  = _repository.Get("").Result;


            Assert.That(result, Is.EqualTo(newToken));


        }

        [Test]
        public void get_WhenCalled_ReturnException()
        {
            Mock<ITokenService> mockRepo = new Mock<ITokenService>();


            Token newToken = new Token("", new ApplicationUser(), false, DateTime.Now.AddHours(2), DateTime.Now.AddHours(3), DateTime.Now);

            var expected = mockRepo.Setup(m => m.Get(Guid.NewGuid().ToString())).ReturnsAsync(newToken);

            Assert.AreNotEqual(expected, Is.EqualTo(newToken));


        }
    }
}
