using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using TalkToApiStudyTest.Database;
using TalkToApiStudyTest.V1.Models;
using TalkToApiStudyTest.V1.Repositories;
using TalkToApiStudyTest.V1.Repositories.Contracts;
using TalkToApiStudyTest.V1.Services;
using TalkToApiStudyTest.V1.Services.Contracts;

namespace TalktoApiTest.TestProject.mocking
{

    [TestFixture]
    class TokingRepositoryTests
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
        public void get_WhenCalled_ReturnException()
        {
            Mock<ITokenService> mockRepo = new Mock<ITokenService>();


            Token newToken = new Token("", new ApplicationUser(), false, DateTime.Now.AddHours(2), DateTime.Now.AddHours(3), DateTime.Now);

            var expected = mockRepo.Setup(m => m.Get(Guid.NewGuid().ToString())).ReturnsAsync(newToken);

            Assert.AreNotEqual(expected, Is.EqualTo(newToken));


        }
    }
}
