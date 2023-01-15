
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TalkToApiStudyTest;
using TalkToApiStudyTest.Helpers.Contants;
using TalkToApiStudyTest.V1.Models;

namespace TalktoApiTest.TestProject.IntegrationTests.Controllers
{

    [TestFixture]
    class UserControllerTests: CustomWebApplicationFactory<Program> , IDisposable
    {
        private  CustomWebApplicationFactory<Program> _factory;
        private  HttpClient _client;

        [SetUp]
        public void beforeEach()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            _client = _factory.CreateClient();

        }

        [Test]
        public async Task Get()
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://localhost:44334/api/message/edit"),
                Headers = {
            { HttpRequestHeader.Accept.ToString(), CustomMediaType.Hateoas },
                 { "api-version", "1" }
                 },
            };

            var response = _client.SendAsync(httpRequestMessage).Result;

            var message = await response.Content.ReadAsStringAsync();

            Assert.AreEqual("ola lewis", message.Substring(1, message.Length -2));
        }

        [Test]
        public async Task getById()
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://localhost:44334/api/user/e3169bc6-f433-4f97-9aed-93027cec38e0"),
                Headers = {
                    { HttpRequestHeader.Accept.ToString(), CustomMediaType.Hateoas },
                    { "api-version", "1" }
                },
            
            };

            var response = _client.SendAsync(httpRequestMessage).Result;
            var message =  response.Content.ReadAsStringAsync().Result;
            ApplicationUser user = JsonConvert.DeserializeObject<ApplicationUser>(message);

            Assert.AreEqual("lewiscarlos@gmail.com", user.Email);
        }

        [TearDown]
        public void afterEach()
        {

            _client.Dispose();
            _factory.Dispose();
        }
    }
}
