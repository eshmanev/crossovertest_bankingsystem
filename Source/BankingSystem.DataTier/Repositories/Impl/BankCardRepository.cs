using System.Linq;
using BankingSystem.Common.Data;

namespace BankingSystem.DataTier.Repositories.Impl
{
    /// <summary>
    ///     Represents a repository of bank cards.
    /// </summary>
    /// <seealso cref="IBankCardRepository" />
    public class BankCardRepository : Repository<IBankCard>, IBankCardRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BankCardRepository" /> class.
        /// </summary>
        /// <param name="databaseContext"></param>
        public BankCardRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        /// <summary>
        ///     Searches for a card by number.
        /// </summary>
        /// <param name="cardNumber">The card number.</param>
        /// <returns>
        ///     A bank card on null.
        /// </returns>
        public IBankCard FindByNumber(string cardNumber)
        {
            return base.Filter(x => x.CardNumber == cardNumber).FirstOrDefault();
        }
    }
}