using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using BankingSystem.ATM.Services;
using Prism.Commands;
using Prism.Regions;

namespace BankingSystem.ATM.ViewModels
{
    /// <summary>
    ///     Represents a view model that allows to change a PIN code.
    /// </summary>
    public class ChangePinViewModel
    {
        private readonly IRegionManager _regionManager;
        private readonly ICredentialsProvider _provider;
        private readonly IBankingService _service;
        private readonly IDispatcherAccessor _dispatcherAccessor;
        private string _errorMessage;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ChangePinViewModel" /> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="service">The service.</param>
        /// <param name="dispatcherAccessor">The dispatcher accessor.</param>
        /// <exception cref="System.ArgumentNullException">
        /// </exception>
        public ChangePinViewModel(IRegionManager regionManager, ICredentialsProvider provider, IBankingService service, IDispatcherAccessor dispatcherAccessor)
        {
            if (regionManager == null) throw new ArgumentNullException(nameof(regionManager));
            if (provider == null) throw new ArgumentNullException(nameof(provider));
            if (service == null) throw new ArgumentNullException(nameof(service));
            if (dispatcherAccessor == null) throw new ArgumentNullException(nameof(dispatcherAccessor));

            _regionManager = regionManager;
            _provider = provider;
            _service = service;
            _dispatcherAccessor = dispatcherAccessor;
            OkCommand = new DelegateCommand<IWrappedValue<string>>(ChangePin);
        }

        /// <summary>
        ///     Gets the ok command.
        /// </summary>
        /// <value>
        ///     The ok command.
        /// </value>
        public ICommand OkCommand { get; }

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
        ///     Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Changed the pin.
        /// </summary>
        /// <param name="newPin">The pin.</param>
        private async void ChangePin(IWrappedValue<string> newPin)
        {
            var result = await _service.ChangePin(_provider.GetBankCardNumber(), _provider.CurrentPin, newPin.Value);
            _dispatcherAccessor.Dispatcher.Invoke(
                () =>
                {
                    if (result)
                    {
                        // store in context
                        _provider.CurrentPin = newPin.Value;

                        // reset on UI
                        newPin.Value = null;

                        // navigate to actions
                        _regionManager.RequestNavigate(RegionName.MainRegion, ViewName.ActionsView);

                        ErrorMessage = null;
                    }
                    else
                        ErrorMessage = "PIN was not changed.";
                });
        }

        /// <summary>
        ///     Called when a property changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}