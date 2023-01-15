
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TalkToApiStudyTest;
using TalkToApiStudyTest.Helpers.Contants;

namespace TalktoApiTest.TestProject.IntegrationTests.Controllers
{

    [TestFixture]
    class UserControllerTests: CustomWebApplicationFactory<Program>
    {
        private  CustomWebApplicationFactory<Program> _factory;
        private  HttpClient _client;

        [SetUp]
        public void setUp()
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
    }
}
