using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
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
        private readonly IDispatcherAccessor _dispatcherAccessor;
        private string _errorMessage;

        /// <summary>
        ///     Initializes a new instance of the <see cref="WithdrawViewModel" /> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="service">The service.</param>
        /// <param name="dispatcherAccessor">The dispatcher accessor.</param>
        public WithdrawViewModel(ICredentialsProvider provider, IBankingService service, IDispatcherAccessor dispatcherAccessor)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));
            if (service == null) throw new ArgumentNullException(nameof(service));
            if (dispatcherAccessor == null) throw new ArgumentNullException(nameof(dispatcherAccessor));

            _provider = provider;
            _service = service;
            _dispatcherAccessor = dispatcherAccessor;
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
        private async void Withdraw()
        {
            if (Amount <= 0)
            {
                ErrorMessage = "Please specify an amount";
                Amount = 0;
                return;
            }

            // we should send a negative value for withdrawal.
            var errorMessage = await _service.Withdraw(_provider.GetBankCardNumber(), _provider.CurrentPin, -Amount);
            _dispatcherAccessor.Dispatcher.Invoke(() =>
            {
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    ErrorMessage = errorMessage;
                    Amount = 0;
                }
                else
                {
                    // This is just to notify user of the application. Normally we should not show any message boxes, 
                    // because hardware is reponsible for getting cache.
                    MessageBox.Show($"You have withdrown {Amount}. Now you will be redirected on the PIN screen.");

                    ErrorMessage = null;
                    Amount = 0;
                    GlobalCommands.FinishCommand.Execute(null);
                }
            });
        }
    }
}