using System.Threading.Tasks;

namespace BankingSystem.Domain
{
    /// <summary>
    ///     Defines a service of exchange rates.
    /// </summary>
    public interface IExchangeRateService
    {
        /// <summary>
        ///     Gets the exhange rate for the given pair of currencies.
        /// </summary>
        /// <param name="sourceCurrency">The source currency.</param>
        /// <param name="destCurrency">The dest currency.</param>
        /// <returns></returns>
        Task<decimal> GetExhangeRateAsync(string sourceCurrency, string destCurrency);
    }
}