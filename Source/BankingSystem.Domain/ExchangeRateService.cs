using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BankingSystem.Domain
{
    /// <summary>
    ///     Represents a service of exchange rates.
    /// </summary>
    public class ExchangeRateService : IExchangeRateService
    {
        public const string AccessKey = "53d94b7170f4fa9ed81dea1f28c28bf0";

        /// <summary>
        ///     Gets the exhange rate for the given pair of currencies.
        /// </summary>
        /// <param name="sourceCurrency">The source currency.</param>
        /// <param name="destCurrency">The dest currency.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<decimal> GetExhangeRateAsync(string sourceCurrency, string destCurrency)
        {
            using (var client = new HttpClient())
            {
                // Login to https://currencylayer.com/dashboard
                // Credentials 
                //   email: evgeny.shmanev@aurea.com
                //   pass: testevgeny
                client.BaseAddress = new Uri("http://www.apilayer.net/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"api/live?access_key={AccessKey}&format=1&currencies={sourceCurrency},{destCurrency}");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var quotesResponse = JsonConvert.DeserializeObject<ApiResponse>(content);

                var key = sourceCurrency + destCurrency;
                return quotesResponse.Quotes[key];
            }
        }

        private class ApiResponse
        {
            [JsonProperty("source")]
            public string Source { get; set; }

            [JsonProperty("quotes")]
            public Dictionary<string, decimal> Quotes { get; set; } 
        }
    }
}