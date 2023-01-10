
using NUnit.Framework;
using System;
using TalkToApiStudyTest.Helpers.Token;
using TalkToApiStudyTest.V1.Models;
using TalkToApiStudyTest.V1.Models.dto;

namespace TalktoApiTest.TestProject.UnitTesting.Token
{

    [TestFixture]
    class CreateTokenTests
    {


        [Test]
        public void buildToken_WhenCalled_ReturnTokenDto()
        {

            ApplicationUser user = new ApplicationUser();
            user = new ApplicationUser() { Email = "example@gmail.com", FullName = "Lewis", Id = "a1978bad-bb4f-426d-9a7e-7579d6226639" };
           TokenDTO token = CreateToken.BuildToken(user);

            Assert.NotNull(token);

        }

        [Test]
        public void buildToken_WhenCalled_AddEmptyApplicationThrowsArgumentNullException()
        {
            ApplicationUser user = new ApplicationUser();

            Assert.Throws<ArgumentNullException>(() =>  CreateToken.BuildToken(user));
        }

        [Test]
        public void buildToken_WhenCalled_AddEmptyEmailThrowsArgumentNullException()
        {
            ApplicationUser user = new ApplicationUser();
            user = new ApplicationUser() {  FullName = "Lewis", Id = "a1978bad-bb4f-426d-9a7e-7579d6226639" };

            Assert.Throws<ArgumentNullException>(() => CreateToken.BuildToken(user));
        }


        [Test]
        public void buildToken_WhenCalled_AddEmptyIdThrowsArgumentNullException()
        {
            ApplicationUser user = new ApplicationUser();
            user = new ApplicationUser() { Email = "example@gmail.com", FullName = "Lewis"};
            user.Id = "";

            Assert.Throws<ArgumentNullException>(() => CreateToken.BuildToken(user));
        }
    }
}
