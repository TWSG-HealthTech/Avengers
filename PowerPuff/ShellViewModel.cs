using System;
using PowerPuff.Common;
using PowerPuff.Common.Events;
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
        }

        private void NavigateTo(NavigationEventPayload payload)
        {
            _regionManager.RequestNavigate(RegionNames.MainContentRegion, payload.DestinationViewType.FullName);
        }
    }
}
