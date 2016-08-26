using Prism.Events;

namespace PowerPuff.Common.Events
{
    public class NavigationEvent : PubSubEvent<NavigationEventPayload>
    {
    }

    public class HomeNavigationEvent : PubSubEvent
    {
    }
}
