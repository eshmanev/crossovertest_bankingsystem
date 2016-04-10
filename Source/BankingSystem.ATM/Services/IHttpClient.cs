using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BankingSystem.ATM.Services
{
    /// <summary>
    ///     Defines an HTTP client.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IHttpClient : IDisposable
    {
        /// <summary>
        ///     Asynchronously executes POST HTTP request with the given data.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>An HTTP response message.</returns>
        Task<HttpResponseMessage> GetAsync(string url);

        /// <summary>
        ///     Asynchronously executes POST HTTP request with the given data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">The URL.</param>
        /// <param name="data">The data.</param>
        /// <returns>An HTTP response message.</returns>
        Task<HttpResponseMessage> PostAsync<T>(string url, T data);

        /// <summary>
        ///     Asynchronously executes PUT HTTP request with the given data..
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">The URL.</param>
        /// <param name="data">The data.</param>
        /// <returns>An HTTP response message.</returns>
        Task<HttpResponseMessage> PutAsync<T>(string url, T data);

        /// <summary>
        ///     Asynchronously executes PUT HTTP request with the given data..
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>An HTTP response message.</returns>
        Task<HttpResponseMessage> DeleteAsync(string url);
    }
}