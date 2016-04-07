using System.Threading.Tasks;

namespace BankingSystem.ATM.Services
{
    /// <summary>
    ///     Defines a banking service.
    /// </summary>
    public interface IBankingService
    {
        /// <summary>
        ///     Validates the pin.
        /// </summary>
        /// <param name="bankCardNumber">The bank card number.</param>
        /// <param name="pin">The pin.</param>
        /// <returns>true if the pin is valid; otherwise false.</returns>
        Task<bool> ValidatePin(string bankCardNumber, string pin);

        /// <summary>
        ///     Changes the pin.
        /// </summary>
        /// <param name="bankCardNumber">The bank card number.</param>
        /// <param name="pin">The pin.</param>
        /// <param name="newPin">The new pin.</param>
        /// <returns>true if the pin has changed; otherwise false.</returns>
        Task<bool> ChangePin(string bankCardNumber, string pin, string newPin);

        /// <summary>
        ///     Withdraws the specified amount.
        /// </summary>
        /// <param name="bankCardNumber">The bank card number.</param>
        /// <param name="pin">The pin.</param>
        /// <param name="amount">The amount to withdraw.</param>
        /// <returns><c>null</c> if operation has completed successfully; otherwise an error message.</returns>
        Task<string> Withdraw(string bankCardNumber, string pin, decimal amount);

        /// <summary>
        ///     Gets the balance.
        /// </summary>
        /// <param name="bankCardNumber">The bank card number.</param>
        /// <param name="pin">The pin.</param>
        /// <returns>The current balance with currency.</returns>
        Task<string> GetBalance(string bankCardNumber, string pin);
    }
}