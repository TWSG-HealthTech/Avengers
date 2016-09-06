using PowerPuff.Common;
using PowerPuff.Common.Helpers;
using Prism.Regions;

namespace PowerPuff.Features.Timer.Navigation
{
    public class TimerNavigator
    {
        private readonly IRegionManager _regionManager;
        private readonly IDispatcher _dispatcher;

        public TimerNavigator(IRegionManager regionManager, IDispatcher dispatcher)
        {
            _regionManager = regionManager;
            _dispatcher = dispatcher;
        }

        public void GoToTimerPage()
        {
            _dispatcher.Invoke(() =>
            {
                _regionManager.RequestNavigate(RegionNames.MainContentRegion,
                    NavigableViews.Main.FeatureLayoutView.GetFullName(),
                    result =>
                    {
                        _regionManager.RequestNavigate(RegionNames.FeatureMainContentRegion,
                            NavigableViews.Timer.MainView.GetFullName());
                    });
            });
        }
    }
}
