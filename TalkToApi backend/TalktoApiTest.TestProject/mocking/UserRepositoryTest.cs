using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using TalkToApiStudyTest.V1.Models;
using TalkToApiStudyTest.V1.Repositories;

namespace TalktoApiTest.mocking.TestProject
{

    [TestFixture]
   public class UserRepositoryTest
    {


        private readonly Mock<UserManager<ApplicationUser>> mockUserManager;
        private UserService _userRepository;


        [SetUp]
        public void SetUp()
        {
            _userRepository = new UserRepository(mockUserManager.Object);
        }


        [Test]
        public void get_whenCalled_ReturnUser()
        {

            ApplicationUser user = new ApplicationUser();

            user.UserName ="lewis";
            user.Email = "lewis@gmail.com";
                


            mockUserManager.Setup(fr => fr.FindByEmailAsync("lewis@gmail.com")).ReturnsAsync(user);


            var result = _userRepository.Get("lewis@gmail.com").Result;


            Assert.That(result, Is.EqualTo(user));



        }

        [Test]
        public void register_whenCalled_CreateANewUser()
        {

        }

    }
}
