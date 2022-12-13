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


        [SetUp]
        public void SetUp()
        {
          //  _userRepository = new UserRepository(mockUserManager.Object);
        }


        [Test]
        public void get_whenCalled_ReturnUser()
        {

            Mock<UserRepository> repository = new  Mock<UserRepository>();



        }

        [Test]
        public void register_whenCalled_CreateANewUser()
        {

        }

    }
}
