using System.Net.Http;
using BankingSystem.ATM;
using BankingSystem.ATM.Services;
using BankingSystem.IntegrationTests.WebApiTests;

namespace BankingSystem.IntegrationTests.Environment.Services
{
    /// <summary>
    ///     Represents a banking service proxy for test environment.
    /// </summary>
    /// <seealso cref="BankingSystem.ATM.Services.BankingServiceProxy" />
    internal class TestBankingServiceProxy : BankingServiceProxy
    {
        private readonly HttpClient _client;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TestBankingServiceProxy" /> class.
        /// </summary>
        /// <param name="client">The client.</param>
        public TestBankingServiceProxy(HttpClient client) : base(new Settings())
        {
            _client = client;
        }

        /// <summary>
        ///     Creates the HTTP client.
        /// </summary>
        /// <returns></returns>
        protected override IHttpClient CreateClient()
        {
            return new NonDisposableHttpClient(_client);
        }
    }
}