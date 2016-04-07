using Prism.Commands;
using Prism.Modularity;
using Prism.Regions;

namespace BankingSystem.ATM.ViewModels
{
    public class ShellViewModel
    {
        private readonly IRegionManager _regionManager;

        public ShellViewModel(IModuleManager moduleManager, IRegionManager regionManager)
        {
            _regionManager = regionManager;
            moduleManager.LoadModuleCompleted += delegate { NavigateToPinScreen(); };
            GlobalCommands.FinishCommand = new DelegateCommand(NavigateToPinScreen);
        }

        private void NavigateToPinScreen()
        {
            _regionManager.RequestNavigate(RegionName.MainRegion, ViewName.PinView);
        }
    }
}