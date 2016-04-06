namespace BankingSystem.Domain
{
    /// <summary>
    ///     Represents a service of exchange rates.
    /// </summary>
    public class ExchangeRateService : IExchangeRateService
    {
        /// <summary>
        ///     Gets the exhange rate for the given pair of currencies.
        /// </summary>
        /// <param name="sourceCurrency">The source currency.</param>
        /// <param name="destCurrency">The dest currency.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public decimal GetExhangeRate(string sourceCurrency, string destCurrency)
        {
            throw new System.NotImplementedException();
        }
    }
}