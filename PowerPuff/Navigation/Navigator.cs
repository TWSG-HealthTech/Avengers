using PowerPuff.Common;
using PowerPuff.Common.Helpers;
using PowerPuff.Common.Navigation;
using Prism.Regions;

namespace PowerPuff.Navigation
{
    public class Navigator : INavigator
    {
        private readonly IRegionManager _regionManager;
        private readonly IDispatcher _dispatcher;

        public Navigator(IRegionManager regionManager, IDispatcher dispatcher)
        {
            _regionManager = regionManager;
            _dispatcher = dispatcher;
        }

        public void GoToPage(string viewName)
        {
            _dispatcher.Invoke(() =>
            {
                if (_regionManager.Regions.ContainsRegionWithName(RegionNames.FeatureMainContentRegion))
                {
                    _regionManager.RequestNavigate(RegionNames.FeatureMainContentRegion,
                        viewName);
                }
                else
                {
                    _regionManager.RequestNavigate(RegionNames.MainContentRegion,
                        NavigableViews.Main.FeatureLayoutView.GetFullName(),
                        result =>
                        {
                            _regionManager.RequestNavigate(RegionNames.FeatureMainContentRegion,
                                viewName);
                        });
                }
            });
        }
    }
}
