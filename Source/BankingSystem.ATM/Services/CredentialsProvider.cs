namespace BankingSystem.ATM.Services
{
    /// <summary>
    ///     Represents a provider of the current bank card credentials.
    /// </summary>
    /// <seealso cref="ICredentialsProvider" />
    public class CredentialsProvider : ICredentialsProvider
    {
        /// <summary>
        ///     Gets or sets the current pin entered by the user.
        /// </summary>
        /// <value>
        ///     The current pin.
        /// </value>
        public string CurrentPin { get; set; }

        /// <summary>
        ///     Gets the number of the current bank card in ATM.
        /// </summary>
        /// <returns>
        ///     The number of the bank card.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public string GetBankCardNumber()
        {
            throw new System.NotImplementedException();
        }
    }
}