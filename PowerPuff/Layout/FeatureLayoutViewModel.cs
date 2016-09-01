using PowerPuff.Common;
using PowerPuff.Common.Helpers;
using Prism.Commands;
using Prism.Regions;

namespace PowerPuff.Layout
{
    public class FeatureLayoutViewModel : IRegionMemberLifetime
    {
        private readonly IRegionManager _regionManager;

        public FeatureLayoutViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            GoBackCommand = new DelegateCommand(GoBack);
        }

        public DelegateCommand GoBackCommand { get; set; }

        private void GoBack()
        {
            _regionManager.RequestNavigate(RegionNames.MainContentRegion, NavigableViews.Main.HomeView.GetFullName());
        }

        public bool KeepAlive { get; } = false;
    }
}
