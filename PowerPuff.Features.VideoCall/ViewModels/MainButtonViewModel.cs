using PowerPuff.Common;
using PowerPuff.Common.Helpers;
using Prism.Commands;
using Prism.Regions;

namespace PowerPuff.Features.VideoCall.ViewModels
{
    public class MainButtonViewModel
    {
        private readonly IRegionManager _regionManager;

        public MainButtonViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            GoToVideoPageCommand = new DelegateCommand(GoToVideoPage);
        }

        public DelegateCommand GoToVideoPageCommand { get; private set; }

        private void GoToVideoPage()
        {
            _regionManager.RequestNavigate(RegionNames.MainContentRegion, NavigableViews.Main.FeatureLayoutView.GetFullName(),
                result =>
                {
                    _regionManager.RequestNavigate(RegionNames.FeatureMainContentRegion, NavigableViews.VideoCall.MainView.GetFullName());
                });
        }
    }
}
