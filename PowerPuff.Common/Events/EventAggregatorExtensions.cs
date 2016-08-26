using System;
using Prism.Events;

namespace PowerPuff.Common.Events
{
    public static class EventAggregatorExtensions
    {
        public static void PublishMainContentNavigationEvent(this IEventAggregator eventAggregator,
            Type destinationViewType)
        {
            eventAggregator
                .GetEvent<NavigationEvent>()
                .Publish(new NavigationEventPayload(RegionNames.MainContentRegion, destinationViewType));
        }

        public static SubscriptionToken SubscribeToMainContentNavigationEvent(this IEventAggregator eventAggregator,
            Action<NavigationEventPayload> action)
        {
            return eventAggregator
                .GetEvent<NavigationEvent>()
                .Subscribe(action, 
                           ThreadOption.PublisherThread, 
                           false, 
                           payload => payload.DestinationRegion == RegionNames.MainContentRegion);
        }
    }
}
