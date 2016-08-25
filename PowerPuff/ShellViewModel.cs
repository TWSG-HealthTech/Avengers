using System;
using PowerPuff.Common;
using PowerPuff.Common.Events;
using Prism.Events;
using Prism.Regions;

namespace PowerPuff
{
    public class ShellViewModel
    {
        private readonly IRegionManager _regoinManager;

        public ShellViewModel(IRegionManager regoinManager, IEventAggregator eventAggregator)
        {
            _regoinManager = regoinManager;
            eventAggregator.GetEvent<NavigationEvent>().Subscribe(NavigateTo);
        }

        private void NavigateTo(Type destinationPageType)
        {
            _regoinManager.RequestNavigate(RegionNames.MainContentRegion, destinationPageType.FullName);
        }
    }
}
