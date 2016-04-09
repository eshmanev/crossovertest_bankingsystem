using BankingSystem.Domain;

namespace BankingSystem.LogicTier
{
    /// <summary>
    ///     Defines a service of bank cards.
    /// </summary>
    public interface IBankCardService
    {
        /// <summary>
        ///     Checks the specified pair of bank card number and pin.
        /// </summary>
        /// <param name="bankCardNumber">The bank card number.</param>
        /// <param name="pinCode">The pin code.</param>
        /// <returns><c>true</c> if credentials are valid; otherwise false.</returns>
        bool CheckPin(string bankCardNumber, string pinCode);

        /// <summary>
        ///     Updates the pin of the specified bank card.
        /// </summary>
        /// <param name="bankCardNumber">The bank card number.</param>
        /// <param name="pinCode">The pin code.</param>
        /// <exception cref="System.ArgumentException">Bank card cannot be found</exception>
        void UpdatePin(string bankCardNumber, string pinCode);

        /// <summary>
        ///     Gets the balance.
        /// </summary>
        /// <param name="bankCardNumber">The bank card number.</param>
        /// <returns>A string representation of the card balance.</returns>
        /// <exception cref="System.ArgumentException">Bank card cannot be found</exception>
        string GetBalance(string bankCardNumber);

        /// <summary>
        ///     Searches for a bank card by number.
        /// </summary>
        /// <param name="bankCardNumber">The bank card number.</param>
        /// <returns>A bank card or null.</returns>
        IBankCard FindBankCard(string bankCardNumber);
    }
}