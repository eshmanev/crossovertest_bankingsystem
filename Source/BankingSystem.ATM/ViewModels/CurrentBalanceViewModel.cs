using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using BankingSystem.ATM.Annotations;
using BankingSystem.ATM.Services;
using Prism;
using Prism.Commands;
using Prism.Regions;

namespace BankingSystem.ATM.ViewModels
{
    /// <summary>
    ///     Represents a view model that shows the current balance.
    /// </summary>
    public class CurrentBalanceViewModel : IActiveAware, INotifyPropertyChanged
    {
        private readonly ICredentialsProvider _provider;
        private readonly IBankingService _service;
        private readonly IDispatcherAccessor _dispatcherAccessor;
        private bool _isActive;
        private string _balance;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CurrentBalanceViewModel" /> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="service">The service.</param>
        /// <param name="dispatcherAccessor">The dispatcher accessor.</param>
        public CurrentBalanceViewModel(IRegionManager regionManager, ICredentialsProvider provider, IBankingService service, IDispatcherAccessor dispatcherAccessor)
        {
            if (regionManager == null) throw new ArgumentNullException(nameof(regionManager));
            if (provider == null) throw new ArgumentNullException(nameof(provider));
            if (service == null) throw new ArgumentNullException(nameof(service));
            if (dispatcherAccessor == null) throw new ArgumentNullException(nameof(dispatcherAccessor));

            _provider = provider;
            _service = service;
            _dispatcherAccessor = dispatcherAccessor;
            BackCommand = new DelegateCommand(() => regionManager.RequestNavigate(RegionName.MainRegion, ViewName.ActionsView));
        }

        /// <summary>
        ///     Gets the ok command.
        /// </summary>
        /// <value>
        ///     The ok command.
        /// </value>
        public ICommand BackCommand { get; }

        /// <summary>
        ///     Gets or sets the balance.
        /// </summary>
        /// <value>
        ///     The balance.
        /// </value>
        public string Balance
        {
            get { return _balance; }
            private set
            {
                _balance = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the view model is active.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if the object is active; otherwise <see langword="false" />.
        /// </value>
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                _isActive = value;
                if (_isActive)
                    RequestBalance();
            }
        }

        /// <summary>
        ///     Notifies that the value for <see cref="P:Prism.IActiveAware.IsActive" /> property has changed.
        /// </summary>
        public event EventHandler IsActiveChanged;

        /// <summary>
        ///     Requests the balance.
        /// </summary>
        private async void RequestBalance()
        {
            Balance = "Please wait for a while...";
            var result = await _service.GetBalance(_provider.GetBankCardNumber(), _provider.CurrentPin);
            _dispatcherAccessor.Dispatcher.Invoke(() => Balance = result);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}