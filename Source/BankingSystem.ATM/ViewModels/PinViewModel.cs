using System;
using System.Windows.Input;
using Prism.Commands;
using Prism.Regions;

namespace BankingSystem.ATM.ViewModels
{
    public class PinViewModel
    {
        private readonly IRegionManager _regionManager;

        public PinViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            OkCommand = new DelegateCommand<string>(CheckPin);
        }

        public ICommand OkCommand { get; }

        private void CheckPin(string pin)
        {
            _regionManager.RequestNavigate(RegionName.MainRegion, ViewName.ActionsView);
        }
    }
}