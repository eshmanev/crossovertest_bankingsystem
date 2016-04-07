namespace BankingSystem.ATM.Services
{
    /// <summary>
    ///     Defines a provider of the current bank card credentials.
    /// </summary>
    public interface ICredentialsProvider
    {
        /// <summary>
        ///     Gets or sets the current pin entered by the user.
        /// </summary>
        /// <value>
        ///     The current pin.
        /// </value>
        string CurrentPin { get; set; }

        /// <summary>
        ///     Gets the number of the current bank card in ATM.
        /// </summary>
        /// <returns>The number of the bank card.</returns>
        string GetBankCardNumber();
    }
}