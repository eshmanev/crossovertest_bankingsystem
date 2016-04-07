using System;
using System.Windows.Input;
using BankingSystem.ATM.Services;
using Prism.Commands;
using Prism.Regions;

namespace BankingSystem.ATM.ViewModels
{
    /// <summary>
    ///     Represents a view model that shows the current balance.
    /// </summary>
    public class CurrentBalanceViewModel
    {
        private readonly ICredentialsProvider _provider;
        private readonly IBankingService _service;
        private readonly IDispatcherAccessor _dispatcherAccessor;

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
            RequestBalance();
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
        public string Balance { get; set; }

        /// <summary>
        ///     Requests the balance.
        /// </summary>
        private async void RequestBalance()
        {
            var result = await _service.GetBalance(_provider.GetBankCardNumber(), _provider.CurrentPin);
            _dispatcherAccessor.Dispatcher.Invoke(() => Balance = result);
        }
    }
}