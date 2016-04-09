using BankingSystem.Domain;

namespace BankingSystem.DataTier.Repositories
{
    /// <summary>
    ///     Defines a repository of bank cards.
    /// </summary>
    public interface IBankCardRepository : IRepository<IBankCard>
    {
        /// <summary>
        ///     Searches for a card by number.
        /// </summary>
        /// <param name="cardNumber">The card number.</param>
        /// <returns>A bank card on null.</returns>
        IBankCard FindByNumber(string cardNumber);
    }
}