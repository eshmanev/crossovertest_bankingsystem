using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BankingSystem.Messages;
using log4net;

namespace BankingSystem.ATM.Services
{
    /// <summary>
    ///     Represents a banking service.
    /// </summary>
    /// <seealso cref="IBankingService" />
    public class BankingServiceProxy : IBankingService
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (BankingServiceProxy));
        private const string BaseUrl = "api/bankcard/{0}/pin/{1}/{2}";
        private readonly ISettings _settings;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BankingServiceProxy" /> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public BankingServiceProxy(ISettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            _settings = settings;
        }

        /// <summary>
        ///     Validates the pin.
        /// </summary>
        /// <param name="bankCardNumber">The bank card number.</param>
        /// <param name="pin">The pin.</param>
        /// <returns>
        ///     true if the pin is valid; otherwise false.
        /// </returns>
        public async Task<bool> ValidatePin(string bankCardNumber, string pin)
        {
            using (var client = CreateClient())
            {
                try
                {
                    var response = await client.GetAsync(GetBaseUrl(bankCardNumber, pin));
                    response.EnsureSuccessStatusCode();
                    return true;
                }
                catch (HttpRequestException)
                {
                    return false;
                }
                catch (Exception ex)
                {
                    Log.Error($"An error occurred while validation PIN code. Bank card number: {bankCardNumber}", ex);
                    return false;
                }
            }
        }

        /// <summary>
        ///     Changes the pin.
        /// </summary>
        /// <param name="bankCardNumber">The bank card number.</param>
        /// <param name="pin">The pin.</param>
        /// <param name="newPin">The new pin.</param>
        /// <returns>
        ///     true if the pin has changed; otherwise false.
        /// </returns>
        public async Task<bool> ChangePin(string bankCardNumber, string pin, string newPin)
        {
            using (var client = CreateClient())
            {
                try
                {
                    var response = await client.PutAsync(GetBaseUrl(bankCardNumber, pin), new NewPinMessage {NewPin = newPin});
                    response.EnsureSuccessStatusCode();
                    return true;
                }
                catch (HttpRequestException)
                {
                    return false;
                }
                catch (Exception ex)
                {
                    Log.Error($"An error occurred while validation PIN code. Bank card number: {bankCardNumber}", ex);
                    return false;
                }
            }
        }

        /// <summary>
        ///     Withdraws the specified amount.
        /// </summary>
        /// <param name="bankCardNumber">The bank card number.</param>
        /// <param name="pin">The pin.</param>
        /// <param name="amount">The amount to withdraw.</param>
        /// <returns>
        ///     <c>null</c> if operation has completed successfully; otherwise an error message.
        /// </returns>
        public async Task<string> Withdraw(string bankCardNumber, string pin, decimal amount)
        {
            using (var client = CreateClient())
            {
                try
                {
                    var url = GetBaseUrl(bankCardNumber, pin, "balance");
                    var response = await client.PutAsync(url, new ChangeAmountMessage
                    {
                        // we should send a negative value for withdrawal.
                        Amount = -amount,
                        Description = $"ATM withdrawal. Amount: {amount}"
                    });

                    if (response.StatusCode == HttpStatusCode.Forbidden)
                    {
                        var error = await response.Content.ReadAs<HttpErrorDto>();
                        return error.Message;
                    }

                    response.EnsureSuccessStatusCode();
                    return null;
                }
                catch (HttpRequestException ex)
                {
                    Log.Error($"An HTTP error has occurred while withdrawing an amount, but server has not provided a message. Bank card number: {bankCardNumber}", ex);
                    return "Unexpected error";
                }
                catch (Exception ex)
                {
                    Log.Error($"An error occurred while withdrawing an amount. Bank card number: {bankCardNumber}", ex);
                    return "Unexpected error";
                }
            }
        }

        /// <summary>
        ///     Gets the balance.
        /// </summary>
        /// <param name="bankCardNumber">The bank card number.</param>
        /// <param name="pin">The pin.</param>
        /// <returns>
        ///     The current balance.
        /// </returns>
        public async Task<string> GetBalance(string bankCardNumber, string pin)
        {
            using (var client = CreateClient())
            {
                try
                {
                    var url = GetBaseUrl(bankCardNumber, pin, "balance");
                    var response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
                catch (HttpRequestException)
                {
                    return string.Empty;
                }
                catch (Exception ex)
                {
                    Log.Error($"An error occurred while withdrawing an amount. Bank card number: {bankCardNumber}", ex);
                    return "Unexpected error";
                }
            }
        }

        /// <summary>
        ///     Gets the base URL.
        /// </summary>
        /// <param name="bankCardNumber">The bank card number.</param>
        /// <param name="pin">The pin.</param>
        /// <param name="relativeUrl">The relative URL.</param>
        /// <returns></returns>
        private string GetBaseUrl(string bankCardNumber, string pin, string relativeUrl = null)
        {
            return string.Format(BaseUrl, bankCardNumber, pin, relativeUrl);
        }

        private HttpClient CreateClient()
        {
            var client = new HttpClient {BaseAddress = new Uri(_settings.WebApiHost)};
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}