namespace BankingSystem.ATM.Services
{
    /// <summary>
    ///     Represents a fake implementation of the current bank card credentials provider.
    /// </summary>
    /// <remarks>
    ///     Normally it should be replaced with a real implementation which is connected to ATM hardware.
    ///     Now I'm just opening a separte window and simulating inseration of a bank card.
    /// </remarks>
    /// <seealso cref="ICredentialsProvider" />
    public class FakeCredentialsProvider : ICredentialsProvider
    {
        private FakeBankCardSlot _fakeSlot;

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
            if (_fakeSlot == null)
            {
                _fakeSlot = new FakeBankCardSlot();
                _fakeSlot.Show();
            }

            return _fakeSlot.SelectedCard;
        }
    }
}