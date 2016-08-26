using PowerPuff.Common;
using PowerPuff.Common.Helpers;
using Prism.Commands;
using Prism.Regions;

namespace PowerPuff.Features.Timer.ViewModels
{
    public class TimerMainViewModel
    {
        private readonly IRegionManager _regionManager;

        public TimerMainViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            GoBackCommand = new DelegateCommand(GoBack);
        }

        public DelegateCommand GoBackCommand { get; set; }

        private void GoBack()
        {
            _regionManager.RequestNavigate(RegionNames.MainContentRegion, NavigableViews.Main.HomeView.GetFullName());
        }
    }
}
