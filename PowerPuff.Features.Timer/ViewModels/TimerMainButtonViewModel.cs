using PowerPuff.Common;
using PowerPuff.Common.Helpers;
using Prism.Commands;
using Prism.Regions;

namespace PowerPuff.Features.Timer.ViewModels
{
    public class TimerMainButtonViewModel
    {
        private readonly IRegionManager _regionManager;

        public TimerMainButtonViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            GoToTimerPageCommand = new DelegateCommand(GoToTimerPage);
        }

        public DelegateCommand GoToTimerPageCommand { get; private set; }

        private void GoToTimerPage()
        {
            _regionManager.RequestNavigate(RegionNames.MainContentRegion, NavigableViews.Main.FeatureLayoutView.GetFullName(),
                result =>
                {
                    _regionManager.RequestNavigate(RegionNames.FeatureMainContentRegion, NavigableViews.Timer.MainView.GetFullName());
                });
        }
    }
}
