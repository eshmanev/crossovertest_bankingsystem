using System;
using BankingSystem.DataTier;
using BankingSystem.Domain;

namespace BankingSystem.LogicTier.Impl
{
    /// <summary>
    ///     Represents a service of bank cards.
    /// </summary>
    /// <seealso cref="BankingSystem.LogicTier.IBankCardService" />
    public class BankCardService : IBankCardService
    {
        private readonly IDatabaseContext _databaseContext;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BankCardService" /> class.
        /// </summary>
        /// <param name="databaseContext">The database context.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public BankCardService(IDatabaseContext databaseContext)
        {
            if (databaseContext == null)
                throw new ArgumentNullException(nameof(databaseContext));

            _databaseContext = databaseContext;
        }

        /// <summary>
        ///     Checks the specified pair of bank card number and pin.
        /// </summary>
        /// <param name="bankCardNumber">The bank card number.</param>
        /// <param name="pinCode">The pin code.</param>
        /// <returns>
        ///     <c>true</c> if credentials are valid; otherwise false.
        /// </returns>
        public bool CheckPin(string bankCardNumber, string pinCode)
        {
            var card = _databaseContext.BankCards.FindByNumber(bankCardNumber);
            return card != null && card.PinCode == pinCode;
        }

        /// <summary>
        ///     Updates the pin of the specified bank card.
        /// </summary>
        /// <param name="bankCardNumber">The bank card number.</param>
        /// <param name="pinCode">The pin code.</param>
        /// <exception cref="System.ArgumentException">Bank card cannot be found</exception>
        public void UpdatePin(string bankCardNumber, string pinCode)
        {
            var card = _databaseContext.BankCards.FindByNumber(bankCardNumber);
            if (card == null)
                throw new ArgumentException("Bank card cannot be found", nameof(bankCardNumber));

            card.PinCode = pinCode;
            _databaseContext.BankCards.Update(card);
        }

        /// <summary>
        ///     Gets the balance.
        /// </summary>
        /// <param name="bankCardNumber">The bank card number.</param>
        /// <returns>
        ///     A string representation of the card balance.
        /// </returns>
        /// <exception cref="System.ArgumentException">Bank card cannot be found</exception>
        public string GetBalance(string bankCardNumber)
        {
            var card = _databaseContext.BankCards.FindByNumber(bankCardNumber);
            if (card == null)
                throw new ArgumentException("Bank card cannot be found", nameof(bankCardNumber));

            return $"{card.Account.Balance.ToString("N2")} {card.Account.Currency}";
        }

        /// <summary>
        ///     Searches for a bank card by number.
        /// </summary>
        /// <param name="bankCardNumber">The bank card number.</param>
        /// <returns>
        ///     A bank card or null.
        /// </returns>
        public IBankCard FindBankCard(string bankCardNumber)
        {
            return _databaseContext.BankCards.FindByNumber(bankCardNumber);
        }
    }
}