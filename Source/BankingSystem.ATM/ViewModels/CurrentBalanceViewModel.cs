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

        /// <summary>
        ///     Initializes a new instance of the <see cref="PinViewModel" /> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="service">The service.</param>
        public CurrentBalanceViewModel(IRegionManager regionManager, ICredentialsProvider provider, IBankingService service)
        {
            _provider = provider;
            _service = service;
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
        private void RequestBalance()
        {
            Balance = _service.GetBalance(_provider.GetBankCardNumber(), _provider.CurrentPin);
        }
    }
}