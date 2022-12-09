using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using TalkToApiStudyTest.V1.Models;
using TalkToApiStudyTest.V1.Repositories;
using TalkToApiStudyTest.V1.Repositories.Contracts;

namespace TalktoApiTest.TestProject
{

    [TestFixture]
   public class UserRepositoryTest
    {


        private Mock<UserManager<ApplicationUser>> mockUserManager;
        private UserRepository _userRepository;


        [SetUp]
        public void SetUp()
        {
            _userRepository = new UserRepository(mockUserManager.Object);
        }


        [Test]
        public void getUser_whenCalled_ReturnUser()
        {

            ApplicationUser user = new ApplicationUser();

            user.UserName ="lewis";
            user.Email = "lewis@gmail.com";

          

            mockUserManager.Setup(fr => fr.FindByEmailAsync("lewis@gmail.com")).ReturnsAsync(new ApplicationUser());


            var result = _userRepository.Get("lewis@gmail.com").Result;


            Assert.That(result, Is.Null);


        }
    }
}
