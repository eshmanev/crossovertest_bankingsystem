using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BankingSystem.ATM.Services
{
    /// <summary>
    ///     Contains a set of HTTP extensions methods.
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        ///     Asynchronously oosts the given data..
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="client">The client.</param>
        /// <param name="url">The URL.</param>
        /// <param name="data">The data.</param>
        /// <returns>An HTTP response message.</returns>
        public static Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, string url, T data)
        {
            var json = JsonConvert.SerializeObject(data);
            return client.PostAsync(url, new StringContent(json));
        }

        /// <summary>
        ///     Reads the HTTP content to a specified type..
        /// </summary>
        /// <typeparam name="T">The result type.</typeparam>
        /// <param name="content">The content.</param>
        /// <returns>The result.</returns>
        public static async Task<T> ReadAs<T>(this HttpContent content)
        {
            var contentStr = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(contentStr);
        }
    }
}