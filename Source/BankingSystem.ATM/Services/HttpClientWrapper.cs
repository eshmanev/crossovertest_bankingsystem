using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BankingSystem.ATM.Services
{
    /// <summary>
    ///     Represents a wrapper of the <see cref="HttpClient" /> class.
    /// </summary>
    /// <seealso cref="BankingSystem.ATM.Services.IHttpClient" />
    public class HttpClientWrapper : IHttpClient
    {
        private readonly HttpClient _client;

        /// <summary>
        ///     Initializes a new instance of the <see cref="HttpClientWrapper" /> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public HttpClientWrapper(HttpClient client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            _client = client;
        }

        /// <summary>
        ///     Asynchronously executes POST HTTP request with the given data.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>
        ///     An HTTP response message.
        /// </returns>
        public Task<HttpResponseMessage> GetAsync(string url)
        {
            return _client.GetAsync(url);
        }

        /// <summary>
        ///     Asynchronously executes POST HTTP request with the given data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">The URL.</param>
        /// <param name="data">The data.</param>
        /// <returns>
        ///     An HTTP response message.
        /// </returns>
        public Task<HttpResponseMessage> PostAsync<T>(string url, T data)
        {
            var json = JsonConvert.SerializeObject(data);
            return _client.PostAsync(url, new StringContent(json, Encoding.Unicode, "application/json"));
        }

        /// <summary>
        ///     Asynchronously executes PUT HTTP request with the given data..
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">The URL.</param>
        /// <param name="data">The data.</param>
        /// <returns>
        ///     An HTTP response message.
        /// </returns>
        public Task<HttpResponseMessage> PutAsync<T>(string url, T data)
        {
            var json = JsonConvert.SerializeObject(data);
            return _client.PutAsync(url, new StringContent(json, Encoding.Unicode, "application/json"));
        }

        /// <summary>
        ///     Asynchronously executes PUT HTTP request with the given data..
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>
        ///     An HTTP response message.
        /// </returns>
        public Task<HttpResponseMessage> DeleteAsync(string url)
        {
            return _client.DeleteAsync(url);
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _client.Dispose();
        }
    }
}