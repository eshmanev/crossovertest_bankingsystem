using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BankingSystem.LogicTier.Impl
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
                // For more information login to https://currencylayer.com/dashboard
                // Credentials 
                //   email: evgeny.shmanev@aurea.com
                //   pass: testevgeny
                //   AccessKey = "53d94b7170f4fa9ed81dea1f28c28bf0"

                try
                {
                    client.BaseAddress = new Uri("http://www.apilayer.net/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.GetAsync($"api/live?access_key={AccessKey}&format=1&source={sourceCurrency}&currencies={destCurrency}");
                    response.EnsureSuccessStatusCode();

                    var content = await response.Content.ReadAsStringAsync();
                    var quotesResponse = JsonConvert.DeserializeObject<QuotesDto>(content);

                    var key = sourceCurrency + destCurrency;
                    return quotesResponse.Quotes[key];
                }
                catch (Exception ex)
                {
                    throw new BankingServiceException(
                        "Unexpected exception has occurred while getting exchange rates from http://www.apilayer.net/." +
                        "If you try to convert from non USD, please ensure that your have prepaid Basic Plan subscription.",
                        ex);
                }
               
            }
        }

        /// <summary>
        ///     Represents a DTO with quotes.
        /// </summary>
        private class QuotesDto
        {
            /// <summary>
            ///     Gets or sets the source currency.
            /// </summary>
            /// <value>
            ///     The source.
            /// </value>
            [JsonProperty("source")]
            public string Source { get; set; }

            /// <summary>
            ///     Gets or sets the quotes.
            /// </summary>
            /// <value>
            ///     The quotes.
            /// </value>
            [JsonProperty("quotes")]
            public Dictionary<string, decimal> Quotes { get; set; }
        }
    }
}