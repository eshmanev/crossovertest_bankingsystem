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
    ///     Represents a view model which allows to enter a PIN code.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class PinViewModel : INotifyPropertyChanged
    {
        private readonly IRegionManager _regionManager;
        private readonly ICredentialsProvider _provider;
        private readonly IBankingService _service;
        private readonly IDispatcherAccessor _dispatcherAccessor;
        private string _errorMessage;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PinViewModel" /> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="service">The service.</param>
        /// <param name="dispatcherAccessor">The dispatcher accessor.</param>
        public PinViewModel(IRegionManager regionManager, ICredentialsProvider provider, IBankingService service, IDispatcherAccessor dispatcherAccessor)
        {
            if (regionManager == null) throw new ArgumentNullException(nameof(regionManager));
            if (provider == null) throw new ArgumentNullException(nameof(provider));
            if (service == null) throw new ArgumentNullException(nameof(service));
            if (dispatcherAccessor == null) throw new ArgumentNullException(nameof(dispatcherAccessor));

            _regionManager = regionManager;
            _provider = provider;
            _service = service;
            _dispatcherAccessor = dispatcherAccessor;
            OkCommand = new DelegateCommand<IWrappedValue<string>>(CheckPin);
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
        ///     Checks the pin.
        /// </summary>
        /// <param name="pin">The pin.</param>
        private async void CheckPin(IWrappedValue<string> pin)
        {
            var result = await _service.ValidatePin(_provider.GetBankCardNumber(), pin.Value);

            _dispatcherAccessor.Dispatcher.Invoke(() =>
            {
                if (result)
                {
                    // store entered value in context.
                    _provider.CurrentPin = pin.Value;

                    // reset entered value on UI
                    pin.Value = null;

                    // navigate to actions
                    _regionManager.RequestNavigate(RegionName.MainRegion, ViewName.ActionsView);

                    ErrorMessage = null;
                }
                else
                {
                    ErrorMessage = "Invalid PIN";
                }
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