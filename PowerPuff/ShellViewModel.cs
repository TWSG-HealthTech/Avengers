using PowerPuff.Common;
using PowerPuff.Common.Events;
using PowerPuff.Views;
using Prism.Events;
using Prism.Regions;

namespace PowerPuff
{
    public class ShellViewModel
    {
        private readonly IRegionManager _regionManager;

        public ShellViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            eventAggregator.SubscribeToMainContentNavigationEvent(NavigateTo);
            eventAggregator.GetEvent<HomeNavigationEvent>().Subscribe(BackToHome);
        }

        private void NavigateTo(NavigationEventPayload payload)
        {
            _regionManager.RequestNavigate(RegionNames.MainContentRegion, payload.DestinationViewType.FullName);
        }

        private void BackToHome()
        {
            _regionManager.RequestNavigate(RegionNames.MainContentRegion, typeof(MainButtonsView).FullName);
        }
    }
}
