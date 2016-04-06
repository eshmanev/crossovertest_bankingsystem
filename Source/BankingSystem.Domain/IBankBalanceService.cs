namespace BankingSystem.Domain
{
    /// <summary>
    ///     Defines a service of bank balance.
    /// </summary>
    public interface IBankBalanceService
    {
        /// <summary>
        ///     Adds the revenue.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="description">The description.</param>
        void AddRevenue(decimal amount, string description);

        /// <summary>
        ///     Adds the expences.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="description">The description.</param>
        void AddExpences(decimal amount, string description);
    }
}