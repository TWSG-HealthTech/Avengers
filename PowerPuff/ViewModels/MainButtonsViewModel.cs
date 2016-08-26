using PowerPuff.Common;
using PowerPuff.Common.Helpers;
using Prism.Commands;
using Prism.Regions;

namespace PowerPuff.ViewModels
{
    public class MainButtonsViewModel : IRegionMemberLifetime
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
            _regionManager.RequestNavigate(RegionNames.MainContentRegion, NavigableViews.Main.SettingsView.GetFullName());
        }

        public bool KeepAlive { get; } = false;
    }
}
