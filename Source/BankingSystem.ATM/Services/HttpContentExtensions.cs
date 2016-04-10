using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BankingSystem.ATM.Services
{
    /// <summary>
    ///     Contains a set of extensions methods for the <see cref="HttpContent" /> class..
    /// </summary>
    public static class HttpContentExtensions
    {
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