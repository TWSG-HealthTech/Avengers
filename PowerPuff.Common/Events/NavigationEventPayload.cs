using System;

namespace PowerPuff.Common.Events
{
    public class NavigationEventPayload : IEquatable<NavigationEventPayload>
    {
        public Type DestinationViewType { get; private set; }
        public string DestinationRegion { get; private set; }

        public NavigationEventPayload(string destinationRegion, Type destinationViewType)
        {
            DestinationRegion = destinationRegion;
            DestinationViewType = destinationViewType;
        }

        public bool Equals(NavigationEventPayload other)
        {
            return DestinationViewType == other.DestinationViewType &&
                   DestinationRegion == other.DestinationRegion;
        }

        public override bool Equals(object obj)
        {
            var other = obj as NavigationEventPayload;
            return other != null && Equals(other);
        }
    }
}
