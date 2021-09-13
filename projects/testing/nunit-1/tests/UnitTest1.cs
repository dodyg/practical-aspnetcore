using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace Tests
{
    public class Tests
    {
        private WebApplicationFactory<PracticalAspNetCore.Startup> _factory;
        private HttpClient _client;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _factory = new WebApplicationFactory<PracticalAspNetCore.Startup>();
        }

        [SetUp]
        public void Setup()
        {
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task ReturnsTextStartingWithHelloWorld()
        {
            var result = await _client.GetStringAsync("/");

            Assert.That(result, Does.StartWith("Hello world"));
        }

        [Test]
        public async Task Returns200()
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, "/");

            using var response = await _client.SendAsync(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}