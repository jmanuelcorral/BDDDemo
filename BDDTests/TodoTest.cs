using System.Net.Http;

namespace BDDTests
{
    using BDDDemo;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using System;
    using System.Threading.Tasks;
    using TestStack.BDDfy;
    using Xunit;

    [Story(
        AsA = "registered User",
        IWant = "I want to load a generic todo list",
        SoThat = "Check what is my next job")]
    public class TodoTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        private string _userId;
        private string _response;
        private string _expectedResponse;

        public TodoTest()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();
        }


        [Fact]
        public void FindCriteria()
        {
            this.Given(x => AValidUserId())
                .When(x => IConnectToRestApiAndInvokeTheEndpoint())
                .Then(x => ICheckIHaveTodos())
                .BDDfy<TodoTest>();
        }

        [Fact]
        public void FindWithoutCriteria()
        {
            this.Given(x => AnonymousUser())
                .When(x => IConnectToRestApiAndInvokeTheEndpoint())
                .Then(x => ICheckIDontHaveTodos())
                .BDDfy<TodoTest>();
        }

        #region FindWithoutCriteria
        private void ICheckIDontHaveTodos()
        {
            Assert.Equal(_response, _expectedResponse);
        }

        private void AnonymousUser()
        {
            _userId = String.Empty;
            _expectedResponse = String.Empty;
        }
        #endregion

        #region FindCriteria
        private void ICheckIHaveTodos()
        {
            Assert.Equal(_response, _expectedResponse);
        }

        private async Task IConnectToRestApiAndInvokeTheEndpoint()
        {
            var request = $"api/todos/{_userId}";
            var response = await _client.GetAsync(request);
            response.EnsureSuccessStatusCode();
            _response = await response.Content.ReadAsStringAsync();
            Console.Write(_response);
        }

        private void AValidUserId()
        {
            _userId = "jose";
            _expectedResponse = "[\"Comprar el Pan\",\"value2\"]";
        }
        #endregion

    }
}
