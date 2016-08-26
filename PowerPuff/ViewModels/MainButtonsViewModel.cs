using PowerPuff.Common;
using PowerPuff.Views;
using Prism.Commands;
using Prism.Regions;

namespace PowerPuff.ViewModels
{
    public class MainButtonsViewModel
    {
        private readonly IRegionManager _regionManager;

        public MainButtonsViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            GoToSettingsCommand = new DelegateCommand(GoToSettings);
        }

        public DelegateCommand GoToSettingsCommand { get; private set; }
        private void GoToSettings()
        {
            _regionManager.RequestNavigate(RegionNames.MainContentRegion, typeof(SettingsView).FullName);
        }
    }
}
