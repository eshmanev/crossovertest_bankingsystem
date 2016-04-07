using BankingSystem.ATM.Services;
using Prism.Commands;
using Prism.Modularity;
using Prism.Regions;

namespace BankingSystem.ATM.ViewModels
{
    /// <summary>
    ///     Represents a view model of the main shell.
    /// </summary>
    public class ShellViewModel
    {
        private readonly IRegionManager _regionManager;
        private readonly ICredentialsProvider _provider;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ShellViewModel" /> class.
        /// </summary>
        /// <param name="moduleManager">The module manager.</param>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="provider">The provider.</param>
        public ShellViewModel(IModuleManager moduleManager, IRegionManager regionManager, ICredentialsProvider provider)
        {
            _regionManager = regionManager;
            _provider = provider;
            moduleManager.LoadModuleCompleted += delegate { NavigateToPinScreen(); };
            GlobalCommands.FinishCommand = new DelegateCommand(NavigateToPinScreen);
        }

        /// <summary>
        ///     Navigates to the pin screen.
        /// </summary>
        private void NavigateToPinScreen()
        {
            _provider.CurrentPin = null;
            _regionManager.RequestNavigate(RegionName.MainRegion, ViewName.PinView);
        }
    }
}