namespace BankingSystem.ATM.Services
{
    /// <summary>
    ///     Represents a banking service.
    /// </summary>
    /// <seealso cref="IBankingService" />
    public class BankingServiceProxy : IBankingService
    {
        /// <summary>
        ///     Validates the pin.
        /// </summary>
        /// <param name="bankCardNumber">The bank card number.</param>
        /// <param name="pin">The pin.</param>
        /// <returns>
        ///     true if the pin is valid; otherwise false.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool ValidatePin(string bankCardNumber, string pin)
        {
            return false;
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
        /// <exception cref="System.NotImplementedException"></exception>
        public bool ChangePin(string bankCardNumber, string pin, string newPin)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        ///     Withdraws the specified amount.
        /// </summary>
        /// <param name="bankCardNumber">The bank card number.</param>
        /// <param name="pin">The pin.</param>
        /// <param name="amount">The amount to withdraw.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <returns>
        ///     true if operation has completed successfully; otherwise false.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool Withdraw(string bankCardNumber, string pin, decimal amount, out string errorMessage)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        ///     Gets the balance.
        /// </summary>
        /// <param name="bankCardNumber">The bank card number.</param>
        /// <param name="pin">The pin.</param>
        /// <returns>
        ///     The current balance.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public string GetBalance(string bankCardNumber, string pin)
        {
            throw new System.NotImplementedException();
        }
    }
}