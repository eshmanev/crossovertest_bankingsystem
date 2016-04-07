using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using BankingSystem.ATM.Services;
using Prism.Commands;

namespace BankingSystem.ATM.ViewModels
{
    /// <summary>
    ///     Represents a view model which allows to withdraw money.
    /// </summary>
    public class WithdrawViewModel : INotifyPropertyChanged
    {
        private readonly ICredentialsProvider _provider;
        private readonly IBankingService _service;
        private string _errorMessage;

        /// <summary>
        ///     Initializes a new instance of the <see cref="WithdrawViewModel" /> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="service">The service.</param>
        public WithdrawViewModel(ICredentialsProvider provider, IBankingService service)
        {
            _provider = provider;
            _service = service;
            GetCommand = new DelegateCommand(Withdraw);
        }

        /// <summary>
        ///     Gets the error message.
        /// </summary>
        /// <value>
        ///     The error message.
        /// </value>
        public string ErrorMessage
        {
            get { return _errorMessage; }
            private set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the amount.
        /// </summary>
        /// <value>
        ///     The amount.
        /// </value>
        public decimal Amount { get; set; }

        /// <summary>
        ///     Gets or sets the get command.
        /// </summary>
        /// <value>
        ///     The get command.
        /// </value>
        public ICommand GetCommand { get; }

        /// <summary>
        ///     Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Called when property has changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     Withdraws the specified cache.
        /// </summary>
        private void Withdraw()
        {
            if (Amount <= 0)
            {
                ErrorMessage = "Please specify an amount";
                Amount = 0;
                return;
            }

            string errorMessage;
            if (!_service.Withdraw(_provider.GetBankCardNumber(), _provider.CurrentPin, Amount, out errorMessage))
            {
                ErrorMessage = errorMessage;
                Amount = 0;
            }
            else
            {
                GlobalCommands.FinishCommand.Execute(null);
            }
        }
    }
}