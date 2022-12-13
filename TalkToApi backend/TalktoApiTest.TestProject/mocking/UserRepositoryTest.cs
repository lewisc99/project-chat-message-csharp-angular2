﻿using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using System;
using TalkToApiStudyTest.V1.Models;
using TalkToApiStudyTest.V1.Repositories;
using TalkToApiStudyTest.V1.Repositories.Contracts;
using TalkToApiStudyTest.V1.Services;

namespace TalktoApiTest.mocking.TestProject
{

    [TestFixture]
   public class UserRepositoryTest
    {


        private readonly Mock<UserManager<ApplicationUser>> mockUserManager;


        [SetUp]
        public void SetUp()
        {
          //  _userRepository = new UserRepository(mockUserManager.Object);
        }


        [Test]
        public void get_whenCalled_ReturnUser()
        {

            Mock<IUserRepository> repository = new  Mock<IUserRepository>();


            ApplicationUser user = new ApplicationUser() { Email = "example@gmail.com", FullName = "Lewis", Id = ""};
            repository.Setup(m => m.Get(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(user);


            UserService result = new UserService(repository.Object);

            Assert.That(result.Get(It.IsAny<string>(), It.IsAny<string>()).Result, Is.EqualTo(user));

        }


        [Test]
        public void get_whenCalled_ThrowException()
        {

            Mock<IUserRepository> repository = new Mock<IUserRepository>();


            repository.Setup(m => m.Get(It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new Exception());


            UserService result = new UserService(repository.Object);

            Assert.ThrowsAsync<Exception>(() => result.Get(It.IsAny<string>(), It.IsAny<string>()));

        }


        [Test]
        public void register_whenCalled_CreateANewUser()
        {

        }

    }
}
