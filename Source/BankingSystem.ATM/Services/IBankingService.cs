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
        bool ValidatePin(string bankCardNumber, string pin);

        /// <summary>
        ///     Changes the pin.
        /// </summary>
        /// <param name="bankCardNumber">The bank card number.</param>
        /// <param name="pin">The pin.</param>
        /// <param name="newPin">The new pin.</param>
        /// <returns>true if the pin has changed; otherwise false.</returns>
        bool ChangePin(string bankCardNumber, string pin, string newPin);

        /// <summary>
        ///     Withdraws the specified amount.
        /// </summary>
        /// <param name="bankCardNumber">The bank card number.</param>
        /// <param name="pin">The pin.</param>
        /// <param name="amount">The amount to withdraw.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <returns>true if operation has completed successfully; otherwise false.</returns>
        bool Withdraw(string bankCardNumber, string pin, decimal amount, out string errorMessage);

        /// <summary>
        ///     Gets the balance.
        /// </summary>
        /// <param name="bankCardNumber">The bank card number.</param>
        /// <param name="pin">The pin.</param>
        /// <returns>The current balance with currency.</returns>
        string GetBalance(string bankCardNumber, string pin);
    }
}