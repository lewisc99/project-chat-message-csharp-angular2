using Moq;
using NUnit.Framework;
using System;
using TalkToApiStudyTest.V1.Models;
using TalkToApiStudyTest.V1.Repositories.Contracts;
using TalkToApiStudyTest.V1.Services;

namespace TalktoApiTest.TestProject.Mocking.Services
{

    [TestFixture]
   public class UserRepositoryTests
    {

        Mock<IUserRepository> repository;
        ApplicationUser user;
        UserService result;

        [SetUp]
        public void SetUp()
        {
           repository = new  Mock<IUserRepository>();
           user = new ApplicationUser() { Email = "example@gmail.com", FullName = "Lewis", Id = "" };
           repository.Setup(m => m.Get(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(user);
           result = new UserService(repository.Object);
        }


        [Test]
        public void get_whenCalled_ReturnUser()
        {
            Assert.That(result.Get(It.IsAny<string>(), It.IsAny<string>()).Result, Is.EqualTo(user));
        }


        [Test]
        public void get_whenCalled_ThrowException()
        {
            repository.Setup(m => m.Get(It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new Exception());
            UserService result = new UserService(repository.Object);

            Assert.ThrowsAsync<Exception>(() => result.Get(It.IsAny<string>(), It.IsAny<string>()));
        }

        [Test]
        public void register_whenCalled_GetWithFullNameLewis()
        {
            Assert.That(result.Get(It.IsAny<string>(), It.IsAny<string>()).Result.FullName, Is.EqualTo("Lewis"));
        }



        [Test]
        public void register_whenCalled_CreateANewUser()
        {
            repository.Setup(m => m.Register(user, It.IsAny<string>()));
            //  var result = new UserService(repository.Object);

            Assert.That(result.Get(It.IsAny<string>()).Result, Is.EqualTo(It.IsAny<ApplicationUser>()));
        }
    }
}
