using System.Windows.Input;
using Prism.Commands;
using Prism.Regions;

namespace BankingSystem.ATM.ViewModels
{
    /// <summary>
    ///     Represents a view model with available actions.
    /// </summary>
    public class ActionsViewModel
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ActionsViewModel" /> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        public ActionsViewModel(IRegionManager regionManager)
        {
            CheckBalanceCommand = new DelegateCommand(() => regionManager.RequestNavigate(RegionName.MainRegion, ViewName.CurrentBalanceView));
            WithdrawCacheCommand = new DelegateCommand(() => regionManager.RequestNavigate(RegionName.MainRegion, ViewName.WithdrawView));
            ChangePinCommand = new DelegateCommand(() => regionManager.RequestNavigate(RegionName.MainRegion, ViewName.ChangePinView));
        }

        /// <summary>
        ///     Gets the check balance command.
        /// </summary>
        /// <value>
        ///     The check balance command.
        /// </value>
        public ICommand CheckBalanceCommand { get; }

        /// <summary>
        ///     Gets the withdraw cache command.
        /// </summary>
        /// <value>
        ///     The withdraw cache command.
        /// </value>
        public ICommand WithdrawCacheCommand { get; }

        /// <summary>
        ///     Gets the change pin command.
        /// </summary>
        /// <value>
        ///     The change pin command.
        /// </value>
        public ICommand ChangePinCommand { get; }
    }
}