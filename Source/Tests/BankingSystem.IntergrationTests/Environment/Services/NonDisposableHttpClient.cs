using System.Net.Http;
using System.Threading.Tasks;
using BankingSystem.ATM.Services;

namespace BankingSystem.IntegrationTests.WebApiTests
{
    /// <summary>
    ///     This class prevents disposal of the HttpClient.
    /// </summary>
    /// <remarks>
    ///     It is usefull when the client is used in the following construction.
    ///     If the client is disposed it also disposed the HTTP server handler and all requests become unavailable.
    ///
    ///     using (var client = new HttpClient(_httpServerHandler))
    ///     {
    ///        // process the request 1
    ///     }
    /// 
    ///     using (var client = new HttpClient(_httpServerHandler))
    ///     {
    ///        // process the request 2
    ///     }
    /// </remarks>
    /// <seealso cref="BankingSystem.ATM.Services.IHttpClient" />
    public class NonDisposableHttpClient : IHttpClient
    {
        private readonly IHttpClient _client;

        public NonDisposableHttpClient(HttpClient client)
        {
            _client = new HttpClientWrapper(client);
        }

        public void Dispose()
        {
        }

        public Task<HttpResponseMessage> GetAsync(string url)
        {
            return _client.GetAsync(url);
        }

        public Task<HttpResponseMessage> PostAsync<T>(string url, T data)
        {
            return _client.PostAsync(url, data);
        }

        public Task<HttpResponseMessage> PutAsync<T>(string url, T data)
        {
            return _client.PutAsync(url, data);
        }

        public Task<HttpResponseMessage> DeleteAsync(string url)
        {
            return _client.DeleteAsync(url);
        }
    }
}